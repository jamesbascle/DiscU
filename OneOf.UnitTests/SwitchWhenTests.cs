using System;
using NUnit.Framework;

namespace OneOf.Tests
{
    [TestFixture]
    public class SwitchWhenTests
    {
        void FailIfCalled<T>(T value) => Assert.Fail();

        OneOf<string, int> CreateOneOf(object val) => new OneOf<string, int>(val);

        [Test]
        public void SwitchWhenConditionIsTrue()
        {
            var success = false;

            CreateOneOf("apple")
                .SwitchWhen(v => v == "mango", FailIfCalled)
                .SwitchWhen(v => v == "apple", v => success = true)
                .SwitchWhen(v => v == "pear", FailIfCalled)
                .Else(FailIfCalled);

            Assert.IsTrue(success);
        }

        [Test]
        public void DoesNotSwitchWhenConditionIsFalse()
        {
            var success = false;

            CreateOneOf("monkey")
                .SwitchWhen(v => v == "mango", FailIfCalled)
                .SwitchWhen(v => v == "apple", FailIfCalled)
                .SwitchWhen(v => v == "pear", FailIfCalled)
                .Else(v => success = true);

            Assert.IsTrue(success);
        }
    }
}
