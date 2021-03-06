﻿using CadContato.Shared.Commands;
using Flunt.Notifications;
using Flunt.Validations;

namespace CadContato.Domain.Commands.Contato
{
    public class DeleteContatoCommand : Notifiable, ICommand
    {
        public string Id { get; set; }

        public DeleteContatoCommand(string id)
        {
            Id = id;
        }

        public void Validate()
        {
            AddNotifications(new Contract()
                .Requires()
                .IsNotNullOrEmpty(Id, "Id", "Id não pode ser vazio!"));
        }
    }
}
