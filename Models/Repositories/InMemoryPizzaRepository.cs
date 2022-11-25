using Azure;
using Microsoft.Extensions.Hosting;

namespace la_mia_pizzeria_static.Models.Repositories
{
    public class InMemoryPizzaRepository : IDbPizzeriaRepository
    {
        public static List<Pizza> Pizzas = new List<Pizza>();

        public List<Pizza> All()
        {
            return Pizzas;
        }

        public void Create(Pizza pizza, List<int> selectedIngredients)
        {
            //simuliamo la primary key
            pizza.Id = Pizzas.Count;
            pizza.Category = new Category() { Id = 1, Name = "Fake cateogry" };

            //simulazione da implentare con ListTagRepository
            pizza.Ingredients = new List<Ingredient>();

            IngredientToPizza(pizza, selectedIngredients);
            //fine simulazione

            Pizzas.Add(pizza);
        }
        private static void IngredientToPizza(Pizza pizza, List<int> selectedIngredient)
        {
            pizza.Category = new Category() { Id = 1, Name = "Fake cateogry" };

            foreach (int tagId in selectedIngredient)
            {
                pizza.Ingredients.Add(new Ingredient() { Id = tagId, Name = "Fake ingredient " + tagId });
            }
        }
        public void Delete(Pizza pizza)
        {
            Pizzas.Remove(pizza);
        }

        public Pizza getID(int id)
        {
            Pizza pizza = Pizzas.Where(pizza => pizza.Id == id).FirstOrDefault();

            pizza.Category = new Category() { Id = 1, Name = "Fake cateogry" };

            return pizza;
        }

        public void Update(Pizza pizza, Pizza formData, List<int>? SelectedIngredients)
        {
            pizza = formData;
            pizza.Category = new Category() { Id = 1, Name = "Fake cateogry" };

            pizza.Ingredients = new List<Ingredient>();

            //simulazione da implentare con ListTagRepository

            IngredientToPizza(pizza, SelectedIngredients);
            //fine simulazione
        }
    
    }
}
