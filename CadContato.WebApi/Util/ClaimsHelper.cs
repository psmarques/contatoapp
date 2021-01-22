using CadContato.WebApi.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CadContato.WebApi.Util
{
    public class ClaimsHelper
    {

        public static UserDTO GetUserDtoFromHttpContext(HttpContext ctx)
        {
            var ret = new UserDTO();

            foreach(var claim in ctx.User.Claims)
            {
                if (claim.Type == "email")
                    ret.Email = claim.Value;

                if (claim.Type == "name")
                    ret.Name = claim.Value;

                if (claim.Type == "id")
                    ret.Id = claim.Value;
            }

            return ret;
        }
    }
}
