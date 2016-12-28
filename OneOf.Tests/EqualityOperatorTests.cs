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
    public class EqualityOperatorTests
    {
        [Test]
        public void EqualsWorks()
        {
            var a1 = (OneOf<string, bool>)"A";
            var a2 = (OneOf<string, bool>)"A";
            var b1 = (OneOf<string, bool>)"B";
            //var b2 = (OneOf<string, bool>)"B";

            Assert.That(a1 == a2);
            Assert.That(!(a1 == b1));
            Assert.That(!(a1 == null));
        }

        [Test]
        public void NotEqualsWorks()
        {
            var a1 = (OneOf<string, bool>)"A";
            var a2 = (OneOf<string, bool>)"A";
            var b1 = (OneOf<string, bool>)"B";
            //var b2 = (OneOf<string, bool>)"B";

            Assert.That(!(a1 != a2));
            Assert.That(a1 != b1);
            Assert.That(a1 != null);
        }
    }
}
