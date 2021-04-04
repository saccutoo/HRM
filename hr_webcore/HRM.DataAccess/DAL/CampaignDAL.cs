using ERP.Framework.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using ERP.Framework.Data;
using ERP.Framework.DataBusiness.Common;
using HRM.DataAccess.Common;
using HRM.DataAccess.Entity;
using System.Web;

namespace HRM.DataAccess.DAL
{
    public class CampaignDAL : BaseDal<ADOProvider>
    {
        public List<CampaignReopened> GetListReopenCampaign(BaseListParam listParam, ListFilterParam listFilterParam, out int? totalRecord)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@FilterField", listParam.FilterField);
                param.Add("@OrderByField", listParam.OrderByField);
                param.Add("@PageNumber", listParam.PageIndex);
                param.Add("@PageSize", listParam.PageSize);
                param.Add("@UserId", listParam.UserId);
                param.Add("@RoleID", listParam.UserType);
                param.Add("@OrganizationUnitId", listParam.DeptId);
                param.Add("@isExcel", 0);
                param.Add("@LanguageID", listParam.LanguageCode);
                param.Add("@FromDate", listFilterParam.FromDate);
                param.Add("@ToDate", listFilterParam.ToDate);
                param.Add("@TotalRecord", 0, DbType.Int32, ParameterDirection.InputOutput);
                var list = UnitOfWork.Procedure<CampaignReopened>("CampaignReopened_GetAll", param).ToList();
                param = HttpRuntime.Cache.Get("CampaignReopened_GetAll-" + ERP.Framework.Common.Utils.GetKeyFromParam(param as object) + "-" + HRM.Common.Global.CurrentUser.UserID + "-output") as DynamicParameters;
                totalRecord = param.GetDataOutput<int>("@TotalRecord");

                return list;
            }
            catch (Exception ex)
            {
                totalRecord = 0;
                return new List<CampaignReopened>();
            }
        }
        public List<CampaignReopened> ExportListReopenCampaign(BaseListParam listParam, ListFilterParam listFilterParam, out int? totalRecord)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@FilterField", listParam.FilterField);
                param.Add("@OrderByField", listParam.OrderByField);
                param.Add("@PageNumber", listParam.PageIndex);
                param.Add("@PageSize", listParam.PageSize);
                param.Add("@UserId", listParam.UserId);
                param.Add("@RoleID", listParam.UserType);
                param.Add("@OrganizationUnitId", listParam.DeptId);
                param.Add("@isExcel", 1);
                param.Add("@LanguageID", listParam.LanguageCode);
                param.Add("@FromDate", listFilterParam.FromDate);
                param.Add("@ToDate", listFilterParam.ToDate);
                param.Add("@TotalRecord", 0, DbType.Int32, ParameterDirection.InputOutput);
                var list = UnitOfWork.Procedure<CampaignReopened>("CampaignReopened_GetAll", param).ToList();
                param = HttpRuntime.Cache.Get("CampaignReopened_GetAll-" + ERP.Framework.Common.Utils.GetKeyFromParam(param as object) + "-" + HRM.Common.Global.CurrentUser.UserID + "-output") as DynamicParameters;
                totalRecord = param.GetDataOutput<int>("@TotalRecord");

                return list;
            }
            catch (Exception ex)
            {
                totalRecord = 0;
                return new List<CampaignReopened>();
            }
        }
    }
}
