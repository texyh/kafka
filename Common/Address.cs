using System;

namespace Common
{
    public class Address
    {
        public string Street { get; set; }

        public string State { get; set; }

        public string ZipCode { get; set; }

        public int PersonId { get; set; }

        public override string ToString()
        {
            return $"{PersonId} {Street} {State} {ZipCode}";
        }
    }
}
