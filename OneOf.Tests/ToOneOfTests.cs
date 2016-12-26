using NUnit.Framework;

namespace OneOf.Tests
{
    [TestFixture]
    [Category("Unit")]
    public class ToOneOfTests
    {
        [Test]
        public void ToOneOf_WhenCalledOnExistingOneOf_ProperlyConvertsToAnotherOneOf()
        {
            OneOf<int, string, double> oo = 1;

            var a = oo.ToOneOf<byte, long, int>();

            Assert.IsTrue(a.Match((int v) => true).Else(false));
        }
    }
}