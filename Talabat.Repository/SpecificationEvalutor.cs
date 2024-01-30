using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Specifications;

namespace Talabat.Repository
{
    public static class SpecificationEvalutor<TEntity> where TEntity : BaseEntity 
    {
        public static IQueryable<TEntity> GetQuery(IQueryable<TEntity> inputQuery, ISpecification<TEntity> spec)
        {
            var Query = inputQuery;
            // _dbcontext.products
            if (spec.Criteria is not null)
            {
                Query = Query.Where(spec.Criteria);
            }
            // _dbcontext.products
            if (spec.OrderBy is not null) 
            {
             Query=Query.OrderBy(spec.OrderBy);
            }
            // _dbcontext.products.orderby(p=>p.Name)
            if (spec.OrderByDesc is not null)        //p=>p.Name
            {
              Query = Query.OrderByDescending(spec.OrderByDesc);
            }
            
            //query = _dbcontext.products.where(p=>p.Id=1)

            Query = spec.Includes.Aggregate(Query, (currentQuery, includeExpression) => currentQuery.Include(includeExpression));
            return Query;
        }

    }
}
