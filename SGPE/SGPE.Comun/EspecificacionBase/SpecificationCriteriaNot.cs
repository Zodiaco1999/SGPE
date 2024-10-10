using System;
using System.Linq;
using System.Linq.Expressions;

namespace SGPE.Comun.EspecificacionBase
{
    public sealed class SpecificationCriteriaNot<TEntity>
       : SpecificationCriteria<TEntity>
       where TEntity : class
    {
        #region Members

        private Expression<Func<TEntity, bool>> _OriginalCriteria;

        #endregion Members

        #region Constructor

        /// <summary>
        /// Constructor for NotSpecificaiton
        /// </summary>
        /// <param name="originalSpecification">Original specification</param>
        public SpecificationCriteriaNot(ISpecificationCriteria<TEntity> originalSpecification)
        {
            if (originalSpecification == (ISpecificationCriteria<TEntity>)null)
                throw new ArgumentNullException("originalSpecification");

            _OriginalCriteria = originalSpecification.SatisfiedBy();
        }

        /// <summary>
        /// Constructor for NotSpecification
        /// </summary>
        /// <param name="originalSpecification">Original specificaiton</param>
        public SpecificationCriteriaNot(Expression<Func<TEntity, bool>> originalSpecification)
        {
            if (originalSpecification == (Expression<Func<TEntity, bool>>)null)
                throw new ArgumentNullException("originalSpecification");

            _OriginalCriteria = originalSpecification;
        }

        #endregion Constructor

        #region Override Specification methods

        /// <summary>
        /// <see cref="Microsoft.Samples.NLayerApp.Domain.Seedwork.Specification.ISpecification{TEntity}"/>
        /// </summary>
        /// <returns><see cref="Microsoft.Samples.NLayerApp.Domain.Seedwork.Specification.ISpecification{TEntity}"/></returns>
        public override Expression<Func<TEntity, bool>> SatisfiedBy()
        {
            return Expression.Lambda<Func<TEntity, bool>>(Expression.Not(_OriginalCriteria.Body),
                                                         _OriginalCriteria.Parameters.Single());
        }

        #endregion Override Specification methods
    }
}