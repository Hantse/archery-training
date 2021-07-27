using Firebase.Database;
using Firebase.Database.Offline;
using System;
using System.Threading.Tasks;

namespace Database.Sync.Tests
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

            var realTimeDb = client.Child("sessions")
                            .AsRealtimeDatabase<object>();

            foreach(var item in realTimeDb.Once())
            {
                Console.WriteLine($"Item Key : {item.Key}");
            }

            var obs = client.Child("sessions")
                .AsObservable<object>()
                .Subscribe((value) =>
                {
                    Console.WriteLine($"OBS : {value.Key} - {value.EventSource} - {value.EventType}");
                });

            await Task.Delay(-1);
        }
    }
}
