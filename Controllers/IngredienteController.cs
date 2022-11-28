using la_mia_pizzeria_static.Data;
using la_mia_pizzeria_static.Models;
using la_mia_pizzeria_static.Models.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace la_mia_pizzeria_static.Controllers
{
    public class IngredienteController : Controller
    {
        //PizzaDbContext db;
        //DbPizzeriaRepository pizzeria;
        IPizzeriaRepository pizzeria;
        public IngredienteController(IPizzeriaRepository _pizzeria) : base()
        {
            //db = new PizzaDbContext();
            //pizzeria = new DbPizzeriaRepository();
            pizzeria= _pizzeria;
        }

        public IActionResult Index()
        {
            List<Ingredient> listIngredient = pizzeria.AllIng();
            return View(listIngredient);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Ingredient ingredient)
        {
            if (pizzeria.GetByNameIngredient(ingredient) > 0)
            {
                return View("Errore", "L'ingrediente esiste già");

            }

            if (!ModelState.IsValid)
            {

                return View();
            }
            pizzeria.CreateIng(ingredient);

            return RedirectToAction("Index");
        }

        public IActionResult Update(int id)
        {
            Ingredient ingredient = pizzeria.GetByIdIng(id);

            if (ingredient == null)
                return NotFound();

            return View(ingredient);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Update(Ingredient ingredient)
        {

            if (!ModelState.IsValid)
            {
                return View(ingredient);
            }

           pizzeria.UpdateIng(ingredient);
           

            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            Ingredient ingredient = pizzeria.GetByIdIng(id);

            if (ingredient == null)
            {
                return NotFound();
            }

            //if (ingredient.Pizzas.Count > 0)
            //    return View("Errore", "L'ingrediente non può essere eliminata in quanto ha delle pizze già assegnate ad essa");
            pizzeria.DeleteIng(ingredient);

            return RedirectToAction("Index");
        }
    }
}
