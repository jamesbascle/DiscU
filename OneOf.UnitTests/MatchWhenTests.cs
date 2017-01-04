using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace OneOf.Tests
{
    [TestClass]
    public class MatchWhenTests
    {
        [TestMethod]
        public void MatchWhenConditionIsTrue()
        {
            var oneOf = (OneOf<string, bool>) "apple";

            var success = oneOf
                .MatchWhen(v => v == "mango", v => false)
                .MatchWhen(v => v == "apple", v => true)
                .MatchWhen(v => v == "pear", v => false)
                .Else(false);

            Assert.IsTrue(success);
        }

        [TestMethod]
        public void DoesntMatchWhenConditionIsFalse()
        {
            var oneOf = (OneOf<string, bool>)"monkey";

            var success = oneOf
                .MatchWhen(v => v == "mango", v => false)
                .MatchWhen(v => v == "apple", v => false)
                .MatchWhen(v => v == "pear", v => false)
                .Else(true);

            Assert.IsTrue(success);
        }
    }
}
