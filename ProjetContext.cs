using Microsoft.EntityFrameworkCore;
using ProjetEtudiant.Models;

namespace ProjetEtudiant.Services
{
    public class ProjetContext : DbContext
    {
        public DbSet<Etudiant> Etudiants { get; set; }
        public DbSet<Matiére> Matiéres { get; set; }
        public DbSet<Note> Notes { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optBuilder)
        {
            optBuilder.UseSqlServer("Server=.\\SQLEXPRESS;database=Gestion_projet;Trusted_Connection =True");
        }
       
    }
}
