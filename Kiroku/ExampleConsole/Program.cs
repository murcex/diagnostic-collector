using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Collections.Specialized;

// Kiroku Logging Library
using Kiroku;

namespace ExampleConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            KManager.Online((NameValueCollection)ConfigurationManager.GetSection("Kiroku"));

            // Method 1
            ExampleClass.Test1();

            // Method 2
            ExampleClass.Test2();

            KManager.Offline();

            Console.ReadKey();
        }
    }

    class ExampleClass
    {
        public static void Test1()
        {
            using (KLog Test1 = new KLog("Test1"))
            {
                for (int a = 10; a < 20; a = a + 1)
                {
                    Test1.Info(a.ToString());
                }
            }
        }

        public static void Test2()
        {
            using (KLog test2 = new KLog("Test2"))
            {
                int i, j;

                for (i = 2; i < 100; i++)
                {
                    for (j = 2; j <= (i / j); j++)
                        if ((i % j) == 0) break; // if factor found, not prime
                    test2.Info(i.ToString());
                }
            }
        }
    }
}
