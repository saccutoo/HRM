﻿using Dapper;
using ERP.Framework.Common;
using ERP.Framework.Data;
using HRM.DataAccess.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRM.DataAccess.DAL
{
    public class Sec_UserMenuDAL : BaseDal<ADOProvider>
    {
        public List<Sys_Table_Column> GetTableColumns(string tableName)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@tableName", tableName);
                var list = UnitOfWork.Procedure<Sys_Table_Column>("GetSys_Table_Column", param,useCache:true).ToList();
                return list;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<Sec_Menu> GetIDMenuByUserID(int roleId, int idTable, int id)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@id", id);
                return UnitOfWork.Procedure<Sec_Menu>("sec_User_Menu_GetByUserID", param).ToList();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public SystemMessage AddSecUserMenu(int roleId, int idTable, Sec_User_Menu secrolemenu)
        {
            SystemMessage systemMessage = new SystemMessage();
            try
            {
                var param1 = new DynamicParameters();
                param1.Add("@UserID ", secrolemenu.UserID);
                param1.Add("@MenuID ", secrolemenu.MenuID);
                UnitOfWork.ProcedureExecute("sec_User_Menu_Insert", param1);
                systemMessage.IsSuccess = true;
                systemMessage.Message = "Thêm mới thành công";
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