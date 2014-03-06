using System;
using System.Linq;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver.Linq;

// ReSharper disable once CheckNamespace
namespace MongoDB
{
	public static class MongoExplainExtensions
	{
	

	/// <summary>
		/// Returns an explanation of how the query was executed (instead of the results).
		/// </summary>
		/// <param name="source">The LINQ query to explain</param>
		/// <returns>A strongly-typed explanation of how the query was executed.</returns>
		public static QueryPlan ExplainTyped<T>(this IQueryable<T> source)
		{
			return ExplainTyped(source, false);
		}

		/// <summary>
		/// Returns an explanation of how the query was executed (instead of the results).
		/// </summary>
		/// <param name="source">The LINQ query to explain</param>
		/// <param name="verbose">Whether the explanation should contain more details.</param>
		/// <returns>A strongly-typed explanation of how the query was executed.</returns>
		public static QueryPlan ExplainTyped<T>(this IQueryable<T> source, bool verbose)
		{
			BsonDocument doc = source.Explain(verbose);
			return BsonSerializer.Deserialize<QueryPlan>(doc);
		}

	}

	
}