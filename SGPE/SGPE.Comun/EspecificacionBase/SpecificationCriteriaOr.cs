using System;
using System.Linq.Expressions;

namespace SGPE.Comun.EspecificacionBase
{
    public sealed class SpecificationCriteriaOr<T>
          : SpecificationCriteriaComposite<T>
          where T : class
    {
        #region Members

        private ISpecificationCriteria<T> _RightSideSpecification = null;
        private ISpecificationCriteria<T> _LeftSideSpecification = null;

        #endregion Members

        #region Public Constructor

        /// <summary>
        /// Default constructor for AndSpecification
        /// </summary>
        /// <param name="leftSide">Left side specification</param>
        /// <param name="rightSide">Right side specification</param>
        public SpecificationCriteriaOr(ISpecificationCriteria<T> leftSide, ISpecificationCriteria<T> rightSide)
        {
            _LeftSideSpecification = leftSide ?? throw new ArgumentNullException("leftSide");
            _RightSideSpecification = rightSide ?? throw new ArgumentNullException("rightSide");
        }

        #endregion Public Constructor

        #region Composite Specification overrides

        /// <summary>
        /// Left side specification
        /// </summary>
        public override ISpecificationCriteria<T> LeftSideSpecification
        {
            get { return _LeftSideSpecification; }
        }

        /// <summary>
        /// Righ side specification
        /// </summary>
        public override ISpecificationCriteria<T> RightSideSpecification
        {
            get { return _RightSideSpecification; }
        }

        /// <summary>
        /// <see cref="Microsoft.Samples.NLayerApp.Domain.Seedwork.Specification.ISpecification{T}"/>
        /// </summary>
        /// <returns><see cref="Microsoft.Samples.NLayerApp.Domain.Seedwork.Specification.ISpecification{T}"/></returns>
        public override Expression<Func<T, bool>> SatisfiedBy()
        {
            Expression<Func<T, bool>> left = _LeftSideSpecification.SatisfiedBy();
            Expression<Func<T, bool>> right = _RightSideSpecification.SatisfiedBy();

            return (left.Or(right));
        }

        #endregion Composite Specification overrides
    }
}