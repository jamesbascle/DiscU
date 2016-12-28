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

            TestCtor();
            TestMatch();

            Console.WriteLine("");
            Console.WriteLine("Press any key to end.");
            Console.ReadKey();
        }

        private static void TestCtor()
        {
            var c1 = new C1();
            RunTest("Ctor1", () => { OneOf<C1, C2, C3, C4, C5, C6, C7, C8, C9> oo = c1; return oo; });

            var c5 = new C5();
            RunTest("Ctor5", () => { OneOf<C1, C2, C3, C4, C5, C6, C7, C8, C9> oo = c5; return oo; });

            var c9 = new C9();
            RunTest("Ctor9", () => { OneOf<C1, C2, C3, C4, C5, C6, C7, C8, C9> oo = c9; return oo; });
        }

        private static void TestMatch()
        {
            OneOf<C1, C2, C3, C4, C5, C6, C7, C8, C9> oo1 = new C1();
            RunTest("Match1", () => oo1.Match((C1 v) => 1).Match((C2 v) => 2).Match((C3 v) => 3).Match((C4 v) => 4).Match((C5 v) => 5).Match((C6 v) => 6).Match((C7 v) => 7).Match((C8 v) => 8).Match((C9 v) => 9));

            OneOf<C1, C2, C3, C4, C5, C6, C7, C8, C9> oo5 = new C5();
            RunTest("Match5", () => oo5.Match((C1 v) => 1).Match((C2 v) => 2).Match((C3 v) => 3).Match((C4 v) => 4).Match((C5 v) => 5).Match((C6 v) => 6).Match((C7 v) => 7).Match((C8 v) => 8).Match((C9 v) => 9));

            OneOf<C1, C2, C3, C4, C5, C6, C7, C8, C9> oo9 = new C9();
            RunTest("Match9", () => oo9.Match((C1 v) => 1).Match((C2 v) => 2).Match((C3 v) => 3).Match((C4 v) => 4).Match((C5 v) => 5).Match((C6 v) => 6).Match((C7 v) => 7).Match((C8 v) => 8).Match((C9 v) => 9));
        }

        static void RunTest<T>(string nameOfTest, Func<T> thingToTest)
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();

            Console.Write("{0:15} ... ", nameOfTest);

            // warm it up first
            for (var i = 0; i < 100; i++)
                thingToTest();

            // and then measure it
            var sw = Stopwatch.StartNew();

            var iterations = 0;
            while (sw.ElapsedMilliseconds < 1000)
            {
                // don't check the clock too often, and
                // unroll the loop 10x to take the cost of looping out of the equation as much as possible
                for(var i = 0; i < 100; i++)
                {
                    thingToTest();
                    thingToTest();
                    thingToTest();
                    thingToTest();
                    thingToTest();
                    thingToTest();
                    thingToTest();
                    thingToTest();
                    thingToTest();
                    thingToTest();
                }

                iterations += 1000;     // 100 loops of 10 tests = 1000
            }

            sw.Stop();
            Console.WriteLine("{0:0.00}M per second", iterations / 1000000d);
        }

        class C1 { }
        class C2 { }
        class C3 { }
        class C4 { }
        class C5 { }
        class C6 { }
        class C7 { }
        class C8 { }
        class C9 { }
    }
}
