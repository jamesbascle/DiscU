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
            Assert.AreEqual(true, trueValue.AsT1);

            var falseValue = (OneOf<string, bool>)false;
            Assert.AreEqual(false, falseValue.As<bool>());
            Assert.AreEqual(false, falseValue.AsT1);
        }

        [Test]
        public void AsReturnsString()
        {
            var x = (OneOf<string, bool>)"xyz";
            Assert.AreEqual("xyz", x.As<string>());
            Assert.AreEqual("xyz", x.AsT0);
        }

        [Test]
        public void AsFailsWhenWrongType()
        {
            var str = (OneOf<string, bool>)"xyz";
            Assert.Throws<InvalidOperationException>(() => str.As<bool>());
            Assert.Throws<InvalidOperationException>(() => { var x = str.AsT1; return; });

            var bln = (OneOf<string, bool>)true;
            Assert.Throws<InvalidOperationException>(() => bln.As<string>());
            Assert.Throws<InvalidOperationException>(() => { var x = bln.AsT0; return; });
        }
    }

}
