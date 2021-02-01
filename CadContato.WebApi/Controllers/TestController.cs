using CadContato.Domain.Commands.Contato;
using CadContato.Domain.Handlers;
using CadContato.Domain.Repositories;
using CadContato.WebApi.Models;
using CadContato.WebApi.Util;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace CadContato.WebApi.Controllers
{
    [ApiController]
    [Route("/api/v1/test")]
    public class TestController : Controller
    {

        [Route("")]
        [HttpGet]
        public IEnumerable<ContatoDTO> GetAll([FromServices] IContatoRepository repo, [FromServices] ContatoHandler hnd, [FromQuery] string filter)
        {
            var cmd = new CreateContatoCommand("Paulo", "Marques", "teste@teste.com", "11", "123456789");
            var fakeClaims = CriarFakeClaims();
            var r = hnd.Handle(cmd, fakeClaims);

            if (!string.IsNullOrEmpty(filter))
            {
                return repo.GetLike(filter)
                           .Select(x => ContatoConverter.Convert(x));
            }

            return repo.GetAll()
                       .Select(x => ContatoConverter.Convert(x));
        }

        [Route("2")]
        [HttpGet]
        public IEnumerable<ContatoDTO> GetByMail([FromServices] IContatoRepository repo, [FromServices] ContatoHandler hnd)
        {
            var cmd = new CreateContatoCommand("Paulo", "Marques", "teste@teste.com", "11", "123456789");
            var fakeClaims = CriarFakeClaims();
            var r = hnd.Handle(cmd, fakeClaims);

            return repo.GetAllByMail("psmarques@gmail.com")
                       .Select(x => ContatoConverter.Convert(x));
        }

        [Route("3")]
        [HttpGet]
        public IEnumerable<ContatoDTO> GetAll([FromServices] IContatoRepository repo)
        {
            return repo.GetAll().Select(x => ContatoConverter.Convert(x));
        }


        private ClaimsPrincipal CriarFakeClaims()
        {
            var cli = new ClaimsIdentity();
            cli.AddClaim(new Claim("emailaddress", "psmarques@gmail.com"));
            cli.AddClaim(new Claim("/name", "Paulo Marques"));
            cli.AddClaim(new Claim("picture", "xyz"));

            var r = new ClaimsPrincipal(cli);

            return r;
        }
    }
}
