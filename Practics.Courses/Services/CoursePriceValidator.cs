using System.Windows.Controls;
using Practics.Courses.Models;
using ValidationResult = Practics.Courses.Models.ValidationResult;

namespace Practics.Courses.Services
{
    public class CoursePriceValidator : EntityValidator<CoursePrice>
    {
        public override ValidationResult Validate(CoursePrice entity)
        {
            if (entity.Price == 0)
                return new ValidationResult("Цена не может равняться 0 рублей");

            if (entity.PriceWithTax == 0)
                return new ValidationResult("Цена с налогом не может равняться 0 рублей");

            return new ValidationResult(true);
        }
    }
}