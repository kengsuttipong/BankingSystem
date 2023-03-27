using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BankingSystem_Challenge.Models;

public partial class BankingSystemChallengeContext : IdentityDbContext<ApplicationUser>
{
    public BankingSystemChallengeContext()
    {
    }

    public BankingSystemChallengeContext(DbContextOptions<BankingSystemChallengeContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Account> Accounts { get; set; }

    public virtual DbSet<AccountType> AccountTypes { get; set; }

    public virtual DbSet<Country> Countries { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Transaction> Transactions { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=DESKTOP-VS1TT45;Database=BankingSystem_Challenge;Trusted_Connection=True;TrustServerCertificate=True;");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Account>(entity =>
        {
            entity.Property(e => e.AccountId).HasColumnName("account_id");
            entity.Property(e => e.AccountType)
                .HasMaxLength(50)
                .HasColumnName("account_type");
            entity.Property(e => e.Balance).HasColumnName("balance");
            entity.Property(e => e.Currency)
                .HasMaxLength(10)
                .HasColumnName("currency");
            entity.Property(e => e.CustomerId)
                .HasMaxLength(50)
                .HasColumnName("customer_id");
            entity.Property(e => e.DateCreated)
                .HasColumnType("datetime")
                .HasColumnName("date_created");
            entity.Property(e => e.Iban)
                .HasMaxLength(50)
                .HasColumnName("iban");
        });

        modelBuilder.Entity<AccountType>(entity =>
        {
            entity.ToTable("AccountType");

            entity.Property(e => e.AccountTypeId).HasColumnName("AccountTypeID");
            entity.Property(e => e.AccountTypeName).HasMaxLength(50);
        });

        modelBuilder.Entity<Country>(entity =>
        {
            entity.ToTable("Country");

            entity.Property(e => e.CountryId).HasColumnName("CountryID");
            entity.Property(e => e.CountryCode).HasMaxLength(5);
            entity.Property(e => e.CountryName).HasMaxLength(50);
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.CustomerId).HasName("PK_Customer");

            entity.Property(e => e.CustomerId).HasColumnName("customer_id");
            entity.Property(e => e.Address)
                .HasMaxLength(1000)
                .HasColumnName("address");
            entity.Property(e => e.DateOfBirth)
                .HasColumnType("date")
                .HasColumnName("date_of_birth");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .HasColumnName("email");
            entity.Property(e => e.FirstName)
                .HasMaxLength(100)
                .HasColumnName("first_name");
            entity.Property(e => e.LastName)
                .HasMaxLength(100)
                .HasColumnName("last_name");
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(50)
                .HasColumnName("phone_number");
        });

        modelBuilder.Entity<Transaction>(entity =>
        {
            entity.Property(e => e.TransactionId).HasColumnName("transaction_id");
            entity.Property(e => e.Amount).HasColumnName("amount");
            entity.Property(e => e.Currency)
                .HasMaxLength(5)
                .HasColumnName("currency");
            entity.Property(e => e.DateExecuted)
                .HasColumnType("datetime")
                .HasColumnName("date_executed");
            entity.Property(e => e.FromAccountId).HasColumnName("from_account_id");
            entity.Property(e => e.ToIban)
                .HasMaxLength(50)
                .HasColumnName("to_iban");
            entity.Property(e => e.TransactionFee).HasColumnName("transaction_fee");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
