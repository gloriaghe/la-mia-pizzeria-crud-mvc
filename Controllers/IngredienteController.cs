using la_mia_pizzeria_static.Data;
using la_mia_pizzeria_static.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace la_mia_pizzeria_static.Controllers
{
    public class IngredienteController : Controller
    {
        PizzaDbContext db;

        public IngredienteController() : base()
        {
            db = new PizzaDbContext();
        }

        public IActionResult Index()
        {
            List<Ingredient> listIngredient = db.Ingredients.Include("Pizzas").ToList();
            return View(listIngredient);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Ingredient ingrediente)
        {
            if (db.Ingredients.Where(i => i.Name == ingrediente.Name).Count() > 0)
            {
                return View("Errore", "L'ingrediente esiste già");

            }

            if (!ModelState.IsValid)
            {

                return View();
            }
            db.Ingredients.Add(ingrediente);
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult Update(int id)
        {
            Ingredient ingrediente = db.Ingredients.Where(i => i.Id == id).FirstOrDefault();

            if (ingrediente == null)
                return NotFound();

            return View(ingrediente);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Update(Ingredient ingrediente)
        {

            if (!ModelState.IsValid)
            {
                return View(ingrediente);
            }

            db.Ingredients.Update(ingrediente);
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            Ingredient ingredient = db.Ingredients.Where(i => i.Id == id).Include("Pizzas").FirstOrDefault();

            if (ingredient == null)
            {
                return NotFound();
            }

            //if (ingredient.Pizzas.Count > 0)
            //    return View("Errore", "L'ingrediente non può essere eliminata in quanto ha delle pizze già assegnate ad essa");

            db.Ingredients.Remove(ingredient);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
