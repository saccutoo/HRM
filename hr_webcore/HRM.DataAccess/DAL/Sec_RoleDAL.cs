using Dapper;
using ERP.Framework.Common;
using ERP.Framework.Data;
using HRM.Common;
using HRM.DataAccess.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace HRM.DataAccess.DAL
{
    public class Sec_RoleDAL : BaseDal<ADOProvider>
    {
        public List<Sys_Table_Column> GetTableColumns(string tableName)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@tableName", tableName);
                var list = UnitOfWork.Procedure<Sys_Table_Column>("GetSys_Table_Column", param,useCache:true).ToList();
                return list;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public Sec_Role GetSecRoleById(int roleId, int idTable, int id)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@id", id);
                return UnitOfWork.Procedure<Sec_Role>("sec_Role_GetByID", param)
                        .FirstOrDefault();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public SystemMessage SaveSecRole(int roleId, int idTable, Sec_Role secRole)
        {
            SystemMessage systemMessage = new SystemMessage();
            try
            {
                //UnitOfWork.ConnectionString = ERP.Framework.Common.Utils.GetSetting<string>("NovaonADConnection");
                var param1 = new DynamicParameters();
                param1.Add("@Name ", secRole.Name);
                param1.Add("@NameEN ", secRole.NameEN);
                param1.Add("@RoleID", secRole.RoleID, DbType.Int32, ParameterDirection.InputOutput);

                UnitOfWork.ProcedureExecute("SecRole_Save", param1);
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
                //UnitOfWork.ConnectionString = null;

            }
        }

        public SystemMessage DeleteSecRole(int roleId, int idTable, int id)
        {
            SystemMessage systemMessage = new SystemMessage();
            try
            {
                var param = new DynamicParameters();
                param.Add("@id", id);
                var checkExisted = UnitOfWork.Procedure<Sec_Role>("sec_Role_GetByID", param).FirstOrDefault();
                if (checkExisted == null)
                {
                    systemMessage.IsSuccess = false;
                    systemMessage.Message = "Dữ liệu ko tồn tại !";
                    return systemMessage;
                }
                else
                {
                    var param1 = new DynamicParameters();
                    param1.Add("@id", id);
                    UnitOfWork.ProcedureExecute("sec_Role_Delete", param1);
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

        public List<Sec_Role> GetSecRole(int pageNumber, int pageSize, string filter, out int total)
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
                param.Add("@TotalRecord", 0, DbType.Int32, ParameterDirection.InputOutput);
                var list = UnitOfWork.Procedure<Sec_Role>("sec_Role_List", param).ToList();
                var userId = Global.CurrentUser.UserID;
                param = HttpRuntime.Cache.Get("sec_Role_List-" + ERP.Framework.Common.Utils.GetKeyFromParam(param as object) + "-" + userId + "-output") as DynamicParameters;
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
        public List<Sec_Role> ExportExcelSecRole(string filter)
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
                var list = UnitOfWork.Procedure<Sec_Role>("sec_Role_List", param).ToList();
                var userId = Global.CurrentUser.UserID;
                param = HttpRuntime.Cache.Get("sec_Role_List-" + ERP.Framework.Common.Utils.GetKeyFromParam(param as object) + "-" + userId + "-output") as DynamicParameters;
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
