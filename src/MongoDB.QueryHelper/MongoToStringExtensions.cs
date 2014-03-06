using System;
using System.Collections.Generic;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

// ReSharper disable once CheckNamespace
namespace MongoDB
{
	public static class MongoToStringExtensions
	{
		public static IMongoQuery ToMongoQuery<T>(this IEnumerable<T> query)
		{
			MongoQueryable<T> mongoQuery = query as MongoQueryable<T>;
			if (mongoQuery != null)
				return mongoQuery.GetMongoQuery();

			MongoCursor<T> mongoCursor = query as MongoCursor<T>;
			if (mongoCursor != null)
				return mongoCursor.Query;

			string msg = string.Format("Cannot convert from {0} to either {1} or {2}.",
				query.GetType().Name, typeof (MongoQueryable<T>).Name, typeof (MongoCursor<T>).Name);
			throw new ArgumentException(msg);
		}

		public static string ToMongoQueryText<T>(this IEnumerable<T> query)
		{
			var mongoQuery = query.ToMongoQuery();
			if (mongoQuery == null)
				return null;

			return mongoQuery.ToString();
		}
	}
}