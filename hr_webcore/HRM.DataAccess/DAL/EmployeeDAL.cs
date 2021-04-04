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
using HRM.Common;
using System.Web;

namespace HRM.DataAccess.DAL
{
    public class EmployeeDAL : BaseDal<ADOProvider>
    {
      
        public Employee GetEmployeeById(int roleId, int idTable, int id)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@UserID", id);
                param.Add("@Roleid", roleId);
                return UnitOfWork.Procedure<Employee>("Employee_GetInfo", param).FirstOrDefault();
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public List<HR_WorkingDayMachine> GetWorkingDayMachine()
        {
            try
            {
                return UnitOfWork.Procedure<HR_WorkingDayMachine>("Get_HR_WorkingDayMachine",useCache:true).ToList();
            }
            catch (Exception e)
            {
                return null;
            }
        }
        public List<Employee> GetEmployee(BaseListParam listParam, out int? totalRecord)
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
                param.Add("@TotalRecord", 0, DbType.Int32, ParameterDirection.InputOutput);
                var list = UnitOfWork.Procedure<Employee>("Employee_Gets", param).ToList();
                param = HttpRuntime.Cache.Get("Employee_Gets-" + ERP.Framework.Common.Utils.GetKeyFromParam(param as object) + "-" + HRM.Common.Global.CurrentUser.UserID + "-output") as DynamicParameters;
                totalRecord = param.GetDataOutput<int>("@TotalRecord");
                return list;
            }
            catch (Exception ex)
            {
                totalRecord = 0;
                return new List<Employee>();
            }


        }
        public List<EmloyeeDemoView> GetHRIds()
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@Result");
                return UnitOfWork.Procedure<EmloyeeDemoView>("Employee_getHRids",param).ToList();
            }
            catch (Exception e)
            {
                return null;
            }
        }
        public List<EmloyeeDemoView> GetStaff()
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@Result");
                return UnitOfWork.Procedure<EmloyeeDemoView>("Get_All_Employee", param,useCache:true).ToList();
            }
            catch (Exception e)
            {
                return null;
            }
        }
     
     
        public List<Sec_Role> GetRoleID(BaseListParam listParam)
        {
            try
            {
                //UnitOfWork.ConnectionString = ERP.Framework.Common.Utils.GetSetting<string>("NovaonADConnection");
                var param = new DynamicParameters();
                param.Add("@RoleId", listParam.UserType);
                param.Add("@LanguageId", listParam.LanguageCode);
                param.Add("@UserId", listParam.UserId);
                return UnitOfWork.Procedure<Sec_Role>("Get_RoleId", param,useCache:true).ToList();
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

        public List<Province> GetProvinceByCountry(int countryID)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@CountryID", countryID);
                param.Add("@Result");
                return UnitOfWork.Procedure<Province>("Get_Province_By_Country", param).ToList();
            }
            catch (Exception)
            {
                return null;
            }
            finally
            {
            }
        }

        public SystemMessage SaveEmployee(int roleId, int idTable, Employee obj)
        {
            SystemMessage systemMessage = new SystemMessage();
            
                try
                {
                    var param1 = new DynamicParameters();
                    param1.Add("@StaffCode", obj.StaffCode);
                    param1.Add("@SSN", obj.SSN);
                    param1.Add("@StaffID", obj.StaffID);
                    param1.Add("@UserName", obj.UserName);
                    param1.Add("@ExistedResult", 0, DbType.Int32, ParameterDirection.InputOutput);           
                    UnitOfWork.ProcedureExecute("checkExistedStaffCode_UserName", param1);
                    var existedResult = param1.GetDataOutput<int>("@ExistedResult");
                    if (existedResult < 0)
                    {
                        systemMessage.IsSuccess = false;
                        systemMessage.existedResult = existedResult;
                        return systemMessage;
                    }
                    else
                    {
                    var param = new DynamicParameters();
                        param.Add("@FullNameEN", obj.FullName);
                        param.Add("@imageLink", obj.imageLink);
                        param.Add("@StaffCode", obj.StaffCode);
                        param.Add("@FullName", obj.FullName);
                        param.Add("@GenderID", obj.GenderID);
                        param.Add("@BirthDay", obj.BirthDay);
                        param.Add("@Password", obj.Password);
                        param.Add("@OfficePositionID", obj.OfficePositionID);
                        param.Add("@Address", obj.Address);
                        param.Add("@CountryID", obj.CountryID);
                        param.Add("@ProvinceID", obj.ProvinceID);
                        param.Add("@ContactCountryID", obj.ContactCountryID);
                        param.Add("@ContactAddress", obj.ContactAddress);
                        param.Add("@ContactCountryID", obj.ContactCountryID);
                        param.Add("@ContactProvinceID", obj.ContactProvinceID);
                        param.Add("@StartWorkingDate", obj.StartWorkingDate);
                        param.Add("@OrganizationUnitID", obj.OrganizationUnitID);
                        param.Add("@TrialDate", obj.TrialDate);
                        param.Add("@OfficialDate", obj.OfficialDate);
                        param.Add("@IdentityNumber", obj.IdentityNumber);
                        param.Add("@IDIssuedDate", obj.IDIssuedDate);
                        param.Add("@IDIssuedBy", obj.IDIssuedBy);
                        param.Add("@TaxID", obj.TaxID);
                        param.Add("@TaxDate", obj.TaxDate);
                        param.Add("@TaxBy", obj.TaxBy);
                        param.Add("@BankNumber", obj.BankNumber);
                        param.Add("@BankName", obj.BankName);
                        param.Add("@AccountName", obj.AcountName);
                        param.Add("@Skype", obj.Skype);
                        param.Add("@LinkFacebook", obj.LinkFacebook);
                        param.Add("@ParentID", obj.ParentID);
                        param.Add("@HRIds", obj.HRIds);
                        param.Add("@Mobile", obj.Mobile);
                        param.Add("@EmailPersonal", obj.EmailPersonal);
                        param.Add("@EmailCompany", obj.EmailCompany);
                        param.Add("@ContactName", obj.ContactName);
                        param.Add("@ContactPhone", obj.ContactPhone);
                        param.Add("@AddressOfContact", obj.AddressOfContact);
                        param.Add("@Ds_StatusContractID", obj.Ds_StatusContractID);
                        param.Add("@HoldSaraly", obj.HoldSaraly);
                        param.Add("@Status", obj.Status);
                        param.Add("@PhoneEx", obj.PhoneEx);
                        param.Add("@StaffLevel", obj.StaffLevel);
                        param.Add("@TreeLevel", obj.TreeLevel);
                        param.Add("@HasChild", obj.HasChild);
                        param.Add("@MCCs", obj.MCCs);
                        param.Add("@BMs", obj.BMs);
                        param.Add("@SSN", obj.SSN);
                        param.Add("@WorkingDayMachineID", obj.WorkingDayMachineID);
                        param.Add("@WorkingManagerID", obj.WorkingManagerID);
                        param.Add("@WorkingHRID", obj.WorkingHRID);
                        param.Add("@LastChildBirthday", obj.LastChildBirthday);
                        param.Add("@CommitmentBudgetLimit", obj.CommitmentBudgetLimit);
                        param.Add("@BranchId", obj.BranchId);
                        param.Add("@IsApprovementDirectManager", obj.IsApprovementDirectManager);
                        param.Add("@ModifiedDate", obj.ModifiedDate);
                        param.Add("@ModifiedBy", obj.ModifiedBy);
                        param.Add("@CreatedBy", obj.CreatedBy);
                        param.Add("@Note", obj.Note);
                        param.Add("@RoleID", obj.RoleID);
                        param.Add("@UserName", obj.UserName);
                        param.Add("@IsActivated", obj.IsActivated);
                        param.Add("@OnlyApproveDirectManagerialStaff", obj.OnlyApproveDirectManagerialStaff);
                        param.Add("@StaffID", obj.StaffID, DbType.Int32, ParameterDirection.InputOutput);
                        UnitOfWork.ProcedureExecute("Employee_Save", param);
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

        public SystemMessage DeleteEmployee(int roleId, int idTable, int id)
        {
            SystemMessage systemMessage = new SystemMessage();
            try
            {
              
                //var param = new DynamicParameters();
                //param.Add("@StaffID", id);
                //var checkExisted = UnitOfWork.Procedure<Employee>("globallist_GetByGlobalListID", param).FirstOrDefault();
                //if (checkExisted == null)
                //{
                //    systemMessage.IsSuccess = false;
                //    return systemMessage;
                //}
                //else
                //{
                    var param1 = new DynamicParameters();
                    param1.Add("@StaffID", id);
                    param1.Add("@Result");
                    UnitOfWork.ProcedureExecute("EmployeeDelete", param1);
                    systemMessage.IsSuccess = true;
                    return systemMessage;

                //}
            }
            catch (Exception e)
            {
                systemMessage.IsSuccess = false;
                systemMessage.Message = e.ToString();
                return systemMessage;
            }
        }

        public List<Employee> ExportExcelEmployee(BaseListParam listParam, out int? totalRecord)
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
                param.Add("@LanguageID", listParam.LanguageCode);
                param.Add("@DeptId", listParam.DeptId);
                param.Add("@TotalRecord", 0, DbType.Int32, ParameterDirection.InputOutput);
                var list = UnitOfWork.Procedure<Employee>("Employee_Gets", param).ToList();
                param = HttpRuntime.Cache.Get("Employee_Gets-" + ERP.Framework.Common.Utils.GetKeyFromParam(param as object) + "-" + HRM.Common.Global.CurrentUser.UserID + "-output") as DynamicParameters;
                totalRecord = param.GetDataOutput<int>("@TotalRecord");
                return list;
            }
            catch (Exception e)
            {
                totalRecord = 0;
                return new List<Employee>();
            }
        }

    
    }
}
