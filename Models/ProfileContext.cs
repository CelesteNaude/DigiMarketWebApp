using DigiMarketWebApp.Areas.Identity.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DigiMarketWebApp.Models
{
    public class ProfileContext : DbContext
    {
        public ProfileContext()
        {

        }
        public ProfileContext(DbContextOptions<ProfileContext> options) : base(options)
        {

        }

        public DbSet<Photo> Photos { get; set; }
        public DbSet<Album> Albums { get; set; }
        public DbSet<UserAccess> UserAccesses { get; set; }
        public DbSet<Metadata> Metadatas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Photo>().ToTable("Photo");
            modelBuilder.Entity<Album>().ToTable("Album");
            modelBuilder.Entity<UserAccess>().ToTable("UserAccess");
            modelBuilder.Entity<Metadata>().ToTable("Metadata");

            modelBuilder
                .Entity<WebAppUser>()
                .Property(e => e.Id)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<Photo>()
                .HasOne(a => a.WebAppUser)
                .WithMany(al => al.Photos)//A single user can have many photos
                .HasForeignKey(ab => ab.Id);

            modelBuilder.Entity<Album>()
                .HasOne(a => a.WebAppUser)
                .WithMany(al => al.Albums)//A single user can have many albums
                .HasForeignKey(ab => ab.Id)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Album>()
                .HasOne(a => a.Photo)
                .WithMany(al => al.Albums)//A single photo can be in many albums
                .HasForeignKey(ab => ab.PhotoID);

            modelBuilder.Entity<UserAccess>()
                .HasOne(u => u.WebAppUser)
                .WithMany(us => us.UserAccesses)
                .HasForeignKey(ue => ue.Id)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<UserAccess>()
                .HasOne(u => u.Photo)
                .WithMany(us => us.UserAccesses)
                .HasForeignKey(ue => ue.PhotoID);
        }
    }
}