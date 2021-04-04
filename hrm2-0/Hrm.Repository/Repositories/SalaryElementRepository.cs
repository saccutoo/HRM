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
    public partial class SalaryElementRepository : CommonRepository, ISalaryElementRepository
    {
        public HrmResultEntity<SalaryElementEntity> GetSalaryElement(BasicParamType param, out int totalRecord)
        {
            //FilterField : 
            var par = new DynamicParameters();
            par.Add("@BaseParam", param.ToUserDefinedDataTable(), DbType.Object);
            par.Add("@TotalRecord", 0, DbType.Int32, ParameterDirection.InputOutput);
            var result = ListProcedure<SalaryElementEntity>("SalaryElement_Get_GetSalaryElement", par, dbName: param.DbName);
            if (result.StatusCode != StatusCode.AccessDenied)
            {
                par = HttpRuntime.Cache.Get(param.DbName + "-SalaryElement_Get_GetSalaryElement-" + GetKeyFromParam(par as object) + "-" + CurrentUser.UserId + "-output") as DynamicParameters;
                totalRecord = par.GetDataOutput<int>("@TotalRecord");
            }
            else
                totalRecord = 0;

            return result;
        }
        public HrmResultEntity<SalaryElementEntity> SaveSalaryElement(SalaryElementEntity data, List<LocalizedDataEntity> listData, string dbName)
        {
            var par = new DynamicParameters();
            par.Add("@Id", data.Id);
            par.Add("@Name", data.Name);
            par.Add("@Code", data.Code);
            par.Add("@TypeId", data.TypeId);
            par.Add("@DataTypeId", data.DataTypeId);
            par.Add("@Formula", data.Formula);
            par.Add("@DataFormat", data.DataFormat);
            par.Add("@Css", data.Css);
            par.Add("@OrderNo", data.OrderNo);
            par.Add("@Description", data.Description);
            par.Add("@Value", data.Value);
            par.Add("@IsEdit", data.IsEdit);
            par.Add("@CreatedBy", data.CreatedBy);
            par.Add("@UpdatedBy", data.UpdatedBy);
            par.Add("@DataType", DataType.SalaryElement);
            par.Add("@LanguageId", CurrentUser.LanguageId);
            par.Add("@Localized", listData.ConvertToUserDefinedDataTable(), DbType.Object);
            par.Add("@DbName", dbName);
            return ListProcedure<SalaryElementEntity>("SalaryElement_Update_SalaryElement", par, dbName: dbName);
        }
        public HrmResultEntity<SalaryElementEntity> GetSalaryElementById(long Id, string dbName)
        {
            var par = new DynamicParameters();
            par.Add("@Id", Id);
            par.Add("@DbName", dbName);
            return ListProcedure<SalaryElementEntity>("SalaryElement_Get_GetSalaryElementById", par, dbName: dbName);
        }
        public HrmResultEntity<bool> GetResultSFormular(string sFormularstr, string dbName, out string resultFormularstr, out float resultValue)
        {
            var par = new DynamicParameters();
            par.Add("@SFormularstr", sFormularstr);
            par.Add("@DbName", dbName);
            par.Add("@ResultFormularstr", "", DbType.String, ParameterDirection.InputOutput);
            par.Add("@ResultValue", 0, DbType.Double, ParameterDirection.InputOutput);
            var result = Procedure("System_Salary_Element_GetResultSFormular", par);
            if (result.StatusCode != StatusCode.AccessDenied)
            {
                resultFormularstr = par.GetDataOutput<string>("@ResultFormularstr");
                double outPutDouble = par.GetDataOutput<double>("@ResultValue");
                resultValue = float.Parse(outPutDouble.ToString());
            }
            else
            {
                resultFormularstr = null;
                resultValue = 0;
            }

            return result;
        }
        public HrmResultEntity<SalaryElementEntity> DeleteSalaryElement(long Id, string dbName)
        {
            var par = new DynamicParameters();
            par.Add("@Id", Id);
            par.Add("@DbName", dbName);
            return ListProcedure<SalaryElementEntity>("SalaryElement_Del_DeleteSalaryElementById", par, dbName: dbName);
        }
        public HrmResultEntity<SalaryElementEntity> GetSalaryElementBySalaryTypeId(long Id, string dbName)
        {
            var par = new DynamicParameters();
            par.Add("@Id", Id);
            par.Add("@DbName", dbName);
            return ListProcedure<SalaryElementEntity>("SalaryElement_Get_GetSalaryElementBySalaryTypeId", par, dbName: dbName);
        }
    }
}
