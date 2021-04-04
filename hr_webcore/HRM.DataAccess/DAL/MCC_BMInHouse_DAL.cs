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
    public class MCC_BMInHouse_DAL : BaseDal<ADOProvider>
    {
        public List<MCC_BMInHouse> MCC_BMInHouse_GetList(int pageNumber, int pageSize, string filter, out int total)
        {
            try
            {
                //UnitOfWork.ConnectionString = ERP.Framework.Common.Utils.GetSetting<string>("NovaonADConnection");
                var param = new DynamicParameters();
                if (filter.Contains("AccountName") == true)
                {
                    filter = filter.Replace("AccountName", "AccountType");
                }
                if (filter.Contains("TypeName") == true)
                {
                    filter = filter.Replace("TypeName", "Type");
                }
                param.Add("@FilterField", filter);
                param.Add("@OrderBy", "");
                param.Add("@PageNumber", pageNumber);
                param.Add("@PageSize", pageSize);
                param.Add("@Type", 1);
                param.Add("@TotalRecord", 0, DbType.Int32, ParameterDirection.InputOutput);
                var list = UnitOfWork.Procedure<MCC_BMInHouse>("MCC_BMInHouse_GetList", param).ToList();
                var userId = Global.CurrentUser.UserID;
                param = HttpRuntime.Cache.Get("MCC_BMInHouse_GetList-" + ERP.Framework.Common.Utils.GetKeyFromParam(param as object) + "-" + userId + "-output") as DynamicParameters;
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
        public SystemMessage MCC_BMInHouse_Save(MCC_BMInHouse data)
        {

            SystemMessage systemMessage = new SystemMessage();
            try
            {
                var param = new DynamicParameters();
                param.Add("@Id", data.Id);
                param.Add("@CustomerId", data.CustomerId);
                param.Add("@BM_MCCId", data.BM_MCCId);
                param.Add("@AccountType", data.AccountType);
                param.Add("@IsPartner", data.IsPartner);
                param.Add("@Type", data.Type);
                UnitOfWork.ProcedureExecute("MCC_BMInHouse_Save", param);
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
        public SystemMessage MCC_BMInHouse_Delete(int ID)
        {
            SystemMessage systemMessage = new SystemMessage();
            try
            {
                var param = new DynamicParameters();
                param.Add("@Id", ID);
                UnitOfWork.ProcedureExecute("MCC_BMInHouse_Delete", param);
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


