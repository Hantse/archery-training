using Firebase.Database;
using Firebase.Database.Offline;
using Shared.Contracts.Response;
using System;
using System.Threading.Tasks;

namespace Database.Sync.Update.Tests
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            var client = new FirebaseClient("https://archery-training-320117-default-rtdb.europe-west1.firebasedatabase.app/",
                                new FirebaseOptions
                                {
                                    OfflineDatabaseFactory = (t, s) => new OfflineDatabase(t, s)
                                });

            var realTimeDb = client.Child("users")
                            .AsRealtimeDatabase<object>();

            while (true)
            {
                var k = Console.ReadKey();
                Console.WriteLine("Insert item.");
                realTimeDb.Set("MonSuperUser", new { Test = "Test" }, SyncOptions.Put);
            }
        }
    }
}
