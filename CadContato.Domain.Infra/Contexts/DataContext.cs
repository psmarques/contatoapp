using CadContato.Domain.Entities;
using CadContato.Domain.ValueObjects;
using CadContato.Shared.Util;
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

        public DbSet<Contato> Contatos { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Ignore<ValueObject>();
            modelBuilder.Ignore<Notification>();
            modelBuilder.Ignore<Nome>();
            modelBuilder.Ignore<Email>();
            modelBuilder.Ignore<Telefone>();

            var builderContato = modelBuilder.Entity<Contato>();
            var builderUser = modelBuilder.Entity<User>();

            builderContato.ToTable("Contato");

            builderContato.OwnsOne(x => x.Nome).Property(y => y.PrimeiroNome).HasColumnName("PrimeiroNome").HasDefaultValue(string.Empty).HasMaxLength(250);
            builderContato.OwnsOne(x => x.Nome).Property(y => y.UltimoNome).HasColumnName("UltimoNome").HasDefaultValue(string.Empty).HasMaxLength(250);
            builderContato.OwnsOne(x => x.Email).Property(y => y.Address).HasColumnName("Email").HasDefaultValue(string.Empty).HasMaxLength(250);
            builderContato.OwnsOne(x => x.Telefone).Property(y => y.DDD).HasColumnName("TelefoneDDD");
            builderContato.OwnsOne(x => x.Telefone).Property(y => y.Numero).HasColumnName("TelefoneNumero");

            builderContato.HasOne(u => u.User)
                          .WithMany(c => c.Contatos);

            builderUser.ToTable("User");
            builderUser.Property(x => x.NomeCompleto).HasMaxLength(250);
            builderUser.OwnsOne(x => x.Email).Property(y => y.Address).HasColumnName("EmailUser").HasMaxLength(250);
        }
    }
}
