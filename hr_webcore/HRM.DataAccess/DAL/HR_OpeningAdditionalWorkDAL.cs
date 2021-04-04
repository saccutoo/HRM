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
using HRM.DataAccess.Entity.UserDefinedType;
using HRM.DataAccess.Helpers;

namespace HRM.DataAccess.DAL
{
    public class HR_OpeningAdditionalWorkDAL : BaseDal<ADOProvider>
    {
        public List<HR_OpeningAdditionalWork> HR_OpeningAdditionalWork_GetList(int pageNumber, int pageSize, string filter, int LanguageCode, out int total)
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
                var list = UnitOfWork.Procedure<HR_OpeningAdditionalWork>("HR_OpeningAdditionalWork_GetList", param,useCache:false).ToList();
                var userId = Global.CurrentUser.UserID;
                //param = HttpRuntime.Cache.Get("HR_OpeningAdditionalWork_GetList-" + ERP.Framework.Common.Utils.GetKeyFromParam(param as object) + "-" + userId + "-output") as DynamicParameters;
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
        public SystemMessage OpeningAdditionalWorkSave(List<HR_OpeningAdditionalWork> Data)
        {
            SystemMessage systemMessage = new SystemMessage();
            try
            {
                var param = new DynamicParameters();
                param.Add("@HR_OpeningAdditionalWorkType", Data.ToUserDefinedDataTable(), DbType.Object);
                UnitOfWork.ProcedureExecute("HR_OpeningAdditionalWork_Save", param);
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
        public SystemMessage OpeningAdditionalWorkDelete(int AutoID,out int Status)
        {
            SystemMessage systemMessage = new SystemMessage();
            try
            {
                var param = new DynamicParameters();
                param.Add("@AutoID", AutoID);
                param.Add("@Status", 0, DbType.Int32, ParameterDirection.InputOutput);
                UnitOfWork.ProcedureExecute("OpeningAdditionalWorkDelete", param);
                Status = param.GetDataOutput<int>("@Status");
                if (Status==0)
                    systemMessage.IsSuccess = true;
                else
                    systemMessage.IsSuccess = false;
                return systemMessage;
            }
            catch (Exception e)
            {
                systemMessage.IsSuccess = false;
                systemMessage.Message = e.ToString();
                Status = 0;
                return systemMessage;
            }
        }

        public SystemMessage OpeningAdditionalWorkEdit(HR_OpeningAdditionalWork Data,out int status)
        {
            SystemMessage systemMessage = new SystemMessage();
            try
            {
                var param = new DynamicParameters();
                //param.Add("@HR_OpeningAdditionalWorkType", Data.ToUserDefinedDataTable(), DbType.Object);
                param.Add("@AutoID", Data.AutoID);
                param.Add("@StaffID", Data.StaffID);
                param.Add("@OpenDay", Data.OpenDay);
                param.Add("@CreatedBy", Data.CreatedBy);
                param.Add("@CreatedDate", Data.CreatedDate);
                param.Add("@ModifiedBy", Data.ModifiedBy);
                param.Add("@ModifiedDate", Data.ModifiedDate);
                param.Add("@Status", Data.Status);
                param.Add("@OutStatus", 0, DbType.Int32, ParameterDirection.InputOutput);
                UnitOfWork.ProcedureExecute("HR_OpeningAdditionalWork_Edit", param);
                status = param.GetDataOutput<int>("@OutStatus");
                if (status == 1)
                    systemMessage.IsSuccess = true;
                else
                    systemMessage.IsSuccess = false;
                return systemMessage;
            }
            catch (Exception e)
            {
                status = 0;
                systemMessage.IsSuccess = false;
                systemMessage.Message = e.ToString();
                return systemMessage;
            }
        }
    }

}



