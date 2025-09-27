using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.DataBase
{
    internal class GenericRepository<T> where T : class
    {
        internal ObservableCollection<T> Get()
        {
            using( var context = new DataBaseContext())
            {
                return new ObservableCollection<T>(context.Set<T>().ToList());
            }
        }

        internal void Add(T entity)
        {
            using (var context = new DataBaseContext())
            {
                context.Set<T>().Add(entity);
                context.SaveChanges();
            }
        }

        internal void Delete(T entity)
        {
            using (var context = new DataBaseContext())
            {
                context.Set<T>().Remove(entity);
                context.SaveChanges();
            }
        }

        internal void Update(T entity)
        {
            using (var context = new DataBaseContext())
            {
                context.Set<T>().Update(entity);
                context.SaveChanges();            
            }
        }
    }
}
