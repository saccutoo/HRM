using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using ERP.Framework.Common;
using ERP.Framework.Data;
using ERP.Framework.DataBusiness.Common;
using HRM.DataAccess.Entity;
using Newtonsoft.Json;
using System.Collections;
using HRM.Common;
using System.Web;

namespace HRM.DataAccess.DAL
{
    public class ReportAccountCS_DAL : BaseDal<ADOProvider>
    {
        public List<ReportAccountCS> ReportAccountCS_GetAccountNumber(int pageNumber, int pageSize, string filter, DateTime? date, out int total, out int total1)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@FilterField", filter);
                param.Add("@PageNumber", pageNumber);
                param.Add("@PageSize", pageSize);
                param.Add("@Date", date);
                param.Add("@TotalRecord", 0, DbType.Int32, ParameterDirection.InputOutput);
                param.Add("@TotalAccountActive", 0, DbType.Int32, ParameterDirection.InputOutput);
                var list = UnitOfWork.Procedure<ReportAccountCS>("ReportAccountCS_GetAccountNumber", param).ToList();
                total = param.GetDataOutput<int>("@TotalRecord");
                total1 = param.GetDataOutput<int>("@TotalAccountActive");
                return list;
            }
            catch (Exception ex)
            {
                total = 0;
                total1 = 0;
                return null;
            }
            finally
            {

            }

        }

    }

}
