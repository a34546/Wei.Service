using Microsoft.EntityFrameworkCore;
using Nelibur.ObjectMapper;
using Nelibur.ObjectMapper.Bindings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Wei.Repository;

namespace Wei.Service
{

    public class AppService<TEntity, TDto> : AppService<TEntity, TDto, int>, IAppService<TEntity, TDto> where TDto : class where TEntity : class, IEntity
    {
        public AppService(IRepository<TEntity> repository, IUnitOfWork unitOfWork) : base(repository, unitOfWork)
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
        public Action<IBindingConfig<TDto, TEntity>> MapToEntityConfig = default;
        public Action<IBindingConfig<TEntity, TDto>> MapToDtoConfig = default;

        public virtual TEntity ToEntity(TDto dto)
        {
            return dto.MapTo(MapToEntityConfig);
        }

        public virtual TDto ToDto(TEntity entity)
        {
            return entity.MapTo(MapToDtoConfig);
        }

        public virtual List<TEntity> ToEntities(List<TDto> dtos)
        {
            if (MapToEntityConfig != default && !TinyMapper.BindingExists<TDto, TEntity>()) TinyMapper.Bind(MapToEntityConfig);
            return dtos.MapTo<List<TDto>, List<TEntity>>();
        }

        public virtual List<TDto> ToDtos(List<TEntity> entities)
        {
            if (MapToDtoConfig != default && !TinyMapper.BindingExists<TEntity, TDto>()) TinyMapper.Bind(MapToDtoConfig);
            return entities.MapTo<List<TEntity>, List<TDto>>();
        }

        #region Get
        public virtual TDto Get(TPrimaryKey id)
        {
            var entity = Repository.Get(id);
            return ToDto(entity);
        }

        public virtual async Task<TDto> GetAsync(TPrimaryKey id)
        {
            var entity = await Repository.GetAsync(id);
            return ToDto(entity);
        }
        public virtual List<TDto> GetAll()
        {
            var entities = Repository.GetAll();
            return ToDtos(entities);
        }
        public virtual async Task<List<TDto>> GetAllAsync()
        {
            var entities = await Repository.GetAllAsync();
            return ToDtos(entities);
        }
        public virtual List<TDto> GetAll(Expression<Func<TEntity, bool>> predicate)
        {
            var entities = Repository.GetAll(predicate);
            return ToDtos(entities);
        }
        public virtual async Task<List<TDto>> GetAllAsync(Expression<Func<TEntity, bool>> predicate)
        {
            var entities = await Repository.GetAllAsync(predicate);
            return ToDtos(entities);
        }
        public virtual TDto FirstOrDefault()
        {
            var entity = Repository.FirstOrDefault();
            return ToDto(entity);
        }
        public virtual async Task<TDto> FirstOrDefaultAsync()
        {
            var entity = await Repository.FirstOrDefaultAsync();
            return ToDto(entity);
        }
        public virtual TDto FirstOrDefault(Expression<Func<TEntity, bool>> predicate)
        {
            var entity = Repository.FirstOrDefault(predicate);
            return ToDto(entity);
        }
        public virtual async Task<TDto> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate)
        {
            var entity = await Repository.FirstOrDefaultAsync(predicate);
            return ToDto(entity);
        }
        public virtual TDto FirstOrDefaultNoTracking(Expression<Func<TEntity, bool>> predicate)
        {
            var entity = Repository.QueryNoTracking(predicate).FirstOrDefault();
            return ToDto(entity);
        }
        public virtual async Task<TDto> FirstOrDefaultNoTrackingAsync(Expression<Func<TEntity, bool>> predicate)
        {
            var entity = await Repository.QueryNoTracking(predicate).FirstOrDefaultAsync();
            return ToDto(entity);
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
        public virtual async Task<TDto> InsertAsync(TDto dto)
        {
            var entity = ToEntity(dto);
            await Repository.InsertAsync(entity);
            await _unitOfWork.SaveChangesAsync();
            return ToDto(entity);
        }
        public virtual void Insert(List<TDto> dtos)
        {
            var entities = ToEntities(dtos);
            Repository.Insert(entities);
            _unitOfWork.SaveChanges();
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
        public virtual async Task<TDto> UpdateAsync(TDto dto)
        {
            var entity = ToEntity(dto);
            await Repository.UpdateAsync(entity);
            await _unitOfWork.SaveChangesAsync();
            return ToDto(entity);
        }
        #endregion

        #region Delete
        public virtual void Delete(TPrimaryKey id)
        {
            Repository.Delete(id);
            _unitOfWork.SaveChanges();
        }
        public virtual async Task DeleteAsync(TPrimaryKey id)
        {
            await Repository.DeleteAsync(id);
            await _unitOfWork.SaveChangesAsync();
        }
        public virtual void Delete(TEntity entity)
        {
            Repository.Delete(entity);
            _unitOfWork.SaveChanges();
        }
        public virtual async Task DeleteAsync(TEntity entity)
        {
            await Repository.DeleteAsync(entity);
            await _unitOfWork.SaveChangesAsync();
        }
        public virtual void Delete(Expression<Func<TEntity, bool>> predicate)
        {
            Repository.Delete(predicate);
            _unitOfWork.SaveChanges();
        }
        public virtual async Task DeleteAsync(Expression<Func<TEntity, bool>> predicate)
        {
            await Repository.DeleteAsync(predicate);
            await _unitOfWork.SaveChangesAsync();
        }
        #endregion

        #region HardDelete
        public virtual void HardDelete(TPrimaryKey id)
        {
            Repository.HardDelete(id);
            _unitOfWork.SaveChanges();
        }
        public virtual async Task HardDeleteAsync(TPrimaryKey id)
        {
            await Repository.HardDeleteAsync(id);
            await _unitOfWork.SaveChangesAsync();
        }
        public virtual void HardDelete(TEntity entity)
        {
            Repository.HardDelete(entity);
            _unitOfWork.SaveChanges();
        }
        public virtual async Task HardDeleteAsync(TEntity entity)
        {
            await Repository.HardDeleteAsync(entity);
            await _unitOfWork.SaveChangesAsync();
        }
        public virtual void HardDelete(Expression<Func<TEntity, bool>> predicate)
        {
            Repository.HardDelete(predicate);
            _unitOfWork.SaveChanges();
        }
        public virtual async Task HardDeleteAsync(Expression<Func<TEntity, bool>> predicate)
        {
            await Repository.HardDeleteAsync(predicate);
            await _unitOfWork.SaveChangesAsync();
        }
        #endregion

        #region Aggregate
        public bool Any(Expression<Func<TEntity, bool>> predicate)
        {
            return Repository.Any(predicate);
        }

        public async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await Repository.AnyAsync(predicate);
        }

        public int Count()
        {
            return Repository.Count();
        }

        public async Task<int> CountAsync()
        {
            return await Repository.CountAsync();
        }

        public int Count(Expression<Func<TEntity, bool>> predicate)
        {
            return Repository.Count(predicate);
        }

        public async Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await Repository.CountAsync(predicate);
        }

        public long LongCount()
        {
            return Repository.LongCount();
        }

        public async Task<long> LongCountAsync()
        {
            return await Repository.LongCountAsync();
        }

        public long LongCount(Expression<Func<TEntity, bool>> predicate)
        {
            return Repository.LongCount(predicate);
        }

        public async Task<long> LongCountAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await Repository.LongCountAsync(predicate);
        }
        #endregion
    }

}
