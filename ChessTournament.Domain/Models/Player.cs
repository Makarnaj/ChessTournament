using ChessTournament.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessTournament.Domain.Models
{
    public class Player
    {
        public int Id { get; set; }
        public string Pseudo { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public DateTime BirthDate { get; set; }
        public Gender Gender { get; set; }
        public int ELO { get; set; } = 1200; 
        public PlayerRole Role { get; set; }

        public ICollection<PlayerTournament> PlayerTournaments { get; set; }

    }
}
