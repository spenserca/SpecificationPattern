using System.Linq;

namespace SpecificationPattern.Specifications
{
    // TODO: where TEntity: BaseEntity
    public class SpecificationEvaluator<TEntity>
    {
        public static IQueryable<TEntity> GetQuery(IQueryable<TEntity> inputQuery,
            ISpecification<TEntity> specification)
        {
            var query = inputQuery;

            if (specification.Criteria != null)
            {
                query = query.Where(specification.Criteria);
            }

            return query;
        }
    }
}