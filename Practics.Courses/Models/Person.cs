using System;
using Practics.Courses.Models.Base;

namespace Practics.Courses.Models
{
    public class Person : Entity
    {
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public DateTime Birthday { get; set; }
        public bool IsMale { get; set; }
    }
}