using System;
using System.Collections.Generic;
using System.Text;
using Wei.Repository;

namespace Wei.Service.Test
{
    public class UserAppService : AppService<User, UserDto>, IUserAppService
    {
        public UserAppService(IRepository<User> repository,
            IUnitOfWork unitOfWork) : base(repository, unitOfWork)
        {
        }
    }

    public interface IUserAppService : IAppService<User, UserDto>
    {

    }
}
