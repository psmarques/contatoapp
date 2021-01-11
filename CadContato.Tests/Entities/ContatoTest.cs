using CadContato.Domain.Entities;
using CadContato.Domain.ValueObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CadContato.Tests.Entities
{
    [TestClass]
    public class ContatoTest
    {
        private Nome nomeValido;
        private Email emailInvalido;
        private Email emailValido;
        private Telefone telefoneValido;


        public ContatoTest()
        {
            nomeValido = new Nome("João", "Silva");
            emailInvalido = new Email("asd@asd");
            emailValido = new Email("asdasd@asd.com");
            telefoneValido = new Telefone(11, 999912345);
        }

        [TestMethod]
        public void DeveRetornarFalsoEmailInvalido()
        {
            var c = new Contato(nomeValido,
                                emailInvalido,
                                telefoneValido);

            Assert.IsFalse(c.Valid);
            Assert.IsTrue(c.Notifications.Count > 0);
        }

    }
}
