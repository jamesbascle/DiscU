using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace OneOf.Tests
{
    public class SwitchTests
    {
        [Test]
        public void SwitchBool()
        {
            var success = false;
            var oneOf = (OneOf<string, bool>)true;

            oneOf.Switch((string str) => Assert.Fail())
                 .Switch((bool bln) => success = (bln == true));

            Assert.AreEqual(true, success);
        }

        [Test]
        public void SwitchString()
        {
            var success = false;
            var oneOf = (OneOf<string, bool>)"xyz";

            oneOf.Switch((string str) => success = (str == "xyz"))
                 .Switch((bool bln) => Assert.Fail());

            Assert.AreEqual(true, success);
        }

        [Test]
        public void NoSwitchCallsDefault()
        {
            var success = false;
            var oneOf = (OneOf<string, bool>)"xyz";

            oneOf.Switch((bool bln) => Assert.Fail())
                 .Else(v => success = v.ToString() == "xyz");

            Assert.AreEqual(true, success);
        }

        [Test]
        public void NoSwitchThrowsException()
        {
            var oneOf = (OneOf<string, bool>)"xyz";

            Assert.Throws<InvalidOperationException>(() =>
                oneOf.Switch((bool bln) => Assert.Fail())
                     .ElseThrow(obj => new InvalidOperationException())
                );
        }
    }

}
