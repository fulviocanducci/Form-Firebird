using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace WinFirebase.Utils
{
    public static class EFExtensions
    {
        public static void Detach<T>(this DbContext ctx, T model) where T : class, new()
        {
            ctx.Entry(model).State = EntityState.Detached;
        }

        public static IEnumerable<T> All<T>(this IQueryable<T> query) where T : class, new()
        {
            return query.AsNoTracking().ToList();
        }
        public static async Task<IEnumerable<T>> AllAsync<T>(this IQueryable<T> query) where T : class, new()
        {
            return await query.AsNoTracking().ToListAsync();
        }

        public static T Get<T>(this IQueryable<T> query, Expression<Func<T, bool>> where) where T : class, new()
        {
            return query.AsNoTracking().Where(where).FirstOrDefault();
        }

        public static async Task<T> GetAsync<T>(this IQueryable<T> query, Expression<Func<T, bool>> where) where T : class, new()
        {
            return await query.AsNoTracking().Where(where).FirstOrDefaultAsync();
        }
    }
}
