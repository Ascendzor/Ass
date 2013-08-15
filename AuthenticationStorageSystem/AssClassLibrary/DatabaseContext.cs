using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace AssClassLibrary
{
    public class DatabaseContext : DbContext
    {

        public DbSet<Client> Clients { get; set; }

        public DbSet<Project> Projects { get; set; }

        public DbSet<Entry> Entries { get; set; }
    }
}
