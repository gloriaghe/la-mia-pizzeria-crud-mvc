using Azure;
using Microsoft.Extensions.Hosting;

namespace la_mia_pizzeria_static.Models.Repositories
{
    public class InMemoryPizzaRepository : IPizzeriaRepository
    {
        public static List<Pizza> Pizzas = new List<Pizza>();
        public static List<Category> Categories = new List<Category>();
        public static List<Ingredient> Ingredients = new List<Ingredient>();


        public List<Pizza> All()
        {
            return Pizzas;
        }


        public void Create(Pizza pizza, List<int> selectedIngredients)
        {
            //simuliamo la primary key
            pizza.Id = Pizzas.Count;

            if (selectedIngredients != null)
            {

                foreach (int ingID in selectedIngredients)
                {
                    Ingredient ingredient = Ingredients.Where(i => i.Id == ingID).FirstOrDefault();
                    pizza.Ingredients.Add(ingredient);
                }
            }



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
            pizza = formData;
            pizza.Category = category;

            pizza.Ingredients = new List<Ingredient>();


            IngredientToPizza(pizza, SelectedIngredients);
            //fine simulazione
        }
        private static void IngredientToPizza(Pizza pizza, List<int> selectedIngredient)
        {
            pizza.Category = new Category() { Id = 1, Name = "Fake cateogry" };

            foreach (int tagId in selectedIngredient)
            {
                pizza.Ingredients.Add(new Ingredient() { Id = tagId, Name = "Fake ingredient " + tagId });
            }
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
            Categories.Add(category);
        }

        public void CreateIng(Ingredient ingredient)
        {
            Ingredients.Add(ingredient);
        }

        public int CountCat(Category category)
        {
            return 0;
;        }

        public int CountIng(Ingredient ingredient)
        {
            return 0;
        }

        public void UpdateCat(Category category)
        {
            //category = formdata;
        }

        public void Deletecat(Category category)
        {
            Categories.Remove(category);
        }

        public void UpdateIng(Ingredient ingredient)
        {
            throw new NotImplementedException();
        }

        public void DeleteIng(Ingredient ingredient)
        {
            Ingredients.Remove(ingredient);
        }
    }
}
