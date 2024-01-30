using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Talabat.APIs.Errors;
using Talabat.APIs.Extensions;
using Talabat.APIs.Helper;
using Talabat.APIs.Middlewares;
using Talabat.Core.Repositories;
using Talabat.Repository;
using Talabat.Repository.Data;

namespace Talabat.APIs
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            #region Cofigure Services

            builder.Services.AddControllers();
           
            builder.Services.AddSwaggerService(); // Confirm in ApplicationServicesExtension
            builder.Services.AddApplicationServices();//Confirm in ApplicationServicsExtensions
            
            //////////////
            builder.Services.AddDbContext<StoreContext>(Options => {
                Options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            }); 
            #endregion

            var app = builder.Build();

            #region Update Database
            using var Scope = app.Services.CreateScope();
            var Services  = Scope.ServiceProvider;
            var LoggerFactory = Services.GetRequiredService<ILoggerFactory>();
            try
            {
                var dbContext = Services.GetRequiredService<StoreContext>();//ASk CLR For Create Object
                await dbContext.Database.MigrateAsync(); //Update Database
                await StoreContextSeed.SeedAsync(dbContext);
            }
            catch (Exception ex)
            {
                var Logger = LoggerFactory.CreateLogger<Program>();
                Logger.LogError(ex,"An Error Occured During Appling The Migrations");
            }
            #endregion

            // Configure the HTTP request pipeline.
            #region Configure , Middleware
            app.UseMiddleware<ExceptionMiddleware>();   // add middleware server errorrs
            if (app.Environment.IsDevelopment())
            {
               app.UseSwaggerMiddlewares();
            }

            app.UseStatusCodePagesWithRedirects("/errors/{0}");  // not found end point
            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.UseStaticFiles();   /// use any file in wwwroot

            app.MapControllers(); 
            #endregion

            app.Run();
        }
    }
}