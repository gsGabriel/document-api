using GS.Document.Domain.ValueObjects;
using System;
using Xunit;

namespace GS.Document.Domain.Tests
{
    public class DocumentIdValueObjectUnitTest
    {
        [Fact]
        public void Should_create_new_document_id()
        {
            //given
            var id = Guid.NewGuid();

            //when
            var documentId = DocumentId.From(id);

            //then
            Assert.NotNull(documentId);
            Assert.Equal(id, documentId.Value);
        }

        [Fact]
        public void Should_not_be_able_to_create_new_document_id()
        {
            //given
            var id = Guid.Empty;

            //when
            Assert.Throws<ArgumentException>(() => DocumentId.From(id));
        }
    }
}
