using ChessTournament.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessTournament.Domain.Models
{
    public class Tournament
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public int MinPlayers { get; set; }
        public int MaxPlayers { get; set; }
        public int? MinELO { get; set; }
        public int? MaxELO { get; set; }
        public ICollection<Category> Categories { get; set; }
        public TournamentStatus Status { get; set; }
        public int CurrentRound { get; set; }
        public bool WomenOnly { get; set; }
        public DateTime RegistrationEndDate { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public ICollection<PlayerTournament> PlayerTournaments { get; set; }
    }

}

