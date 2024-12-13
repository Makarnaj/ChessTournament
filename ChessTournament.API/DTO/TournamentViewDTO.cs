using ChessTournament.Domain.Enums;
using ChessTournament.Domain.Models;

namespace ChessTournament.API.DTO
{
    public class TournamentViewDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public int MinPlayers { get; set; }
        public int MaxPlayers { get; set; }
        public int? MinELO { get; set; }
        public int? MaxELO { get; set; }
        public ICollection<string> Categories { get; set; } // Liste des noms des catégories
        public bool WomenOnly { get; set; }
        public DateTime RegistrationEndDate { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public TournamentStatus Status { get; set; }
        public int CurrentRound { get; set; }
    }

    public class TournamentListViewDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime UpdateDate { get; set; }
    }
}
