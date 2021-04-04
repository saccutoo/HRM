using System;
using Hrm.Repository.Entity;
using Hrm.Repository.Type;
using System.Collections.Generic;

namespace Hrm.Service
{
    public partial interface IEmailService : IBaseService
    {
        string GetEmail(BasicParamType param, bool isMailWelcomeKit);
        string GetEmailById(long Id);
        string SaveEmailTemplate(EmailEntity email, List<AttachmentType> attachments);
    }
}
