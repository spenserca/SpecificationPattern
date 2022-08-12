using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace SpecificationPattern.Common.Specifications
{
    public abstract class SpecificationBase<T> : ISpecification<T>
    {
        public Expression<Func<T, bool>> Criteria { get; set; }
        public List<Expression<Func<T, object>>> Includes { get; } = new();
        
        /// <summary>
        /// This allows chaining a .Include on the IQueryable
        /// </summary>
        /// <param name="includeExpression"></param>
        protected virtual void AddInclude(Expression<Func<T, object>> includeExpression)
        {
            Includes.Add(includeExpression);
        }
    }
}