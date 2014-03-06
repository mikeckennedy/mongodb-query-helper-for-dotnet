using System;
using System.Collections.Generic;
using MongoDB.Bson;

namespace MongoDB.QueryHelper.Tests.Data
{
	public class Person
	{
		public ObjectId Id { get; set; }
		public string Name { get; set; }
		public int Age { get; set; }

		public List<ObjectId> FriendIds { get; set; }

		public Person()
		{
			FriendIds = new List<ObjectId>();
		}
	}
}