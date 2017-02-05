using System;
using NUnit.Framework;

namespace OneOf.UnitTests
{
    [TestFixture]
    public class SwitchTests : OneOfTestBase
    {
        [Test]
        public void SwitchesWhenInt()
        {
            var called = false;

            CreateOneOf(123).Switch((string v) => FailIfCalled())
                            .Switch((int v) => { FailIf(v != 123); called = true; });

            Assert.IsTrue(called);
        }

        [Test]
        public void CallsDefaultActionWhenNoMatch()
        {
            var called = false;

            CreateOneOf("xyz").Switch((int v) => FailIfCalled())
                              .Else(v => { FailIf(v.ToString() != "xyz"); called = true; });

            Assert.IsTrue(called);
        }

        [Test]
        public void DoesntThrowExceptionWhenMatch()
        {
            var called = false;

            CreateOneOf("xyz").Switch((string v) => { FailIf(v.ToString() != "xyz"); called = true; })
                              .ElseThrow(v => new InvalidOperationException());

            Assert.IsTrue(called);
        }

        [Test]
        public void ThrowsExceptionWhenNoMatch()
        {
            Assert.Throws<InvalidOperationException>(() =>
                CreateOneOf("xyz").Switch((int v) => FailIfCalled())
                                  .ElseThrow(v => new InvalidOperationException())
                );
        }


    }

}
