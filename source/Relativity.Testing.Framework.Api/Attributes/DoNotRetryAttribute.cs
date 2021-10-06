using System;

namespace Relativity.Testing.Framework.Api.Attributes
{
	/// <summary>
	/// Use this to mark services and strategies which should not use retry mechanism.
	/// </summary>
	[AttributeUsage(AttributeTargets.Class)]
	internal class DoNotRetryAttribute : Attribute
	{
	}
}
