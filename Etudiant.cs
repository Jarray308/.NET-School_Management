using System.ComponentModel.DataAnnotations;

namespace ProjetEtudiant.Models
{
    public class Etudiant
    {
        [Key]
        public int Id { get; set; }
        public String Nom { get; set; }
        public String Prenom { get; set; }
        public ICollection<Note> notes { get; set; }
 
    }
}
