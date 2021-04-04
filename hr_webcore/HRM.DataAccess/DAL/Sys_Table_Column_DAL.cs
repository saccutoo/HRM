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
    public class Sys_Table_Column_DAL : BaseDal<ADOProvider>
    {
        public List<Sys_Table_Column_list> Get_SyS_Table_CoLumn(int pageNumber, int pageSize, string filter, out int total)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@FilterField", "");
                param.Add("@OrderBy", "");
                param.Add("@PageNumber", pageNumber);
                param.Add("@PageSize", pageSize);
                param.Add("@Type", 1);
                param.Add("@TotalRecord", 0, DbType.Int32, ParameterDirection.InputOutput);
                var list = UnitOfWork.Procedure<Sys_Table_Column_list>("Sys_table_column_Role_list", param).ToList();
                var userId = Global.CurrentUser.UserID;
                param = HttpRuntime.Cache.Get("Sys_table_column_Role_list-" + ERP.Framework.Common.Utils.GetKeyFromParam(param as object) + "-" + userId + "-output") as DynamicParameters;
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
            }

        }
        public SystemMessage Save_Sys_Table_Column_Role(/*int RoleId, int TableColumnId*/ List<Sys_Table_Column_Role> data)
        {
            SystemMessage systemMessage = new SystemMessage();
            try
            {
                foreach (var item in data)
                {
                    var param = new DynamicParameters();
                    param.Add("@RoleID", Convert.ToInt32(item.RoleId));
                    param.Add("@TableColumnId", Convert.ToInt32(item.TableColumnId));
                    param.Add("@CreateDate", DateTime.Now);
                    param.Add("@CreateBy", 1);
                    param.Add("@isActive ", true);
                    param.Add("@Order", 1);
                    UnitOfWork.ProcedureExecute("Sys_Table_Column_Role_Insert", param);
                }                
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
        public List<Sys_Table_Column_Role> Sys_Table_Column_Role_GetID(int roleId, int idTable, int id)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@RoleId", id);
                return UnitOfWork.Procedure<Sys_Table_Column_Role>("dbo.Sys_Table_Column_Role_GetID", param,useCache:true).ToList();
            }
            catch (Exception e)
            {
                return null;
            }
            finally
            {
            }
        }
        public List<Sys_Table_Column_Role> GetAllSysTableColumnRole(int RoleId)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@RoleId", RoleId);
                return UnitOfWork.Procedure<Sys_Table_Column_Role>("dbo.Sys_Table_Column_Role_GetID", param, useCache: true).ToList();
            }
            catch (Exception e)
            {
                return null;
            }
            finally
            {
            }
        }
    }
}
