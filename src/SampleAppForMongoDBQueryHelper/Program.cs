using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB;
using MongoDB.QueryHelper.Tests.Data;

namespace SampleAppForMongoDBQueryHelper
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine("This sample app demos how to use MongoDb.QueryHelper.");
			Console.WriteLine("There are two basic use cases:");
			Console.WriteLine("1. Explain a LINQ query (does it use an index for example?)");
			Console.WriteLine("2. Convert a LINQ query to the JavaScript code run in MongoDB");
			Console.WriteLine();
			Console.WriteLine("Find more about this on GitHub:");
			Console.WriteLine("https://github.com/mikeckennedy/mongodb-query-helper-for-dotnet");
			Console.WriteLine("By @mkennedy");
			Console.WriteLine();
			Console.WriteLine("We are working with this LINQ query:");
			Console.WriteLine();
			Console.WriteLine("var query =");
			Console.WriteLine("	from p in mongo.People");
			Console.WriteLine("	where p.Age > 20 && p.Name.Length >= 2");
			Console.WriteLine("	orderby p.Age descending");
			Console.WriteLine("	select p;");
			Console.WriteLine();

			TestMongoContext.BuildTestData();

			ToStringAQuery();
			ExplainQuery();
		}

		private static void ToStringAQuery()
		{
			Console.WriteLine("=== Query.ToMongoQueryText ===========================");

			var mongo = new TestMongoContext();
			var query =
				from p in mongo.People
				where p.Age > 20 && p.Name.Length >= 2
				orderby p.Age descending 
				select p;

			Console.WriteLine(query.ToMongoQueryText());
			Console.WriteLine();
			Console.WriteLine();
			Console.WriteLine("Enter to continue");
			Console.ReadKey(true);
			Console.WriteLine();
		}

		private static void ExplainQuery()
		{
			Console.WriteLine("=== Explaining queries ===========================");
			TestMongoContext.DropIndexes();
			Console.WriteLine("Without an index:");
			Console.WriteLine();
			Console.WriteLine("Enter to continue");
			Console.ReadKey(true);

			var mongo = new TestMongoContext();
			var query =
				from p in mongo.People
				where p.Age > 20 && p.Name.Length >= 2
				orderby p.Age descending
				select p;

			QueryPlan plan = query.ExplainTyped();
			PrettyPrint(plan);
			Console.WriteLine();
			Console.WriteLine("Using an index? " + (plan.CursorType == CursorType.BtreeCursor));
			Console.WriteLine();

			Console.WriteLine("Enter to continue");
			Console.ReadKey(true);

			TestMongoContext.AddIndexes();
			Console.WriteLine("With an index");
			plan = query.ExplainTyped();
			PrettyPrint(plan);
			Console.WriteLine();
			Console.WriteLine("Using an index? " + (plan.CursorType == CursorType.BtreeCursor));
			Console.WriteLine();

			Console.WriteLine("Enter to continue");
			Console.ReadKey(true);
		}

		private static void PrettyPrint(QueryPlan queryPlan)
		{
			var json = Newtonsoft.Json.JsonConvert.DeserializeObject(queryPlan.ToString());
			Console.WriteLine(json);
		}
	}
}
