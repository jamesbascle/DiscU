using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace OneOf.UnitTests
{
    [TestFixture]
    public class EqualsTests : OneOfTestBase
    {
        [Test]
        public void EqualsReturnsTrueWhenSameValue() => RunTestForAllOneOfTypes(cn =>
        {
            dynamic oo1 = CreateOneOfCn(cn, CreateCn(cn, "A"));
            dynamic oo2 = CreateOneOfCn(cn, CreateCn(cn, "A"));
            Assert.IsTrue(oo1.Equals(oo2));
        });

        [Test]
        public void EqualsReturnsFalseWhenDifferingValue() => RunTestForAllOneOfTypes(cn =>
        {
            dynamic oo1 = CreateOneOfCn(cn, CreateCn(cn, "A"));
            dynamic oo2 = CreateOneOfCn(cn, CreateCn(cn, "B"));
            Assert.IsFalse(oo1.Equals(oo2));
        });

        [Test]
        public void EqualsReturnsFalseWhenDifferingValueType() => RunTestForAllOneOfTypes(cn =>
        {
            dynamic oo1 = CreateOneOfCn(cn, CreateCn(cn, "A"));
            dynamic oo2 = new OneOf<int, string>("A");
            Assert.IsFalse(oo1.Equals(oo2));
        });

        [Test]
        public void EqualsReturnsFalseWhenNotOneOf() => RunTestForAllOneOfTypes(cn =>
        {
            dynamic oo1 = CreateOneOfCn(cn, CreateCn(cn, "A"));
            Assert.IsFalse(oo1.Equals("A"));
        });

        [Test]
        public void EqualsReturnsFalseWhenNullValue() => RunTestForAllOneOfTypes(cn =>
        {
            dynamic oo1 = CreateOneOfCn(cn, CreateCn(cn, "A"));
            Assert.IsFalse(oo1.Equals(null));
        });
    }
}
