using ERP.Framework.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using ERP.Framework.Data;
using ERP.Framework.DataBusiness.Common;
using HRM.DataAccess.Common;
using HRM.DataAccess.Entity;
using HRM.DataAccess.Entity.UserDefinedType;
using HRM.DataAccess.Helpers;
using System.Web;

namespace HRM.DataAccess.DAL
{
    public class ShareRateSalaryCostDAL : BaseDal<ADOProvider>
    {
        public List<ShareRateSalaryCost> GetShareRateSalaryCostByStaff(BaseListParam listParam, int isExcel, ListFilterParam listFilterParam, out int? totalRecord, out TableColumnsTotal totalColumns)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@FilterField", listParam.FilterField);
                param.Add("@OrderByField", listParam.OrderByField);
                param.Add("@PageNumber", listParam.PageIndex);
                param.Add("@PageSize", listParam.PageSize);
                param.Add("@UserId", listParam.UserId);
                param.Add("@RoleID", listParam.UserType);
                param.Add("@LanguageID", listParam.LanguageCode);
                param.Add("@FromDate", listFilterParam.FromDate);
                param.Add("@ToDate", listFilterParam.ToDate);
                param.Add("@IsExcel", isExcel);
                param.Add("@Total1", dbType: DbType.Double, direction: ParameterDirection.Output);
                param.Add("@TotalRecord", 0, DbType.Int32, ParameterDirection.InputOutput);
                var list = UnitOfWork.Procedure<ShareRateSalaryCost>("GetShareRateSalaryCostByStaff", param).ToList();
                param = HttpRuntime.Cache.Get("GetShareRateSalaryCostByStaff-" + ERP.Framework.Common.Utils.GetKeyFromParam(param as object) + "-" + HRM.Common.Global.CurrentUser.UserID + "-output") as DynamicParameters;
                totalColumns = new TableColumnsTotal();
                totalRecord = param.GetDataOutput<int>("@TotalRecord");
                totalColumns.Total1 = param.GetDataOutput<double>("@Total1").ToString();

                return list;
            }
            catch (Exception ex)
            {
                totalColumns = new TableColumnsTotal();
                totalRecord = 0;
                return new List<ShareRateSalaryCost>();
            }
        }
        public List<ShareRateSalaryCost> GetShareRateSalaryCostByDept(BaseListParam listParam, int isExcel, ListFilterParam listFilterParam, out int? totalRecord, out TableColumnsTotal totalColumns)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@FilterField", listParam.FilterField);
                param.Add("@OrderByField", listParam.OrderByField);
                param.Add("@PageNumber", listParam.PageIndex);
                param.Add("@PageSize", listParam.PageSize);
                param.Add("@UserId", listParam.UserId);
                param.Add("@RoleID", listParam.UserType);
                param.Add("@LanguageID", listParam.LanguageCode);
                param.Add("@FromDate", listFilterParam.FromDate);
                param.Add("@ToDate", listFilterParam.ToDate);
                param.Add("@IsExcel", isExcel);
                param.Add("@Total1", dbType: DbType.Double, direction: ParameterDirection.Output);
                param.Add("@TotalRecord", 0, DbType.Int32, ParameterDirection.InputOutput);
                var list = UnitOfWork.Procedure<ShareRateSalaryCost>("GetShareRateSalaryCostByDept", param).ToList();
                param = HttpRuntime.Cache.Get("GetShareRateSalaryCostByDept-" + ERP.Framework.Common.Utils.GetKeyFromParam(param as object) + "-" + HRM.Common.Global.CurrentUser.UserID + "-output") as DynamicParameters;
                totalColumns = new TableColumnsTotal();
                totalColumns.Total1 = param.GetDataOutput<double>("@Total1").ToString();
                totalRecord = param.GetDataOutput<int>("@TotalRecord");

                return list;
            }
            catch (Exception ex)
            {
                totalColumns = new TableColumnsTotal();
                totalRecord = 0;
                return new List<ShareRateSalaryCost>();
            }
        }

        public SystemMessage Save(BaseListParam listParam, ShareRateSalaryCost obj)
        {
            SystemMessage systemMessage = new SystemMessage();
            try
            {
                var param = new DynamicParameters();
                param.Add("@Id", obj.Id);
                param.Add("@StaffId", obj.StaffId);
                param.Add("@OrganizationUnitId", obj.OrganizationUnitId);
                param.Add("@ShareRate", obj.ShareRate);
                param.Add("@StartDate", obj.StartDate);
                param.Add("@EndDate", obj.EndDate);
                param.Add("@Note", obj.Note);
                param.Add("@Status", obj.Status);
                param.Add("@CompanyId", obj.CompanyId);
                param.Add("@ModifiedDate", obj.ModifiedDate);
                param.Add("@ModifiedBy", obj.ModifiedBy);
                param.Add("@Type", obj.Type);
                param.Add("@Result", 0, DbType.Int32, ParameterDirection.InputOutput);

                UnitOfWork.ProcedureExecute("SaveShareRateSalaryCost", param);
                systemMessage.existedResult = param.GetDataOutput<int>("Result");

                systemMessage.IsSuccess = systemMessage.existedResult != -1;
                return systemMessage;
            }
            catch (Exception e)
            {
                systemMessage.IsSuccess = false;
                systemMessage.Message = e.ToString();
                return systemMessage;
            }
        }
        public SystemMessage SaveByDept(BaseListParam listParam, ShareRateSalaryCost obj)
        {
            SystemMessage systemMessage = new SystemMessage();
            try
            {
                var param = new DynamicParameters();
                param.Add("@Id", obj.Id);
                param.Add("@StaffId", obj.StaffId);
                param.Add("@OrganizationUnitId", obj.OrganizationUnitId);
                param.Add("@ShareRate", obj.ShareRate);
                param.Add("@StartDate", obj.StartDate);
                param.Add("@EndDate", obj.EndDate);
                param.Add("@Note", obj.Note);
                param.Add("@Status", obj.Status);
                param.Add("@CompanyId", obj.CompanyId);
                param.Add("@ModifiedDate", obj.ModifiedDate);
                param.Add("@ModifiedBy", obj.ModifiedBy);
                param.Add("@Result", 0, DbType.Int32, ParameterDirection.InputOutput);

                UnitOfWork.ProcedureExecute("SaveShareRateSalaryCostByDept", param);
                systemMessage.existedResult = param.GetDataOutput<int>("Result");

                systemMessage.IsSuccess = systemMessage.existedResult != -1;
                return systemMessage;
            }
            catch (Exception e)
            {
                systemMessage.IsSuccess = false;
                systemMessage.Message = e.ToString();
                return systemMessage;
            }
        }
        public SystemMessage SaveByStaff(BaseListParam listParam, ShareRateSalaryCost obj)
        {
            SystemMessage systemMessage = new SystemMessage();
            try
            {
                var param = new DynamicParameters();
                param.Add("@Id", obj.Id);
                param.Add("@StaffId", obj.StaffId);
                param.Add("@OrganizationUnitId", obj.OrganizationUnitId);
                param.Add("@ShareRate", obj.ShareRate);
                param.Add("@StartDate", obj.StartDate);
                param.Add("@EndDate", obj.EndDate);
                param.Add("@Note", obj.Note);
                param.Add("@Status", obj.Status);
                param.Add("@CompanyId", obj.CompanyId);
                param.Add("@ModifiedDate", obj.ModifiedDate);
                param.Add("@ModifiedBy", obj.ModifiedBy);
                param.Add("@Result", 0, DbType.Int32, ParameterDirection.InputOutput);

                UnitOfWork.ProcedureExecute("SaveShareRateSalaryCostByStaff", param);
                systemMessage.existedResult = param.GetDataOutput<int>("Result");

                systemMessage.IsSuccess = systemMessage.existedResult != -1;
                return systemMessage;
            }
            catch (Exception e)
            {
                systemMessage.IsSuccess = false;
                systemMessage.Message = e.ToString();
                return systemMessage;
            }
        }

        public ShareRateSalaryCost ShareRateSalaryCostGetByID(BaseListParam listParam, int id, int type)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@UserID", listParam.UserId);
                param.Add("@Roleid", listParam.UserType);
                param.Add("@LanguageID", listParam.LanguageCode);
                param.Add("@Type", type);
                param.Add("@Id", id);
                return UnitOfWork.Procedure<ShareRateSalaryCost>("ShareRateSalaryCostGetByID", param).FirstOrDefault();
            }
            catch (Exception e)
            {
                return null;
            }
        }
        public ShareRateSalaryCost ShareRateSalaryCostStaffGetByID(BaseListParam listParam, int id)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@UserID", listParam.UserId);
                param.Add("@Roleid", listParam.UserType);
                param.Add("@LanguageID", listParam.LanguageCode);
                param.Add("@Id", id);
                return UnitOfWork.Procedure<ShareRateSalaryCost>("ShareRateSalaryCostStaffGetByID", param).FirstOrDefault();
            }
            catch (Exception e)
            {
                return null;
            }
        }
        public ShareRateSalaryCost ShareRateSalaryCostDeptGetByID(BaseListParam listParam, int id)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@UserID", listParam.UserId);
                param.Add("@Roleid", listParam.UserType);
                param.Add("@LanguageID", listParam.LanguageCode);
                param.Add("@Id", id);
                return UnitOfWork.Procedure<ShareRateSalaryCost>("ShareRateSalaryCostDeptGetByID", param).FirstOrDefault();
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public SystemMessage Delete(ShareRateSalaryCost obj)
        {
            SystemMessage systemMessage = new SystemMessage();
            try
            {
                var param = new DynamicParameters();
                param.Add("@Id", obj.Id);
                param.Add("@ModifiedBy", obj.ModifiedBy);
                param.Add("@Type", obj.Type);
                param.Add("@Result", 0, DbType.Int32, ParameterDirection.InputOutput);

                UnitOfWork.ProcedureExecute("DeleteShareRateSalaryCost", param);
                systemMessage.existedResult = param.GetDataOutput<int>("Result");

                systemMessage.IsSuccess = systemMessage.existedResult != -1;
                return systemMessage;

            }
            catch (Exception e)
            {
                systemMessage.IsSuccess = false;
                systemMessage.Message = e.ToString();
                return systemMessage;
            }


        }
        public SystemMessage DeleteShareRateSalaryCostByDept(ShareRateSalaryCost obj)
        {
            SystemMessage systemMessage = new SystemMessage();
            try
            {
                var param = new DynamicParameters();
                param.Add("@Id", obj.Id);
                param.Add("@ModifiedBy", obj.ModifiedBy);
                param.Add("@Result", 0, DbType.Int32, ParameterDirection.InputOutput);

                UnitOfWork.ProcedureExecute("DeleteShareRateSalaryCostByDept", param);
                systemMessage.existedResult = param.GetDataOutput<int>("Result");

                systemMessage.IsSuccess = systemMessage.existedResult != -1;
                return systemMessage;

            }
            catch (Exception e)
            {
                systemMessage.IsSuccess = false;
                systemMessage.Message = e.ToString();
                return systemMessage;
            }


        }
        public SystemMessage DeleteShareRateSalaryCostByStaff(ShareRateSalaryCost obj)
        {
            SystemMessage systemMessage = new SystemMessage();
            try
            {
                var param = new DynamicParameters();
                param.Add("@Id", obj.Id);
                param.Add("@ModifiedBy", obj.ModifiedBy);
                param.Add("@Result", 0, DbType.Int32, ParameterDirection.InputOutput);

                UnitOfWork.ProcedureExecute("DeleteShareRateSalaryCostByStaff", param);
                systemMessage.existedResult = param.GetDataOutput<int>("Result");

                systemMessage.IsSuccess = systemMessage.existedResult != -1;
                return systemMessage;

            }
            catch (Exception e)
            {
                systemMessage.IsSuccess = false;
                systemMessage.Message = e.ToString();
                return systemMessage;
            }


        }

        public List<ShareRateSalaryCostType> ImportShareRateSalaryCostByDept(List<ShareRateSalaryCostType> data, int userId, int languageId, int roleID)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@StaffId", userId);
                param.Add("@RoleID", roleID);
                param.Add("@LanguageId", languageId);
                param.Add("@Data", data.ToUserDefinedDataTable(), DbType.Object);
                var result = UnitOfWork.Procedure<ShareRateSalaryCostType>("ImportShareRateSalaryCostByDept", param);
                if (result != null)
                    return result.ToList();
                else
                    return new List<ShareRateSalaryCostType>();
            }
            catch (Exception ex)
            {
                return new List<ShareRateSalaryCostType>();
            }
        }
        public List<ShareRateSalaryCostType> ImportShareRateSalaryCostByStaff(List<ShareRateSalaryCostType> data, int userId, int languageId, int roleID)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@StaffId", userId);
                param.Add("@RoleID", roleID);
                param.Add("@LanguageId", languageId);
                param.Add("@Data", data.ToUserDefinedDataTable(), DbType.Object);
                var result = UnitOfWork.Procedure<ShareRateSalaryCostType>("ImportShareRateSalaryCostByStaff", param);
                if (result != null)
                    return result.ToList();
                else
                    return new List<ShareRateSalaryCostType>();
            }
            catch (Exception ex)
            {
                return new List<ShareRateSalaryCostType>();
            }
        }
    }
}
