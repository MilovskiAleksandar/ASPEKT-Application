using ASPEKT.Application.Core.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASPEKT.Application.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options) { }

        public DbSet<Company> Companies { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Country> Countries { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //COMPANY
            modelBuilder.Entity<Company>()
                .Property(x => x.CompanyName)
                .HasMaxLength(50)
                .IsRequired();

            //CONTACT
            modelBuilder.Entity<Contact>()
                .Property(x => x.ContactName)
                .HasMaxLength(50)
                .IsRequired();

            //COUNTRY
            modelBuilder.Entity<Country>()
                .Property(x => x.CountryName)
                .HasMaxLength(50)
                .IsRequired();

            //RELATIONS

            //Company => Contact
            modelBuilder.Entity<Company>()
                .HasMany(x => x.CompanyContacts)
                .WithOne(x => x.Company)
                .HasForeignKey(x => x.CompanyId);

            //Country => Contact
            modelBuilder.Entity<Country>()
                .HasMany(x => x.CountryContacts)
                .WithOne(x => x.Country)
                .HasForeignKey(x => x.CountryId);

            //Contact => Company
            modelBuilder.Entity<Contact>()
                .HasOne(x => x.Company)
                .WithMany(x => x.CompanyContacts)
                .HasForeignKey(x => x.CompanyId);

            //Contact => Country
            modelBuilder.Entity<Contact>()
                .HasOne(x => x.Country)
                .WithMany(x => x.CountryContacts)
                .HasForeignKey(x => x.CountryId);


            //SEED

            modelBuilder.Entity<Company>()
                .HasData(new Company
                {
                    Id = 1,
                    CompanyName = "Apple"
                },
                new Company
                {
                    Id = 2, 
                    CompanyName = "Aspekt"
                });

            modelBuilder.Entity<Country>()
                .HasData(new Country
                {
                    Id = 1,
                    CountryName = "USA"
                },
                new Country
                {
                    Id = 2,
                    CountryName = "Macedonia"
                });

            modelBuilder.Entity<Contact>()
                .HasData(new Contact
                {
                    Id = 1,
                    ContactName = "Aleksandar",
                    CompanyId = 1,
                    CountryId = 1,

                }, new Contact
                {
                    Id = 2,
                    ContactName = "Marko",
                    CompanyId = 2,
                    CountryId = 2,
                });
        }
    }
}
