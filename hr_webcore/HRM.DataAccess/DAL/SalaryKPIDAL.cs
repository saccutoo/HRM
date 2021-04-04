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
using HRM.DataAccess.Entity.UserDefinedType;
using HRM.DataAccess.Helpers;
using HRM.Common;
using System.Web;

namespace HRM.DataAccess.DAL
{
    public class SalaryKPIDAL : BaseDal<ADOProvider>
    {
        public List<SalaryKPI> SalaryKPI_GetList(int pageNumber, int pageSize, string filter, int LanguageCode, out int total)
        {
            try {
                filter = filter.Replace("!!","%");
                var monthFrom = string.Empty;
                var yearFrom = string.Empty;
                var monthTo = string.Empty;
                var yearTo = string.Empty;
                var conditionMonthFrom = string.Empty;
                var conditionMonthTo = string.Empty;
                var conditionYearFrom = string.Empty;
                var conditionYearTo = string.Empty;
                var conditionFilterTo = string.Empty;
                var conditionFilterFrom = string.Empty;
                var conditionFilter = string.Empty;
                string b = string.Empty;

                var valueFrom = new List<string>();
                var valueTo = new List<string>();
                //UnitOfWork.ConnectionString = ERP.Framework.Common.Utils.GetSetting<string>("NovaonADConnection");
                #region Chỉnh sửa filter khi có tháng và năm truyền vào
                if (filter != string.Empty && filter != null && filter != " ")
                {
                    string a = filter.Replace("and", ";");
                    int iFrom = 0;
                    int iTo = 0;
                    var test = a.Split(';');

                    for (var i = 0; i < test.Count(); i++)
                    {
                        if (test[i].Contains("MonthYearTo")) //Chỉnh sửa điều kiện tháng năm từ
                        {
                            valueFrom =  test[i].Replace("MonthYearTo LIKE N'%", " ").Replace("%'", " ").Split('/').ToList();
                            monthFrom = valueFrom[0];
                            yearFrom = valueFrom[1];
                            conditionMonthFrom = "Month <=" + valueFrom[0] + " ";
                            conditionYearFrom = "Year <= " + valueFrom[1] + " ";
                            conditionFilterFrom = conditionMonthFrom + " and " + conditionYearFrom;
                            //conditionFilter += conditionFilterFrom;
                            iFrom = i;
                            if (conditionFilter != string.Empty)
                            {
                                //conditionFilter = "(" + conditionFilter + ")";
                                conditionFilter += " and " + "(" + conditionFilterFrom + ")";
                            }
                            else
                            {
                                conditionFilter = conditionFilterFrom;
                                conditionFilter = "(" + conditionFilter + ")";
                            }
                        }
                        else if (test[i].Contains("MonthYearFrom")) //Chỉnh sửa điều kiện tháng năm đến
                        {
                            valueTo = test[i].Replace("MonthYearFrom LIKE N'%", " ").Replace("%'", " ").Split('/').ToList();
                            monthTo = valueTo[0];
                            yearTo = valueTo[1];
                            conditionMonthTo = "Month >=" + valueTo[0] + " ";
                            conditionYearTo = "Year >= " + valueTo[1] + " ";
                            conditionFilterTo = conditionMonthTo + " and " + conditionYearTo;
                            iTo = i;
                            if (conditionFilter != string.Empty)
                            {
                                conditionFilter += " and " + "(" + conditionFilterTo + ")";
                                //conditionFilter = "(" + conditionFilter + ")";
                            }
                            else
                            {
                                conditionFilter = conditionFilterTo;
                                conditionFilter = "(" + conditionFilter + ")";
                            }
                        }                                         
                    }
                    for (int i = 0; i < test.Count(); i++) //Ghép điều kiện tháng năm
                    {
                        if (!test[i].Contains("MonthYearTo") ==true && !test[i].Contains("MonthYearFrom") == true)
                        {
                            if (b!=string.Empty)
                            {
                                b += "and" + test[i];
                            }
                            else
                            {
                                b= test[i];
                            }
                        }
                    }
                };
                if (b != string.Empty) //Ghép toàn bộ câu điều kiện
                {
                    if (conditionFilter != null && conditionFilter != "")
                    {
                        filter = b + " and " + conditionFilter;
                    }
                    else
                    {
                        filter = b;
                    }
                   
                }
                else
                {
                    filter = conditionFilter;
                }
                #endregion
                var param = new DynamicParameters();
                param.Add("@FilterField", filter);
                param.Add("@OrderBy", "");
                param.Add("@PageNumber", pageNumber);
                param.Add("@PageSize", pageSize);
                param.Add("@Type", 1);
                param.Add("@LanguageID", LanguageCode);
                param.Add("@TotalRecord", 0, DbType.Int32, ParameterDirection.InputOutput);
                var list = UnitOfWork.Procedure<SalaryKPI>("SalaryKPI_GetList", param).ToList();
                var userId = Global.CurrentUser.UserID;
                param = HttpRuntime.Cache.Get("SalaryKPI_GetList-" + ERP.Framework.Common.Utils.GetKeyFromParam(param as object) + "-" + userId + "-output") as DynamicParameters;
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
        public SystemMessage SalaryKPI_Save(List<SalaryKPI> data,int type)
        {

            SystemMessage systemMessage = new SystemMessage();
            try
            {
                var param = new DynamicParameters();
                param.Add("@Type", type);
                param.Add("@SalaryKpiType", data.ToUserDefinedDataTable(), DbType.Object);
                UnitOfWork.ProcedureExecute("SalaryKPI_Save", param);
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
        public SystemMessage SalaryKPI_Delete(int AutoID)
        {
            SystemMessage systemMessage = new SystemMessage();
            try
            {
                var param = new DynamicParameters();
                param.Add("@AutoID", AutoID);
                UnitOfWork.ProcedureExecute("SalaryKPI_Delete", param);
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

        public List<SalaryKPI> ImPortSalaryKPI(List<SalaryKPI> data,out bool IsSuccess)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@LanguageID", HRM.Common.Global.CurrentLanguage);
                param.Add("@SalaryKpiType", data.ToUserDefinedDataTable(), DbType.Object);
                var list = UnitOfWork.Procedure<SalaryKPI>("ImPortSalaryKPI", param).ToList();
                IsSuccess = true;
                return list;
            }
            catch (Exception ex)
            {
                IsSuccess = false;
                return new List<SalaryKPI>();
                throw;
            }
            
        }

    }

}


