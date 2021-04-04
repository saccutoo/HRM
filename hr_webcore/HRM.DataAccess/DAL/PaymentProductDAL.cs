using Dapper;
using ERP.Framework.Common;
using ERP.Framework.Data;
using ERP.Framework.DataBusiness.Common;
using HRM.DataAccess.Common;
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
    public class PaymentProductDAL : BaseDal<ADOProvider>
    {
        public List<PaymentProduct> GetPaymentProduct(BaseListParam listParam, out int? totalRecord,out TableColumnsTotal totalColumns)
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
                param.Add("@Total1", dbType: DbType.Double, direction: ParameterDirection.Output);
                param.Add("@Total2", dbType: DbType.Double, direction: ParameterDirection.Output);
                var list = UnitOfWork.Procedure<PaymentProduct>("PaymentProduct_Gets", param).ToList();
                param = HttpRuntime.Cache.Get("PaymentProduct_Gets-" + ERP.Framework.Common.Utils.GetKeyFromParam(param as object) + "-" + HRM.Common.Global.CurrentUser.UserID + "-output") as DynamicParameters;
                totalRecord = param.GetDataOutput<int>("@TotalRecord");
                totalColumns = new TableColumnsTotal();
                totalColumns.Total1 = param.GetDataOutput<double>("@Total1").ToString();
                totalColumns.Total2 = param.GetDataOutput<double>("@Total2").ToString();
              
                return list;
            }
            catch (Exception ex)
            {
                totalRecord = 0;
                totalColumns = new TableColumnsTotal();
                return new List<PaymentProduct>();
            }


        }
        public List<PaymentProduct> GetPaymentProductReport(BaseListParam listParam, ListFilterParam listFilterParam, out int? totalRecord, out TableColumnsTotal totalColumns)
        {
            try
            {

                var param = new DynamicParameters();
                param.Add("@FilterField", listParam.FilterField);
                param.Add("@OrderByField", listParam.OrderByField);
                param.Add("@PageIndex", listParam.PageIndex);
                param.Add("@PageSize", listParam.PageSize);
                param.Add("@startdate",listFilterParam.FromDate);
                param.Add("@enddate", listFilterParam.ToDate);
                param.Add("@UserId", listParam.UserId);
                param.Add("@RoleId", listParam.UserType);
                param.Add("@DeptId", listParam.DeptId);
                param.Add("@LanguageID", listParam.LanguageCode);
                param.Add("@TotalRecord", 0, DbType.Int32, ParameterDirection.InputOutput);
                param.Add("@Total1", dbType: DbType.Double, direction: ParameterDirection.Output);
                param.Add("@Total2", dbType: DbType.Double, direction: ParameterDirection.Output);
                var list = UnitOfWork.Procedure<PaymentProduct>("PaymentProductReportGets", param).ToList();
                param = HttpRuntime.Cache.Get("PaymentProductReportGets-" + ERP.Framework.Common.Utils.GetKeyFromParam(param as object) + "-" + HRM.Common.Global.CurrentUser.UserID + "-output") as DynamicParameters;
                totalRecord = param.GetDataOutput<int>("@TotalRecord");
                totalColumns = new TableColumnsTotal();
                totalColumns.Total1 = param.GetDataOutput<double>("@Total1").ToString();
                totalColumns.Total2 = param.GetDataOutput<double>("@Total2").ToString();

                return list;
            }
            catch (Exception ex)
            {
                totalRecord = 0;
                totalColumns = new TableColumnsTotal();
                return new List<PaymentProduct>();
            }
        }

        public List<PaymentProductRefer> GetPaymentProductRefer(BaseListParam listParam, ListFilterParam listFilterParam, out int? totalRecord, out TableColumnsTotal totalColumns)
        {
            try
            {

                var param = new DynamicParameters();
                param.Add("@FilterField", listParam.FilterField);
                param.Add("@OrderBy", listParam.OrderByField);
                param.Add("@PageNumber", listParam.PageIndex);
                param.Add("@PageSize", listParam.PageSize);
                param.Add("@UserId", listParam.UserId);
                param.Add("@CurrencyID", 0);
                param.Add("@Branchid", 0);
                param.Add("@IsNewCustomer", 0);
                param.Add("@startdate", listFilterParam.FromDate);
                param.Add("@enddate", listFilterParam.ToDate);
                param.Add("@RoleId", listParam.UserType);
                param.Add("@DepartmentId", listParam.DeptId);
                param.Add("@LanguageID", listParam.LanguageCode);
                param.Add("@TotalRecord", 0, DbType.Int32, ParameterDirection.InputOutput);
                param.Add("@Total1", dbType: DbType.Double, direction: ParameterDirection.Output);
                param.Add("@Total2", dbType: DbType.Double, direction: ParameterDirection.Output);
                param.Add("@Total3", dbType: DbType.Double, direction: ParameterDirection.Output);
                param.Add("@Total4", dbType: DbType.Double, direction: ParameterDirection.Output);
                var list = UnitOfWork.Procedure<PaymentProductRefer>("ReportsMarginRefer", param).ToList();
                param = HttpRuntime.Cache.Get("ReportsMarginRefer-" + ERP.Framework.Common.Utils.GetKeyFromParam(param as object) + "-" + HRM.Common.Global.CurrentUser.UserID + "-output") as DynamicParameters;
                totalRecord = param.GetDataOutput<int>("@TotalRecord");
                totalColumns = new TableColumnsTotal();
                totalColumns.Total1 = param.GetDataOutput<double>("@Total1").ToString();
                totalColumns.Total2 = param.GetDataOutput<double>("@Total2").ToString();
                totalColumns.Total3 = param.GetDataOutput<double>("@Total3").ToString();
                totalColumns.Total4 = param.GetDataOutput<double>("@Total4").ToString();
                return list;
            }
            catch (Exception ex)
            {
                totalRecord = 0;
                totalColumns = new TableColumnsTotal();
                return new List<PaymentProductRefer>();
            }


        }
        public List<Employee> StaffWhereRoleBD()
        {
            try
            {
                var param = new DynamicParameters();
                var list = UnitOfWork.Procedure<Employee>("StaffWhereRoleBD", param,useCache:true).ToList();
                return list;
            }
            catch (Exception ex)
            {
                return new List<Employee>();
            }
        }
        public SystemMessage SavePaymentProduct(BaseListParam listParam, PaymentProduct obj)
        {
            SystemMessage systemMessage = new SystemMessage();
            try
            {
                    var param = new DynamicParameters();
                    param.Add("@AutoID", obj.AutoID);
                    param.Add("@CustomerID", obj.CustomerID);
                    param.Add("@StaffID", obj.StaffID);
                    param.Add("@Status", obj.Status);
                    param.Add("@PaymentDate", obj.PaymentDate);
                    param.Add("@Amount", obj.Amount);
                    param.Add("@ProductID", obj.ProductID);
                    param.Add("@CreatedBy", obj.CreatedBy);
                    param.Add("@OrganizationUnitID", obj.OrganizationUnitID);
                    UnitOfWork.ProcedureExecute("PaymentProduct_Save", param);
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
        public PaymentProduct GetPaymentProductById(BaseListParam listParam, int id,int CustomerID)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@UserID", listParam.UserId);
                param.Add("@Roleid", listParam.UserType);
                param.Add("@LanguageID", listParam.LanguageCode);
                param.Add("@AutoID", id);
                param.Add("@CustomerID", CustomerID);
                return UnitOfWork.Procedure<PaymentProduct>("PaymentProduct_GetInfo", param).FirstOrDefault();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<PaymentProduct> ExportExcelPaymentProduct(BaseListParam listParam, ListFilterParam listFilterParam, out int? totalRecord)
        {

            try
            {
                var param = new DynamicParameters();
                param.Add("@FilterField", listParam.FilterField);
                param.Add("@OrderByField", listParam.OrderByField);
                param.Add("@PageIndex", listParam.PageIndex);
                param.Add("@PageSize", listParam.PageSize);
                param.Add("@startdate", listFilterParam.FromDate);
                param.Add("@enddate", listFilterParam.ToDate);
                param.Add("@UserId", listParam.UserId);
                param.Add("@RoleId", listParam.UserType);
                param.Add("@DeptId", listParam.DeptId);
                param.Add("@LanguageID", listParam.LanguageCode);
                param.Add("@TotalRecord", 0, DbType.Int32, ParameterDirection.InputOutput);
                param.Add("@Total1", dbType: DbType.Double, direction: ParameterDirection.Output);
                param.Add("@Total2", dbType: DbType.Double, direction: ParameterDirection.Output);
                var list = UnitOfWork.Procedure<PaymentProduct>("PaymentProductReportGets", param).ToList();
                param = HttpRuntime.Cache.Get("PaymentProductReportGets-" + ERP.Framework.Common.Utils.GetKeyFromParam(param as object) + "-" + HRM.Common.Global.CurrentUser.UserID + "-output") as DynamicParameters;
                totalRecord = param.GetDataOutput<int>("@TotalRecord");
                return list;
            }
            catch (Exception e)
            {
                totalRecord = 0;
                return new List<PaymentProduct>();
            }
        }

        public List<PaymentProductRefer> ExportExcelPaymentRefer(BaseListParam listParam, ListFilterParam listFilterParam, out int? totalRecord)
        {

            try
            {
                var param = new DynamicParameters();
                param.Add("@FilterField", listParam.FilterField);
                param.Add("@OrderBy", listParam.OrderByField);
                param.Add("@PageNumber", listParam.PageIndex);
                param.Add("@PageSize", listParam.PageSize);
                param.Add("@UserId", listParam.UserId);
                param.Add("@CurrencyID", 194);
                param.Add("@Branchid", 0);
                param.Add("@IsNewCustomer", 0);
                param.Add("@startdate", listFilterParam.FromDate);
                param.Add("@enddate", listFilterParam.ToDate);
                param.Add("@RoleId", listParam.UserType);
                param.Add("@DepartmentId", listParam.DeptId);
                param.Add("@LanguageID", listParam.LanguageCode);
                param.Add("@TotalRecord", 0, DbType.Int32, ParameterDirection.InputOutput);
                param.Add("@Total1", dbType: DbType.Double, direction: ParameterDirection.Output);
                param.Add("@Total2", dbType: DbType.Double, direction: ParameterDirection.Output);
                param.Add("@Total3", dbType: DbType.Double, direction: ParameterDirection.Output);
                param.Add("@Total4", dbType: DbType.Double, direction: ParameterDirection.Output);
                var list = UnitOfWork.Procedure<PaymentProductRefer>("ReportsMarginRefer", param).ToList();
                param = HttpRuntime.Cache.Get("ReportsMarginRefer-" + ERP.Framework.Common.Utils.GetKeyFromParam(param as object) + "-" + HRM.Common.Global.CurrentUser.UserID + "-output") as DynamicParameters;
                totalRecord = param.GetDataOutput<int>("@TotalRecord");
                return list;
            }
            catch (Exception e)
            {
                totalRecord = 0;
                return new List<PaymentProductRefer>();
            }
        }

    }
}
