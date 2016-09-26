using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace OneOf.Tests
{
    public class AsTests
    {
        [Test]
        public void AsReturnsBool()
        {
            var trueValue = (OneOf<string, bool>)true;
            Assert.AreEqual(true, trueValue.As<bool>());

            var falseValue = (OneOf<string, bool>)false;
            Assert.AreEqual(false, falseValue.As<bool>());
        }

        [Test]
        public void AsReturnsString()
        {
            var x = (OneOf<string, bool>)"xyz";
            Assert.AreEqual("xyz", x.As<string>());
        }

        [Test]
        public void AsFailsWhenWrongType()
        {
            var str = (OneOf<string, bool>)"xyz";
            Assert.Throws<InvalidOperationException>(() => str.As<bool>());
            Assert.Throws<InvalidOperationException>(() => str.As<double>());

            var bln = (OneOf<string, bool>)true;
            Assert.Throws<InvalidOperationException>(() => bln.As<string>());
        }
    }

}
