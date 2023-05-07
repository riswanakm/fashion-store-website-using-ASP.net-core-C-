using System.Collections.Generic;

namespace Group7_FinalProject.Models
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> List(QueryOptions<T> options);

        int Count { get; }

        T Get(QueryOptions<T> options);
        T Get(int id);
        T Get(string id);

        void Insert(T entity);
        void Update(T entity);
        void Delete(T entity);

        void Save();
    }
}
