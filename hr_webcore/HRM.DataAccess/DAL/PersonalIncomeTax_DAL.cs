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
    public class PersonalIncomeTax_DAL : BaseDal<ADOProvider>
    {
        public List<PersonalIncomeTax> PersonalIncomeTax_GetList(int pageNumber, int pageSize, string filter,int languageCode, out int total)
        {
            try
            {
                //UnitOfWork.ConnectionString = ERP.Framework.Common.Utils.GetSetting<string>("NovaonADConnection");
                var param = new DynamicParameters();
                param.Add("@FilterField", filter);
                param.Add("@OrderBy", "");
                param.Add("@PageNumber", pageNumber);
                param.Add("@PageSize", pageSize);
                param.Add("@Type", 1);
                param.Add("@LanguageID", languageCode);
                param.Add("@TotalRecord", 0, DbType.Int32, ParameterDirection.InputOutput);
                var list = UnitOfWork.Procedure<PersonalIncomeTax>("PersonalIncomeTax_GetList", param).ToList();
                var userId = Global.CurrentUser.UserID;
                param = HttpRuntime.Cache.Get("PersonalIncomeTax_GetList-" + ERP.Framework.Common.Utils.GetKeyFromParam(param as object) + "-" + userId + "-output") as DynamicParameters;
                total = param.GetDataOutput<int>("@TotalRecord");
                return list;
            }
            catch (Exception ex)
            {
                total = 0;
                return null;
            }
            finally
            {
                //UnitOfWork.ConnectionString = null;

            }

        }
        public SystemMessage PersonalIncomeTax_Save(PersonalIncomeTax data)
        {

            SystemMessage systemMessage = new SystemMessage();
            try
            {              
                var param = new DynamicParameters();
                param.Add("@AutoID", data.AutoID);
                param.Add("@StartDate", data.StartDate);
                param.Add("@EndDate", data.EndDate);
                param.Add("@Status", data.Status);
                param.Add("@FromAmount", data.FromAmount);
                param.Add("@ToAmount", data.ToAmount);
                param.Add("@CurrencyID", data.CurrencyID);               
                param.Add("@ProgressiveTax", data.ProgressiveTax);
                param.Add("@RateTax", data.RateTax);
                param.Add("@CountryID", data.CountryID);
                param.Add("@Note", data.Note);
                UnitOfWork.ProcedureExecute("PersonalIncomeTax_Save", param);
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
        public SystemMessage PersonalIncomeTax_Delete(int AutoID)
        {
            SystemMessage systemMessage = new SystemMessage();
            try
            {
                var param = new DynamicParameters();
                param.Add("@AutoID", AutoID);
                UnitOfWork.ProcedureExecute("PersonalIncomeTax_Delete", param);
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


