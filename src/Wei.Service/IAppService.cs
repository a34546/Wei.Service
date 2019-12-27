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

        /// <summary>
        /// 根据主键Id获取
        /// </summary>
        TDto Get(TPrimaryKey id);
        Task<TDto> GetAsync(TPrimaryKey id);

        /// <summary>
        /// 获取所有，不包括逻辑删除的
        /// </summary>
        List<TDto> GetAll();
        Task<List<TDto>> GetAllAsync();
        List<TDto> GetAll(Expression<Func<TEntity, bool>> predicate);
        Task<List<TDto>> GetAllAsync(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// 获取第一个
        /// </summary>
        TDto FirstOrDefault();
        Task<TDto> FirstOrDefaultAsync();
        TDto FirstOrDefault(Expression<Func<TEntity, bool>> predicate);
        Task<TDto> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate);
        TDto FirstOrDefaultNoTracking(Expression<Func<TEntity, bool>> predicate);
        Task<TDto> FirstOrDefaultNoTrackingAsync(Expression<Func<TEntity, bool>> predicate);

        #endregion

        #region Insert
        /// <summary>
        /// 新增
        /// </summary>
        TDto Insert(TDto entity);
        Task<TDto> InsertAsync(TDto entity);

        /// <summary>
        /// 批量新增
        /// </summary>
        void Insert(List<TDto> entities);
        Task InsertAsync(List<TDto> entities);

        #endregion Insert

        #region Update

        /// <summary>
        /// 更新
        /// </summary>
        TDto Update(TDto entity);
        Task<TDto> UpdateAsync(TDto entity);

        #endregion Update

        #region Delete

        /// <summary>
        /// 逻辑删除，标记IsDelete = 1
        /// </summary>
        /// <param name="entity"></param>
        void Delete(TEntity entity);
        Task DeleteAsync(TEntity entity);
        void Delete(TPrimaryKey id);
        Task DeleteAsync(TPrimaryKey id);
        void Delete(Expression<Func<TEntity, bool>> predicate);
        Task DeleteAsync(Expression<Func<TEntity, bool>> predicate);

        #endregion

        #region HardDelete

        /// <summary>
        /// 物理删除，从数据库中移除
        /// </summary>
        /// <param name="entity"></param>
        void HardDelete(TEntity entity);
        Task HardDeleteAsync(TEntity entity);
        void HardDelete(TPrimaryKey id);
        Task HardDeleteAsync(TPrimaryKey id);
        void HardDelete(Expression<Func<TEntity, bool>> predicate);
        Task HardDeleteAsync(Expression<Func<TEntity, bool>> predicate);

        #endregion

        #region Aggregate

        /// <summary>
        /// 聚合操作
        /// </summary>
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
