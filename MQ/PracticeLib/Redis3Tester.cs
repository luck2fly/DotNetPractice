using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using StackExchange.Redis;

namespace PracticeLib
{
    public class Redis3Tester
    {

        static ConnectionMultiplexer CreateClient()
        {
            return ConnectionMultiplexer.Connect("127.0.0.1:6379");
        }
        public long Publish(string message)
        {
            var client = CreateClient();
            Debug.WriteLine(">>:" + message);
            var sub = client.GetSubscriber();
            var result = sub.Publish("mq:test", message);


            return result;
        }

        public long Subscribe()
        {
            var client = CreateClient();

            var sub = client.GetSubscriber();

            sub.SubscribeAsync("mq:test", (channel, msg) =>
            {
                Debug.WriteLine("<<:" + msg);
            });

            return 0;
        }
    }
}
