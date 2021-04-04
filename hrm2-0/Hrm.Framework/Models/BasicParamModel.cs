using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hrm.Framework.Models
{
    public class BasicParamModel
    {
        public string FilterField { get; set; } = string.Empty;
        public string OrderBy { get; set; } = "Id desc";
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = int.MaxValue;
        public long UserId { get; set; } = 1;
        public long RoleId { get; set; } = 1;
        public string DbName { get; set; } = "System";
        public long LanguageId { get; set; } = 1;
        public long ReferenceId { get; set; }
        public int GroupId { get; set; } = 0;
        public string StringJson { get; set; } = string.Empty;
    }
}