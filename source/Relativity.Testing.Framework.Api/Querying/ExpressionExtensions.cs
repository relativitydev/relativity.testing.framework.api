using System;
using System.Linq.Expressions;
using System.Reflection;

namespace Relativity.Testing.Framework.Api.Querying
{
	internal static class ExpressionExtensions
	{
		internal static string ExtractMemberName(this Expression expression)
		{
			return expression.ExtractMember().Name;
		}

		internal static MemberInfo ExtractMember(this Expression expression)
		{
			if (expression == null)
			{
				throw new ArgumentNullException(nameof(expression));
			}

			if (expression is LambdaExpression lambdaExpression)
			{
				return ExtractMember(lambdaExpression.Body);
			}
			else if (expression is MemberExpression memberExpression)
			{
				return memberExpression.Member;
			}
			else if (expression is UnaryExpression unaryExpression)
			{
				return ExtractMember(unaryExpression.Operand);
			}
			else
			{
				throw new ArgumentException("Inappropriate expression kind.", nameof(expression));
			}
		}
	}
}
