using GS.Document.Domain.Aggregates;
using System;
using Xunit;

namespace GS.Document.Domain.Tests
{
    public class CustomersAggregateUnitTest
    {
        [Fact]
        public void Should_be_create_new_customer_aggregate()
        {
            //given
            var customerId = Guid.NewGuid();

            //when
            var customerAgg = CustomerAggregate.CreateFrom(customerId);

            //then
            Assert.NotNull(customerAgg);
            Assert.Empty(customerAgg.Documents);
            Assert.Empty(customerAgg.DomainEvents);
            Assert.Equal(customerId, customerAgg.Id);
        }

        [Fact]
        public void Should_not_be_create_new_customer_aggregate_with_empty_id()
        {
            //given
            var customerId = Guid.Empty;

            //when
            Assert.Throws<ArgumentException>(() => CustomerAggregate.CreateFrom(customerId));
        }

        [Fact]
        public void Should_add_document_to_customer_aggregate()
        {
            //given
            string fileName = "document.pdf";
            string contentType = "application/pdf";
            string path = Guid.NewGuid().ToString();
            CustomerAggregate customerAgg = CustomerAggregate.CreateFrom(Guid.NewGuid());

            //when
            customerAgg.AddDocument(fileName, contentType, path);

            //then
            Assert.NotEmpty(customerAgg.Documents);
            Assert.True(customerAgg.Documents.Count == 1);
            Assert.NotEmpty(customerAgg.DomainEvents);
            Assert.True(customerAgg.DomainEvents.Count == 1);
        }

        [Fact]
        public void Should_not_be_able_to_add_document_with_invalid_file_name()
        {
            //given
            string fileName = string.Empty;
            string contentType = "application/pdf";
            string path = Guid.NewGuid().ToString();
            CustomerAggregate customerAgg = CustomerAggregate.CreateFrom(Guid.NewGuid());

            //when
            Assert.Throws<ArgumentException>(() => customerAgg.AddDocument(fileName, contentType, path));
        }

        [Fact]
        public void Should_not_be_able_to_add_document_with_invalid_content_type()
        {
            //given
            string fileName = "document.pdf";
            string contentType = string.Empty;
            string path = Guid.NewGuid().ToString();
            CustomerAggregate customerAgg = CustomerAggregate.CreateFrom(Guid.NewGuid());

            //when
            Assert.Throws<ArgumentException>(() => customerAgg.AddDocument(fileName, contentType, path));
        }

        [Fact]
        public void Should_not_be_able_to_add_document_with_invalid_path()
        {
            //given
            string fileName = "document.pdf";
            string contentType = "application/pdf";
            string path = string.Empty;
            CustomerAggregate customerAgg = CustomerAggregate.CreateFrom(Guid.NewGuid());

            //when
            Assert.Throws<ArgumentException>(() => customerAgg.AddDocument(fileName, contentType, path));
        }

        [Fact]
        public void Should_not_be_able_to_add_document_with_duplicity()
        {
            //given
            string fileName = "document.pdf";
            string contentType = "application/pdf";
            string path = Guid.NewGuid().ToString();
            CustomerAggregate customerAgg = CustomerAggregate.CreateFrom(Guid.NewGuid());
            customerAgg.AddDocument(fileName, contentType, path);

            //when
            Assert.Throws<InvalidOperationException>(() => customerAgg.AddDocument(fileName, contentType, path));
        }

        [Fact]
        public void Should_not_be_able_to_add_document_with_not_allowed_type()
        {
            //given
            string fileName = "document.excel";
            string contentType = "application/excel";
            string path = Guid.NewGuid().ToString();
            CustomerAggregate customerAgg = CustomerAggregate.CreateFrom(Guid.NewGuid());

            //when
            Assert.Throws<InvalidOperationException>(() => customerAgg.AddDocument(fileName, contentType, path));
        }
    }
}
