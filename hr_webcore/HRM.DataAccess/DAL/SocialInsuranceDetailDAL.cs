using Dapper;
using ERP.Framework.Common;
using ERP.Framework.Data;
using ERP.Framework.DataBusiness.Common;
using HRM.DataAccess.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace HRM.DataAccess.DAL
{
    public class SocialInsuranceDetailDAL : BaseDal<ADOProvider>
    {
        public List<SocialInsuranceDetail> GetSocialInsuranceDetail(BaseListParam listParam, out int? total,int staffID)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@FilterField", listParam.FilterField);
                param.Add("@OrderByField", "");
                param.Add("@PageIndex", listParam.PageIndex);
                param.Add("@PageSize", listParam.PageSize);
                param.Add("@StaffID", staffID);
                param.Add("@UserId", listParam.UserId);
                param.Add("@RoleId", listParam.UserType);
                param.Add("@DeptID", listParam.DeptId);
                param.Add("@LanguageID", listParam.LanguageCode);
                param.Add("@TotalRecord", 0, DbType.Int32, ParameterDirection.InputOutput);
                var list = UnitOfWork.Procedure<SocialInsuranceDetail>("SocialInsuranceDetail_Gets", param).ToList();
                param = HttpRuntime.Cache.Get("SocialInsuranceDetail_Gets-" + ERP.Framework.Common.Utils.GetKeyFromParam(param as object) + "-" + HRM.Common.Global.CurrentUser.UserID + "-output") as DynamicParameters;
                total = param.GetDataOutput<int>("@TotalRecord");
                return list;
            }
            catch (Exception ex)
            {
                total = 0;
                return null;
            }


        }

        public List<SocialInsuranceDetail> GetSocialInsuranceLastID(int staffID)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("StaffID",staffID);
                var list = UnitOfWork.Procedure<SocialInsuranceDetail>("SocialInsuranceDetail_LastID", param,useCache:true).ToList();
                return list;
            }
            catch (Exception ex)
            {
                return null;
            }


        }

        public SystemMessage SaveSocialInsuranceDetail(int roleId, int idTable,SocialInsuranceDetail obj, int staffID)
        {
            SystemMessage systemMessage = new SystemMessage();

            try
            {
                var param1 = new DynamicParameters();

                param1.Add("@FromMonth", obj.FromMonth);
                param1.Add("@StaffID", staffID);
                param1.Add("@AutoID", obj.AutoID);
                param1.Add("@ExistedResult", 0, DbType.Int32, ParameterDirection.InputOutput);
                param1.Add("@ExistedDate", "", DbType.String, ParameterDirection.InputOutput);
                UnitOfWork.ProcedureExecute("SocialInsuranceDetail_CheckFromMonth", param1);
                var existedResult = param1.GetDataOutput<int>("@ExistedResult");
                var existedDate = param1.GetDataOutput<string>("@ExistedDate");
                if (existedResult < 0)
                {
                    systemMessage.IsSuccess = false;
                    systemMessage.existedResult = existedResult;
                    if (existedDate != "")
                    {
                        DateTime tempDate = Convert.ToDateTime(existedDate, CultureInfo.InvariantCulture);
                        systemMessage.ExistedDate = tempDate.ToString().Substring(3, 8);
                    }
                    return systemMessage;
                }
                else
                {
                    var param = new DynamicParameters();
                    param.Add("@StaffID", staffID);
                    param.Add("@InsuranceCode", obj.InsuranceCode);
                    param.Add("@InsuranceNumber", obj.InsuranceNumber);
                    param.Add("@HealthNumber", obj.HealthNumber);
                    param.Add("@FamilyCode", obj.FamilyCode);
                    param.Add("@MonthStart", obj.MonthStart);
                    param.Add("@PlaceHold", obj.PlaceHold);
                    param.Add("@InsuranceID", obj.InsuranceID);
                    param.Add("@FromMonth", obj.FromMonth);
                    param.Add("@ToMonth", obj.ToMonth);
                    param.Add("@Status", obj.Status);
                    param.Add("@BasicSalary", obj.BasicSalary);
                    param.Add("@RateCompany", obj.RateCompany);
                    param.Add("@RatePerson", obj.RatePerson);
                    param.Add("@DateReturn", obj.DateReturn);
                    param.Add("@PlaceHealthCare", obj.PlaceHealthCare);
                    param.Add("@Regime", obj.Regime);
                    param.Add("@ApproveStatus", obj.ApproveStatus);
                    param.Add("@Note", obj.Note);
                    param.Add("@InsuranceID", obj.InsuranceID, DbType.Int32, ParameterDirection.InputOutput);
                    param.Add("@AutoID", obj.AutoID, DbType.Int32, ParameterDirection.InputOutput);
                    UnitOfWork.ProcedureExecute("SocialInsuranceDetail_Save", param);
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

     

        public SystemMessage DeleteSocialInsuranceDetail(int roleId, int idTable, int id ,int language)
        {
            SystemMessage systemMessage = new SystemMessage();
            try
            {

                var param = new DynamicParameters();
                param.Add("@AutoID", id);
                param.Add("@Roleid", roleId);
                param.Add("@LanguageID", language);
                var checkExisted = UnitOfWork.Procedure<SocialInsurance>("SocialInsurance_GetInfo", param).FirstOrDefault();
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
                    UnitOfWork.ProcedureExecute("SocialInsurance_Delete", param1);
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

        public SocialInsuranceDetail GetSocialInsuranceDetailById(int roleId, int idTable, int id, int language)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@AutoID", id);
                param.Add("@RoleID",roleId);
                param.Add("@LanguageID", language);
                return UnitOfWork.Procedure<SocialInsuranceDetail>("SocialInsurance_GetInfo", param).FirstOrDefault();
            }
            catch (Exception e)
            {
                return null;
            }
        }
        public List<SocialInsuranceDetail> ExportExcelSocialInsurance(BaseListParam listParam, out int? totalRecord, int staffID)
        {

            try
            {
                var param = new DynamicParameters();
                param.Add("@FilterField", listParam.FilterField);
                param.Add("@OrderByField", "");
                param.Add("@PageIndex", listParam.PageIndex);
                param.Add("@PageSize", listParam.PageSize);
                param.Add("@StaffID", staffID);
                param.Add("@UserId", listParam.UserId);
                param.Add("@RoleId", listParam.UserType);
                param.Add("@DeptID", listParam.DeptId);
                param.Add("@LanguageID", listParam.LanguageCode);
                param.Add("@TotalRecord", 0, DbType.Int32, ParameterDirection.InputOutput);
                var list = UnitOfWork.Procedure<SocialInsuranceDetail>("SocialInsuranceDetail_Gets", param).ToList();
                param = HttpRuntime.Cache.Get("SocialInsuranceDetail_Gets-" + ERP.Framework.Common.Utils.GetKeyFromParam(param as object) + "-" + HRM.Common.Global.CurrentUser.UserID + "-output") as DynamicParameters;
                totalRecord = param.GetDataOutput<int>("@TotalRecord");
                return list;
            }
            catch (Exception e)
            {
                totalRecord = 0;
                return new List<SocialInsuranceDetail>();
            }
        }

    }
}
