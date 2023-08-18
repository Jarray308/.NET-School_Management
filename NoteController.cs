using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProjetEtudiant.Models.Repositories;
using ProjetEtudiant.Models;
using ProjetEtudiant.Services;
using SelectPdf;
using Microsoft.EntityFrameworkCore;
using iTextSharp.text;
using System.Xml.Linq;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using iTextSharp.text.xml.xmp;

namespace ProjetEtudiant.Controllers
{
    public class NoteController : Controller
    {
        private ProjetContext etuds;
        public IRepository<Note> repos { get; }

        public NoteController(IRepository<Note> repos, ProjetContext etuds)
        {
            this.repos = repos;
            this.etuds = etuds;
        }
        // GET: 
        public async Task<IActionResult> Index(string searching)
        {
            var note0 = etuds.Notes
                .Include(c => c.Etudiant)
                .Include(i => i.Matiére)
                .AsNoTracking();
            ViewData["GetNotDetails"] = searching;

            var n = from p in etuds.Notes
                    select p;
            if (!string.IsNullOrWhiteSpace(searching))
            {
                note0 = note0.Where(p => p.IdEtudiant.ToString().Contains(searching));
            }
            return View(await note0.AsNoTracking().ToListAsync());
        }
        [HttpGet]

        // GET: NoteController/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var note = await etuds.Notes
                .Include(c => c.Etudiant)
                .Include(i => i.Matiére)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.Id == id);
            if (note == null)
            {
                return NotFound();
            }

            return View(note);
        }
        public FileResult GeneratePdf(string html)
        {
            html = html.Replace("strTag", "<").Replace("EndTag", ">");
            HtmlToPdf objhtml = new HtmlToPdf();
            PdfDocument objdoc = objhtml.ConvertHtmlString(html);
            byte[] pdf = objdoc.Save();
            objdoc.Close();
            return File(pdf, "application/pdf", "MyPdf.pdf");

        }
        private void PopulateEtudiantsDropDownList(object selectedEtudiant = null)
        {
            var etudiantsQuery = from d in etuds.Etudiants
                                 orderby d.Nom
                                 select d;
            ViewBag.IdEtudiant = new SelectList(etudiantsQuery.AsNoTracking(), "Id", "Nom", selectedEtudiant);
        }
        private void PopulateMatiéresDropDownList(object selectedCourse = null)
        {
            var coursesQuery = from d in etuds.Matiéres
                               orderby d.description
                               select d;
            ViewBag.IdMatiére = new SelectList(coursesQuery.AsNoTracking(), "Id", "description", selectedCourse);
        }
        // GET: NoteController/Create
        public ActionResult Create()
        {
            PopulateEtudiantsDropDownList();
            PopulateMatiéresDropDownList();

            return View();
        }

        // POST: NoteController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Note element)
        {
            try
            {
                repos.Ajouter(element);

                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                throw;
            }
            PopulateEtudiantsDropDownList(element.IdEtudiant);
            PopulateMatiéresDropDownList(element.IdMatiére);



        }
        

        // GET: NoteController/Edit/5
        public ActionResult Edit(int id)
        {
            var note = repos.ListerSelonId(id);
            PopulateEtudiantsDropDownList(note.IdEtudiant);
            PopulateMatiéresDropDownList(note.IdMatiére);
            return View(note);
        }

        // POST: NoteController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Note notee)
        {
            try
            {
                repos.Update(id, notee);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                throw;
            }
            PopulateEtudiantsDropDownList(notee.IdEtudiant);
            PopulateMatiéresDropDownList(notee.IdMatiére);
        }

        // GET: NoteController/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course = await etuds.Notes
                .Include(c => c.Etudiant)
                .Include(i => i.Matiére)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.Id == id);
            if (course == null)
            {
                return NotFound();
            }

            return View(course);
        }



        // POST: NoteController/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var student = await etuds.Notes.FindAsync(id);
            if (student == null)
            {
                return RedirectToAction(nameof(Index));
            }

            try
            {
                etuds.Notes.Remove(student);
                await etuds.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException /* ex */)
            {
                //Log the error (uncomment ex variable name and write a log.)
                return RedirectToAction(nameof(Delete), new { id = id, saveChangesError = true });
            }
        }

    }

    }
