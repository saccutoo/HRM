using Hrm.Repository.Entity;
using Hrm.Repository.Type;
using System.Collections.Generic;
namespace Hrm.Repository
{
    public partial interface IEmailRepository
    {
        HrmResultEntity<EmailEntity> GetEmail(BasicParamType param, bool isMailWelcomeKit);
        HrmResultEntity<EmailEntity> GetEmailById(long Id, string dbName);
        HrmResultEntity<bool> SaveEmailTemplate(EmailEntity email, List<AttachmentType> attachments, string dbName);
    }
}
