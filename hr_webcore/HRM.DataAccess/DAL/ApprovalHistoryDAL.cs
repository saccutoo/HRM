using Dapper;
using ERP.Framework.Common;
using ERP.Framework.Data;
using ERP.Framework.DataBusiness.Common;
using HRM.DataAccess.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace HRM.DataAccess.DAL
{
    public class ApprovalHistoryDAL : BaseDal<ADOProvider>
    {
       
        public List<ApprovalHistory> GetApprovalHistory(BaseListParam listParam, out int? totalRecord)
        {
            try
            {
                var param = new DynamicParameters();
                 param.Add("@FilterField", listParam.FilterField);
                param.Add("@OrderByField", listParam.OrderByField);
                param.Add("@PageIndex", listParam.PageIndex);
                param.Add("@PageSize", listParam.PageSize);
                param.Add("@UserId", listParam.UserId);
                param.Add("@RoleId", listParam.UserType);
                param.Add("@DeptId", listParam.DeptId);
                param.Add("@LanguageID", listParam.LanguageCode); 
                param.Add("@TotalRecord", 0, DbType.Int32, ParameterDirection.InputOutput);
                var list = UnitOfWork.Procedure<ApprovalHistory>("ApprovalHistory_Gets", param).ToList();
                param = HttpRuntime.Cache.Get("ApprovalHistory_Gets-" + ERP.Framework.Common.Utils.GetKeyFromParam(param as object) + "-" + HRM.Common.Global.CurrentUser.UserID + "-output") as DynamicParameters;
                totalRecord = param.GetDataOutput<int>("@TotalRecord");
                return list;
            }
            catch (Exception ex)
            {
                totalRecord = 0;
                return new List<ApprovalHistory>();
            }


        } 
        public SystemMessage SaveEmployee(int roleId, int idTable, ApprovalHistory obj)
        {
            SystemMessage systemMessage = new SystemMessage();
                try
                {
                    var param = new DynamicParameters();
                    param.Add("@IDofTable", obj.IDofTable);
                    param.Add("@TableName", obj.TableName.ToUpper());
                    param.Add("@Status", obj.Status);
                    param.Add("@SubmittedBy", obj.SubmittedBy);
                    param.Add("@SubmittedDate", obj.SubmittedDate);
                    param.Add("@Note", obj.Note);
                    param.Add("@AutoID", obj.AutoID, DbType.Int32, ParameterDirection.InputOutput);
                    UnitOfWork.ProcedureExecute("ApprovalHistory_Save", param);
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
