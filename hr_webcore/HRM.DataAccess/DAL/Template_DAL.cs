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
    public class Template_DAL : BaseDal<ADOProvider>
    {
        public List<Template2> Template_GetList(int pageNumber, int pageSize, string filter,int languageCode ,out int total)
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
                var list = UnitOfWork.Procedure<Template2>("Template_GetList", param).ToList();
                var userId = Global.CurrentUser.UserID;
                param = HttpRuntime.Cache.Get("Template_GetList-" + ERP.Framework.Common.Utils.GetKeyFromParam(param as object) + "-" + userId + "-output") as DynamicParameters;
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
        public SystemMessage Teamplate_Save(Template2 data)
        {

            SystemMessage systemMessage = new SystemMessage();
            try
            {
                var param = new DynamicParameters();
                param.Add("@AutoID", data.AutoID);
                param.Add("@Name", data.Name);
                param.Add("@NameEN", data.NameEN);
                param.Add("@Value", data.Value);
                param.Add("@Type", data.Type);
                param.Add("@DisplayType", data.DisplayType);
                param.Add("@Align", data.Align);
                param.Add("@Css", data.Css);
                param.Add("@OrderNo", data.OrderNo);
                param.Add("@Hide", data.Hide);
                param.Add("@Colspan", data.Colspan);
                param.Add("@DataFormat", data.DataFormat);
                param.Add("@CustomHTML", data.CustomHTML);
                param.Add("@HideRow", data.HideRow);
                param.Add("@Output", 0, DbType.Int32, ParameterDirection.InputOutput);
                UnitOfWork.ProcedureExecute("Teamplate_Save", param);
                int ouput = param.GetDataOutput<int>("@Output");
                if (ouput==1)
                {
                    systemMessage.IsSuccess = true;
                    if (Global.CurrentLanguage == 5)
                        systemMessage.Message = "Cập nhập thành công !!!";
                    else
                        systemMessage.Message = "Successful update !!!";

                }
                if (ouput==2)
                {
                    systemMessage.IsSuccess = true;
                   
                    if (Global.CurrentLanguage == 5)
                        systemMessage.Message = "Thêm mới thành công !!!";
                    else
                        systemMessage.Message = "Successful add !!!";
                }
                return systemMessage;
            }
            catch (Exception e)
            {
                systemMessage.IsSuccess = false;
                systemMessage.Message = e.ToString();
                return systemMessage;
            }

        }
        public SystemMessage Template_Delete(int AutoID)
        {
            SystemMessage systemMessage = new SystemMessage();
            try
            {
                var param = new DynamicParameters();
                param.Add("@AutoID", AutoID);
                UnitOfWork.ProcedureExecute("Template_Delete", param);
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
        public List<Sys_Table_Column_list> Sys_Table_Column_GetALL()
        {
            try
            {
                //UnitOfWork.ConnectionString = ERP.Framework.Common.Utils.GetSetting<string>("NovaonADConnection");
                var list = UnitOfWork.Procedure<Sys_Table_Column_list>("Sys_Table_Column_GetALL",useCache:true).ToList();
                return list;
            }
            catch (Exception ex)
            {
                return null;
            }
            finally
            {
                //UnitOfWork.ConnectionString = null;

            }

        }
    }

}


