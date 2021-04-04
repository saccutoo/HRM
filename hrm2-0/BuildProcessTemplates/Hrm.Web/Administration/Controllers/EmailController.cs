using Hrm.Admin.ViewModels;
using Hrm.Common;
using Hrm.Common.Helpers;
using Hrm.Framework.Controllers;
using Hrm.Framework.Helper;
using Hrm.Framework.Helpers;
using Hrm.Framework.Models;
using Hrm.Repository.Entity;
using Hrm.Repository.Type;
using Hrm.Service;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
namespace Hrm.Admin.Controllers
{
    public partial class EmailController : BaseController
    {
        #region Fields
        private IEmailService _emailService;
        private ILocalizationService _localizationService;
        private IAttachmentService _attachmentService;

        private long _languageId;
        #endregion Fields
        #region Constructors
        public EmailController(IEmailService emailService, ILocalizationService localizationService, IAttachmentService attachmentService)
        {
            _emailService = emailService;
            _attachmentService = attachmentService;
            _localizationService = localizationService;
            _languageId = CurrentUser.LanguageId;
        }
        #endregion
        public ActionResult Index()
        {
            EmailViewModel email = new EmailViewModel();
            email.ListEmail = new List<EmailModel>();
            long id = 0;
            email.EmailDetail = new EmailDetailViewModel();
            email.EmailDetail.Email = new EmailModel();
            email.EmailDetail.Attachments = new List<AttachmentModel>();
            var param = new BasicParamModel()
            {
                FilterField = string.Empty,
                PageNumber = 1,
                LanguageId = _languageId,
                DbName = CurrentUser.DbName
            };
            //Gọi hàm lấy thông tin nhân viên
            var paramEntity = MapperHelper.Map<BasicParamModel, BasicParamType>(param);
            var respone = _emailService.GetEmail(paramEntity,false);
            if (respone!=null)
            {
                var result = JsonConvert.DeserializeObject<HrmResultModel<EmailModel>>(respone);
                if (!CheckPermission(result))
                {
                    //return to Access Denied
                }
                else
                {
                    if (result.Results.Count()>0)
                    {
                        email.ListEmail = result.Results;
                        id = result.Results.FirstOrDefault().Id;
                    }
                }
            }

            var responseEmail = _emailService.GetEmailById(id);
            if (responseEmail!=null)
            {
                var resultEmail= JsonConvert.DeserializeObject<HrmResultModel<EmailModel>>(respone);
                if (!CheckPermission(resultEmail))
                {
                    //return to Access Denied
                }
                else
                {
                    if (resultEmail.Results.Count() > 0)
                    {
                        email.EmailDetail.Email = resultEmail.Results.FirstOrDefault();
                    }
                }
            }
            var responseAttackment = _attachmentService.GetAttackmenByRecordId(id,DataType.Email);
            if (responseEmail != null)
            {
                var resultAttackment = JsonConvert.DeserializeObject<HrmResultModel<AttachmentModel>>(responseAttackment);
                if (!CheckPermission(resultAttackment))
                {
                    //return to Access Denied
                }
                else
                {
                    if (resultAttackment.Results.Count() > 0)
                    {
                        email.EmailDetail.Attachments = resultAttackment.Results;
                    }
                }
            }
            return View(email);
        }

        public ActionResult ReloadEmailDetail(long id)
        {
            EmailDetailViewModel emailDetail = new EmailDetailViewModel();
            emailDetail.Email = new EmailModel();
            emailDetail.Attachments = new List<AttachmentModel>();
            var responseEmail = _emailService.GetEmailById(id);
            if (responseEmail!=null)
            {
                var resultEmail = JsonConvert.DeserializeObject<HrmResultModel<EmailModel>>(responseEmail);
                if (!CheckPermission(resultEmail))
                {
                    //return to Access Denied
                }
                else {
                    emailDetail.Email = resultEmail.Results.FirstOrDefault();
                }
            }
            var responseAttachment = _attachmentService.GetAttackmenByRecordId(id, DataType.Email);
            if (responseAttachment!=null)
            {
                var resultAttachment = JsonConvert.DeserializeObject<HrmResultModel<AttachmentModel>>(responseAttachment);
                if (!CheckPermission(resultAttachment))
                {
                    //return to Access Denied
                }
                else
                {
                    emailDetail.Attachments = resultAttachment.Results;
                }
            }

            return PartialView("~/Administration/Views/Email/_EmailDetail.cshtml", emailDetail);

        }
        public ActionResult ReloadEmailAttachment(List<AttachmentJs> atchments )
        {
            
            List<AttachmentModel> attachments = new List<AttachmentModel>();
            if (atchments!=null && atchments.Count()>0)
            {
                foreach (var item in atchments)
                {
                    attachments.Add(new AttachmentModel()
                    {
                        FileName = item.Name,
                        DisplayFileName = item.Realname,
                        FileSize = item.Size,
                        FileExtension = item.Extension
                    });
                }
            }         
            return PartialView("~/Administration/Views/Email/_ListAttackment.cshtml", attachments);

        }
        public ActionResult Save(IEnumerable<HttpPostedFileBase> attachment)
        {
            var result = UploadFileHelper.GetFileUpload(attachment);
            return Json(new { Result = result, IsSuccess= result.Count() > 0, Invalid = true }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Remove(string realName)
        {
            return Json(new {IsSuccess = UploadFileHelper.RemoveFileUpload(realName), Invalid = true }, JsonRequestBehavior.AllowGet);
        }

        [ValidateInput(false)]
        public ActionResult SaveTemplateEmail(EmailModel model, List<AttachmentJs> attachments, List<AttachmentJs> deleteAttachments)
        {
            List<AttachmentModel> attachmentsModel = new List<AttachmentModel>();
            if (model != null)
            {
                var validations = ValidationHelper.Validation(model, "");
                if (validations.Count > 0)
                {
                    return Json(new { Result = validations, Invalid = true }, JsonRequestBehavior.AllowGet);
                }
            }
            model.MailTo = string.Join(",", model.ListMailTo);
            model.MailBcc = string.Join(",", model.ListMailBcc);
            model.MailCc = string.Join(",", model.ListMailCc);

            if (attachments!=null)
            {
                foreach (var item in attachments)
                {
                    attachmentsModel.Add(new AttachmentModel
                    {
                        Id=item.Id,
                        FileName= item.Name,
                        DisplayFileName = item.Realname,
                        FileExtension = item.Extension,
                        FileSize = item.Size,
                    });
                }
            }
            if (deleteAttachments != null)
            {
                foreach (var item in deleteAttachments)
                {
                    if (!string.IsNullOrEmpty(item.Realname))
                    {
                        var dir = System.Configuration.ConfigurationManager.AppSettings["UploadFolder"].Replace("\\", "/");
                        var path = Path.Combine(dir, item.Realname);
                        if (System.IO.File.Exists(path))
                        {
                            System.IO.File.Delete(path);
                        }
                    }

                }
            }
            var attachmentsType = MapperHelper.MapList<AttachmentModel, AttachmentType>(attachmentsModel);
            var emailEntity = MapperHelper.Map<EmailModel, EmailEntity>(model);
            emailEntity.CreatedBy = CurrentUser.UserId;
            emailEntity.CreatedDate = DateTime.Now;
            var result = new HrmResultModel<bool>();
            var response = _emailService.SaveEmailTemplate(emailEntity, attachmentsType);
            string responeseResources = string.Empty;
            if (response != null)
            {
                result = JsonConvert.DeserializeObject<HrmResultModel<bool>>(response);
                if (!CheckPermission(result))
                {
                    //return to Access Denied
                }
                else
                {
                    if (model.Id!=0)
                    {
                        if (result.Success == false)
                        {
                            responeseResources = _localizationService.GetResources("Message.Update.UnSuccessful");

                        }
                        else
                        {
                            responeseResources = _localizationService.GetResources("Message.Update.Successful");

                        }
                    }
                    else
                    {
                        if (result.Success == false)
                        {
                            responeseResources = _localizationService.GetResources("Message.Create.UnSuccessfu");

                        }
                        else
                        {
                            responeseResources = _localizationService.GetResources("Message.Create.Successful");

                        }
                    }
                   
                }
            }
            return Json(new { result,responeseResources }, JsonRequestBehavior.AllowGet);
        }
    }
}
