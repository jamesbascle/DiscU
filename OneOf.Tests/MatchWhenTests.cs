using NUnit.Framework;
using System;

namespace OneOf.Tests
{
    [TestFixture]
    [Category("Unit")]
    public class MatchWhenTests
    {
        [Test]
        public void MatchWhenConditionIsTrue()
        {
            var oneOf = (OneOf<string, bool>) "apple";

            var success = oneOf
                .MatchWhen(v => v == "mango", v => false)
                .MatchWhen(v => v == "apple", v => true)
                .MatchWhen(v => v == "pear", v => false)
                .Else(false);

            Assert.AreEqual(true, success);
        }

        [Test]
        public void DoesntMatchWhenConditionIsFalse()
        {
            var oneOf = (OneOf<string, bool>)"monkey";

            var success = oneOf
                .MatchWhen(v => v == "mango", v => false)
                .MatchWhen(v => v == "apple", v => false)
                .MatchWhen(v => v == "pear", v => false)
                .Else(true);

            Assert.AreEqual(true, success);
        }
    }
}
