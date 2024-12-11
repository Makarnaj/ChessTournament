using ChessTournament.Domain.Enums;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace ChessTournament.API.DTO
{
    public class PlayerCreateFormDTO
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Pseudo { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        [Compare("Password")]
        public string PasswordCheck { get; set; }
        [Required]
        public DateTime BirthDate { get; set; }
        [Required]
        public Gender Gender { get; set; }

        [Required]
        public int ELO { get; set; }
        

     
    }
}
