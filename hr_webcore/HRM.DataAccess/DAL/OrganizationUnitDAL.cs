using Dapper;
using ERP.Framework.Common;
using ERP.Framework.Data;
using HRM.DataAccess.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HRM.DataAccess.Common;
using ERP.Framework.DataBusiness.Common;
using System.Web;
using HRM.Common;

namespace HRM.DataAccess.DAL
{
    public class OrganizationUnitDAL : BaseDal<ADOProvider>
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
        public List<Sys_Table_Column_User> GetColumnByUsserID(int TableId, int UserID, string filter)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@TableID", TableId);
                param.Add("@UserID", UserID);
                param.Add("@filter", filter);
                var list = UnitOfWork.Procedure<Sys_Table_Column_User>("GetColumnByUsserID", param).ToList();
                return list;
            }
            catch (Exception)
            {
                return null;
            }
        }
        public SystemMessage UpdateColumnByUsserID(int UserID, int TableID, string Visible, int OrderNo, int TableColumnId)
        {
            SystemMessage systemMessage = new SystemMessage();
            try
            {
                var param1 = new DynamicParameters();
                param1.Add("@TableID ", TableID);
                param1.Add("@Visible ", Visible);
                param1.Add("@OrderNo ", OrderNo);
                param1.Add("@UserID ", UserID);
                param1.Add("@TableColumnId ", TableColumnId);

                UnitOfWork.ProcedureExecute("UpdateColumnByUsserID", param1);
                systemMessage.IsSuccess = true;
                systemMessage.Message = "Cập nhật thành công";
                return systemMessage;
            }
            catch (Exception e)
            {
                systemMessage.IsSuccess = false;
                systemMessage.Message = e.ToString();
                return systemMessage;
            }
        }
        public List<OrganizationUnit> GetListEmployeeByOrganizationUnitID(int OrganizationUnitID)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@id", OrganizationUnitID);
                param.Add("@Result");
                var list = UnitOfWork.Procedure<OrganizationUnit>("GetListEmployeeByOrganizationUnitID", param).ToList();
                return list;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public List<OrganizationUnit> GetOrganizationUnit(BaseListParam listParam, out int? totalRecord)
        {
            try
            {
                var param = new DynamicParameters();
                if (listParam.FilterField != null)
                {
                    if (listParam.FilterField.Contains("StatusName") == true)
                    {
                        listParam.FilterField = listParam.FilterField.Replace("StatusName", "Status");
                    }
                }
                param.Add("@FilterField", listParam.FilterField);
                param.Add("@OrderByField", listParam.OrderByField);
                param.Add("@PageIndex", listParam.PageIndex);
                param.Add("@PageSize", listParam.PageSize);
                param.Add("@UserId", listParam.UserId);
                param.Add("@RoleId", listParam.UserType);
                param.Add("@DeptId", listParam.DeptId);
                param.Add("@LanguageID", listParam.LanguageCode);
                param.Add("@TotalRecord", 0, DbType.Int32, ParameterDirection.InputOutput);
                var list = UnitOfWork.Procedure<OrganizationUnit>("OrganizationUnit_List_All", param).ToList();
                param = HttpRuntime.Cache.Get("OrganizationUnit_List_All-" + ERP.Framework.Common.Utils.GetKeyFromParam(param as object) + "-" + HRM.Common.Global.CurrentUser.UserID + "-output") as DynamicParameters;
                totalRecord = param.GetDataOutput<int>("@TotalRecord");

                return list;
            }
            catch (Exception ex)
            {
                totalRecord = 0;
                return null;
            }

        }
        public List<OrganizationUnit> GetOrganizationUnitForEmployee(int pageNumber, int pageSize, string filter, out int total, int usertype, int userid)
        {
            try
            {

                var param = new DynamicParameters();
                param.Add("@FilterField", filter);
                param.Add("@OrderBy", "");
                param.Add("@PageNumber", pageNumber);
                param.Add("@PageSize", pageSize);
                param.Add("@Type", 1);
                param.Add("@UserType", usertype);
                param.Add("@UserID", userid);
                param.Add("@TotalRecord", 0, DbType.Int32, ParameterDirection.InputOutput);
                param.Add("@UserType", usertype);
                param.Add("@UserID", userid);
                var list = UnitOfWork.Procedure<OrganizationUnit>("OrganizationUnit_List", param).ToList();
                var userId = Global.CurrentUser.UserID;
                param = HttpRuntime.Cache.Get("OrganizationUnit_List-" + ERP.Framework.Common.Utils.GetKeyFromParam(param as object) + "-" + userId + "-output") as DynamicParameters;
                total = param.GetDataOutput<int>("@TotalRecord");
                return list;
            }
            catch (Exception ex)
            {
                total = 0;
                return null;
            }

        }

        public List<OrganizationUnit> GetTreeOrganizationUnit(BaseListParam listParam, out int? totalRecord)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@UserId", listParam.UserId);
                param.Add("@RoleId", listParam.UserType);
                param.Add("@DeptId", listParam.DeptId);
                param.Add("@TotalRecord", 0, DbType.Int32, ParameterDirection.InputOutput);
                var list = UnitOfWork.Procedure<OrganizationUnit>("GetTreeOrganizationunit",param, useCache:true).ToList();
                param = HttpRuntime.Cache.Get("GetTreeOrganizationunit-" + ERP.Framework.Common.Utils.GetKeyFromParam(param as object) + "-" + HRM.Common.Global.CurrentUser.UserID + "-output") as DynamicParameters;
                totalRecord = param.GetDataOutput<int>("@TotalRecord");

                return list;
            }
            catch (Exception ex)
            {
                totalRecord = 0;
                return null;
            }
        }

        public List<Department> GetListDepartmemt(BaseListParam listParam, out int? totalRecord)
        {
            List<OrganizationUnit> dbResult =
                GetTreeOrganizationUnit(listParam, out totalRecord);
            int parentid = 0;
            if (dbResult != null && dbResult.Count > 0)
            {
                parentid = Convert.ToInt32(dbResult[0].ParentID);
            }

            var result = GetTreeDepartment(dbResult, parentid);

            return result;
        }
        public List<Department> GetTreeDepartment(List<OrganizationUnit> source, int ParentID)
        {
            var entity = MapFromOrganizationUnit(source);

            List<Department> treeLevel1 = entity.Where(x => x.ParentID == ParentID).ToList();

            int level = 0;
            foreach (Department item in treeLevel1)
            {
                level = 0;
                item.collapsed = false;
                item.children = GetAllChild(item, entity, level);
            }

            return treeLevel1;
        }
        public List<Department> MapFromOrganizationUnit(List<OrganizationUnit> source)
        {
            var configMapper = new AutoMapper.MapperConfiguration(cfg =>
            {
                cfg.CreateMap<OrganizationUnit, Department>()
                .ForMember(dest => dest.id, opts => opts.MapFrom(src => src.OrganizationUnitID))
                    .ForMember(dest => dest.Value, opts => opts.MapFrom(src => src.Deptlist))
                    .ForMember(dest => dest.label, opts => opts.MapFrom(src => src.Name))
                    .BeforeMap((s, d) => d.collapsed = true);
            });

            List<Department> entity = configMapper
                .CreateMapper().Map<List<OrganizationUnit>, List<Department>>(source);
            return entity;
        }

        public List<Department> GetAllChild(Department model, List<Department> source, int level)
        {
            if (level > 10) return null;

            level++;
            var subMenu = source.Where(x => x.ParentID == model.id).ToList();
            if (subMenu != null)
            {
                foreach (var item in subMenu)
                {
                    item.children = GetAllChild(item, source, level);
                }
            }

            return subMenu;
        }

        public SystemMessage SaveOrganizationUnit(int roleId, int idTable, OrganizationUnit secRole, out int trung)
        {
            SystemMessage systemMessage = new SystemMessage();
            try
            {
                var param1 = new DynamicParameters();
                param1.Add("@OrganizationUnitCode ", secRole.OrganizationUnitCode);
                param1.Add("@Name ", secRole.Name);
                param1.Add("@NameEN ", secRole.NameEN);
                param1.Add("@ParentID ", secRole.ParentID);
                param1.Add("@DS_CompanyID ", secRole.DS_CompanyID);
                param1.Add("@DS_UnitID ", secRole.DS_UnitID);
                param1.Add("@DS_BranchID ", secRole.DS_BranchID);
                param1.Add("@DS_BUID ", secRole.DS_BUID);
                param1.Add("@OrderNo ", secRole.OrderNo);
                param1.Add("@Status ", secRole.Status);
                param1.Add("@IsImplementationReport", secRole.IsImplementationReport);
                param1.Add("@Type ", secRole.Type);
                param1.Add("@Email ", secRole.Email);
                param1.Add("@Phone ", secRole.Phone);
                param1.Add("@CommitmentBudgetLimit ", secRole.CommitmentBudgetLimit);
                param1.Add("@MarginMultiplierRate ", secRole.MarginMultiplierRate);
                param1.Add("@CompanyType ", secRole.CompanyType);
                param1.Add("@CurrencyTypeID ", secRole.CurrencyTypeID);
                param1.Add("@RoleId ", secRole.RoleId);
                param1.Add("@OrganizationUnitID", secRole.OrganizationUnitID, DbType.Int32, ParameterDirection.InputOutput);
                param1.Add("@trung", 0, DbType.Int32, ParameterDirection.InputOutput);
                UnitOfWork.ProcedureExecute("OrganizationUnit_Save", param1);
                trung = param1.GetDataOutput<int>("@trung");
                systemMessage.IsSuccess = true;
                systemMessage.Message = "Thêm mới thành công";
                return systemMessage;
            }
            catch (Exception e)
            {
                trung = 0;
                systemMessage.IsSuccess = false;
                systemMessage.Message = e.ToString();
                return systemMessage;
            }
        }

        public SystemMessage DeleteOrganizationUnit(int id, int idTable, int userid, int usertype, int LanguageId)
        {
            SystemMessage systemMessage = new SystemMessage();
            try
            {

                var param = new DynamicParameters();
                param.Add("@OrganizationUnitID", id);
                param.Add("@idTable", idTable);
                param.Add("@UserId", userid);
                param.Add("@UserType", usertype);
                param.Add("@LanguageId", LanguageId);
                param.Add("@Result");
                var checkExisted = UnitOfWork.Procedure<OrganizationUnit>("OrganizationUnit_GetsByID", param).FirstOrDefault();
                if (checkExisted == null)
                {
                    systemMessage.IsSuccess = false;
                    systemMessage.Message = "Dữ liệu ko tồn tại !";
                    return systemMessage;
                }
                else
                {
                    var param1 = new DynamicParameters();
                    param1.Add("@OrganizationUnitID", id);
                    param1.Add("@idTable", idTable);
                    param1.Add("@UserId", userid);
                    param1.Add("@UserType", usertype);
                    param1.Add("@LanguageId", LanguageId);
                    param1.Add("@Result");
                    UnitOfWork.ProcedureExecute("OrganizationUnit_DeleteByID", param1);
                    systemMessage.IsSuccess = true;
                    systemMessage.Message = "Xóa thành công";
                    return systemMessage;

                }
            }
            catch (Exception e)
            {
                systemMessage.IsSuccess = false;
                systemMessage.Message = e.ToString();
                return systemMessage;
            }
        }

        public List<OrganizationUnit> ExportOrganizationUnit(BaseListParam listParam, string filter)
        {
            int totalRecord = 0;
            try
            {
                var param = new DynamicParameters();
                param.Add("@FilterField", filter);
                param.Add("@OrderByField", listParam.OrderByField);
                param.Add("@PageIndex", listParam.PageIndex);
                param.Add("@PageSize", listParam.PageSize);
                param.Add("@UserId", listParam.UserId);
                param.Add("@RoleId", listParam.UserType);
                param.Add("@DeptId", listParam.DeptId);
                param.Add("@LanguageID", listParam.LanguageCode);
                param.Add("@TotalRecord", 0, DbType.Int32, ParameterDirection.InputOutput);
                var list = UnitOfWork.Procedure<OrganizationUnit>("OrganizationUnit_List_All", param).ToList();
                param = HttpRuntime.Cache.Get("OrganizationUnit_List_All-" + ERP.Framework.Common.Utils.GetKeyFromParam(param as object) + "-" + HRM.Common.Global.CurrentUser.UserID + "-output") as DynamicParameters;
                totalRecord = param.GetDataOutput<int>("@TotalRecord");

                return list;
            }
            catch (Exception)
            {
                totalRecord = 0;
                return null;
            }
        }

        public OrganizationUnit GetOrganizationUnitById(int id, int idTable, int userid, int usertype, int LanguageId)
        {
            try
            {
                var param1 = new DynamicParameters();
                param1.Add("@OrganizationUnitID", id);
                param1.Add("@idTable", idTable);
                param1.Add("@UserId", userid);
                param1.Add("@UserType", usertype);
                param1.Add("@LanguageId", LanguageId);
                param1.Add("@Result");
                return UnitOfWork.Procedure<OrganizationUnit>("OrganizationUnit_GetsByID", param1).FirstOrDefault();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public List<OrganizationUnit> OrganizationUnitAll(int chon, int roleid, int Staffid)
        {
            try
            {

                var param = new DynamicParameters();
                param.Add("@Result");
                param.Add("@Staffid", Staffid);
                param.Add("@chon", chon);
                param.Add("@Roleid", roleid);
                return UnitOfWork.Procedure<OrganizationUnit>("Get_All_OrganizationUnitWhereRole", param,useCache:false).ToList();
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public List<Employee> GetEmployeeWhereOrganizationUnit(int languageID, string id, int Staffid, int Roleid)
        {
            try
            {
                var param = new DynamicParameters();
                if (id == "0" || id == null)
                {
                    param.Add("@OrganizationUnitID", "");
                }
                else
                {
                    param.Add("@OrganizationUnitID", id);
                }
                param.Add("@StaffID", Staffid);
                param.Add("@Roleid", Roleid);
                param.Add("@LanguageID", languageID);
                return UnitOfWork.Procedure<Employee>("Get_EmployeeWhereOrganizationUnitID", param).ToList();
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public List<Employee> EmployeeByOrganizationUnitID(int languageID, int id, int Staffid, int Roleid)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@OrganizationUnitID", id);
                param.Add("@StaffID", Staffid);
                param.Add("@Roleid", Roleid);
                param.Add("@LanguageID", languageID);
                return UnitOfWork.Procedure<Employee>("EmployeeByOrganizationUnitID", param).ToList();
            }
            catch (Exception e)
            {
                return null;
            }
        }
        public List<Employee> EmployeeByOrganizationUnitIDSupplement(int languageID, int id, int Staffid, int Roleid)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@OrganizationUnitID", id);
                param.Add("@StaffID", Staffid);
                param.Add("@Roleid", Roleid);
                param.Add("@LanguageID", languageID);
                return UnitOfWork.Procedure<Employee>("EmployeeByOrganizationUnitIDSupplement", param).ToList();
            }
            catch (Exception e)
            {
                return null;
            }
        }
        public List<Employee> GetEmployeeByStatusAndDept(int languageID, int id, int status, int Staffid, int Roleid)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@OrganizationUnitID", id);
                param.Add("@StaffID", Staffid);
                param.Add("@Roleid", Roleid);
                param.Add("@Status", status);
                param.Add("@LanguageID", languageID);
                return UnitOfWork.Procedure<Employee>("GetEmployeeByStatusAndDept", param).ToList();
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public List<OrganizationUnit> OrganizationUnitWhereParentID(int parentID)
        {
            try
            {

                var param = new DynamicParameters();
                param.Add("@ParentID", parentID);
                param.Add("@Result");
                return UnitOfWork.Procedure<OrganizationUnit>("GetOrganizationUnitWhereParentID", param).ToList();
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public List<OrganizationUnit> OrganizationUnitWhereCompanyID(int CompanyID)
        {
            try
            {

                var param = new DynamicParameters();
                param.Add("@ParentID", CompanyID);
                param.Add("@Result");
                return UnitOfWork.Procedure<OrganizationUnit>("GetOrganizationUnitWhereCompanyID", param).ToList();
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public List<OrganizationUnit> GetCompany()
        {
            try
            {

                var param = new DynamicParameters();
                param.Add("@Type", EnumCompanyRoomList.Company);
                return UnitOfWork.Procedure<OrganizationUnit>("Get_OrganizationUnit_Where_Type", param).ToList();
            }
            catch (Exception e)
            {
                return null;
            }
        }
    }
}
