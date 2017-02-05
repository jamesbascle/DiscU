using System;
using NUnit.Framework;

namespace OneOf.UnitTests
{
    [TestFixture]
    public class MatchTests : OneOfTestBase
    {
        Func<T1, TRet> Fun<T1, TRet>(Func<T1, TRet> x) => x;

        dynamic MatchCn(int cn, dynamic oo)
        {
            switch (cn)
            {
                case 9: return oo.Match(Fun((C9 v) => "9:" + v.Value));
                case 8: return oo.Match(Fun((C8 v) => "8:" + v.Value));
                case 7: return oo.Match(Fun((C7 v) => "7:" + v.Value));
                case 6: return oo.Match(Fun((C6 v) => "6:" + v.Value));
                case 5: return oo.Match(Fun((C5 v) => "5:" + v.Value));
                case 4: return oo.Match(Fun((C4 v) => "4:" + v.Value));
                case 3: return oo.Match(Fun((C3 v) => "3:" + v.Value));
                case 2: return oo.Match(Fun((C2 v) => "2:" + v.Value));
                case 1: return oo.Match(Fun((C1 v) => "1:" + v.Value));
                default: throw new ArgumentOutOfRangeException(nameof(cn), "must be 1-9");
            }
        }

        [Test(Description = "Tests Match for every type of the OneOf")]
        public void MatchesOnType() => RunTestForAllOneOfTypes(cnMax =>
        {
            // ensure every type can be matched
            for (var cn = 1; cn <= cnMax; cn++)
            {
                var cnInstance = CreateCn(cn, "xyz");                           // create a Cn
                dynamic oo = CreateOneOfCn(cnMax, cnInstance);                  // chuck it into a OneOf<C1...CMax>

                // ensure every OneOf.Match and OneOfMatcher.Match method is called
                for (var matchStart = 1; matchStart <= cnMax; matchStart++)
                {
                    var ret = oo;
                    var matchNext = matchStart;

                    do
                    {                                                           // try matching against C1..CnMax 
                        ret = MatchCn(matchNext, ret);                          // it should only match Cn

                        matchNext++;
                        if (matchNext > cnMax) matchNext = 1;

                    } while (matchNext != matchStart);

                    NUnit.Framework.Assert.IsTrue(ret == $"{cn}:xyz");          // make sure it matched Cn, and also gave us back the right value
                }
            }
        });

        [Test]
        public void ReturnsDefaultWhenNoMatch() => RunTestForAllOneOfTypes(cnMax =>
        {
            for (var cn = 1; cn <= cnMax; cn++)
            {
                var cnInstance = CreateCn(cn, "xyz");                           // create an instance of Cn
                dynamic oo = CreateOneOfCn(cnMax, cnInstance);                  // chuck it into a OneOf<C1...Cn>

                // ensure every OneOf.Match and OneOfMatcher.Match method is called, except for Cn
                for (var matchStart = 1; matchStart <= cnMax; matchStart++)
                {
                    var ret = oo;
                    var matchNext = matchStart;

                    do
                    {
                        // try matching every type except cn
                        if (matchNext != cn)
                        {
                            ret = MatchCn(matchNext, ret);                      // none of the matches should work
                        }

                        matchNext++;
                        if (matchNext > cnMax) matchNext = 1;

                    } while (matchNext != matchStart);

                    // let the else handle cn
                    ret = ret.Else(Fun((object v) => $"else:{v.GetType().Name}:{v}"));  // but it should trigger the Else

                    NUnit.Framework.Assert.AreEqual($"else:C{cn}:xyz", ret);     // make sure it did trigger the else with correct type and value
                }
            }
        });

        [Test]
        public void ReturnsDefaultWhenMoMatch2() => RunTestForAllOneOfTypes(cnMax =>
        {
            var c1 = CreateCn(1, "xyz");                                    // create an instance of C1 (all tests will be against C2..C9)
            dynamic oo = CreateOneOfCn(cnMax, c1);                             // chuck it into a OneOf<C1...Cn>

            var ret = oo;
            ret = MatchCn(cnMax, ret);                                         // matching against Cn should fail (it's C1)
            ret = ret.Else("Yes");                                          // but it should trigger the Else

            NUnit.Framework.Assert.IsTrue(ret == $"Yes");                   // make sure it did trigger the else
        });

        [Test]
        public void ThrowsExceptionWhenNoMatch() => RunTestForAllOneOfTypes(cnMax =>
        {
            var c1 = CreateCn(1, "xyz");                                    // create a C1
            dynamic oo = CreateOneOfCn(cnMax, c1);                             // chuck it into a OneOf<C1...Cn>

            TestDelegate test = () =>
            {
                var ret = oo;
                ret = MatchCn(cnMax, ret);                                     // matching against Cn should fail (it's C1)
                ret = ret.ElseThrow(Fun((object v) => new InvalidOperationException()));  // so this should throw
            };

            NUnit.Framework.Assert.Throws<InvalidOperationException>(test);
        });

        [Test]
        public void DoesntThrowExceptionWhenMatch() => RunTestForAllOneOfTypes(cnMax =>
        {
            var cnInstance = CreateCn(cnMax, "xyz");                           // create a Cn
            dynamic oo = CreateOneOfCn(cnMax, cnInstance);                     // chuck it into a OneOf<C1...Cn>

            var ret = oo;
            ret = MatchCn(cnMax, ret);                                         // matching against Cn should work
            ret = ret.ElseThrow(Fun((object v) => new InvalidOperationException()));  // so this shouldn't throw

            NUnit.Framework.Assert.IsTrue(ret == $"{cnMax}:xyz");              // make sure we get the correct return value considering we matched Cn
        });
    }
}
