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
    public class HR_HolidayDAL : BaseDal<ADOProvider>
    {
        public List<HR_Holiday> GetHR_Holiday(int pageNumber, int pageSize, string filter, out int total, int LanguageCode)
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
                var list = UnitOfWork.Procedure<HR_Holiday>("GetHR_Holiday", param).ToList();
                var userId = Global.CurrentUser.UserID;
                param = HttpRuntime.Cache.Get("GetHR_Holiday-" + ERP.Framework.Common.Utils.GetKeyFromParam(param as object) + "-" + userId + "-output") as DynamicParameters;
                total = param.GetDataOutput<int>("@TotalRecord");
                return list;
            }
            catch (Exception ex)
            {
                total = 0;
                return null;
            }
        }

        public HR_Holiday GetHR_HolidayByAutoID(int HolidayID, int LanguageCode)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@HolidayID", HolidayID);
                param.Add("@LanguageID", LanguageCode);
                var list = UnitOfWork.Procedure<HR_Holiday>("GetHR_HolidayByAutoID", param).FirstOrDefault();
                return list;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public SystemMessage SaveHR_Holiday(HR_Holiday HR_Holiday)
        {
            SystemMessage systemMessage = new SystemMessage();
            try
            {
                var param1 = new DynamicParameters();
                param1.Add("@HolidayID ", HR_Holiday.HolidayID);
                param1.Add("@FromDate ", HR_Holiday.FromDate);
                param1.Add("@ToDate ", HR_Holiday.ToDate);
                param1.Add("@Type ", HR_Holiday.Type);
                param1.Add("@Note ", HR_Holiday.Note);
                param1.Add("@WorkingDayMachineID ", HR_Holiday.WorkingDayMachineID);

                UnitOfWork.ProcedureExecute("SaveHR_Holiday", param1);
                systemMessage.IsSuccess = true;
                if (Global.CurrentLanguage == 5)
                    systemMessage.Message = "Cập nhật thành công";
                else
                    systemMessage.Message = "Update successful";
                return systemMessage;
            }
            catch (Exception e)
            {
                systemMessage.IsSuccess = false;
                systemMessage.Message = e.ToString();
                return systemMessage;
            }
        }

        public SystemMessage DeleteHR_Holiday(int roleId, int idTable, int HolidayID, int LanguageCode)
        {
            SystemMessage systemMessage = new SystemMessage();
            try
            {
                var param = new DynamicParameters();
                param.Add("@HolidayID", HolidayID);
                param.Add("@LanguageID", LanguageCode);
                var checkExisted = UnitOfWork.Procedure<HR_Holiday>("GetHR_HolidayByAutoID", param).FirstOrDefault();
                if (checkExisted == null)
                {
                    systemMessage.IsSuccess = false;
                    if(Global.CurrentLanguage==5)
                        systemMessage.Message = "Dữ liệu ko tồn tại !";
                    else
                        systemMessage.Message = "Data does not exist!";
                    return systemMessage;
                }
                else
                {
                    var param1 = new DynamicParameters();
                    param1.Add("@HolidayID", HolidayID);
                    UnitOfWork.ProcedureExecute("DeleteHR_Holiday", param1);
                    systemMessage.IsSuccess = true;
                    if (Global.CurrentLanguage == 5)
                        systemMessage.Message = "Xóa thành công";
                    else
                        systemMessage.Message = "successfully deleted";
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
