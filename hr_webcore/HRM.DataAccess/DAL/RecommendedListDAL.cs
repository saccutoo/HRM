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

namespace ERP.DataAccess.DAL
{
    public class RecommendedListDAL : BaseDal<ADOProvider>
    {
        public List<RecommendedList> GetWorkingDaySupplementManager(BaseListParam listParam, int month, int year, int CurrentType, int status, out int? totalRecord)
        {
            try
            {
                var param = new DynamicParameters();
                var fillter = listParam.FilterField;
                string month1 = month.ToString() ;
                if (month<10)
                {
                    month1 = "0" + month;
                }
                if (status == 0)
                {
                    param.Add("@FilterField", " and MonthVacation = '" + month1 + "-" + year + "'" + fillter);
                }
                else if (status == 1)
                {
                    param.Add("@FilterField", " and MonthVacation = '" + month1 + "-" + year + "' AND Status IN (" + 3 +"," + 1 + "," + 8 + "," + 6 + ")" + fillter);
                }
                else if (status == 2)
                {
                    param.Add("@FilterField", " and MonthVacation = '" + month1 + "-" + year + "' AND Status IN (" + 5 + "," + 10 + ")" + fillter);
                }
                else if (status == 3)
                {
                    param.Add("@FilterField", " and MonthVacation = '" + month1 + "-" + year + "' AND Status IN (" + 2 + "," + 4 + "," + 9 + "," +7+ " )" + fillter);
                }               
                param.Add("@UserId", listParam.UserId);
                param.Add("@OrderByField", listParam.OrderByField);
                param.Add("@PageIndex", listParam.PageIndex);
                param.Add("@PageSize", listParam.PageSize);
                //param.Add("@Roleid", listParam.UserType);
                param.Add("@LanguageId", int.Parse(listParam.LanguageCode));
                param.Add("@CurrentType", CurrentType);            
                param.Add("@TotalRecord", dbType: DbType.Int32, direction: ParameterDirection.Output);
                var list = UnitOfWork.Procedure<RecommendedList>("HR_WorkingDaySupplement_RecommendedList_Get", param).ToList();
                param = HttpRuntime.Cache.Get("HR_WorkingDaySupplement_RecommendedList_Get-" + ERP.Framework.Common.Utils.GetKeyFromParam(param as object) + "-" + HRM.Common.Global.CurrentUser.UserID + "-output") as DynamicParameters;
                totalRecord = param.GetDataOutput<int>("@TotalRecord");
                return list;
            }
            catch (Exception ex)
            {
                totalRecord = 0;
                return new List<RecommendedList>();
            }
        }
        public List<TimeKeeping_TimeSSN> GetTimeKeepingMachine(BaseListParam listParam, int month, int year, out int? totalRecord,int userid)
        {
            try
            {
                var param = new DynamicParameters();
                //param.Add("@FilterField", listParam.FilterField?.Replace("undefined", ""));
                //param.Add("@OrderByField", listParam.OrderByField);
                //param.Add("@PageIndex", listParam.PageIndex);
                //param.Add("@PageSize", listParam.PageSize);
                param.Add("@UserId", userid);
                param.Add("@Month", month);
                param.Add("@Year", year);
                //param.Add("@RoleId", listParam.UserType);
                //param.Add("@LanguageId", int.Parse(listParam.LanguageCode));
                //param.Add("@Month", month);
                //param.Add("@year", year);
                //param.Add("@OrganizationUnitID", listParam.DeptId);
                //param.Add("@TotalRecord", dbType: DbType.Int32, direction: ParameterDirection.Output);
                var list = UnitOfWork.Procedure<TimeKeeping_TimeSSN>("Get_TimeKeepingMachine", param).ToList();
                totalRecord = 1;
                return list;
            }
            catch (Exception ex)
            {
                totalRecord = 0;
                return new List<TimeKeeping_TimeSSN>();
            }
        }
    }
}
