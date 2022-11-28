using la_mia_pizzeria_static.Data;
using la_mia_pizzeria_static.Models;
using la_mia_pizzeria_static.Models.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

namespace la_mia_pizzeria_static.Controllers
{
    public class CategoriaController : Controller
    {
        //PizzaDbContext db;
        //DbPizzeriaRepository pizzeria;
        IPizzeriaRepository pizzeria;
        public CategoriaController(IPizzeriaRepository _pizzeria) : base()
        {
            //db = new PizzaDbContext();
            //pizzeria = new DbPizzeriaRepository();
            pizzeria = _pizzeria;

        }

        public IActionResult Index()
        {
            List<Category> listCategories = pizzeria.AllCat();
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
            if (pizzeria.GetByNameCategory(categoria) > 0)
            {
                return View("Errore", "La categoria esiste già");

            }

            if (!ModelState.IsValid)
            {

                return View();
            }
            pizzeria.CreateCat(categoria);

            return RedirectToAction("Index");
        }

        public IActionResult Update(int id)
        {
            Category categoria = pizzeria.GetByIdCat(id);

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
            pizzeria.UpdateCat( categoria);

            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            Category category = pizzeria.GetByIdCat(id);

            if (category == null)
            {
                return NotFound();
            }

            if (category.Pizzas.Count > 0)
                return View("Errore", "La categoria non può essere eliminata in quanto ha delle pizze già assegnate ad essa");
          
                pizzeria.Deletecat(category);
                return RedirectToAction("Index");
        }
    }
}
