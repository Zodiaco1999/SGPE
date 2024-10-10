using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using SGPE.Comun.Models;
using System.Linq.Expressions;
using System.Reflection;

namespace SGPE.Comun.Extensions;

public static class EFCoreExtensions
{

    public static IQueryable<TEntity> Query<TEntity>(this IQueryable<TEntity> source, Expression<Func<TEntity, bool>> query)
      where TEntity : class
    {
        return source.Provider.CreateQuery<TEntity>(query);
    }

    public static async Task<PagedResult<TEntity>> GetPagedResultAsync<TEntity>(this IQueryable<TEntity> source, int pageSize, int currentPage)
      where TEntity : class
    {
        var rows = source.Count();
        var results = await source
            .Skip(pageSize * (currentPage - 1))
            .Take(pageSize)
            .ToListAsync();

        return new PagedResult<TEntity>
        {
            CurrentPage = currentPage,
            PageCount = (int)Math.Ceiling((double)rows / pageSize),
            PageSize = pageSize,
            Results = results,
            RowsCount = rows
        };
    }

    public static IQueryable<TEntity> OrderBy<TEntity>(this IQueryable<TEntity> source, string orderByStrValues)
        where TEntity : class
    {
        var command = orderByStrValues.ToUpper().EndsWith("DESC") ? "OrderByDescending" : "OrderBy";
        var propertyName = orderByStrValues.Split(' ')[0].Trim();
        var properties = propertyName.Split(".");
        bool isSubProperty = properties.Length > 1;

        if (isSubProperty)
        {
            propertyName = properties[0];
        }

        var type = typeof(TEntity);
        var property = GetProperty(type, propertyName);

        if (property == null) return source;
        // p
        var parameter = Expression.Parameter(type, "p");
        // p.Precio
        var propertyAccess = Expression.MakeMemberAccess(parameter, property);
        // Crear expresión Lambda de ordenamiento
        var orderByExpression = Expression.Lambda(propertyAccess, parameter);

        var typeArguments = new Type[] { type, propertyAccess.Type };

        if (isSubProperty)
        {
            var subPropName = properties[1];
            var subProp = GetProperty(property.PropertyType, subPropName);
            if (subProp == null) return source;
            // p.Categoria.NombreCategoria
            var subPropertyAccess = Expression.MakeMemberAccess(propertyAccess, subProp);
            orderByExpression = Expression.Lambda(subPropertyAccess, parameter);
            typeArguments = new Type[] { type, subPropertyAccess.Type };
        }

        // Ejem. final: OrderBy(p => p.Precio) / OrderByDescending(p => p.Precio)
        var queryExpr = Expression.Call(
            type: typeof(Queryable),
            methodName: command,
            typeArguments,
            source.Expression,
            Expression.Quote(orderByExpression));

        return source.Provider.CreateQuery<TEntity>(queryExpr);
    }

    private static PropertyInfo? GetProperty(Type type, string propertyName)
    {
        return type.GetProperties()
            .Where(item => item.Name.ToLower() == propertyName.ToLower())
            .FirstOrDefault();
    }
}
