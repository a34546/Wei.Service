using Nelibur.ObjectMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wei.Repository;
using Wei.Service;

namespace WebApiDemo.Data
{
    public class UserAppService : AppService<User, UserDto>, IUserAppService
    {
        public UserAppService(IRepository<User> repository, IUnitOfWork unitOfWork) : base(repository, unitOfWork)
        {
            //EntityToDto, 忽略Password映射，Name映射成UserName
            base.MapToDtoConfig = config =>
            {
                config.Ignore(x => x.Password);
                config.Bind(x => x.UserName, x => x.Name);
            };

            //DtoToEntity, Name映射成UserName
            base.MapToEntityConfig = config => config.Bind(x => x.Name, x => x.UserName);
        }

    }
    public interface IUserAppService : IAppService<User, UserDto>
    {

    }
}
