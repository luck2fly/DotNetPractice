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

        public long Publish(string message)
        {
            RedisClient client = new RedisClient("192.168.22.236", 6379);
            client.AddItemToList("mq:list", message);

            var result = client.PublishMessage("mq:test", message);
            client.Quit();

            return result;
        }

        public long Subscribe()
        {
            RedisClient client = new RedisClient("192.168.22.236", 6379);
            RedisClient client2 = new RedisClient("192.168.22.236", 6379);
            client.AddItemToList("mq:list", "subscribe");

            RedisSubscription sub = new RedisSubscription(client);

            sub.OnMessage += (channel, msg) =>
            {
                Debug.WriteLine(channel + ":" + msg);
            };

            Task.Factory.StartNew(() =>
            {
                sub.SubscribeToChannels("mq:test");



            });

            Task.Factory.StartNew(() =>
            {
                while (true)
                {
                    try
                    {
                        var item = client2.DequeueItemFromList("mq:list");
                        if (item == null)
                        {
                            Thread.Sleep(2000);
                        }
                        else {
                            Debug.WriteLine("list:" + item);
                        }
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine(ex.Message);
                        Debug.WriteLine(ex.StackTrace);
                    }

                }
            });
            return 0;
        }
    }
}
