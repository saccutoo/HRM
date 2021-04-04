using Dapper;
using ERP.Framework.Common;
using ERP.Framework.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using AutoMapper;
using HRM.DataAccess.Entity;
using HRM.DataAccess.Commons;
using HRM.Common;
using System.Web;

namespace HRM.DataAccess.DAL
{
    public class Sec_MenuDal : BaseDal<ADOProvider>
    {
        public Sec_Menu GetSecMenuById(int roleId, int idTable, int id)
        {
            try
            {
                //UnitOfWork.ConnectionString = ERP.Framework.Common.Utils.GetSetting<string>("NovaonADConnection");
                var param = new DynamicParameters();
                param.Add("@MenuID", id);
                return UnitOfWork.Procedure<Sec_Menu>("sec_Menu_GetByID", param).FirstOrDefault();
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

        public SystemMessage SaveSecMenu(int roleId, int idTable, Sec_Menu obj)
        {
            SystemMessage systemMessage = new SystemMessage();
            try
            {
                //UnitOfWork.ConnectionString = ERP.Framework.Common.Utils.GetSetting<string>("NovaonADConnection");
                var param = new DynamicParameters();
                param.Add("@MenuID", obj.MenuID);
                param.Add("@Name", obj.Name);
                param.Add("@NameEN", obj.NameEN);
                param.Add("@Param", obj.Param);
                param.Add("@IsActive", obj.IsActive);
                param.Add("@IncludeMenu", obj.IncludeMenu);
                param.Add("@OrderNo", obj.OrderNo);
                param.Add("@Url", obj.Url);
                param.Add("@ParentID", obj.ParentID);
                param.Add("@MenuPositionID", obj.MenuPositionID);
                param.Add("@ActionName", obj.ActionName);
                UnitOfWork.ProcedureExecute("Sec_Menu_Save", param);
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

        public SystemMessage DeleteSecMenu(int roleId, int idTable, int id)
        {
            SystemMessage systemMessage = new SystemMessage();
            try
            {
                //UnitOfWork.ConnectionString = ERP.Framework.Common.Utils.GetSetting<string>("NovaonADConnection");
                var param = new DynamicParameters();
                param.Add("@MenuID", id);
                var checkExisted = UnitOfWork.Procedure<Sec_Menu>("sec_Menu_GetByID", param).FirstOrDefault();
                if (checkExisted == null)
                {
                    systemMessage.IsSuccess = false;
                    return systemMessage;
                }
                else
                {
                    var param1 = new DynamicParameters();
                    param1.Add("@MenuID", id);
                    param1.Add("@Result");
                    UnitOfWork.ProcedureExecute("sec_Menu_Delete", param1);
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

        public List<Sec_Menu> GetSecMenu(int pageNumber, int pageSize, string filter, out int total, int Language)
        {
            try
            {
                //UnitOfWork.ConnectionString = ERP.Framework.Common.Utils.GetSetting<string>("NovaonADConnection");
                var param = new DynamicParameters();
                param.Add("@FilterField", filter);
                param.Add("@OrderByField", "");
                param.Add("@PageIndex", pageNumber);
                param.Add("@PageSize", pageSize);
                param.Add("@LanguageID", Language);
                param.Add("@TotalRecord", 0, DbType.Int32, ParameterDirection.InputOutput);
                var list = UnitOfWork.Procedure<Sec_Menu>("sec_Menu_List", param).ToList();
                var userId = Global.CurrentUser.UserID;
                param = HttpRuntime.Cache.Get("sec_Menu_List-" + ERP.Framework.Common.Utils.GetKeyFromParam(param as object) + "-" + userId + "-output") as DynamicParameters;
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
        public List<Sec_Menu> ExportExcelSecMenu(string filter)
        {
            int total = 0;
            try
            {
                //UnitOfWork.ConnectionString = ERP.Framework.Common.Utils.GetSetting<string>("NovaonADConnection");
                var param = new DynamicParameters();
                param.Add("@FilterField", filter);
                param.Add("@OrderBy", "");
                param.Add("@PageNumber", 1);
                param.Add("@PageSize", 10);
                param.Add("@Type", 2);
                param.Add("@TotalRecord", 0, DbType.Int32, ParameterDirection.InputOutput);
                var list = UnitOfWork.Procedure<Sec_Menu>("sec_Menu_List", param).ToList();
                var userId = Global.CurrentUser.UserID;
                param = HttpRuntime.Cache.Get("sec_Menu_List-" + ERP.Framework.Common.Utils.GetKeyFromParam(param as object) + "-" + userId + "-output") as DynamicParameters;
                total = param.GetDataOutput<int>("@TotalRecord");
                return list;
            }
            catch (Exception)
            {
                total = 0;
                return null;
            }
            finally
            {
                //UnitOfWork.ConnectionString = null;

            }
        }
        public List<Sec_Menu> ListParent()
        {
            try
            {
                //UnitOfWork.ConnectionString = ERP.Framework.Common.Utils.GetSetting<string>("NovaonADConnection");
                return UnitOfWork.Procedure<Sec_Menu>("sec_Menu_GetParentMenu",useCache:true).ToList();
            }
            catch (Exception e)
            {
                return null;
            }
            finally
            {
                //UnitOfWork.ConnectionString = null;

            }
        }



        //""
        public List<Sec_Menu> GetDB_SetCacheMenuSystem(string proc, int userId)
        {
            var param = new DynamicParameters();
            param.Add("@UserId", userId);
            var dbresult =
                    UnitOfWork.Procedure<Sec_Menu>(proc, param, useCache: true).ToList();
            return dbresult;
        }

        public List<Sec_Menu> GetMenuSystem(int userId, int roleId)
        {
            try
            {
                return GetTreeMenu(GetMenuSystemNoTree(userId, roleId));
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                return null;
            }
            finally
            {
                //UnitOfWork.ConnectionString = null;

            }
        }

        public List<Sec_Menu> GetMenuSystemNoTree(int userId, int roleId)
        {
            try
            {
                //Nếu không có dữ liệu trong cache lấy trong DB
                var resultExistUserMenu = GetDB_SetCacheMenuSystem("[Menu_System_GetList_ExistUserMenu]", userId);

                if (resultExistUserMenu.Count > 0)
                {
                    return resultExistUserMenu;
                }
                else
                {

                    var resultNoExistUserMenu = GetDB_SetCacheMenuSystem("[Menu_System_GetList_NoExistUserMenu]", userId);
                    return resultNoExistUserMenu;
                }

            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                return null;
            }
            finally
            {
                //UnitOfWork.ConnectionString = null;

            }
        }

        public List<Sec_Menu> GetTreeMenu(List<Sec_Menu> source)
        {

            var configMapper = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<Sec_Menu, Sec_Menu>()
                .ForMember(dest => dest.ActionName, opts => opts.MapFrom(src => src.ActionName))
                .ForMember(dest => dest.MenuName, opts => opts.MapFrom(src => src.Name))
                .ForMember(dest => dest.MenuName_EN, opts => opts.MapFrom(src => src.NameEN))
                .ForMember(dest => dest.ParentID, opts => opts.MapFrom(src => src.ParentID))
                .ForMember(dest => dest.CssIconClass, opts => opts.MapFrom(src => src.CssIconClass));
        });

            List<Sec_Menu> entity = configMapper
                .CreateMapper().Map<List<Sec_Menu>, List<Sec_Menu>>(source);

            List<Sec_Menu> treeLevel1 = entity.Where(x => x.ParentID == 0).OrderBy(x => x.OrderNo).ToList();

            int level = 0;
            foreach (Sec_Menu item in treeLevel1)
            {
                level = 0;
                item.SubMenu = GetAllChild(item, entity, level);
            }

            return treeLevel1;

        }

        public List<Sec_Menu> GetAllChild(Sec_Menu model, List<Sec_Menu> source, int level)
        {
            if (level > 10) return null;

            level++;
            var subMenu = source.Where(x => x.ParentID == model.MenuID).ToList();
            if (subMenu != null)
            {
                foreach (var item in subMenu)
                {
                    item.SubMenu = GetAllChild(item, source, level);
                }
            }

            return subMenu;
        }
    }
}
