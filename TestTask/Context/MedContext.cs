using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestTask.Context
{
    public class MedContext: DbContext
    {
        public MedContext(DbContextOptions<MedContext> options): base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Patient> Patients { get; set; }
        public DbSet<Graft> Grafts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
