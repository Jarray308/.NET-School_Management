using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjetEtudiant.Models
{
    public class Note
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("Etudiant")]
        public int IdEtudiant { get; set; }
        public Etudiant Etudiant { get; set; }

        [ForeignKey("Matiére")]
        public int IdMatiére { get; set; }
        public Matiére Matiére { get; set; }
        public double note { get; set; }


    }
}
