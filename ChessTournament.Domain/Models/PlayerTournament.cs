using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessTournament.Domain.Models
{
    public class PlayerTournament
    {
        public int PlayerId { get; set; }

        public Player Player { get; set; }  

        public int TournamentId { get; set; }

        public Tournament Tournament { get; set; }
    }
}
