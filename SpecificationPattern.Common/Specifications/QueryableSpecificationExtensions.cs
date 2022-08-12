using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace SpecificationPattern.Common.Specifications
{
    public static class QueryableSpecificationExtensions
    {
        public static IQueryable<TEntity> Where<TEntity>(this IQueryable<TEntity> query,
            ISpecification<TEntity> specification) where TEntity : class
        {
            specification.Includes
                .Aggregate(query,
                    (current, include) => current.Include(include));

            return query.Where(specification.Criteria);
        }
    }
}