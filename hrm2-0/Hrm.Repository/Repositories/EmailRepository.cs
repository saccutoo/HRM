using Hrm.Common.Dapper;
using Hrm.Repository.Entity;
using Hrm.Repository.Type;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Hrm.Common.Helpers;

namespace Hrm.Repository
{
    public partial class EmailRepository : CommonRepository, IEmailRepository
    {
        public HrmResultEntity<EmailEntity> GetEmail(BasicParamType param,bool isMailWelcomeKit)
        {
            var par = new DynamicParameters();
            par.Add("@IsMailWelcomeKit", isMailWelcomeKit);
            par.Add("@BaseParam", param.ToUserDefinedDataTable(), DbType.Object);
            var result = ListProcedure<EmailEntity>("Email_Get_GetEmail", par, dbName: param.DbName);
            return result;
        }
        public HrmResultEntity<EmailEntity> GetEmailById(long Id,string dbName)
        {
            var par = new DynamicParameters();
            par.Add("@Id", Id);
            par.Add("DbName", dbName);
            var result = ListProcedure<EmailEntity>("Email_Get_GetEmailById", par, dbName: dbName);
            return result;
        }
        public HrmResultEntity<bool> SaveEmailTemplate(EmailEntity email,List<AttachmentType> attachments, string dbName)
        {
            var par = new DynamicParameters();
            par.Add("@Id", email.Id);
            par.Add("@Name", email.Name);
            par.Add("@Title", email.Title);
            par.Add("@ContentTemplate", email.ContentTemplate);
            par.Add("@MailTo", email.MailTo);
            par.Add("@MailCc", email.MailCc);
            par.Add("@MailBcc", email.MailBcc);
            par.Add("@CreatedBy", email.CreatedBy);
            par.Add("@AttachmentType", attachments.ConvertToUserDefinedDataTable(), DbType.Object);
            par.Add("DbName", dbName);
            var result = Procedure("Email_Update_SaveEmailTemplate",par);
            return result;
        }
    }

}
