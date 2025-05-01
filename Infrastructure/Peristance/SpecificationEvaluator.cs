using DomainLayer.Contracts;
using DomainLayer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Peristance
{
    static class SpecificationEvaluator
    {
        //create Query
        public static IQueryable<TEntity>CreateQuery<TEntity,Tkey>(IQueryable<TEntity> InputQuery,ISpecifications<TEntity,Tkey>specifications) where TEntity : BaseEntity<Tkey>
        {
            var Query = InputQuery;
            if(specifications.Criteria is not null)
            {
                Query= Query.Where(specifications.Criteria);
            }

            if(specifications.OrderBy is not null)
            {
                Query=Query.OrderBy(specifications.OrderBy);
            }
            if(specifications.OrderByDescending is not null)
            {
                Query=Query.OrderByDescending(specifications.OrderByDescending);
            }

            if(specifications.IncludeExpression is not null && specifications.IncludeExpression.Count > 0)
            {
                //foreach(var exp in specifications.IncludeExpression)
                //{
                //    Query = Query.Include(exp);
                //}
                Query = specifications.IncludeExpression.Aggregate(Query, (CurrentQuery, IncludeExp) => CurrentQuery.Include(IncludeExp));
            }

            return Query;
        }
    }
}
