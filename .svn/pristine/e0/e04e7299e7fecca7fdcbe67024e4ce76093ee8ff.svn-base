using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CmCustomDal
{
    public interface ICrudDAL<T> where T : class
    {
        void create(T oEntity);
        void delete(Func<T, bool> predicate);
        void update(T oEntity);
        IQueryable<T> find();
    }
}
