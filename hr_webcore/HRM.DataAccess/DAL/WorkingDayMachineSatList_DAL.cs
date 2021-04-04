
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

namespace ERP.DataAccess.DAL
{
    public class WorkingDayMachineSatList_DAL : BaseDal<ADOProvider>
    {
        public List<HR_WorkingDayMachineSatList> WorkingDayMachineSatList_GetList(int pageNumber, int pageSize, string filter, int LanguageCode, out int total)
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
                var list = UnitOfWork.Procedure<HR_WorkingDayMachineSatList>("WorkingDayMachineSatList_GetList", param).ToList();
                var userId = Global.CurrentUser.UserID;
                param = HttpRuntime.Cache.Get("WorkingDayMachineSatList_GetList-" + ERP.Framework.Common.Utils.GetKeyFromParam(param as object) + "-" + userId + "-output") as DynamicParameters;
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

        public List<HR_WorkingDayMachine> WorkingDayMachineSatList_GetList()
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@DayOffWeekFormat", 0);
                return UnitOfWork.Procedure<HR_WorkingDayMachine>("HR_WorkingDayMachine_GetList", param,useCache:true).ToList();
            }
            catch (Exception e)
            {
                return null;
            }
        }
        public SystemMessage WorkingDayMachineSatList_Save(HR_WorkingDayMachineSatList data)
        {
            var date = data.Day.ToString("yyyy/MM/dd");
            var createdate = data.CreatedDate.ToString("yyyy/MM/dd hh:mm:ss");
            SystemMessage systemMessage = new SystemMessage();
            try
            {
                var param = new DynamicParameters();
                param.Add("@AutoID", data.AutoID);
                param.Add("@WorkingDayMachineID", data.WorkingDayMachineID);
                param.Add("@Day", date);
                param.Add("@IsFullday", data.IsFullday);
                param.Add("@Note", data.Note);
                param.Add("@CreatedDate", createdate);
                param.Add("@CreatedBy", data.CreatedBy);
                param.Add("@ModifiedDate", createdate);
                param.Add("@Modifiedby", data.Modifiedby);
                UnitOfWork.ProcedureExecute("HR_WorkingDayMachineSatList_Save", param);
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
        public SystemMessage HR_WorkingDayMachineSatList_Delete(int AutoID)
        {
            SystemMessage systemMessage = new SystemMessage();
            try
            {
                var param = new DynamicParameters();
                param.Add("@AutoID", AutoID);
                UnitOfWork.ProcedureExecute("HR_WorkingDayMachineSatList_Delete", param);
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
