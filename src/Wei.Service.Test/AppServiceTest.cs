using Dapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Nelibur.ObjectMapper;
using System;
using System.Collections.Generic;
using Wei.Repository;
using Xunit;

namespace Wei.Service.Test
{
    public class AppServiceTest
    {
        readonly ServiceProvider _serviceProvider;

        /// <summary>
        /// 泛型AppService
        /// </summary>
        readonly IAppService<TestEntity, TestEntityDto, long> _testEntityService;
        readonly IAppService<TestEntity2, TestEntity2Dto> _testEntity2Service;

        /// <summary>
        /// 自定义AppService
        /// </summary>
        readonly ITestEntityAppService _testEntityAppService;
        readonly ITestEntity1AppService _testEntity1Service;
        public AppServiceTest()
        {
            var services = new ServiceCollection();
            services.AddRepository(opt =>
            {
                opt.UseMySql("server = 127.0.0.1;database=demo;uid=root;password=root;");
            });
            services.AddAppService();
            _serviceProvider = services.BuildServiceProvider();
            _testEntityService = _serviceProvider.GetRequiredService<IAppService<TestEntity, TestEntityDto, long>>();
            _testEntity2Service = _serviceProvider.GetRequiredService<IAppService<TestEntity2, TestEntity2Dto>>();

            _testEntityAppService = _serviceProvider.GetRequiredService<ITestEntityAppService>();
            _testEntity1Service = _serviceProvider.GetRequiredService<ITestEntity1AppService>();

            InitTestEntity();
        }

        private void InitTestEntity()
        {
            var unitOfWork = _serviceProvider.GetRequiredService<IUnitOfWork>();

            var conn = unitOfWork.GetConnection();
            var testTable = conn.QueryFirstOrDefault<string>("SHOW TABLES LIKE 'TestEntity';");
            if (!"testentity".Equals(testTable, StringComparison.CurrentCultureIgnoreCase))
            {
                conn.Execute(@"
                             CREATE TABLE `testentity` (
                              `Id` int(11) NOT NULL AUTO_INCREMENT,
                              `TestMethodName` varchar(200)  DEFAULT NULL,
                              `TestResult` varchar(200)  DEFAULT NULL,
                              `CreateTime` datetime(6) NOT NULL,
                              `UpdateTime` datetime(6) DEFAULT NULL,
                              `IsDelete` bit(1) NOT NULL,
                              `DeleteTime` datetime(6) DEFAULT NULL,
                              PRIMARY KEY (`Id`)
                            ) ENGINE=InnoDB AUTO_INCREMENT=15 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
                    ");
            }
            var testTable1 = conn.QueryFirstOrDefault<string>("SHOW TABLES LIKE 'TestEntity1';");
            if (!"testentity1".Equals(testTable1, StringComparison.CurrentCultureIgnoreCase))
            {
                conn.Execute(@"
                             CREATE TABLE `testentity1` (
                              `Id` int(11) NOT NULL AUTO_INCREMENT,
                              `TestMethodName` varchar(200)  DEFAULT NULL,
                              `TestResult` varchar(200)  DEFAULT NULL,
                              `CreateTime` datetime(6) NOT NULL,
                              `UpdateTime` datetime(6) DEFAULT NULL,
                              `IsDelete` bit(1) NOT NULL,
                              `DeleteTime` datetime(6) DEFAULT NULL,
                              PRIMARY KEY (`Id`)
                            ) ENGINE=InnoDB AUTO_INCREMENT=15 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
                    ");
            }
            var testTable2 = conn.QueryFirstOrDefault<string>("SHOW TABLES LIKE 'TestEntity2';");
            if (!"testentity2".Equals(testTable2, StringComparison.CurrentCultureIgnoreCase))
            {
                conn.Execute(@"
                             CREATE TABLE `testentity2` (
                              `Id` int(11) NOT NULL AUTO_INCREMENT,
                              `TestMethodName` varchar(200)  DEFAULT NULL,
                              `TestResult` varchar(200)  DEFAULT NULL,
                              `CreateTime` datetime(6) NOT NULL,
                              `UpdateTime` datetime(6) DEFAULT NULL,
                              `IsDelete` bit(1) NOT NULL,
                              `DeleteTime` datetime(6) DEFAULT NULL,
                              PRIMARY KEY (`Id`)
                            ) ENGINE=InnoDB AUTO_INCREMENT=15 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
                    ");
            }
        }


        [Fact]
        public void GetById()
        {
            var entity = _testEntityService.GetById(1);
            Assert.Equal(1, entity.Id);

            //重写ToDto方法，忽略Id映射，TestMethodName映射为Name
            var entity1 = _testEntity1Service.GetById(1);
            Assert.Equal(0, entity1.Id);
            Assert.NotNull(entity1.Name);


            var entity2 = _testEntity2Service.GetById(1);
            Assert.Equal(1, entity2.Id);

            //调用重写的GetById方法,返回null
            var entity3 = _testEntityAppService.GetById(1);
            Assert.Null(entity3);

        }

        [Fact]
        public void GetAllList()
        {
            var dtos = _testEntityService.GetAllList();
            var dto2s = _testEntity2Service.GetAllList();
            Assert.NotNull(dtos);
            Assert.NotNull(dto2s);
        }

        [Fact]
        public void Insert()
        {
            var dto = new TestEntityDto { TestMethodName = "Insert", TestResult = "Insert success" };
            _testEntityService.Insert(dto);
            Assert.True(dto.Id > 0);
        }

    }

}
