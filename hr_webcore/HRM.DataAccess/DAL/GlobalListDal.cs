using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using ERP.Framework.Common;
using ERP.Framework.Data;
using ERP.Framework.DataBusiness.Common;
using ERP.DataAccess.DAL;
using HRM.DataAccess.Entity;
using System.Web;

namespace ERP.DataAccess.DAL
{
    public class GlobalListDal : BaseDal<ADOProvider>
    {
        public GlobalList Get(int id)
        {
            return UnitOfWork.Procedure<GlobalList>("qlgx_Get_APDomainInfo", new { Id = id })
                    .FirstOrDefault();
        }

        public IList<GlobalList> GetContractContactCustSevice(int branchId)
        {
            var param = new DynamicParameters();
            param.Add("@Branchid", branchId);
            return UnitOfWork.Procedure<GlobalList>("GetContractContactCustSevice", param).ToList();
        }

        public IList<GlobalList> GlobalList_Gets(int ParentId, int? LanguageID)
      
        {
            var param = new DynamicParameters();
            param.Add("@ParentId", ParentId);
            param.Add("@LanguageID", LanguageID);
            return UnitOfWork.Procedure<GlobalList>("GetGlobalList_GetsWhereParentID", param).ToList();
        }
       

        public dynamic GetGlobalListByParentId(int parentId, int? languageId)
        {
            var param = new DynamicParameters();
            param.Add("@ParentId", parentId);
            param.Add("@LanguageID", languageId);
            return UnitOfWork.Procedure<dynamic>("GetGlobalList_GetsWhereParentID", param);
        }
        public IList<GlobalList> CustomerCompanyType_Gets(int userId, int roleId, int? LanguageID)
        {
            var param = new DynamicParameters();
            param.Add("@LanguageID", LanguageID);
            param.Add("@UserId", userId);
            param.Add("@RoleId", roleId);
            return UnitOfWork.Procedure<GlobalList>("CustomerCompanyType_Gets", param).ToList();
        }
        /// <summary>
        /// Lấy đối tượng Danh sách Nhân viên thuộc quản lý của account
        /// </summary>
        /// <param name="AccountId">Id của AccountId</param>
        /// <returns></returns>
        public IList<Staff> Staff_Gets_ByUserId(int UserId)
        {
            var param = new DynamicParameters();
            param.Add("@UserID", UserId);
            return UnitOfWork.Procedure<Staff>("[Staff_Gets_ByUserID]", param).ToList();
        }
        /// <summary>
        /// Lấy toàn bộ danh sách nhân viên đang hoạt động
        /// </summary>
        /// <param name="langId">Ngôn ngữ</param>
        /// <returns></returns>
        public IList<Staff> Staff_GetsAll(int langId)
        {
            var param = new DynamicParameters();
            param.Add("@LanguageId", langId);
            return UnitOfWork.Procedure<Staff>("Staff_GetAll", param).ToList();
        }

        /// <summary>
        /// Lấy 1 danh sách GlobalList + số lượng danh sách
        /// </summary>
        /// <param name="listParam">Lấy 1 list danh sách</param>
        /// <param name="totalRecord">Tổng số lượng danh sách</param>
        /// <returns>trả về 1 list danh sách, tổng số lưong danh sách</returns>
        public IList<GlobalList> Get(BaseListParam listParam, out int? totalRecord)
        {
            var param = new DynamicParameters();

            param.Add("@FilterField", listParam.FilterField);
            param.Add("@OrderByField", listParam.OrderByField);
            param.Add("@PageNumber", listParam.PageIndex);
            param.Add("@PageSize", listParam.PageSize);
            param.Add("@LanguageCode", listParam.LanguageCode);

            param.Add("@TotalRecord", dbType: DbType.Int32, direction: ParameterDirection.Output);
            var values = UnitOfWork.Procedure<GlobalList>("qlgx_Get_GlobalList", param).ToList();
            param = HttpRuntime.Cache.Get("qlgx_Get_GlobalList-" + ERP.Framework.Common.Utils.GetKeyFromParam(param as object) + "-" + HRM.Common.Global.CurrentUser.UserID + "-output") as DynamicParameters;
            totalRecord = param.GetDataOutput<int>("@TotalRecord");
            return values;
        }

        /// <summary>
        /// Save GlobalList
        /// </summary>
        /// <param name="obj">GlobalList</param>
        /// <param name="userId">userID</param>
        /// <returns>Id</returns>

        //public int Save(GlobalList obj, long userId)
        //{
        //    try
        //    {
        //        var param = new DynamicParameters();
        //        param.Add("@Id", obj.GlobalListID);
        //        param.Add("@Type", obj.Type);
        //        param.Add("@Value", obj.Value);
        //        param.Add("@Name", obj.Name);
        //        param.Add("@Status", obj.Status);
        //        param.Add("@Discription", obj.Descriptions);
        //        param.Add("@ParentId", obj.ParentId);
        //        param.Add("@Type", obj.Type);
        //        param.Add("@Code", obj.Code);
        //        param.Add("@UserId", userId);

        //        param.Add("@Id", obj.Id, DbType.Int32, ParameterDirection.InputOutput);
        //        UnitOfWork.ProcedureExecute("qlgx_Save_GlobalList", param);
        //        return param.Get<int>("@Id");
        //    }
        //    catch (Exception ex)
        //    {
        //        return 0;
        //    }
        //}

        public int CheckCode(int? type, int? code, int? id = 0)
        {
            var param = new DynamicParameters();
            param.Add("@Type", type);
            param.Add("@Code", code);
            param.Add("@Id", id);
            param.Add("@Count", 0, DbType.Int32, ParameterDirection.InputOutput);
            UnitOfWork.ProcedureExecute("CheckCodeandType_APDomain", param);
            return param.GetDataOutput<int>("@Count");
        }


        /// <summary>
        /// Delete Country
        /// </summary>
        /// <param name="id"></param>
        /// <param name="userId">UserID</param>
        /// <returns></returns>
        public bool Delete(int id, long userId)
        {
            try
            {
                return UnitOfWork.ProcedureExecute("qlgx_Delete_GlobalList", new { Id = id, UserID = userId });
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
