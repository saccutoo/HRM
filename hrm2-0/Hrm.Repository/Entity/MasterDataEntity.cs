using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Repository.Entity
{
    public class MasterDataEntity
    {
        public long Id { get; set; }
        public long GroupId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string GroupName { get; set; }
        public string Value { get; set; }
        public string Description { get; set; }
        public long OrderNo { get; set; }
        public long LanguageId { get; set; }
        public long IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public string Color { get; set; }
        public long CreatedBy { get; set; }
        public string DataType { get; set; }
        public long LocalizedId { get; set; }
        public bool IsDisbleEditing { get; set; }

    }
}
