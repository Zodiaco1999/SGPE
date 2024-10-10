using System;
using System.Linq.Expressions;

namespace SGPE.Comun.EspecificacionBase
{
    public sealed class SpecificationCriteriaDirect<TEntity>
       : SpecificationCriteria<TEntity>
       where TEntity : class
    {
        #region Members

        private Expression<Func<TEntity, bool>> _MatchingCriteria;

        #endregion Members

        #region Constructor

        /// <summary>
        /// Default constructor for Direct Specification
        /// </summary>
        /// <param name="matchingCriteria">A Matching Criteria</param>
        public SpecificationCriteriaDirect(Expression<Func<TEntity, bool>> matchingCriteria)
        {
            if (matchingCriteria == (Expression<Func<TEntity, bool>>)null)
                throw new ArgumentNullException("matchingCriteria");

            _MatchingCriteria = matchingCriteria;
        }

        #endregion Constructor

        #region Override

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public override Expression<Func<TEntity, bool>> SatisfiedBy()
        {
            return _MatchingCriteria;
        }

        #endregion Override
    }
}