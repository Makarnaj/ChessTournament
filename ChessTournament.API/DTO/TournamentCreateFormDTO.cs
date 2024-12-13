using System.ComponentModel.DataAnnotations;

namespace ChessTournament.API.DTO
{
    public class TournamentCreateFormDTO
    {
        [Required]
        public string Name { get; set; }

        public string Location { get; set; }

        [Required]
        [Range(2, 32, ErrorMessage = "Le nombre minimum de joueurs doit être entre 2 et 32.")]
        public int MinPlayers { get; set; }

        [Required]
        [Range(2, 32, ErrorMessage = "Le nombre maximum de joueurs doit être entre 2 et 32.")]
        public int MaxPlayers { get; set; }

        [Range(0, 3000, ErrorMessage = "L'ELO minimum doit être entre 0 et 3000.")]
        public int? MinELO { get; set; }

        [Range(0, 3000, ErrorMessage = "L'ELO maximum doit être entre 0 et 3000.")]
        public int? MaxELO { get; set; }

        [Required]
        public ICollection<int>? CategoryIds { get; set; }

        [Required]
        public bool WomenOnly { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime RegistrationEndDate { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime CreationDate { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime UpdateDate { get; set; }
    }

}
