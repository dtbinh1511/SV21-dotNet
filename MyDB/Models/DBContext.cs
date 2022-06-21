using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace MyDB.Models
{
    public partial class DBContext : DbContext
    {
        public DBContext()
            : base("name=StrConnect")
        {
        }

        public virtual DbSet<Author> Authors { get; set; }
        public virtual DbSet<Book> Books { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Link> Links { get; set; }
        public virtual DbSet<Menu> Menus { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderDetail> OrderDetails { get; set; }
        public virtual DbSet<Post> Posts { get; set; }
        public virtual DbSet<Publisher> Publishers { get; set; }
        public virtual DbSet<Slide> Slides { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Author>()
                .Property(e => e.Slug)
                .IsUnicode(false);

            modelBuilder.Entity<Book>()
                .Property(e => e.Image)
                .IsUnicode(false);

            modelBuilder.Entity<Book>()
                .Property(e => e.Price)
                .HasPrecision(10, 0);

            modelBuilder.Entity<Book>()
                .Property(e => e.Rate)
                .HasPrecision(2, 2);

            modelBuilder.Entity<Book>()
                .Property(e => e.Size)
                .IsUnicode(false);

            modelBuilder.Entity<Book>()
                .Property(e => e.Weight)
                .IsUnicode(false);

            modelBuilder.Entity<Book>()
                .Property(e => e.Slug)
                .IsUnicode(false);

            modelBuilder.Entity<Category>()
                .Property(e => e.Slug)
                .IsUnicode(false);

            modelBuilder.Entity<Link>()
                .Property(e => e.Slug)
                .IsUnicode(false);

            modelBuilder.Entity<Link>()
                .Property(e => e.Type)
                .IsUnicode(false);

            modelBuilder.Entity<Menu>()
                .Property(e => e.Link)
                .IsUnicode(false);

            modelBuilder.Entity<Menu>()
                .Property(e => e.TypeMenu)
                .IsUnicode(false);

            modelBuilder.Entity<Menu>()
                .Property(e => e.Position)
                .IsUnicode(false);

            modelBuilder.Entity<Order>()
                .Property(e => e.Slug)
                .IsUnicode(false);

            modelBuilder.Entity<Order>()
                .Property(e => e.ReceiverPhone)
                .IsUnicode(false);

            modelBuilder.Entity<OrderDetail>()
                .Property(e => e.Price)
                .HasPrecision(10, 2);

            modelBuilder.Entity<OrderDetail>()
                .Property(e => e.Amount)
                .HasPrecision(10, 2);

            modelBuilder.Entity<Post>()
                .Property(e => e.Slug)
                .IsUnicode(false);

            modelBuilder.Entity<Post>()
                .Property(e => e.PostType)
                .IsUnicode(false);

            modelBuilder.Entity<Publisher>()
                .Property(e => e.Slug)
                .IsUnicode(false);

            modelBuilder.Entity<Slide>()
                .Property(e => e.Image)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.Phone)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.Slug)
                .IsUnicode(false);
        }
    }
}
