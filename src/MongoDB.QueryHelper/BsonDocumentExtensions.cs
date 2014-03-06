using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;

// ReSharper disable once CheckNamespace
namespace MongoDB
{
	public static class BsonDocumentExtensions
	{
		public static T Deserialize<T>(this BsonDocument doc)
		{
			return BsonSerializer.Deserialize<T>(doc);
		}
	}
}