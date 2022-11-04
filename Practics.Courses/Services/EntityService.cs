using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Windows;
using Practics.Courses.Contexts;
using Practics.Courses.Models.Base;
using ValidationResult = Practics.Courses.Models.ValidationResult;

namespace Practics.Courses.Services
{
    public class EntityService<TEntity> where TEntity : Entity
    {
        private readonly ApplicationContext _context;
        private readonly DbSet<TEntity> _dbSet;

        public EntityService(ApplicationContext context)
        {
            _context = context;
            _dbSet = context.Set<TEntity>();
        }

        public IEnumerable<TEntity> Read()
        {
            _dbSet.Load();

            var data = _dbSet.Local;

            return data;
        }

        public IEnumerable<DbEntityValidationResult> SaveChanges()
        {
            _context.SaveChanges();

            return _context.GetValidationErrors();
        }
    }
}