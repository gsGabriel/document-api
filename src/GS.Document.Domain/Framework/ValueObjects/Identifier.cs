using System;

namespace GS.Document.Domain.Framework.ValueObjects
{
    public class Identifier : IComparable
    {
        private Guid _value;

        protected Identifier(Guid value)
        {
            _value = value;
        }

        public Guid Value => _value;

        /// <summary>
        /// Description for identifier
        /// </summary>
        /// <returns>Identifier formated for string</returns>
        public override string ToString() => _value.ToString();

        /// <summary>
        /// Compare to identifier
        /// </summary>
        /// <param name="obj">Object to compare</param>
        /// <returns>Indication of their relative values</returns>
        public int CompareTo(object obj)
        {
            return obj is Identifier identifier
                ? _value.CompareTo(identifier.Value)
                : throw new ArgumentException($"Tipo inválido para comparar do identificador");
        }
    }
}
