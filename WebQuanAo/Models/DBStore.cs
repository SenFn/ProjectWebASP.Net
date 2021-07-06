using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace WebQuanAo.Models
{
    public partial class DBStore : DbContext
    {
        public DBStore()
            : base("name=DBStore")
        {
        }

        public virtual DbSet<account> accounts { get; set; }
        public virtual DbSet<bodySave> bodySaves { get; set; }
        public virtual DbSet<cart> carts { get; set; }
        public virtual DbSet<headerSave> headerSaves { get; set; }
        public virtual DbSet<product> products { get; set; }
        public virtual DbSet<cardInfo> cardInfoes { get; set; }
        public virtual DbSet<productInfo> productInfoes { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<account>()
                .Property(e => e.username)
                .IsUnicode(false);

            modelBuilder.Entity<account>()
                .Property(e => e.password)
                .IsUnicode(false);

            modelBuilder.Entity<account>()
                .Property(e => e.email)
                .IsUnicode(false);

            modelBuilder.Entity<account>()
                .Property(e => e.phone)
                .IsUnicode(false);

            modelBuilder.Entity<account>()
                .HasMany(e => e.carts)
                .WithRequired(e => e.account)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<cart>()
                .HasMany(e => e.cardInfoes)
                .WithRequired(e => e.cart)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<product>()
                .Property(e => e.srcImage)
                .IsUnicode(false);

            modelBuilder.Entity<product>()
                .Property(e => e.price)
                .HasPrecision(19, 4);

            modelBuilder.Entity<product>()
                .HasMany(e => e.cardInfoes)
                .WithRequired(e => e.product)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<cardInfo>()
                .Property(e => e.price)
                .HasPrecision(19, 4);

            modelBuilder.Entity<productInfo>()
                .Property(e => e.price)
                .HasPrecision(19, 4);
        }
    }
}
