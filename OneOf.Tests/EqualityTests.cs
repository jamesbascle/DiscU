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
            var true1 = (OneOf<string, bool>)true;
            var true2 = (OneOf<string, bool>)true;
            var false1 = (OneOf<string, bool>)false;
            var false2 = (OneOf<string, bool>)false;

            Assert.That(true1.Equals(true2));
            Assert.That(!true1.Equals(false2));
        }

        [Test]
        public void EqualsWorksWithString()
        {
            var a1 = (OneOf<string, bool>)"A";
            var a2 = (OneOf<string, bool>)"A";
            var b1 = (OneOf<string, bool>)"B";
            var b2 = (OneOf<string, bool>)"B";

            Assert.That(a1.Equals(a2));
            Assert.That(!a1.Equals(b2));
        }

        [Test]
        public void EqualsRequiresSameType()
        {
            var a1 = new OneOf<string, bool>("x");
            var a2 = new OneOf<string, bool>("x");
            var b1 = new OneOf<string, bool, int>("x");

            Assert.That(a1.Equals(a2));
            Assert.That(!a1.Equals(null));
            Assert.That(!a1.Equals(b1));
        }
    }
}
