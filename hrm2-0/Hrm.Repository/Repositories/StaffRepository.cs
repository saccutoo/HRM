 using Hrm.Common;
using Hrm.Common.Dapper;
using Hrm.Common.Helpers;
using Hrm.Repository.Entity;
using Hrm.Repository.Type;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Hrm.Repository
{
    public partial class StaffRepository : CommonRepository, IStaffRepository
    {
        public HrmResultEntity<StaffEntity> GetStaff(BasicParamType param, out int totalRecord)
        {
            var par = new DynamicParameters();
            par.Add("@BaseParam", param.ToUserDefinedDataTable(), DbType.Object);
            par.Add("@TotalRecord", 0, DbType.Int32, ParameterDirection.InputOutput);
            var result = ListProcedure<StaffEntity>("Staff_Get_GetStaff", par, dbName: param.DbName);
            if (result.StatusCode != StatusCode.AccessDenied)
            {
                par = HttpRuntime.Cache.Get(param.DbName + "-Staff_Get_GetStaff-" + GetKeyFromParam(par as object) + "-" + CurrentUser.UserId + "-output") as DynamicParameters;
                totalRecord = par.GetDataOutput<int>("@TotalRecord");
            }
            else
                totalRecord = 0;

            return result;
        }

        public HrmResultEntity<StaffEntity> GetStaffByOrganizationId(BasicParamType param, long parentId, out int totalRecord)
        {
            var par = new DynamicParameters();
            par.Add("@BaseParam", param.ToUserDefinedDataTable(), DbType.Object);
            par.Add("@ParentId", parentId);
            par.Add("@TotalRecord", 0, DbType.Int32, ParameterDirection.InputOutput);
            var result = ListProcedure<StaffEntity>("Staff_Get_GetStaffByOrganizationId", par, dbName: param.DbName);
            if (result.StatusCode != StatusCode.AccessDenied)
            {
                par = HttpRuntime.Cache.Get(param.DbName + "-Staff_Get_GetStaffByOrganizationId-" + GetKeyFromParam(par as object) + "-" + CurrentUser.UserId + "-output") as DynamicParameters;
                totalRecord = par.GetDataOutput<int>("@TotalRecord");
            }
            else
                totalRecord = 0;

            return result;
        }
        public HrmResultEntity<StaffEntity> GetStaffParentById(long id, DateTime date, string dbName)
        {
            var par = new DynamicParameters();
            par.Add("@StaffId", id);
            par.Add("@Date", date);
            par.Add("@DbName", dbName);
            par.Add("@UserId", CurrentUser.UserId);
            par.Add("@RoleId", CurrentUser.RoleId);
            return ListProcedure<StaffEntity>("Staff_Get_GetStaffParentById", par, dbName: dbName);
        }

        public HrmResultEntity<StaffEntity> GetStaffByParentId(long id, string dbName)
        {
            var par = new DynamicParameters();
            par.Add("@ParentId", id);
            par.Add("@DbName", dbName);
            return ListProcedure<StaffEntity>("Staff_Get_GetStaffByParentId", par, dbName: dbName);
        }

        public HrmResultEntity<StaffEntity> GetStaffInformationById(long staffId, string dbName)
        {
            var par = new DynamicParameters();
            par.Add("@StaffId", staffId);
            par.Add("@DbName", dbName);
            par.Add("@UserID", CurrentUser.UserId);
            par.Add("@RoleId", 1);
            return ListProcedure<StaffEntity>("Staff_Get_GetStaffInformationById", par, dbName: dbName);
        }

        public HrmResultEntity<StaffEntity> GetPipelineStepStaffByMenuName(BasicParamType param, string menuName, out int totalRecord)
        {
            var par = new DynamicParameters();
            par.Add("@BaseParam", param.ToUserDefinedDataTable(), DbType.Object);
            par.Add("@MenuName", menuName);
            par.Add("@TotalRecord", 0, DbType.Int32, ParameterDirection.InputOutput);
            var result = ListProcedure<StaffEntity>("Staff_Get_GetPipelineStepStaffByMenuName", par , dbName: param.DbName);
            if (result.StatusCode != StatusCode.AccessDenied)
            {
                par = HttpRuntime.Cache.Get(param.DbName + "-Staff_Get_GetPipelineStepStaffByMenuName-" + GetKeyFromParam(par as object) + "-" + CurrentUser.UserId + "-output") as DynamicParameters;
                totalRecord = par.GetDataOutput<int>("@TotalRecord");
            }
            else
                totalRecord = 0;

            return result;
        }
        public HrmResultEntity<StaffOnboardInfoEntity> GetPipelineStepStaffByStaffIdAndMenuName(BasicParamType param, string menuName, long staffId)
        {
            var par = new DynamicParameters();
            par.Add("@BaseParam", param.ToUserDefinedDataTable(), DbType.Object);
            par.Add("@MenuName", menuName);
            par.Add("@StaffId", staffId);
            par.Add("@TotalRecord", 0, DbType.Int32, ParameterDirection.InputOutput);
            return  ListProcedure<StaffOnboardInfoEntity>("Staff_Get_GetPipelineStepStaffByStaffIdAndMenuName", par, dbName: param.DbName);
        }
        public HrmResultEntity<StaffEntity> GetAllStaffForDropDown(BasicParamType param)
        {
            var par = new DynamicParameters();
            par.Add("@BaseParam", param.ToUserDefinedDataTable(), DbType.Object);
            return ListProcedure<StaffEntity>("Staff_Get_GetAllStaffForDropDown", par, dbName: param.DbName);
        }
        public HrmResultEntity<UserEntity> GetDataUserByUserName(string userName, string dbName)
        {
            var par = new DynamicParameters();
            par.Add("@UserName", userName);
            par.Add("@DbName", dbName);
            par.Add("@UserID", CurrentUser.UserId);
            par.Add("@RoleId", 1);
            return ListProcedure<UserEntity>("Staff_Get_GetDataUserByUserName", par, dbName: dbName);
        }
        public HrmResultEntity<UserEntity> CheckStaffInfoByStaffCode(string staffCode, string dbName)
        {
            var par = new DynamicParameters();
            par.Add("@StaffCode", staffCode);
            par.Add("@DbName", dbName);
            par.Add("@UserID", CurrentUser.UserId);
            par.Add("@RoleId", 1);
            return ListProcedure<UserEntity>("Staff_Get_CheckStaffInfoByStaffCode", par, dbName: dbName);
        }

        public HrmResultEntity<StaffEntity> SearchPermissionStaff(string searchKey, string dbName)
        {
            var par = new DynamicParameters();
            par.Add("@SearchKey", searchKey);
            par.Add("@DbName", dbName);
            return ListProcedure<StaffEntity>("Staff_Get_SearchPermissionStaff", par, dbName: dbName);
        }
        #region staff benefit
        public HrmResultEntity<StaffBenefitsEntity> GetStaffBenefitByStaff(BasicParamType param, long staffId)
        {

            var par = new DynamicParameters();
            par.Add("@BaseParam", param.ToUserDefinedDataTable(), DbType.Object);
            par.Add("@StaffId", staffId, DbType.Int32);
            return ListProcedure<StaffBenefitsEntity>("Staff_Get_GetStaffBenefitByStaff", par, dbName: param.DbName);
        }
        public HrmResultEntity<StaffBenefitsEntity> GetStaffBenefitByWorkingprocess(BasicParamType param, long workingprocessId)
        {
            var par = new DynamicParameters();
            par.Add("@BaseParam", param.ToUserDefinedDataTable(), DbType.Object);
            par.Add("@WorkingprocessId", workingprocessId, DbType.Int32);
            return ListProcedure<StaffBenefitsEntity>("Staff_Get_GetStaffBenefitByWorkingprocess", par, dbName: param.DbName);
        }
        public HrmResultEntity<StaffBenefitsEntity> GetStaffBenefitById(BasicParamType param, long id)
        {
            var par = new DynamicParameters();
            par.Add("@Id", id);
            par.Add("@DbName", param.DbName);
            par.Add("@UserID", param.UserId);
            par.Add("@RoleID", param.RoleId);
            return ListProcedure<StaffBenefitsEntity>("Staff_Get_GetStaffBenefitById", par, dbName: param.DbName);
        }
        #endregion
        #region staff Allowance
        public HrmResultEntity<StaffAllowanceEntity> GetStaffAllowanceByStaff(BasicParamType param, long staffId)
        {
           
                var par = new DynamicParameters();
                par.Add("@BaseParam", param.ToUserDefinedDataTable(), DbType.Object);
                par.Add("@StaffId", staffId, DbType.Int32);
                return ListProcedure<StaffAllowanceEntity>("Staff_Get_GetStaffAllowanceByStaff", par, dbName: param.DbName);
             
          
        }
        public HrmResultEntity<StaffAllowanceEntity> GetStaffAllowanceByWorkingprocess(BasicParamType param, long workingprocessId)
        {

            var par = new DynamicParameters();
            par.Add("@BaseParam", param.ToUserDefinedDataTable(), DbType.Object);
            par.Add("@WorkingprocessId", workingprocessId, DbType.Int32);
            return ListProcedure<StaffAllowanceEntity>("Staff_Get_GetStaffAllowanceByWorkingprocess", par, dbName: param.DbName);


        }
        public HrmResultEntity<StaffAllowanceEntity> GetStaffAllowanceById(BasicParamType param, long id)
        {
            var par = new DynamicParameters();
            par.Add("@Id", id);
            par.Add("@DbName", param.DbName);
            par.Add("@UserID", param.UserId);
            par.Add("@RoleID", param.RoleId);
            return ListProcedure<StaffAllowanceEntity>("Staff_Get_GetStaffAllowanceById", par, dbName: param.DbName);
        }
        #endregion
        #region Staff Experience
        public HrmResultEntity<StaffExperienceEntity> GetStaffExperience(BasicParamType param, long staffId)
        {
            
                var par = new DynamicParameters();
                par.Add("@BaseParam", param.ToUserDefinedDataTable(), DbType.Object);
                par.Add("@StaffId", staffId, DbType.Int32);
                return ListProcedure<StaffExperienceEntity>("Staff_Get_GetStaffExperience", par, dbName: param.DbName);
        }
        public HrmResultEntity<StaffExperienceEntity> GetStaffExperienceById(BasicParamType param, long id)
        {
            var par = new DynamicParameters();
            par.Add("@Id", id);
            par.Add("@DbName", param.DbName);
            par.Add("@UserID", param.UserId);
            par.Add("@RoleID", param.RoleId);
            return ListProcedure<StaffExperienceEntity>("Staff_Get_GetStaffExperienceById", par, dbName: param.DbName);
        }
        public HrmResultEntity<StaffExperienceEntity> SaveExperience(StaffExperienceEntity data, BasicParamType param)
        {
            var par = new DynamicParameters();
            par.Add("@Id", data.Id);
            par.Add("@StaffId", data.StaffId);
            par.Add("@FromDate", data.FromDate);
            par.Add("@ToDate", data.ToDate);
            par.Add("@OfficePositionID", data.OfficePositionID);
            par.Add("@OfficeRoleID", data.OfficeRoleID);
            par.Add("@CompanyName", data.CompanyName);
            par.Add("@Salary", data.Salary);
            par.Add("@CurrencyId", data.CurrencyId);
            par.Add("@Note", data.Note);
            par.Add("@BaseParam", param.ToUserDefinedDataTable(), DbType.Object);
            var result = ListProcedure<StaffExperienceEntity>("Staff_Update_SaveExperience", par, useCache: false);
            return result;
        }
        #endregion
        #region Staff Certificate
        public HrmResultEntity<StaffCertificateEntity> GetStaffCertificate(BasicParamType param, long staffId)
        {
           
                var par = new DynamicParameters();
                par.Add("@BaseParam", param.ToUserDefinedDataTable(), DbType.Object);
                par.Add("@StaffId", staffId, DbType.Int32);
                return ListProcedure<StaffCertificateEntity>("Staff_Get_GetStaffCertificate", par, dbName: param.DbName);
        }
        public HrmResultEntity<StaffCertificateEntity> GetStaffCertificateById(BasicParamType param, long id)
        {
            var par = new DynamicParameters();
            par.Add("@Id", id);
            par.Add("@DbName", param.DbName);
            par.Add("@UserID", param.UserId);
            par.Add("@RoleID", param.RoleId);
            return ListProcedure<StaffCertificateEntity>("Staff_Get_GetStaffCertificateById", par, dbName: param.DbName);
        }
        public HrmResultEntity<StaffCertificateEntity> SaveCertificate(StaffCertificateEntity data, BasicParamType param)
        {
            var par = new DynamicParameters();
            par.Add("@Id", data.Id);
            par.Add("@StaffId", data.StaffId);
            par.Add("@Name", data.Name);
            par.Add("@IssuedDate", data.IssuedDate);
            par.Add("@RankId", data.RankId);
            par.Add("@IssuedBy", data.IssuedBy);
            par.Add("@Point", data.Point);
            par.Add("@Note", data.Note);
            par.Add("@BaseParam", param.ToUserDefinedDataTable(), DbType.Object);
            var result = ListProcedure<StaffCertificateEntity>("Staff_Update_SaveCertificate", par, useCache: false);
            return result;
        }
        #endregion
        #region Staff Insurance
        public HrmResultEntity<StaffSocialInsuranceEntity> GetStaffSocialInsurance(BasicParamType param, long staffId)
        {
                var par = new DynamicParameters();
                par.Add("@BaseParam", param.ToUserDefinedDataTable(), DbType.Object);
                par.Add("@StaffId", staffId, DbType.Int32);
                return ListProcedure<StaffSocialInsuranceEntity>("Staff_Get_GetStaffSocialInsurance", par, dbName: param.DbName);
          
          
        }
        public HrmResultEntity<HealthInsuranceEntity> GetStaffHealthInsurance(BasicParamType param, long staffId)
        {
          
                var par = new DynamicParameters();
                par.Add("@BaseParam", param.ToUserDefinedDataTable(), DbType.Object);
                par.Add("@StaffId", staffId, DbType.Int32);
                return ListProcedure<HealthInsuranceEntity>("Staff_Get_GetStaffHealthInsurance", par, dbName: param.DbName);
        }
        public HrmResultEntity<HealthInsuranceEntity> SaveHealthInsurance(HealthInsuranceEntity data, BasicParamType param)
        {
            var par = new DynamicParameters();
            par.Add("@Id", data.Id);
            par.Add("@StaffId", data.StaffId);
            par.Add("@InsuranceCode", data.InsuranceCode);
            par.Add("@TypeId", data.TypeId);
            par.Add("@PlaceHealthCare", data.PlaceHealthCare);
            par.Add("@IssuedDate", data.IssuedDate);
            par.Add("@StartDate", data.StartDate);
            par.Add("@EndDate", data.EndDate);
            par.Add("@Note", data.Note);
            par.Add("@BaseParam", param.ToUserDefinedDataTable(), DbType.Object);
            var result = ListProcedure<HealthInsuranceEntity>("Staff_Update_SaveHealthInsurance", par, useCache: false);
            return result;
        }
        public HrmResultEntity<StaffSocialInsuranceEntity> SaveSocialInsurance(StaffSocialInsuranceEntity data, BasicParamType param)
        {
            var par = new DynamicParameters();
            par.Add("@Id", data.Id);
            par.Add("@StaffId", data.StaffId);
            par.Add("@TypeId", data.TypeId);
            par.Add("@InsuranceCode", data.InsuranceCode);
            par.Add("@FamilyCode", data.FamilyCode);
            par.Add("@PlaceHold", data.PlaceHold);
            par.Add("@UnionAmount", data.UnionAmount);
            par.Add("@CurrencyId", data.CurrencyId);
            //par.Add("@DateReturn", data.DateReturn);
            par.Add("@DateReturn", data.DateReturn);
            par.Add("@StartDate", data.StartDate);
            par.Add("@EndDate", data.EndDate);
            par.Add("@Note", data.Note);
            par.Add("@BaseParam", param.ToUserDefinedDataTable(), DbType.Object);
            var result = ListProcedure<StaffSocialInsuranceEntity>("Staff_Update_SaveSocialInsurance", par, useCache: false);
            return result;
        }
        public HrmResultEntity<HealthInsuranceEntity> GetHealthInsuranceByStaff(BasicParamType param, long staffId)
        {
            var par = new DynamicParameters();
            par.Add("@BaseParam", param.ToUserDefinedDataTable(), DbType.Object);
            par.Add("@StaffId", staffId, DbType.Int32);
            return ListProcedure<HealthInsuranceEntity>("Staff_Get_GetHealthInsuranceByStaff", par, dbName: param.DbName);
        }
        public HrmResultEntity<StaffSocialInsuranceEntity> GetSocialInsuranceByStaff(BasicParamType param, long staffId)
        {
            var par = new DynamicParameters();
            par.Add("@BaseParam", param.ToUserDefinedDataTable(), DbType.Object);
            par.Add("@StaffId", staffId, DbType.Int32);
            return ListProcedure<StaffSocialInsuranceEntity>("Staff_Get_GetSocialInsuranceByStaff", par, dbName: param.DbName);
        }
        #endregion
        #region staff contract
        public HrmResultEntity<long> SaveContract(List<StaffContractType> contract, BasicParamType param)
        {
            var par = new DynamicParameters();
            par.Add("@Contract", contract.ConvertToUserDefinedDataTable());
            par.Add("@DbName", param.DbName);
            par.Add("@UserId", param.UserId);
            par.Add("@Id", 0, DbType.Int64, ParameterDirection.InputOutput);
            var result = ListProcedure <long>("Staff_Update_SaveContract", par, useCache: false);
            return result;
        }
        public HrmResultEntity<StaffContractEntity> GetCurrentStaffContractByStaff(BasicParamType param, long staffId)
        {
            var par = new DynamicParameters();
            par.Add("@BaseParam", param.ToUserDefinedDataTable(), DbType.Object);
            par.Add("@StaffId", staffId);
            return ListProcedure<StaffContractEntity>("Staff_Get_GetCurrentStaffContractByStaff", par, dbName: param.DbName);
        }
        public HrmResultEntity<StaffContractEntity> GetStaffContractById(BasicParamType param, long id)
        {
            var par = new DynamicParameters();
            par.Add("@Id", id);
            par.Add("@DbName", param.DbName);
            par.Add("@UserID", param.UserId);
            par.Add("@RoleID", param.RoleId);
            return ListProcedure<StaffContractEntity>("Staff_Get_GetStaffContractById", par, dbName: param.DbName);
        }
        #endregion

        #region Save Staff
        public HrmResultEntity<bool> SaveStaff(StaffEntity data, BasicParamType param)
        {
            var par = new DynamicParameters();
            par.Add("@StaffCode" , data.StaffCode);
            par.Add("@Name", data.Name);
            par.Add("@Birthday", data.Birthday);
            par.Add("@GenderId", data.GenderId);
            par.Add("@CurrentWorkingProcessId", data.CurrentWorkingProcessId);
            par.Add("@Email", data.Email);
            par.Add("@Phone", data.Phone);
            par.Add("@PhoneCompany", data.PhoneCompany);
            par.Add("@Skype", data.Skype);
            par.Add("@IdentityNumber", data.IdentityNumber);
            par.Add("@IdIssuedDate", data.IdIssuedDate);
            par.Add("@IdIssuedBy",data.IdIssuedBy) ;
    
            par.Add("@TaxId", data.TaxId);
            par.Add("@TaxDate", data.TaxDate);
            par.Add("@TaxBy", data.TaxBy);
            par.Add("@UserName", data.UserName);
            par.Add("@BankNumber", data.BankNumber);
            par.Add("@BankName", data.BankName);
            par.Add("@BankBranch", data.BankBranch);
            par.Add("@Address", data.Address);
            par.Add("@ContactAddress", data.ContactAddress);
            par.Add("@NationalId", data.NationalId);
            par.Add("@LinkFacebook", data.LinkFacebook);
            par.Add("@ImageLink", data.ImageLink);
            par.Add("@EmailCompany", data.EmailCompany);
            par.Add("@PresentationContactName", data.PresentationContactName);
            par.Add("@PresentationContactPhone", data.PresentationContactPhone);
            par.Add("@Note", data.Note);
            par.Add("@IsSendCheckList", data.IsSendChecklist);
            par.Add("@SendCheckListDate", data.SendCheckListDate);
            par.Add("@SendCheckListBy", data.SendCheckListBy);
            par.Add("@IsDeleted", data.IsDeleted);
            par.Add("@EthnicityId", data.EthnicityId);
            par.Add("@MaritalStatus", data.MaritalStatus);
            par.Add("@IsOnboarding", data.IsOnboarding);
            par.Add("@OnboardingDate", data.OnboardingDate);
            par.Add("@CreatedBy", param.UserId);
            par.Add("@DbName", param.DbName);
            par.Add("@Id", data.Id, DbType.Int32, ParameterDirection.InputOutput);
            var result = Procedure("Staff_Update_SaveShortStaff", par);
            return result;
        }
        public HrmResultEntity<StaffEntity> SaveStaffFull(List<StaffType> staff, List<WorkingProcessType> workingPorcess, List<StaffContractType> contract, List<StaffRoleType> role, StaffOnboardInfoType staffOnboardInfo
            , List<StaffWorkingDayMachineType> staffWorkingDayMachine, List<StaffRelationshipsType> staffRelationship,
            List<StaffAllowanceType> staffAllowance, List<StaffBenefitsType> staffBenefit, BasicParamType param)
        {
            var par = new DynamicParameters();
            par.Add("@Staff", staff.ConvertToUserDefinedDataTable(), DbType.Object);
            par.Add("@WorkingPorcess", workingPorcess.ConvertToUserDefinedDataTable(), DbType.Object);
            par.Add("@Contract", contract.ConvertToUserDefinedDataTable(), DbType.Object);
            par.Add("@Role", role.ConvertToUserDefinedDataTable(), DbType.Object);
            par.Add("@StaffOnboardInfo", staffOnboardInfo.ToUserDefinedDataTable(), DbType.Object);
            par.Add("@StaffWorkingDayMachine", staffWorkingDayMachine.ConvertToUserDefinedDataTable(), DbType.Object);
            par.Add("@StaffRelationship", staffRelationship.ConvertToUserDefinedDataTable(), DbType.Object);
            par.Add("@StaffAllowance", staffAllowance.ConvertToUserDefinedDataTable(), DbType.Object);
            par.Add("@StaffBenefit", staffBenefit.ConvertToUserDefinedDataTable(), DbType.Object);
            par.Add("@BaseParam", param.ToUserDefinedDataTable(), DbType.Object);
            var result = ListProcedure<StaffEntity>("Staff_Update_SaveStaffFull", par, useCache : true);
            return result;
        }
        public HrmResultEntity<StaffEntity> SaveStaffInformation(List<StaffType> staff, List<StaffRoleType> role, List<StaffRelationshipsType> staffRelationship, List<StaffWorkingDayMachineType> staffWorkingDayMachine, BasicParamType param)
        {
            var par = new DynamicParameters();
            par.Add("@Staff", staff.ConvertToUserDefinedDataTable(), DbType.Object);
            par.Add("@Role", role.ConvertToUserDefinedDataTable(), DbType.Object);
            par.Add("@StaffRelatioship", staffRelationship.ConvertToUserDefinedDataTable(), DbType.Object);
            par.Add("@StaffWorkingDayMachine", staffWorkingDayMachine.ConvertToUserDefinedDataTable(), DbType.Object);
            par.Add("@BaseParam", param.ToUserDefinedDataTable(), DbType.Object);
            var result = ListProcedure<StaffEntity>("Staff_Update_SaveStaffInformation", par, useCache : true);
            return result;
        }
        public HrmResultEntity<bool> SaveStaffChecklist(List<Type.ChecklistDetailType> type,BaseEntity staff, string dbName)
        {
            var par = new DynamicParameters();
            par.Add("@StaffId", staff.Id);
            par.Add("@CreatedBy", staff.CreatedBy);
            par.Add("@UpdatedBy", staff.UpdatedBy);
            par.Add("@DbName", dbName);
            par.Add("@ChecklistDetailType", type.ConvertToUserDefinedDataTable(), DbType.Object);
            return  Procedure("Staff_Update_SaveStaffChecklist", par);
        }
        #endregion
        public HrmResultEntity<StaffBonusDisciplineEntity> SaveBonusDiscipline(StaffBonusDisciplineEntity data, BasicParamType param)
        {
            var par = new DynamicParameters();
            par.Add("@Id", data.Id);
            par.Add("@StaffId", data.StaffId);
            par.Add("@TypeId", data.TypeId);
            par.Add("@GroupId", data.GroupId);
            par.Add("@DecisionNo", data.DecisionNo);
            par.Add("@Content", data.Content);
            par.Add("@ActionId", data.ActionId);
            par.Add("@Amount", data.Amount);
            par.Add("@CurrencyId", data.CurrencyId);
            par.Add("@SignDate", data.SignDate);
            par.Add("@ApplyDate", data.ApplyDate);
            par.Add("@Note", data.Note);
            par.Add("@Status", data.Status);
            par.Add("@PaySamePeriod", data.PaySamePeriod);
            par.Add("@BaseParam", param.ToUserDefinedDataTable(), DbType.Object);
            var result = ListProcedure<StaffBonusDisciplineEntity>("Staff_Update_SaveBonusDiscipline", par, useCache: false);
            return result;
        }
        public HrmResultEntity<StaffEntity> SearchStaff(BasicParamType param, string searchKey, out int totalRecord)
        {
            var par = new DynamicParameters();
            par.Add("@BaseParam", param.ToUserDefinedDataTable(), DbType.Object);
            par.Add("@SearchKey", searchKey);
            par.Add("@TotalRecord", 0, DbType.Int32, ParameterDirection.InputOutput);
            var result = ListProcedure<StaffEntity>("System_Get_GlobalSearchStaff", par, dbName: param.DbName);
            if (result.StatusCode != StatusCode.AccessDenied)
            {
                par = HttpRuntime.Cache.Get(param.DbName + "-System_Get_GlobalSearchStaff-" + GetKeyFromParam(par as object) + "-" + CurrentUser.UserId + "-output") as DynamicParameters;
                totalRecord = par.GetDataOutput<int>("@TotalRecord");
            }
            else
                totalRecord = 0;

            return result;
        }
        public HrmResultEntity<bool> SaveStatusPipeline(PipelineStepEntity pipelineStep,long staffId,string dbName)
        {
            var par = new DynamicParameters();
            par.Add("@StaffId", staffId);
            par.Add("@PipelineId", pipelineStep.PipelineId);
            par.Add("@PipelineStepId", pipelineStep.Id);
            par.Add("@Date", pipelineStep.PipelineDate);
            par.Add("@Note", "");
            par.Add("@IsDeleted", pipelineStep.IsDeleted);
            par.Add("@DbName", dbName);
            par.Add("@Id", pipelineStep.PipelineStaffStatusId,DbType.Int32, ParameterDirection.InputOutput);
            return Procedure("Data_Staff_PipelineStatus_Update_Staff_PipelineStatus", par);
        }
        public HrmResultEntity<bool> SavePassword(BasicParamType param, string newPassword, int staffId)
        {
            var par = new DynamicParameters();
            par.Add("@BaseParam", param.ToUserDefinedDataTable(), DbType.Object);
            par.Add("@NewPassword", newPassword);
            par.Add("@StaffId", staffId);
            par.Add("@Result", 0, DbType.Int32, ParameterDirection.InputOutput);
            return Procedure("Password_Update_SavePassword", par);
        }
        public HrmResultEntity<StaffWorkingDayMachineEntity> GetStaffWorkingDayMachineByStaff(BasicParamType param, long staffId, out int totalRecord)
        {

            var par = new DynamicParameters();
            par.Add("@BaseParam", param.ToUserDefinedDataTable(), DbType.Object);
            par.Add("@StaffId", staffId, DbType.Int32);
            par.Add("@TotalRecord", 0, DbType.Int32, ParameterDirection.InputOutput);
            var result = ListProcedure<StaffWorkingDayMachineEntity>("Staff_Get_GetStaffWorkingDayMachineByStaff", par, dbName: param.DbName);
            if (result.StatusCode != StatusCode.AccessDenied)
            {
                par = HttpRuntime.Cache.Get(param.DbName + "-Staff_Get_GetStaffWorkingDayMachineByStaff-" + GetKeyFromParam(par as object) + "-" + CurrentUser.UserId + "-output") as DynamicParameters;
                totalRecord = par.GetDataOutput<int>("@TotalRecord");
            }
            else
                totalRecord = 0;

            return result;
        }
        public HrmResultEntity<bool> CheckNextPipeLineStep(BasicParamType param, long pipeLineSteffId, long staffId, out bool result)
        {
            var par = new DynamicParameters();
            par.Add("@PipelineStepIdNew", pipeLineSteffId);
            par.Add("@StaffId", staffId);
            par.Add("@DbName", param.DbName);
            par.Add("@UserID", param.UserId);
            par.Add("@RoleID", param.RoleId);
            par.Add("@Result", 0, DbType.Boolean, ParameterDirection.InputOutput);
            var response = ListProcedure<bool>("Staff_Get_CheckNextPipeLineStep", par);
            result = par.GetDataOutput<bool>("@Result");
            return response;
        }
    }
}
