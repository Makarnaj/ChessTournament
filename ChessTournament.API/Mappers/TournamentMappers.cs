using ChessTournament.API.DTO;
using ChessTournament.Domain.Models;

namespace ChessTournament.API.Mappers
{
    public static class TournamentMappers
    {
        public static TournamentViewDTO ToDTO(this Tournament tournament)
        {
            return new TournamentViewDTO
            {
                Id = tournament.Id,
                Name = tournament.Name,
                Location = tournament.Location,
                MinPlayers = tournament.MinPlayers,
                MaxPlayers = tournament.MaxPlayers,
                MinELO = tournament.MinELO,
                MaxELO = tournament.MaxELO,
                Categories = tournament.Categories.Select(c => c.Name).ToList(),
                WomenOnly = tournament.WomenOnly,
                RegistrationEndDate = tournament.RegistrationEndDate,
                CreationDate = tournament.CreationDate,
                UpdateDate = tournament.UpdateDate,
                Status = tournament.Status,
                CurrentRound = tournament.CurrentRound
            };
        }

        public static Tournament ToTournament(this TournamentCreateFormDTO dto, ICollection<Category> categories)
        {
            return new Tournament
            {
                Name = dto.Name,
                Location = dto.Location,
                MinPlayers = dto.MinPlayers,
                MaxPlayers = dto.MaxPlayers,
                MinELO = dto.MinELO,
                MaxELO = dto.MaxELO,
                WomenOnly = dto.WomenOnly,
                RegistrationEndDate = dto.RegistrationEndDate,
                CreationDate = dto.CreationDate,
                UpdateDate = dto.UpdateDate,
                Categories = categories.ToList() 
            };
        }

        public static TournamentListViewDTO ToListDTO(this Tournament tournament)
        {
            return new TournamentListViewDTO
            {
                Id = tournament.Id,
                Name = tournament.Name,
                UpdateDate = tournament.UpdateDate
            };
        }

        public static TournamentDetailsDTO ToTournamentDetailsDTO(this Tournament tournament)
        {
            return new TournamentDetailsDTO
            {
                Id = tournament.Id,
                Name = tournament.Name,
                Location = tournament.Location,
                MinPlayers = tournament.MinPlayers,
                MaxPlayers = tournament.MaxPlayers,
                MinElo = tournament.MinELO,
                MaxElo = tournament.MaxELO,
                Categories = tournament.Categories.Select(c => c.Name).ToList(),
                Status = tournament.Status,
                RegistrationEndDate = tournament.RegistrationEndDate,
                CreationDate = tournament.CreationDate,
                UpdateDate = tournament.UpdateDate,
                CurrentRound = tournament.CurrentRound,
                Players = tournament.PlayerTournaments
            .Select(pt => new PlayerViewDTO
            {
                Id = pt.Player.Id,
                Pseudo = pt.Player.Pseudo,
                Email = pt.Player.Email,
                BirthDate = pt.Player.BirthDate,
                ELO = pt.Player.ELO
            }).ToList()
            };
        }
    }
}
