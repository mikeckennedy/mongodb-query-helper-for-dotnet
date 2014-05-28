using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MongoDB.Driver.Builders;
using MongoDB.QueryHelper.Tests.Data;

namespace MongoDB.QueryHelper.Tests
{
	[TestClass]
	public class ExplainTests
	{
		readonly TestMongoContext mongo = new TestMongoContext();

		public ExplainTests()
		{
			TestMongoContext.BuildTestData();
		}

		[TestMethod]
		public void TestTypedExplainFromLinqQueryDetectsIndexedCursor()
		{
			mongo.PeopleCollection.CreateIndex(IndexKeys<Person>.Ascending(c => c.Age));
			QueryPlan typedExplain = mongo.People
				.Where(p => p.Age > 30)
				.Take(1)
				.ExplainTyped();

			Assert.AreEqual(CursorType.BtreeCursor, typedExplain.CursorType);
		}

		[TestMethod]
		public void TestTypedExplainVerbose()
		{
			SafeDropIndex(IndexKeys<Person>.Ascending(c => c.Age));
			QueryPlan typedExplain = mongo.People
				.Where(p => p.Age > 30)
				.Take(1)
				.ExplainTyped(verbose: true);

			Assert.AreEqual(CursorType.BasicCursor, typedExplain.CursorType);
		}

		private void SafeDropIndex(IndexKeysBuilder<Person> indexKeysBuilder)
		{
			try
			{
				mongo.PeopleCollection.DropIndex(indexKeysBuilder);
			}
			catch
			{
			}
		}
	}
}
