using DomainLayer.Contracts;
using DomainLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Service.Specifications
{
    abstract class BaseSpecifications<TEntity, Tkey> : ISpecifications<TEntity, Tkey> where TEntity : BaseEntity<Tkey>
    {
        protected BaseSpecifications(Expression<Func<TEntity, bool>>? ExCriteria)
        {
            Criteria = ExCriteria;
        }
        public Expression<Func<TEntity, bool>>? Criteria { get; private set; }

        public List<Expression<Func<TEntity, object>>> IncludeExpression { get; } = [];

        protected void AddInclude(Expression<Func<TEntity, object>> includeExpression)=>IncludeExpression.Add(includeExpression);
        
    }
}
