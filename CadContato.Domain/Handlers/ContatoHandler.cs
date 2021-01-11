using CadContato.Domain.Commands.Contato;
using CadContato.Domain.Entities;
using CadContato.Domain.Repositories;
using CadContato.Shared.Commands;
using Flunt.Notifications;
using System;
using System.Collections.Generic;
using System.Text;

namespace CadContato.Domain.Handlers
{
    public class ContatoHandler : Notifiable, 
        IHandler<DeleteContatoCommand>,
        IHandler<UpdateContatoCommand>,
        IHandler<CreateContatoCommand>
    {
        private readonly IContatoRepository _repo;

        public ContatoHandler(IContatoRepository repo)
        {
            _repo = repo;
        }

        public ICommandResult Handle(DeleteContatoCommand command)
        {
            command.Validate();

            if (command.Invalid)
                return new GenericCommandResult(command.Notifications);

            var item = _repo.GetById(new Guid(command.Id));

            if (item == null)
                return new GenericCommandResult(false, "Contato não localizado!", command.Id);

            _repo.Delete(item);

            return new GenericCommandResult(true, "Contato excluído com sucesso!", command.Id);
        }

        public ICommandResult Handle(UpdateContatoCommand command)
        {
            command.Validate();

            if (command.Invalid)
                return new GenericCommandResult(command.Notifications);

            var item = _repo.GetById(new Guid(command.Id));

            if (item == null)
                return new GenericCommandResult(false, "Contato não localizado!", command.Id);

            item.ChangeNome(new ValueObjects.Nome(command.PrimeiroNome, command.UltimoNome));
            item.ChangeEmail(new ValueObjects.Email(command.Email));
            item.ChangeTelefone(new ValueObjects.Telefone(command.TelefoneDDD, command.TelefoneNumero));

            _repo.Update(item);
            return new GenericCommandResult(true, "Contato atualizado com sucesso!", item.Id);
        }

        public ICommandResult Handle(CreateContatoCommand command)
        {
            //Validação Especifica do Command
            command.Validate();
            
            //Ja Abortar
            if (command.Invalid)
                return new GenericCommandResult(command.Notifications);

            //Instacia e já faz as Validações de Negócio da Entity
            var contato = new Contato(new ValueObjects.Nome(command.PrimeiroNome, command.UltimoNome),
                new ValueObjects.Email(command.Email),
                new ValueObjects.Telefone(command.TelefoneDDD, command.TelefoneNumero));

            if (contato.Invalid)
                return new GenericCommandResult(contato.Notifications);

            //Tudo Certo
            _repo.Create(contato);
            return new GenericCommandResult(true, "Contato criado com sucesso!", contato.Id);
        }
    }
}
