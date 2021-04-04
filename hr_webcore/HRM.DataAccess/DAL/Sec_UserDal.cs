using Dapper;
using ERP.Framework.Common;
using ERP.Framework.Data;
using ERP.Framework.DataBusiness.Common;
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
    public class Sec_UserDal : BaseDal<ADOProvider>
    {
        public List<Sys_Table_Column> GetTableColumns(string tableName)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@tableName", tableName);
                var list = UnitOfWork.Procedure<Sys_Table_Column>("GetSys_Table_Column", param, useCache: true).ToList();
                return list;
            }
            catch (Exception)
            {
                return null;
            }
        }
        public List<Sec_User> GetSecUser(int pageNumber, int pageSize, string filter, out int total)
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
                var list = UnitOfWork.Procedure<Sec_User>("sec_User_List", param).ToList();
                var userId = Global.CurrentUser.UserID;
                param = HttpRuntime.Cache.Get("sec_User_List-" + ERP.Framework.Common.Utils.GetKeyFromParam(param as object) + "-" + userId + "-output") as DynamicParameters;
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

        public Sec_User GetSec_UserById(int Userid)
        {
            try
            {
                //UnitOfWork.ConnectionString = ERP.Framework.Common.Utils.GetSetting<string>("NovaonADConnection");
                var param = new DynamicParameters();
                param.Add("@Userid", Userid);
                return UnitOfWork.Procedure<Sec_User>("GetById", param)
                        .FirstOrDefault();
            }
            catch (Exception)
            {
                return null;
            }
            finally
            {
                //UnitOfWork.ConnectionString = null;

            }

        }
        public bool DoLogin(string username, string password, out Sec_UserLogin acc)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@UserName", username);
                param.Add("@Password", password);
                var result = UnitOfWork.Procedure<Sec_UserLogin>("sec_User_Authenticated", param, useCache:false).ToList();
                if (result.Any())
                    acc = result.FirstOrDefault();
                else acc = new Sec_UserLogin();
                return acc.UserID > 0;
            }
            catch (Exception)
            {
                acc = new Sec_UserLogin();
                return false;
            }
            finally
            {
                //UnitOfWork.ConnectionString = null;

            }
        }

        public bool DoLoginAll(string username, out Sec_UserLogin acc)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@UserName", username);
                var result = UnitOfWork.Procedure<Sec_UserLogin>("sec_User_Authenticated_list", param, useCache:false).ToList();
                if (result.Any())
                    acc = result.FirstOrDefault();
                else acc = new Sec_UserLogin();
                return acc.UserID > 0;
            }
            catch (Exception)
            {
                acc = new Sec_UserLogin();
                return false;
            }
            finally
            {
                //UnitOfWork.ConnectionString = null;

            }
        }
        public SystemMessage SavePassword(Sec_UserLogin acc, string Password)
        {
            SystemMessage systemMessage = new SystemMessage();
            try
            {
                var param = new DynamicParameters();
                param.Add("@UserID", acc.UserID);
                param.Add("@Password", Password);
                UnitOfWork.ProcedureExecute("SavePassword", param);
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
        public List<Sec_User> GetUserToResetPass()
        {
            try
            {
                var list = UnitOfWork.Procedure<Sec_User>("GetUserToResetPass", useCache: true).ToList();
                return list;
            }
            catch (Exception ex)
            {
                return null;
            }
            finally
            {
                //UnitOfWork.ConnectionString = null;

            }

        }
    }
}
