using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using ConsoleApp38.Entities;
using Microsoft.EntityFrameworkCore;

namespace AppDbContext
{
    public class LibraryContext : DbContext
    {
        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Promotion> Promotions { get; set; }
        public DbSet<Publisher> Publishers { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Sale> Sales { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Store> Stores { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;
                                        Initial Catalog = BookStore;
                                        Integrated Security=True;
                                        Connect Timeout=5;
                                        Encrypt=False;Trust Server Certificate=False;
                                        Application Intent=ReadWrite;Multi Subnet Failover=False");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Reservation>()
                .HasOne(r => r.User)
                .WithMany(u => u.Reservations)
                .HasForeignKey(r => r.UserId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Sale>()
                .HasOne(s => s.User)
                .WithMany(u => u.Sales)
                .HasForeignKey(s => s.UserId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Review>()
                .HasOne(r => r.User)
                .WithMany(u => u.Reviews)
                .HasForeignKey(r => r.UserId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Address>()
                .HasOne(a => a.User)
                .WithMany(u => u.Addresses)
                .HasForeignKey(a => a.UserId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<City>()
                .HasOne(c => c.Country)
                .WithMany(co => co.Cities)
                .HasForeignKey(c => c.CountryId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Author>()
                .Property(a => a.FullName)
                .IsRequired()
                .HasMaxLength(150);
            modelBuilder.Entity<Publisher>()
                .Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(150);
            modelBuilder.Entity<Genre>()
                .Property(g => g.Name)
                .IsRequired()
                .HasMaxLength(100);
            modelBuilder.Entity<Book>()
                .Property(b => b.Title)
                .IsRequired()
                .HasMaxLength(200);

            modelBuilder.Entity<Book>()
                .Property(b => b.Price)
                .IsRequired();

            modelBuilder.Entity<Book>()
                .Property(b => b.Year)
                .IsRequired();
            modelBuilder.Entity<Book>()
                .HasOne(b => b.Author)
                .WithMany(a => a.Books)
                .HasForeignKey(b => b.AuthorId);

            modelBuilder.Entity<Book>()
                .HasOne(b => b.Publisher)
                .WithMany(p => p.Books)
                .HasForeignKey(b => b.PublisherId);

            modelBuilder.Entity<Book>()
                .HasOne(b => b.Genre)
                .WithMany(g => g.Books)
                .HasForeignKey(b => b.GenreId);
        }
    }
}
