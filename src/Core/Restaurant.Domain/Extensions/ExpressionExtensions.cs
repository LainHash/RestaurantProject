using System.Linq.Expressions;

namespace Restaurant.Domain.Extensions
{
    public static class ExpressionExtensions
    {
        public static Expression<Func<T, bool>> And<T>(
            this Expression<Func<T, bool>> first,
            Expression<Func<T, bool>> second)
        {
            var parameter = Expression.Parameter(typeof(T));

            var firstBody = new ReplaceExpressionVisitor(
                first.Parameters[0],
                parameter
            ).Visit(first.Body);

            var secondBody = new ReplaceExpressionVisitor(
                second.Parameters[0],
                parameter
            ).Visit(second.Body);

            return Expression.Lambda<Func<T, bool>>(
                Expression.AndAlso(firstBody!, secondBody!),
                parameter
            );
        }

        public static Expression<Func<T, bool>> Or<T>(
            this Expression<Func<T, bool>> first,
            Expression<Func<T, bool>> second)
        {
            var parameter = Expression.Parameter(typeof(T));

            var firstBody = new ReplaceExpressionVisitor(
                first.Parameters[0],
                parameter
            ).Visit(first.Body);

            var secondBody = new ReplaceExpressionVisitor(
                second.Parameters[0],
                parameter
            ).Visit(second.Body);

            return Expression.Lambda<Func<T, bool>>(
                Expression.OrElse(firstBody!, secondBody!),
                parameter
            );
        }

        private sealed class ReplaceExpressionVisitor
            : ExpressionVisitor
        {
            private readonly Expression _oldExpression;
            private readonly Expression _newExpression;

            public ReplaceExpressionVisitor(
                Expression oldExpression,
                Expression newExpression)
            {
                _oldExpression = oldExpression;
                _newExpression = newExpression;
            }

            public override Expression Visit(Expression? node)
            {
                return node == _oldExpression
                    ? _newExpression
                    : base.Visit(node)!;
            }
        }
    }
}
