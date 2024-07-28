using System.ComponentModel.DataAnnotations;

namespace TaskManager.Attributes

{//Criando novo atributo para a data ser maior ou igual a data atual
    public class FutureDateAttribute : ValidationAttribute

    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is DateTime date)
            {
                if (date < DateTime.Today)
                {
                    return new ValidationResult("Due date must be greater than today's date!");
                }
                
            }
            return ValidationResult.Success;
        }
    }
}
