using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using _0_Framework.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace _0_Framework.Infrastructure
{
	public class RepositoryBase <TKey, T> : IRepository<TKey, T> where T : class
	{
		private readonly DbContext _context;

		public RepositoryBase(DbContext context)
		{
			_context = context;

		}


		public T Get(TKey id)
		{
			return _context.Find<T>(id);

			
		}

		

		public List<T> Get()
		{
			return _context.Set<T>().ToList();
		}

		public bool Exists(Expression<Func<T, bool>> exception)
		{
			return _context.Set<T>().Any(exception);
		}

		public void Create(T entity)
		{
			_context.Add(entity);
		}

		public List<T> ToPaged(int page, int pagesize = 1)
		{
			var source = Get();

			return source.Skip((page - 1) * pagesize).Take(pagesize).ToList();
		}

		public void SaveChanges()
        {
	        var modify = _context.ChangeTracker.Entries()
		        .Where(p => p.State == EntityState.Modified ||
					 p.State == EntityState.Added || 
					 p.State == EntityState.Deleted);

	        foreach (var item in modify)
	        {
		        var entityType = item.Context.Model.FindEntityType(item.Entity.GetType());

		        var Inserted = entityType.FindProperty("InsertTime");
		        var Updated = entityType.FindProperty("UpdateTime");
		        var Removed = entityType.FindProperty("RemoveTime");
		        var IsRemoved = entityType.FindProperty("IsRemoved");

		        if (item.State == EntityState.Added && Inserted != null)
		        {
			        item.Property("InsertTime").CurrentValue = DateTime.Now;
		        }
		        if (item.State == EntityState.Modified && Updated != null)
		        {
			        item.Property("UpdateTime").CurrentValue = DateTime.Now;
		        }
		        if (item.State == EntityState.Deleted && Removed != null)
		        {
			        item.Property("RemoveTime").CurrentValue = DateTime.Now;
			        item.Property("IsRemoved").CurrentValue = true;
		        }
		        
			}

			
	        _context.SaveChanges();
        }
	}
}
