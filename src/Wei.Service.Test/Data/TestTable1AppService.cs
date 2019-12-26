using System;
using System.Collections.Generic;
using System.Text;
using Wei.Repository;

namespace Wei.Service.Test
{
    public class TestTable1AppService : AppService<TestTable1, TestTable1Dto>, ITestTable1AppService
    {
        public TestTable1AppService(IRepository<TestTable1> repository,
            IUnitOfWork unitOfWork) : base(repository, unitOfWork)
        {
        }
    }

    public interface ITestTable1AppService : IAppService<TestTable1, TestTable1Dto>
    {

    }
}
