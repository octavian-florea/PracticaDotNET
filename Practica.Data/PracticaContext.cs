using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Practica.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Practica.Data
{
    public class PracticaContext: IdentityDbContext<PracticaUser>
    {
        public DbSet<Activity> Activities { get; set; }
        public DbSet<ActivityType> ActivityTypes { get; set; }
        public DbSet<Aplication> Aplications { get; set; }
        public DbSet<University> Universities { get; set; }
        public DbSet<Faculty> Faculties { get; set; }

        public PracticaContext(DbContextOptions<PracticaContext> options):base(options)
        {

        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer("Server=DESKTOP-28P3VE1;Database=practica;Trusted_Connection=True");
        //    base.OnConfiguring(optionsBuilder);
        //}
    }
}
