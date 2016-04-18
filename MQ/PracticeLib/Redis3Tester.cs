using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using StackExchange.Redis;

namespace PracticeLib
{
    public class Redis3Tester
    {

        static ConnectionMultiplexer manager = CreateClient();


        static ConnectionMultiplexer CreateClient()
        {
            var m = ConnectionMultiplexer.Connect("127.0.0.1:6379");
            return m;
        }

        public long Publish(string message)
        {
            Debug.WriteLine(">>:" + message);
            var sub = manager.GetSubscriber();
            var result = sub.Publish("mq:test", message);


            return result;
        }

        public long Subscribe()
        {

            var sub = manager.GetSubscriber();

            sub.SubscribeAsync("mq:test", (channel, msg) =>
            {
                Debug.WriteLine("<<:" + msg);
                Console.WriteLine("<<:" + msg);
            });

            return 0;
        }
    }
}
