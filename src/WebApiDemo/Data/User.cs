
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Wei.Repository;

namespace WebApiDemo.Data
{
    public class User : Entity
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Mobile { get; set; }


    }

    public class UserDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string Mobile { get; set; }
    }
}
