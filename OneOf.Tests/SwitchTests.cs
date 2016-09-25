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
            x.Switch(
                str => Assert.Fail(),
                bln => success = (bln == true));
            Assert.AreEqual(true, success);
        }

        [Test]
        public void SwitchCallsStringActionWhenString()
        {
            var success = false;

            var x = (OneOf<string, bool>)"xyz";
            x.Switch(
                str => success = (str == "xyz"),
                bln => Assert.Fail());
            Assert.AreEqual(true, success);
        }

        [Test]
        public void SwitchCallsOtherActionWhenNoMatch()
        {
            var success = false;

            var x = (OneOf<string, bool>)"xyz";
            x.Switch(
                otherwise: () => success = true);
            Assert.AreEqual(true, success);
        }
    }

}
