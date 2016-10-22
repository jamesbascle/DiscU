using System;
using System.Linq;
using NBench;
using NUnit.Framework;
// ReSharper disable UnusedMember.Global - test classes are obviously not instantiated by reference.
// ReSharper disable UnusedParameter.Global - having BenchmarkContext in the signature slightly improves performance of testing.

namespace OneOf.Tests
{
    [Category("Performance")]
    public class T1 : PerfTest<T1>
    {
        private static string Func(OneOf<PerfFakeBase, One, Two, Three, Four> oo, Counter c)
        {
            c.Increment();
            return oo
                .Match(o => o.OneProp)
                .Match(tw => tw.TwoProp)
                .Match(th => th.ThreeProp)
                .Match(f => f.FourProp).
                Match(b => b.Type.ToString());
        }

        private Counter _counter;

        [PerfSetup]
        public void PerfSetup(BenchmarkContext context)
        {
            _counter = context.GetCounter("TestCounter");
        }

        
        [PerfBenchmark(Description = "Test performance of Matching.", RunMode = RunMode.Throughput, RunTimeMilliseconds = 500, TestMode = TestMode.Test)]
        [CounterTotalAssertion("TestCounter", MustBe.GreaterThanOrEqualTo, 0)]
        [MemoryAssertion(MemoryMetric.TotalBytesAllocated, MustBe.GreaterThanOrEqualTo, 0)]
        [GcTotalAssertion(GcMetric.TotalCollections, GcGeneration.Gen2, MustBe.GreaterThanOrEqualTo, 0)]
        public void Benchmark1(BenchmarkContext c)
        {
            Func(PerfOneOfs.One, _counter);
        }
    }

    [Category("Performance")]
    public class T2 : PerfTest<T2>
    {
        private static readonly Func<OneOf<PerfFakeBase, One, Two, Three, Four>, Counter, string> Func = (oo, c) =>
        {
            c.Increment();
            return oo
                .Match(o => o.OneProp)
                .Match(tw => tw.TwoProp)
                .Match(th => th.ThreeProp)
                .Match(f => f.FourProp).
                Match(b => b.Type.ToString());
        };

        private Counter _counter;

        [PerfSetup]
        public void PerfSetup(BenchmarkContext context)
        {
            _counter = context.GetCounter("TestCounter");
        }

        [PerfBenchmark(Description = "Test performance of Matching.", RunMode = RunMode.Throughput, RunTimeMilliseconds = 500, TestMode = TestMode.Test)]
        [CounterTotalAssertion("TestCounter", MustBe.GreaterThanOrEqualTo, 0)]
        [MemoryAssertion(MemoryMetric.TotalBytesAllocated, MustBe.GreaterThanOrEqualTo, 0)]
        [GcTotalAssertion(GcMetric.TotalCollections, GcGeneration.Gen2, MustBe.GreaterThanOrEqualTo, 0)]
        public void Benchmark2(BenchmarkContext c)
        {
            Func(PerfOneOfs.Two, _counter);
        }
    }

    [Category("Performance")]
    public class T3 : PerfTest<T3>
    {
        private static readonly Func<OneOf<PerfFakeBase, One, Two, Three, Four>, Counter, string> Func = (oo, c) =>
        {
            c.Increment();
            return oo
                .Match(o => o.OneProp)
                .Match(tw => tw.TwoProp)
                .Match(th => th.ThreeProp)
                .Match(f => f.FourProp).
                Match(b => b.Type.ToString());
        };

        private Counter _counter;

        [PerfSetup]
        public void PerfSetup(BenchmarkContext context)
        {
            _counter = context.GetCounter("TestCounter");
        }

        [PerfBenchmark(Description = "Test performance of Matching.", RunMode = RunMode.Throughput, RunTimeMilliseconds = 500, TestMode = TestMode.Test)]
        [CounterTotalAssertion("TestCounter", MustBe.GreaterThanOrEqualTo, 0)]
        [MemoryAssertion(MemoryMetric.TotalBytesAllocated, MustBe.GreaterThanOrEqualTo, 0)]
        [GcTotalAssertion(GcMetric.TotalCollections, GcGeneration.Gen2, MustBe.GreaterThanOrEqualTo, 0)]
        public void Benchmark3(BenchmarkContext c)
        {
            Func(PerfOneOfs.Three, _counter);
        }
    }

    [Category("Performance")]
    public class T4 : PerfTest<T4>
    {
        private static readonly Func<OneOf<PerfFakeBase, One, Two, Three, Four>, Counter, string> Func = (oo, c) =>
        {
            c.Increment();
            return oo
                .Match(o => o.OneProp)
                .Match(tw => tw.TwoProp)
                .Match(th => th.ThreeProp)
                .Match(f => f.FourProp).
                Match(b => b.Type.ToString());
        };

        private Counter _counter;

        [PerfSetup]
        public void PerfSetup(BenchmarkContext context)
        {
            _counter = context.GetCounter("TestCounter");
        }

        [PerfBenchmark(Description = "Test performance of Matching.", RunMode = RunMode.Throughput, RunTimeMilliseconds = 500, TestMode = TestMode.Test)]
        [CounterTotalAssertion("TestCounter", MustBe.GreaterThanOrEqualTo, 0)]
        [MemoryAssertion(MemoryMetric.TotalBytesAllocated, MustBe.GreaterThanOrEqualTo, 0)]
        [GcTotalAssertion(GcMetric.TotalCollections, GcGeneration.Gen2, MustBe.GreaterThanOrEqualTo, 0)]
        public void Benchmark4(BenchmarkContext c)
        {
            Func(PerfOneOfs.Four, _counter);
        }
    }

    [Category("Performance")]
    public class T5 : PerfTest<T5>
    {
        private static readonly Func<OneOf<PerfFakeBase, One, Two, Three, Four>, Counter, string> Func = (oo, c) =>
        {
            c.Increment();
            return oo
                .Match(o => o.OneProp)
                .Match(tw => tw.TwoProp)
                .Match(th => th.ThreeProp)
                .Match(f => f.FourProp).
                Match(b => b.Type.ToString());
        };

        private Counter _counter;

        [PerfSetup]
        public void PerfSetup(BenchmarkContext context)
        {
            _counter = context.GetCounter("TestCounter");
        }

        [PerfBenchmark(Description = "Test performance of Matching.", RunMode = RunMode.Throughput, RunTimeMilliseconds = 500, TestMode = TestMode.Test)]
        [CounterTotalAssertion("TestCounter", MustBe.GreaterThanOrEqualTo, 0)]
        [MemoryAssertion(MemoryMetric.TotalBytesAllocated, MustBe.GreaterThanOrEqualTo, 0)]
        [GcTotalAssertion(GcMetric.TotalCollections, GcGeneration.Gen2, MustBe.GreaterThanOrEqualTo, 0)]
        public void Benchmark5(BenchmarkContext c)
        {
            Func(PerfOneOfs.Five, _counter);
        }
    }

    [Category("Performance")]
    public class T6 : PerfTest<T6>
    {
        private static readonly Func<OneOf<PerfFakeBase, One, Two, Three, Four>, Counter, string> Func = (oo, c) =>
        {
            c.Increment();
            return oo
                .Match(o => o.OneProp)
                .Match(tw => tw.TwoProp)
                .Match(th => th.ThreeProp)
                .Match(f => f.FourProp).
                Match(b => b.Type.ToString());
        };

        private Counter _counter;

        [PerfSetup]
        public void PerfSetup(BenchmarkContext context)
        {
            _counter = context.GetCounter("TestCounter");
        }

        [PerfBenchmark(Description = "Test performance of Matching.", RunMode = RunMode.Throughput, RunTimeMilliseconds = 500, TestMode = TestMode.Test)]
        [CounterTotalAssertion("TestCounter", MustBe.GreaterThanOrEqualTo, 0)]
        [MemoryAssertion(MemoryMetric.TotalBytesAllocated, MustBe.GreaterThanOrEqualTo, 0)]
        [GcTotalAssertion(GcMetric.TotalCollections, GcGeneration.Gen2, MustBe.GreaterThanOrEqualTo, 0)]
        public void Benchmark6(BenchmarkContext c)
        {
            Func(PerfOneOfs.Six, _counter);
        }
    }
}