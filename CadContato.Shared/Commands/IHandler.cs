using System;
using System.Collections.Generic;
using System.Text;

namespace CadContato.Shared.Commands
{
    public interface IHandler<T> where T : ICommand
    {
        ICommandResult Handle(T command);
    }
}
