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
    public class ShareRateForPerformanceReportDAL : BaseDal<ADOProvider>
    {
        public List<ShareRateForPerformanceReport> GetShareRateForPerformanceReportbyStaff(BaseListParam listParam, ListFilterParam listFilterParam, out int? totalRecord)
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
                param.Add("@LanguageID", listParam.LanguageCode);
                param.Add("@FromDate", listFilterParam.FromDate);
                param.Add("@ToDate", listFilterParam.ToDate);
                param.Add("@TotalRecord", 0, DbType.Int32, ParameterDirection.InputOutput);
                var list = UnitOfWork.Procedure<ShareRateForPerformanceReport>("GetShareRateForPerformanceReportbyStaff", param).ToList();
                param = HttpRuntime.Cache.Get("GetShareRateForPerformanceReportbyStaff-" + ERP.Framework.Common.Utils.GetKeyFromParam(param as object) + "-" + HRM.Common.Global.CurrentUser.UserID + "-output") as DynamicParameters;
                totalRecord = param.GetDataOutput<int>("@TotalRecord");

                return list;
            }
            catch (Exception ex)
            {
                totalRecord = 0;
                return new List<ShareRateForPerformanceReport>();
            }
        }
        public List<ShareRateForPerformanceReport> GetShareRateForPerformanceReportbyOrganizationUnit(BaseListParam listParam, ListFilterParam listFilterParam, out int? totalRecord)
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
                param.Add("@LanguageID", listParam.LanguageCode);
                param.Add("@FromDate", listFilterParam.FromDate);
                param.Add("@ToDate", listFilterParam.ToDate);
                param.Add("@TotalRecord", 0, DbType.Int32, ParameterDirection.InputOutput);
                var list = UnitOfWork.Procedure<ShareRateForPerformanceReport>("GetShareRateForPerformanceReportbyOrganizationUnit", param).ToList();
                param = HttpRuntime.Cache.Get("GetShareRateForPerformanceReportbyOrganizationUnit-" + ERP.Framework.Common.Utils.GetKeyFromParam(param as object) + "-" + HRM.Common.Global.CurrentUser.UserID + "-output") as DynamicParameters;
                totalRecord = param.GetDataOutput<int>("@TotalRecord");

                return list;
            }
            catch (Exception ex)
            {
                totalRecord = 0;
                return new List<ShareRateForPerformanceReport>();
            }
        }

        public List<PerformenceReport> getListPerformanceReport(int languageID, int id, int Staffid, int Roleid)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@StaffID", Staffid);
                param.Add("@Roleid", Roleid);
                param.Add("@LanguageID", languageID);
                return UnitOfWork.Procedure<PerformenceReport>("getListPerformenceReport", param,useCache:true).ToList();
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public SystemMessage Save(BaseListParam listParam, ShareRateForPerformanceReport obj)
        {
            SystemMessage systemMessage = new SystemMessage();
            try
            {
                var param = new DynamicParameters();
                param.Add("@Id", obj.Id);
                param.Add("@StaffId", obj.StaffId);
                param.Add("@OrganizationUnitId", obj.OrganizationUnitId);
                param.Add("@ShareRate", obj.ShareRate);
                param.Add("@StartDate", obj.StartDate);
                param.Add("@EndDate", obj.EndDate);
                param.Add("@Note", obj.Note);
                param.Add("@Status", obj.Status);
                param.Add("@PerformanceReportId", obj.PerformanceReportId);
                param.Add("@ModifiedDate", obj.ModifiedDate);
                param.Add("@ModifiedBy", obj.ModifiedBy);
                param.Add("@Type", obj.Type);
                param.Add("@Result", 0, DbType.Int32, ParameterDirection.InputOutput);

                UnitOfWork.ProcedureExecute("SaveShareRateForPerformanceReport", param);
                systemMessage.existedResult = param.GetDataOutput<int>("Result");

                systemMessage.IsSuccess = systemMessage.existedResult!=-1;
                return systemMessage;

            }
            catch (Exception e)
            {
                systemMessage.IsSuccess = false;
                systemMessage.Message = e.ToString();
                return systemMessage;
            }


        }

        public ShareRateForPerformanceReport ShareRateForPerformanceReportGetByID(BaseListParam listParam, int id, int type)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@UserID", listParam.UserId);
                param.Add("@Roleid", listParam.UserType);
                param.Add("@LanguageID", listParam.LanguageCode);
                param.Add("@Type", type);
                param.Add("@Id", id);
                return UnitOfWork.Procedure<ShareRateForPerformanceReport>("ShareRateForPerformanceReportGetByID", param).FirstOrDefault();
            }
            catch (Exception e)
            {
                return null;
            }
        }
    }
}
