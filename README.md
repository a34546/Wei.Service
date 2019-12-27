# Wei.Service
基于Wei.Repository,封装的AppService，提供基本的CURD操作,也可以继承AppService，重写CURD操作，实现Entity>Dto自动映射

## 快速开始

> Nuget引用包：Wei.Service


1. 实体对象需要继承Entity
```cs
public class User : Entity
{
public string UserName { get; set; }
public string Password { get; set; }
public string Mobile { get; set; }
}
```
2. 【可选】继承BaseDbContext,如果不需要DbContext，可以忽略该步骤
```cs
public class DemoDbContext : DbContext
{
    public DemoDbContext(DbContextOptions options) : base(options)
    {
    }
}
```
3. 注入服务,修改Startup.cs,添加AddRepository,AddAppService
```cs
 public void ConfigureServices(IServiceCollection services)
{
	services.AddRepository<UserDbContext>(ops =>
	{
		ops.UseMySql("server = 127.0.0.1;database=demo;uid=root;password=root;");
	});
   services.AddAppService();

   services.AddControllers();
}
```
4.  【可选】如果不用泛型Repository注入,可以自定义Repository,需要继承Repository,IRepository,可以重写基类CURD方法
```cs
public class UserAppService : AppService<User, UserDto>, IUserAppService
{
	public UserAppService(IRepository<User> repository, IUnitOfWork unitOfWork) : base(repository, unitOfWork)
	{
		//EntityToDto
		base.MapToDtoConfig = config =>
		{
			config.Ignore(x => x.Password);//忽略Password映射，
			config.Bind(x => x.UserName, x => x.Name);//Name映射成UserName
		};

		//DtoToEntity, Name映射成UserName
		base.MapToEntityConfig = config => config.Bind(x => x.Name, x => x.UserName);
	}

}
public interface IUserAppService : IAppService<User, UserDto>
{

}
```
4.  在Controller中使用
```cs
public class UserController : ControllerBase
{
	/// <summary>
	/// 泛型注入
	/// </summary>
	readonly IAppService<User, UserDto> _userService;

	/// <summary>
	/// 自定义UserRepository
	/// </summary>
	readonly IUserAppService _userAppService;
	public UserController(IAppService<User, UserDto> userService,
		IUserAppService userAppService)
	{
		_userService = userService;
		_userAppService = userAppService;
	}

	[HttpGet("GetAll")]
	public async Task<List<UserDto>> GetAll()
	{
		var users = await _userAppService.GetAllAsync();
		return users;
	}
}
```

## 详细介绍
**1. Wei.Repository 文档**
	> [Wei.Repository](https://github.com/a34546/Wei.Repository/blob/master/README.md)

**2. IAppService接口**
```cs
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
```


