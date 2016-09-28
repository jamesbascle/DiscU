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
        public void SwitchCallsBoolActionWhenBool()
        {
            var success = false;
            var x = (OneOf<string, bool>)true;

            x.Switch((string str) => Assert.Fail())
                .Switch((bool bln) => success = (bln == true));

            Assert.AreEqual(true, success);
        }

        [Test]
        public void SwitchCallsStringActionWhenString()
        {
            var success = false;
            var x = (OneOf<string, bool>)"xyz";

            x.Switch((string str) => success = (str == "xyz"))
                .Switch((bool bln) => Assert.Fail());

            Assert.AreEqual(true, success);
        }

        [Test]
        public void SwitchCallsOtherActionWhenNoMatch()
        {
            var success = false;
            var x = (OneOf<string, bool>)"xyz";

            x.Switch((bool bln) => Assert.Fail())
                .Otherwise(v => success = v.ToString() == "xyz");

            Assert.AreEqual(true, success);
        }
    }

}
