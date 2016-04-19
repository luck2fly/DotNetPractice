using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Messaging;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PracticeLib
{
    public class MsmqTester
    {

        private static MessageQueue CreateConnection()
        {
            var queuePath = @".\private$\myQueue";
            if (!MessageQueue.Exists(queuePath))
            {
                MessageQueue.Create(queuePath);
            }
            MessageQueue mq = new MessageQueue(queuePath);
            mq.Formatter = new XmlMessageFormatter(new Type[] { typeof(string) });

            return mq;
        }

        public long Publish(string message)
        {
            var mq = CreateConnection();
            Debug.WriteLine(">>:" + message);

            mq.Send(message);

            return 1;
        }

        public long Subscribe()
        {
            var conn = CreateConnection();

            Task.Run(() =>
            {
                while (true)
                {
                    var msg = conn.Receive();
                    if (msg != null)
                    {
                        Debug.WriteLine("<<:" + msg.Body);
                    }
                }
            });


            return 0;
        }


    }

}
