using Practics.Courses.Models;

namespace Practics.Courses.Services
{
    public abstract class EntityValidator<TEntity>
    {
        public abstract ValidationResult Validate(TEntity entity);
    }
}