using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Relativity.Testing.Framework.Api.Querying
{
	internal abstract class QueryRequestBase : IQueryRequest
	{
		[JsonIgnore]
		public virtual int Start { get; set; }

		[JsonIgnore]
		public virtual int Length { get; set; } = int.MaxValue;

		public string Condition { get; set; }

		public void SetLength(int length)
		{
			Length = length;
		}

		public void AddCondition(string rowCondition)
		{
			if (!string.IsNullOrWhiteSpace(Condition))
			{
				Condition += $" AND {rowCondition}";
			}
			else
			{
				Condition = rowCondition;
			}
		}

		public void AddCondition(string fieldName, object fieldValue)
		{
			string conditionOperator = fieldValue is IEnumerable && !(fieldValue is string) ? "IN" : "==";

			string valueAsString = StringifyValueForCondition(fieldValue);
			AddCondition($"'{fieldName}' {conditionOperator} {valueAsString}");
		}

		private static string StringifyValueForCondition(object value)
		{
			if (value is IEnumerable enumerableValue && !(value is string))
			{
				var conditionBody = string.Join(",", enumerableValue.Cast<object>().Select(x => $"'{x}'"));
				return $"[{conditionBody}]";
			}
			else if (value is int)
			{
				return value.ToString();
			}
			else
			{
				return $"'{value}'";
			}
		}

		public abstract void SetFieldsToFetch(IEnumerable<string> fieldNames);
	}
}
