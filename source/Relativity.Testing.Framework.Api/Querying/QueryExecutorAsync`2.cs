using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Relativity.Testing.Framework.Api.Querying
{
	internal class QueryExecutorAsync<TObject, TQueryRequestAsync> : IQueryExecutorAsync<TObject>
		where TQueryRequestAsync : IQueryRequest
	{
		private readonly Func<TQueryRequestAsync, Task<IEnumerable<TObject>>> _function;

		public QueryExecutorAsync(Func<TQueryRequestAsync, Task<IEnumerable<TObject>>> function)
		{
			if (function == null)
			{
				throw new ArgumentNullException(nameof(function));
			}

			_function = function;
		}

		public async Task<IEnumerable<TObject>> ExecuteAsync(IQueryRequest request)
		{
			return await _function.Invoke((TQueryRequestAsync)request).ConfigureAwait(false);
		}
	}
}
