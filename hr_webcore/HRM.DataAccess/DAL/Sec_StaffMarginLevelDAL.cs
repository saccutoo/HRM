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
    public class Sec_StaffMarginLevelDAL : BaseDal<ADOProvider>
    {
        public List<Sec_StaffMarginLevel> GetSec_StaffMarginLevel(int pageNumber, int pageSize, string filter, out int total, int LanguageCode)
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
                var list = UnitOfWork.Procedure<Sec_StaffMarginLevel>("GetSec_StaffMarginLevel", param).ToList();
                var userId = Global.CurrentUser.UserID;
                param = HttpRuntime.Cache.Get("GetSec_StaffMarginLevel-" + ERP.Framework.Common.Utils.GetKeyFromParam(param as object) + "-" + userId + "-output") as DynamicParameters;
                total = param.GetDataOutput<int>("@TotalRecord");
                return list;
            }
            catch (Exception ex)
            {
                total = 0;
                return null;
            }
        }

        public Sec_StaffMarginLevel GetSec_StaffMarginLevelByAutoID(int AutoID, int LanguageCode)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@AutoID", AutoID);
                param.Add("@LanguageID", LanguageCode);
                var list = UnitOfWork.Procedure<Sec_StaffMarginLevel>("GetSec_StaffMarginLevelByAutoID", param).FirstOrDefault();
                return list;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public SystemMessage SaveSec_StaffMarginLevel(Sec_StaffMarginLevel Sec_StaffMarginLevel)
        {
            SystemMessage systemMessage = new SystemMessage();
            try
            {
                var param1 = new DynamicParameters();
                param1.Add("@AutoID ", Sec_StaffMarginLevel.AutoID);
                param1.Add("@StaffLevelID ", Sec_StaffMarginLevel.StaffLevelID);
                param1.Add("@MinMargin ", Sec_StaffMarginLevel.MinMargin);
                param1.Add("@MaxMargin ", Sec_StaffMarginLevel.MaxMargin);
                param1.Add("@CreatedOn ", Sec_StaffMarginLevel.CreatedOn);
                param1.Add("@CreatedBy ", Sec_StaffMarginLevel.CreatedBy);
                param1.Add("@Status ", Sec_StaffMarginLevel.Status);
                param1.Add("@StartDate ", Sec_StaffMarginLevel.StartDate);
                param1.Add("@EndDate ", Sec_StaffMarginLevel.EndDate);

                UnitOfWork.ProcedureExecute("SaveSec_StaffMarginLevel", param1);
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

        public SystemMessage DeleteSec_StaffMarginLevel(int roleId, int idTable,int AutoID, int LanguageCode)
        {
            SystemMessage systemMessage = new SystemMessage();
            try
            {
                var param = new DynamicParameters();
                param.Add("@AutoID", AutoID);
                param.Add("@LanguageID", LanguageCode);
                var checkExisted = UnitOfWork.Procedure<Sec_StaffMarginLevel>("GetSec_StaffMarginLevelByAutoID", param).FirstOrDefault();
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
                    param1.Add("@AutoID", AutoID);
                    UnitOfWork.ProcedureExecute("DeleteSec_StaffMarginLevel", param1);
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
    }
}
