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
        [TestMethod]
        public void EqualsWorks()
        {
            var a1 = (OneOf<string, bool>)"A";
            var a2 = (OneOf<string, bool>)"A";
            var b1 = (OneOf<string, bool>)"B";

            Assert.IsTrue(a1 == a2);
            Assert.IsFalse(a1 == b1);
            Assert.IsFalse(a1 == null);
        }

        [TestMethod]
        public void NotEqualsWorks()
        {
            var a1 = (OneOf<string, bool>)"A";
            var a2 = (OneOf<string, bool>)"A";
            var b1 = (OneOf<string, bool>)"B";

            Assert.IsFalse(a1 != a2);
            Assert.IsTrue(a1 != b1);
            Assert.IsTrue(a1 != null);
        }
    }
}
