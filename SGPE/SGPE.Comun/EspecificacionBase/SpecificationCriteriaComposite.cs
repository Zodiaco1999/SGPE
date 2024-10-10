namespace SGPE.Comun.EspecificacionBase
{
    public abstract class SpecificationCriteriaComposite<TEntity>
         : SpecificationCriteria<TEntity>
         where TEntity : class
    {
        #region Properties

        /// <summary>
        /// Left side specification for this composite element
        /// </summary>
        public abstract ISpecificationCriteria<TEntity> LeftSideSpecification { get; }

        /// <summary>
        /// Right side specification for this composite element
        /// </summary>
        public abstract ISpecificationCriteria<TEntity> RightSideSpecification { get; }

        #endregion Properties
    }
}