using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace OneOf.Tests
{
    [TestClass]
    public class MatchTests
    {
        [TestMethod]
        public void MatchesOnIntType() => Assert.IsTrue(
            new OneOf<string, int>(123)
                .Match((string v) => false)
                .Match((int v) => v == 123)
            );

        [TestMethod]
        public void MatchesOnStringType() => Assert.IsTrue(
            new OneOf<string, int>("xyz")
                .Match((string v) => v == "xyz")
                .Match((int v) => false)
            );

        [TestMethod]
        public void ReturnsDefaultWhenMoMatch() => Assert.IsTrue(
            new OneOf<string, int>("xyz")
                .Match((int v) => false)
                .Else(v => v.ToString() == "xyz")
            );

        [TestMethod]
        public void NoMatchReturnsDefault() => Assert.IsTrue(
            new OneOf<string, int>("xyz")
                .Match((int v) => false)
                .Else(true)
            );

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void NoMatchThrowsException() => Assert.IsTrue(
            new OneOf<string, int>("xyz")
                .Match((int v) => false)
                .ElseThrow(v => new InvalidOperationException())
            );

        [TestMethod]
        public void MatchDoesntThrowException() => Assert.IsTrue(
            new OneOf<string, int>("xyz")
                .Match((string v) => true)
                .ElseThrow(v => new InvalidOperationException("should not be thrown"))
            );
    }
}
