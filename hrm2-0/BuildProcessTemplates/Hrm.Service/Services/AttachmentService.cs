using Hrm.Common;
using Hrm.Repository;
using Hrm.Repository.Entity;
using Hrm.Repository.Type;
using Hrm.Service;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Hrm.Service
{
    public partial class AttachmentService : IAttachmentService
    {
        IAttachmentRepository _attachmentRepository;
        private string _dbName;
        public AttachmentService(IAttachmentRepository attachmentRepository)
        {
            _attachmentRepository = attachmentRepository;
            if (!string.IsNullOrEmpty(CurrentUser.DbName))
            {
                this._dbName = CurrentUser.DbName;
            }
        }      
        public string GetAttackmenByRecordId(long recordId, string dataType)
        {
            var response = _attachmentRepository.GetAttackmenByRecordId(recordId, dataType, _dbName);
            return JsonConvert.SerializeObject(response);
        }
        public string SaveAttachment(AttachmentEntity attachment)
        {
            var response = _attachmentRepository.SaveAttachment(attachment, _dbName);
            return JsonConvert.SerializeObject(response);
        }
        public string GetAttachmentByDataType(string dataType)
        {
            var response = _attachmentRepository.GetAttachmentByDataType(dataType, _dbName);
            return JsonConvert.SerializeObject(response);
        }
        public string GetAttackmentById(long id,string dataType)
        {
            var response = _attachmentRepository.GetAttackmentById(id,dataType, _dbName);
            return JsonConvert.SerializeObject(response);
        }
        public string DeleteAttackmentById(long id)
        {
            var response = _attachmentRepository.DeleteAttackmentById(id, _dbName);
            return JsonConvert.SerializeObject(response);
        }

    }
}
