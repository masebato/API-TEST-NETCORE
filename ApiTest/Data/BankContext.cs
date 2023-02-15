using System;
using System.Collections.Generic;
using ApiTest.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiTest.Data;

public partial class BankContext : DbContext
{
    public BankContext()
    {
    }

    public BankContext(DbContextOptions<BankContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Account> Accounts { get; set; }

    public virtual DbSet<Client> Clients { get; set; }

    public virtual DbSet<Person> People { get; set; }

    public virtual DbSet<Trasnsaction> Trasnsactions { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {

    }
      

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Account>(entity =>
        {
            entity.HasKey(e => e.AccountId).HasName("PK__ACCOUNT__F267251E95D82BED");

            entity.ToTable("ACCOUNT");

            entity.Property(e => e.AccountId).HasColumnName("accountId");
            entity.Property(e => e.ClientIdFk).HasColumnName("clientIdFK");
            entity.Property(e => e.InitialBalance)
                .HasMaxLength(45)
                .IsUnicode(false)
                .HasColumnName("initialBalance");
            entity.Property(e => e.Number)
                .HasMaxLength(45)
                .IsUnicode(false)
                .HasColumnName("number");
            entity.Property(e => e.State)
                .HasMaxLength(45)
                .IsUnicode(false)
                .HasColumnName("state");
            entity.Property(e => e.Type)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("type");

            entity.HasOne(d => d.oClient).WithMany(p => p.Accounts)
                .HasForeignKey(d => d.ClientIdFk)
                .HasConstraintName("FK_IDCLIENT");
        });

        modelBuilder.Entity<Client>(entity =>
        {
            entity.HasKey(e => e.ClientId).HasName("PK__CLIENT__81A2CBE1B027C828");

            entity.ToTable("CLIENT");

            entity.Property(e => e.ClientId).HasColumnName("clientId");
            entity.Property(e => e.Password)
                .HasMaxLength(45)
                .IsUnicode(false);
            entity.Property(e => e.PersonIdFk).HasColumnName("personIdFK");
            entity.Property(e => e.State)
                .HasMaxLength(45)
                .IsUnicode(false);

            entity.HasOne(d => d.oPerson).WithMany(p => p.Clients)
                .HasForeignKey(d => d.PersonIdFk)
                .HasConstraintName("FK_IDPERSON");
        });

        modelBuilder.Entity<Person>(entity =>
        {
            entity.HasKey(e => e.PersonId).HasName("PK__PERSON__EC7D7D4D219CA97A");

            entity.ToTable("PERSON");

            entity.Property(e => e.PersonId).HasColumnName("personId");
            entity.Property(e => e.Address)
                .HasMaxLength(45)
                .IsUnicode(false)
                .HasColumnName("address");
            entity.Property(e => e.Age)
                .HasMaxLength(45)
                .IsUnicode(false)
                .HasColumnName("age");
            entity.Property(e => e.Gender)
                .HasMaxLength(45)
                .IsUnicode(false)
                .HasColumnName("gender");
            entity.Property(e => e.Identification)
                .HasMaxLength(45)
                .IsUnicode(false)
                .HasColumnName("identification");
            entity.Property(e => e.Name)
                .HasMaxLength(45)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.Phone)
                .HasMaxLength(45)
                .IsUnicode(false)
                .HasColumnName("phone");
        });

        modelBuilder.Entity<Trasnsaction>(entity =>
        {
            entity.HasKey(e => e.TransactionId).HasName("PK__TRASNSAC__9B57CF72EB4E719B");

            entity.ToTable("TRASNSACTION");

            entity.Property(e => e.TransactionId).HasColumnName("transactionId");
            entity.Property(e => e.AccountIdFk).HasColumnName("accountIdFK");
            entity.Property(e => e.Balance)
                .HasMaxLength(45)
                .IsUnicode(false)
                .HasColumnName("balance");
            entity.Property(e => e.DateTransaction)
                .HasColumnType("date")
                .HasColumnName("dateTransaction");
            entity.Property(e => e.Type)
                .HasMaxLength(45)
                .IsUnicode(false)
                .HasColumnName("type");
            entity.Property(e => e.Value)
                .HasMaxLength(45)
                .IsUnicode(false)
                .HasColumnName("value");

            entity.HasOne(d => d.oAccount).WithMany(p => p.Trasnsactions)
                .HasForeignKey(d => d.AccountIdFk)
                .HasConstraintName("FK_IDACCOUNT");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
