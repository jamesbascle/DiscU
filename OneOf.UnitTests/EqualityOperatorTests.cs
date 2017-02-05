using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using OneOf;

namespace OneOf.UnitTests
{
    [TestFixture]
    public class EqualityOperatorTests : OneOfTestBase
    {
        [Test]
        public void EqualityOperatorReturnsTrueWhenSameValue() => RunTestForAllOneOfTypes(cn =>
        {
            dynamic oo1 = CreateOneOfCn(cn, CreateCn(cn, "A"));
            dynamic oo2 = CreateOneOfCn(cn, CreateCn(cn, "A"));
            Assert.IsTrue(oo1 == oo2);
        });

        [Test]
        public void EqualityOperatorReturnsFalseWhenDifferingValue() => RunTestForAllOneOfTypes(cn =>
        {
            dynamic oo1 = CreateOneOfCn(cn, CreateCn(cn, "A"));
            dynamic oo2 = CreateOneOfCn(cn, CreateCn(cn, "B"));
            Assert.IsFalse(oo1 == oo2);
        });

        [Test]
        public void EqualityOperatorReturnsFalseWhenNullValue() => RunTestForAllOneOfTypes(cn =>
        {
            dynamic oo1 = CreateOneOfCn(cn, CreateCn(cn, "A"));
            Assert.IsFalse(oo1 == null);
        }); 

        // These should implicitly cast the string literals into OneOfs before doing the comparison

        [Test]
        public void EqualityOperatorReturnsTrueWhenSameValue2() => Assert.IsTrue(new OneOf<int, string>("A") == "A");

        [Test]
        public void EqualityOperatorReturnsFalseWhenDifferingValue2() => Assert.IsFalse(new OneOf<int, string>("A") == "B");
    }
}
