using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talabat.Core.Specifications
{
    public interface ISpecification<T> where T : BaseEntity
    {
        // signature property
        public Expression<Func<T,bool>> Criteria { get; set; } // where(p=>p.id)
        public List<Expression<Func<T,object>>> Includes { get; set; } //include(p=>p,product..)
       
        //////  Sorting
        public Expression<Func <T,object>> OrderBy { get; set; }
        public Expression<Func<T, object>> OrderByDesc { get; set; }


    }
}
