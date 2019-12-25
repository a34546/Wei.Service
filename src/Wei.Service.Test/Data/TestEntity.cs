using System;
using System.Collections.Generic;
using System.Text;
using Wei.Repository;

namespace Wei.Service.Test
{
    public class TestEntity : Entity<long>
    {
        public string TestMethodName { get; set; }
        public string TestResult { get; set; }
    }
}
