using CadContato.Domain.Entities;
using CadContato.Domain.ValueObjects;
using Flunt.Notifications;
using Microsoft.EntityFrameworkCore;

namespace CadContato.Domain.Infra.Contexts
{
    public class DataContext : DbContext
    {
        public bool IsInMemory { get; private set; }

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            IsInMemory = Database.IsInMemory();
        }

        public DbSet<Contato> Contatos {get;set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Ignore<Notification>();
            modelBuilder.Ignore<Nome>();
            modelBuilder.Ignore<Email>();
            modelBuilder.Ignore<Telefone>();

            modelBuilder.Entity<Contato>().ToTable("Contato");

            modelBuilder.Entity<Contato>().OwnsOne(x => x.Nome).Property(x => x.PrimeiroNome).HasColumnName("PrimeiroNome").HasMaxLength(250);
            modelBuilder.Entity<Contato>().OwnsOne(x => x.Nome).Property(x => x.UltimoNome).HasColumnName("UltimoNome").HasMaxLength(250);

            modelBuilder.Entity<Contato>().OwnsOne(x => x.Email).Property(x => x.Address).HasColumnName("Email").HasMaxLength(250);

            modelBuilder.Entity<Contato>().OwnsOne(x => x.Telefone).Property(x => x.DDD).HasColumnName("TelefoneDDD");
            modelBuilder.Entity<Contato>().OwnsOne(x => x.Telefone).Property(x => x.Numero).HasColumnName("TelefoneNumero");
        }
    }
}
