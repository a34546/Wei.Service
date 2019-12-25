using Nelibur.ObjectMapper;
using Nelibur.ObjectMapper.Bindings;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Wei.Repository;

namespace Wei.Service
{

    public class AppService<TEntity, TDto> : AppService<TEntity, TDto, int>, IAppService<TEntity, TDto> where TDto : class where TEntity : class, IEntity<int>
    {
        public AppService(IRepository<TEntity, int> repository, IUnitOfWork unitOfWork) : base(repository, unitOfWork)
        {
        }
    }

    public class AppService<TEntity, TDto, TPrimaryKey> : IAppService<TEntity, TDto, TPrimaryKey> where TDto : class where TEntity : class, IEntity<TPrimaryKey>
    {
        public IRepository<TEntity, TPrimaryKey> Repository { get; private set; }

        readonly IUnitOfWork _unitOfWork;
        public AppService(IRepository<TEntity, TPrimaryKey> repository, IUnitOfWork unitOfWork)
        {
            Repository = repository;
            _unitOfWork = unitOfWork;
        }

        public void Commit()
        {
            _unitOfWork.SaveChanges();
        }

        public async Task CommitAsync()
        {
            await _unitOfWork.SaveChangesAsync();
        }

        public virtual TEntity ToEntity(TDto dto)
        {
            return dto.MapTo<TDto, TEntity>();
        }

        public virtual TDto ToDto(TEntity entity)
        {
            return entity.MapTo<TEntity, TDto>();
        }

        public virtual List<TEntity> ToEntities(List<TDto> dtos)
        {
            return dtos.MapTo<List<TDto>, List<TEntity>>();
        }

        public virtual List<TDto> ToDtos(List<TEntity> entities)
        {
            return entities.MapTo<List<TEntity>, List<TDto>>();
        }

        #region Get
        public virtual TDto GetById(TPrimaryKey id)
        {
            var entity = Repository.GetById(id);
            return ToDto(entity);
        }
        public virtual TDto GetByIdNoTracking(TPrimaryKey id)
        {
            var entity = Repository.GetByIdNoTracking(id);
            return ToDto(entity);
        }
        public virtual List<TDto> GetAllList()
        {
            var entities = Repository.GetAllList();
            return ToDtos(entities);
        }
        public virtual List<TDto> GetAllList(Expression<Func<TEntity, bool>> predicate)
        {
            var entities = Repository.GetAllList(predicate);
            return ToDtos(entities);
        }
        public virtual async Task<TDto> GetByIdAsync(TPrimaryKey id)
        {
            var entity = await Repository.GetByIdAsync(id);
            return ToDto(entity);
        }
        public virtual async Task<TDto> GetByIdNoTrackingAsync(TPrimaryKey id)
        {
            var entity = await Repository.GetByIdNoTrackingAsync(id);
            return ToDto(entity);
        }
        public virtual async Task<List<TDto>> GetAllListAsync()
        {
            var entities = await Repository.GetAllListAsync();
            return ToDtos(entities);
        }
        public virtual async Task<List<TDto>> GetAllListAsync(Expression<Func<TEntity, bool>> predicate)
        {
            var entities = await Repository.GetAllListAsync(predicate);
            return ToDtos(entities);
        }
        #endregion

        #region Insert
        public virtual TDto Insert(TDto dto)
        {
            var entity = ToEntity(dto);
            Repository.Insert(entity);
            _unitOfWork.SaveChanges();
            return ToDto(entity);
        }
        public virtual void Insert(List<TDto> dtos)
        {
            var entities = ToEntities(dtos);
            Repository.Insert(entities);
            _unitOfWork.SaveChanges();
        }
        public virtual async Task<TDto> InsertAsync(TDto dto)
        {
            var entity = ToEntity(dto);
            await Repository.InsertAsync(entity);
            await _unitOfWork.SaveChangesAsync();
            return ToDto(entity);
        }
        public virtual async Task InsertAsync(List<TDto> dtos)
        {
            var entities = ToEntities(dtos);
            await Repository.InsertAsync(entities);
            await _unitOfWork.SaveChangesAsync();

        }
        #endregion

        #region Update
        public virtual TDto Update(TDto dto)
        {
            var entity = ToEntity(dto);
            Repository.Update(entity);
            _unitOfWork.SaveChanges();
            return ToDto(entity);
        }
        public virtual void Update(List<TDto> dtos)
        {
            var entities = ToEntities(dtos);
            Repository.Update(entities);
            _unitOfWork.SaveChanges();
        }
        public virtual TEntity Update(TEntity entity, params Expression<Func<TEntity, object>>[] properties)
        {
            Repository.Update(entity, properties);
            _unitOfWork.SaveChanges();
            return entity;
        }
        public virtual async Task<TDto> UpdateAsync(TDto dto)
        {
            var entity = ToEntity(dto);
            await Repository.UpdateAsync(entity);
            await _unitOfWork.SaveChangesAsync();
            return ToDto(entity);
        }
        public virtual async Task UpdateAsync(List<TDto> dtos)
        {
            var entities = ToEntities(dtos);
            await Repository.UpdateAsync(entities);
            await _unitOfWork.SaveChangesAsync();
        }
        public virtual async Task<TEntity> UpdateAsync(TEntity entity, params Expression<Func<TEntity, object>>[] properties)
        {
            await Repository.UpdateAsync(entity, properties);
            await _unitOfWork.SaveChangesAsync();
            return entity;
        }
        #endregion

        #region Delete/HardDelete
        public virtual void Delete(TPrimaryKey id)
        {
            Repository.Delete(id);
            _unitOfWork.SaveChanges();
        }
        public virtual void Delete(TEntity entity)
        {
            Repository.Delete(entity);
            _unitOfWork.SaveChanges();
        }
        public virtual void Delete(IEnumerable<TEntity> entities)
        {
            Repository.Delete(entities);
            _unitOfWork.SaveChanges();
        }
        public virtual void Delete(Expression<Func<TEntity, bool>> predicate)
        {
            Repository.Delete(predicate);
            _unitOfWork.SaveChanges();
        }
        public virtual async Task DeleteAsync(TPrimaryKey id)
        {
            await Repository.DeleteAsync(id);
            await _unitOfWork.SaveChangesAsync();
        }
        public virtual async Task DeleteAsync(TEntity entity)
        {
            await Repository.DeleteAsync(entity);
            await _unitOfWork.SaveChangesAsync();
        }
        public virtual async Task DeleteAsync(IEnumerable<TEntity> entities)
        {
            await Repository.DeleteAsync(entities);
            await _unitOfWork.SaveChangesAsync();
        }
        public virtual async Task DeleteAsync(Expression<Func<TEntity, bool>> predicate)
        {
            await Repository.DeleteAsync(predicate);
            await _unitOfWork.SaveChangesAsync();
        }
        public virtual void HardDelete(TPrimaryKey id)
        {
            Repository.HardDelete(id);
            _unitOfWork.SaveChanges();
        }
        public virtual void HardDelete(TEntity entity)
        {
            Repository.HardDelete(entity);
            _unitOfWork.SaveChanges();
        }
        public virtual void HardDelete(IEnumerable<TEntity> entities)
        {
            Repository.HardDelete(entities);
            _unitOfWork.SaveChanges();
        }
        public virtual void HardDelete(Expression<Func<TEntity, bool>> predicate)
        {
            Repository.HardDelete(predicate);
            _unitOfWork.SaveChanges();
        }
        public virtual async Task HardDeleteAsync(TPrimaryKey id)
        {
            await Repository.HardDeleteAsync(id);
            await _unitOfWork.SaveChangesAsync();
        }
        public virtual async Task HardDeleteAsync(TEntity entity)
        {
            await Repository.HardDeleteAsync(entity);
            await _unitOfWork.SaveChangesAsync();
        }
        public virtual async Task HardDeleteAsync(IEnumerable<TEntity> entities)
        {
            await Repository.HardDeleteAsync(entities);
            await _unitOfWork.SaveChangesAsync();
        }
        public virtual async Task HardDeleteAsync(Expression<Func<TEntity, bool>> predicate)
        {
            await Repository.HardDeleteAsync(predicate);
            await _unitOfWork.SaveChangesAsync();
        }
        #endregion
    }

}
