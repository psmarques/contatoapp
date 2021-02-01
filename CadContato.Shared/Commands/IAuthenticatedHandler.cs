using System.Security.Claims;

namespace CadContato.Shared.Commands
{
    public interface IAuthenticatedHandler<T> where T : ICommand
    {
        ICommandResult Handle(T command, ClaimsPrincipal claims);
    }
}
