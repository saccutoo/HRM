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
    public class OrganizationController : BaseController
    {

        #region Fields
        #endregion Fields
        #region Constructors
        //private int totalRecord;

        #endregion      
        public List<OrganizationModel> GetOrganization(IOrganizationService _organizationService, BasicParamModel param,out int totalRecord)
        {
            var paramType = MapperHelper.Map<BasicParamModel, BasicParamType>(param);
            var response = _organizationService.GetOrganization(paramType, out totalRecord);
            var result = JsonConvert.DeserializeObject<HrmResultModel<OrganizationModel>>(response);
            if (!CheckPermission(result))
            {
                //return to Access Denied
            }
            else
            {
                return result.Results;
            }            
            totalRecord = 0;
            return new List<OrganizationModel>();
        }
        //public List<OrganizationModel> GetOrganizationById(IOrganizationService _organizationService, BasicParamModel param, long organizationId)
        //{
        //    param.FilterField = " AND Id = " + organizationId.ToString();
        //    var paramType = MapperHelper.Map<BasicParamModel, BasicParamType>(param);
        //    var response = _organizationService.GetOrganization(paramType, out totalRecord);
        //    if (response != null)
        //    {
        //        var result = JsonConvert.DeserializeObject<List<OrganizationModel>>(response);
        //        return result;
        //    }
        //    return new List<OrganizationModel>();
        //}
    }
}