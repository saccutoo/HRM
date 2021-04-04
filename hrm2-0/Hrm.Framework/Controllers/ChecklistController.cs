using Hrm.Framework.Context;
using Hrm.Framework.Models;
using Newtonsoft.Json;
using System.Security.Cryptography;
using System.Text;
using System.Web.Mvc;
using Hrm.Service;
using System.Web;
using System;
using Hrm.Common;
using Hrm.Framework;
using System.Web.Security;
using static Hrm.Common.Constant;
using Hrm.Framework.Helper;
using System.Collections.Generic;
using System.Linq;
using Hrm.Common.Helpers;
using Hrm.Core.Infrastructure;
using Hrm.Framework.ViewModels;
using Hrm.Repository.Type;

namespace Hrm.Framework.Controllers
{
    public class ChecklistController : BaseController
    {

        public ChecklistModel GetChecklistById(IChecklistService checklistService, long checklistId)
        {
            var response = checklistService.GetChecklistById(checklistId);
            var result = JsonConvert.DeserializeObject<HrmResultModel<ChecklistModel>>(response);
            if (!CheckPermission(result))
            {
                //return to Access Denied
            }
            else
            {
                return result.Results.FirstOrDefault();
            }
            return new ChecklistModel();
        }
        public List<ChecklistDetailModel> GetChecklistDetailByChecklistId(IChecklistDetailService checklistDetailService, long checklistId)
        {
            var response = checklistDetailService.GetChecklistDetailByChecklistId(checklistId);
            var result = JsonConvert.DeserializeObject<HrmResultModel<ChecklistDetailModel>>(response);
            if (!CheckPermission(result))
            {
                //return to Access Denied
            }
            else
            {
                return result.Results;
            }
            return new List<ChecklistDetailModel>();
        }
        public List<ChecklistDetailModel> GetChecklistStaffDetail(IChecklistDetailService checklistDetailService, long checklistId)
        {
            var response = checklistDetailService.GetChecklistStaffDetail(checklistId);
            var result = JsonConvert.DeserializeObject<HrmResultModel<ChecklistDetailModel>>(response);
            if (!CheckPermission(result))
            {
                //return to Access Denied
            }
            else
            {
                return result.Results;
            }
            return new List<ChecklistDetailModel>();
        }
        public List<ChecklistDetailModel> GetChecklistDetailByStaffId(IChecklistDetailService checklistDetailService, long staffId)
        {
            var response = checklistDetailService.GetChecklistDetailByStaffId(staffId);
            var result = JsonConvert.DeserializeObject<HrmResultModel<ChecklistDetailModel>>(response);
            if (!CheckPermission(result))
            {
                //return to Access Denied
            }
            else
            {
                return result.Results;
            }
            return new List<ChecklistDetailModel>();
        }

        public List<ChecklistModel> GetChecklist(IChecklistService checklistService)
        {
            var response = checklistService.GetChecklist();
            var result = JsonConvert.DeserializeObject<HrmResultModel<ChecklistModel>>(response);
            if (!CheckPermission(result))
            {
                //return to Access Denied
            }
            else
            {
                return result.Results;
            }
            return new List<ChecklistModel>();
        }
    }
}