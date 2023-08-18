using System.ComponentModel.DataAnnotations;

namespace ProjetEtudiant.Models
{
    public class Matiére
    {
        [Key]
        public int Id { get; set; }
        public int Coef { get; set; }
        public String description { get; set; }
        public ICollection<Note> notes { get; set; }

    }
}
