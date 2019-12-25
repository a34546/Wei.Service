using System;
using System.Collections.Generic;
using System.Text;

namespace Wei.Service
{
    public class TinyMapAttribute : Attribute
    {
        public Type TargetType { get; set; }
    }
}
