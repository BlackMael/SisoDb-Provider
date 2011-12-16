using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using EnsureThat;
using SisoDb.Querying;

namespace SisoDb
{
    [DebuggerStepThrough]
    public class ReadOnce : IReadOnce
    {
    	private readonly ISisoDatabase _db;

        public ReadOnce(ISisoDatabase db)
        {
            _db = db;
        }

		public int Count<T>() where T : class
		{
			using (var rs = _db.CreateReadSession())
			{
				return rs.Query<T>().Count();
			}
		}

		public int Count<T>(Expression<Func<T, bool>> expression) where T : class
		{
			Ensure.That(expression, "expression").IsNotNull();

			return Query<T>().Count(expression);
		}

		public T GetById<T>(object id) where T : class
		{
			using (var rs = _db.CreateReadSession())
			{
				return rs.GetById<T>(id);
			}
		}

		public TOut GetByIdAs<TContract, TOut>(object id)
			where TContract : class
			where TOut : class
		{
			using (var rs = _db.CreateReadSession())
			{
				return rs.GetByIdAs<TContract, TOut>(id);
			}
		}

		public IList<T> GetByIds<T>(params object[] ids) where T : class
		{
			Ensure.That(ids, "ids").HasItems();

			using (var rs = _db.CreateReadSession())
			{
				return rs.GetByIds<T>(ids).ToList();
			}
		}

		public IList<TOut> GetByIdsAs<TContract, TOut>(params object[] ids)
			where TContract : class
			where TOut : class
		{
			Ensure.That(ids, "ids").HasItems();

			using (var rs = _db.CreateReadSession())
			{
				return rs.GetByIdsAs<TContract, TOut>(ids).ToList();
			}
		}

		public IList<T> GetByIdInterval<T>(object idFrom, object idTo) where T : class
		{
			using (var rs = _db.CreateReadSession())
			{
				return rs.GetByIdInterval<T>(idFrom, idTo).ToList();
			}
		}

		public string GetByIdAsJson<T>(object id) where T : class
		{
			using (var rs = _db.CreateReadSession())
			{
				return rs.GetByIdAsJson<T>(id);
			}
		}

		public IList<string> GetByIdsAsJson<T>(params object[] ids) where T : class
		{
			Ensure.That(ids, "ids").HasItems();

			using (var rs = _db.CreateReadSession())
			{
				return rs.GetByIdsAsJson<T>(ids).ToList();
			}
		}

		public IList<T> NamedQuery<T>(INamedQuery query) where T : class
		{
			Ensure.That(query, "query").IsNotNull();

			using (var rs = _db.CreateReadSession())
			{
				return rs.Advanced.NamedQuery<T>(query).ToList();
			}
		}

		public IList<TOut> NamedQueryAs<TContract, TOut>(INamedQuery query)
			where TContract : class
			where TOut : class
		{
			Ensure.That(query, "query").IsNotNull();

			using (var rs = _db.CreateReadSession())
			{
				return rs.Advanced.NamedQueryAs<TContract, TOut>(query).ToList();
			}
		}

		public IList<string> NamedQueryAsJson<T>(INamedQuery query) where T : class
		{
			Ensure.That(query, "query").IsNotNull();

			using (var rs = _db.CreateReadSession())
			{
				return rs.Advanced.NamedQueryAsJson<T>(query).ToList();
			}
		}

		public ISisoQueryable<T> Query<T>() where T : class
		{
			return new SisoReadOnceQueryable<T>(_db.ProviderFactory.GetQueryBuilder<T>(_db.StructureSchemas), () => _db.CreateReadSession());
		}
    }
}