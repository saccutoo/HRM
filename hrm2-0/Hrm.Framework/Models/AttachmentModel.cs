using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Framework.Models
{
    public class AttachmentModel:BaseModel
    {
        public string Label { get; set; }
        public string FileName { get; set; }
        public string DisplayFileName { get; set; }
        public string FileExtension { get; set; }
        public float FileSize { get; set; }
        public long RecordId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

    }
}
