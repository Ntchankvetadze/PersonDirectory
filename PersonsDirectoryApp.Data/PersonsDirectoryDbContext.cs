using Microsoft.EntityFrameworkCore;
using PersonsDirectoryApp.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PersonsDirectoryApp.Data
{
    public class PersonsDirectoryDbContext : DbContext
    {
        public DbSet<Person> Persons { get; set; }
        public DbSet<TelephoneNumber> TelephoneNumbers { get; set; }
        public DbSet<PersonRelationMap> PersonRelationMap { get; set; }
        public DbSet<City> City { get; set; }

        public PersonsDirectoryDbContext(DbContextOptions<PersonsDirectoryDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Person>()
                .HasOne(p => p.City);

            modelBuilder.Entity<TelephoneNumber>()
                .HasOne(t => t.Person)
                .WithMany(p => p.TelephoneNumbers)
                .HasForeignKey(t => t.PersonId);

            modelBuilder.Entity<PersonRelationMap>()
                    .HasOne(pm => pm.PersonOne)
                    .WithMany(p => p.PersonOneRelationMaps)
                    .HasForeignKey(pm => pm.PersonOneId)
                    .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<PersonRelationMap>()
                    .HasOne(pm => pm.PersonTwo)
                    .WithMany(p => p.PersonTwoRelationMaps)
                    .HasForeignKey(pm => pm.PersonTwoId)
                    .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
