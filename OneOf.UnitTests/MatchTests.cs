using System;
using NUnit.Framework;

namespace OneOf.UnitTests
{
    [TestFixture]
    public class MatchTests : OneOfTestBase
    {
        [Test]
        public void ReturnsDefaultWhenNoMatch()
        {
            var oo = new OneOf<int, string>(123);

            var ret = oo.Match((string x) => FailIfCalled<string>())
                        .Else((object x) => "ELSE:" + x.ToString());

            Assert.AreEqual("ELSE:123", ret);
        }


        [Test]
        public void ThrowsExceptionWhenNoMatch()
        {
            var oo = new OneOf<int, string>(123);

            TestDelegate test = () =>
            {
                var ret = oo
                    .Match((string x) => FailIfCalled<string>())
                    .ElseThrow((object x) => new InvalidOperationException());
            };

            NUnit.Framework.Assert.Throws<InvalidOperationException>(test);
        }

        [Test]
        public void ReturnsValueWhenMatch()
        {
            var oo = new OneOf<int, string>(123);

            var ret = oo.Match((string x) => FailIfCalled<string>())
                        .Match((int x) => "OK:" + x.ToString());

            Assert.AreEqual("OK:123", ret);
        }
    }
}
