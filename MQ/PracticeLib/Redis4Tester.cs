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
    public class Redis4Tester
    {


        static ConnectionMultiplexer CreateClient()
        {
            return ConnectionMultiplexer.Connect("127.0.0.1:6379");
        }
        public long Publish(string message)
        {
            using (var client = CreateClient())
            {
                var db = client.GetDatabase();
                Debug.WriteLine(">>:" + message);

                db.ListLeftPush("mq:list", message);

            }
            return 0;
        }

        public long Subscribe()
        {
            var client = CreateClient();

            //很low的轮询式
            Task.Factory.StartNew(() =>
            {
                while (true)
                {
                    try
                    {
                        var item = client.GetDatabase().ListRightPop("mq:list");
                        if (!item.HasValue)
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
