using CommonApi.application.Common;
using System.Linq.Expressions;

namespace CommonApi.application.Interfaces.Repositories
{
	public interface IGenericRepository<T> where T : class
	{
		//Synchronous methods
		T? GeyById(string id);
		T? GetInclude(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes);
		List<T> GetIncludeList(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes);
		IQueryable<T> GetAll();
		IQueryable<T> Find(Expression<Func<T, bool>> expression);
		ResultData<T> FindWithPagination(Expression<Func<T, bool>> expression, string orderBy, string direction, int pageIndex = 0, int pageSize = 0);
		ResultData<T> FindWithPaginationV2(Expression<Func<T, bool>> expression, PaginationInfo pageInfo);
		public ResultData<T> FindWithPaginationAndIncludes(Expression<Func<T, bool>> expression, PaginationInfo pageInfo, params Expression<Func<T, object>>[] includes);
		void Add(T entity);
		void AddRange(IEnumerable<T> entities);
		void Update(T entity);
		void UpdateRange(IEnumerable<T> entities);
		void Delete(T entity);
		void DeleteRange(IEnumerable<T> entities);
		int SaveChanges();

		//Asynchronous methods
		Task<T?> GetByIdAsync(string id);
		Task<T?> GetIncludeAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes);
		Task AddAsync(T entity);
		Task AddRangeAsync(IEnumerable<T> entities);
		Task<int> SaveChangesAsync();
	}
}
