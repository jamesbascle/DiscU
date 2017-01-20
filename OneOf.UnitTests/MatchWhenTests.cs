using System;
using NUnit.Framework;

namespace OneOf.Tests
{
    [TestFixture]
    public class MatchWhenTests
    {
        OneOf<string, int> CreateOneOf(object val) => new OneOf<string, int>(val);

        [Test]
        public void MatchWhenConditionIsTrue() => Assert.IsTrue(
            CreateOneOf("apple")
                .MatchWhen(v => v == "mango", FailIfCalled<string, bool>)
                .MatchWhen(v => v == "apple", v => true)
                .MatchWhen(v => v == "pear", FailIfCalled<string, bool>)
                .Else(FailIfCalled<object, bool>)
            );

        [Test]
        public void DoesntMatchWhenConditionIsFalse() => Assert.IsTrue(
            CreateOneOf("monkey")
                .MatchWhen(v => v == "mango", FailIfCalled<string, bool>)
                .MatchWhen(v => v == "apple", FailIfCalled<string, bool>)
                .MatchWhen(v => v == "pear", FailIfCalled<string, bool>)
                .Else(v => true)
            );

        TResult FailIfCalled<TValue, TResult>(TValue value)
        {
            Assert.Fail();
            throw new Exception("will never get here");  // needed so will compile
        }
    }
}
