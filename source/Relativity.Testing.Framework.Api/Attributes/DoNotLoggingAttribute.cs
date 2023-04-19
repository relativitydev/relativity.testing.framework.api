using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Relativity.Testing.Framework.Api.Attributes
{
	/// <summary>
	/// Use this to mark services and strategies which should not use retry mechanism.
	/// </summary>
	[AttributeUsage(AttributeTargets.Class)]
	internal class DoNotLoggingAttribute : Attribute
	{
	}
}
