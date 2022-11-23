using Microsoft.Extensions.Hosting;
using System.ComponentModel.DataAnnotations;
using la_mia_pizzeria_static.Validation;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace la_mia_pizzeria_static.Models
{
    public class Category
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Il nome è obbligatorio")]
        [StringLength(20, ErrorMessage = "Il nome non può essere oltre 20 caratteri")]
        public string Name { get; set; }

        [Required(ErrorMessage = "La descrizione è obbligatoria")]
        [StringLength(100, ErrorMessage = "La descrizione non può essere oltre 100 caratteri")]
        public string Description { get; set; }

        public List<Pizza>? Pizzas { get; set; }

        public Category()
        {
        }

        public Category(string name)
        {
            Name = name;
        }
    }
}
