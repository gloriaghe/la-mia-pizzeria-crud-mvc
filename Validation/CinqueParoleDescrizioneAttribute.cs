using System.ComponentModel.DataAnnotations;

namespace la_mia_pizzeria_static.Validation
{
    public class CinqueParoleDescrizioneAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if ((string)value == null)
            {
                return new ValidationResult("Il campo descrizione è vuoto");
            }

            string stringValue = (string)value;
            stringValue = stringValue.Trim();

            char space = ' ';

            int count(string stringa, char space)
            {
                int count = 0;

                foreach (char spaces in stringa)
                {
                    if (spaces == ' ')
                        count++;
                }
                return count;
            }
            count(stringValue, space);


            if (count(stringValue, space) < 4)
            {
                return new ValidationResult("Il campo descrizione deve contenere almeno cinque parole");
            }

            return ValidationResult.Success;
        }
    }
}
