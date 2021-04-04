
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
    public class MergeDAL : BaseDal<ADOProvider>
    {
        public List<Merge> Merge_GetList(int pageNumber, int pageSize, string filter, int LanguageCode,int WorkingDayMachineID, int StaffID, out int total)
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
                param.Add("@WorkingDayMachineID", WorkingDayMachineID);
                param.Add("@StaffID",StaffID);
                param.Add("@TotalRecord", 0, DbType.Int32, ParameterDirection.InputOutput);
                var list = UnitOfWork.Procedure<Merge>("CHECKINOUT_GetList", param,useCache:true).ToList();
                var userId = Global.CurrentUser.UserID;
                param = HttpRuntime.Cache.Get("CHECKINOUT_GetList-" + ERP.Framework.Common.Utils.GetKeyFromParam(param as object) + "-" + userId + "-output") as DynamicParameters;
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
        public List<Employee> EmployeeByWorkingDayMachineID(int Id)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@Id", Id);
                var list = UnitOfWork.Procedure<Employee>("Employee_Get_ByWorkingDayMachineID", param).ToList();
                return list;
            }
            catch (Exception)
            {
                return null;
            }


        }
    }

}



