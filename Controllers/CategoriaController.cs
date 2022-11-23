using la_mia_pizzeria_static.Data;
using la_mia_pizzeria_static.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Microsoft.Extensions.Hosting;

namespace la_mia_pizzeria_static.Controllers
{
    public class CategoriaController : Controller
    {
        PizzaDbContext db;

        public CategoriaController() : base()
        {
            db = new PizzaDbContext();
        }
        public IActionResult Index()
        {
            List<Category> listCategories = db.Categories.ToList();
            return View(listCategories);
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category categoria)
        {
            if (!ModelState.IsValid)
            {

                return View();
            }
            db.Categories.Add(categoria);
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult Update(int id)
        {
            Category categoria = db.Categories.Where(c => c.Id == id).FirstOrDefault();

            if (categoria == null)
                return NotFound();

            return View(categoria);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Update(Category categoria)
        {

            if (!ModelState.IsValid)
            {
                return View(categoria);
            }

            db.Categories.Update(categoria);
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            Category post = db.Categories.Where(c => c.Id == id).FirstOrDefault();

            if (post == null)
            {
                return NotFound();
            }

            db.Categories.Remove(post);
            db.SaveChanges();


            return RedirectToAction("Index");
        }
    }
}
