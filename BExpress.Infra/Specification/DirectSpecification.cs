using System;
using System.Linq.Expressions;

namespace BExpress.Infra.Specification
{
    public sealed class DirectSpecification<TEntity> : Specification<TEntity> where TEntity : class
    {
        Expression<Func<TEntity, bool>> _MatchingCriteria;

        public DirectSpecification()
        {
            _MatchingCriteria = m => true;
        }

        public DirectSpecification(Expression<Func<TEntity, bool>> matchingCriteria)
        {
            if (matchingCriteria == (Expression<Func<TEntity, bool>>)null)
                throw new ArgumentNullException("matchingCriteria");

            _MatchingCriteria = matchingCriteria;
        }

        public override Expression<Func<TEntity, bool>> SatisfiedBy()
        {
            return _MatchingCriteria;
        }
    }
}
