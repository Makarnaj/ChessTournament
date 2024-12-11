using ChessTournament.Domain.Enums;
using static ChessTournament.Domain.Models.Player;

namespace ChessTournament.API.DTO
{
    public class PlayerViewDTO
    {
        public int Id { get; set; }
        public string Pseudo { get; set; }
        public string Email { get; set; }
        public DateTime BirthDate { get; set; }
        public Gender Gender { get; set; }
        public int ELO { get; set; }
        public PlayerRole Role { get; set; }
    }
}
