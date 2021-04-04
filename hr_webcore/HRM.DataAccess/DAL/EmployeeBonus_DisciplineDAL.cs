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
    public class EmployeeBonus_DisciplineDAL : BaseDal<ADOProvider>
    {
        public List<EmployeeBonus_Discipline> GetEmployeeBonus_Discipline(BaseListParam listParam, out int? totalRecord, int staffID,int Type)
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
                param.Add("@Type", Type);
                param.Add("@TotalRecord", 0, DbType.Int32, ParameterDirection.InputOutput);
                var list = UnitOfWork.Procedure<EmployeeBonus_Discipline>("EmployeeBonus_Discipline_Gets", param).ToList();
                param = HttpRuntime.Cache.Get("EmployeeBonus_Discipline_Gets-" + ERP.Framework.Common.Utils.GetKeyFromParam(param as object) + "-" + HRM.Common.Global.CurrentUser.UserID + "-output") as DynamicParameters;
                totalRecord = param.GetDataOutput<int>("@TotalRecord");
                return list;
            }
            catch (Exception ex)
            {
                totalRecord = 0;
                return new List<EmployeeBonus_Discipline>();
            }


        }

        public EmployeeBonus_Discipline GetEmployeeBonus_DisciplineDALById(int roleId, int idTable, int id, int language)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@AutoID", id);
                param.Add("@RoleID", roleId);
                param.Add("@LanguageID", language);
                return UnitOfWork.Procedure<EmployeeBonus_Discipline>("EmployeeBonus_Discipline_GetInfo", param).FirstOrDefault();
            }
            catch (Exception e)
            {
                return null;
            }
        }
        public List<GlobalList> GetActionByParentAndType(int ParentID, int Type)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@ParentID", ParentID);
                param.Add("@Type", Type);
                param.Add("@Result");
                return UnitOfWork.Procedure<GlobalList>("EmployeeBonus_Discipline_GetDropdownByType", param).ToList();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public SystemMessage SaveEmployeeBonusDiscipline(int roleId, int idTable, EmployeeBonus_Discipline obj, int staffID)
        {
            SystemMessage systemMessage = new SystemMessage();

            try
            {
                var param = new DynamicParameters();
                param.Add("@StaffID", staffID);
                param.Add("@Type", obj.Type);
                param.Add("@GroupID", obj.GroupID);
                param.Add("@DecisionNo", obj.DecisionNo);
                param.Add("@Content", obj.Content);
                param.Add("@ActionID", obj.ActionID);
                param.Add("@Amount", obj.Amount);
                param.Add("@CurrencyID", obj.CurrencyID);
                param.Add("@SignDate", obj.SignDate);
                param.Add("@ApplyDate", obj.ApplyDate);
                param.Add("@Note", obj.Note);
                param.Add("@CreatedBy", obj.CreatedBy);
                param.Add("@ModifiedBy", obj.ModifiedBy);
                param.Add("@AutoID", obj.AutoID, DbType.Int32, ParameterDirection.InputOutput);
                UnitOfWork.ProcedureExecute("EmployeeBonus_Discipline_Save", param);
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
        public SystemMessage DeleteEmployeeBonusDiscipline(int roleId, int idTable, int id, int language)
        {
            SystemMessage systemMessage = new SystemMessage();
            try
            {

                var param = new DynamicParameters();
                param.Add("@AutoID", id);
                param.Add("@Roleid", roleId);
                param.Add("@LanguageID", language);
                var checkExisted = UnitOfWork.Procedure<SocialInsurance>("EmployeeBonus_Discipline_GetInfo", param).FirstOrDefault();
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
                    UnitOfWork.ProcedureExecute("EmployeeBonus_Discipline_Delete", param1);
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

        public List<EmployeeBonus_Discipline> ExportExcelEmployeeBonusDiscipline(BaseListParam listParam, out int? totalRecord, int staffID, int Type)
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
                param.Add("@Type", Type);
                param.Add("@TotalRecord", 0, DbType.Int32, ParameterDirection.InputOutput);
                var list = UnitOfWork.Procedure<EmployeeBonus_Discipline>("EmployeeBonus_Discipline_Gets", param).ToList();
                param = HttpRuntime.Cache.Get("EmployeeBonus_Discipline_Gets-" + ERP.Framework.Common.Utils.GetKeyFromParam(param as object) + "-" + HRM.Common.Global.CurrentUser.UserID + "-output") as DynamicParameters;
                totalRecord = param.GetDataOutput<int>("@TotalRecord");
                return list;
            }
            catch (Exception e)
            {
                totalRecord = 0;
                return new List<EmployeeBonus_Discipline>();
            }
        }

        public SystemMessage ImportExcelBonus_Discipline(List<EmployeeBonus_Discipline> Bonus_DisciplineList,int type)
        {

            SystemMessage systemMessage = new SystemMessage();

            try
            {
                if (Bonus_DisciplineList != null)
                {
                    foreach (var item in Bonus_DisciplineList)
                    {
                        var param = new DynamicParameters();
                        param.Add("@AutoID", item.AutoID);
                        param.Add("@StaffCode", item.StaffCode);
                        param.Add("@StaffName", item.StaffName);
                        param.Add("@GroupName", item.GroupName);
                        param.Add("@Content", item.Content);
                        param.Add("@ActionName", item.ActionName);
                        param.Add("@Amount", item.Amount);
                        param.Add("@CurrencyName", item.CurrencyName);
                        param.Add("@DecisionNo", item.DecisionNo);
                        param.Add("@SignDate", item.SignDate);
                        param.Add("@ApplyDate", item.ApplyDate);
                        param.Add("@Type", type);
                        param.Add("@Note", item.Note);
                        UnitOfWork.ProcedureExecute("ImportEmployeeBonus_Discipline", param);
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
