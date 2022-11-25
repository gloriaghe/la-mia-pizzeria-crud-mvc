namespace la_mia_pizzeria_static.Models.Repositories
{
    public interface IDbPizzeriaRepository
    {
        List<Pizza> All();
        void Create(Pizza pizza, List<int> selectedIngredients);
        void Delete(Pizza pizza);
        Pizza getID(int id);
        void Update(Pizza pizza, Pizza formData, List<int>? SelectedIngredients);
    }
}