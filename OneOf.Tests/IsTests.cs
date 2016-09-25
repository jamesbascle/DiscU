using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace OneOf.Tests
{
    public class IsTests
    {
        [Test]
        public void IsReturnsTrueWhenBool()
        {
            var x = (OneOf<string, bool>)true;
            Assert.AreEqual(false, x.Is<string>());
            Assert.AreEqual(true, x.Is<bool>());
            Assert.AreEqual(false, x.IsT0);
            Assert.AreEqual(true, x.IsT1);
        }

        [Test]
        public void IsReturnsTrueWhenString()
        {
            var x = (OneOf<string, bool>)"xyz";
            Assert.AreEqual(true, x.Is<string>());
            Assert.AreEqual(false, x.Is<bool>());
            Assert.AreEqual(true, x.IsT0);
            Assert.AreEqual(false, x.IsT1);
        }
    }

}
