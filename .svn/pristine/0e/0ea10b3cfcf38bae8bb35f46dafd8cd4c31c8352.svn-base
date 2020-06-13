using System;
using System.Data.Entity;
using System.Linq;

namespace CmCustomDal
{
    public abstract class CrudDAL<T> : ICrudDAL<T> where T : class
    {
        CmContext ctx = new CmContext(ConnectionOracleDal.connectionString);

        public void create(T oEntity)
        {
            ctx.Set<T>().Add(oEntity);
            ctx.SaveChanges();
        }

        public void delete(Func<T, bool> predicate)
        {
            ctx.Set<T>()
           .Where(predicate).ToList()
           .ForEach(del => ctx.Set<T>().Remove(del));
            ctx.SaveChanges();
        }

        public void update(T oEntity)
        {
            ctx.Set<T>().Attach(oEntity);
            ctx.Entry(oEntity).State = EntityState.Modified;
            ctx.SaveChanges();
        }

        public IQueryable<T> find()
        {
            return ctx.Set<T>();
        }

    }
}
