using System;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal class DefaultFieldValueDto<T>
	{
		public int ID { get; set; }

#pragma warning disable CA1720 // Identifier contains type name
		public Guid Guid { get; set; }
#pragma warning restore CA1720 // Identifier contains type name

		public T DefaultValue { get; set; }
	}
}
