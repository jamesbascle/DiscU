using NUnit.Framework;
using System;

namespace OneOf.Tests
{
    [TestFixture]
    [Category("Unit")]
    public class MatcherTests
    {
        [Test]
        public void MatchBool()
        {
            var oneOf = (OneOf<string, bool>) true;

            var success = oneOf
                .Match((string str) => false)
                .Match((bool bln) => bln == true);

            Assert.AreEqual(true, success);
        }

        [Test]
        public void MatchString()
        {
            var oneOf = (OneOf<string, bool>) "xyz";

            var success = oneOf
                .Match((string str) => str == "xyz")
                .Match((bool bln) => false);

            Assert.AreEqual(true, success);
        }

        [Test]
        public void NoMatchReturnsDefault()
        {
            var oneOf = (OneOf<string, bool>) "xyz";

            var success = oneOf
                .Match((bool bln) => false)
                .Else(obj => obj.ToString() == "xyz");

            Assert.AreEqual(true, success);
        }

        [Test]
        public void NoMatchThrowsException()
        {
            var oneOf = (OneOf<string, bool>) "xyz";

            Assert.Throws<InvalidOperationException>(() =>
                oneOf.Match((bool bln) => false)
                    .ElseThrow(obj => new InvalidOperationException())
                );
        }

    }
}
