﻿using Hrm.Repository.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Repository.Type
{
    public class StringType : IUserDefinedType
    {
        public long OrderNo { get; set; }
        public string Value { get; set; }
    }
}