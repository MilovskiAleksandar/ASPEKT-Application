using ASPEKT.Application.Core.Models;


namespace ASPEKT.Application.Core.Repositories
{
    public interface IRepository<T> where T : BaseEntity
    {
        List<T> GetAll();
        T GetById(int id);
        int Create(T entity);
        void Update(T entity);
        void Delete(int id);
    }
}
