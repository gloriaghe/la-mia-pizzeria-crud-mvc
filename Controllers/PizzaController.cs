using la_mia_pizzeria_static.Data;
using la_mia_pizzeria_static.Models;
using Microsoft.AspNetCore.Mvc;
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
            List<Pizza> listaPizze = db.Pizzas.ToList();
            return View(listaPizze);
        }

        public IActionResult Detail(int id)
        {
            Pizza pizza = db.Pizzas.Where(pizza => pizza.Id == id).FirstOrDefault();
            if (pizza == null)
                return View("Errore", "Pizza non trovata");

            return View(pizza);

        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Pizza pizza)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            db.Pizzas.Add(pizza);
            db.SaveChanges();

            return RedirectToAction("Index");
        }
        public IActionResult Update(int id)
        {
            Pizza post = db.Pizzas.Where(post => post.Id == id).FirstOrDefault();

            if (post == null)
                return NotFound();

            //dobbiamo passare anche il post alla view
            return View(post);
        }
        [HttpPost]
        public IActionResult Update(Pizza post)
        {

            if (!ModelState.IsValid)
            {
                //return View(post);
                return View();
            }

            db.Pizzas.Update(post);
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        //altro modo
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public IActionResult Update(int id, Pizza formData)
        //{

        //    if (!ModelState.IsValid)
        //    {
        //        //return View(post);
        //        return View();
        //    }

        //    Pizza pizza = db.Pizzas.Where(pizza => pizza.Id == id).FirstOrDefault();

        //    if (pizza == null)
        //    {
        //        return NotFound();
        //    }

        //    pizza.Name = formData.Name;
        //    pizza.Description = formData.Description;
        //    pizza.Image = formData.Image;
        //    pizza.Price = formData.Price;

        //    db.SaveChanges();

        //    return RedirectToAction("Index");
        //}

    }
}