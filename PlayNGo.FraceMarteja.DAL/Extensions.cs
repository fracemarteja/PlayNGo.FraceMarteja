using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PlayNGo.FraceMarteja.DAL
{
    public static class Extensions
    {
        public static IQueryable<T> Where<T>(this IQueryable<T> source, string propertyName, object value)
        {
            return (IQueryable<T>)Where((IQueryable)source, propertyName, value);
        }

        public static IQueryable Where(this IQueryable source, string propertyName, object value)
        {
            var x = Expression.Parameter(source.ElementType, "x");
            var selector = Expression.Lambda(
                Expression.Equal(
                    Expression.PropertyOrField(x, propertyName),
                    Expression.Constant(value)
                ), x
            );

            return source.Provider.CreateQuery(
                Expression.Call(typeof(Queryable), "Where", new Type[] { source.ElementType }, source.Expression, selector)
            );
        }
    }
}
