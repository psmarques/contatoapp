using CadContato.Domain.Commands.Contato;
using CadContato.Domain.Entities;
using CadContato.Domain.Handlers;
using CadContato.Domain.Repositories;
using CadContato.WebApi.Models;
using CadContato.WebApi.Util;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
            cmd.UsuarioEmail = "psmarques@gmail.com";
            cmd.UsuarioNome = "Paulo";

            var r = hnd.Handle(cmd);

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
        public IEnumerable<ContatoDTO> GetByMail([FromServices] IContatoRepository repo, [FromServices] ContatoHandler hnd, [FromQuery] string filter)
        {
            var cmd = new CreateContatoCommand("Paulo", "Marques", "teste@teste.com", "11", "123456789");
            cmd.UsuarioEmail = "psmarques@gmail.com";
            cmd.UsuarioNome = "Paulo";

            var r = hnd.Handle(cmd);

            return repo.GetAllByMail(cmd.UsuarioEmail)
                       .Select(x => ContatoConverter.Convert(x));
        }

        [Route("3")]
        [HttpGet]
        public IEnumerable<ContatoDTO> GetAll([FromServices] IContatoRepository repo)
        {
            return repo.GetAll().Select(x => ContatoConverter.Convert(x));
        }



    }
}
