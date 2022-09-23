using StackExchange.Redis;

ConnectionMultiplexer redisCon = ConnectionMultiplexer.Connect("localhost");
var db = redisCon.GetDatabase();


//1)String
db.StringSet("foo", "Boooom!");
Console.WriteLine("1)String: " + db.StringGet("foo"));

//2)List
RedisValue[] listColors = new RedisValue[] { "green", "red", "blue", "black", "yellow" };
db.ListRightPush("colors", listColors);
Console.WriteLine("2)List: " + db.ListLeftPop("colors"));

//3)Set
db.SetAdd("user:1", new RedisValue[] { 756, 849, 358, 861 });
db.SetAdd("user:2", new RedisValue[] { 400, 849, 358, 891 });
Console.WriteLine("3)Set: " + String.Join(", ", db.SetCombine(SetOperation.Intersect, "user:1", "user:2")));

//4)Hashes
db.HashSet("person:1", new HashEntry[]
{
    new HashEntry("name","Alex"),
    new HashEntry("city","Tomsk"),
    new HashEntry("year", 2000),
    new HashEntry("conscription", true)
});

//var val = db.HashGet("person:1", "name");
//Console.WriteLine(val);
var val = db.HashGetAll("person:1");
Console.WriteLine("4)Hashes: " + String.Join(", ", val));