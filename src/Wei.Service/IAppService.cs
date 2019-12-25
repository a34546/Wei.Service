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
        #region Get
        TDto GetById(TPrimaryKey id);
        TDto GetByIdNoTracking(TPrimaryKey id);
        List<TDto> GetAllList();
        List<TDto> GetAllList(Expression<Func<TEntity, bool>> predicate);
        Task<TDto> GetByIdAsync(TPrimaryKey id);
        Task<TDto> GetByIdNoTrackingAsync(TPrimaryKey id);
        Task<List<TDto>> GetAllListAsync();
        Task<List<TDto>> GetAllListAsync(Expression<Func<TEntity, bool>> predicate);
        #endregion

        #region Insert
        TDto Insert(TDto dto);
        void Insert(List<TDto> dtos);
        Task<TDto> InsertAsync(TDto dto);
        Task InsertAsync(List<TDto> dtos);
        #endregion

        #region Update
        TDto Update(TDto dto);
        void Update(List<TDto> dtos);
        TEntity Update(TEntity entity, params Expression<Func<TEntity, object>>[] properties);
        Task<TDto> UpdateAsync(TDto dto);
        Task UpdateAsync(List<TDto> dtos);
        Task<TEntity> UpdateAsync(TEntity entity, params Expression<Func<TEntity, object>>[] properties);
        #endregion

        #region Delete/HardDelete
        void Delete(TPrimaryKey id);
        void Delete(TEntity entity);
        void Delete(IEnumerable<TEntity> entities);
        void Delete(Expression<Func<TEntity, bool>> predicate);
        Task DeleteAsync(TPrimaryKey id);
        Task DeleteAsync(TEntity entity);
        Task DeleteAsync(IEnumerable<TEntity> entities);
        Task DeleteAsync(Expression<Func<TEntity, bool>> predicate);
        void HardDelete(TPrimaryKey id);
        void HardDelete(TEntity entity);
        void HardDelete(IEnumerable<TEntity> entities);
        void HardDelete(Expression<Func<TEntity, bool>> predicate);
        Task HardDeleteAsync(TPrimaryKey id);
        Task HardDeleteAsync(TEntity entity);
        Task HardDeleteAsync(IEnumerable<TEntity> entities);
        Task HardDeleteAsync(Expression<Func<TEntity, bool>> predicate);
        #endregion
    }
}
