 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace OfficeApp.Models
{
    public class OfficeContext : DbContext
    {
        public OfficeContext(DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<Person> Persons { get; set; }
        public DbSet<Device> Devices { get; set; }
        public DbSet<Office> Offices { get; set; }
        public DbSet<Usage> Usages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        //base.OnModelCreating(modelBuilder);
        /*
        // Persons data
        modelBuilder.Entity<Person>().HasData(new Person {Id = 1, FirstName = "Andrej", LastName = "Bojic", OfficeId = 1},
            new Person{ Id = 2, FirstName = "Dejan", LastName = "Jaredic", OfficeId = 2 },
            new Person{ Id = 3, FirstName = "Bane", LastName = "Vujovic", OfficeId = 3 },
            new Person{ Id = 4, FirstName = "Magdalena", LastName = "Doderovic", OfficeId = 3 });

        // Offices data
        modelBuilder.Entity<Office>().HasData(new Office{Id = 1, Description = "Backend"},
            new Office { Id = 2, Description = "Marketing" },
            new Office { Id = 3, Description = "Finansije" },
            new Office { Id = 4, Description = "Prodaja" });

        // Devices data
        modelBuilder.Entity<Device>().HasData(new Device {Id = 1, Name = "Telefon"},
            new Device { Id = 2, Name = "Desktop"},
            new Device { Id = 3, Name = "Tablet"},
            new Device { Id = 4, Name = "Laptop"});

        // Usages data
        modelBuilder.Entity<Usage>().HasData(new Usage{ Id = 1, PersonId = 1, Deviceid = 4, usedFrom = new DateTime(2019, 01, 12), usedTo = new DateTime(2019, 01, 13)},
            new Usage {Id = 2, PersonId = 1, Deviceid = 3, usedFrom = new DateTime(2018, 05, 12), usedTo = new DateTime(2019, 01, 13) },
            new Usage { Id = 3, PersonId = 2, Deviceid = 1, usedFrom = new DateTime(2017, 01, 12), usedTo = new DateTime(2019, 01, 13) },
            new Usage { Id = 4, PersonId = 4, Deviceid = 2, usedFrom = new DateTime(2018, 02, 12), usedTo = new DateTime(2018, 12, 20) });
            */
    }
    }
}
