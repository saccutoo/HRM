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

namespace HRM.DataAccess.DAL
{
    public class Sec_Role_UserDal : BaseDal<ADOProvider>
    {
        public List<Sec_Role> GetIDMenuByIdUser(int roleId, int idTable, int id)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@id", id);
                return UnitOfWork.Procedure<Sec_Role>("sec_Role_User_GetByIDUser", param).ToList();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public SystemMessage AddSecRoleUser(int roleId, int idTable, Sec_Role_User secroleuser)
        {
            SystemMessage systemMessage = new SystemMessage();
            try
            {
                var param1 = new DynamicParameters();
                param1.Add("@RoleID ", secroleuser.RoleID);
                param1.Add("@UserID ", secroleuser.UserID);
                UnitOfWork.ProcedureExecute("sec_Role_User_Insert", param1);
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
