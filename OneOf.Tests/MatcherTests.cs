using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace OneOf.Tests
{
    public class MatcherTests
    {
        [Test]
        public void MatchCallsBoolFuncWhenBool()
        {
            var oneOf = (OneOf<string, bool>)true;

            var success = oneOf
                .Match<bool>()
                .When((string str) => false)
                .When((bool bln) => bln == true)
                .Otherwise(v => false)
                .Result;

            Assert.AreEqual(true, success);
        }

        [Test]
        public void MatchCallsStringFuncWhenString()
        {
            var oneOf = (OneOf<string, bool>)"xyz";

            var success = oneOf
                .Match<bool>()
                .When((string str) => str == "xyz")
                .When((bool bln) => false)
                .Otherwise(v => false)
                .Result;

            Assert.AreEqual(true, success);
        }

        [Test]
        public void MatchCallsOtherFuncWhenNoMatch()
        {
            var oneOf = (OneOf<string, bool>)"xyz";

            var success = oneOf
                .Match<bool>()
                .Otherwise(obj => obj.ToString() == "xyz")
                .Result;

            Assert.AreEqual(true, success);
        }
    }

}
