namespace ChessTournament.Domain.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int AgeMin { get; set; }
        public int AgeMax { get; set; }
        public ICollection<Tournament> Tournaments { get; set; }


    }
}