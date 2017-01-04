using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace OneOf.Tests
{
    [TestClass]
    public class MatcherTests
    {
        [TestMethod]
        public void MatchBool()
        {
            var oneOf = (OneOf<string, bool>)true;

            var success = oneOf
                .Match((string str) => false)
                .Match((bool bln) => bln == true);

            Assert.IsTrue(success);
        }

        [TestMethod]
        public void MatchString()
        {
            var oneOf = (OneOf<string, bool>)"xyz";

            var success = oneOf
                .Match((string str) => str == "xyz")
                .Match((bool bln) => false);

            Assert.IsTrue(success);
        }

        [TestMethod]
        public void NoMatchReturnsDefault()
        {
            var oneOf = (OneOf<string, bool>)"xyz";

            var success = oneOf
                .Match((bool bln) => false)
                .Else(obj => obj.ToString() == "xyz");

            Assert.IsTrue(success);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void NoMatchThrowsException()
        {
            var oneOf = (OneOf<string, bool>)"xyz";

            oneOf.Match((bool bln) => false)
                .ElseThrow(obj => new InvalidOperationException());
        }

    }
}
