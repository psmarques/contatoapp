using CadContato.Shared.Util;
using Flunt.Validations;
using System;
using System.Collections.Generic;
using System.Text;

namespace CadContato.Domain.ValueObjects
{
    public class Telefone : ValueObject, IEquatable<Telefone>
    {
        public int? DDD { get; private set; }

        public int? Numero { get; private set; }

        private Telefone() { }

        public Telefone(int? dDD, int? numero)
        {
            DDD = dDD;
            Numero = numero;

            Validate();
        }

        public Telefone(string dDD, string numero)
        {
            int ddd = int.MinValue;
            int num = int.MinValue;

            if (int.TryParse(dDD, out ddd))
                DDD = ddd;

            if (int.TryParse(numero, out num))
                Numero = num;

            Validate();
        }


        private void Validate()
        {
            AddNotifications(new Contract()
                .Requires()
                .IfNotNull(DDD, x => x.IsBetween(DDD.Value, 1, 99, "DDD", "DDD deve estar entre 1 e 99"))
                .IfNotNull(Numero, x => x.IsBetween(Numero.Value, 10000000, 999999999, "Numero Telefone", "Numero Telefone deve conter 8 ou 9 digitos"))
                );
        }

        public bool Equals(Telefone other)
        {
            if (other == null) return false;
            return string.Equals(this.DDD, other.DDD) &&
                   string.Equals(this.Numero, other.Numero);
        }
        public override bool Equals(object obj)
        {
            return Equals(obj as Telefone);
        }

        public static bool operator ==(Telefone obj1, Telefone obj2)
        {
            if (obj1 == null && obj2 == null) return true;
            if (obj1 == null || obj2 == null) return false;

            return obj1.Equals(obj2);
        }

        public static bool operator !=(Telefone obj1, Telefone obj2)
        {
            return !(obj1 == obj2);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 7;

                hash = hash * 13 + (Numero ?? 1).GetHashCode();
                return hash;
            }
        }
    }
}
