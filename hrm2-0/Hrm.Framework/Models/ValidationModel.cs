﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Framework.Models
{
    public class ValidationModel
    {
        public string ColumnName { get; set; }
        public bool IsError { get; set; }
        public string ErrorMessage { get; set; }
    }
}
