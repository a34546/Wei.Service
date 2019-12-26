using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Wei.Repository;

namespace Wei.Service
{

    public interface IAppService<TEntity, TDto> : IAppService<TEntity, TDto, int> where TDto : class
    {

    }

    public interface IAppService<TEntity, TDto, TPrimaryKey> where TDto : class
    {
        #region Query

        TDto Get(TPrimaryKey id);

        Task<TDto> GetAsync(TPrimaryKey id);

        List<TDto> GetAll();

        Task<List<TDto>> GetAllAsync();

        List<TDto> GetAll(Expression<Func<TEntity, bool>> predicate);

        Task<List<TDto>> GetAllAsync(Expression<Func<TEntity, bool>> predicate);

        TDto FirstOrDefault();

        Task<TDto> FirstOrDefaultAsync();

        TDto FirstOrDefault(Expression<Func<TEntity, bool>> predicate);

        Task<TDto> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate);

        #endregion

        #region Insert

        TDto Insert(TDto entity);

        Task<TDto> InsertAsync(TDto entity);

        void Insert(List<TDto> entities);

        Task InsertAsync(List<TDto> entities);

        #endregion Insert

        #region Update

        TDto Update(TDto entity);

        Task<TDto> UpdateAsync(TDto entity);

        #endregion Update

        #region Delete

        void Delete(TEntity entity);

        Task DeleteAsync(TEntity entity);

        void Delete(TPrimaryKey id);

        Task DeleteAsync(TPrimaryKey id);

        void Delete(Expression<Func<TEntity, bool>> predicate);

        Task DeleteAsync(Expression<Func<TEntity, bool>> predicate);

        #endregion

        #region HardDelete

        void HardDelete(TEntity entity);

        Task HardDeleteAsync(TEntity entity);

        void HardDelete(TPrimaryKey id);

        Task HardDeleteAsync(TPrimaryKey id);

        void HardDelete(Expression<Func<TEntity, bool>> predicate);

        Task HardDeleteAsync(Expression<Func<TEntity, bool>> predicate);

        #endregion

        #region Aggregate

        bool Any(Expression<Func<TEntity, bool>> predicate);

        Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate);

        int Count();

        Task<int> CountAsync();

        int Count(Expression<Func<TEntity, bool>> predicate);

        Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate);

        long LongCount();

        Task<long> LongCountAsync();

        long LongCount(Expression<Func<TEntity, bool>> predicate);

        Task<long> LongCountAsync(Expression<Func<TEntity, bool>> predicate);

        #endregion
    }
}
