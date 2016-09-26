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

            x.Switch()
                .When<string>(str => Assert.Fail())
                .When<bool>(bln => success = (bln == true));

            Assert.AreEqual(true, success);
        }

        [Test]
        public void SwitchCallsStringActionWhenString()
        {
            var success = false;
            var x = (OneOf<string, bool>)"xyz";

            x.Switch()
                .When<string>(str => success = (str == "xyz"))
                .When<bool>(bln => Assert.Fail());

            Assert.AreEqual(true, success);
        }

        [Test]
        public void SwitchCallsOtherActionWhenNoMatch()
        {
            var success = false;
            var x = (OneOf<string, bool>)"xyz";

            x.Switch()
                .Otherwise(v => success = true);

            Assert.AreEqual(true, success);
        }
    }

}
