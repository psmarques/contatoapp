using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CadContato.Web.Angular.Models
{
    public class Contato
    {
        public string Id { get; set; }
        public string PrimeiroNome { get; set; }

        public string UltimoNome { get; set; }

        public string Email { get; set; }

        public string TelefoneDDD { get; set; }

        public string TelefoneNumero { get; set; }

    }
}
