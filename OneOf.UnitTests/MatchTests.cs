using System;
using NUnit.Framework;

namespace OneOf.Tests
{
    [TestFixture]
    public class MatchTests
    {
        OneOf<string, int> CreateOneOf(object val) => new OneOf<string, int>(val);

        [Test]
        public void MatchesOnType() => Assert.IsTrue(
            CreateOneOf("xyz")
                .Match((string v) => v == "xyz")
                .Match((int v) => false)
            );

        [Test]
        public void ReturnsDefaultWhenNoMatch() => Assert.IsTrue(
            CreateOneOf("xyz")
                .Match((int v) => false)
                .Else(v => v.ToString() == "xyz")
            );

        [Test]
        public void ReturnsDefaultWhenMoMatch2() => Assert.IsTrue(
            CreateOneOf("xyz")
                .Match((int v) => false)
                .Else(true)
            );

        [Test]
        public void ThrowsExceptionWhenNoMatch() => Assert.Throws<InvalidOperationException>(() =>
            CreateOneOf("xyz")
                .Match((int v) => false)
                .ElseThrow(v => new InvalidOperationException())
            );

        [Test]
        public void DoesntThrowExceptionWhenNoMatch() => Assert.IsTrue(
            CreateOneOf("xyz")
                .Match((string v) => true)
                .ElseThrow(v => new InvalidOperationException("should not be thrown"))
            );
    }
}
