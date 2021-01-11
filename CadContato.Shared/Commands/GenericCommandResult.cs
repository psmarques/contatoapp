using Flunt.Notifications;
using System;
using System.Collections.Generic;
using System.Text;

namespace CadContato.Shared.Commands
{
    public class GenericCommandResult : ICommandResult
    {
        public bool Success { get; set; }

        public string Message { get; set; }

        public object Data { get; set; }

        public GenericCommandResult(bool success, string message, object data)
        {
            Success = success;
            Message = message;
            Data = data;
        }

        public GenericCommandResult(IReadOnlyCollection<Notification> errors)
        {
            if (errors == null) return;

            Success = false;
            var sb = new StringBuilder();

            foreach (var er in errors)
                sb.AppendLine($"{er.Message}");

            Message = sb.ToString();
        }
    }
}
