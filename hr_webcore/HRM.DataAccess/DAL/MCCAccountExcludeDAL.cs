using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using ERP.Framework.Common;
using ERP.Framework.Data;
using ERP.Framework.DataBusiness.Common;
using HRM.DataAccess.Common;
using HRM.DataAccess.Entity;
using System.Web;

namespace HRM.DataAccess.DAL
{
    public class MCCAccountExcludeDAL : BaseDal<ADOProvider>
    {
        public List<MCCAccountExclude> GetMCCAccountExclude(BaseListParam listParam, out int? totalRecord, out TableColumnsTotal totalColumns)
        {
            try
            {

                var param = new DynamicParameters();
                param.Add("@FilterField", listParam.FilterField);
                param.Add("@OrderByField", listParam.OrderByField);
                param.Add("@PageIndex", listParam.PageIndex);
                param.Add("@PageSize", listParam.PageSize);
                param.Add("@UserId", listParam.UserId);
                param.Add("@RoleId", listParam.UserType);
                param.Add("@DeptId", listParam.DeptId);
                param.Add("@LanguageID", listParam.LanguageCode);
                param.Add("@TotalRecord", 0, DbType.Int32, ParameterDirection.InputOutput);             
                var list = UnitOfWork.Procedure<MCCAccountExclude>("GetMCCAccountExclude", param).ToList();
                param = HttpRuntime.Cache.Get("GetMCCAccountExclude-" + ERP.Framework.Common.Utils.GetKeyFromParam(param as object) + "-" + HRM.Common.Global.CurrentUser.UserID + "-output") as DynamicParameters;
                totalRecord = param.GetDataOutput<int>("@TotalRecord");
                totalColumns = new TableColumnsTotal();
                return list;
            }
            catch (Exception ex)
            {
                totalRecord = 0;
                totalColumns = new TableColumnsTotal();
                return new List<MCCAccountExclude>();
            }


        }

        public SystemMessage SaveMCCAcountExclude(BaseListParam listParam, MCCAccountExclude obj)
        {
            SystemMessage systemMessage = new SystemMessage();
            try
            {
                var param = new DynamicParameters();
                param.Add("@AutoID", obj.AutoID);
                param.Add("@AccountNumber", obj.AccountNumber);
                param.Add("@StartDate", obj.StartDate);
                param.Add("@Status", obj.Status);
                param.Add("@EndDate", obj.EndDate);
                param.Add("@CreatedBy", obj.CreatedBy);
                param.Add("@CreatedOn", obj.CreatedOn);
                param.Add("@ModifiedBy", obj.ModifiedBy);
                param.Add("@ModifiedOn", obj.ModifiedOn);
                param.Add("@Note", obj.Note);
                UnitOfWork.ProcedureExecute("SaveMCCAcountExclude", param);
                systemMessage.IsSuccess = true;
                return systemMessage;

            }
            catch (Exception e)
            {
                systemMessage.IsSuccess = false;
                systemMessage.Message = e.ToString();
                return systemMessage;
            }


        }

        public List<MCCAccountExclude> ExportExcelMCCAccountExclude(BaseListParam listParam, out int? totalRecord)
        {

            try
            {
                var param = new DynamicParameters();
                param.Add("@FilterField", listParam.FilterField);
                param.Add("@OrderByField", listParam.OrderByField);
                param.Add("@PageIndex", listParam.PageIndex);
                param.Add("@PageSize", listParam.PageSize);
                param.Add("@UserId", listParam.UserId);
                param.Add("@RoleId", listParam.UserType);
                param.Add("@DeptId", listParam.DeptId);
                param.Add("@LanguageID", listParam.LanguageCode);
                param.Add("@TotalRecord", 0, DbType.Int32, ParameterDirection.InputOutput);
                var list = UnitOfWork.Procedure<MCCAccountExclude>("GetMCCAccountExclude", param).ToList();
                param = HttpRuntime.Cache.Get("GetMCCAccountExclude-" + ERP.Framework.Common.Utils.GetKeyFromParam(param as object) + "-" + HRM.Common.Global.CurrentUser.UserID + "-output") as DynamicParameters;
                totalRecord = param.GetDataOutput<int>("@TotalRecord");
                return list;
            }
            catch (Exception e)
            {
                totalRecord = 0;
                return new List<MCCAccountExclude>();
            }
        }

        public MCCAccountExclude GetMCCAccountExcludeByID(BaseListParam listParam, int id)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@UserID", listParam.UserId);
                param.Add("@Roleid", listParam.UserType);
                param.Add("@LanguageID", listParam.LanguageCode);
                param.Add("@AutoID", id);
                return UnitOfWork.Procedure<MCCAccountExclude>("MCCAccountExcludeGetByID", param).FirstOrDefault();
            }
            catch (Exception)
            {
                return null;
            }
        }


    }
}
