using la_mia_pizzeria_static.Data;
using la_mia_pizzeria_static.Models;
using la_mia_pizzeria_static.Models.Form;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

namespace la_mia_pizzeria_static.Controllers
{
    public class PizzaController : Controller
    {
        PizzaDbContext db;

        public PizzaController() : base()
        {
            db = new PizzaDbContext();
        }

        public IActionResult Index()
        {
            List<Pizza> listaPizze = db.Pizzas.Include(pizza => pizza.Category).ToList();
            return View(listaPizze);
        }

        public IActionResult Detail(int id)
        {
            Pizza pizza = db.Pizzas.Where(pizza => pizza.Id == id).Include(pizza => pizza.Category).FirstOrDefault();
            if (pizza == null)
                return View("Errore", "Pizza non trovata");

            return View(pizza);

        }

        public IActionResult Create()
        {
            PizzaForm formData = new PizzaForm();
            formData.Pizza = new Pizza();
            formData.Categories = db.Categories.ToList();

            return View(formData);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(PizzaForm formData)
        {
            if (!ModelState.IsValid)
            {
                formData.Categories = db.Categories.ToList();
                return View(formData);
            }

            db.Pizzas.Add(formData.Pizza);
            db.SaveChanges();

            return RedirectToAction("Detail", new { id = formData.Pizza.Id });
        }

        public IActionResult Update(int id)
        {
            Pizza pizza = db.Pizzas.Where(pizza => pizza.Id == id).FirstOrDefault();

            if (pizza == null)
                return NotFound();

            PizzaForm formData = new PizzaForm();
            formData.Pizza = pizza;
            formData.Categories = db.Categories.ToList();

            //dobbiamo passare anche il post alla view
            return View(formData);
        }

        //primo modo passando solo il modello
        //[HttpPost]
        //public IActionResult Update(Pizza pizza)
        //{

        //    if (!ModelState.IsValid)
        //    {
        //        return View(pizza);
        //    }

        //    db.Pizzas.Update(pizza);
        //    db.SaveChanges();

        //    return RedirectToAction("Index");
        //}

        //secondo modo in cui passiamo sia l'id che il modello e modifichiamo dato per dato
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(int id, PizzaForm formData)
        {
            if (!ModelState.IsValid)
            {
                formData.Categories = db.Categories.ToList();

                return View(formData);
            }

            //Update esplicito
            //Pizza pizza = db.Pizzas.Where(pizza => pizza.Id == id).FirstOrDefault();


            //if (pizza == null)
            //{
            //    return NotFound();
            //}

            //pizza.Name = formData.Pizza.Name;
            //pizza.Description = formData.Pizza.Description;
            //pizza.Image = formData.Pizza.Image;
            //pizza.Price = formData.Pizza.Price;

            //Update implicito
            formData.Pizza.Id = id;
            db.Pizzas.Update(formData.Pizza);

            //salviamo
            db.SaveChanges();

            return RedirectToAction("Detail", new { id = formData.Pizza.Id });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            Pizza pizza = db.Pizzas.Where(pizza => pizza.Id == id).FirstOrDefault();

            if (pizza == null)
            {
                return NotFound();
            }

            db.Pizzas.Remove(pizza);
            db.SaveChanges();


            return RedirectToAction("Index");
        }
    }
}