using System;

namespace Common
{
    public class Person
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Age { get; set; }

        public string PhoneNumber { get; set; }

        public string Email { get; set; }

        public override string ToString()
        {
            return $"{Name} {Email} {Age} {PhoneNumber}";
        }
    }
}
