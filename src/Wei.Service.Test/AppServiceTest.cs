using Dapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Nelibur.ObjectMapper;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Wei.Repository;
using Xunit;
using Xunit.Extensions.Ordering;

namespace Wei.Service.Test
{
    public class AppServiceTest
    {
        readonly ServiceProvider _serviceProvider;

        /// <summary>
        /// 自定义AppService
        /// </summary>
        readonly IUserAppService _userAppService;

        /// <summary>
        /// 泛型AppService
        /// </summary>
        readonly IAppService<User, UserDto> _userService;
        public AppServiceTest()
        {
            var services = new ServiceCollection();
            services.AddRepository(opt =>
            {
                opt.UseMySql("server = 127.0.0.1;database=demo;uid=root;password=root;");
            });
            services.AddAppService();
            _serviceProvider = services.BuildServiceProvider();
            _userAppService = _serviceProvider.GetRequiredService<IUserAppService>();
            _userService = _serviceProvider.GetRequiredService<IAppService<User, UserDto>>();
            InitUserTable();
        }

        private void InitUserTable()
        {
            using var scope = _serviceProvider.CreateScope();
            var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
            using var conn = unitOfWork.GetConnection();
            var userTable = conn.QueryFirstOrDefault<string>("SHOW TABLES LIKE 'User';");
            if (!"User".Equals(userTable, StringComparison.CurrentCultureIgnoreCase))
            {
                conn.Execute(@"
                            CREATE TABLE `user` (
                              `Id` int(11) NOT NULL AUTO_INCREMENT,
                              `CreateTime` datetime(6) NOT NULL,
                              `UpdateTime` datetime(6) DEFAULT NULL,
                              `IsDelete` tinyint(1) NOT NULL,
                              `DeleteTime` datetime(6) DEFAULT NULL,
                              `UserName` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL,
                              `Password` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL,
                              `Mobile` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL,
                              PRIMARY KEY (`Id`)
                            ) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
                    ");
            }
        }




        #region Insert

        [Fact, Order(1)]
        public void Insert()
        {
            var user = _userAppService.Insert(new UserDto { UserName = "AppService_Insert" });
            Assert.True(user.Id > 0);
        }

        [Fact, Order(1)]
        public async Task InsertAsync()
        {
            var user = await _userAppService.InsertAsync(new UserDto { UserName = "AppService_InsertAsync" });
            Assert.True(user.Id > 0);
        }

        #endregion Insert

        #region Update

        [Fact, Order(2)]
        public void Update()
        {
            var dto = _userAppService.FirstOrDefaultNoTracking(x => x.UserName == "AppService_Insert");
            dto.Mobile = "13012341234";
            dto = _userAppService.Update(dto);
            dto = _userAppService.Get(dto.Id);
            Assert.NotNull(dto.Mobile);
        }

        [Fact, Order(2)]
        public async Task UpdateAsync()
        {
            var dto = await _userAppService.FirstOrDefaultNoTrackingAsync(x => x.UserName == "AppService_InsertAsync");
            dto.Mobile = "13012341234";
            dto = await _userAppService.UpdateAsync(dto);
            dto = await _userAppService.GetAsync(dto.Id);
            Assert.NotNull(dto.Mobile);
        }

        #endregion Update

        #region Delete

        [Fact, Order(3)]
        public void Delete()
        {
            var dto = _userAppService.FirstOrDefaultNoTracking(x => x.UserName == "AppService_Insert");
            _userAppService.Delete(dto.MapTo<UserDto, User>());
            dto = _userAppService.Get(dto.Id);
            Assert.True(dto.IsDelete);
        }

        [Fact, Order(3)]
        public async Task DeleteAsync()
        {
            var dto = await _userAppService.FirstOrDefaultNoTrackingAsync(x => x.UserName == "AppService_InsertAsync");
            await _userAppService.DeleteAsync(dto.MapTo<UserDto, User>());
            dto = await _userAppService.GetAsync(dto.Id);
            Assert.True(dto.IsDelete);
        }

        [Fact, Order(3)]
        public void DeleteById()
        {
            var dto = _userAppService.Insert(new UserDto { UserName = "AppService_DeleteById_Insert" });
            _userAppService.Delete(dto.Id);
            dto = _userAppService.Get(dto.Id);
            Assert.True(dto.IsDelete);
        }

        [Fact, Order(3)]
        public async Task DeleteByIdAsync()
        {
            var dto = await _userAppService.InsertAsync(new UserDto { UserName = "AppService_DeleteById_Insert" });
            await _userAppService.DeleteAsync(dto.Id);
            dto = await _userAppService.GetAsync(dto.Id);
            Assert.True(dto.IsDelete);
        }

        [Fact, Order(3)]
        public void DeleteBy()
        {
            var dto = _userAppService.Insert(new UserDto { UserName = "AppService_DeleteBy_Insert" });
            _userAppService.Delete(x => x.Id == dto.Id);
            dto = _userAppService.Get(dto.Id);
            Assert.True(dto.IsDelete);
        }

        [Fact, Order(3)]
        public async Task DeleteByAsync()
        {
            var dto = await _userAppService.InsertAsync(new UserDto { UserName = "AppService_DeleteByAsync_Insert" });
            await _userAppService.DeleteAsync(x => x.Id == dto.Id);
            dto = await _userAppService.GetAsync(dto.Id);
            Assert.True(dto.IsDelete);
        }

        #endregion

        #region HardDelete

        [Fact, Order(4)]
        public void HardDelete()
        {
            var dto = _userAppService.FirstOrDefaultNoTracking(x => x.UserName == "AppService_Insert");
            _userAppService.HardDelete(dto.MapTo<UserDto, User>());
            dto = _userAppService.Get(dto.Id);
            Assert.Null(dto);
        }

        [Fact, Order(4)]
        public async Task HardDeleteAsync()
        {
            var dto = await _userAppService.FirstOrDefaultNoTrackingAsync(x => x.UserName == "AppService_InsertAsync");
            await _userAppService.HardDeleteAsync(dto.MapTo<UserDto, User>());
            dto = await _userAppService.GetAsync(dto.Id);
            Assert.Null(dto);
        }

        [Fact, Order(4)]
        public void HardDeleteById()
        {
            var dto = _userAppService.Insert(new UserDto { UserName = "AppService_HardDeleteById_Insert" });
            _userAppService.HardDelete(dto.Id);
            dto = _userAppService.Get(dto.Id);
            Assert.Null(dto);
        }

        [Fact, Order(4)]
        public async Task HardDeleteByIdAsync()
        {
            var dto = await _userAppService.InsertAsync(new UserDto { UserName = "AppService_HardDeleteById_Insert" });
            await _userAppService.HardDeleteAsync(dto.Id);
            dto = await _userAppService.GetAsync(dto.Id);
            Assert.Null(dto);
        }

        [Fact, Order(4)]
        public void HardDeleteBy()
        {
            var dto = _userAppService.Insert(new UserDto { UserName = "AppService_HardDeleteBy_Insert" });
            _userAppService.HardDelete(x => x.Id == dto.Id);
            dto = _userAppService.Get(dto.Id);
            Assert.Null(dto);
        }

        [Fact, Order(4)]
        public async Task HardDeleteByAsync()
        {
            var dto = await _userAppService.InsertAsync(new UserDto { UserName = "AppService_HardDeleteByAsync_Insert" });
            await _userAppService.HardDeleteAsync(x => x.Id == dto.Id);
            dto = await _userAppService.GetAsync(dto.Id);
            Assert.Null(dto);
        }

        #endregion

        #region Aggregate
        [Fact, Order(5)]
        public void Any()
        {
            var isDelete = _userAppService.Any(x => x.IsDelete);
            Assert.True(isDelete);
        }

        [Fact, Order(5)]
        public async Task AnyAsync()
        {
            var isDelete = await _userAppService.AnyAsync(x => x.IsDelete);
            Assert.True(isDelete);
        }

        [Fact, Order(5)]
        public void Count()
        {
            var deleteCount = _userAppService.Count();
            Assert.True(deleteCount > 0);
        }

        [Fact, Order(5)]
        public async Task CountAsync()
        {
            var deleteCount = await _userAppService.CountAsync();
            Assert.True(deleteCount > 0);
        }

        [Fact, Order(5)]
        public void CountBy()
        {
            var deleteCount = _userAppService.Count(x => x.IsDelete);
            Assert.True(deleteCount > 0);
        }

        [Fact, Order(5)]
        public async Task CountByAsync()
        {
            var deleteCount = await _userAppService.CountAsync(x => x.IsDelete);
            Assert.True(deleteCount > 0);
        }
        [Fact, Order(5)]
        public void LongCount()
        {
            var deleteCount = _userAppService.LongCount();
            Assert.True(deleteCount > 0);
        }

        [Fact, Order(5)]
        public async Task LongCountAsync()
        {
            var deleteCount = await _userAppService.LongCountAsync();
            Assert.True(deleteCount > 0);
        }

        [Fact, Order(5)]
        public void LongCountBy()
        {
            var deleteCount = _userAppService.Count(x => x.IsDelete);
            Assert.True(deleteCount > 0);
        }

        [Fact, Order(5)]
        public async Task LongCountByAsync()
        {
            var deleteCount = await _userAppService.CountAsync(x => x.IsDelete);
            Assert.True(deleteCount > 0);
        }

        #endregion
    }

}
