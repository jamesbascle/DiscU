using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace OneOf.Tests
{
    [TestClass]
    public class SwitchTests
    {
        void FailIfCalled<T>(T value) => Assert.Fail();

        [TestMethod]
        public void SwitchInt()
        {
            var success = false;

            new OneOf<string, int>(123)
                .Switch((string v) => FailIfCalled(v))
                .Switch((int v) => success = (v == 123));

            Assert.IsTrue(success);
        }

        [TestMethod]
        public void SwitchString()
        {
            var success = false;

            new OneOf<string, bool>("xyz")
                .Switch((string v) => success = (v == "xyz"))
                .Switch((bool v) => FailIfCalled(v));

            Assert.IsTrue(success);
        }

        [TestMethod]
        public void NoSwitchCallsDefault()
        {
            var success = false;

            new OneOf<string, int>("xyz")
                .Switch((int v) => FailIfCalled(v))
                .Else(v => success = v.ToString() == "xyz");

            Assert.IsTrue(success);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void NoSwitchThrowsException()
        {
            new OneOf<string, int>("xyz")
                .Switch((int v) => FailIfCalled(v))
                .ElseThrow(v => new InvalidOperationException());
        }

        [TestMethod]
        public void SwitchDoesntThrowException()
        {
            var success = false;

            new OneOf<string, int>("xyz")
                .Switch((string v) => success = (v == "xyz"))
                .ElseThrow(v => new InvalidOperationException());

            Assert.IsTrue(success);
        }

    }

}
