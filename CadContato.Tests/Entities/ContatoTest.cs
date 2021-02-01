using CadContato.Domain.Entities;
using CadContato.Domain.ValueObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CadContato.Tests.Entities
{
    [TestClass]
    public class ContatoTest
    {
        private readonly Nome nomeValido;
        private readonly Email emailInvalido;
        private readonly Email emailValido;
        private readonly Telefone telefoneValido;
        private readonly User usuario;


        public ContatoTest()
        {
            nomeValido = new Nome("João", "Silva");
            emailInvalido = new Email("asd@asd");
            emailValido = new Email("asdasd@asd.com");
            telefoneValido = new Telefone(11, 999912345);
            usuario = new User("Teste", emailValido, string.Empty);
        }

        [TestMethod]
        public void DeveRetornarFalsoEmailInvalido()
        {
            var c = new Contato(nomeValido,
                                emailInvalido,
                                telefoneValido,
                                usuario);

            Assert.IsFalse(c.Valid);
            Assert.IsTrue(c.Notifications.Count > 0);
        }


        [TestMethod]
        public void DeveRetornarTrueEmailValido()
        {
            var c = new Contato(nomeValido,
                                emailValido,
                                telefoneValido,
                                usuario);

            Assert.IsTrue(c.Valid);
        }
    }
}
