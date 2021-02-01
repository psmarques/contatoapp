using CadContato.Domain.Handlers;
using CadContato.Shared.Commands;
using CadContato.Tests.Commands;
using CadContato.Tests.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Security.Claims;

namespace CadContato.Tests.Handlers
{
    [TestClass]
    public class ContatoHandlerTest
    {


        [TestMethod]
        public void DeveRetornarErroEmailInvalido()
        {
            var tst = new CreateContatoCommandTest();
            var handler = new ContatoHandler(new FakeContatoRepository(), new FakeUserRepository());
            
            tst.CommandValido.Email = string.Empty;
            var r = (GenericCommandResult)handler.Handle(tst.CommandValido, CriarFakeClaims());
            
            Assert.IsFalse(r.Success);
            Assert.IsNotNull(r.Message);
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
