using Hrm.Repository.Entity;

namespace Hrm.Repository.Type
{
    public class AttachmentType: IUserDefinedType
    {
        public long Id { get; set; }
        public string FileName { get; set; }
        public string DisplayFileName { get; set; }
        public string FileExtension { get; set; }
        public float FileSize { get; set; }
        public long RecordId { get; set; }
    }
}
