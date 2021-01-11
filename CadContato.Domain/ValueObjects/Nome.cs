using CadContato.Shared.Util;
using Flunt.Notifications;
using Flunt.Validations;
using System;
using System.Collections.Generic;
using System.Text;

namespace CadContato.Domain.ValueObjects
{
    public class Nome : ValueObject
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
    }
}
