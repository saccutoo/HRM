using AutoMapper;
using ERP.Framework.Constants;
using HRM.App_LocalResources;
using HRM.DataAccess.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HRM.Models
{
    public class SendSyncEmailModel /*: IValidatableObject*/
    {
        public int Id { get; set; }
        [Display(Name = "DeptId", ResourceType = typeof(AppRes))]
        public Nullable<int> DeptId { get; set; }
        public int? Status { get; set; }
        public string DeptEmail { get; set; }

        public string DeptName { get; set; }
        [Display(Name = "DeptIdGG", ResourceType = typeof(AppRes))]
        public int? DeptIdGG { get; set; }
        [Display(Name = "DeptIdFB", ResourceType = typeof(AppRes))]
        public int? DeptIdFB { get; set; }
        [Display(Name = "DeptIdOther", ResourceType = typeof(AppRes))]
        public int? DeptIdOTher { get; set; }
        public string[] ListDepartment { get; set; }

        [Display(Name = "SendTo", ResourceType = typeof(AppRes))]
        //[Required(ErrorMessageResourceName = "ErrorMessageNull", ErrorMessageResourceType = typeof(AppRes))]
        public string SendTo { get; set; }
        // [RegularExpression(@"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", ErrorMessageResourceName = "ErrorFormat", ErrorMessageResourceType = typeof(AppRes))]
        [RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,6})+)([,]([\w\.\-]+)@([\w\-]+)((\.(\w){2,6})+))*$", ErrorMessageResourceName = "ErrorFormatEmail", ErrorMessageResourceType = typeof(AppRes))]
        public string AddTo { get; set; }
        [RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,6})+)([,]([\w\.\-]+)@([\w\-]+)((\.(\w){2,6})+))*$", ErrorMessageResourceName = "ErrorFormatEmail", ErrorMessageResourceType = typeof(AppRes))]
        public string AddCc { get; set; }

        [Display(Name = "CC")]
        public string Cc { get; set; }


        public int Type { get; set; }
        public int AuthorType { get; set; }

        public string StatusName { get; set; }

        [Display(Name = "Subject", ResourceType = typeof(AppRes))]
        [AllowHtml]
        public string Subject { get; set; }
        //[Display(Name = AppRes.Body.ToString())]
        [Display(Name = "Body", ResourceType = typeof(AppRes))]
        public string Body { get; set; }
        public int? ServiceId { get; set; }
        [Display(Name = "Note", ResourceType = typeof(AppRes))]
        public string Note { get; set; }
        public Nullable<bool> ServiceGG { get; set; }
        public Nullable<bool> ServiceFB { get; set; }
        public Nullable<bool> ServiceOS { get; set; }
        public int? QuotationserviceId { get; set; }
        public int? COAServiceID { get; set; }
        public int? COAInfoID { get; set; }
        public string ToDept { get; set; }
        public string JsonAttachment { get; set; }


        public int? StatusGG { get; set; }
        public int? StatusFB { get; set; }
        public int? StatusOther { get; set; }

        public SendSyncEmailModel()
        {

        }
        public SendSyncEmail GetEntity()
        {
            var configMapper = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<SendSyncEmailModel, SendSyncEmail>();
            });

            SendSyncEmail entity = configMapper.CreateMapper().Map<SendSyncEmailModel, SendSyncEmail>(this);

            return entity;
        }
        public SendSyncEmailModel(SendSyncEmail entity, bool encodeHtml = false)
        {
            if (entity != null)
            {
                var configMapper = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<SendSyncEmail, SendSyncEmailModel>();
                });
                configMapper.CreateMapper().Map(entity, this);

            }
        }

        //public IEnumerable<ValidationResult> Validate(System.ComponentModel.DataAnnotations.ValidationContext validationContext)
        //{

        //    if ((StatusGG == @Constant.numStatus.Draft.GetHashCode() || StatusGG == @Constant.numStatus.Notapproved.GetHashCode()) && ServiceGG == true)
        //    {
        //        if (DeptIdGG == null || DeptIdGG <= 0)
        //        {
        //            yield return new ValidationResult(AppRes.VaidateExcepcionRequest_ToOrganizationUnitID, new[] { "DeptIdGG" });
        //        }
        //    }


        //    if ((StatusFB == @Constant.numStatus.Draft.GetHashCode() || StatusFB == @Constant.numStatus.Notapproved.GetHashCode()) && ServiceFB == true)
        //    {
        //        if (DeptIdFB == null || DeptIdFB <= 0)
        //        {
        //            yield return new ValidationResult(AppRes.VaidateExcepcionRequest_ToOrganizationUnitID, new[] { "DeptIdFB" });
        //        }
        //    }


        //    if ((StatusOther == @Constant.numStatus.Draft.GetHashCode() || StatusOther == @Constant.numStatus.Notapproved.GetHashCode()) && ServiceOS == true)
        //    {
        //        if (DeptIdOTher == null || DeptIdOTher <= 0)
        //        {
        //            yield return new ValidationResult(AppRes.VaidateExcepcionRequest_ToOrganizationUnitID, new[] { "DeptIdOTher" });
        //        }
        //    }



        //    if ((SendTo == null || SendTo == string.Empty) & (AddTo == null || AddTo == string.Empty))
        //    {
        //        yield return new ValidationResult(AppRes.ErrorMessageNull, new[] { "AddTo" });

        //    }

        //    //if (COAInfoID != null && COAInfoID > 0)
        //    //{
        //    //    if (Status != null && Status == Constant.numStatus.Notapproved.GetHashCode())
        //    //    {
        //    //        yield return new ValidationResult(AppRes.ErrorMessageNullAll, new[] { "Note" });
        //    //    }
        //    //}
        //}
    }
}