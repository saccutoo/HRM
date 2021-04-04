using Dapper;
using ERP.Framework.Common;
using ERP.Framework.Data;
using HRM.DataAccess.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HRM.Common;
using System.Web;

namespace HRM.DataAccess.DAL
{
    public class HR_WorkingDayDAL : BaseDal<ADOProvider>
    {
        public List<HR_WorkingDay> GetHR_WorkingDay(int pageNumber, int pageSize, string filter, out int total, int LanguageCode)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@FilterField", filter);
                param.Add("@OrderBy", "");
                param.Add("@PageNumber", pageNumber);
                param.Add("@PageSize", pageSize);
                param.Add("@Type", 1);
                param.Add("@TotalRecord", 0, DbType.Int32, ParameterDirection.InputOutput);
                param.Add("@LanguageID", LanguageCode);
                var list = UnitOfWork.Procedure<HR_WorkingDay>("GetHR_WorkingDay", param).ToList();
                var userId = Global.CurrentUser.UserID;
                param = HttpRuntime.Cache.Get("GetHR_WorkingDay-" + ERP.Framework.Common.Utils.GetKeyFromParam(param as object) + "-" + userId + "-output") as DynamicParameters;
                total = param.GetDataOutput<int>("@TotalRecord");
                return list;
            }
            catch (Exception ex)
            {
                total = 0;
                return null;
            }
        }

        public HR_WorkingDay GetHR_WorkingDayByAutoID(int WorkingDayID, int LanguageCode)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@WorkingDayID", WorkingDayID);
                param.Add("@LanguageID", LanguageCode);
                var list = UnitOfWork.Procedure<HR_WorkingDay>("GetHR_WorkingDayByAutoID", param).FirstOrDefault();
                return list;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public SystemMessage SaveHR_WorkingDay(HR_WorkingDay HR_WorkingDay)
        {
            SystemMessage systemMessage = new SystemMessage();
            try
            {
                var param1 = new DynamicParameters();
                param1.Add("@WorkingDayID ", HR_WorkingDay.WorkingDayID);
                param1.Add("@WorkingDayMachineID ", HR_WorkingDay.WorkingDayMachineID);
                param1.Add("@MorningHourStart ", HR_WorkingDay.MorningHourStart.ToString());
                param1.Add("@MorningHourMid ", HR_WorkingDay.MorningHourMid.ToString());
                param1.Add("@MorningHourEnd ", HR_WorkingDay.MorningHourEnd.ToString());
                param1.Add("@AfernoonHourStart ", HR_WorkingDay.AfernoonHourStart.ToString());
                param1.Add("@AfernoonHourMid ", HR_WorkingDay.AfernoonHourMid.ToString());
                param1.Add("@AfternoonHourEnd ", HR_WorkingDay.AfternoonHourEnd.ToString());
                param1.Add("@StartDate ", HR_WorkingDay.StartDate);
                param1.Add("@EndDate ", HR_WorkingDay.EndDate);

                UnitOfWork.ProcedureExecute("SaveHR_WorkingDay", param1);
                systemMessage.IsSuccess = true;
                if (Global.CurrentLanguage == 5)
                    systemMessage.Message = "Cập nhật thành công";
                else
                    systemMessage.Message = "successful update";
                return systemMessage;
            }
            catch (Exception e)
            {
                systemMessage.IsSuccess = false;
                systemMessage.Message = e.ToString();
                return systemMessage;
            }
        }

        public SystemMessage DeleteHR_WorkingDay(int roleId, int idTable, int WorkingDayID, int LanguageCode)
        {
            SystemMessage systemMessage = new SystemMessage();
            try
            {
                var param = new DynamicParameters();
                param.Add("@WorkingDayID", WorkingDayID);
                param.Add("@LanguageID", LanguageCode);
                var checkExisted = UnitOfWork.Procedure<HR_WorkingDay>("GetHR_WorkingDayByAutoID", param).FirstOrDefault();
                if (checkExisted == null)
                {
                    systemMessage.IsSuccess = false;
                    if (Global.CurrentLanguage == 5)
                        systemMessage.Message = "Dữ liệu ko tồn tại !";
                    else
                        systemMessage.Message = "Data does not exist!";
                    return systemMessage;
                }
                else
                {
                    var param1 = new DynamicParameters();
                    param1.Add("@WorkingDayID", WorkingDayID);
                    UnitOfWork.ProcedureExecute("DeleteHR_WorkingDay", param1);
                    systemMessage.IsSuccess = true;
                    if (Global.CurrentLanguage == 5)
                        systemMessage.Message = "Xóa thành công";
                    else
                        systemMessage.Message = "Delete success";
                    return systemMessage;

                }
            }
            catch (Exception e)
            {
                systemMessage.IsSuccess = false;
                systemMessage.Message = e.ToString();
                return systemMessage;
            }
        }

        public List<HR_WorkingDayMachine> Get_HR_WorkingDayMachine()
        {
            try
            {
                return UnitOfWork.Procedure<HR_WorkingDayMachine>("Get_HR_WorkingDayMachine",useCache:true).ToList();
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
