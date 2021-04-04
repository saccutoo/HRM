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
    public class HR_WorkingDaySupplementDal : BaseDal<ADOProvider>
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

        public List<HR_WorkingDaySupplement> GetHR_WorkingDaySupplement(BaseListParam listParam, out int? totalRecord)
        {
            try
            {
                var param = new DynamicParameters();
                if(listParam.FilterField.Contains("AND a.AutoID IN ("))
                {
                    listParam.PageIndex = 1;
                    listParam.PageSize = 50000;
                }
                param.Add("@FilterField", listParam.FilterField);
                param.Add("@OrderByField", listParam.OrderByField);
                param.Add("@PageIndex", listParam.PageIndex);
                param.Add("@PageSize", listParam.PageSize);
                param.Add("@UserId", HRM.Common.Global.CurrentUser.UserID);
                param.Add("@LanguageId", int.Parse(listParam.LanguageCode));
                param.Add("@CurrentType", listParam.UserType);
                param.Add("@TotalRecord", dbType: DbType.Int32, direction: ParameterDirection.Output);
                var list = UnitOfWork.Procedure<HR_WorkingDaySupplement>("HR_WorkingDaySupplement_Get", param).ToList();
                param = HttpRuntime.Cache.Get("HR_WorkingDaySupplement_Get-" + ERP.Framework.Common.Utils.GetKeyFromParam(param as object) + "-" + HRM.Common.Global.CurrentUser.UserID + "-output") as DynamicParameters;
                totalRecord = param.GetDataOutput<int>("@TotalRecord");

                return list;
            }
            catch (Exception ex)
            {
                totalRecord = 0;
                return null;
            }

        }

        public int HR_GetStatusForCheckApproval(int userId)
        {
            var param = new DynamicParameters();
            param.Add("@UserId", userId);
            param.Add("@StatusID", 0, DbType.Int32, ParameterDirection.InputOutput);
            UnitOfWork.ProcedureExecute("HR_GetStatusForCheckApproval", param);
            var statusId = param.GetDataOutput<int>("@StatusID");
            return statusId;

        }

        public HR_WorkingDaySupplement HR_WorkingDaySupplement_GetListId(BaseListParam listParam, int autoid)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@ListAutoID", autoid);
                param.Add("@LanguageId", int.Parse(listParam.LanguageCode));
                var list = UnitOfWork.Procedure<HR_WorkingDaySupplement>("HR_WorkingDaySupplement_GetByAutoId", param).FirstOrDefault();
                return list;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public SystemMessage HR_WorkingDaySupplement_DeleteByAutoId(int autoid)
        {
            SystemMessage systemMessage = new SystemMessage();
            try
            {
                var param = new DynamicParameters();
                param.Add("@ListAutoID", autoid);
                UnitOfWork.ProcedureExecute("HR_WorkingDaySupplement_DeleteByAutoId", param);
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

        //public int HR_WorkingDaySupplement_Approval(BaseListParam listParam, int AutoID, string Note, int Type)
        //{
        //    try
        //    {
        //        var param1 = new DynamicParameters();
        //        param1.Add("@AutoID ", AutoID);
        //        param1.Add("@UserID ", listParam.UserId);
        //        param1.Add("@Note", Note);
        //        param1.Add("@Type", Type);
        //        param1.Add("@ChoPhepDuyet", 0, DbType.Int32, ParameterDirection.InputOutput);
        //        UnitOfWork.ProcedureExecute("HR_WorkingDaySupplement_Approval", param1);
        //        var statusId = param1.Get<int>("@ChoPhepDuyet");
        //        return statusId;
        //    }
        //    catch (Exception e)
        //    {
        //        return -1;
        //    }
        //}
        public SystemMessage HR_WorkingDaySupplement_Approval(BaseListParam listParam, List<HR_WorkingDaySupplementType> data)
        {
            SystemMessage message = new SystemMessage();
            try
            {
                var param1 = new DynamicParameters();
                param1.Add("@UserID ", listParam.UserId);
                param1.Add("@HR_WorkingDaySupplementType", data.ToUserDefinedDataTable(), DbType.Object);
                UnitOfWork.ProcedureExecute("HR_WorkingDaySupplement_Approval", param1);
                message.IsSuccess = true;
                return message;
            }
            catch (Exception e)
            {
                message.IsSuccess = false;
                return message;
            }
        }
    }
}