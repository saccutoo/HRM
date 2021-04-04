using System;
using Hrm.Repository.Entity;
using Hrm.Repository.Type;
using System.Collections.Generic;

namespace Hrm.Service
{
    public partial interface IAttachmentService : IBaseService
    {
        string GetAttackmenByRecordId(long recordId, string dataType);
        string SaveAttachment(AttachmentEntity attachment);
        string GetAttachmentByDataType(string dataType);
        string GetAttackmentById(long id, string dataType);
        string DeleteAttackmentById(long id);
    }
}
