// --------------------------------------------------------------------------------------------------------------------
// <copyright file="QueryableExtensions.cs" company="koshin">
//   Copyright (c) 2011, koshin
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ClickpointAuto.Web.Models.Core.Linq
{
    using System;
    using System.ComponentModel;
    using System.Linq;
    using System.Linq.Expressions;

    public enum SortDirection
    {
        Ascending,
        Descending,
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    internal static class QueryableExtensions
    {
        public static IQueryable<TSource> Page<TSource>(this IQueryable<TSource> source, int page, int size) where TSource : class
        {
            CheckSource(source);

            if (page < 0)
            {
                throw new ArgumentOutOfRangeException("page");
            }

            if (size <= 0)
            {
                throw new ArgumentOutOfRangeException("size");
            }

            int index = GetIndex(page, size);

            return source.Skip(index).Take(size);
        }

        public static TSource SelectScalar<TSource>(this IQueryable<TSource> source, int value, string propertyName = "id") where TSource : class
        {
            CheckSource(source);

            if (value < 0)
            {
                throw new ArgumentOutOfRangeException("value");
            }

            CheckNullOrEmpty(propertyName);

            ParameterExpression parameterExpression = Expression.Parameter(source.ElementType);

            MemberExpression left = Expression.Property(parameterExpression, propertyName);
            Expression right = Expression.Constant(value);
            BinaryExpression binaryExpression = Expression.Equal(left, right);

            var exp = Expression.Lambda<Func<TSource, bool>>(binaryExpression, new[] { parameterExpression });

            return source.FirstOrDefault(exp);
        }

        public static IQueryable<TSource> OrderBy<TSource>(this IQueryable<TSource> source, string propertyName, SortDirection direction = SortDirection.Ascending)
        {
            CheckSource(source);
            CheckNullOrEmpty(propertyName);

            string func = "OrderBy";

            if (direction == SortDirection.Descending)
            {
                func = "OrderByDescending";
            }

            ParameterExpression parameterExpression = Expression.Parameter(source.ElementType);

            MemberExpression propertyExpression = Expression.PropertyOrField(parameterExpression, propertyName);

            LambdaExpression selector = Expression.Lambda(propertyExpression, parameterExpression);

            MethodCallExpression orderByCallExpression = Expression.Call(typeof(Queryable), func, new[] { source.ElementType, propertyExpression.Type }, source.Expression, selector);

            return source.Provider.CreateQuery<TSource>(orderByCallExpression);
        }

        private static void CheckNullOrEmpty(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentException("propertyName");
            }
        }

        private static void CheckSource(object source)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }
        }

        private static int GetIndex(int page, int size)
        {
            return page * size;
        }
    }
}