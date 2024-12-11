using ChessTournament.API.DTO;
using ChessTournament.Domain.Models;


namespace ChessTournament.API.Mappers
{
    public static class PlayerMappers
    {
        public static PlayerViewDTO ToDTO(this Player player)
        {
            return new PlayerViewDTO
            {
                Id = player.Id,
                Email = player.Email,
                Gender = player.Gender,
                ELO = player.ELO,
                BirthDate = player.BirthDate,
                Pseudo = player.Pseudo,
                Role = player.Role
            };
        }
        public static Player ToPlayer(this PlayerCreateFormDTO player)
        {
            return new Player
            {

                Email = player.Email,
                Gender = player.Gender,
                ELO = player.ELO,
                BirthDate = player.BirthDate,
                Pseudo = player.Pseudo,
                PasswordHash = player.Password
            };
        }
    }
}
