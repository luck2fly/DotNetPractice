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
    public class Redis2Tester
    {

        static RedisClient CreateClient()
        {
            return new RedisClient("127.0.0.1", 6379);
        }
        public long Publish(string message)
        {
            RedisClient client = CreateClient();
            Debug.WriteLine(">>:" + message);
            client.EnqueueItemOnList("mq:list", message);

            client.Quit();

            return 0;
        }

        public long Subscribe()
        {
            RedisClient client = CreateClient();

            //很low的轮询式
            Task.Factory.StartNew(() =>
            {
                while (true)
                {
                    try
                    {
                        var item = client.DequeueItemFromList("mq:list");
                        if (item == null)
                        {
                            Thread.Sleep(100);
                        }
                        else
                        {
                            Debug.WriteLine("<<:" + item);
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
