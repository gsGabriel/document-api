using GS.Document.Domain.ValueObjects;
using System;
using Xunit;

namespace GS.Document.Domain.Tests
{
    public class DocumentEntityUnitTest
    {
        [Fact]
        public void Should_be_create_new_document()
        {
            //given
            string fileName = "document.pdf";
            string contentType = "application/pdf";
            string path = Guid.NewGuid().ToString();
            var customerId = CustomerId.From(Guid.NewGuid());

            //when
            var document = Entities.Documents.CreateFrom(customerId, fileName, contentType, path);

            //then
            Assert.Equal(customerId, document.CustomerId);
            Assert.Equal(fileName, document.FileName);
            Assert.Equal(contentType, document.ContentType);
            Assert.Equal(path, document.Path);
        }

        [Fact]
        public void Should_not_be_create_new_document_with_invalid_file_name()
        {
            //given
            string fileName = string.Empty;
            string contentType = "application/pdf";
            string path = Guid.NewGuid().ToString();
            var customerId = CustomerId.From(Guid.NewGuid());

            //when
            Assert.Throws<ArgumentException>(() => Entities.Documents.CreateFrom(customerId, fileName, contentType, path));
        }

        [Fact]
        public void Should_not_be_create_new_document_with_invalid_content_type()
        {
            //given
            string fileName = "document.pdf";
            string contentType = string.Empty;
            string path = Guid.NewGuid().ToString();
            var customerId = CustomerId.From(Guid.NewGuid());

            //when
            Assert.Throws<ArgumentException>(() => Entities.Documents.CreateFrom(customerId, fileName, contentType, path));
        }

        [Fact]
        public void Should_not_be_create_new_document_with_invalid_path()
        {
            //given
            string fileName = "document.pdf";
            string contentType = "application/pdf";
            string path = string.Empty;
            var customerId = CustomerId.From(Guid.NewGuid());

            //when
            Assert.Throws<ArgumentException>(() => Entities.Documents.CreateFrom(customerId, fileName, contentType, path));
        }

        [Fact]
        public void Should_not_be_create_new_document_with_not_allowed_type()
        {
            //given
            string fileName = "document.excel";
            string contentType = "application/excel";
            string path = Guid.NewGuid().ToString();
            var customerId = CustomerId.From(Guid.NewGuid());

            //when
            Assert.Throws<InvalidOperationException>(() => Entities.Documents.CreateFrom(customerId, fileName, contentType, path));
        }
    }
}
