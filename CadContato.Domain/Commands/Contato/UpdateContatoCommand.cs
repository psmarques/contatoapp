using CadContato.Shared.Commands;
using Flunt.Notifications;
using Flunt.Validations;

namespace CadContato.Domain.Commands.Contato
{
    public class UpdateContatoCommand : Notifiable, ICommand
    {
        public string Id { get; set; }

        public string PrimeiroNome { get; set; }

        public string UltimoNome { get; set; }

        public string Email { get; set; }

        public string TelefoneDDD { get; set; }

        public string TelefoneNumero { get; set; }

        public UpdateContatoCommand(string id, string primeiroNome, string ultimoNome, string email, string telefoneDDD, string telefoneNumero)
        {
            Id = id;
            PrimeiroNome = primeiroNome;
            UltimoNome = ultimoNome;
            Email = email;
            TelefoneDDD = telefoneDDD;
            TelefoneNumero = telefoneNumero;
        }

        public void Validate()
        {
            AddNotifications(new Contract()
                .Requires()
                .IsNotNullOrEmpty(Id, "Id", "Id não pode ser vazio!")
                .IsNotNullOrEmpty(PrimeiroNome, "Primeiro Nome", "Primeiro Nome não pode ser vazio!")
                .IsNotNullOrEmpty(UltimoNome, "Ultimo Nome", "Ultimo Nome não pode ser vazio!"));
        }
    }
}
