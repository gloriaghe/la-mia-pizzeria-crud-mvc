using Azure;
using la_mia_pizzeria_static.Data;
using la_mia_pizzeria_static.Models;
using la_mia_pizzeria_static.Models.Form;
using la_mia_pizzeria_static.Models.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

namespace la_mia_pizzeria_static.Controllers
{
    public class PizzaController : Controller
    {
        //PizzaDbContext db;
        //DbPizzeriaRepository pizzeria;

        IPizzeriaRepository pizzeria;


        public PizzaController(IPizzeriaRepository _pizzeria) : base()
        {
            //db = new PizzaDbContext();
            //pizzeria = new DbPizzeriaRepository();
            pizzeria = _pizzeria;
        }

        public IActionResult Index()
        {
            List<Pizza> listaPizze = pizzeria.All();
            return View(listaPizze);
        }

        public IActionResult Detail(int id)
        {
            Pizza pizza = pizzeria.getID(id);
            if (pizza == null)
                return View("Errore", "Pizza non trovata");

            return View(pizza);

        }

        public IActionResult Create()
        {
            PizzaForm formData = new PizzaForm();
            formData.Pizza = new Pizza();
            formData.Categories = pizzeria.AllCat();
            formData.Ingredients = new List<SelectListItem>();

            List<Ingredient> ingredientList = pizzeria.AllIng();

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
                formData.Categories = pizzeria.AllCat(); ;
                formData.Ingredients = new List<SelectListItem>();

                List<Ingredient> ingredientList = pizzeria.AllIng();

                foreach (Ingredient ing in ingredientList)
                {
                    formData.Ingredients.Add(new SelectListItem(ing.Name, ing.Id.ToString()));
                }

                return View(formData);
            }

            pizzeria.Create(formData.Pizza, formData.SelectedIngredients);


            return RedirectToAction("Detail", new { id = formData.Pizza.Id });
        }

        public IActionResult Update(int id)
        {
            Pizza pizza = pizzeria.getID(id);

            if (pizza == null)
                return NotFound();

            PizzaForm formData = new PizzaForm();
            formData.Pizza = pizza;
            formData.Categories = pizzeria.AllCat();
            formData.Ingredients = new List<SelectListItem>();

            List<Ingredient> ingredientsList = pizzeria.AllIng();

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

                formData.Categories = pizzeria.AllCat();
                formData.Ingredients = new List<SelectListItem>();

                List<Ingredient> ingredientsList = pizzeria.AllIng();

                foreach (Ingredient ing in ingredientsList)
                {
                    formData.Ingredients.Add(new SelectListItem(ing.Name, ing.Id.ToString()));
                }
                return View(formData);
            }

            //Update esplicito
            Pizza pizza = pizzeria.getID(id);

            if (pizza == null)
            {
                return NotFound();
            }


            ////Update implicito
            //formData.Pizza.Id = id;
            //db.Pizzas.Update(formData.Pizza);


            pizzeria.Update(pizza, formData.Pizza, formData.SelectedIngredients, formData.Pizza.Category);
            return RedirectToAction("Detail", new { id = pizza.Id });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            Pizza pizza = pizzeria.getID(id);

            if (pizza == null)
            {
                return NotFound();
            }
            pizzeria.Delete(pizza);
            //db.Pizzas.Remove(pizza);
            //db.SaveChanges();


            return RedirectToAction("Index");
        }
    }
}