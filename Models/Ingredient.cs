using System.ComponentModel.DataAnnotations;

namespace la_mia_pizzeria_static.Models
{
    public class Ingredient
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Il nome ingrediente è obbligatorio")]
        [StringLength(20, ErrorMessage = "Il nome non può essere oltre 20 caratteri")]
        public string Name { get; set; }

        //relazione molti a molti con Pizza 
        public List<Pizza>? Pizzas { get; set; }

        public Ingredient()
        {
        }

        public Ingredient(string name, List<Pizza>? pizzas)
        {
            Name = name;
            Pizzas = pizzas;
        }
    }
}
