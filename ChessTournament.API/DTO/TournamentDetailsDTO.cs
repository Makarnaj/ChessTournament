using ChessTournament.Domain.Enums;

namespace ChessTournament.API.DTO
{
    public class TournamentDetailsDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public int MinPlayers { get; set; }
        public int MaxPlayers { get; set; }
        public int? MinElo { get; set; }
        public int? MaxElo { get; set; }
        public List<string> Categories { get; set; }
        public TournamentStatus Status { get; set; }
        public DateTime RegistrationEndDate { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public int CurrentRound { get; set; }
        public List<PlayerViewDTO> Players { get; set; } 
    }
}
