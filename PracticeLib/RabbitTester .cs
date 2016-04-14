using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using RabbitMQ.Client;

namespace PracticeLib
{
    public class RabbitTester
    {

        private static IConnection CreateConnection()
        {
            var fac = new ConnectionFactory();
            fac.HostName = "127.0.0.1";
            fac.Port = 5672;
            fac.UserName = "guest";
            fac.Password = "guest";
            //fac.VirtualHost = "v";
            var conn = fac.CreateConnection();
            return conn;
        }
        IConnection pconn = CreateConnection();
        public long Publish(string message)
        {
           
            using (var channel = pconn.CreateModel())
            {
                var prep = channel.CreateBasicProperties();
                Debug.WriteLine(">>:" + message);
                channel.BasicPublish("f.exchange", "", prep, Utils.ToBytes(message));
            }

            return 1;
        }

        public long Subscribe()
        {
            var conn = CreateConnection();
            var channel = conn.CreateModel();

            var consumer = new QueueingBasicConsumer(channel);
            var value = channel.BasicConsume("test", true, consumer);

            Task.Factory.StartNew(() =>
            {
                while (true)
                {
                    try
                    {
                        var args = consumer.Queue.Dequeue();

                        if (args == null || args.Body == null)
                        {
                            Thread.Sleep(100);
                        }
                        else {
                            Debug.WriteLine("<<:" + Utils.ParseBytes(args.Body));
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
