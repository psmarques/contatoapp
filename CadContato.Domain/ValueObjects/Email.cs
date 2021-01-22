using CadContato.Shared.Util;
using Flunt.Validations;
using System;
using System.Collections.Generic;
using System.Text;

namespace CadContato.Domain.ValueObjects
{
    public class Email : ValueObject, IEquatable<Email>
    {
        public string Address { get; private set; }

        public Email(string address)
        {
            Address = address;
            Validate();
        }

        private Email() { }

        private void Validate()
        {
            AddNotifications(new Contract()
                .Requires()
                .IsEmail(Address, "Email", "E-mail inválido!")
                );
        }

        public bool Equals(Email other)
        {
            if (other == null) return false;
            return string.Equals(this.Address, other.Address);
        }
        public override bool Equals(object obj)
        {
            return Equals(obj as Email);
        }

        public static bool operator ==(Email obj1, Email obj2)
        {
            if (obj1 == null && obj2 == null) return true;
            if (obj1 == null || obj2 == null) return false;

            return obj1.Equals(obj2);
        }

        public static bool operator !=(Email obj1, Email obj2)
        {
            return !(obj1 == obj2);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 3;

                // Suitable nullity checks etc, of course :)
                hash = hash * 5 + (Address ?? string.Empty).GetHashCode();
                return hash;
            }
        }
    }
}
