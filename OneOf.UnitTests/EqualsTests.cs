using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace OneOf.Tests
{
    [TestFixture]
    public class EqualsTests
    {
        OneOf<string, int> CreateOneOf(object val) => new OneOf<string, int>(val);

        [Test]
        public void EqualsReturnsTrueWhenSameValue() => Assert.IsTrue(CreateOneOf(123).Equals(CreateOneOf(123)));

        [Test]
        public void EqualsReturnsFalseWhenDifferingValue() => Assert.IsFalse(CreateOneOf(123).Equals(CreateOneOf(456)));

        [Test]
        public void EqualsReturnsFalseWhenDifferingValueType() => Assert.IsFalse(CreateOneOf(123).Equals(CreateOneOf("A")));

        [Test]
        public void EqualsReturnsFalseWhenNotOneOf() => Assert.IsFalse(CreateOneOf(123).Equals(123));

        [Test]
        public void EqualsReturnsFalseWhenNullValue() => Assert.IsFalse(CreateOneOf(123).Equals(null));
    }
}
