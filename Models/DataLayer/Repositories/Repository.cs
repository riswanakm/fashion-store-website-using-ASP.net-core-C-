using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Group7_FinalProject.Models
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected FashionstoreContext context { get; set; }
        private DbSet<T> dbset { get; set; }
        public Repository(FashionstoreContext ctx)
        {
            context = ctx;
            dbset = context.Set<T>();
        }

        private int? count;
        public int Count => count ?? dbset.Count();

        public virtual IEnumerable<T> List(QueryOptions<T> options)
        {
            IQueryable<T> query = BuildQuery(options);
            return query.ToList();
        }

        public virtual T Get(int id) => dbset.Find(id);
        public virtual T Get(string id) => dbset.Find(id);
        public virtual T Get(QueryOptions<T> options)
        {
            IQueryable<T> query = BuildQuery(options);
            return query.FirstOrDefault();
        }

        public virtual void Insert(T entity) => dbset.Add(entity);
        public virtual void Update(T entity) => dbset.Update(entity);
        public virtual void Delete(T entity) => dbset.Remove(entity);
        public virtual void Save() => context.SaveChanges();

        private IQueryable<T> BuildQuery(QueryOptions<T> options)
        {
            IQueryable<T> query = dbset;
            foreach (string include in options.GetIncludes())
            {
                query = query.Include(include);
            }
            if (options.HasWhere)
            {
                foreach (var clause in options.WhereClauses)
                {
                    query = query.Where(clause);
                }
                count = query.Count();
            }
            if (options.HasOrderBy)
            {
                if (options.OrderByDirection == "asc")
                    query = query.OrderBy(options.OrderBy);
                else
                    query = query.OrderByDescending(options.OrderBy);
            }
            if (options.HasPaging)
                query = query.PageBy(options.PageNumber, options.PageSize);

            return query;
        }
    }
}
