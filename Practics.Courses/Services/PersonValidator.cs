using Practics.Courses.Models;

namespace Practics.Courses.Services
{
    public class PersonValidator : EntityValidator<Person>
    {
        public override ValidationResult Validate(Person entity)
        {
            if (entity.Birthday.Year < 1753)
                return new ValidationResult("Проверьте корректность ввода даты");

            if (entity.LastName.Length == 0)
                return new ValidationResult("Фамилия не должна быть пустой");

            if (entity.FirstName.Length == 0)
                return new ValidationResult("Имя не должно быть пустым");

            return new ValidationResult(true);
        }
    }
}