using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace OneOf.Tests
{
    [TestClass]
    public class MatchWhenTests
    {
        [TestMethod]
        public void MatchWhenConditionIsTrue() => Assert.IsTrue(
            new OneOf<string, bool>("apple")
                .MatchWhen(v => v == "mango", FailIfCalled<string, bool>)
                .MatchWhen(v => v == "apple", v => true)
                .MatchWhen(v => v == "pear", FailIfCalled<string, bool>)
                .Else(FailIfCalled<object, bool>)
            );

        [TestMethod]
        public void DoesntMatchWhenConditionIsFalse() => Assert.IsTrue(
            new OneOf<string, bool>("monkey")
                .MatchWhen(v => v == "mango", FailIfCalled<string, bool>)
                .MatchWhen(v => v == "apple", FailIfCalled<string, bool>)
                .MatchWhen(v => v == "pear", FailIfCalled<string, bool>)
                .Else(v => true)
            );

        TResult FailIfCalled<TValue, TResult>(TValue value)
        {
            Assert.Fail();
            throw new Exception("will never get here");
        }
    }
}
