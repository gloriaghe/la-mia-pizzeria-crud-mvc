using Azure;
using Microsoft.Extensions.Hosting;
using Microsoft.SqlServer.Server;

namespace la_mia_pizzeria_static.Models.Repositories
{
    public class InMemoryPizzaRepository : IPizzeriaRepository
    {
        public static List<Pizza> Pizzas = new List<Pizza>();
        public static List<Category> Categories = new List<Category>();
        public static List<Ingredient> Ingredients = new List<Ingredient>();
        private static int counter = 1;


        public List<Pizza> All()
        {
            return Pizzas;
        }


        public void Create(Pizza pizza, List<int> selectedIngredients)
        {
            //simuliamo la primary key
            pizza.Id = counter;
            counter++;

            Category categoryChose = Categories.Where(c => c.Id == pizza.CategoryId).FirstOrDefault();
            pizza.Category = categoryChose;
            pizza.Ingredients = null;
            List<Ingredient> ingredients = new List<Ingredient>();

            if (selectedIngredients != null)
            {
                foreach (int ingID in selectedIngredients)
                {
                    Ingredient ingredient = Ingredients.Where(i => i.Id == ingID).FirstOrDefault();

                    ingredient.Pizzas.Add(pizza);

                    ingredients.Add(ingredient);
                }
            }

            pizza.Ingredients = ingredients;

            Pizzas.Add(pizza);
        }
        public void Delete(Pizza pizza)
        {
            Pizzas.Remove(pizza);
        }

        public Pizza getID(int id)
        {
            Pizza pizza = Pizzas.Where(pizza => pizza.Id == id)
                                //.Include("Categories")
                                //.Include("Ingredients")
                                .FirstOrDefault();


            return pizza;
        }

        public void Update(Pizza pizza, Pizza formData, List<int>? SelectedIngredients, Category category)
        {
            //pizza = formData;
            pizza.Name = formData.Name;
            pizza.Description = formData.Description;
            pizza.Price = formData.Price;
            pizza.Image = formData.Image;
            Category categoryChose = Categories.Where(c => c.Id == formData.CategoryId).FirstOrDefault();
            pizza.Category = categoryChose;
            pizza.CategoryId = formData.CategoryId;


            pizza.Ingredients = new List<Ingredient>();


            IngredientToPizza(pizza, SelectedIngredients);
        }

        private static void IngredientToPizza(Pizza pizza, List<int> selectedIngredient)
        {

            List<Ingredient> ingredients = new List<Ingredient>();

            if (selectedIngredient != null)
            {
                foreach (int ingID in selectedIngredient)
                {
                    Ingredient ingredient = Ingredients.Where(i => i.Id == ingID).FirstOrDefault();

                    ingredient.Pizzas.Add(pizza);

                    ingredients.Add(ingredient);
                }
            }

            pizza.Ingredients = ingredients;
        }

        public List<Category> AllCat()
        {
            return Categories;
        }


        public List<Ingredient> AllIng()
        {
            return Ingredients;
        }

        public Category GetByIdCat(int id)
        {
            Category category = Categories.Where(category => category.Id == id)
                                //.Include("Pizzas")
                                .FirstOrDefault();
            return category;
        }

        public Ingredient GetByIdIng(int id)
        {

            Ingredient ingredient = Ingredients.Where(ingredient => ingredient.Id == id)
                                //.Include("Pizzas")
                                .FirstOrDefault();
            return ingredient;
        }

        public void CreateCat(Category category)
        {
            category.Id = counter;
            counter++;
            category.Pizzas = new List<Pizza>();
            Categories.Add(category);
        }

        public void CreateIng(Ingredient ingredient)
        {
            ingredient.Id = counter;
            counter++;
            ingredient.Pizzas = new List<Pizza>();

            Ingredients.Add(ingredient);
        }

        public int GetByNameCategory(Category category)
        {
            Category categor = Categories.Where(c => c.Name == category.Name)
                                .FirstOrDefault();
            if (categor != null)
                return 1;
            else
                return 0;
        }

        public int GetByNameIngredient(Ingredient ingredient)
        {
            Ingredient ingrediente = Ingredients.Where(i => i.Name == ingredient.Name)
                                            .FirstOrDefault();
            if (ingrediente != null)
                return 1;
            else
                return 0;
        }

        public void UpdateCat(Category category)
        {
            Category cat = GetByIdCat(category.Id);
            cat.Name = category.Name;
            cat.Description = category.Description;
        }

        public void UpdateIng(Ingredient ingredient)
        {
            Ingredient ing = GetByIdIng(ingredient.Id);
            ing.Name = ingredient.Name;
        }
        public void Deletecat(Category category)
        {
            Categories.Remove(category);
        }


        public void DeleteIng(Ingredient ingredient)
        {
            Ingredients.Remove(ingredient);
        }
    }
}
