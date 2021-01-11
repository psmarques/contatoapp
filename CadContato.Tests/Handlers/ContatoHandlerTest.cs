using CadContato.Domain.Commands.Contato;
using CadContato.Domain.Handlers;
using CadContato.Shared.Commands;
using CadContato.Tests.Commands;
using CadContato.Tests.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace CadContato.Tests.Handlers
{
    [TestClass]
    public class ContatoHandlerTest
    {


        [TestMethod]
        public void DeveRetornarErroEmailInvalido()
        {
            var tst = new CreateContatoCommandTest();
            var handler = new ContatoHandler(new FakeContatoRepository());

            var r = (GenericCommandResult) handler.Handle(tst.CommandInvalido);
            Assert.IsFalse(r.Success);
            Assert.IsNotNull(r.Message);

        }

    }
}
