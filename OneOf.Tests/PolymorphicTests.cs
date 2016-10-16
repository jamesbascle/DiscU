using System;
using System.Security.Cryptography.X509Certificates;
using NUnit.Framework;

namespace OneOf.Tests
{
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
                var i = 0;

                var result = o.Match((Fake f) => i = 3).
                    Match((Fake.One f) => i = 1).
                    Match((Fake.Two f) => i = 2);

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
                var i = 0;

                var result = o.Match((Fake f) => i = 3).
                    Match((Fake.One f) => i = 1).
                    Match((Fake.Two f) => i = 2);

                return result;
            };

            Assert.AreEqual(1, testerFunc(hiddenOne));
        }


        [Test]
        public void IsReturnsTrueOnlyWhenTWasTheOriginalTypeOneOfWasConstructedWith()
        {
            OneOf<Fake, Fake.One, Fake.Two> one = new Fake.One();
            OneOf<Fake, Fake.One, Fake.Two> two = new Fake.Two();
            OneOf<Fake, Fake.One, Fake.Two> three = new Fake.Three();

            Assert.True(one.Is<Fake.One>());
            Assert.False(one.Is<Fake>());

            Assert.True(two.Is<Fake.Two>());
            Assert.False(two.Is<Fake>());

            Assert.True(three.Is<Fake>());
            Assert.False(three.Is<Fake.Three>());
        }

        [Test]
        public void EqualsOperatorProperlyComparesUnderlyingTypesToOneOfs()
        {
            var origOne = new Fake.One();
            var origTwo = new Fake.Two();
            var origThree = new Fake.Three();
            
            OneOf<Fake, Fake.One, Fake.Two> one = origOne;
            OneOf<Fake, Fake.One, Fake.Two> two = origTwo;
            OneOf<Fake, Fake.One, Fake.Two> three = origThree;

            Assert.IsTrue(one == origOne);
            Assert.IsTrue(two == origTwo);
            Assert.IsTrue(three == origThree);

            Assert.IsTrue(origOne == one);
            Assert.IsTrue(origTwo == two);
            Assert.IsTrue(origThree == three);
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
    }


    public class Fake
    {
        public class One : Fake { }
        public class Two : Fake { }
        public class Three : Fake { }
    }
}