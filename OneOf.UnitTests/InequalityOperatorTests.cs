using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace OneOf.UnitTests
{
    [TestFixture]
    public class InequalityOperatorTests : OneOfTestBase
    {
        [Test]
        public void InequalityOperatorReturnsFalseWhenSameValue() => RunTestForAllOneOfTypes(cn =>
        {
            dynamic oo1 = CreateOneOfCn(cn, CreateCn(cn, "A"));
            dynamic oo2 = CreateOneOfCn(cn, CreateCn(cn, "A"));
            Assert.IsFalse(oo1 != oo2);
        });

        [Test]
        public void InequalityOperatorReturnsTrueWhenDifferingValue() => RunTestForAllOneOfTypes(cn =>
        {
            dynamic oo1 = CreateOneOfCn(cn, CreateCn(cn, "A"));
            dynamic oo2 = CreateOneOfCn(cn, CreateCn(cn, "B"));
            Assert.IsTrue(oo1 != oo2);
        });

        [Test]
        public void InequalityOperatorReturnsFalseWhenNullValue() => RunTestForAllOneOfTypes(cn =>
        {
            dynamic oo1 = CreateOneOfCn(cn, CreateCn(cn, "A"));
            Assert.IsTrue(oo1 != null);
        }); 

        // These should implicitly cast the string literals into OneOfs before doing the comparison

        [Test]
        public void InequalityOperatorReturnsFalseWhenSameValue2() => Assert.IsFalse(new OneOf<int, string>("A") != "A");

        [Test]
        public void InequalityOperatorReturnsTrueWhenDifferingValue2() => Assert.IsTrue(new OneOf<int, string>("A") != "B");
    }
}
