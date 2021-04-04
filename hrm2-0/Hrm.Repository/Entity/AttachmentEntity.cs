using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Repository.Entity
{
    public class AttachmentEntity:BaseEntity
    {
        public string FileName { get; set; }
        public string DisplayFileName { get; set; }
        public string FileExtension { get; set; }
        public float FileSize { get; set; }
        public int RecordId { get; set; }
        public string DataType { get; set; }
        public string Description { get; set; }
    }
}
