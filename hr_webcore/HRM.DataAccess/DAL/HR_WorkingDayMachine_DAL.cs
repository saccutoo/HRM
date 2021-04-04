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
    public class HR_WorkingDayMachine_DAL : BaseDal<ADOProvider>
    {
        public List<HR_WorkingDayMachine> HR_WorkingDayMachine_GetList(int pageNumber, int pageSize, string filter,int LanguageCode, out int total)
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
                var list = UnitOfWork.Procedure<HR_WorkingDayMachine>("HR_WorkingDayMachine_GetList1", param).ToList();
                var userId = Global.CurrentUser.UserID;
                param = HttpRuntime.Cache.Get("HR_WorkingDayMachine_GetList1-" + ERP.Framework.Common.Utils.GetKeyFromParam(param as object) + "-" + userId + "-output") as DynamicParameters;
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
        public SystemMessage HR_WorkingDayMachine_Save(HR_WorkingDayMachine data)
        {

            SystemMessage systemMessage = new SystemMessage();
            try
            {
                var param = new DynamicParameters();
                if (data.WorkingDayMachineID == 0)
                {
                    data.WorkingDayMachineID =-1;
                }
                param.Add("@WorkingDayMachineID", data.WorkingDayMachineID);
                param.Add("@Name", data.Name);
                param.Add("@NameEN", data.NameEN);
                param.Add("@DatabaseName", data.DatabaseName);
                param.Add("@DayOfWeekFomat", data.DayOfWeekFomat);
                param.Add("@StatusSat", data.StatusSat);
                UnitOfWork.ProcedureExecute("HR_WorkingDayMachine_Save", param);
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
        public SystemMessage HR_WorkingDayMachine_Delete(int ID)
        {
            SystemMessage systemMessage = new SystemMessage();
            try
            {
                var param = new DynamicParameters();
                param.Add("@ID", ID);
                UnitOfWork.ProcedureExecute("HR_WorkingDayMachine_Delete", param);
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


