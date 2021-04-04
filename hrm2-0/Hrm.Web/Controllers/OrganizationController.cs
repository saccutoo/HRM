using Hrm.Framework.Models;
using System.Collections.Generic;
using Hrm.Service;
using Hrm.Common;
using Hrm.Common.Helpers;
using Hrm.Framework.Controllers;
using Hrm.Repository.Type;
using Newtonsoft.Json;
using System.Web.Mvc;
using Hrm.Framework.Helper;

namespace Hrm.Web.Controllers
{
    public partial class OrganizationController : Hrm.Framework.Controllers.OrganizationController
    {
        #region Fields
        #endregion Fields
        #region Constructors

        private IOrganizationService _organizationService;
        private long _languageId;

        public OrganizationController(IOrganizationService organizationService)
        {
            _organizationService = organizationService;
            _languageId = CurrentUser.LanguageId;
        }
        #endregion      

        //public List<OrganizationModel> GetOrganization(BasicParamModel param)
        //{
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
