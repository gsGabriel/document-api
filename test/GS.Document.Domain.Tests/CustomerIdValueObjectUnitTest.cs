using GS.Document.Domain.ValueObjects;
using System;
using Xunit;

namespace GS.Document.Domain.Tests
{
    public class CustomerIdValueObjectUnitTest
    {
        [Fact]
        public void Should_create_new_customer_id()
        {
            //given
            var id = Guid.NewGuid();

            //when
            var customerId = CustomerId.From(id);

            //then
            Assert.NotNull(customerId);
            Assert.Equal(id, customerId.Value);
        }

        [Fact]
        public void Should_not_be_able_to_create_new_customer_id()
        {
            //given
            var id = Guid.Empty;

            //when
            Assert.Throws<ArgumentException>(() => CustomerId.From(id));
        }
    }
}
