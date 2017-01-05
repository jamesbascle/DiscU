using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace OneOf.Tests
{
    [TestClass]
    public class EqualsTests
    {
        OneOf<string, int> CreateOneOf(object val) => new OneOf<string, int>(val);

        [TestMethod]
        public void EqualsReturnsTrueWhenSameValue() => Assert.IsTrue(CreateOneOf(123).Equals(CreateOneOf(123)));

        [TestMethod]
        public void EqualsReturnsFalseWhenDifferingValue() => Assert.IsFalse(CreateOneOf(123).Equals(CreateOneOf(456)));

        [TestMethod]
        public void EqualsReturnsFalseWhenDifferingValueType() => Assert.IsFalse(CreateOneOf(123).Equals(CreateOneOf("A")));

        [TestMethod]
        public void EqualsReturnsFalseWhenNotOneOf() => Assert.IsFalse(CreateOneOf(123).Equals(123));

        [TestMethod]
        public void EqualsReturnsFalseWhenNullValue() => Assert.IsFalse(CreateOneOf(123).Equals(null));
    }
}
