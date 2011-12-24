using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Meitanbot
{
    class Program
    {
        static void Main(string[] args)
        {
            var bot = new MeitanCore("credential.dat", "config.dat");
            while (true) { }
        }
    }
}
