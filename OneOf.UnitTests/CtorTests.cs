using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace OneOf.Tests
{
    [TestFixture]
    public class CtorTests
    {
        OneOf<string, int> CreateOneOf(object val) => new OneOf<string, int>(val);

        [Test]
        public void NullValueThrowsException() => Assert.Throws<ArgumentNullException>(() => CreateOneOf(null));

        [Test]
        public void WrongTypeThrowsException() => Assert.Throws<ArgumentException>(() => CreateOneOf(DateTime.Now));
    }
}
