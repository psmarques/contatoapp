using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CadContato.WebApi.Models
{
    public class ContatoDTO
    {
        public string Id { get; set; }
        public string PrimeiroNome { get; set; }

        public string UltimoNome { get; set; }

        public string Email { get; set; }

        public string TelefoneDDD { get; set; }

        public string TelefoneNumero { get; set; }

        public string UsuarioEmail { get; set; }

    }
}
