using CadContato.Domain.Commands.Contato;
using CadContato.Domain.Entities;
using CadContato.Domain.Repositories;
using CadContato.Shared.Commands;
using CadContato.Shared.Util;
using Flunt.Notifications;
using System;
using System.Security.Claims;

namespace CadContato.Domain.Handlers
{
    public class ContatoHandler : Notifiable,
        IAuthenticatedHandler<DeleteContatoCommand>,
        IAuthenticatedHandler<UpdateContatoCommand>,
        IAuthenticatedHandler<CreateContatoCommand>
    {
        private readonly IContatoRepository _repo;
        private readonly IUserRepository _repoUser;
        private readonly ClaimsHelper clHelper;

        public ContatoHandler(IContatoRepository repo, IUserRepository repoUser)
        {
            _repo = repo;
            _repoUser = repoUser;
            clHelper = new ClaimsHelper();
        }

        public ICommandResult Handle(DeleteContatoCommand command, ClaimsPrincipal claims)
        {
            command.Validate();

            if (command.Invalid)
                return new GenericCommandResult(command.Notifications);

            clHelper.PopulateFromPrincipal(claims);
            var item = _repo.GetById(new Guid(command.Id));
            var user = new User(clHelper.Name, new ValueObjects.Email(clHelper.Email), clHelper.Picture);

            if (!ValidateItemAndUser(item, user))
                return new GenericCommandResult(false, "Contato não localizado!", command.Id);

            _repo.Delete(item);

            return new GenericCommandResult(true, "Contato excluído com sucesso!", command.Id);
        }

        public ICommandResult Handle(UpdateContatoCommand command, ClaimsPrincipal claims)
        {
            command.Validate();

            if (command.Invalid)
                return new GenericCommandResult(command.Notifications);

            clHelper.PopulateFromPrincipal(claims);
            var item = _repo.GetById(new Guid(command.Id));
            var user = new User(clHelper.Name, new ValueObjects.Email(clHelper.Email), clHelper.Picture);

            if (!ValidateItemAndUser(item, user))
                return new GenericCommandResult(false, "Contato não localizado!", command.Id);

            item.ChangeNome(new ValueObjects.Nome(command.PrimeiroNome, command.UltimoNome));
            item.ChangeEmail(new ValueObjects.Email(command.Email));
            item.ChangeTelefone(new ValueObjects.Telefone(command.TelefoneDDD, command.TelefoneNumero));

            _repo.Update(item);
            return new GenericCommandResult(true, "Contato atualizado com sucesso!", item.Id);
        }

        public ICommandResult Handle(CreateContatoCommand command, ClaimsPrincipal claims)
        {
            //Validação Especifica do Command
            command.Validate();

            //Ja Abortar
            if (command.Invalid)
                return new GenericCommandResult(command.Notifications);


            //verifca se o usuário existe e então o cria caso não exista
            clHelper.PopulateFromPrincipal(claims);
            var user = new User(clHelper.Name, new ValueObjects.Email(clHelper.Email), clHelper.Picture);
            var u = CheckAndCretateUser(user);

            //Instacia e já faz as Validações de Negócio da Entity
            var contato = new Contato(new ValueObjects.Nome(command.PrimeiroNome, command.UltimoNome),
                                      new ValueObjects.Email(command.Email),
                                      new ValueObjects.Telefone(command.TelefoneDDD, command.TelefoneNumero),
                                      u);

            if (contato.Invalid)
                return new GenericCommandResult(contato.Notifications);

            //Tudo Certo
            _repo.Create(contato);
            return new GenericCommandResult(true, "Contato criado com sucesso!", contato.Id);
        }

        private bool ValidateItemAndUser(Contato item, User user)
        {
            return !(item == null || user == null || item.User.Email.Address != user.Email.Address);
        }

        private User CheckAndCretateUser(User user)
        {
            var u = _repoUser.GetByEmail(user.Email);

            if (u == null)
            {
                _repoUser.Create(user);
                u = user;
            }

            return u;
        }
    }
}
