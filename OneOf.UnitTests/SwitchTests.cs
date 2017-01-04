using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace OneOf.Tests
{
    [TestClass]
    public class SwitchTests
    {
        [TestMethod]
        public void SwitchBool()
        {
            var success = false;
            var oneOf = (OneOf<string, bool>)true;

            oneOf.Switch((string str) => Assert.Fail())
                 .Switch((bool bln) => success = (bln == true));

            Assert.IsTrue(success);
        }

        [TestMethod]
        public void SwitchString()
        {
            var success = false;
            var oneOf = (OneOf<string, bool>)"xyz";

            oneOf.Switch((string str) => success = (str == "xyz"))
                 .Switch((bool bln) => Assert.Fail());

            Assert.IsTrue(success);
        }

        [TestMethod]
        public void NoSwitchCallsDefault()
        {
            var success = false;
            var oneOf = (OneOf<string, bool>)"xyz";

            oneOf.Switch((bool bln) => Assert.Fail())
                 .Else(v => success = v.ToString() == "xyz");

            Assert.IsTrue(success);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void NoSwitchThrowsException()
        {
            var oneOf = (OneOf<string, bool>)"xyz";

            oneOf.Switch((bool bln) => Assert.Fail())
                 .ElseThrow(obj => new InvalidOperationException());
        }
    }

}
