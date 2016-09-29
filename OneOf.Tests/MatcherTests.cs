using NUnit.Framework;
using System;

namespace OneOf.Tests
{
    public class MatcherTests
    {
        [Test]
        public void MatchCallsBoolFuncWhenBool()
        {
            var oneOf = (OneOf<string, bool>)true;

            var success = oneOf
                .Match((string str) => false)
                .Match((bool bln) => bln == true);

            Assert.AreEqual(true, success);
        }

        [Test]
        public void MatchCallsStringFuncWhenString()
        {
            var oneOf = (OneOf<string, bool>)"xyz";

            var success = oneOf
                .Match((string str) => str == "xyz")
                .Match((bool bln) => false);

            Assert.AreEqual(true, success);
        }

        [Test]
        public void MatchCallsDefaultFuncWhenNoMatch()
        {
            var oneOf = (OneOf<string, bool>)"xyz";

            var success = oneOf
                .Match((bool bln) => false)
                .Default(obj => obj.ToString() == "xyz");

            Assert.AreEqual(true, success);
        }

        [Test]
        public void MatchCallsOrThrowWhenNoMatch()
        {
            var oneOf = (OneOf<string, bool>)"xyz";

            Assert.Throws<InvalidOperationException>(() =>
                oneOf.Match((bool bln) => false)
                     .OrThrow(obj => new InvalidOperationException())
                );
        }
    }

}
