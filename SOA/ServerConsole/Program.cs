using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SoaServerConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            ServiceLibrary.WcfServer.StartServer();
            Console.Read();
        }
    }
}
