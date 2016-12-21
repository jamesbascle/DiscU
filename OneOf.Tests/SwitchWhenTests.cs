using NUnit.Framework;
using System;

namespace OneOf.Tests
{
    [TestFixture]
    [Category("Unit")]
    public class SwitchWhenTests
    {
        [Test]
        public void SwitchWhenConditionIsTrue()
        {
            var success = false;
            var switches = 0;

            var oneOf = (OneOf<string, bool>) "apple";

            oneOf.SwitchWhen(v => v == "mango", v => { switches++; success = false; })
                 .SwitchWhen(v => v == "apple", v => { switches++; success = true; })
                 .SwitchWhen(v => v == "pear", v => { switches++; success = false; })
                 .Else(v => success = false);

            Assert.AreEqual(true, switches == 1 && success);
        }

        [Test]
        public void DoesNotSwitchWhenConditionIsFalse()
        {
            var success = false;
            var switches = 0;

            var oneOf = (OneOf<string, bool>)"monkey";

            oneOf.SwitchWhen(v => v == "mango", v => { switches++; success = false; })
                 .SwitchWhen(v => v == "apple", v => { switches++; success = false; })
                 .SwitchWhen(v => v == "pear", v => { switches++; success = false; })
                 .Else(v => success = true);

            Assert.AreEqual(true, switches == 0 && success);
        }
    }
}
