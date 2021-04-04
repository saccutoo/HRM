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
    public class ReportSourceLeadDAL : BaseDal<ADOProvider>
    {
        public List<ReportSourceLead> Get_CustomerClientSource_BDX(BaseListParam listParam, string startdate, string enddate,int OrganizationUnitID, out int? totalRecord)
        {
            try
            {
                DateTime dt = DateTime.ParseExact(startdate, "dd-MM-yyyy HH:mm:ss", CultureInfo.InvariantCulture);
                startdate = dt.ToString("yyyy-MM-dd HH:mm:ss");
                DateTime dt1 = DateTime.ParseExact(enddate, "dd-MM-yyyy HH:mm:ss", CultureInfo.InvariantCulture);
                enddate = dt1.ToString("yyyy-MM-dd HH:mm:ss");
                var param = new DynamicParameters();
                param.Add("@FilterField", listParam.FilterField);
                param.Add("@OrderByField", listParam.OrderByField);
                param.Add("@PageNumber", listParam.PageIndex);
                param.Add("@PageSize", listParam.PageSize);
                param.Add("@UserType", listParam.UserType);
                param.Add("@UserId", listParam.UserId);
                param.Add("@LanguageId", listParam.LanguageCode);
                param.Add("@startdate", startdate);
                param.Add("@enddate", enddate);
                param.Add("@OrganizationUnitID", OrganizationUnitID);
                param.Add("@TotalRecord", 0, DbType.Int32, ParameterDirection.InputOutput);
                var list = UnitOfWork.Procedure<ReportSourceLead>("Get_CustomerClientSource_BDX", param).ToList();
                param = HttpRuntime.Cache.Get("Get_CustomerClientSource_BDX-" + ERP.Framework.Common.Utils.GetKeyFromParam(param as object) + "-" + HRM.Common.Global.CurrentUser.UserID + "-output") as DynamicParameters;
                totalRecord = param.GetDataOutput<int>("@TotalRecord");
                return list;
            }
            catch (Exception ex)
            {
                totalRecord = 0;
                return new List<ReportSourceLead>();
            }
        }
    }
}
