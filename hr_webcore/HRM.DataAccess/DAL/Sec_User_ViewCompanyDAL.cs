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
using HRM.DataAccess.Common;
using ERP.Framework.DataBusiness.Common;

namespace HRM.DataAccess.DAL
{
    public class Sec_User_ViewCompanyDAL : BaseDal<ADOProvider>
    {

        public List<Sec_User_ViewCompany> GetUserByIdCompany(int roleId, int idTable, int id)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@id", id);
                return UnitOfWork.Procedure<Sec_User_ViewCompany>("GetUserByIdCompany", param).ToList();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public SystemMessage SaveSec_User_ViewCompany(int roleId, int idTable, Sec_User_ViewCompany Sec_User_ViewCompany)
        {
            SystemMessage systemMessage = new SystemMessage();
            try
            {
                var param1 = new DynamicParameters();
                param1.Add("@CompanyID ", Sec_User_ViewCompany.CompanyID);
                param1.Add("@UserID ", Sec_User_ViewCompany.UserID);
                UnitOfWork.ProcedureExecute("SaveSec_User_ViewCompany", param1);
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
