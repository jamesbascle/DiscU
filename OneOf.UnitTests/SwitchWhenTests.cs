using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace OneOf.Tests
{
    [TestClass]
    public class SwitchWhenTests
    {
        void FailIfCalled<T>(T value) => Assert.Fail();

        [TestMethod]
        public void SwitchWhenConditionIsTrue()
        {
            var success = false;

            new OneOf<string, int>("apple")
                .SwitchWhen(v => v == "mango", FailIfCalled)
                .SwitchWhen(v => v == "apple", v => success = true)
                .SwitchWhen(v => v == "pear", FailIfCalled)
                .Else(FailIfCalled);

            Assert.IsTrue(success);
        }

        [TestMethod]
        public void DoesNotSwitchWhenConditionIsFalse()
        {
            var success = false;

            new OneOf<string, int>("monkey")
                .SwitchWhen(v => v == "mango", FailIfCalled)
                .SwitchWhen(v => v == "apple", FailIfCalled)
                .SwitchWhen(v => v == "pear", FailIfCalled)
                .Else(v => success = true);

            Assert.IsTrue(success);
        }
    }
}
