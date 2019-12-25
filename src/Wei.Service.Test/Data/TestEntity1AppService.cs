using System;
using System.Collections.Generic;
using System.Text;
using Wei.Repository;

namespace Wei.Service.Test
{
    public class TestEntity1AppService : AppService<TestEntity1, TestEntity1Dto>, ITestEntity1AppService
    {
        public TestEntity1AppService(IRepository<TestEntity1> repository,
            IUnitOfWork unitOfWork) : base(repository, unitOfWork)
        {
        }

        public override TestEntity1Dto ToDto(TestEntity1 entity)
        {
            return entity.MapTo<TestEntity1, TestEntity1Dto>(config =>
            {
                config.Ignore(x => x.Id);
                config.Bind(x => x.TestMethodName, x => x.Name);
            });
        }
    }

    public interface ITestEntity1AppService : IAppService<TestEntity1, TestEntity1Dto>
    {

    }
}
