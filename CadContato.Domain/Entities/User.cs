using CadContato.Domain.ValueObjects;
using Flunt.Validations;
using System.Collections.Generic;

namespace CadContato.Domain.Entities
{
    public class User : Entity
    {
        public string NomeCompleto { get; private set; }

        public Email Email { get; private set; }

        public ICollection<Contato> Contatos { get; private set; }

        private User() { }

        public User(string nomeCompleto, Email email)
        {
            this.NomeCompleto = nomeCompleto;
            this.Email = email;

            this.Validate();
        }

        public User(string nomeCompleto, Email email, ICollection<Contato> contatos)
        {
            this.NomeCompleto = nomeCompleto;
            this.Email = email;
            this.Contatos = contatos;

            this.Validate();
        }

        private void Validate()
        {
            AddNotifications(Email);
        }
    }
}
