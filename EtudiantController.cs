using Microsoft.AspNetCore.Mvc;
using ProjetEtudiant.Models.Repositories;
using ProjetEtudiant.Models;

namespace ProjetEtudiant.Controllers
{
    public class EtudiantController : Controller
    {
        public IRepository<Etudiant> repos { get; }

        public EtudiantController(IRepository<Etudiant> repos)
        {
            this.repos = repos;
        }

        // GET: Etudiants
        [HttpGet]
        public IActionResult Index()
        {
            var etudiant = repos.Lister();
            return View(etudiant);
        }

        // GET: Etudiants/Details/5
        [HttpGet]
        public IActionResult Details(int id)
        {
            var etudiant = repos.ListerSelonId(id);
            return View(etudiant);
        }

        // GET: Etudiants/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Etudiants/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public IActionResult Create(Etudiant etudiant)
        {
            try
            {

                repos.Ajouter(etudiant);
                return RedirectToAction("Index");

            }
            catch (Exception /* dex */)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.
                throw;
            }
        }

        // GET: Etudiants/Edit/5
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var etudiant = repos.ListerSelonId(id);
            return View(etudiant);
        }

        // POST: Etudiants/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Etudiant etudiant)
        {
            try
            {
                repos.Update(id, etudiant);
                return RedirectToAction(nameof(Index));

            }
            catch (Exception)
            {
                throw;
            }


        }


        // GET: Etudiants/Delete/5
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var etudiant = repos.ListerSelonId(id);
            return View(etudiant);
        }

        // POST: Etudiants/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            try
            {
                repos.Delete(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                throw;
            }
        }


    
    }
}
