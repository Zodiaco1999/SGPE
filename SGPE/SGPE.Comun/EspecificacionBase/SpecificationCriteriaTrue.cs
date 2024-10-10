using System;
using System.Linq.Expressions;

namespace SGPE.Comun.EspecificacionBase
{
    public sealed class SpecificationCriteriaTrue<TEntity> : SpecificationCriteria<TEntity> where TEntity : class
    {
        #region Specification overrides

        /// <summary>
        /// <see cref=" Microsoft.Samples.NLayerApp.Domain.Seedwork.Specification.Specification{TEntity}"/>
        /// </summary>
        /// <returns><see cref=" Microsoft.Samples.NLayerApp.Domain.Seedwork.Specification.Specification{TEntity}"/></returns>
        public override System.Linq.Expressions.Expression<Func<TEntity, bool>> SatisfiedBy()
        {
            //Create "result variable" transform adhoc execution plan in prepared plan
            //for more info: http://geeks.ms/blogs/unai/2010/07/91/ef-4-0-performance-tips-1.aspx
            bool result = true;

            Expression<Func<TEntity, bool>> trueExpression = t => result;
            return trueExpression;
        }

        #endregion Specification overrides
    }
}