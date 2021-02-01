using CadContato.Domain.Entities;
using CadContato.Domain.Queries;
using CadContato.Domain.ValueObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace CadContato.Tests.Queries
{
    [TestClass]
    public class ContatoQueriesTest
    {
        private readonly ICollection<Contato> repo;

        public ContatoQueriesTest()
        {
            repo = new List<Contato>();
        }

        private void PopularLista()
        {
            repo.Add(new Contato(
                new Nome("João", "Da Silva"),
                new Email("joao@contatoapp.com"),
                new Telefone(11, 12345678)));

            repo.Add(new Contato(
                new Nome("Maria", "Da Silva"),
                new Email("maria@contatoapp.com"),
                new Telefone(11, 12345678)));

            repo.Add(new Contato(
                new Nome("Pedro", "Da Silva"),
                new Email("pedro@contatoapp.com"),
                new Telefone(11, 12345678)));

            repo.Add(new Contato(
                new Nome("Marcos", "Santos"),
                new Email("marcos@contatoapp.com"),
                new Telefone(11, 12345678)));
        }


        [TestMethod]
        public void DeveRetornarContatosComSobrenomeSilva()
        {
            PopularLista();

            var r = repo.AsQueryable().Where(ContatoQueries.GetLike("Silva"));

            Assert.AreEqual(r.Count(), 3);
        }

    }
}
