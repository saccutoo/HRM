using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using HRM.DataAccess.Entity;
using ERP.Framework.Common;
using ERP.Framework.Data;
using ERP.Framework.DataBusiness.Common;
using HRM.DataAccess.Commons;
using static HRM.Constants.Constant;
using HRM.DataAccess.Entity.UserDefinedType;
using HRM.DataAccess.Helpers;

namespace HRM.DataAccess.DAL
{
    public class CommonDal : BaseDal<ADOProvider>
    {
        public CommonDal()
        {
            //UnitOfWork.ConnectionString = ERP.Framework.Common.Utils.GetSetting<string>("ErpCacheDependency");
        }
        public List<Sys_Table_Column> GetTableColumns(int roleId, string url)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@roleId", roleId);
                param.Add("@url", url);
                var list = UnitOfWork.Procedure<Sys_Table_Column>("GetTableColumnsByRole", param, useCache: true).ToList();
                return list;
            }
            catch (Exception)
            {
                return null;
            }


        }
        public List<Sys_Table_Column> GetTableColumnsByUser(string url, int UserID)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@url", url);
                param.Add("@UserID", UserID);
                var list = UnitOfWork.Procedure<Sys_Table_Column>("GetTableColumnsByUser", param).ToList();
                return list;
            }
            catch (Exception)
            {
                return null;
            }
        }
        public List<Sys_Table_Column> GetTableFilterColumns(int roleId, int tableId)
        {

            try
            {

                var param = new DynamicParameters();
                param.Add("@roleId", roleId);
                param.Add("@tableId", tableId);
                var list = UnitOfWork.Procedure<Sys_Table_Column>("GetTableFilterColumnsByRole", param, useCache: true).ToList();
                return list;
            }
            catch (Exception e)
            {
                return null;
            }
        }
        public List<Sys_Table_Column> GetTableFilterColumnsByUser(int userId, int tableId)
        {

            try
            {

                var param = new DynamicParameters();
                param.Add("@UserId", userId);
                param.Add("@tableId", tableId);
                var list = UnitOfWork.Procedure<Sys_Table_Column>("GetTableFilterColumnsByUser", param, useCache: true).ToList();
                return list;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public Sys_Table_Role_Action GetTableAction(int roleId, int idTable)
        {
            try
            {
                //var param = new DynamicParameters();
                //param.Add("@roleId", roleId);
                //param.Add("@tableId", idTable);
                //return UnitOfWork.Procedure<Sys_Table_Role_Action>("GetSys_Table_Role_Action", param)
                //        .FirstOrDefault();
                Sys_Table_Role_ActionDal db = new Sys_Table_Role_ActionDal();
                return db.GetAll_Sys_Table_Role_ActionToCache()
                    .Find(x => x.RoleId == roleId && x.TableId == idTable);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public Sys_Table_Role_Action GetTableActionByUser(int userId,int roleId, int idTable)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@UerId", userId);
                param.Add("@roleId", roleId);
                param.Add("@tableId", idTable);
                return UnitOfWork.Procedure<Sys_Table_Role_Action>("GetSys_Table_User_Action", param)
                        .FirstOrDefault();
            }
            catch (Exception)
            {
                return null;
            }
        }
        public Sys_Table GetTableInfo(string url)
        {
            try
            {

                var param = new DynamicParameters();
                param.Add("@url", url);
                return UnitOfWork.Procedure<Sys_Table>("GetSys_Table", param, useCache: true)
                        .FirstOrDefault();
            }
            catch (Exception ex)
            {
                return null;
            }


        }
        public List<GlobalList> GetsWhereParentID(int ParentID, int LanguageID)
        {
            try
            {
                //UnitOfWork.ConnectionString = ERP.Framework.Common.Utils.GetSetting<string>("NovaonADConnection");
                //var param = new DynamicParameters();
                //param.Add("@ParentID", ParentID);
                //param.Add("@LanguageID", LanguageID);
                //return UnitOfWork.Procedure<GlobalList>("Globallist_GetsWhereParentID", param).ToList();
                return GetAllGlobalList(LanguageID).Where(x => x.ParentID == ParentID).ToList();
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
        public List<GlobalList> GetsWhereParentIDnotTree(int ParentID, int LanguageID)
        {
            try
            {
                //UnitOfWork.ConnectionString = ERP.Framework.Common.Utils.GetSetting<string>("NovaonADConnection");
                //var param = new DynamicParameters();
                //param.Add("@ParentID", ParentID);
                //param.Add("@LanguageID", LanguageID);
                //return UnitOfWork.Procedure<GlobalList>("Globallist_GetsWhereParentIDnotTree", param).ToList();
                return GetAllGlobalList(LanguageID).Where(x => x.ParentID == ParentID).ToList();
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


        public List<GlobalList> GetAllGlobalListLanguage(List<GlobalList> data, int LanguageID)
        {
            switch (LanguageID)
            {
                case (int)numLanguage.VN:
                    foreach (var item in data)
                    {
                        item.DisplayName = item.Name;
                        item.DisplayValue = item.Value;
                    }
                    break;
                case (int)numLanguage.EN:
                    foreach (var item in data)
                    {
                        item.DisplayName = item.Name;
                        item.DisplayValue = item.Value;
                    }
                    break;
            }
            return data;
        }

        public List<GlobalList> GetAllGlobalList(int langID)
        {

            try
            {
                var result = UnitOfWork.Procedure<GlobalList>("Globallist_GetAll").ToList();
                return GetAllGlobalListLanguage(result, langID);
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
        public List<GlobalList> GetColumnDataById(int id, int languageID)
        {
            try
            {
                var param1 = new DynamicParameters();
                param1.Add("@ColumnId", id);
                param1.Add("@LanguageID", languageID);
                var result = UnitOfWork.Procedure<GlobalList>("ColumnSQLGetDatas", param1).ToList();
                return result;
            }
            catch (Exception e)
            {
                return new List<GlobalList>();
            }

        }

        public List<Sys_Table_Column> GetColumnDataByRoleID(int RoleID)
        {
            try
            {
                var param1 = new DynamicParameters();
                param1.Add("@RoleID", RoleID);
                var result = UnitOfWork.Procedure<Sys_Table_Column>("Sys_Table_Column_GetList", param1).ToList();
                return result;
            }
            catch (Exception e)
            {
                return null;
            }

        }
        public List<Sys_Table> GetTableDataByRoleID(int RoleID)
        {
            try
            {
                var param1 = new DynamicParameters();
                param1.Add("@RoleID", RoleID);
                var result = UnitOfWork.Procedure<Sys_Table>("Sys_Table_GetList", param1).ToList();
                return result;
            }
            catch (Exception e)
            {
                return null;
            }

        }
        public List<Policy> GetListPolicy()
        {
            try
            {
                var result = UnitOfWork.Procedure<Policy>("Policy_GetAll", useCache: true).ToList();
                return result;
            }
            catch (Exception e)
            {
                return null;
            }
        }
        public List<Sec_Role> GetListRole()
        {
            try
            {
                var result = UnitOfWork.Procedure<Sec_Role>("Sec_Role_GetAll", useCache: true).ToList();
                return result;
            }
            catch (Exception e)
            {
                return null;
            }
        }
        public WorkingDay GetWorkingDayTimePeriod(int staffId)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@StaffID", staffId);
                var result = UnitOfWork.Procedure<WorkingDay>("HR_GetWorkingDayTimePeriod", param, useCache: true).ToList().FirstOrDefault();
                return result;
            }
            catch (Exception e)
            {
                return new WorkingDay();
            }
        }
        public SystemMessage ColumnSave(List<Sys_Table_Column_Type> Data,int RoleId)
        {
            SystemMessage messenger = new SystemMessage();
            try
            {
                var param = new DynamicParameters();
                param.Add("@RoleId", RoleId);
                param.Add("@TableColumnType", Data.ToUserDefinedDataTable(), DbType.Object);
                var result = UnitOfWork.ProcedureExecute("SaveColumn", param);
                messenger.IsSuccess = true;
                return messenger;
            }
            catch (Exception e)
            {
                messenger.IsSuccess = false;
                return messenger;
            }

        }

        public List<Sys_Table_Column> GetALLColumns(int roleId, string url)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@roleId", roleId);
                param.Add("@url", url);
                var list = UnitOfWork.Procedure<Sys_Table_Column>("GetALLColumnsbyRoleId", param, useCache: true).ToList();
                return list;
            }
            catch (Exception)
            {
                return null;
            }


        }

    }
    public class WorkingDay
    {
        public int WorkingDayID { get; set; }
        public int WorkingDayMachineID { get; set; }
        public string MorningHourStart { get; set; }
        public string MorningHourMid { get; set; }
        public string MorningHourEnd { get; set; }
        public string AfernoonHourStart { get; set; }
        public string AfernoonHourMid { get; set; }
        public string AfternoonHourEnd { get; set; }
    }
}
