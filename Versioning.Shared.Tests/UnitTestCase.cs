using Moq;
using shared.domain;
using shared.domain.Bus.Event;

namespace Versioning.Shared.Tests
{
    /// <summary>
    /// Its intent is to provide a base class for testing Use Cases
    /// in other words Application services
    /// </summary>
    public class UnitTestCase // I suggest: ApplicationTestCase
    {
        //[TestMethod]
        //public void TestMethod1()
        //{
        //}
        protected Mock<IEventBus> EventBus { get; private set; }
        protected Mock<UuidGenerator> UuidGenerator { get; private set; }

        public UnitTestCase()
        {
            this.EventBus = new Mock<IEventBus>();
            this.UuidGenerator = new Mock<UuidGenerator>();
        }

        public void ShouldHavePublished(List<DomainEvent> domainEvents)
        {
            this.EventBus.Verify(x => x.Publish(domainEvents), Times.AtLeastOnce());
        }

        public void ShouldHavePublished(DomainEvent domainEvent)
        {
            this.ShouldHavePublished(new List<DomainEvent> { domainEvent });
        }

        public void ShouldGenerateUuid(string uuid)
        {
            this.UuidGenerator.Setup(x => x.Generate()).Returns(uuid);
        }
    }
}
