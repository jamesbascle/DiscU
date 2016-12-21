using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneOf.PerfTests
{
    /// <summary>
    /// Performance tests.  Assumes OneOf is working correctly, per the Unit Tests.
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
#if DEBUG
            Console.WriteLine("Debug build, tests will run much slower.");
            Console.WriteLine("");
#endif

            OneOf<C0, C1, C2, C3, C4, C5, C6, C7, C8> test0 = new C0();
            Test("T0", () => test0.Match((C0 v) => 0).Match((C1 v) => 1).Match((C2 v) => 2).Match((C3 v) => 3).Match((C4 v) => 4).Match((C5 v) => 5).Match((C6 v) => 6).Match((C7 v) => 7).Match((C8 v) => 8));

            OneOf<C0, C1, C2, C3, C4, C5, C6, C7, C8> test8 = new C8();
            Test("T8", () => test8.Match((C0 v) => 0).Match((C1 v) => 1).Match((C2 v) => 2).Match((C3 v) => 3).Match((C4 v) => 4).Match((C5 v) => 5).Match((C6 v) => 6).Match((C7 v) => 7).Match((C8 v) => 8));

            var c0 = new C0();
            Test("Ctor0", () => { OneOf<C0, C1, C2, C3, C4, C5, C6, C7, C8> oo = c0; return oo; });

            var c8 = new C8();
            Test("Ctor8", () => { OneOf<C0, C1, C2, C3, C4, C5, C6, C7, C8> oo = c8; return oo; });

            Console.WriteLine("");
            Console.WriteLine("Press any key to end.");
            Console.ReadKey();
        }

        static void Test<T>(string nameOfTest, Func<T> thingToTest)
        {
            Console.Write("{0:15} ... ", nameOfTest);
            var sw = Stopwatch.StartNew();

            var iterations = 0;
            while (sw.ElapsedMilliseconds < 1000)
            {
                for(var i = 0; i < 1000; i++)
                {
                    thingToTest();
                    iterations++;
                }
            }

            sw.Stop();
            Console.WriteLine("{0:0.00}M per second", iterations / 1000000d);
        }

        class C0 { }
        class C1 { }
        class C2 { }
        class C3 { }
        class C4 { }
        class C5 { }
        class C6 { }
        class C7 { }
        class C8 { }
    }
}
