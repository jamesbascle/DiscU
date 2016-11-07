using NUnit.Framework;
using System;

namespace OneOf.Tests
{
    [TestFixture]
    [Category("Unit")]
    public class ImplicitConversionTests
    {
        [Test]
        public void FailsWhenValueIsNull()
        {
            OneOf<int, string> x;

            Assert.Throws(typeof(ArgumentNullException),
                () => x = null
                );
        }
    }
}