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
    public class Sec_Role_OrganizationunitDAL : BaseDal<ADOProvider>
    {
        public List<OrganizationUnit> GetOrganizationUnitByIdRole(int roleId, int idTable, int id)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@id", id);
                return UnitOfWork.Procedure<OrganizationUnit>("Sec_Role_Organizationunit_GetByIDRole", param).ToList();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public SystemMessage UpdateSec_Role_Organizationunit(int roleId, int idTable, Sec_Role_Organizationunit secrolemenu)
        {
            SystemMessage systemMessage = new SystemMessage();
            try
            {
                var param1 = new DynamicParameters();
                param1.Add("@RoleID ", secrolemenu.RoleID);
                param1.Add("@OrganizationunitID ", secrolemenu.OrganizationunitID);
                UnitOfWork.ProcedureExecute("UpdateSec_Role_Organizationunit", param1);
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
