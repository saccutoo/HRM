using Dapper;
using ERP.Framework.Common;
using ERP.Framework.Data;
using HRM.Common;
using HRM.DataAccess.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace HRM.DataAccess.DAL
{
    public class GlobalDal : BaseDal<ADOProvider>
    {
        public GlobalList GetGlobalListById(int roleId, int idTable, int id)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@GlobalListID", id);
                return UnitOfWork.Procedure<GlobalList>("globallist_GetByGlobalListID", param).FirstOrDefault();
            }
            catch (Exception)
            {
                return null;
            }
        }
        public EmloyeeDemoView GetNameByUserID(int id)
        {
            try
            {
                //UnitOfWork.ConnectionString = ERP.Framework.Common.Utils.GetSetting<string>("NovaonADConnection");
                var param = new DynamicParameters();
                param.Add("@id", id);
                return UnitOfWork.Procedure<EmloyeeDemoView>("globallist_GetNameByUserID", param).FirstOrDefault();
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
        public SystemMessage AddGlobalList(int roleId, int idTable, GlobalList obj)
        {
            SystemMessage systemMessage = new SystemMessage();
            try
            {
                //UnitOfWork.ConnectionString = ERP.Framework.Common.Utils.GetSetting<string>("NovaonADConnection");
                var param1 = new DynamicParameters();
                if(obj.GlobalListID > 0)
                {
                    param1.Add("@GlobalListID", obj.GlobalListID);
                }
                else
                {
                    param1.Add("@GlobalListID", 0);
                }
                param1.Add("@Name", obj.Name);
                param1.Add("@NameEN", obj.NameEN);
                param1.Add("@IsActive", obj.IsActive);
                param1.Add("@Value", obj.Value);
                param1.Add("@ValueEN", obj.ValueEN);
                param1.Add("@OrderNo", obj.OrderNo);
                param1.Add("@Descriptions", obj.Descriptions);
                param1.Add("@ParentID", obj.ParentID);
                param1.Add("@CreatedBy", obj.CreatedBy1);
                param1.Add("@ParentDetailID", obj.ParentDetailID);
                UnitOfWork.ProcedureExecute("globallist_Save", param1);
                systemMessage.IsSuccess = true;
                return systemMessage;
            }
            catch (Exception e)
            {
                systemMessage.IsSuccess = false;
                systemMessage.Message = e.ToString();
                return systemMessage;
            }
            finally
            {
                //UnitOfWork.ConnectionString = null;
            }
        }
        public SystemMessage DeleteGlobalList(int roleId, int idTable, int id)
        {
            SystemMessage systemMessage = new SystemMessage();
            try
            {
                //UnitOfWork.ConnectionString = ERP.Framework.Common.Utils.GetSetting<string>("NovaonADConnection");
                var param = new DynamicParameters();
                param.Add("@GlobalListID", id);
                var checkExisted = UnitOfWork.Procedure<GlobalList>("globallist_GetByGlobalListID", param).FirstOrDefault();
                if (checkExisted == null)
                {
                    systemMessage.IsSuccess = false;
                    return systemMessage;
                }
                else
                {
                    var param1 = new DynamicParameters();
                    param1.Add("@GlobalListID", id);
                    param1.Add("@Result");
                    UnitOfWork.ProcedureExecute("globallist_Detele", param1);
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
            finally
            {
                //UnitOfWork.ConnectionString = null;
            }
        }
        public List<GlobalList> GetGlobalList(int pageNumber, int pageSize, string filter, out int total)
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
                param.Add("@TotalRecord", 0, DbType.Int32, ParameterDirection.InputOutput);
                var list = UnitOfWork.Procedure<GlobalList>("globallist_List", param).ToList();
                var userId = Global.CurrentUser.UserID;
                param = HttpRuntime.Cache.Get("globallist_List-" + ERP.Framework.Common.Utils.GetKeyFromParam(param as object) + "-" + userId + "-output") as DynamicParameters;
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
        public List<GlobalList> ExportExcelGlobalList(string filter)
        {
            int total = 0;
            try
            {
                var param = new DynamicParameters();
                param.Add("@FilterField", filter);
                param.Add("@OrderBy", "");
                param.Add("@PageNumber", 1);
                param.Add("@PageSize", 10);
                param.Add("@Type", 2);
                param.Add("@TotalRecord", 0, DbType.Int32, ParameterDirection.InputOutput);
                var list = UnitOfWork.Procedure<GlobalList>("globallist_List", param).ToList();
                var userId = HttpContext.Current.Session["UserId"];
                param = HttpRuntime.Cache.Get("globallist_List-" + ERP.Framework.Common.Utils.GetKeyFromParam(param as object) + "-" + userId + "-output") as DynamicParameters;
                total = param.GetDataOutput<int>("@TotalRecord");
                return list;
            }
            catch (Exception)
            {
                total = 0;
                return null;
            }
        }
        public List<GlobalList> GetAllParent()
        {
            try
            {
                //UnitOfWork.ConnectionString = ERP.Framework.Common.Utils.GetSetting<string>("NovaonADConnection");
                var param = new DynamicParameters();
                param.Add("@ParentID", 0);
                param.Add("@LanguageID", 5);
                return UnitOfWork.Procedure<GlobalList>("Globallist_GetsWhereParentID", param).ToList();
            }
            catch (Exception)
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
