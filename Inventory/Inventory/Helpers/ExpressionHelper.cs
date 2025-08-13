using Inventory.Models;
using System.Linq.Expressions;
using System.Reflection;

namespace Inventory.Helpers
{
    public class ExpressionHelper
    {
        static Expression<Func<T, bool>> CombineAnd<T>(params Expression<Func<T, bool>>[] predicates)
        {
            if (predicates == null || predicates.Length == 0)
            {
                return x => true;
            }
            Expression<Func<T, bool>> result = predicates[0];
            for (int i = 1; i < predicates.Length; i++)
            {
                var invokedExpr = Expression.Invoke(predicates[i], result.Parameters.Cast<Expression>());
                result = Expression.Lambda<Func<T, bool>>(Expression.AndAlso(result.Body, invokedExpr), result.Parameters);
            }
            return result;
        }

        static MethodInfo GetQueryableContainsMethodInfo()
        {
            return typeof(Enumerable)
            .GetMethods(BindingFlags.Static | BindingFlags.Public)
            .First(m => m.Name == "Contains" &&
                        m.GetParameters().Length == 2)
            .MakeGenericMethod(typeof(int));
        }


        static Expression<Func<T, bool>> CreateFilter<T>(IList<int> list, string par)
        {
            ParameterExpression parameter = Expression.Parameter(typeof(T), "x");
            MemberExpression property = Expression.PropertyOrField(parameter, par);
            MethodInfo containsMethod = GetQueryableContainsMethodInfo();
            ConstantExpression constant = Expression.Constant(list);
            MethodCallExpression containsCall = Expression.Call(containsMethod, constant, property);
            Expression<Func<T, bool>> lambda = Expression.Lambda<Func<T, bool>>(containsCall, parameter);
            return lambda;
        }

        public static Expression<Func<T, bool>> GetFilter<T>(BalanceFilter filter) => filter switch
        {
            { resources: IList<int> { Count: 0 }, units: IList<int> { Count: > 0 } } => CreateFilter<T>(filter.units, "unitofmeasurementid"),
            { resources: IList<int> { Count: > 0 }, units: IList<int> { Count: 0 } } => CreateFilter<T>(filter.resources, "resourceid"),
            { resources: IList<int> { Count: 0 }, units: IList<int> { Count: 0 } } => u => true,
            _ => CombineAnd(CreateFilter<T>(filter.resources, "resourceid"), CreateFilter<T>(filter.units, "unitofmeasurementid"))
        };
    }
}
