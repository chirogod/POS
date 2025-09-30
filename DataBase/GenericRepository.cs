using Microsoft.EntityFrameworkCore;
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
        internal async Task<ObservableCollection<T>> GetAsync()
        {
            using( var context = new DataBaseContext())
            {
                var list = await context.Set<T>().ToListAsync();
                return new ObservableCollection<T>(list);
            }
        }

        internal async Task AddAsync(T entity)
        {
            using (var context = new DataBaseContext())
            {
                await context.Set<T>().AddAsync(entity);
                await context.SaveChangesAsync();
            }
        }

        internal async Task DeleteAsync(T entity)
        {
            using (var context = new DataBaseContext())
            {
                context.Set<T>().Remove(entity);
                await context.SaveChangesAsync();
            }
        }

        internal async Task UpdateAsync(T entity)
        {
            using (var context = new DataBaseContext())
            {
                context.Set<T>().Update(entity);
                await context.SaveChangesAsync();
            }
        }
    }
}
