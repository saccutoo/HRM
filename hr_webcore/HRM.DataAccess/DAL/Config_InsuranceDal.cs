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
    public class Config_InsuranceDal : BaseDal<ADOProvider>
    {
        public List<Config_Insurance> GetListInsurance(int pageNumber, int pageSize, string filter, int languageId, out int total)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@FilterField", filter);
                param.Add("@OrderBy", "");
                param.Add("@PageNumber", pageNumber);
                param.Add("@PageSize", pageSize);
                param.Add("@Type", 1);
                param.Add("@LanguageId", languageId);
                param.Add("@TotalRecord", 0, DbType.Int32, ParameterDirection.InputOutput);
                var list = UnitOfWork.Procedure<Config_Insurance>("Config_Insurance_list", param).ToList();
                var userId = Global.CurrentUser.UserID;
                param = HttpRuntime.Cache.Get("Config_Insurance_list-" + ERP.Framework.Common.Utils.GetKeyFromParam(param as object) + "-" + userId + "-output") as DynamicParameters;
                total = param.GetDataOutput<int>("@TotalRecord");
                return list;
            }
            catch (Exception ex)
            {
                total = 0;
                return null;
            }

        }

        public Config_Insurance GetInsuranceById(int roleId, int idTable, int id)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@AutoID", id);
                return UnitOfWork.Procedure<Config_Insurance>("Config_Insurance_GetByID", param)
                        .FirstOrDefault();
            }
            catch (Exception)
            {
                return null;
            }
        } 

        
        public SystemMessage UpdateInsuranceById(int roleId, int idTable, int id, Config_Insurance insuranceVM)
        {


            SystemMessage systemMessage = new SystemMessage();
            try
            {
       
                int status=0;
                var param = new DynamicParameters();
                param.Add("@AutoID", id);
                var checkExisted = UnitOfWork.Procedure<Config_Insurance>("Config_Insurance_GetByID", param).FirstOrDefault();
                if (checkExisted == null)
                {
                    systemMessage.IsSuccess = false;
                    systemMessage.Message = "Dữ liệu ko tồn tại !";
                    return systemMessage;
                }
                var param1 = new DynamicParameters();
                if (insuranceVM.Status == 2015)
                {
                    status = 2017;
                }
                param1.Add("@AutoID", insuranceVM.AutoID);
                param1.Add("@InsuranceTypeID");
                param1.Add("@DecisionCode");
                param1.Add("@Status", status);
                param1.Add("@ApplyDate");
                param1.Add("@RateCompany");
                param1.Add("@RatePerson");
                param1.Add("@CreatedBy");
                UnitOfWork.ProcedureExecute("Config_Insurance_Save", param1);
                systemMessage.IsSuccess = true;
                systemMessage.Message = "Cập nhật thành công";
                return systemMessage;
            }
            catch (Exception e)
            {
                systemMessage.IsSuccess = false;
                systemMessage.Message = e.ToString();
                return systemMessage;
            }
        }
        public SystemMessage AddInsurance(int roleId, int idTable, Config_Insurance insuranceVM)
        {
            SystemMessage systemMessage = new SystemMessage();
            try
            {
                var param1 = new DynamicParameters();
                param1.Add("@AutoID", insuranceVM.AutoID);
                param1.Add("@InsuranceTypeID", insuranceVM.InsuranceTypeID);
                param1.Add("@DecisionCode", insuranceVM.DecisionCode);
                param1.Add("@Status", insuranceVM.Status);
                param1.Add("@ApplyDate", insuranceVM.ApplyDate);
                param1.Add("@RateCompany", insuranceVM.RateCompany);
                param1.Add("@RatePerson", insuranceVM.RatePerson);
                param1.Add("@CreatedBy", insuranceVM.CreatedBy);
                UnitOfWork.ProcedureExecute("Config_Insurance_Save", param1);
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
        public SystemMessage DeleteInsurance(int roleId, int idTable, int id)
        {
            SystemMessage systemMessage = new SystemMessage();
            try
            {
                var param = new DynamicParameters();
                param.Add("@AutoID", id);
               
                var checkExisted = UnitOfWork.Procedure<Config_Insurance>("Config_Insurance_GetByID", param).FirstOrDefault();
                if (checkExisted == null)
                {
                    systemMessage.IsSuccess = false;
                    systemMessage.Message = "Dữ liệu ko tồn tại !";
                    return systemMessage;
                }
                else
                {
                    var param1 = new DynamicParameters();
                    param1.Add("@AutoID", id);
                    param1.Add("@Result");
                    UnitOfWork.ProcedureExecute("Config_Insurance_Delete", param1);
                    systemMessage.IsSuccess = true;
                    systemMessage.Message = "Xóa thành công";
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

        public List<Config_Insurance> ExportExcelInsurance(string filter,int  languageId)
        {
            int total = 0;
            try
            {
                var param = new DynamicParameters();
                param.Add("@FilterField", filter);
                param.Add("@OrderBy", "");
                param.Add("@PageNumber");
                param.Add("@LanguageId", languageId);
                param.Add("@PageSize");
                param.Add("@TotalRecord", 0, DbType.Int32, ParameterDirection.InputOutput);
                var list = UnitOfWork.Procedure<Config_Insurance>("Config_Insurance_list", param).ToList();
                total = param.GetDataOutput<int>("@TotalRecord");
                return list;
            }
            catch (Exception)
            {
                total = 0;
                return null;
            }
        }

        public Config_Insurance GetSumRate()
        {
            try
            {
               
                return UnitOfWork.Procedure<Config_Insurance>("SocialInsuranceDetail_GetSumRate").FirstOrDefault();
            }
            catch (Exception e)
            {
                return null;
            }
         
        }
    }
}
