

namespace ServiceAgregator.DataBase
{
    using ServiceAgregator.Models;
    using System;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity;
    using System.Linq;
    public partial class DBContext : DbContext
    {
        public DBContext()
            : base("name=DBContext")
        {
        }

        public virtual DbSet<Orders> Orders { get; set; }
        public virtual DbSet<Reviews> Reviews { get; set; }
        public virtual DbSet<Services> Services { get; set; }
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }
        public virtual DbSet<Tags> Tags { get; set; }
        public virtual DbSet<Users> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Reviews>()
                .Property(e => e.Text)
                .IsUnicode(false);

            modelBuilder.Entity<Services>()
                .Property(e => e.Tag)
                .IsFixedLength();

            modelBuilder.Entity<Services>()
                .Property(e => e.Cost)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Services>()
                .HasMany(e => e.Orders)
                .WithRequired(e => e.Services)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Tags>()
             .HasMany(e => e.Services)
             .WithRequired(e => e.Tags)
             .WillCascadeOnDelete(false);

            modelBuilder.Entity<Users>()
                .Property(e => e.Password)
                .IsFixedLength();

            modelBuilder.Entity<Users>()
                .HasMany(e => e.Orders)
                .WithRequired(e => e.Users)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Users>()
                .HasMany(e => e.ReviewsSender)
                .WithOptional(e => e.UsersSender)
                .HasForeignKey(e => e.Sender_Id);

            modelBuilder.Entity<Users>()
                .HasMany(e => e.ReviewsRecepient)
                .WithOptional(e => e.UsersRecepient)
                .HasForeignKey(e => e.Recipient_Id);

            modelBuilder.Entity<Users>()
                .HasMany(e => e.Services)
                .WithRequired(e => e.Users)
                .WillCascadeOnDelete(false);
        }
    }
}
