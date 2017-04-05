using System.Collections.Generic;

namespace Infrastructure.DataAccess
{
    public abstract class BaseRepository<T>
    {
        public abstract void Create(T entity);

        public abstract IEnumerable<T> Read();

        public abstract T Read(int id);

        public abstract void Update(T entity);

        public abstract void Delete(int id);
    }
}