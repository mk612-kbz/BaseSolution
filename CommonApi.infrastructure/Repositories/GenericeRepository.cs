
using CommonApi.application.Common;
using CommonApi.application.Interfaces.Repositories;
using CommonApi.infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CommonApi.infrastructure.Repositories
{
	public class GenericeRepository<T> : IGenericRepository<T> where T : class
	{
		private readonly ApplicationDbContext _context;
		private readonly DbSet<T> _dbSet;

		public void Add(T entity)
		{
			throw new NotImplementedException();
		}

		public Task AddAsync(T entity)
		{
			throw new NotImplementedException();
		}

		public void AddRange(IEnumerable<T> entities)
		{
			throw new NotImplementedException();
		}

		public Task AddRangeAsync(IEnumerable<T> entities)
		{
			throw new NotImplementedException();
		}

		public void Delete(T entity)
		{
			throw new NotImplementedException();
		}

		public void DeleteRange(IEnumerable<T> entities)
		{
			throw new NotImplementedException();
		}

		public IQueryable<T> Find(Expression<Func<T, bool>> expression)
		{
			throw new NotImplementedException();
		}

		public ResultData<T> FindWithPagination(Expression<Func<T, bool>> expression, string orderBy, string direction, int pageIndex = 0, int pageSize = 0)
		{
			throw new NotImplementedException();
		}

		public ResultData<T> FindWithPaginationAndIncludes(Expression<Func<T, bool>> expression, PaginationInfo pageInfo, params Expression<Func<T, object>>[] includes)
		{
			throw new NotImplementedException();
		}

		public ResultData<T> FindWithPaginationV2(Expression<Func<T, bool>> expression, PaginationInfo pageInfo)
		{
			throw new NotImplementedException();
		}

		public IQueryable<T> GetAll()
		{
			throw new NotImplementedException();
		}

		public Task<T?> GetByIdAsync(string id)
		{
			throw new NotImplementedException();
		}

		public T? GetInclude(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes)
		{
			throw new NotImplementedException();
		}

		public Task<T?> GetIncludeAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes)
		{
			throw new NotImplementedException();
		}

		public List<T> GetIncludeList(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes)
		{
			throw new NotImplementedException();
		}

		public T? GeyById(string id)
		{
			throw new NotImplementedException();
		}

		public int SaveChanges()
		{
			throw new NotImplementedException();
		}

		public Task<int> SaveChangesAsync()
		{
			throw new NotImplementedException();
		}

		public void Update(T entity)
		{
			throw new NotImplementedException();
		}

		public void UpdateRange(IEnumerable<T> entities)
		{
			throw new NotImplementedException();
		}
	}
}
