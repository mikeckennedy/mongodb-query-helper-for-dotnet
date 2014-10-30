Thanks for installing MongoDB.QueryHelper. 

Michael (@mkennedy)

See this document and more at the GitHub project:
https://github.com/mikeckennedy/mongodb-query-helper-for-dotnet

======================================================
MongoDB query helper for .NET

This library makes working with LINQ to MongoDB queries easier 
from C#. Specificially it is for debugging and runtime inquiry 
of LINQ queries.

There are two basic use cases:

    Explain a LINQ query (does it use an index for example?)
    Convert a LINQ query to the JavaScript code run in MongoDB

Consider this LINQ query:

var query =
        from p in mongo.People
        where p.Age > 20 && p.Name.Length >= 2
        orderby p.Age descending
        select p;

Calling

Console.WriteLine( query.ToMongoQueryText() );

Outputs the following text;

{ "Age" : { "$gt" : 20 }, "Name" : /^.{2,}$/s }

Next, calling explain on query will return a strongly-typed 
QueryPlan object.

QueryPlan plan = query.ExplainTyped();

To see the output either work with it's properties, e.g.

bool usesIndex = plan.CursorType == CursorType.BtreeCursor;

Or just ToString the plan to get:

{
  "cursor": "BtreeCursor Age_1 reverse",
  "isMultiKey": false,
  "n": 3,
  "nscanned": 3,
  "nscannedObjectsAllPlans": 5,
  "nscannedAllPlans": 5,
  "nscannedObjects": 3,
  "scanAndOrder": false,
  "indexOnly": false,
  "nYields": 0,
  "nChunkSkips": 0,
  "millis": 0,
  "indexBounds": {
    "Age": [
      [
        "Infinity",
        20
      ]
    ]
  },
  "server": "WIN-7S2IMPQ2TOE:27017",
  "allPlans": null,
  "oldPlan": null,
  "clusteredType": null,
  "millisShardTotal": 0,
  "millisShardAvg": 0,
  "numQueries": 0,
  "numShards": 0,
  "filterSet": false,
  "stats": {
    "type": "FETCH",
    "works": 5,
    "yields": 0,
    "unyields": 0,
    "invalidates": 0,
    "advanced": 3,
    "needTime": false,
    "needFetch": false,
    "isEOF": true,
    "docsTested": 0,
    "children": [
      {
        "type": "IXSCAN",
        "works": 3,
        "yields": 0,
        "unyields": 0,
        "invalidates": 0,
        "advanced": 3,
        "needTime": false,
        "needFetch": false,
        "isEOF": true,
        "docsTested": 0,
        "children": [],
        "keyPattern": "{ Age: 1 }",
        "boundsVerbose": "field #0['Age']: [1.#INF, 20)",
        "isMultiKey": 0,
        "yieldMovedCursor": 0,
        "dupsTested": 0,
        "dupsDropped": 0,
        "seenInvalidated": 0,
        "matchTested": 0,
        "keysExamined": 3
      }
    ],
    "alreadyHasObj": 0,
    "forcedFetches": 0,
    "matchTested": 3
  }
}
