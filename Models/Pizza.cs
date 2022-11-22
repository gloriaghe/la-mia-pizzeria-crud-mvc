using la_mia_pizzeria_static.Validation;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace la_mia_pizzeria_static.Models
{
    public class Pizza
    {

        public int Id { get; set; }

        [Required(ErrorMessage = "Il titolo è obbligatorio")]
        [StringLength(80, ErrorMessage = "Il titolo non può essere oltre 80 caratteri")]
        public string Name { get; set; }

        [Column(TypeName = "text")]
        [Required(ErrorMessage = "La descrizione è obbligatoria")]
        [StringLength(250, ErrorMessage = "La descrizione non può superare i 250 caratteri")]
        [CinqueParoleDescrizione]
        public string Description { get; set; }

        [Required(ErrorMessage = "L'immagine è obbligatoria")]
        public string Image { get; set; }

        [Required(ErrorMessage = "Il prezzo è obbligatorio")]
        [Range(0, 200, ErrorMessage = "Il prezzo deve essere compreso fra 0 e 200")]
        public double Price { get; set; }

        public Pizza(string name, string description, string image, double price)
        {
            Name = name;
            Description = description;
            Image = image;
            Price = price;
        }
        public Pizza()
        {
        }

    }
}
