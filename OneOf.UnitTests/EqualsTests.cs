using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace OneOf.UnitTests
{
    [TestFixture]
    public class EqualsTests : OneOfTestBase
    {
        [Test]
        public void EqualsReturnsTrueWhenSameValue() 
        {
            var oo1 = CreateOneOf("A");
            var oo2 = CreateOneOf("A");
            Assert.IsTrue(oo1.Equals(oo2));
        }

        [Test]
        public void EqualsReturnsFalseWhenDifferingValue() 
        {
            var oo1 = CreateOneOf("A");
            var oo2 = CreateOneOf("B");
            Assert.IsFalse(oo1.Equals(oo2));
        }

        [Test]
        public void EqualsReturnsFalseWhenDifferingValueType()
        {
            var oo1 = CreateOneOf("1");
            var oo2 = CreateOneOf(1);
            Assert.IsFalse(oo1.Equals(oo2));
        }

        [Test]
        public void EqualsReturnsFalseWhenNotOneOf()
        {
            var oo1 = CreateOneOf("A");
            Assert.IsFalse(oo1.Equals("A"));
        }

        [Test]
        public void EqualsReturnsFalseWhenNullValue()
        {
            var oo1 = CreateOneOf("A");
            Assert.IsFalse(oo1.Equals(null));
        }
    }
}
