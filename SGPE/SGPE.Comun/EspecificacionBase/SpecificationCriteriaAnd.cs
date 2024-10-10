using System;
using System.Linq.Expressions;

namespace SGPE.Comun.EspecificacionBase
{
    public sealed class SpecificationCriteriaAnd<T> : SpecificationCriteriaComposite<T>
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
        public SpecificationCriteriaAnd(ISpecificationCriteria<T> leftSide, ISpecificationCriteria<T> rightSide)
        {
            if (leftSide == (ISpecificationCriteria<T>)null)
                throw new ArgumentNullException("leftSide");

            if (rightSide == (ISpecificationCriteria<T>)null)
                throw new ArgumentNullException("rightSide");

            this._LeftSideSpecification = leftSide;
            this._RightSideSpecification = rightSide;
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
        /// Right side specification
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

            return (left.And(right));
        }

        #endregion Composite Specification overrides
    }
}