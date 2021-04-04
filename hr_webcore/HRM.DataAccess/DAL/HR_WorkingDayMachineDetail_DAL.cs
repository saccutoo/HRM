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
    public class HR_WorkingDayMachineDetail_DAL : BaseDal<ADOProvider>
    {
        public List<HR_WorkingDayMarchineDetail> HR_WorkingDayMachineDetail_GetList(int pageNumber, int pageSize, string filter,int LanguageCode, out int total)
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
                param.Add("@LanguageID", 1);
                param.Add("@TotalRecord", 0, DbType.Int32, ParameterDirection.InputOutput);
                var list = UnitOfWork.Procedure<HR_WorkingDayMarchineDetail>("HR_WorkingDayMachineDetail_GetList", param).ToList();
                var userId = Global.CurrentUser.UserID;
                param = HttpRuntime.Cache.Get("HR_WorkingDayMachineDetail_GetList-" + ERP.Framework.Common.Utils.GetKeyFromParam(param as object) + "-" + userId + "-output") as DynamicParameters;
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
        public SystemMessage HR_WorkingDayMachineDetail_Save(HR_WorkingDayMarchineDetail data)
        {

            SystemMessage systemMessage = new SystemMessage();
            try
            {
                var param = new DynamicParameters();
                param.Add("@AutoID", data.AutoID);
                param.Add("@WorkingDayMachineID", data.WorkingDayMachineID);
                param.Add("@StartTime", data.StartTime.ToString());
                param.Add("@EndTime", data.EndTime.ToString());
                param.Add("@StartDate", data.StartDate);
                param.Add("@EndDate", data.EndDate);
                param.Add("@Status", data.Status);
                param.Add("@WorkingType", data.WorkingType);
                param.Add("@Timekeeping", data.Timekeeping);
                UnitOfWork.ProcedureExecute("HR_WorkingDayMachineDetail_Save", param);
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
        public SystemMessage HR_WorkingDayMachineDetail_Delete(int AutoID)
        {
            SystemMessage systemMessage = new SystemMessage();
            try
            {
                var param = new DynamicParameters();
                param.Add("@AutoID", AutoID);
                UnitOfWork.ProcedureExecute("HR_WorkingDayMachineDetail_Delete", param);
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


