using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using System.Reflection;

namespace OneOf.UnitTests
{
    [TestFixture]
    public class CtorTests : OneOfTestBase
    {
        [Test]
        public void NullValueThrowsException()
        {
            Assert.Throws<ArgumentNullException>(() => CreateOneOf(null));
        }

        [Test]
        public void WrongTypeThrowsException()
        {
            Assert.Throws<ArgumentException>(() => new OneOf<int, string>(123.456));
        }
    }
}
