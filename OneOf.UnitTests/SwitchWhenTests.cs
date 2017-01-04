using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace OneOf.Tests
{
    [TestClass]
    public class SwitchWhenTests
    {
        [TestMethod]
        public void SwitchWhenConditionIsTrue()
        {
            var success = false;
            var switches = 0;

            var oneOf = (OneOf<string, bool>) "apple";

            oneOf.SwitchWhen(v => v == "mango", v => { switches++; success = false; })
                 .SwitchWhen(v => v == "apple", v => { switches++; success = true; })
                 .SwitchWhen(v => v == "pear", v => { switches++; success = false; })
                 .Else(v => success = false);

            Assert.IsTrue(switches == 1 && success);
        }

        [TestMethod]
        public void DoesNotSwitchWhenConditionIsFalse()
        {
            var success = false;
            var switches = 0;

            var oneOf = (OneOf<string, bool>)"monkey";

            oneOf.SwitchWhen(v => v == "mango", v => { switches++; success = false; })
                 .SwitchWhen(v => v == "apple", v => { switches++; success = false; })
                 .SwitchWhen(v => v == "pear", v => { switches++; success = false; })
                 .Else(v => success = true);

            Assert.IsTrue(switches == 0 && success);
        }
    }
}
