using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace SisoDb
{
	public interface ISisoQueryable<T> where T : class
	{
		T[] ToArray();

		TResult[] ToArrayOf<TResult>() where TResult : class;

		string[] ToArrayOfJson();

		IEnumerable<T> ToEnumerable();

		IEnumerable<TResult> ToEnumerableOf<TResult>() where TResult : class;

		IEnumerable<string> ToEnumerableOfJson();

		IList<T> ToList();

		IList<TResult> ToListOf<TResult>() where TResult : class;

		IList<string> ToListOfJson();

		T First();

		TResult FirstAs<TResult>() where TResult : class;

		string FirstAsJson();

		T FirstOrDefault();

		TResult FirstOrDefaultAs<TResult>() where TResult : class;

		string FirstOrDefaultAsJson();

		T Single();

		TResult SingleAs<TResult>() where TResult : class;

		string SingleAsJson();
		
		T SingleOrDefault();

		TResult SingleOrDefaultAs<TResult>() where TResult : class;

		string SingleOrDefaultAsJson();

		int Count();

		int Count(Expression<Func<T, bool>> expression);

		ISisoQueryable<T> Take(int numOfStructures);

		ISisoQueryable<T> Page(int pageIndex, int pageSize);

		ISisoQueryable<T> Include<TInclude>(params Expression<Func<T, object>>[] expression) where TInclude : class;

		ISisoQueryable<T> Where(params Expression<Func<T, bool>>[] expression);

		ISisoQueryable<T> OrderBy(params Expression<Func<T, object>>[] expressions);

		ISisoQueryable<T> OrderByDescending(params Expression<Func<T, object>>[] expressions);
	}
}