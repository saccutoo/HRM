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
    public class EmployeeAllowanceDAL : BaseDal<ADOProvider>
    {
        public List<EmployeeAllowance> GetEmployeeAllowance(int pageNumber, int pageSize, string filter, out int total, int LanguageID, int RoleID, int UserID, int DeptID, int wpID)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@FilterField", filter);
                //param.Add("@OrderByField", "");
                //param.Add("@PageIndex", pageNumber);
                param.Add("@OrderBy", "");
                param.Add("@PageNumber", pageNumber);
                param.Add("@PageSize", pageSize);
                param.Add("@type", 1);
                //param.Add("@WPID", wpID);
                //param.Add("@UserId", UserID);
                //param.Add("@RoleId", RoleID);
                //param.Add("@DeptID", DeptID);
                //param.Add("@LanguageID", LanguageID);
                param.Add("@TotalRecord", 0, DbType.Int32, ParameterDirection.InputOutput);
                var list = UnitOfWork.Procedure<EmployeeAllowance>("EmployeeAllowance_List", param).ToList();
                var userId = Global.CurrentUser.UserID;
                param = HttpRuntime.Cache.Get("EmployeeAllowance_List-" + ERP.Framework.Common.Utils.GetKeyFromParam(param as object) + "-" + userId + "-output") as DynamicParameters;
                total = param.GetDataOutput<int>("@TotalRecord");
                return list;
            }
            catch (Exception ex)
            {
                total = 0;
                return null;
            }

        }
    }
}
