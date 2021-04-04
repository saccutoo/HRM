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
    public class HR_WorkingDayConfigDAL : BaseDal<ADOProvider>
    {
        public List<HR_WorkingDayConfig> GetHR_WorkingDayConfig(int pageNumber, int pageSize, string filter, out int total, int LanguageCode)
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
                var list = UnitOfWork.Procedure<HR_WorkingDayConfig>("GetHR_WorkingDayConfig", param).ToList();
                var userId = Global.CurrentUser.UserID;
                param = HttpRuntime.Cache.Get("GetHR_WorkingDayConfig-" + ERP.Framework.Common.Utils.GetKeyFromParam(param as object) + "-" + userId + "-output") as DynamicParameters;
                total = param.GetDataOutput<int>("@TotalRecord");
                return list;
            }
            catch (Exception ex)
            {
                total = 0;
                return null;
            }
        }

        public HR_WorkingDayConfig GetHR_WorkingDayConfigByAutoID(int AutoID, int LanguageCode)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@AutoID", AutoID);
                param.Add("@LanguageID", LanguageCode);
                var list = UnitOfWork.Procedure<HR_WorkingDayConfig>("GetHR_WorkingDayConfigByAutoID", param).FirstOrDefault();
                return list;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public SystemMessage SaveHR_WorkingDayConfig(HR_WorkingDayConfig HR_WorkingDayConfig)
        {
            SystemMessage systemMessage = new SystemMessage();
            try
            {
                var param1 = new DynamicParameters();
                param1.Add("@AutoID ", HR_WorkingDayConfig.AutoID);
                param1.Add("@DateFromNumber ", HR_WorkingDayConfig.DateFromNumber);
                param1.Add("@DateToNumber ", HR_WorkingDayConfig.DateToNumber);
                param1.Add("@WorkingDayMachineID ", HR_WorkingDayConfig.WorkingDayMachineID);
                param1.Add("@StartMonth ", HR_WorkingDayConfig.StartMonth);
                param1.Add("@EndMonth ", HR_WorkingDayConfig.EndMonth);
                param1.Add("@NoTimeChecking ", HR_WorkingDayConfig.NoTimeChecking.ToString());

                UnitOfWork.ProcedureExecute("SaveHR_WorkingDayConfig", param1);
                systemMessage.IsSuccess = true;
                if (Global.CurrentLanguage==5)
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

        public SystemMessage DeleteHR_WorkingDayConfig(int roleId, int idTable, int AutoID, int LanguageCode)
        {
            SystemMessage systemMessage = new SystemMessage();
            try
            {
                var param = new DynamicParameters();
                param.Add("@AutoID", AutoID);
                param.Add("@LanguageID", LanguageCode);
                var checkExisted = UnitOfWork.Procedure<HR_WorkingDayConfig>("GetHR_WorkingDayConfigByAutoID", param).FirstOrDefault();
                if (checkExisted == null)
                {
                    systemMessage.IsSuccess = false;
                    if (Global.CurrentLanguage==5)
                        systemMessage.Message = "Dữ liệu ko tồn tại !";
                    else
                        systemMessage.Message = "Data does not exist!";
                    return systemMessage;
                }
                else
                {
                    var param1 = new DynamicParameters();
                    param1.Add("@AutoID", AutoID);
                    UnitOfWork.ProcedureExecute("DeleteHR_WorkingDayConfig", param1);
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
