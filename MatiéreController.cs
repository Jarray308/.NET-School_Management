using Microsoft.AspNetCore.Mvc;
using ProjetEtudiant.Models.Repositories;
using ProjetEtudiant.Models;

namespace ProjetEtudiant.Controllers
{
    public class MatiéreController : Controller
    {
        public IRepository<Matiére> repos { get; }

        public MatiéreController(IRepository<Matiére> repos)
        {
            this.repos = repos;
        }
        [HttpGet]
        public IActionResult Index()
        {
            var matiére = repos.Lister();
            return View(matiére); View();
        }
        [HttpGet]
        public IActionResult Details(int id)
        {
            var matiére = repos.ListerSelonId(id);
            return View(matiére);
        }
        public IActionResult Create()
        {
            return View();
        }

        // POST: Etudiants/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public IActionResult Create(Matiére matiére)
        {
            try
            {

                repos.Ajouter(matiére);
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
            var matiére = repos.ListerSelonId(id);
            return View(matiére);
        }

        // POST: Etudiants/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Matiére matiére)
        {
            try
            {
                repos.Update(id, matiére);
                return RedirectToAction(nameof(Index));

            }
            catch (Exception)
            {
                throw;
            }


        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var matiére = repos.ListerSelonId(id);
            return View(matiére);
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
