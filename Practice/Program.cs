using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Practice
{
    class Program
    {
        static void Main(string[] args)
        {
            Test1();
            Test2();
            Test3();
            Console.ReadKey();

        }
        static void Test1()
        {
            var tester = new PracticeLib.RedisTester();
            tester.Subscribe();

            for (var i = 0; i < 10; i++)
            {
                var value = tester.Publish("mq1-" + i + "消息：" + DateTime.Now.ToString("o"));
                Thread.Sleep(200);
            }

            Console.WriteLine("over1");
        }

        static void Test2()
        {
            var tester = new PracticeLib.Redis2Tester();
            tester.Subscribe();

            for (var i = 0; i < 10; i++)
            {
                var value = tester.Publish("mq2-" + i + "消息：" + DateTime.Now.ToString("o"));
                Thread.Sleep(200);
            }

            Console.WriteLine("over2");
        }
        static void Test3()
        {
            var tester = new PracticeLib.RabbitTester();
            tester.Subscribe();

            for (var i = 0; i < 10; i++)
            {
                var value = tester.Publish("mq3-" + i + "消息：" + DateTime.Now.ToString("o"));
                Thread.Sleep(200);
            }

            Console.WriteLine("over3");
        }


    }
}
