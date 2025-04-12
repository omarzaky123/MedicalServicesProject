using MedicalServices.Models;
using MedicalServices.ViewModel;
using Microsoft.EntityFrameworkCore;

namespace MedicalServices.Repository
{
    public class GeneralRepository<T> : IGeneralRepository<T> where T : class, new()
    {
        private readonly MyContext context;
        private readonly DbSet<T> dbSet;

        public GeneralRepository(MyContext _myContext)
        {
            context = _myContext;
            dbSet = context.Set<T>();

        }

        public void Delete(int id)
        {
            T element=dbSet.Find(id);
            if (element != null) { 
                dbSet.Remove(element);
                context.SaveChanges();
            } 
        }

        public T GetById(int id)
        {
            T element = dbSet.Find(id);
            if (element != null)
                return element;
            return new T();
          
        }

        public List<T> GetAll()
        {
            return dbSet.ToList();
        }

        public void Insert(T entity)
        {
            var existingEntity = dbSet.Local.FirstOrDefault(e => e == entity);
            if (existingEntity != null)
            {
                context.Entry(existingEntity).State = EntityState.Detached;
            }
            dbSet.Add(entity);
            context.SaveChanges();
        }


        //still Update
        //public void Update(int id, T entity)
        //{
        //    T existingEntity = dbSet.Find(id);
        //    if (existingEntity != null)
        //    {
        //        context.Entry(existingEntity).CurrentValues.SetValues(entity);
        //        context.SaveChanges();
        //    }
        //}
        public void Update(int id, T entity)
        {
            T existingEntity = dbSet.Find(id);
            if (existingEntity != null)
            {
                var entry = context.Entry(existingEntity);
                foreach (var property in entry.OriginalValues.Properties)
                {
                    if (property.Name != "ID") // Skip the primary key
                    {
                        var currentValue = entry.Property(property.Name).CurrentValue;
                        var newValue = entity.GetType().GetProperty(property.Name).GetValue(entity);
                        if (!Equals(currentValue, newValue))
                        {
                            entry.Property(property.Name).CurrentValue = newValue;
                        }
                    }
                }
                context.SaveChanges();
            }
        }

    }
}
