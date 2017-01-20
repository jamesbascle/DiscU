using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using OneOf;

namespace OneOf.Tests
{
    [TestFixture]
    public class EqualityOperatorTests
    {
        OneOf<string, int> CreateOneOf(string val) => new OneOf<string, int>(val);

        [Test]
        public void EqualityOperatorReturnsTrueWhenSameValue() => Assert.IsTrue(CreateOneOf("A") == CreateOneOf("A"));

        [Test]
        public void EqualityOperatorReturnsFalseWhenDifferingValue() => Assert.IsFalse(CreateOneOf("A") == CreateOneOf("B"));

        [Test]
        public void EqualityOperatorReturnsFalseWhenNullValue() => Assert.IsFalse(CreateOneOf("A") == null);

        [Test]
        public void InequalityOperatorReturnsFalseWhenSameValue() => Assert.IsFalse(CreateOneOf("A") != CreateOneOf("A"));

        [Test]
        public void InequalityOperatorReturnsTrueWhenDifferingValue() => Assert.IsTrue(CreateOneOf("A") != CreateOneOf("B"));

        [Test]
        public void InequalityOperatorReturnsTrueWhenNullValue() => Assert.IsTrue(CreateOneOf("A") != null);


        [Test]
        public void EqualityOperatorReturnsTrueWhenSameValue2() => Assert.IsTrue(CreateOneOf("A") == "A");

        [Test]
        public void EqualityOperatorReturnsFalseWhenDifferingValue2() => Assert.IsFalse(CreateOneOf("A") == "B");

        [Test]
        public void InequalityOperatorReturnsFalseWhenSameValue2() => Assert.IsFalse(CreateOneOf("A") == "B");

        [Test]
        public void InequalityOperatorReturnsTrueWhenDifferingValue2() => Assert.IsTrue(CreateOneOf("A") != "B");
    }
}
