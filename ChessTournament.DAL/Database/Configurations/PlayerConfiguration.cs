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
    public class PlayerConfiguration : IEntityTypeConfiguration<Player>
    {
        public void Configure(EntityTypeBuilder<Player> builder)
        {
            {
                
                builder.HasKey(p => p.Id);

                
                builder.Property(p => p.Pseudo)
                    .IsRequired()
                    .HasMaxLength(50);

                builder.Property(p => p.Email)
                    .IsRequired()
                    .HasMaxLength(100); 

                builder.Property(p => p.PasswordHash)
                    .IsRequired()
                    .HasMaxLength(255); 

                builder.Property(p => p.BirthDate)
                    .IsRequired(); 

                builder.Property(p => p.Gender)
                    .IsRequired()
                    .HasMaxLength(10); 

                builder.Property(p => p.ELO)
                    .HasDefaultValue(1200); 

                
                builder.Property(p => p.Role)
                    .IsRequired()
                    .HasConversion<string>(); 

                
                builder.HasIndex(p => p.Email).IsUnique(); 

              
            }
        }
    }
}
