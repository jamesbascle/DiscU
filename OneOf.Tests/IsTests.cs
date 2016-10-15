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
            var x = (OneOf<string, bool>) true;
            Assert.AreEqual(false, x.Is<string>());
            Assert.AreEqual(true, x.Is<bool>());
        }

        [Test]
        public void IsReturnsTrueWhenString()
        {
            var x = (OneOf<string, bool>) "xyz";
            Assert.AreEqual(true, x.Is<string>());
            Assert.AreEqual(false, x.Is<bool>());
        }


    }
}
