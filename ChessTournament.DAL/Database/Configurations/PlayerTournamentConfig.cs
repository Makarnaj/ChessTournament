using ChessTournament.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace ChessTournament.DAL.Database.Configurations
{
    public class PlayerTournamentConfig : IEntityTypeConfiguration<PlayerTournament>
    {
        public void Configure(EntityTypeBuilder<PlayerTournament> builder)
        {
            builder.HasKey(pt => new { pt.PlayerId, pt.TournamentId });

            // Configuration de la relation entre PlayerTournament et Player
            builder.HasOne(pt => pt.Player)
                   .WithMany(p => p.PlayerTournaments)
                   .HasForeignKey(pt => pt.PlayerId)
                   .OnDelete(DeleteBehavior.Cascade); // Si vous voulez une suppression en cascade

            // Configuration de la relation entre PlayerTournament et Tournament
            builder.HasOne(pt => pt.Tournament)
                   .WithMany(t => t.PlayerTournaments)
                   .HasForeignKey(pt => pt.TournamentId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
