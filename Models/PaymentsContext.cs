using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.OpenApi.Extensions;
using WebAppPayments.Models.Enumerations;

namespace WebAppPayments.Models
{
    public partial class PaymentsContext : DbContext
    {
        public PaymentsContext()
        {
        }

        public PaymentsContext(DbContextOptions<PaymentsContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Client> Clients { get; set; } = null!;
        public virtual DbSet<Payment> Payments { get; set; } = null!;
        

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {

            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Client>(entity =>
            {
                entity.ToTable("Client");

                entity.Property(e => e.LastName)
                    .HasMaxLength(100)
                    .IsFixedLength();

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .IsFixedLength();
            });
           
            modelBuilder.Entity<Payment>(entity =>
            {
                entity.ToTable("Payment");

                entity.Property(e => e.PaymentDate).HasColumnType("datetime");
                

                entity.Property(e => e.PaymentDescription)
                    .HasMaxLength(100)
                    .IsUnicode(false);
                    
                entity.Property(e => e.PaymentTypeId)
                    .HasConversion(item => item, 
                        item => (PaymentType)Enum.Parse(typeof(PaymentType), 
                            item.ToString()));


            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
