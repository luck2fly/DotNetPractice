using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ServiceStack.Redis;

namespace PracticeLib
{
    public class RedisTester
    {

        static RedisClient CreateClient()
        {
            return new RedisClient("127.0.0.1", 6379);
        }
        public long Publish(string message)
        {
            RedisClient client = CreateClient();
            Debug.WriteLine(">>:" + message);
            var result = client.PublishMessage("mq:test", message);
            client.Quit();

            return result;
        }

        public long Subscribe()
        {
            RedisClient client = CreateClient();
            RedisSubscription sub = new RedisSubscription(client);

            sub.OnMessage += (channel, msg) =>
            {
                Debug.WriteLine("<<:" + msg);
            };

            Task.Factory.StartNew(() =>
            {
                sub.SubscribeToChannels("mq:test");
            });

            return 0;
        }
    }
}
