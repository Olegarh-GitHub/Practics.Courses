using Practics.Courses.Models.Base;

namespace Practics.Courses.Models
{
    public class CoursePrice : Entity
    {
        public decimal Price { get; set; }
        public decimal PriceWithTax { get; set; }
    }
}