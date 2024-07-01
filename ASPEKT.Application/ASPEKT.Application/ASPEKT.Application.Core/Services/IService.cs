

namespace ASPEKT.Application.Core.Services
{
    public interface IService<T>
    {
        List<T> GetAll();
        T GetById(int id);
        void AddEntity(T entity);
        void UpdateEntity(T entity);
        void DeleteEntity(int id);
    }
}
