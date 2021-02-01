using CadContato.Shared.Commands;
using Flunt.Notifications;
using Flunt.Validations;

namespace CadContato.Domain.Commands.Contato
{
    public class CreateContatoCommand : Notifiable, ICommand
    {
        public string PrimeiroNome { get; set; }

        public string UltimoNome { get; set; }

        public string Email { get; set; }

        public string TelefoneDDD { get; set; }

        public string TelefoneNumero { get; set; }


        public CreateContatoCommand(string primeiroNome, string ultimoNome, string email, string telefoneDDD, string telefoneNumero)
        {
            PrimeiroNome = primeiroNome;
            UltimoNome = ultimoNome;
            Email = email;
            TelefoneDDD = telefoneDDD;
            TelefoneNumero = telefoneNumero;

            Validate();
        }


        public void Validate()
        {
            AddNotifications(new Contract()
                .Requires()
                .IsNotNullOrEmpty(PrimeiroNome, "Primeiro Nome", "Primeiro Nome não pode ser vazio!")
                .IsNotNullOrEmpty(UltimoNome, "Ultimo Nome", "Ultimo Nome não pode ser vazio!")
                .IsNotNullOrEmpty(Email, "Email", "Email não pode ser vazio!"));
        }
    }
}
