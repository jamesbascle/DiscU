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
            var oneOf = (OneOf<string, bool>) "True";

            var success = oneOf
                .MatchWhen(v => v == "True", v => true)
                .Else(false);

            Assert.AreEqual(true, success);
        }

        [Test]
        public void DoesntMatchWhenConditionIsFalse()
        {
            var oneOf = (OneOf<string, bool>)"False";

            var success = oneOf
                .MatchWhen(v => v == "True", v => false)
                .Else(true);

            Assert.AreEqual(true, success);
        }
    }
}
