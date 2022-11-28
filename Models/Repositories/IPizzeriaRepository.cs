namespace la_mia_pizzeria_static.Models.Repositories
{
    public interface IPizzeriaRepository
    {
        List<Pizza> All();
       
         List<Category> AllCat();
        
         List<Ingredient> AllIng();
        
         Pizza getID(int id);
        
         Category GetByIdCat(int id);
        
         Ingredient GetByIdIng(int id);
        
         void Create(Pizza pizza, List<int> selectedIngredients);
        
         void CreateCat(Category category);
        

         void CreateIng(Ingredient ingredient);
        
         int GetByNameCategory(Category category);
        
         int GetByNameIngredient(Ingredient ingredient);
        
         void Update(Pizza pizza, Pizza formData, List<int>? SelectedIngredients, Category category);
       
         void Delete(Pizza pizza);
       
         void UpdateCat(Category category);
       
         void Deletecat(Category category);
      

         void UpdateIng(Ingredient ingredient);
        
         void DeleteIng(Ingredient ingredient);
        
    }


}