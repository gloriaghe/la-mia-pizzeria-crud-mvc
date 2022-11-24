using Azure;
using la_mia_pizzeria_static.Data;
using la_mia_pizzeria_static.Models;
using la_mia_pizzeria_static.Models.Form;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
            List<Pizza> listaPizze = db.Pizzas.Include(pizza => pizza.Category).Include("Ingredients").ToList();
            return View(listaPizze);
        }

        public IActionResult Detail(int id)
        {
            Pizza pizza = db.Pizzas.Where(pizza => pizza.Id == id).Include(pizza => pizza.Category).Include("Ingredients").FirstOrDefault();
            if (pizza == null)
                return View("Errore", "Pizza non trovata");

            return View(pizza);

        }

        public IActionResult Create()
        {
            PizzaForm formData = new PizzaForm();
            formData.Pizza = new Pizza();
            formData.Categories = db.Categories.ToList();
            formData.Ingredients = new List<SelectListItem>();

            List<Ingredient> ingredientList = db.Ingredients.ToList();

            foreach (Ingredient ing in ingredientList)
            {
                formData.Ingredients.Add(new SelectListItem(ing.Name, ing.Id.ToString()));
            }

            return View(formData);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(PizzaForm formData)
        {
            if (!ModelState.IsValid)
            {
                formData.Categories = db.Categories.ToList();
                formData.Ingredients = new List<SelectListItem>();

                List<Ingredient> tagList = db.Ingredients.ToList();

                foreach (Ingredient ing in tagList)
                {
                    formData.Ingredients.Add(new SelectListItem(ing.Name, ing.Id.ToString()));
                }

                return View(formData);
            }

            //associazione dell'ingrediente selezionato dall'user al model
            formData.Pizza.Ingredients = new List<Ingredient>();

            if (formData.SelectedIngredients != null)
            {

                foreach (int ingID in formData.SelectedIngredients)
                {
                    Ingredient ingredient = db.Ingredients.Where(i => i.Id == ingID).FirstOrDefault();
                    formData.Pizza.Ingredients.Add(ingredient);
                }
            }

            db.Pizzas.Add(formData.Pizza);
            db.SaveChanges();

            return RedirectToAction("Detail", new { id = formData.Pizza.Id });
        }

        public IActionResult Update(int id)
        {
            Pizza pizza = db.Pizzas.Where(pizza => pizza.Id == id).Include("Ingredients").FirstOrDefault();

            if (pizza == null)
                return NotFound();

            PizzaForm formData = new PizzaForm();
            formData.Pizza = pizza;
            formData.Categories = db.Categories.ToList();
            formData.Ingredients = new List<SelectListItem>();

            List<Ingredient> ingredientsList = db.Ingredients.ToList();

            foreach (Ingredient ing in ingredientsList)
            {
                formData.Ingredients.Add(new SelectListItem(
                    ing.Name,
                    ing.Id.ToString(),
                    pizza.Ingredients.Any(i => i.Id == ing.Id)
                   ));
            }
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
                formData.Pizza.Id = id;

                formData.Categories = db.Categories.ToList();
                formData.Ingredients = new List<SelectListItem>();

                List<Ingredient> ingredientsList = db.Ingredients.ToList();

                foreach (Ingredient ing in ingredientsList)
                {
                    formData.Ingredients.Add(new SelectListItem(ing.Name, ing.Id.ToString()));
                }
                return View(formData);
            }

            //Update esplicito
            Pizza pizza = db.Pizzas.Where(pizza => pizza.Id == id).Include("Ingredients").FirstOrDefault();


            if (pizza == null)
            {
                return NotFound();
            }

            pizza.Name = formData.Pizza.Name;
            pizza.Description = formData.Pizza.Description;
            pizza.Image = formData.Pizza.Image;
            pizza.Price = formData.Pizza.Price;
            pizza.CategoryId = formData.Pizza.CategoryId;

            pizza.Ingredients.Clear();

            if (formData.SelectedIngredients == null)
            {
                formData.SelectedIngredients = new List<int>();
            }

            foreach (int ingId in formData.SelectedIngredients)
            {
                Ingredient ing = db.Ingredients.Where(i => i.Id == ingId).FirstOrDefault();
                pizza.Ingredients.Add(ing);
            }

            ////Update implicito
            //formData.Pizza.Id = id;
            //db.Pizzas.Update(formData.Pizza);

            //salviamo
            db.SaveChanges();

            return RedirectToAction("Detail", new { id = pizza.Id });
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