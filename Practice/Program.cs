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
            var tester = new PracticeLib.RedisTester();
            tester.Subscribe();

            for (var i = 0; i < 10; i++)
            {
                var value = tester.Publish("消息："+DateTime.Now.ToString());
                Thread.Sleep(200);
            }

            Console.WriteLine("over");
            Console.ReadKey();
        }
    }
}
