using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;

namespace Nut.Core.Extensions
{
    public static class NutExpressionExtensions
    {
        public static string GetPropertyPath<TSource, TProperty>(Expression<Func<TSource, TProperty>> expression)
        {
            var unary = expression.Body as UnaryExpression;
            if (unary != null)
            {
                return GetPropertyPathFromExpression(unary.Operand);
            }

            return GetPropertyPathFromExpression(expression.Body);
        }

        public static string GetPropertyPathFromExpression(Expression expression)
        {
            Stack<string> buffer = null;

            while (true)
            {
                var memberExpression = expression as MemberExpression;
                if (memberExpression == null)
                {
                    break;
                }

                var thisExpression = memberExpression.Expression as MemberExpression;
                expression = thisExpression;

                if (thisExpression != null)
                {
                    if (buffer == null)
                    {
                        buffer = new Stack<string>();
                    }

                    buffer.Push(memberExpression.Member.Name);
                    continue;
                }

                var propertyInfo = memberExpression.Member as PropertyInfo;
                if (propertyInfo != null)
                {
                    if (buffer == null)
                    {
                        return propertyInfo.Name;
                    }

                    buffer.Push(propertyInfo.Name);
                }
            }

            return buffer == null ? null : string.Join(".", buffer);
        }

        public static PropertyInfo GetPropertyInfo<TSource, TProperty>(Expression<Func<TSource, TProperty>> propertyLambda)
        {
            var unary = propertyLambda.Body as UnaryExpression;
            if (unary != null)
            {
                return GetPropertyInfoFromExpression(unary.Operand);
            }

            return GetPropertyInfoFromExpression(propertyLambda.Body);
        }

        private static PropertyInfo GetPropertyInfoFromExpression(Expression expression)
        {
            var member = expression as MemberExpression;
            if (member != null)
            {
                var propInfo = member.Member as PropertyInfo;
                if (propInfo == null)
                {
                    throw new ArgumentException("Expression refers to a field, not a property.");
                }

                return propInfo;
            }

            throw new ArgumentException("Expression is not a MemberExpression");
        }
    }
}