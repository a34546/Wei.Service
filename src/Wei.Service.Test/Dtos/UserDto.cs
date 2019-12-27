using System;
using System.Collections.Generic;
using System.Text;

namespace Wei.Service.Test
{
    public class UserDto
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Mobile { get; set; }
        public bool IsDelete { get; set; }
    }
}
