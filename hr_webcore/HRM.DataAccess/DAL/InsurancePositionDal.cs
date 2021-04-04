using System;
using ERP.Framework.Common;
using ERP.Framework.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.Data;
using HRM.DataAccess.Entity;
using System.Web;
using HRM.Common;

namespace HRM.DataAccess.DAL
{
    public class InsurancePositionDal: BaseDal<ADOProvider>
    {
        public List<Config_Insurance_Position> GetListInsurancePosition(int pageNumber, int pageSize, string filter, out int total)
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
                var list = UnitOfWork.Procedure<Config_Insurance_Position>("Config_Insurance_Position_List", param).ToList();
                var userId = Global.CurrentUser.UserID;
                param = HttpRuntime.Cache.Get("Config_Insurance_Position_List-" + ERP.Framework.Common.Utils.GetKeyFromParam(param as object) + "-" + userId + "-output") as DynamicParameters;
                total = param.GetDataOutput<int>("@TotalRecord");
                return list;
            }
            catch (Exception ex)
            {
                total = 0;
                return null;
            }

        }

        public Config_Insurance_Position GetInsurancePositionById(int roleId, int idTable, int id)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@AutoID", id);
                return UnitOfWork.Procedure<Config_Insurance_Position>("Config_Insurance_Position_GetByID", param).FirstOrDefault();
            }
            catch (Exception)
            {
                return null;
            }
        } 
        public SystemMessage UpdateInsurancePositionById(int roleId, int idTable, int id, Config_Insurance_Position insurancePosition)
        {


            SystemMessage systemMessage = new SystemMessage();
            try
            {
                int status = 0;
                var param = new DynamicParameters();
                param.Add("@AutoID", id);
                var checkExisted = UnitOfWork.Procedure<Config_Insurance_Position>("Config_Insurance_Position_GetByID", param).FirstOrDefault();
                if (checkExisted == null)
                {
                    systemMessage.IsSuccess = false;
                    systemMessage.Message = "Dữ liệu ko tồn tại !";
                    return systemMessage;
                }
                var param1 = new DynamicParameters();
                if (insurancePosition.Status == 2015)
                {
                    status = 2017;
                }
                param1.Add("@AutoID", insurancePosition.AutoID);
                param1.Add("@PositionID", insurancePosition.PositionID);
                param1.Add("@Amount",insurancePosition.Amount);
                param1.Add("@Status", status);
                param1.Add("@ApplyDate", insurancePosition.ApplyDate);
                param1.Add("@Note",insurancePosition.Note);
                param1.Add("@CreatedBy",1);
                UnitOfWork.ProcedureExecute("Config_Insurance_Position_Save", param1);
                systemMessage.IsSuccess = true;
                systemMessage.Message = "Cập nhật thành công";
                return systemMessage;
            }
            catch (Exception e)
            {
                systemMessage.IsSuccess = false;
                systemMessage.Message = e.ToString();
                return systemMessage;
            }
            finally
            {
            }
        }
        public SystemMessage AddInsurancePosition(int roleId, int idTable, Config_Insurance_Position insurancePosition)
        {
            SystemMessage systemMessage = new SystemMessage();
            try
            {
                var param1 = new DynamicParameters();
                param1.Add("@AutoID", insurancePosition.AutoID);
                param1.Add("@PositionID", insurancePosition.PositionID);
                param1.Add("@Amount", insurancePosition.Amount);
                param1.Add("@Status", insurancePosition.Status);
                param1.Add("@ApplyDate", insurancePosition.ApplyDate);
                param1.Add("@CreatedBy", 1);
                param1.Add("@Note", insurancePosition.Note);
                UnitOfWork.ProcedureExecute("Config_Insurance_Position_Save", param1);
                systemMessage.IsSuccess = true;
                systemMessage.Message = "Thêm mới thành công";
                return systemMessage;
            }
            catch (Exception e)
            {
                systemMessage.IsSuccess = false;
                systemMessage.Message = e.ToString();
                return systemMessage;
            }

        }
        public SystemMessage DeleteInsurancePosition(int roleId, int idTable, int id)
        {
            SystemMessage systemMessage = new SystemMessage();
            try
            {
                var param = new DynamicParameters();
                param.Add("@AutoID", id);

                var checkExisted = UnitOfWork.Procedure<Config_Insurance_Position>("Config_Insurance_Position_GetByID", param).FirstOrDefault();
                if (checkExisted == null)
                {
                    systemMessage.IsSuccess = false;
                    systemMessage.Message = "Dữ liệu ko tồn tại !";
                    return systemMessage;
                }
                else
                {
                    var param1 = new DynamicParameters();
                    param1.Add("@AutoID", id);
                    param1.Add("@Result");
                    UnitOfWork.ProcedureExecute("Config_Insurance_Position_Delete", param1);
                    systemMessage.IsSuccess = true;
                    systemMessage.Message = "Xóa thành công";
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

        public List<Config_Insurance_Position> ExportExcelInsurancePosition(string filter)
        {
            int total = 0;
            try
            {
                var param = new DynamicParameters();
                param.Add("@FilterField", filter);
                param.Add("@OrderBy", "");
                param.Add("@PageNumber", 1);
                param.Add("@PageSize", 10);
                param.Add("@Type", 2);
                param.Add("@TotalRecord", 0, DbType.Int32, ParameterDirection.InputOutput);
                var list = UnitOfWork.Procedure<Config_Insurance_Position>("Config_Insurance_List", param).ToList();
                var userId = Global.CurrentUser.UserID;
                param = HttpRuntime.Cache.Get("Config_Insurance_List-" + ERP.Framework.Common.Utils.GetKeyFromParam(param as object) + "-" + userId + "-output") as DynamicParameters;
                total = param.GetDataOutput<int>("@TotalRecord");
                return list;
            }
            catch (Exception)
            {
                total = 0;
                return null;
            }
        }
    }
}
