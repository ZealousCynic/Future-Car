using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public abstract class Repository<T> : IRepository<T>
    {
        public abstract void Create(T entity);

        public abstract void Delete(T entity);

        public abstract T[] GetAll();

        public abstract T GetById(int id);

        public abstract void Update(T entity);
    }
}
