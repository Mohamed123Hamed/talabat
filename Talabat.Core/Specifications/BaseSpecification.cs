using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talabat.Core.Specifications
{
    public class BaseSpecification<T> : ISpecification<T> where T : BaseEntity
    {
        //automaticlty property
        public Expression<Func<T, bool>> Criteria { get; set; } //where(p=>p.id==id)
            //return object===> include(p=>p.product..)
        public List<Expression<Func<T, object>>> Includes { get; set; }= new List<Expression<Func<T, object>>>();
        public Expression<Func<T, object>> OrderBy { get; set; }
        public Expression<Func<T, object>> OrderByDesc { get; set; }

        public BaseSpecification()
        {
            //Includes = new List<Expression<Func<T, object>>>(); 
        }
        public BaseSpecification(Expression<Func<T, bool>> criteriaExperession)
        {
            Criteria = criteriaExperession;
            //Includes = new List<Expression<Func<T, object>>>();
            
        }

        public void AddOrderBy(Expression<Func<T, object>> OrderbyExp)
        {
            OrderBy = OrderbyExp;
        }
        public void AddOrderByDesc(Expression<Func<T, object>> OrderbyDescExp)
        {
            OrderByDesc = OrderbyDescExp;
        }

    }
}
