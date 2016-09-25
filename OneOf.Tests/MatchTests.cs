using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace OneOf.Tests
{
    public class MatchTests
    {
        [Test]
        public void MatchCallsBoolFuncWhenBool()
        {
            var x = (OneOf<string, bool>)true;
            var success = x.Match(
                str => false,
                bln => (bln == true));
            Assert.AreEqual(true, success);
        }

        [Test]
        public void MatchCallsStringFuncWhenString()
        {
            var x = (OneOf<string, bool>)"xyz";
            var success = x.Match(
                str => (str == "xyz"),
                bln => false);
            Assert.AreEqual(true, success);
        }

        [Test]
        public void MatchCallsOtherFuncWhenNoMatch()
        {
            var x = (OneOf<string, bool>)"xyz";
            var success = x.Match(
                otherwise: () => true);
            Assert.AreEqual(true, success);
        }
    }

}
