using System;
//using System.Security.Cryptography.X509Certificates;
using NUnit.Framework;

namespace OneOf.Tests
{
    [TestFixture]
    public class PolymorphicTests
    {
        [Test]
        public void MatcherMatchesOnlyOnceWhenParentClassAndSubClassesAreInOneof()
        {
            OneOf<Fake, Fake.One, Fake.Two> one = new Fake.One();
            OneOf<Fake, Fake.One, Fake.Two> two = new Fake.Two();
            OneOf<Fake, Fake.One, Fake.Two> three = new Fake.Three();

            Func<OneOf<Fake, Fake.One, Fake.Two>, int> testerFunc = o =>
            {
                var result = o.Match((Fake f) => 3).
                    Match((Fake.One f) => 1).
                    Match((Fake.Two f) => 2);

                return result;
            };

            Assert.AreEqual(1, testerFunc(one));
            Assert.AreEqual(2, testerFunc(two));
            Assert.AreEqual(3, testerFunc(three));
        }

        [Test]
        public void MatcherMatchesOnlyOnceWhenParentClassAndSubClassesAreInOneofAndCreationReferenceIsOfTypeParentButInstanceIsReallyChild()
        {
            OneOf<Fake, Fake.One, Fake.Two> hiddenOne = (Fake)new Fake.One();
            
            Func<OneOf<Fake, Fake.One, Fake.Two>, int> testerFunc = o =>
            {
                var result = o.Match((Fake f) => 3).
                    Match((Fake.One f) => 1).
                    Match((Fake.Two f) => 2);

                return result;
            };

            Assert.AreEqual(1, testerFunc(hiddenOne));
        }

        [Test]
        public void SwitchProperlyHandlesSubclassAndMatchesOnce()
        {
            var origOne = new Fake.One();
            var origTwo = new Fake.Two();
            var origThree = new Fake.Three();

            OneOf<Fake, Fake.One, Fake.Two> one = origOne;
            OneOf<Fake, Fake.One, Fake.Two> two = origTwo;
            OneOf<Fake, Fake.One, Fake.Two> three = origThree;

            Func<OneOf<Fake, Fake.One, Fake.Two>, int> testerFunc = o =>
            {
                var i = 0;

                o
                .Switch((Fake f) => i++)
                .Switch((Fake.One f) => i++)
                .Switch((Fake.Two f) => i++);

                return i;
            };

            Assert.AreEqual(1, testerFunc(one));
            Assert.AreEqual(1, testerFunc(two));
            Assert.AreEqual(1, testerFunc(three));
        }

        [Test]
        public void OneOfProperlyHandlesHierarchies()
        {
            var origFour = new Fake.Four();
            var origFive = new Fake.Five();
            var origThree = new Fake.Three();

            OneOf<Fake, Fake.One, Fake.Two, Fake.Four> three = origThree;
            OneOf<Fake, Fake.One, Fake.Two, Fake.Four> four = origFour;
            OneOf<Fake, Fake.One, Fake.Two, Fake.Four> five = origFive;

            Func<OneOf<Fake, Fake.One, Fake.Two, Fake.Four>, int> testerFunc = o =>
            {
                var i = 0;

                o
                .Switch((Fake f) => i = 0)
                .Switch((Fake.One f) => i = 1)
                .Switch((Fake.Two f) => i = 2)
                .Switch((Fake.Four f) => i = 4);

                return i;
            };

            Assert.AreEqual(4, testerFunc(four));
            Assert.AreEqual(4, testerFunc(five));
            Assert.AreEqual(0, testerFunc(three));
        }
    }


    public class Fake
    {
        public class One : Fake { }
        public class Two : Fake { }
        public class Three : Fake { }
        public class Four : Three { }
        public class Five : Four { }
    }
}