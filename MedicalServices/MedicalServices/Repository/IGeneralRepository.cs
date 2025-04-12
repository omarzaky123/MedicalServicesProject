namespace MedicalServices.Repository
{
    public interface IGeneralRepository<T>
        where T : class,new()
    {
        List<T> GetAll();
        T GetById(int id);
        void Insert(T entity);
        void Update(int id,T entity);
        void Delete(int id);

    }
}
