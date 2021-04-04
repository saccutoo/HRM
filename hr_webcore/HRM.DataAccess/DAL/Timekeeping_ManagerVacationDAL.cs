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
    public class Timekeeping_ManagerVacationDAL : BaseDal<ADOProvider>
    {
        public List<Timekeeping_ManagerVacation> GetWorkingDaySupplementManager(BaseListParam listParam, int month, int year, out int? totalRecord)
        {
            try
            {

                var param = new DynamicParameters();                
                param.Add("@FilterField", listParam.FilterField);
                param.Add("@UserId", listParam.UserId);
                param.Add("@OrderByField", null);
                param.Add("@PageIndex", listParam.PageIndex);
                param.Add("@PageSize", listParam.PageSize);
                param.Add("@LanguageId", int.Parse(listParam.LanguageCode));
                param.Add("@year", year);
                param.Add("@TotalRecord", dbType: DbType.Int32, direction: ParameterDirection.Output);
                var list = UnitOfWork.Procedure<Timekeeping_ManagerVacation>("HR_Furlough_GetManager", param).ToList();
                var userId = Global.CurrentUser.UserID;
                param = HttpRuntime.Cache.Get("HR_Furlough_GetManager-" + ERP.Framework.Common.Utils.GetKeyFromParam(param as object) + "-" + userId + "-output") as DynamicParameters;
                totalRecord = param.GetDataOutput<int>("@TotalRecord");
                return list;
            }
            catch (Exception ex)
            {
                totalRecord = 0;
                return new List<Timekeeping_ManagerVacation>();
            }
        }

    }
}
