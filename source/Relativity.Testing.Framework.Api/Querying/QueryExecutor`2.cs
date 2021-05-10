using System;
using System.Collections.Generic;

namespace Relativity.Testing.Framework.Api.Querying
{
	internal class QueryExecutor<TObject, TQueryRequest> : IQueryExecutor<TObject>
		where TQueryRequest : IQueryRequest
	{
		private readonly Func<TQueryRequest, IEnumerable<TObject>> _function;

		public QueryExecutor(Func<TQueryRequest, IEnumerable<TObject>> function)
		{
			if (function == null)
			{
				throw new ArgumentNullException(nameof(function));
			}

			_function = function;
		}

		public IEnumerable<TObject> Execute(IQueryRequest request)
		{
			return _function.Invoke((TQueryRequest)request);
		}
	}
}
