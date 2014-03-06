using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using MongoDB.QueryHelper.Tests.Data;

namespace MongoDB.QueryHelper.Tests
{
	[TestClass]
	public class QueryToStringTests
	{
		readonly TestMongoContext mongo = new TestMongoContext();

		[TestMethod]
		public void query_on_mongo_direct_to_query_test()
		{
			MongoCursor<Person> query = 
				mongo.PeopleCollection.Find(Query<Person>.GT(p => p.Age, 30));

			const string jsQuery = "{ \"Age\" : { \"$gt\" : 30 } }";

			IMongoQuery mongoQuery = query.ToMongoQuery();

			Assert.IsNotNull(mongoQuery);
			Assert.AreEqual(jsQuery, mongoQuery.ToString());
		}

		[TestMethod]
		public void query_on_mongo_direct_to_string_test()
		{
			MongoCursor<Person> query = 
				mongo.PeopleCollection.Find(Query<Person>.GT(p => p.Age, 30));

			const string jsQuery = "{ \"Age\" : { \"$gt\" : 30 } }";

			string mongoQuery = query.ToMongoQueryText();

			Assert.IsNotNull(mongoQuery);
			Assert.AreEqual(jsQuery, mongoQuery);
		}

		[TestMethod]
		public void query_on_linq_to_query_test()
		{
			var query =
				from p in mongo.People
				where p.Age > 30
				select p;

			const string jsQuery = "{ \"Age\" : { \"$gt\" : 30 } }";

			IMongoQuery mongoQuery = query.ToMongoQuery();

			Assert.IsNotNull(mongoQuery);
			Assert.AreEqual(jsQuery, mongoQuery.ToString());
		}

		[TestMethod]
		public void query_on_linq_to_string_test()
		{
			var query =
				from p in mongo.People
				where p.Age > 30
				select p;

			const string jsQuery = "{ \"Age\" : { \"$gt\" : 30 } }";

			string mongoQuery = query.ToMongoQueryText();

			Assert.IsNotNull(mongoQuery);
			Assert.AreEqual(jsQuery, mongoQuery);
		}

		[ExpectedException(typeof(ArgumentException))]
		[TestMethod]
		public void query_on_none_mongo_item_to_is_error_test()
		{
			var query =
				from p in new[] {new Person() {Age = 31}}
				where p.Age > 30
				select p;

			query.ToMongoQuery();
		}

		[ExpectedException(typeof(ArgumentException))]
		[TestMethod]
		public void tostring_on_none_mongo_item_is_error_test()
		{
			var query =
				from p in new[] {new Person() {Age = 31}}
				where p.Age > 30
				select p;

			query.ToMongoQueryText();
		}
	}
}
