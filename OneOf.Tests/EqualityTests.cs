using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace OneOf.Tests
{
    [TestFixture]
    [Category("Unit")]
    public class EqualityTests
    {
        [Test]
        public void EqualsWorksWithBool()
        {
            var same1 = (OneOf<string, bool>)true;
            var same2 = (OneOf<string, bool>)true;
            var diff1 = (OneOf<string, bool>)false;
            var diff2 = (OneOf<string, bool>)false;

            Assert.IsTrue(same1.Equals(same2));
            Assert.IsFalse(same1.Equals(diff2));

            Assert.IsTrue(same1 == same2);
            Assert.IsFalse(same1 == diff2);

            Assert.IsFalse(same1 != same2);
            Assert.IsTrue(same1 != diff2);
        }

        [Test]
        public void EqualsWorksWithString()
        {
            var same1 = (OneOf<string, bool>)"SAME";
            var same2 = (OneOf<string, bool>)"SAME";
            var diff1 = (OneOf<string, bool>)"DIFF";
            var diff2 = (OneOf<string, bool>)"DIFF";

            Assert.IsTrue(same1.Equals(same2));
            Assert.IsFalse(same1.Equals(diff2));

            Assert.IsTrue(same1 == same2);
            Assert.IsFalse(same1 == diff2);

            Assert.IsFalse(same1 != same2);
            Assert.IsTrue(same1 != diff2);
        }

        [Test]
        public void EqualsDoesNotRequireSameType()
        {
            var v1 = (OneOf<string, bool>)"x";
            var v2 = (OneOf<string, bool, int>)"x";

            Assert.IsFalse(v1.Equals(null));
            Assert.IsTrue(v1.Equals(v2));
        }

        [Test]
        public void OpEquals_OneOfVsOneOf_SameType()
        {
            var v1 = (OneOf<string, bool>)"x";
            var v2 = (OneOf<string, bool>)"x";

            Assert.IsTrue(v1 == v2);
            Assert.IsFalse(v1 != v2);
        }

        [Test]
        public void OpEquals_OneOfVsOneOf_DiffType()
        {
            var v1 = (OneOf<string, bool>)"x";
            var v2 = (OneOf<string, int>)"x";

            Assert.IsTrue(v1 == v2);
            Assert.IsFalse(v1 != v2);
        }

        [Test]
        public void OpEquals_OneOfVsValue()
        {
            var v1 = (OneOf<string, bool>)"x";
            var v2 = "x";

            Assert.IsTrue(v1 == v2);
            Assert.IsFalse(v1 != v2);
        }

        [Test]
        public void OpEquals_ValueVsOneOf()
        {
            var v1 = "x";
            var v2 = (OneOf<string, bool>)"x";

            Assert.IsTrue(v1 == v2);
            Assert.IsFalse(v1 != v2);
        }
    }
}
