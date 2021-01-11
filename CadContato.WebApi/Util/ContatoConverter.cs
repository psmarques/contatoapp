using CadContato.Domain.Entities;
using CadContato.WebApi.Models;

namespace CadContato.WebApi.Util
{
    public class ContatoConverter
    {

        public static ContatoDTO Convert(Contato contato)
        {
            return new ContatoDTO
            {
                Id = contato.Id != null ? contato.Id.ToString() : null,
                Email = contato.Email.Address,
                PrimeiroNome = contato.Nome.PrimeiroNome,
                UltimoNome = contato.Nome.UltimoNome,
                TelefoneDDD = contato.Telefone.DDD.HasValue ? contato.Telefone.DDD.ToString() : string.Empty,
                TelefoneNumero = contato.Telefone.Numero.HasValue ? contato.Telefone.Numero.ToString() : string.Empty
            };
        }

    }
}
