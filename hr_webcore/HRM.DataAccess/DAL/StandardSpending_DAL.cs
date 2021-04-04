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
using HRM.Common;
using System.Web;

namespace HRM.DataAccess.DAL
{
    public class StandardSpending_DAL : BaseDal<ADOProvider>
    {
        public List<StandardSpending> StandardSpending_GetList(int pageNumber, int pageSize, string filter,int LanguageCode, out int total)
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
                param.Add("@LanguageID", LanguageCode);
                param.Add("@TotalRecord", 0, DbType.Int32, ParameterDirection.InputOutput);
                var list = UnitOfWork.Procedure<StandardSpending>("StandardSpending_GetList", param).ToList();
                var userId = Global.CurrentUser.UserID;
                param = HttpRuntime.Cache.Get("StandardSpending_GetList-" + ERP.Framework.Common.Utils.GetKeyFromParam(param as object) + "-" + userId + "-output") as DynamicParameters;
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
        public SystemMessage StandardSpending_Save(StandardSpending data)
        {

            SystemMessage systemMessage = new SystemMessage();
            try
            {                
                var param = new DynamicParameters();
                param.Add("@Id", data.Id);
                param.Add("@StaffLevelId", data.StaffLevelId);
                param.Add("@StandardSpendingAmount", data.StandardSpendingAmount);
                param.Add("@PolicyID", data.PolicyID);
                param.Add("@MinSpending", data.MinSpending);
                param.Add("@MinPerson", data.MinPerson);
                param.Add("@StartDate", data.StartDate);
                param.Add("@EndDate", data.EndDate);
                param.Add("@Status", data.Status);
                param.Add("@CreatedBy", data.CreatedBy);
                param.Add("@CreatedDate", data.CreatedDate);
                param.Add("@ModifiedBy", data.ModifiedBy);
                param.Add("@ModifiedDate", data.ModifiedDate);
                UnitOfWork.ProcedureExecute("StandardSpending_Save", param);
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
        public SystemMessage StandardSpending_Delete(int ID)
        {
            SystemMessage systemMessage = new SystemMessage();
            try
            {
                var param = new DynamicParameters();
                param.Add("@Id", ID);
                UnitOfWork.ProcedureExecute("StandardSpending_Delete", param);
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


