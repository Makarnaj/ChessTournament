using ChessTournament.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessTournament.DAL.Database.Configurations
{
    public class TournamentConfiguration : IEntityTypeConfiguration<Tournament>
    {
        public void Configure(EntityTypeBuilder<Tournament> builder)
        {
            builder.HasKey(t => t.Id);

            builder.Property(t => t.Name)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(t => t.Location)
                   .HasMaxLength(200);

            builder.Property(t => t.MinPlayers)
                   .IsRequired();

            builder.Property(t => t.MaxPlayers)
                   .IsRequired();

            builder.Property(t => t.MinELO)
                   .HasDefaultValue(0);

            builder.Property(t => t.MaxELO)
                   .HasDefaultValue(3000);


            builder.Property(t => t.Status)
                   .IsRequired()
                   .HasConversion<string>();

            builder.Property(t => t.CurrentRound)
                   .HasDefaultValue(0);

            builder.Property(t => t.WomenOnly)
                   .IsRequired();

            builder.Property(t => t.RegistrationEndDate)
                   .IsRequired();

            builder.Property(t => t.CreationDate)
                   .IsRequired()
                   .HasDefaultValueSql("GETDATE()");

            builder.Property(t => t.UpdateDate)
                   .IsRequired()
                   .HasDefaultValueSql("GETDATE()");


            builder.HasMany(t => t.Categories)
               .WithMany(c => c.Tournaments)
               .UsingEntity<Dictionary<string, object>>(
                   "TournamentCategory", // Name of the join table
                   join => join.HasOne<Category>().WithMany().HasForeignKey("CategoryId"),
                   join => join.HasOne<Tournament>().WithMany().HasForeignKey("TournamentId"),
                   join =>
                   {
                       join.HasKey("TournamentId", "CategoryId"); // Composite key
                   });
        }
    }
}
