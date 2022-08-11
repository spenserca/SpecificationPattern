using System.Linq;

namespace SpecificationPattern.Specifications
{
    public static class QueryableSpecificationExtensions
    {
        public static IQueryable<TEntity> Where<TEntity>(this IQueryable<TEntity> query,
            ISpecification<TEntity> specification)
        {
            var queryableResultWithIncludes = specification.Includes
                .Aggregate(query,
                    (current, include) => current.Include(include));

            return query.Where(specification.Criteria);
        }
    }
}