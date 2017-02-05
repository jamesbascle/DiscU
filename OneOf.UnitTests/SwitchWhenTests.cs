using System;
using NUnit.Framework;

namespace OneOf.UnitTests
{
    [TestFixture]
    public class SwitchWhenTests : OneOfTestBase
    {
        [Test]
        public void SwitchWhenConditionIsTrue()
        {
            var success = false;

            CreateOneOf("apple")
                .SwitchWhen(v => v == "mango", v => FailIfCalled())
                .SwitchWhen(v => v == "apple", v => success = true)
                .SwitchWhen(v => v == "pear", v => FailIfCalled())
                .Else(v => FailIfCalled());

            Assert.IsTrue(success);
        }

        [Test]
        public void DoesNotSwitchWhenConditionIsFalse()
        {
            var success = false;

            CreateOneOf("monkey")
                .SwitchWhen(v => v == "mango", v => FailIfCalled())
                .SwitchWhen(v => v == "apple", v => FailIfCalled())
                .SwitchWhen(v => v == "pear", v => FailIfCalled())
                .Else(v => success = v.ToString() == "monkey");

            Assert.IsTrue(success);
        }
    }
}
