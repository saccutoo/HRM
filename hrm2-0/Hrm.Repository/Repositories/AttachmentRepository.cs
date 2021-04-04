using Hrm.Common.Dapper;
using Hrm.Repository.Entity;
using Hrm.Repository.Type;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Hrm.Common.Helpers;

namespace Hrm.Repository
{
    public partial class AttachmentRepository : CommonRepository, IAttachmentRepository
    {
        public HrmResultEntity<AttachmentEntity> GetAttackmenByRecordId(long recordId,string dataType, string dbName)
        {
            var par = new DynamicParameters();
            par.Add("@RecordId", recordId);
            par.Add("@DataType", dataType);
            par.Add("DbName", dbName);
            var result = ListProcedure<AttachmentEntity>("Attachment_Get_GetAttackmenByRecordId", par, dbName: dbName);
            return result;
        }
        public HrmResultEntity<AttachmentEntity> SaveAttachment(AttachmentEntity attachment, string dbName)
        {
            var par = new DynamicParameters();
            par.Add("@Id", attachment.Id);
            par.Add("@FileName", attachment.FileName);
            par.Add("@DisplayFileName", attachment.DisplayFileName);
            par.Add("@FileExtension", attachment.FileExtension);
            par.Add("@FileSize", attachment.FileSize);
            par.Add("@Name", attachment.Name);
            par.Add("@Description", attachment.Description);
            par.Add("@RecordId", attachment.RecordId);
            par.Add("@DataType", attachment.DataType);
            par.Add("DbName", dbName);
            var result = ListProcedure<AttachmentEntity>("Attachment_Update_SaveAttachment", par, dbName: dbName);
            return result;
        }
        public HrmResultEntity<AttachmentEntity> GetAttachmentByDataType(string dataType, string dbName)
        {
            var par = new DynamicParameters();
            par.Add("@DataType", dataType);
            par.Add("DbName", dbName);
            var result = ListProcedure<AttachmentEntity>("Attachment_Get_GetAttackmentByDataType", par, dbName: dbName);
            return result;
        }
        public HrmResultEntity<AttachmentEntity> GetAttackmentById(long id, string dataType, string dbName)
        {
            var par = new DynamicParameters();
            par.Add("@Id", id);
            par.Add("@DataType", dataType);
            par.Add("DbName", dbName);
            var result = ListProcedure<AttachmentEntity>("Attachment_Get_GetAttackmentById", par, dbName: dbName);
            return result;
        }

        public HrmResultEntity<AttachmentEntity> DeleteAttackmentById(long id, string dbName)
        {
            var par = new DynamicParameters();
            par.Add("@Id", id);
            par.Add("DbName", dbName);
            var result = ListProcedure<AttachmentEntity>("Attachment_Del_DeleteAttackmentById", par, dbName: dbName);
            return result;
        }
    }

}
