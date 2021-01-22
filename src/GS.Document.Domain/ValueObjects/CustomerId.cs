using GS.Document.Domain.Framework.ValueObjects;
using System;

namespace GS.Document.Domain.ValueObjects
{
    public class CustomerId : Identifier
    {
        private CustomerId(Guid customerId)
            : base(customerId)
        { }

        /// <summary>
        /// Create new customer id
        /// </summary>
        /// <param name="id">Identifier</param>
        /// <returns>Created customer id</returns>
        public static CustomerId From(Guid id)
        {
            if (id == Guid.Empty)
                throw new ArgumentException("Identificador de cliente inválido");

            return new CustomerId(id);
        }
    }
}
