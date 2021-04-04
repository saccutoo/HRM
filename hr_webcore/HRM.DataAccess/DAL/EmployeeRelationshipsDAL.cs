using Dapper;
using ERP.Framework.Common;
using ERP.Framework.Data;
using ERP.Framework.DataBusiness.Common;
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
    public class EmployeeRelationshipsDAL : BaseDal<ADOProvider>
    {
        public List<EmployeeRelationships> GetEmployeeRelationships(BaseListParam listParam, out int? totalRecord, int staffID)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@FilterField", listParam.FilterField);
                param.Add("@OrderByField", listParam.OrderByField);
                param.Add("@PageIndex", listParam.PageIndex);
                param.Add("@PageSize", listParam.PageSize);
                param.Add("@UserId", listParam.UserId);
                param.Add("@RoleId", listParam.UserType);
                param.Add("@DeptId", listParam.DeptId);
                param.Add("@LanguageID", listParam.LanguageCode);
                param.Add("@StaffID", staffID);
                param.Add("@TotalRecord", 0, DbType.Int32, ParameterDirection.InputOutput);
                var list = UnitOfWork.Procedure<EmployeeRelationships>("EmployeeRelationships_Gets", param).ToList();
                param = HttpRuntime.Cache.Get("EmployeeRelationships_Gets-" + ERP.Framework.Common.Utils.GetKeyFromParam(param as object) + "-" + HRM.Common.Global.CurrentUser.UserID + "-output") as DynamicParameters;
                totalRecord = param.GetDataOutput<int>("@TotalRecord");
                return list;
            }
            catch (Exception ex)
            {
                totalRecord = 0;
                return new List<EmployeeRelationships>();
            }


        }
        public EmployeeRelationships GetEmployeeRelationshipsById(int roleId, int idTable, int id,int languageID)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@AutoID", id);
                param.Add("@Roleid", roleId);
                param.Add("@LanguageID", languageID);
                return UnitOfWork.Procedure<EmployeeRelationships>("EmployeeRelationships_GetInfo", param).FirstOrDefault();
            }
            catch (Exception)
            {
                return null;
            }
        }
        public SystemMessage SaveEmployeeRelationships(int roleId, int idTable, EmployeeRelationships obj,int staffID)
        {
            SystemMessage systemMessage = new SystemMessage();

            try
            {
                if (obj.Deduction == false)
                {
                    obj.DeductionCode = null;
                    obj.DeductionFrom = null;
                    obj.DeductionTo = null;
                }
                var param = new DynamicParameters();
                param.Add("@StaffID", staffID);
                param.Add("@BirthDay", obj.BirthDay);
                param.Add("@Deduction", obj.Deduction);
                param.Add("@DeductionCode", obj.DeductionCode);
                param.Add("@DeductionFrom", obj.DeductionFrom);
                param.Add("@DeductionTo", obj.DeductionTo);
                param.Add("@Name", obj.Name);
                param.Add("@Note", obj.Note);
                param.Add("@Phone", obj.Phone);
                param.Add("@Status", obj.Status);
                param.Add("@RelationshipID", obj.RelationshipID);
                param.Add("@CreatedBy", obj.CreatedBy);
                param.Add("@ModifiedBy", obj.ModifiedBy);
                param.Add("@AutoID", obj.AutoID, DbType.Int32, ParameterDirection.InputOutput);
                UnitOfWork.ProcedureExecute("EmployeeRelationships_Save", param);
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
        public SystemMessage DeleteEmployeeRelationships(int roleId, int idTable, int id)
        {
            SystemMessage systemMessage = new SystemMessage();
            try
            {

                var param = new DynamicParameters();
                param.Add("@AutoID", id);
                var checkExisted = UnitOfWork.Procedure<Employee>("EmployeeRelationships_GetInfo", param).FirstOrDefault();
                if (checkExisted == null)
                {
                    systemMessage.IsSuccess = false;
                    return systemMessage;
                }
                else
                {
                    var param1 = new DynamicParameters();
                param1.Add("@AutoID", id);
                param1.Add("@Result");
                UnitOfWork.ProcedureExecute("EmployeeRelationships_Delete", param1);
                systemMessage.IsSuccess = true;
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

        public List<EmployeeRelationships> ExportExcelEmployeeRelationship(BaseListParam listParam, out int? totalRecord, int staffID)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@FilterField", listParam.FilterField);
                param.Add("@OrderByField", listParam.OrderByField);
                param.Add("@PageIndex", listParam.PageIndex);
                param.Add("@PageSize", listParam.PageSize);
                param.Add("@UserId", listParam.UserId);
                param.Add("@RoleId", listParam.UserType);
                param.Add("@DeptId", listParam.DeptId);
                param.Add("@LanguageID", listParam.LanguageCode);
                param.Add("@StaffID", staffID);
                param.Add("@TotalRecord", 0, DbType.Int32, ParameterDirection.InputOutput);
                var list = UnitOfWork.Procedure<EmployeeRelationships>("EmployeeRelationships_Gets", param).ToList();
                param = HttpRuntime.Cache.Get("EmployeeRelationships_Gets-" + ERP.Framework.Common.Utils.GetKeyFromParam(param as object) + "-" + HRM.Common.Global.CurrentUser.UserID + "-output") as DynamicParameters;
                totalRecord = param.GetDataOutput<int>("@TotalRecord");
                return list;
            }
            catch (Exception e)
            {
                totalRecord = 0;
                return new List<EmployeeRelationships>();
            }
        }

        public SystemMessage ImportExcelRelationships(List<EmployeeRelationships> relationshipsList)
        {

            SystemMessage systemMessage = new SystemMessage();

            try
            {
                if (relationshipsList != null)
                {
                    foreach (var item in relationshipsList)
                    {
                        var param = new DynamicParameters();
                        param.Add("@AutoID", item.AutoID);
                        param.Add("@RelationshipName", item.RelationshipName);
                        param.Add("@StaffName", item.StaffName);
                        param.Add("@Name", item.Name);
                        param.Add("@BirthDay", item.BirthDay);
                        param.Add("@Phone", item.Phone);
                        param.Add("@Deduction", item.Deduction);
                        param.Add("@DeductionCode", item.DeductionCode);
                        param.Add("@DeductionFrom", item.DeductionFrom);
                        param.Add("@DeductionTo", item.DeductionTo);
                        param.Add("@OrganizationUnitName", item.OrganizationUnit);
                        param.Add("@StatusName", item.StatusName);
                        param.Add("@Note", item.Note);
                        UnitOfWork.ProcedureExecute("ImportEmployeeRelationship", param);
                    }
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
    }
}
