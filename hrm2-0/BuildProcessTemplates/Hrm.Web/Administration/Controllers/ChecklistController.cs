using System.Web.Mvc;
using System.Collections.Generic;
using Hrm.Framework.Models;
using Hrm.Service;
using Hrm.Common;
using Newtonsoft.Json;
using Hrm.Common.Helpers;
using Hrm.Repository.Entity;
using Hrm.Framework.Helper;
using Hrm.Framework.ViewModels;
using Hrm.Admin.ViewModels;
using Hrm.Repository.Type;
using Hrm.Framework.Helpers;
using System.Linq;
using System;
using System.Web;
using System.IO;

namespace Hrm.Admin.Controllers
{
    public class ChecklistController : Hrm.Framework.Controllers.ChecklistController
    {
        private IChecklistService _checklistService;
        private IChecklistDetailService _checklistDetailService;
        private IStaffService _staffService;
        private IMasterDataService _masterDataService;
        private ITableColumnService _tableColumnService;
        private ILocalizationService _localizationService;
        private IEmailService _emailService;
        private IAttachmentService _attachmentService;

        private long _languageId;
        private long _userId;
        private long _roleId;
        private int _totalRecord=0;
        public ChecklistController(IChecklistService checklistService, IChecklistDetailService checklistDetailService, IStaffService staffService, IMasterDataService masterDataService, ITableColumnService tableColumnService, ILocalizationService localizationService, IEmailService emailService, IAttachmentService attachmentService)
        {
            _checklistService = checklistService;
            _checklistDetailService = checklistDetailService;
            _staffService = staffService;
            _masterDataService = masterDataService;
            _tableColumnService = tableColumnService;
            _localizationService = localizationService;
            _emailService = emailService;
            _attachmentService = attachmentService;
            _languageId = CurrentUser.LanguageId;
            _userId = CurrentUser.UserId;
            _roleId = CurrentUser.RoleId;
        }
        // GET: Staff
        public ActionResult Index()
        {
            var checklist_vm = new ChecklistViewModel();

            checklist_vm.ChecklistDetail = GetChecklistDetailByChecklistId(_checklistDetailService, 2);
            return View(checklist_vm);
        }
        public ActionResult List()
        {
            var checklist_vm = new ChecklistViewModel();
            checklist_vm.Pipelines = new PipelineGridModel();
            checklist_vm.Pipelines.PipelineSteps = new List<PipelineStepModel>();
            checklist_vm.Checklist = GetChecklist(_checklistService);
            if (checklist_vm.Checklist.Count > 0)
            {
                checklist_vm.ChecklistDetail = GetChecklistDetailByChecklistId(_checklistDetailService, checklist_vm.Checklist[0].Id);
            }
            else
            {
                checklist_vm.ChecklistDetail = new List<ChecklistDetailModel>();
            }
            return View(checklist_vm);
        }
        public ActionResult AddReceptionProcedure(long Id,string ActionName)
        {
            ChecklistDetailViewModel checklistViewDetail = new ChecklistDetailViewModel();
            List<ChecklistDetailModel> checklistDetail = new List<ChecklistDetailModel>();
            ChecklistModel Checklist = new ChecklistModel();
            checklistViewDetail.ActionName = ActionName;
            if (Id != 0)
            {
                checklistDetail = GetChecklistDetailByChecklistId(_checklistDetailService, Id);
                if (checklistDetail.Count==0)
                {
                    var checklist = GetChecklistById(_checklistService, Id);
                    if (checklist!=null && checklist.Id!=0)
                    {
                        Checklist.Id = checklist.Id;
                        Checklist.Name = checklist.Name;
                        Checklist.Note = checklist.Note;
                    }
                }
                else
                {
                    Checklist.Id = checklistDetail[0].ChecklistId;
                    Checklist.Name = checklistDetail[0].ChecklistName;
                    Checklist.Note = checklistDetail[0].ChecklistNote;
                    foreach (var item in checklistDetail)
                    {
                        item.Index = item.Id;
                        if (checklistViewDetail.ActionName == Hrm.Common.ActionName.Copy)
                        {
                            item.Id = 0;
                            Checklist.Id = 0;
                        }
                    }
                }
                
            }
            var responseMasterData = this._masterDataService.GetAllMasterDataByName(MasterGroup.ChecklistDetailType, _languageId);
            if (responseMasterData != null)
            {
                var resultMasterData = JsonConvert.DeserializeObject<HrmResultModel<MasterDataModel>>(responseMasterData);
                if (!CheckPermission(resultMasterData))
                {
                    //return to Access Denied
                }
                else
                {
                    checklistViewDetail.MasterData = JsonConvert.DeserializeObject<List<dynamic>>(JsonConvert.SerializeObject(resultMasterData.Results));
                }
            }
            var responseMasterDataControlType = this._masterDataService.GetAllMasterDataByName(MasterGroup.ControlType, _languageId);
            if (responseMasterDataControlType != null)
            {
                var ressultMasterDataControlType = JsonConvert.DeserializeObject<HrmResultModel<MasterDataModel>>(responseMasterDataControlType);
                if (!CheckPermission(ressultMasterDataControlType))
                {
                    //return to Access Denied
                }
                else
                {
                    checklistViewDetail.MasterDataControlType = JsonConvert.DeserializeObject<List<dynamic>>(JsonConvert.SerializeObject(ressultMasterDataControlType.Results));
                }
            }
            checklistViewDetail.Checklist = Checklist;
            checklistViewDetail.ChecklistDetail = JsonConvert.DeserializeObject<List<dynamic>>(JsonConvert.SerializeObject(checklistDetail));
           return View(checklistViewDetail);
        }
        public ActionResult ShowFormAddChecklist(long index)
        {
            StaffViewModel staff = new StaffViewModel();
            staff.ChecklistDetailEdit = new ChecklistDetailModel();
            staff.Index = index;
            staff.ActionName = ActionName.Add;
            var param = new BasicParamModel()
            {
                FilterField = string.Empty,
                PageNumber = 1,
                PageSize = int.MaxValue,
                LanguageId = _languageId,
                RoleId = _roleId,
                UserId = _userId,
                DbName = CurrentUser.DbName
            };
            var paramEntity = MapperHelper.Map<BasicParamModel, BasicParamType>(param);
            var responseStaff = _staffService.GetStaff(paramEntity, out _totalRecord);
            if (responseStaff != null)
            {
                var result = JsonConvert.DeserializeObject<HrmResultModel<StaffModel>>(responseStaff);
                if (!CheckPermission(result))
                {
                    //return to Access Denied
                }
                else
                {
                    staff.Staff = JsonConvert.DeserializeObject<List<dynamic>>(JsonConvert.SerializeObject(result.Results));
                }
            }
            var responseTableColumn = _tableColumnService.GetTableColumn(TableConfig.Staff, true);
            if (responseTableColumn != null)
            {
                var ressultTableColumn= JsonConvert.DeserializeObject<HrmResultModel<TableColumnEntity>>(responseTableColumn);
                if (!CheckPermission(ressultTableColumn))
                {
                    //return to Access Denied
                }
                else
                {
                    staff.TableColum = JsonConvert.DeserializeObject<List<dynamic>>(JsonConvert.SerializeObject(ressultTableColumn.Results));
                }
            }
            return PartialView("~/Administration/Views/Checklist/_AddChecklist.cshtml", staff);
        }
        public ActionResult ShowFormAddGroupChecklist()
        {
            StaffViewModel staff = new StaffViewModel();
            var param = new BasicParamModel()
            {
                FilterField = string.Empty,
                PageNumber = 1,
                PageSize = int.MaxValue,
                LanguageId = _languageId,
                RoleId = _roleId,
                UserId = _userId,
                DbName = CurrentUser.DbName
            };
            var paramEntity = MapperHelper.Map<BasicParamModel, BasicParamType>(param);
            var responseStaff=_staffService.GetStaff(paramEntity, out _totalRecord);
            if (responseStaff!=null)
            {
                var result = JsonConvert.DeserializeObject<HrmResultModel<StaffModel>>(responseStaff);
                if (!CheckPermission(result))
                {
                    //return to Access Denied
                }
                else
                {
                    staff.Staff= JsonConvert.DeserializeObject<List<dynamic>>(JsonConvert.SerializeObject(result.Results));
                }
            }
            return PartialView("~/Administration/Views/Checklist/_AddGroupChecklist.cshtml", staff);
        }
        public ActionResult ReloadChecklist(List<ChecklistDetailModel> data)
        {
            foreach (var item in data)
            {
                if (item.ChecklistDetailTypeName == Hrm.Common.ChecklistDetailType.Group)
                {
                    item.ChecklistDetailTypeId = MasterDataId.Group;
                }
                else
                {
                    item.ChecklistDetailTypeId = MasterDataId.Single;
                }
                if (item.ControlTypeName== ControlType.Checkbox && item.ChecklistDetailTypeName != Hrm.Common.ChecklistDetailType.Group)
                {
                    item.TypeControlId = MasterDataId.Checkbox;
                }
                else if(item.ControlTypeName == ControlType.FieldUpdating && item.ChecklistDetailTypeName != Hrm.Common.ChecklistDetailType.Group)
                {
                    item.TypeControlId = MasterDataId.FieldUpdating;
                }
                else if (item.ControlTypeName == ControlType.TextEditor && item.ChecklistDetailTypeName != Hrm.Common.ChecklistDetailType.Group)
                {
                    item.TypeControlId = MasterDataId.TextEditor;
                }
                else if (item.ControlTypeName == ControlType.Datepicker && item.ChecklistDetailTypeName != Hrm.Common.ChecklistDetailType.Group)
                {
                    item.TypeControlId = MasterDataId.Datepicker;
                }
                else if (item.ControlTypeName == ControlType.FileAttachment && item.ChecklistDetailTypeName != Hrm.Common.ChecklistDetailType.Group)
                {
                    item.TypeControlId = MasterDataId.FileAttachment;
                }
            }
            ChecklistDetailViewModel checklistViewDetail = new ChecklistDetailViewModel();
            checklistViewDetail.ChecklistDetail = JsonConvert.DeserializeObject<List<dynamic>>(JsonConvert.SerializeObject(data));
            return PartialView("~/Administration/Views/Checklist/_ListCheckList.cshtml", checklistViewDetail);
        }
        public ActionResult AddChecklist(ChecklistDetailModel checklist)
        {
            if (checklist != null)
            {
                if (checklist.ChecklistDetailTypeName == Hrm.Common.ChecklistDetailType.Group)
                {
                    checklist.ColumnLink = "-1";
                }
                else
                {
                    if (checklist.ControlTypeName!= ControlType.FieldUpdating)
                    {
                        checklist.ColumnLink = "-1";
                    }
                }
                var validations = ValidationHelper.Validation(checklist, "checklist");
                if (validations.Count > 0)
                {
                    return Json(new { Result = validations, Invalid = true }, JsonRequestBehavior.AllowGet);
                }
            }
            return Json(new { IsSucces =true }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult SaveChecklist(ChecklistModel checklist,List<ChecklistDetailModel> checklistDetail)
        {
            if (checklist != null)
            {
                var validations = ValidationHelper.Validation(checklist, "checklist");
                if (validations.Count > 0)
                {
                    return Json(new { Result = validations, Invalid = true }, JsonRequestBehavior.AllowGet);
                }
            }

            checklist.CreatedBy = CurrentUser.UserId;
            checklist.UpdatedBy = CurrentUser.UserId;
            var checklistEntity = MapperHelper.Map<ChecklistModel, ChecklistEntity>(checklist);
            var checklistDetailType = MapperHelper.MapList<ChecklistDetailModel, Hrm.Repository.Type.ChecklistDetailType>(checklistDetail);
            var respone = _checklistService.SaveChecklist(checklistEntity, checklistDetailType);
            var result = new HrmResultModel<bool>();
            var responeseResources = string.Empty;
            if (respone != null)
            {
                result = JsonConvert.DeserializeObject<HrmResultModel<bool>>(respone);
                if (!CheckPermission(result))
                {
                    //return to Access Denied
                }
                else
                {
                    if (result.Success == true)
                    {
                        if (checklist.Id != 0)
                        {
                            responeseResources = _localizationService.GetResources("Message.Update.Successful");
                        }
                        else
                        {
                            responeseResources = _localizationService.GetResources("Message.Create.Successful");
                        }
                    }
                    else
                    {
                        if (checklist.Id != 0)
                        {
                            responeseResources = _localizationService.GetResources("Message.Update.UnSuccessful");
                        }
                        else
                        {
                            responeseResources = _localizationService.GetResources("Message.Create.UnSuccessfu");
                        }
                    }
                }
            }
            return Json(new { result, responeseResources }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult ShowFormEditChecklist(ChecklistDetailModel data)
        {
            StaffViewModel staff = new StaffViewModel();
            staff.ActionName = ActionName.Edit;
            staff.ChecklistDetailEdit = data;
            var param = new BasicParamModel()
            {
                FilterField = string.Empty,
                PageNumber = 1,
                PageSize = int.MaxValue,
                LanguageId = _languageId,
                RoleId = _roleId,
                UserId = _userId,
                DbName = CurrentUser.DbName
            };
            var paramEntity = MapperHelper.Map<BasicParamModel, BasicParamType>(param);
            var responseStaff = _staffService.GetStaff(paramEntity, out _totalRecord);
            if (responseStaff != null)
            {
                var result = JsonConvert.DeserializeObject<HrmResultModel<StaffModel>>(responseStaff);
                if (!CheckPermission(result))
                {
                    //return to Access Denied
                }
                else
                {
                    staff.Staff = JsonConvert.DeserializeObject<List<dynamic>>(JsonConvert.SerializeObject(result.Results));
                }
            }
            var responseTableColumn = _tableColumnService.GetTableColumn(TableConfig.Staff, true);
            if (responseTableColumn != null)
            {
                var ressultTableColumn = JsonConvert.DeserializeObject<HrmResultModel<TableColumnEntity>>(responseTableColumn);
                if (!CheckPermission(ressultTableColumn))
                {
                    //return to Access Denied
                }
                else
                {
                    staff.TableColum = JsonConvert.DeserializeObject<List<dynamic>>(JsonConvert.SerializeObject(ressultTableColumn.Results));
                }
            }
            return PartialView("~/Administration/Views/Checklist/_AddChecklist.cshtml", staff);
        }
        public ActionResult ShowFormEditGroupChecklist(ChecklistDetailModel data)
        {
            StaffViewModel staff = new StaffViewModel();
            staff.ChecklistDetailEdit = new ChecklistDetailModel();
            var param = new BasicParamModel()
            {
                FilterField = string.Empty,
                PageNumber = 1,
                PageSize = int.MaxValue,
                LanguageId = _languageId,
                RoleId = _roleId,
                UserId = _userId,
                DbName = CurrentUser.DbName
            };
            var paramEntity = MapperHelper.Map<BasicParamModel, BasicParamType>(param);
            var responseStaff = _staffService.GetStaff(paramEntity, out _totalRecord);
            if (responseStaff != null)
            {
                var result = JsonConvert.DeserializeObject<HrmResultModel<StaffModel>>(responseStaff);
                if (!CheckPermission(result))
                {
                    //return to Access Denied
                }
                else
                {
                    staff.Staff = JsonConvert.DeserializeObject<List<dynamic>>(JsonConvert.SerializeObject(result.Results));
                }
            }
            staff.ChecklistDetailEdit = data;
            return PartialView(UrlHelpers.TemplateAdmin("Checklist","_AddGroupChecklist.cshtml"), staff);

        }
        public ActionResult ReloadListChecklist(long Id)
        {
            var checklist_vm = new ChecklistViewModel();
            checklist_vm.Pipelines = new PipelineGridModel()
            {
                PipelineSteps = new List<PipelineStepModel>()
            };
            checklist_vm.Pipelines.PipelineSteps = new List<PipelineStepModel>();
            checklist_vm.Checklist = new List<ChecklistModel>();
            checklist_vm.ChecklistDetail = new List<ChecklistDetailModel>();
            checklist_vm.ChecklistDetail = GetChecklistDetailByChecklistId(_checklistDetailService, Id);
            if (checklist_vm.ChecklistDetail.Count==0)
            {
                var checklist = GetChecklistById(_checklistService, Id);
                if (checklist != null && checklist.Id != 0)
                {
                    checklist_vm.ChecklistDetail.Add(new ChecklistDetailModel
                    {
                        ChecklistId = checklist.Id,
                        ChecklistName = checklist.Name,
                        ChecklistNote = checklist.Note,

                });
                }
                
            }
            return PartialView(UrlHelpers.View("~/Views/Shared/Checklist/_ChecklistSummary.cshtml"), checklist_vm);
        }
        public ActionResult DeleteChecklist(long Id)
        {
            var checklist_vm = new ChecklistViewModel();
            var respone = _checklistService.DeleteChecklist(Id);
            var responeseResources = string.Empty;
            var result = new HrmResultModel<ChecklistModel>();
            if (respone != null)
            {
                result = JsonConvert.DeserializeObject<HrmResultModel<ChecklistModel>>(respone);
                if (!CheckPermission(result))
                {
                    //return to Access Denied
                }
                else
                {
                    if (result.Results.Count>0 && result.Results[0].Id!=0)
                    {
                        responeseResources = _localizationService.GetResources("Checklist.MessageDelete");
                        result.Success = false;
                    }
                    else
                    {
                        responeseResources = _localizationService.GetResources("Message.Delete.Successful");
                        result.Success = true;
                    }                    
                }
            }
            return Json(new { result, responeseResources }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult SearchChecklist(string searchKey)
        {
            var checklist_vm = new ChecklistViewModel();
            var response = _checklistService.SearchChecklist(searchKey,CurrentUser.LanguageId);
            if (response!=null)
            {
                var result = JsonConvert.DeserializeObject<HrmResultModel<ChecklistModel>>(response);
                if (!CheckPermission(result))
                {
                    //return to Access Denied
                }
                else
                {
                    checklist_vm.Checklist = result.Results;
                }
            }
            return PartialView(UrlHelpers.TemplateAdmin("Checklist","_CategoryChecklist.cshtml"), checklist_vm);
        }
        public ActionResult WelcomeKit(string viewType)
        {  
            return View();
        }
        public ActionResult AddWelcomeKit(int activeTab=0,long id=0,bool isAdd=false)
        {
            WelcomeKitViewModel welcomeKit = new WelcomeKitViewModel();
            var listStaff = new List<StaffModel>();
            welcomeKit.ActiveTab = activeTab;
            if (id!=0)
            {
                welcomeKit.isSendWelcomeKit = true;
                welcomeKit.EmailDetail.isSendWelcomeKit= true;
                var response = _staffService.GetStaffInformationById(id);
                if (response!=null)
                {
                    var result = JsonConvert.DeserializeObject<HrmResultModel<StaffModel>>(response);
                    if (!CheckPermission(result))
                    {
                        //return to Access Denied
                    }
                    else
                    {
                        welcomeKit.EmailDetail.staff = result.Results.FirstOrDefault();
                    }
                }
            }
            var param = new BasicParamModel()
            {
                FilterField = string.Empty,
                PageNumber = 1,
                PageSize = int.MaxValue,
                LanguageId = _languageId,
                RoleId = _roleId,
                UserId = _userId,
                DbName = CurrentUser.DbName
            };
            var paramEntity = MapperHelper.Map<BasicParamModel, BasicParamType>(param);

            switch (activeTab)
            {
                case 0: // add template email
                    {
                        welcomeKit.EmailDetail.isAdd = isAdd;
                        #region
                        var responseStaff = _staffService.GetStaff(paramEntity, out _totalRecord);
                        if (responseStaff != null)
                        {
                            var result = JsonConvert.DeserializeObject<HrmResultModel<StaffModel>>(responseStaff);
                            if (!CheckPermission(result))
                            {
                                //return to Access Denied
                            }
                            else
                            {
                                foreach (var item in result.Results)
                                {
                                    if (item.Email == null)
                                    {
                                        item.Email = "0";
                                    }
                                }
                                //welcomeKit.EmailDetail.Staffs = JsonConvert.DeserializeObject<List<dynamic>>(JsonConvert.SerializeObject(result.Results));
                                listStaff = result.Results;
                            }
                        }
                        var responseTableColumn = _tableColumnService.GetTableColumn(TableConfig.Staff, true);
                        if (responseTableColumn != null)
                        {
                            var ressultTableColumn = JsonConvert.DeserializeObject<HrmResultModel<TableColumnEntity>>(responseTableColumn);
                            if (!CheckPermission(ressultTableColumn))
                            {
                                //return to Access Denied
                            }
                            else
                            {
                                welcomeKit.EmailDetail.Columns = JsonConvert.DeserializeObject<List<dynamic>>(JsonConvert.SerializeObject(ressultTableColumn.Results));
                            }
                        }
                        var responseEmail = _emailService.GetEmail(paramEntity,true);
                        if (responseEmail != null)
                        {
                            var resultEmail = JsonConvert.DeserializeObject<HrmResultModel<EmailModel>>(responseEmail);
                            if (!CheckPermission(resultEmail))
                            {
                                //return to Access Denied
                            }
                            else
                            {
                                if (resultEmail.Results.Count > 0)
                                {
                                    welcomeKit.EmailDetail.Email = resultEmail.Results[0];
                                    if (resultEmail.Results.Count > 0)
                                    {
                                        welcomeKit.EmailDetail.Email = resultEmail.Results[0];
                                        if (welcomeKit.EmailDetail.Email.MailTo != null)
                                        {
                                            var mailTo = welcomeKit.EmailDetail.Email.MailTo.Split(',');
                                            for (int i = 0; i < mailTo.Length; i++)
                                            {
                                                welcomeKit.EmailDetail.Email.ListMailTo.Add(mailTo[i]);
                                                var data = listStaff.Where(s => s.Email == mailTo[i]).ToList();
                                                if (data.Count() == 0)
                                                {
                                                    listStaff.Add(new StaffModel
                                                    {
                                                        Email = mailTo[i],
                                                    });
                                                }
                                            }
                                            if (welcomeKit.EmailDetail.staff.Id != 0)
                                            {
                                                welcomeKit.EmailDetail.Email.ListMailTo.Add(welcomeKit.EmailDetail.staff.Email);
                                            }
                                        }
                                        if (welcomeKit.EmailDetail.Email.MailTo != null)
                                        {
                                            var mailCc = welcomeKit.EmailDetail.Email.MailCc.Split(',');
                                            for (int i = 0; i < mailCc.Length; i++)
                                            {
                                                welcomeKit.EmailDetail.Email.ListMailCc.Add(mailCc[i]);
                                                var data = listStaff.Where(s => s.Email == mailCc[i]).ToList();
                                                if (data.Count() == 0)
                                                {
                                                    listStaff.Add(new StaffModel
                                                    {
                                                        Email = mailCc[i],
                                                    });
                                                }
                                            }
                                        }
                                        if (welcomeKit.EmailDetail.Email.MailTo != null)
                                        {
                                            var mailBcc = welcomeKit.EmailDetail.Email.MailBcc.Split(',');
                                            for (int i = 0; i < mailBcc.Length; i++)
                                            {
                                                welcomeKit.EmailDetail.Email.ListMailBcc.Add(mailBcc[i]);
                                                var data = listStaff.Where(s => s.Email == mailBcc[i]).ToList();
                                                if (data.Count() == 0)
                                                {
                                                    listStaff.Add(new StaffModel
                                                    {
                                                        Email = mailBcc[i],
                                                    });
                                                }
                                            }
                                        }
                                     welcomeKit.EmailDetail.Staffs = JsonConvert.DeserializeObject<List<dynamic>>(JsonConvert.SerializeObject(listStaff));                                      
                                    }
                                }
                            }
                        }

                        var responseAttackment = _attachmentService.GetAttackmenByRecordId(MailTemplate.TemplateWelcomeKit, DataType.Email);
                        if (responseAttackment != null)
                        {
                            var resultAttackment = JsonConvert.DeserializeObject<HrmResultModel<AttachmentModel>>(responseAttackment);
                            if (!CheckPermission(resultAttackment))
                            {
                                //return to Access Denied
                            }
                            else
                            {
                                if (resultAttackment.Results.Count > 0)
                                {
                                    welcomeKit.EmailDetail.Attachments = resultAttackment.Results;
                                    foreach (var item in welcomeKit.EmailDetail.Attachments)
                                    {
                                        welcomeKit.EmailDetail.AttachmentJs.Add(new AttachmentJs()
                                        {
                                            Id = item.Id,
                                            Name = item.DisplayFileName,
                                            Realname = item.FileName,
                                            Size = item.FileSize,
                                            Extension = item.FileExtension,
                                            CreatedDate = item.CreatedDate.ToString("yyyy-MM-dd")
                                        });
                                    }
                                }
                            }
                        }
                        #endregion

                        break;
                    }
                case 1: // document
                    {
                        welcomeKit.Attachment.isAdd = isAdd;
                        if (welcomeKit.Attachment.isAdd==false)
                        {
                            var responseDcument = _attachmentService.GetAttachmentByDataType(DataType.Document);
                            if (responseDcument!=null)
                            {
                                var resultDocument = JsonConvert.DeserializeObject<HrmResultModel<AttachmentModel>>(responseDcument);
                                if (!CheckPermission(resultDocument))
                                {
                                    //return to Access Denied
                                }
                                else
                                {
                                    welcomeKit.Attachment.attachments = resultDocument.Results;
                                }
                            }                                                      
                        }
                        if (id!=0 )
                        {
                            var responseDcument = _attachmentService.GetAttackmentById(id, DataType.Document);
                            if (responseDcument != null)
                            {
                                var resultDocument = JsonConvert.DeserializeObject<HrmResultModel<AttachmentModel>>(responseDcument);
                                if (!CheckPermission(resultDocument))
                                {
                                    //return to Access Denied
                                }
                                else
                                {
                                    welcomeKit.Attachment.attachment = resultDocument.Results.FirstOrDefault();
                                    if (welcomeKit.Attachment.attachment.DisplayFileName!=null)
                                    {
                                        welcomeKit.Attachment.AttachmentJs.Add(new AttachmentJs()
                                        {
                                            Id = welcomeKit.Attachment.attachment.Id,
                                            Name = welcomeKit.Attachment.attachment.DisplayFileName,
                                            Realname = welcomeKit.Attachment.attachment.FileName,
                                            Size = welcomeKit.Attachment.attachment.FileSize,
                                            Extension = welcomeKit.Attachment.attachment.FileExtension,
                                            CreatedDate = welcomeKit.Attachment.attachment.CreatedDate.ToString("yyyy-MM-dd")
                                        });
                                    }                                 
                                }
                            }
                        }
                        break;
                    }
            }        
            return View(welcomeKit);
        }
        public ActionResult ReloadEmailAttachment(List<AttachmentJs> atchments)
        {
            List<AttachmentModel> attachments = new List<AttachmentModel>();
            if (atchments != null && atchments.Count > 0)
            {
                foreach (var item in atchments)
                {
                    attachments.Add(new AttachmentModel()
                    {
                        Id=item.Id,
                        FileName = item.Realname,
                        DisplayFileName = item.Name,
                        FileSize = item.Size,
                        FileExtension = item.Extension,
                        CreatedDate= DateTime.ParseExact(item.CreatedDate, System.Configuration.ConfigurationManager.AppSettings["SqlDateFormat"], System.Globalization.CultureInfo.InstalledUICulture),
                    });
                }
            }
            return PartialView(UrlHelpers.TemplateAdmin("Checklist", "_ListAttackment.cshtml"), attachments);

        }
        [ValidateInput(false)]
        public ActionResult SendWelcomeKit(string subject, string body, List<string> toMail, List<string> cc, List<string> bcc, string from, string replyTo, List<AttachmentJs> attachments)
        {
            string responeseResources = string.Empty;
            bool isSuccess = false;
            var resultCode = SendMail(subject, body, toMail, cc, bcc, from, replyTo, attachments);
            if (resultCode==200)
            {
                responeseResources = responeseResources = _localizationService.GetResources("Checklist.SendWelcomeKitMessageSucces");
                isSuccess = true;
            }
            else 
            {
                responeseResources = responeseResources = _localizationService.GetResources("Checklist.SendWelcomeKitMessageError");
            }
            return Json(new { IsSuccess= isSuccess,Result = responeseResources }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult SaveDocument(AttachmentModel model, List<AttachmentJs> deleteAttachments)
        {
            string responeseResources = string.Empty;
            var result = new HrmResultModel<AttachmentModel>();
            if (model != null)
            {
                var validations = ValidationHelper.Validation(model, "");
                if (validations.Count > 0)
                {
                    return Json(new { Result = validations, Invalid = true }, JsonRequestBehavior.AllowGet);
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

                model.DataType = DataType.Document;
                model.CreatedDate = DateTime.Now;
                var attachmentEntity = MapperHelper.Map<AttachmentModel, AttachmentEntity>(model);
                var response = _attachmentService.SaveAttachment(attachmentEntity);              
                if (response!=null)
                {
                    result = JsonConvert.DeserializeObject<HrmResultModel<AttachmentModel>>(response);
                    if (!CheckPermission(result))
                    {
                        //return to Access Denied
                    }
                    else
                    {
                        if (model.Id != 0)
                        {
                            if (result.Results.Count>0 && result.Results.FirstOrDefault().Id!=0)
                            {
                                responeseResources = _localizationService.GetResources("Message.Update.UnSuccessful");
                                result.Success = false;
                            }
                            else
                            {
                                responeseResources = _localizationService.GetResources("Message.Update.Successful");
                                result.Success = true;

                            }
                        }
                        else
                        {
                            if (result.Results.Count > 0)
                            {
                                responeseResources = _localizationService.GetResources("Message.Create.UnSuccessful");
                                result.Success = false;

                            }
                            else
                            {
                                responeseResources = _localizationService.GetResources("Message.Create.Successful");
                                result.Success = true;

                            }
                        }
                    }
                }
                return Json(new { result, responeseResources }, JsonRequestBehavior.AllowGet);

            }
            else
            {
                result.Success = false;
                responeseResources= _localizationService.GetResources("Message.Create.UpdateFail");
                return Json(new { result, responeseResources }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult ReloadDocument(List<AttachmentJs> atchments)
        {
            List<AttachmentModel> attachments = new List<AttachmentModel>();
            AttachmentModel attachment = new AttachmentModel();

            if (atchments != null && atchments.Count > 0)
            {
                foreach (var item in atchments)
                {
                    attachments.Add(new AttachmentModel()
                    {
                        Id = item.Id,
                        FileName = item.Realname,
                        DisplayFileName = item.Name,
                        FileSize = item.Size,
                        FileExtension = item.Extension,
                        CreatedDate = DateTime.ParseExact(item.CreatedDate, System.Configuration.ConfigurationManager.AppSettings["SqlDateFormat"], System.Globalization.CultureInfo.InstalledUICulture),
                    });
                }
                attachment = attachments[0];
            }
            return PartialView(UrlHelpers.TemplateAdmin("Checklist", "_DocumentDetail.cshtml"), attachment);
        }
        public ActionResult DeleteAttackmentById(long id)
        {
            string responeseResources = string.Empty;
            var response = _attachmentService.DeleteAttackmentById(id);
            var result = new HrmResultModel<AttachmentModel>();
            if (response!=null)
            {
                result = JsonConvert.DeserializeObject<HrmResultModel<AttachmentModel>>(response);
                if (!CheckPermission(result))
                {
                    //return to Access Denied
                }
                else
                {
                    if (result.Results.Count>0)
                    {
                        responeseResources = responeseResources = _localizationService.GetResources("Message.Delete.UnSuccessful");
                        result.Success = false;
                    }
                    else
                    {
                        responeseResources = responeseResources = _localizationService.GetResources("Message.Delete.Successful");
                        result.Success = true;

                    }
                }
            }            
            return Json(new { result , responeseResources }, JsonRequestBehavior.AllowGet);
        }
    }
}