using OneOf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using NUnit.Framework;

namespace OneOf.Tests
{
    public class Serialization
    {
        [Test]
        public void CanSerializeString()
        {
            var oneOf = new SomeThing() { Value = "A string value" };
            var json = JsonConvert.SerializeObject(oneOf);
            Assert.AreEqual("{\"Value\":{\"Value\":\"A string value\",\"Type\":\"String\"}}", json);
        }

        [Test]
        public void CanDeserializeString()
        {
            var json = "{\"Value\":{\"Value\":\"A string value\",\"Type\":\"String\"}}";
            var oneOf = JsonConvert.DeserializeObject<SomeThing>(json);
            Assert.AreEqual("A string value", oneOf.Value.As<string>());
        }

        [Test]
        public void CanSerializeSomeOtherThing()
        {
            var oneOf = new SomeThing() { Value = new SomeOtherThing { Value = 123 } };
            var json = JsonConvert.SerializeObject(oneOf);
            Assert.AreEqual("{\"Value\":{\"Value\":{\"Value\":123},\"Type\":\"SomeOtherThing\"}}", json);
        }

        [Test]
        public void CanDeserializeSomeOtherThing()
        {
            var json = "{\"Value\":{\"Value\":{\"Value\":123},\"Type\":\"SomeOtherThing\"}}";
            var oneOf = JsonConvert.DeserializeObject<SomeThing>(json);
            Assert.AreEqual(123, oneOf.Value.As<SomeOtherThing>().Value);
        }
    }

    class SomeThing
    {
        public OneOf<string, SomeOtherThing> Value { get; set; }
    }

    internal class SomeOtherThing
    {
        public int Value { get; set; }
    }
}
