using System;
using System.Collections.Generic;
using System.Text;
using Wei.Repository;

namespace Wei.Service.Test
{
    public class TestEntityAppService : AppService<TestEntity, TestEntityDto, long>, ITestEntityAppService
    {
        public TestEntityAppService(IRepository<TestEntity, long> repository,
            IUnitOfWork unitOfWork) : base(repository, unitOfWork)
        {
        }

        public override TestEntityDto GetById(long id)
        {
            return null;
        }
    }

    public interface ITestEntityAppService : IAppService<TestEntity, TestEntityDto, long>
    {

    }
}
