using Hrm.Common;
using Hrm.Repository;
using Hrm.Repository.Entity;
using Hrm.Repository.Type;
using Hrm.Service;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Hrm.Service
{
    public partial class EmailService : IEmailService
    {
        IEmailRepository _emailRepository;
        private string _dbName;
        public EmailService(IEmailRepository emailRepository)
        {
            _emailRepository = emailRepository;
            if (!string.IsNullOrEmpty(CurrentUser.DbName))
            {
                this._dbName = CurrentUser.DbName;
            }
        }
        public string GetEmail(BasicParamType param, bool isMailWelcomeKit)
        {
            var response = _emailRepository.GetEmail(param, isMailWelcomeKit);
            return JsonConvert.SerializeObject(response);
        }
        public string GetEmailById(long Id)
        {
            var response = _emailRepository.GetEmailById(Id, _dbName);
            return JsonConvert.SerializeObject(response);
        }       
        public string SaveEmailTemplate(EmailEntity email, List<AttachmentType> attachments)
        {
            var response = _emailRepository.SaveEmailTemplate(email, attachments, _dbName);
            return JsonConvert.SerializeObject(response);
        }

    }
}
