using System;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;


// ReSharper disable once CheckNamespace
namespace MongoDB
{
	/// <summary>
	/// Detailed statistics about the query plan.
	/// </summary>
	public class QueryPlanStats
	{
		/// <summary>
		/// 
		/// </summary>
		[BsonElement("type")]
		public string Type { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[BsonElement("works")]
		public int Works { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[BsonElement("yields")]
		public int Yields { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[BsonElement("unyields")]
		public int Unyields { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[BsonElement("invalidates")]
		public int Invalidates { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[BsonElement("advanced")]
		public int Advanced { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[BsonElement("needTime")]
		public bool NeedTime { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[BsonElement("needFetch")]
		public bool NeedFetch { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[BsonElement("isEOF")]
		public bool IsEOF { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[BsonElement("docsTested")]
		public int DocsTested { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[BsonElement("children")]
		public List<QueryPlanStats> Children { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[BsonExtraElements]
		public BsonDocument AdditionalData { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public QueryPlanStats()
		{
			this.Children = new List<QueryPlanStats>();
		}
	}
}