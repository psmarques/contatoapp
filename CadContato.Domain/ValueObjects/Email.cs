using CadContato.Shared.Util;
using Flunt.Validations;
using System;
using System.Collections.Generic;
using System.Text;

namespace CadContato.Domain.ValueObjects
{
    public class Email : ValueObject
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

    }
}
