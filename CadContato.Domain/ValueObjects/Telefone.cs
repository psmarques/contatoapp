using CadContato.Shared.Util;
using Flunt.Validations;
using System;
using System.Collections.Generic;
using System.Text;

namespace CadContato.Domain.ValueObjects
{
    public class Telefone : ValueObject
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

    }
}
