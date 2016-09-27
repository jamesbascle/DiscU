using NUnit.Framework;

namespace OneOf.Tests
{
    public class MatcherTests
    {
        [Test]
        public void MatchCallsBoolFuncWhenBool()
        {
            var oneOf = (OneOf<string, bool>)true;

            var success = oneOf
                .Match((bool bln) => bln == true)
                .Match((string str) => false)
                ;

            Assert.AreEqual(true, success);
        }

        [Test]
        public void MatchCallsStringFuncWhenString()
        {
            var oneOf = (OneOf<string, bool>)"xyz";

            var success = oneOf
                .Match((string str) => str == "xyz")
                .Match((bool bln) => false)
                ;

            Assert.AreEqual(true, success);
        }

        [Test]
        public void MatchCallsOtherFuncWhenNoMatch()
        {
            var oneOf = (OneOf<string, bool>)"xyz";

            var success = oneOf
                .Match((bool bln) => false)
                .Otherwise(obj => obj.ToString() == "xyz")
                ;

            Assert.AreEqual(true, success);
        }
    }

}
