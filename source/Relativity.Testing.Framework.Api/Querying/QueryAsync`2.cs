using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Relativity.Testing.Framework.Extensions;
using Relativity.Testing.Framework.Mapping;

namespace Relativity.Testing.Framework.Api.Querying
{
	/// <summary>
	/// Represents the base query class.
	/// </summary>
	/// <typeparam name="TObject">The type of the object.</typeparam>
	/// <typeparam name="TQuery">The type of the query.</typeparam>
	public abstract class QueryAsync<TObject, TQuery> : IA­syncEnumerable<TObject>
		where TQuery : QueryAsync<TObject, TQuery>
	{
		private const string ArtifactIDFieldName = "Artifact ID";

		protected QueryAsync(IQueryRequest request, IQueryExecutorAsync<TObject> executor)
		{
			Request = request;
			Executor = executor;

			FetchAll();
		}

		protected IQueryRequest Request { get; }

		protected IQueryExecutorAsync<TObject> Executor { get; }

		/// <summary>
		/// Filters the query with specific field value.
		/// </summary>
		/// <param name="propertySelector">The property selector.</param>
		/// <param name="propertyValue">The property value.</param>
		/// <returns>The query object.</returns>
		public TQuery Where(Expression<Func<TObject, object>> propertySelector, object propertyValue)
		{
			string propertyName = propertySelector.ExtractMemberName();
			string fieldName = ObjectFieldMapping.GetFieldName<TObject>(propertyName);

			return Where(fieldName, propertyValue);
		}

		/// <summary>
		/// Filters the query with specific field value.
		/// </summary>
		/// <param name="fieldName">The name of the field.</param>
		/// <param name="fieldValue">The field value.</param>
		/// <returns>The query object.</returns>
		public TQuery Where(string fieldName, object fieldValue)
		{
			Request.AddCondition(fieldName, fieldValue);

			return (TQuery)this;
		}

		/// <summary>
		/// Filters the query by string condition.
		/// </summary>
		/// <param name="condition">The string representation of condition.</param>
		/// <returns>The query object.</returns>
		public TQuery Where(string condition)
		{
			Request.AddCondition(condition);

			return (TQuery)this;
		}

		/// <summary>
		/// Specifies that query should fetch all object fields.
		/// </summary>
		/// <returns>The query object.</returns>
		public TQuery FetchAll()
		{
			return Fetch(ObjectFieldMapping.GetFieldNames<TObject>());
		}

		/// <summary>
		/// Specifies that query should fetch only "Artifact ID" field.
		/// </summary>
		/// <returns>The query object.</returns>
		public TQuery FetchOnlyArtifactID()
		{
			return Fetch(ArtifactIDFieldName);
		}

		/// <summary>
		/// Specifies the set of field names for query to fetch.
		/// </summary>
		/// <param name="fieldNames">The field names.</param>
		/// <returns>The query object.</returns>
		public TQuery Fetch(params string[] fieldNames)
		{
			return Fetch(fieldNames.AsEnumerable());
		}

		/// <summary>
		/// Specifies the set of field names for query to fetch.
		/// </summary>
		/// <param name="fieldNames">The field names.</param>
		/// <returns>The query object.</returns>
		public TQuery Fetch(IEnumerable<string> fieldNames)
		{
			Request.SetFieldsToFetch(fieldNames);

			return (TQuery)this;
		}

		/// <summary>
		/// Specifies the  number of items to return in the query result.
		/// </summary>
		/// <param name="length">The number of items to return in the query result, starting with index in the start parameter.</param>
		/// <returns>The query object.</returns>
		public TQuery SetLength(int length)
		{
			Request.SetLength(length);

			return (TQuery)this;
		}

		/// <summary>
		/// Returns an enumerator that iterates through the collection.
		/// </summary>
		/// <returns>
		/// An enumerator that can be used to iterate through the collection.
		/// </returns>
		/// <param name="cancellationToken">The token for cancellation.</param>
		async IAsyncEnumerator<TObject> IAsyncEnumerable<TObject>.GetAsyncEnumerator(CancellationToken cancellationToken)
		{
			IEnumerable<TObject> result = await Executor.ExecuteAsync(Request).ConfigureAwait(false);
			foreach (TObject item in result)
			{
				yield return item;
			}
		}
	}
}
