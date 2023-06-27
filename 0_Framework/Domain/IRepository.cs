﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace _0_Framework.Domain
{
	public interface IRepository<TKey, T> where T : class
	{
		T Get(TKey key);
		List<T> Get();
		bool Exists(Expression<Func<T,bool>> exception);
		void Create(T entity);
		List<T> ToPaged(int page, int pagesize = 1);
		void SaveChanges();
	}
}
