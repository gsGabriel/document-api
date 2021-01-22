using GS.Document.Domain.Framework.ValueObjects;
using System;

namespace GS.Document.Domain.ValueObjects
{
    public class DocumentId : Identifier
    {
        private DocumentId(Guid documentId)
            : base(documentId)
        { }

        /// <summary>
        /// Create new document id
        /// </summary>
        /// <param name="id">Identifier</param>
        /// <returns>Created document id</returns>
        public static DocumentId From(Guid id)
        {
            if (id == Guid.Empty)
                throw new ArgumentException("Identificador de documento inválido");

            return new DocumentId(id);
        }
    }
}
