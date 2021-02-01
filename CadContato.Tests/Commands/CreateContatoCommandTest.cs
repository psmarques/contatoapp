using CadContato.Domain.Commands.Contato;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CadContato.Tests.Commands
{
    [TestClass]
    public class CreateContatoCommandTest
    {
        public CreateContatoCommand CommandValido { get; set; }
        public CreateContatoCommand CommandInvalido { get; set; }

        public CreateContatoCommandTest()
        {
            CommandValido = new CreateContatoCommand("Jose", "Silva", "email@test.com", "11", "999912345");
            CommandInvalido = new CreateContatoCommand(string.Empty, string.Empty, "email@test.com", "11", "999912345");
        }

        [TestMethod]
        public void DeveRetornarFalsoNomeVazio()
        {
            Assert.IsFalse(CommandInvalido.Valid);
        }

        [TestMethod]
        public void DeveRetornarFalsoEmailVazio()
        {
            var cmd = new CreateContatoCommand("Nome", "Ultimo NOme", string.Empty, "11", "999912345");

            Assert.IsFalse(cmd.Valid);
        }

    }
}
