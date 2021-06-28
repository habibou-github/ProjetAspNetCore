﻿using Microsoft.EntityFrameworkCore;
using migrationProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace migrationProject.Models
{
    public class DbContextIndimnite : DbContext
    {
        public DbContextIndimnite(DbContextOptions
    <DbContextIndimnite> options) : base(options)
        {

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