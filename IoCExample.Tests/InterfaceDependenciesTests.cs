namespace IoCExample.Tests
{
    using NUnit.Framework;

    [TestFixture]
    class InterfaceDependenciesTests
    {
        [Test]
        public void WhenMappingInterfaceDepencies_ShouldCreateProperClass()
        {
            IoCExampleImplementation sut = new IoCExampleImplementation();
            sut.Mapping.Add(typeof(INotification), typeof(EmailNotification));

            OrderProcessor orderProcessor = sut.Create<OrderProcessor>();

            Assert.That(orderProcessor, Is.Not.Null, "The instance of the test object wasn't created");
            Assert.That(orderProcessor.Notification, Is.Not.Null, "The dependency has not been fulfilled");
            Assert.That(orderProcessor.Notification, Is.TypeOf<EmailNotification>(), "The dependency is not of mapped type");

            sut.Mapping[typeof(INotification)] = typeof(SmsNotification);
            orderProcessor = sut.Create<OrderProcessor>();

            Assert.That(orderProcessor, Is.Not.Null, "The instance of the test object wasn't created");
            Assert.That(orderProcessor.Notification, Is.Not.Null, "The dependency has not been fulfilled");
            Assert.That(orderProcessor.Notification, Is.TypeOf<SmsNotification>(), "The dependency is not of mapped type");
        }
    }

    internal interface INotification
    {
    }

    internal class EmailNotification : INotification
    {
    }

    internal class SmsNotification : INotification
    {
    }

    internal class OrderProcessor
    {
        public INotification Notification { get; }

        public OrderProcessor(INotification notification)
        {
            this.Notification = notification;
        }
    }
}
