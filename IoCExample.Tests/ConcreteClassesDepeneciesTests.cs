namespace IoCExample.Tests
{
    using NUnit.Framework;

    [TestFixture]
    class ConcreteClassesDepeneciesTests
    {
        [Test]
        public void WhenRequiringASimpleClass_ShouldReturnClass()
        {
            IoCExampleImplementation sut = new IoCExampleImplementation();

            SimpleClass simpleClass = sut.Create<SimpleClass>();

            Assert.That(simpleClass, Is.Not.Null, "The instance shouldn't be null");
        }

        [Test]
        public void WhenRequiringAClassWithDepencies_ShouldCascadeAndCreateClass()
        {
            IoCExampleImplementation sut = new IoCExampleImplementation();

            SimpleClass2 simpleClass2 = sut.Create<SimpleClass2>();

            Assert.That(simpleClass2, Is.Not.Null, "The instance shouldn't be null");
            Assert.That(simpleClass2.Dependency, Is.Not.Null, "The dependency should be injected via constructor and shouldn't be null");
        }

        [Test]
        public void WhenRequiringAClassWithMultipleAndNestedDepencies_ShouldCascadeAndCreateClass()
        {
            IoCExampleImplementation sut = new IoCExampleImplementation();

            SimpleClass3 simpleClass3 = sut.Create<SimpleClass3>();

            Assert.That(simpleClass3, Is.Not.Null, "The instance shouldn't be null");
            Assert.That(simpleClass3.Dependency1, Is.Not.Null, "The dependency should be injected via constructor and shouldn't be null");
            Assert.That(simpleClass3.Dependency2, Is.Not.Null, "The dependency should be injected via constructor and shouldn't be null");

            Assert.That(simpleClass3.Dependency1.Dependency, Is.Not.Null, "The nested dependency should be fulfilled as well");
        }
    }

    public class SimpleClass3
    {
        public SimpleClass2 Dependency1 { get; }

        public SimpleClass Dependency2 { get; }

        public SimpleClass3(SimpleClass2 simpleClass2, SimpleClass simpleClass)
        {
            this.Dependency1 = simpleClass2;
            this.Dependency2 = simpleClass;
        }
    }

    public class SimpleClass2
    {
        public SimpleClass2(SimpleClass dependency)
        {
            Dependency = dependency;
        }

        internal SimpleClass Dependency { get; }
    }

    public class SimpleClass
    {
    }
}
