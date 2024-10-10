using System;
using System.Linq.Expressions;

namespace SGPE.Comun.EspecificacionBase
{
    public class SpecificationBase<T> : ISpecification<T> where T : class
    {
        protected SpecificationBase()
        {
        }

        public SpecificationBase(Expression<Func<T, bool>> criteria)
        {
            Criteria = criteria;
        }

        //protected SpecificationBase(int? pagina = null, int? registrosPorPagina = null, string ordenarPor = null, string direccionOrdenamiento = "asc", ISpecificationCriteria<T> especificacion = null)
        //{
        //    if (especificacion != null) Criteria = especificacion.SatisfiedBy();

        //    if (pagina != null) ApplyPaging(pagina.Value, registrosPorPagina.Value);

        //    if (ordenarPor != null) ApplyOrderBy(ordenarPor, direccionOrdenamiento);
        //}

        public Expression<Func<T, bool>> Criteria { get; set; }
        //public List<Expression<Func<T, object>>> Includes { get; } = new List<Expression<Func<T, object>>>();
        //public List<string> IncludeStrings { get; } = new List<string>();
        //public Expression<Func<T, object>> OrderBy { get; private set; }
        //public Expression<Func<T, object>> OrderByDescending { get; private set; }
        //public Expression<Func<T, object>> GroupBy { get; private set; }

        //public int Take { get; private set; }
        //public int Skip { get; private set; }
        //public bool IsPagingEnabled { get; private set; } = false;
        //public string PropertyOrderBy { get; private set; }
        //public string SortDirection { get; private set; }

        //public virtual void AddCriteria(Expression<Func<T, bool>> criteria)
        //{
        //    Criteria = criteria;
        //}

        //public virtual void AddInclude(Expression<Func<T, object>> includeExpression)
        //{
        //    Includes.Add(includeExpression);
        //}
        //public virtual void AddInclude(string includeString)
        //{
        //    IncludeStrings.Add(includeString);
        //}
        //protected virtual void ApplyPaging(int page, int pageSize)
        //{
        //    Skip = (page - 1) * pageSize;
        //    Take = pageSize;
        //    IsPagingEnabled = true;
        //}

        ////protected virtual void ApplyPaging(int skip, int take)
        ////{
        ////    Skip = skip;
        ////    Take = take;
        ////    IsPagingEnabled = true;
        ////}

        //protected virtual void ApplyOrderBy(Expression<Func<T, object>> orderByExpression)
        //{
        //    OrderBy = orderByExpression;
        //}

        //protected virtual void ApplyOrderBy(string propertyOrderBy, string sortDirection)
        //{
        //    PropertyOrderBy = propertyOrderBy;
        //    SortDirection = sortDirection;
        //}

        //protected virtual void ApplyOrderByDescending(Expression<Func<T, object>> orderByDescendingExpression)
        //{
        //    OrderByDescending = orderByDescendingExpression;
        //}

        ////Not used anywhere at the moment, but someone requested an example of setting this up.
        //protected virtual void ApplyGroupBy(Expression<Func<T, object>> groupByExpression)
        //{
        //    GroupBy = groupByExpression;
        //}
    }
}