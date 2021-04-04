using Dapper;
using ERP.Framework.Common;
using ERP.Framework.Data;
using HRM.Common;
using HRM.DataAccess.Commons;
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
    public class Sys_Table_Role_ActionDal : BaseDal<ADOProvider>
    {
        public List<Sys_Table> GetSys_Table(int pageNumber, int pageSize, string filter, out int total)
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
                var list = UnitOfWork.Procedure<Sys_Table>("Sys_Table_List", param).ToList();
                var userId = Global.CurrentUser.UserID;
                param = HttpRuntime.Cache.Get("Sys_Table_List-" + ERP.Framework.Common.Utils.GetKeyFromParam(param as object) + "-" + userId + "-output") as DynamicParameters;
                total = param.GetDataOutput<int>("@TotalRecord");
                return list;
            }
            catch (Exception ex)
            {
                total = 0;
                return null;
            }

        }

        public List<Sys_Table_Role_Action> GetAll_Sys_Table_Role_ActionToCache()
        {
            var keyCache = CacheUtils.RenderKeyCache();
            //get data from keyCache
            var data = CacheUtils.SqlCacheDependency_Get<List<Sys_Table_Role_Action>>(keyCache);
            if (data == null)
            {
                var dbresult = UnitOfWork.Procedure<Sys_Table_Role_Action>
                    ("Sys_Table_Role_Action_GetAll",useCache:false).ToList();
                //save cache
                CacheUtils.SqlCacheDependency_Insert(new[]
                { "Sys_Table_Role_Action","Sec_Role_User" }, keyCache, dbresult);
                return dbresult;
            }
            else
            {
                return data;
            }
        }

        public List<Sys_Table_Role_Action> Sys_Table_Role_Action_GetByIDRole(int id)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@id", id);
                return UnitOfWork.Procedure<Sys_Table_Role_Action>("Sys_Table_Role_Action_GetByIDRole", param).ToList();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public SystemMessage Add_Sys_Table_Role_Action(int roleId, int idTable, Sys_Table_Role_Action sys)
        {
            SystemMessage systemMessage = new SystemMessage();
            try
            {
                var param1 = new DynamicParameters();
                param1.Add("@RoleId", sys.RoleId);
                param1.Add("@TableId", sys.TableId);
                param1.Add("@isAdd", sys.isAdd);
                param1.Add("@isEdit", sys.isEdit);
                param1.Add("@isDelete", sys.isDelete);
                param1.Add("@isActive", sys.isActive);
                param1.Add("@isFilterButton", sys.isFilterButton);
                param1.Add("@isExcel", sys.isExcel);
                param1.Add("@isSubmit", sys.isSubmit);
                param1.Add("@isApproval", sys.isApproval);
                param1.Add("@isDisApproval", sys.isDisApproval);
                param1.Add("@isCopy", sys.isCopy);
                param1.Add("@isIndex", sys.isIndex);
                param1.Add("@isGet", sys.isGet);
                UnitOfWork.ProcedureExecute("Add_Sys_Table_Role_Action", param1);
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
    }
}
