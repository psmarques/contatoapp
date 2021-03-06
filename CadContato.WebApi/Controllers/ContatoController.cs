﻿using CadContato.Domain.Commands.Contato;
using CadContato.Domain.Handlers;
using CadContato.Domain.Repositories;
using CadContato.Shared.Commands;
using CadContato.Shared.Util;
using CadContato.WebApi.Models;
using CadContato.WebApi.Util;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace CadContato.WebApi.Controllers
{
    [ApiController]
    [Route("/api/v1/contato")]
    [Authorize]
    public class ContatoController : Controller
    {
        #region Queries

        [Route("")]
        [HttpGet]
        public IEnumerable<ContatoDTO> GetAll([FromServices] IContatoRepository repo, [FromQuery] string filter)
        {
            var clHelper = new ClaimsHelper(HttpContext.User);

            if (!string.IsNullOrEmpty(filter))
            {
                return repo.GetLike(filter)
                           .Select(x => ContatoConverter.Convert(x));
            }

            return repo.GetAllByMail(clHelper.Email)
                       .Select(x => ContatoConverter.Convert(x));
        }

        #endregion

        #region Commands

        //Commands
        [Route("")]
        [HttpPost]
        public GenericCommandResult Create(
            [FromServices] ContatoHandler hnd,
            [FromBody] CreateContatoCommand cmd)
        {
            return (GenericCommandResult)hnd.Handle(cmd, HttpContext.User);
        }

        [Route("")]
        [HttpPut]
        public GenericCommandResult Update(
            [FromServices] ContatoHandler hnd,
            [FromBody] UpdateContatoCommand cmd)
        {
            return (GenericCommandResult)hnd.Handle(cmd, HttpContext.User);
        }

        [Route("")]
        [HttpDelete]
        public GenericCommandResult Delete(
            [FromServices] ContatoHandler hnd,
            [FromBody] DeleteContatoCommand cmd)
        {
            return (GenericCommandResult)hnd.Handle(cmd, HttpContext.User);
        }

        #endregion
    }
}
