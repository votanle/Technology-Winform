using SE214L22.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SE214L22.Data.Repository
{
    public class BaseRepository<T> where T : AppEntity
    {
        public virtual T Get(int id)
        {
            using (var ctx = new AppDbContext())
            {
                var entity = ctx.Set<T>().Where(e => e.Id == id).FirstOrDefault();
                return entity;
            }
        }

        public virtual T Create(T entity)
        {
            using (var ctx = new AppDbContext())
            {
                var storedEntity = ctx.Set<T>().Add(entity);
                ctx.SaveChanges();
                return storedEntity;
            }
        }

        public virtual bool Update(T entity)
        {
            using (var ctx = new AppDbContext())
            {
                var storedEntity = ctx.Set<T>().Where(x => x.Id == entity.Id).FirstOrDefault();
                if (storedEntity != null)
                {
                    ctx.Entry(storedEntity).CurrentValues.SetValues(entity);
                    ctx.SaveChanges();
                    return true;
                }
                return false;
            }
        }

        public virtual bool Delete(int id)
        {
            using (var ctx = new AppDbContext())
            {
                var storedEntity = ctx.Set<T>().Where(x => x.Id == id).FirstOrDefault();
                if (storedEntity != null)
                {
                    ctx.Set<T>().Remove(storedEntity);
                    ctx.SaveChanges();
                    return true;
                }
                return false;
            }
        }

    }
}
