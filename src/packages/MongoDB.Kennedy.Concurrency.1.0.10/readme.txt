Thanks for installing MongoDB.Kennedy.Concurrency. 

To use this library, you need to access MongoDB via a class derived 
from ConcurrentDataContext as follows:

public class DataContext : MongoDB.Kennedy.ConcurrentDataContext
{
	public DataContext(string databaseName, string serverName = "localhost") :
		base(databaseName, serverName)
	{
	}

	public IQueryable<Book> Books { get { return base.GetCollection<Book>(); } }
}

You top level entities must implement MongoDB.Kennedy.IMongoEntity:

class Book : MongoDB.Kennedy.IMongoEntity
{
	public ObjectId _id { get; private set; }
	public string _accessId { get; set; }
	// additional props here.
}

See my blog post for a sample app:

http://blog.michaelckennedy.net/2013/04/08/optimistic-concurrency-in-mongodb-using-net-and-csharp/

Stay in touch on twitter: @mkennedy

Cheers,
Michael