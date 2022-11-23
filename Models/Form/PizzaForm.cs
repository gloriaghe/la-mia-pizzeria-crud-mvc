namespace la_mia_pizzeria_static.Models.Form

    //classe create per passare più modelli alla pagina del form
{
    public class PizzaForm
    {
        public Pizza Pizza { get; set; }

        public List<Category>? Categories { get; set; }
    }
}
