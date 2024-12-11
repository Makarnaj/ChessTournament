using ChessTournament.DAL.Database.Configurations;
using ChessTournament.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace ChessTournament.DAL.Database
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Player> Players { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Appliquer les configurations de chaque entité
            modelBuilder.ApplyConfiguration(new PlayerConfiguration());
           // modelBuilder.ApplyConfiguration(new TournamentConfiguration());
           // modelBuilder.ApplyConfiguration(new CategoryConfiguration());
            // Vous pouvez ajouter d'autres configurations ici
        }




    }
}
