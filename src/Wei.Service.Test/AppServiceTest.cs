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
        /// 自定义AppService
        /// </summary>
        readonly ITestTable1AppService _testTable1AppService;

        /// <summary>
        /// 泛型AppService
        /// </summary>
        readonly IAppService<TestTable2, TestTable2Dto> _testEntity2Service;
        public AppServiceTest()
        {
            var services = new ServiceCollection();
            services.AddRepository(opt =>
            {
                opt.UseMySql("server = 127.0.0.1;database=demo;uid=root;password=root;");
            });
            services.AddAppService();
            _serviceProvider = services.BuildServiceProvider();
            _testTable1AppService = _serviceProvider.GetRequiredService<ITestTable1AppService>();
            _testEntity2Service = _serviceProvider.GetRequiredService<IAppService<TestTable2, TestTable2Dto>>();


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
           

        }

        [Fact]
        public void GetAllList()
        {
            
        }

        [Fact]
        public void Insert()
        {
            
        }

    }

}
