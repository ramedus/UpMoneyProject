using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace UpMoneyProjesi.Models
{
    public partial class WalletContext : DbContext
    {
        public WalletContext()
        {
        }

        public WalletContext(DbContextOptions<WalletContext> options)
            : base(options)
        {
        }


        public virtual DbSet<Budget> Budgets { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Expense> Expenses { get; set; }
        public virtual DbSet<ExpensesType> ExpensesTypes { get; set; }
        public virtual DbSet<MySubscribe> MySubscribes { get; set; }
        public virtual DbSet<SubscribeType> SubscribeTypes { get; set; }
        public virtual DbSet<Wallet> Wallets { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=localhost;Initial Catalog=Wallet;Integrated Security=True;Encrypt=false;TrustServerCertificate=true;MultipleActiveResultSets=true");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Turkish_CI_AS");


            modelBuilder.Entity<Budget>(entity =>
            {
                entity.ToTable("Budget");

                entity.Property(e => e.BudgetId).HasColumnName("budget_id");

                entity.Property(e => e.Budget1)
                    .HasColumnType("decimal(18, 0)")
                    .HasColumnName("budget");

                entity.Property(e => e.BudgetTypeId).HasColumnName("budget_type_id");

                entity.Property(e => e.CustomerId).HasColumnName("customer_id");

                entity.HasOne(d => d.BudgetType)
                    .WithMany(p => p.Budgets)
                    .HasForeignKey(d => d.BudgetTypeId)
                    .HasConstraintName("FK_Budget_Expenses_type");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Budgets)
                    .HasForeignKey(d => d.CustomerId)
                    .HasConstraintName("FK_Budget_Customer");
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.ToTable("Customer");

                entity.Property(e => e.CustomerId).HasColumnName("customer_id");

                entity.Property(e => e.CustomerEmail)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("customer_email");

                entity.Property(e => e.CustomerName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("customer_name");

                entity.Property(e => e.CustomerPassword)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("customer_password");

                entity.Property(e => e.CustomerSurname)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("customer_surname");
            });

            modelBuilder.Entity<Expense>(entity =>
            {
                entity.HasKey(e => e.ExpensesId);

                entity.Property(e => e.ExpensesId).HasColumnName("expenses_id");

                entity.Property(e => e.CustomerId).HasColumnName("customer_id");

                entity.Property(e => e.ExpensesFee)
                    .HasColumnType("decimal(18, 0)")
                    .HasColumnName("expenses_fee");

                entity.Property(e => e.ExpensesTypeId).HasColumnName("expenses_type_id");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Expenses)
                    .HasForeignKey(d => d.CustomerId)
                    .HasConstraintName("FK_Expenses_Customer");

                entity.HasOne(d => d.ExpensesType)
                    .WithMany(p => p.Expenses)
                    .HasForeignKey(d => d.ExpensesTypeId)
                    .HasConstraintName("FK_Expenses_Expenses_type");
            });

            modelBuilder.Entity<ExpensesType>(entity =>
            {
                entity.ToTable("Expenses_type");

                entity.Property(e => e.ExpensesTypeId).HasColumnName("expenses_type_id");

                entity.Property(e => e.ExpensesName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("expenses_name");
            });

            modelBuilder.Entity<MySubscribe>(entity =>
            {
                entity.HasKey(e => e.SubscribeId);

                entity.ToTable("My_subscribe");

                entity.Property(e => e.SubscribeId)
                    
                    .HasColumnName("subscribe_id");

                entity.Property(e => e.CustomerId).HasColumnName("customer_id");

                entity.Property(e => e.SubscribeTypeId).HasColumnName("subscribe_type_id");

                entity.Property(e => e.SubscribeValue)
                    .HasColumnType("decimal(18, 0)")
                    .HasColumnName("subscribe_value");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.MySubscribes)
                    .HasForeignKey(d => d.CustomerId)
                    .HasConstraintName("FK_My_subscribe_Customer");

                entity.HasOne(d => d.SubscribeType)
                    .WithMany(p => p.MySubscribes)
                    .HasForeignKey(d => d.SubscribeTypeId)
                    .HasConstraintName("FK_My_subscribe_Subscribe_type");
            });

            modelBuilder.Entity<SubscribeType>(entity =>
            {
                entity.ToTable("Subscribe_type");

                entity.Property(e => e.SubscribeTypeId).HasColumnName("subscribe_type_id");

                entity.Property(e => e.SubscribeName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("subscribe_name");
            });

            modelBuilder.Entity<Wallet>(entity =>
            {
                entity.ToTable("Wallet");

                entity.Property(e => e.WalletId).HasColumnName("wallet_id");

                

                entity.Property(e => e.Balance)
                    .HasColumnType("decimal(18, 0)")
                    .HasColumnName("balance");

                entity.Property(e => e.CustomerId).HasColumnName("customer_id");

                

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Wallets)
                    .HasForeignKey(d => d.CustomerId)
                    .HasConstraintName("FK_Wallet_Customer");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
