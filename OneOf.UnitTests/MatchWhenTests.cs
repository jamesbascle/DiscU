using System;
using NUnit.Framework;

namespace OneOf.UnitTests
{
    [TestFixture]
    public class MatchWhenTests : OneOfTestBase
    {
        [Test]
        public void MatchWhenConditionIsTrue() => Assert.IsTrue(
            CreateOneOf("apple")
                .MatchWhen(v => v == "mango", v => FailIfCalled<bool>())
                .MatchWhen(v => v == "apple", v => true)
                .MatchWhen(v => v == "pear", v => FailIfCalled<bool>())
                .Else(v => FailIfCalled<bool>())
            );

        [Test]
        public void DoesntMatchWhenConditionIsFalse() => Assert.IsTrue(
            CreateOneOf("monkey")
                .MatchWhen(v => v == "mango", v => FailIfCalled<bool>())
                .MatchWhen(v => v == "apple", v => FailIfCalled<bool>())
                .MatchWhen(v => v == "pear", v => FailIfCalled<bool>())
                .Else(v => v.ToString() == "monkey")
            );
    }
}
