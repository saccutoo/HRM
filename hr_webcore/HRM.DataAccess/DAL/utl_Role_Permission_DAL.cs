﻿using System;
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
    public class utl_Role_Permission_DAL : BaseDal<ADOProvider>
    {
        public List<utl_Role_Permission> utl_Role_Permission_GetList(int pageNumber, int pageSize, string filter, out int total, int Language)
        {
            try
            {
                //UnitOfWork.ConnectionString = ERP.Framework.Common.Utils.GetSetting<string>("NovaonADConnection");
                var param = new DynamicParameters();
                if (filter.Contains("GridName") == true)
                {
                    filter = filter.Replace("GridName", "GridId");
                }
                if (filter.Contains("PermissionName") == true)
                {
                    filter = filter.Replace("PermissionName", "PermissionId");
                }
                param.Add("@FilterField", filter);
                param.Add("@OrderBy", "");
                param.Add("@PageNumber", pageNumber);
                param.Add("@PageSize", pageSize);
                param.Add("@Language", Language);
                param.Add("@Type", 1);
                param.Add("@TotalRecord", 0, DbType.Int32, ParameterDirection.InputOutput);
                var list = UnitOfWork.Procedure<utl_Role_Permission>("utl_Role_Permission_GetList", param).ToList();
                var userId = Global.CurrentUser.UserID;
                param = HttpRuntime.Cache.Get("utl_Role_Permission_GetList-" + ERP.Framework.Common.Utils.GetKeyFromParam(param as object) + "-" + userId + "-output") as DynamicParameters;
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
        public SystemMessage utl_Role_Permission_Save(utl_Role_Permission data)
        {

            SystemMessage systemMessage = new SystemMessage();
            try
            {
                var param = new DynamicParameters();
                param.Add("@Id", data.Id);
                param.Add("@GridId", data.GridId);
                param.Add("@PermissionId", data.PermissionId);
                param.Add("@PermissionType", data.PermissionType);
                param.Add("@Condition", data.Condition);
                param.Add("@Delim", data.Delim);              
                UnitOfWork.ProcedureExecute("utl_Role_Permission_Save", param);
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
        public SystemMessage utl_Role_Permission_Delete(int ID)
        {
            SystemMessage systemMessage = new SystemMessage();
            try
            {
                var param = new DynamicParameters();
                param.Add("@Id", ID);
                UnitOfWork.ProcedureExecute("utl_Role_Permission_Delete", param);
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

