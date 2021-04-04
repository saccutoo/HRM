using Hrm.Repository.Entity;
using Hrm.Repository.Type;
using System.Collections.Generic;
namespace Hrm.Repository
{
    public partial interface IAttachmentRepository
    {
        HrmResultEntity<AttachmentEntity> GetAttackmenByRecordId(long recordId,string dataType, string dbName);
        HrmResultEntity<AttachmentEntity> SaveAttachment(AttachmentEntity attachment, string dbName);
        HrmResultEntity<AttachmentEntity> GetAttachmentByDataType(string dataType, string dbName);
        HrmResultEntity<AttachmentEntity> GetAttackmentById(long id, string dataType, string dbName);
        HrmResultEntity<AttachmentEntity> DeleteAttackmentById(long id, string dbName);
    }
}
