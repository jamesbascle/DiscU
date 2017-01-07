using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace OneOf.Tests
{
    [TestClass]
    public class EqualityOperatorTests
    {
        OneOf<string, int> CreateOneOf(object val) => new OneOf<string, int>(val);

        [TestMethod]
        public void EqualityOperatorReturnsTrueWhenSameValue() => Assert.IsTrue(CreateOneOf("A") == CreateOneOf("A"));

        [TestMethod]
        public void EqualityOperatorReturnsFalseWhenDifferingValue() => Assert.IsFalse(CreateOneOf("A") == CreateOneOf("B"));

        [TestMethod]
        public void EqualityOperatorReturnsFalseWhenNullValue() => Assert.IsFalse(CreateOneOf("A") == null);

        [TestMethod]
        public void InequalityOperatorReturnsFalseWhenSameValue() => Assert.IsFalse(CreateOneOf("A") != CreateOneOf("A"));

        [TestMethod]
        public void InequalityOperatorReturnsTrueWhenDifferingValue() => Assert.IsTrue(CreateOneOf("A") != CreateOneOf("B"));

        [TestMethod]
        public void InequalityOperatorReturnsTrueWhenNullValue() => Assert.IsTrue(CreateOneOf("A") != null);


        [TestMethod]
        public void EqualityOperatorReturnsTrueWhenSameValue2() => Assert.IsTrue(CreateOneOf("A") == "A");

        [TestMethod]
        public void EqualityOperatorReturnsFalseWhenDifferingValue2() => Assert.IsFalse(CreateOneOf("A") == "B");

        [TestMethod]
        public void InequalityOperatorReturnsFalseWhenSameValue2() => Assert.IsFalse(CreateOneOf("A") == "B");

        [TestMethod]
        public void InequalityOperatorReturnsTrueWhenDifferingValue2() => Assert.IsTrue(CreateOneOf("A") != "B");
    }
}
