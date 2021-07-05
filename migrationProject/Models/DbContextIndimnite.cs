using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using migrationProject.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;


namespace migrationProject.Models
{
    public class DbContextIndimnite : DbContext
    {
        public DbContextIndimnite()
        {
        }

        public DbContextIndimnite(DbContextOptions options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                IConfigurationRoot configuration = new ConfigurationBuilder()
               .SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile("appsettings.json")
               .Build();
                var connectionString = configuration.GetConnectionString("migrateDatabase");
                optionsBuilder.UseSqlServer(connectionString);
            }

        }

        public DbSet<Personnel> personnels { get; set; }
        public DbSet<Cadre> cadres { get; set; }
        public DbSet<Corps> corp { get; set; }
        public DbSet<Grades> grade { get; set; }
        public DbSet<OrdreMission> ordremission { get; set; }
        public object OrdreMission { get; internal set; }
        public DbSet<OrdrePayment> ordrepaiement { get; set; }
        public DbSet<OrdreVirement> ordrevirement { get; set; }
        public DbSet<Trajet> trajet { get; set; }
        public DbSet<Parametre_voiture> param_voiture { get; set; }
        public DbSet<Parameter> parametre { get; set; }
        public DbSet<Parametre_paiement> param_paiement { get; set; }
        public DbSet<Parametre_image> parametre_img { get; set; }
    }
}