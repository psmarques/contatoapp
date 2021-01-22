using CadContato.Shared.Util;
using Flunt.Notifications;
using Flunt.Validations;
using System;
using System.Collections.Generic;
using System.Text;

namespace CadContato.Domain.ValueObjects
{
    public class Nome : ValueObject, IEquatable<Nome>
    {
        public string PrimeiroNome { get; private set; }

        public string UltimoNome { get; private set; }

        private Nome() { }

        public Nome(string primeiroNome, string ultimoNome)
        {
            PrimeiroNome = primeiroNome;
            UltimoNome = ultimoNome;

            Validate();
        }

        private void Validate()
        {
            AddNotifications(new Contract()
                .Requires()
                .IsNotNull(PrimeiroNome, "Primeiro Nome", "Primeiro Nome é obrigatório!")
                .IsNotNull(UltimoNome, "Ultimo Nome", "Ultimo Nome é obrigatório!")
                .HasMinLen(PrimeiroNome, 3, "Primeiro Nome", "Primeiro Nome deve conter no mínimo 3 caracteres!"));
        }


        public override string ToString()
        {
            return string.Concat(PrimeiroNome, ' ', UltimoNome);
        }

        public bool Equals(Nome other)
        {
            if (other == null) return false;
            return string.Equals(this.PrimeiroNome, other.PrimeiroNome) &&
                   string.Equals(this.UltimoNome, other.UltimoNome);
        }
        public override bool Equals(object obj)
        {
            return Equals(obj as Nome);
        }

        public static bool operator ==(Nome obj1, Nome obj2)
        {
            if (obj1 == null && obj2 == null) return true;
            if (obj1 == null || obj2 == null) return false;

            return obj1.Equals(obj2);
        }

        public static bool operator !=(Nome obj1, Nome obj2)
        {
            return !(obj1 == obj2);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 5;

                hash = hash * 7 + (PrimeiroNome ?? string.Empty).GetHashCode();
                return hash;
            }
        }
    }
}
