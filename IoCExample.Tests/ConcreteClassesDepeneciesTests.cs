using NUnit.Framework;

namespace IoCExample.Tests
{
    [TestFixture]
    class ConcreteClassesDepeneciesTests
    {
        [Test]
        public void WhenRequiringASimpleClass_ShouldReturnClass()
        {
            IoCExampleImplementation sut = new IoCExampleImplementation();

            SimpleClass simpleClass = sut.Create<SimpleClass>();

            Assert.That(simpleClass, Is.Not.Null);
        }
    }

    internal class SimpleClass
    {
    }
}
