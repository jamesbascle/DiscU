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
        public void NullValueThrowsException() => RunTestForAllOneOfTypes(cn => 
            Assert.Throws<ArgumentNullException>(() => CreateOneOfCn(cn, null))
            );

        [Test]
        public void WrongTypeThrowsException() => RunTestForAllOneOfTypes(cn =>
            Assert.Throws<ArgumentException>(() => CreateOneOfCn(cn, "this string should cause ctor to fail as it only supports C1..C9"))
            );
    }
}
