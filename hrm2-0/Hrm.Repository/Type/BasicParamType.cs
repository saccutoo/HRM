using Hrm.Repository.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Repository.Type
{
    public class BasicParamType : IUserDefinedType
    {
        public string FilterField { get; set; } = string.Empty;
        public string OrderBy { get; set; } = "Id desc";
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = int.MaxValue;
        public long UserId { get; set; } = 1;
        public long RoleId { get; set; } = 1;
        public string DbName { get; set; } = "System";
        public long LanguageId { get; set; } = 1;
    }
}
