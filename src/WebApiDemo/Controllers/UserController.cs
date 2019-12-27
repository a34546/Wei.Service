using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Nelibur.ObjectMapper;
using WebApiDemo.Data;
using Wei.Repository;
using Wei.Service;

namespace WebApiDemo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        readonly IAppService<User, UserDto> _userService;
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
            if (!users.Any())
            {
                _userService.Insert(new List<UserDto>
                {
                    new UserDto { Name = "demo1", Password = "pwd1", Mobile = "13012341234" },
                    new UserDto { Name = "demo2", Password = "pwd2", Mobile = "13066668888" }
                });
                users = await _userAppService.GetAllAsync();
            }
            return users;
        }

        [HttpGet("Add")]
        public async Task<UserDto> Add()
        {
            var ticks = DateTime.Now.Ticks.ToString();
            var dto = await _userAppService.InsertAsync(new UserDto { Name = ticks, Password = ticks, Mobile = "13012341234" });
            return dto;
        }

        [HttpGet("Get")]
        public async Task<UserDto> Get()
        {
            var user = await _userService.FirstOrDefaultAsync();
            return user;
        }
    }
}
