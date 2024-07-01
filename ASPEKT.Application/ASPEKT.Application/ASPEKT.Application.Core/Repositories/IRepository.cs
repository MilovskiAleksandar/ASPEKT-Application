using ASPEKT.Application.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
