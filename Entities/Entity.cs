using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace ExchangeRates
{
    public partial class Entity : DbContext
    {
        public Entity()
            : base("name=Entity")
        {
        }

        public virtual DbSet<Currency> Currencies { get; set; }
        public virtual DbSet<ExchangeRate> ExchangeRates { get; set; }
        public virtual DbSet<OfficialRate> OfficialRates { get; set; }
        public virtual DbSet<Operation> Operations { get; set; }
        public virtual DbSet<OperationsHistory> OperationsHistories { get; set; }
        public virtual DbSet<OperationType> OperationTypes { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Currency>()
                .Property(e => e.Code)
                .IsUnicode(false);

            modelBuilder.Entity<Currency>()
                .Property(e => e.CurrencyName)
                .IsUnicode(false);

            modelBuilder.Entity<Currency>()
                .HasMany(e => e.ExchangeRates)
                .WithOptional(e => e.Currency)
                .HasForeignKey(e => e.CurrencyFrom);

            modelBuilder.Entity<Currency>()
                .HasMany(e => e.ExchangeRates1)
                .WithOptional(e => e.Currency1)
                .HasForeignKey(e => e.CurrencyTo);

            modelBuilder.Entity<Currency>()
                .HasMany(e => e.OfficialRates)
                .WithOptional(e => e.Currency1)
                .HasForeignKey(e => e.Currency);

            modelBuilder.Entity<Currency>()
                .HasMany(e => e.Operations)
                .WithOptional(e => e.Currency)
                .HasForeignKey(e => e.CurrencyFrom);

            modelBuilder.Entity<Currency>()
                .HasMany(e => e.Operations1)
                .WithOptional(e => e.Currency1)
                .HasForeignKey(e => e.CurrencyTo);

            modelBuilder.Entity<Currency>()
                .HasMany(e => e.OperationsHistories)
                .WithOptional(e => e.Currency)
                .HasForeignKey(e => e.CurrencyFrom);

            modelBuilder.Entity<Currency>()
                .HasMany(e => e.OperationsHistories1)
                .WithOptional(e => e.Currency1)
                .HasForeignKey(e => e.CurrencyTo);

            modelBuilder.Entity<OperationType>()
                .Property(e => e.Code)
                .IsUnicode(false);

            modelBuilder.Entity<OperationType>()
                .Property(e => e.OperationName)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.FirstName)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.Surname)
                .IsUnicode(false);
        }
    }
}
