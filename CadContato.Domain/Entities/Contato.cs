using CadContato.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace CadContato.Domain.Entities
{
    public class Contato : Entity
    {

        public Nome Nome { get; private set; }

        public Email Email { get; private set; }

        public Telefone Telefone { get; private set; }

        public User User { get; private set; }

        private Contato() { }

        public Contato(Nome nome, Email email, Telefone telefone)
        {
            Nome = nome;
            Email = email;
            Telefone = telefone;

            Validate();
        }

        public Contato(Nome nome, Email email, Telefone telefone, User user)
        {
            Nome = nome;
            Email = email;
            Telefone = telefone;
            User = user;

            Validate();
        }

        public void ChangeNome(Nome nome)
        {
            Nome = nome;
            Validate();
        }

        public void ChangeEmail(Email email)
        {
            Email = email;
            Validate();
        }

        public void ChangeTelefone(Telefone tel)
        {
            Telefone = tel;
            Validate();
        }


        public void Validate()
        {
            AddNotifications(Nome);
            AddNotifications(Email);
            AddNotifications(Telefone);
            //AddNotifications(User);
        }
    }
}
