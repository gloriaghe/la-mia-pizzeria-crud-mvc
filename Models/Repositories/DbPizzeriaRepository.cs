using Azure;
using la_mia_pizzeria_static.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Microsoft.SqlServer.Server;

namespace la_mia_pizzeria_static.Models.Repositories
{
    public class DbPizzeriaRepository : IPizzeriaRepository
    {
        private PizzaDbContext db;
        public DbPizzeriaRepository()
        {
            db = new PizzaDbContext();
        }
        public List<Pizza> All()
        {
            return db.Pizzas.Include(pizza => pizza.Category).Include("Ingredients").ToList();
        }
        public List<Category> AllCat()
        {
            return db.Categories.Include("Pizzas").ToList();
        }
        public List<Ingredient> AllIng()
        {
            return db.Ingredients.Include("Pizzas").ToList();
        }
        public Pizza getID(int id)
        {
            return db.Pizzas.Where(pizza => pizza.Id == id).Include(pizza => pizza.Category).Include("Ingredients").FirstOrDefault();
        }
        public Category GetByIdCat(int id)
        {
            return db.Categories.Where(c => c.Id == id).Include("Pizzas").FirstOrDefault();
        }
        public Ingredient GetByIdIng(int id)
        {
            return db.Ingredients.Where(i => i.Id == id).Include("Pizzas").FirstOrDefault();
        }
        public void Create(Pizza pizza, List<int> selectedIngredients)
        {
            //associazione dell'ingrediente selezionato dall'user al model
            pizza.Ingredients = new List<Ingredient>();

            if (selectedIngredients != null)
            {

                foreach (int ingID in selectedIngredients)
                {
                    Ingredient ingredient = db.Ingredients.Where(i => i.Id == ingID).FirstOrDefault();
                    pizza.Ingredients.Add(ingredient);
                }
            }

            db.Pizzas.Add(pizza);
            db.SaveChanges();
        }
        public void CreateCat(Category category)
        {
            db.Categories.Add(category);
            db.SaveChanges();
        }

        public void CreateIng(Ingredient ingredient)
        {
            db.Ingredients.Add(ingredient);
            db.SaveChanges();
        }
        public int GetByNameCategory(Category category)
        {
            return db.Categories.Where(c => c.Name == category.Name).Count();
        }
        public int GetByNameIngredient(Ingredient ingredient)
        {
            return db.Ingredients.Where(i => i.Name == ingredient.Name).Count();
        }
        public void Update(Pizza pizza, Pizza formData, List<int>? SelectedIngredients, Category category)
        {
            pizza.Name = formData.Name;
            pizza.Description = formData.Description;
            pizza.Image = formData.Image;
            pizza.Price = formData.Price;
            pizza.CategoryId = formData.CategoryId;

            pizza.Ingredients.Clear();

            if (SelectedIngredients == null)
            {
                SelectedIngredients = new List<int>();
            }

            foreach (int ingId in SelectedIngredients)
            {
                Ingredient ing = db.Ingredients.Where(i => i.Id == ingId).FirstOrDefault();
                pizza.Ingredients.Add(ing);
            }
            db.SaveChanges();
        }

        public void Delete(Pizza pizza)
        {
            db.Pizzas.Remove(pizza);
            db.SaveChanges();
        }
        public void UpdateCat(Category category)
        {
            db.Categories.Update(category);
            db.SaveChanges();
        }
        public void Deletecat(Category category)
        {
            db.Categories.Remove(category);
            db.SaveChanges();
        }

        public void UpdateIng(Ingredient ingredient)
        {
            db.Ingredients.Update(ingredient);
            db.SaveChanges();
        }
        public void DeleteIng(Ingredient ingredient)
        {
            db.Ingredients.Remove(ingredient);
            db.SaveChanges();
        }
    }

}
