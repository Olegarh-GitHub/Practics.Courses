using System.Data.Entity;
using Practics.Courses.Models;

namespace Practics.Courses.Contexts
{
    public class ApplicationContext : DbContext
    {
        public DbSet<CoursePrice> CoursePrices { get; set; }
        public DbSet<Person> Persons { get; set; }

        public ApplicationContext() : base("Courses") { }
    }
}