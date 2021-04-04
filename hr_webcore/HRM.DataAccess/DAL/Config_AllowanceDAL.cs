using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using ERP.Framework.Common;
using ERP.Framework.Data;
using ERP.Framework.DataBusiness.Common;
using HRM.DataAccess.Entity;
using Newtonsoft.Json;
using System.Collections;
using System.Web;
using HRM.Common;

namespace HRM.DataAccess.DAL
{
    public class Config_AllowanceDAL : BaseDal<ADOProvider>
    {
        public List<Cofig_Allowance> GetConfigAllowance()
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@Result");
                return UnitOfWork.Procedure<Cofig_Allowance>("Config_Allowance_GetAll", param,useCache:true).ToList();
            }
            catch (Exception)
            {
                return null;
            }
        }
        public List<Cofig_Allowance> Config_Allowance_GetList(int pageNumber, int pageSize, string filter, out int total)
        {
            try
            {
                //UnitOfWork.ConnectionString = ERP.Framework.Common.Utils.GetSetting<string>("NovaonADConnection");
                var param = new DynamicParameters();
                param.Add("@FilterField", filter);
                param.Add("@OrderBy", "");
                param.Add("@PageNumber", pageNumber);
                param.Add("@PageSize", pageSize);
                param.Add("@Type", 1);
                param.Add("@TotalRecord", 0, DbType.Int32, ParameterDirection.InputOutput);
                var list = UnitOfWork.Procedure<Cofig_Allowance>("Config_Allowance_GetList", param).ToList();
                var userId = Global.CurrentUser.UserID;
                param = HttpRuntime.Cache.Get("Config_Allowance_GetList-" + ERP.Framework.Common.Utils.GetKeyFromParam(param as object) + "-" + userId + "-output") as DynamicParameters;
                total = param.GetDataOutput<int>("@TotalRecord");
                return list;
            }
            catch (Exception ex)
            {
                total = 0;
                return null;
            }
            finally
            {
                //UnitOfWork.ConnectionString = null;

            }

        }
        public List<Config_AllowanceDAL> Config_Approval_GetList()
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@Type", 0);
                return UnitOfWork.Procedure<Config_AllowanceDAL>("Config_Approval_GetList", param).ToList();
            }
            catch (Exception e)
            {
                return null;
            }
        }
        public SystemMessage ConfigAllowance_Save(Cofig_Allowance data)
        {

            SystemMessage systemMessage = new SystemMessage();
            try
            {
                var param = new DynamicParameters();
                param.Add("@AllowanceID", data.AllowanceID);
                param.Add("@Name", data.Name);
                param.Add("@NameEN", data.NameEN);
                param.Add("@Content", data.Name);
                param.Add("@FromAmount", data.FromAmount);
                param.Add("@ToAmount", data.ToAmount);
                param.Add("@sFormular", data.sFormular);
                param.Add("@Note", data.Note);
                UnitOfWork.ProcedureExecute("Config_Allowance_Save", param);
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

        public SystemMessage ConfigAllowance_Delete(int AllowanceID)
        {
            SystemMessage systemMessage = new SystemMessage();
            try
            {
                var param = new DynamicParameters();
                param.Add("@AllowanceID", AllowanceID);
                UnitOfWork.ProcedureExecute("ConfigAllowance_Delete", param);
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

    }

}
