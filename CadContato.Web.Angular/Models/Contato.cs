namespace CadContato.Web.Angular.Models
{
    public class Contato
    {
        public Contato()
        {
        }

        public Contato(string id)
        {
            Id = id;
        }

        public Contato(string id, string primeiroNome, string ultimoNome, string email, string telefoneDDD, string telefoneNumero)
        {
            Id = id;
            PrimeiroNome = primeiroNome;
            UltimoNome = ultimoNome;
            Email = email;
            TelefoneDDD = telefoneDDD;
            TelefoneNumero = telefoneNumero;
        }

        public string Id { get; set; }
        public string PrimeiroNome { get; set; }

        public string UltimoNome { get; set; }

        public string Email { get; set; }

        public string TelefoneDDD { get; set; }

        public string TelefoneNumero { get; set; }

    }
}
