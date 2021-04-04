using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using HRM.DataAccess.Entity;
using ERP.Framework.Common;
using ERP.Framework.Data;
using ERP.Framework.DataBusiness.Common;

namespace HRM.DataAccess.DAL
{
    public class TableDal: BaseDal<ADOProvider>
    {

        public Employee GetEmpById(int roleId, int idTable, int id)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@id", id);
                return UnitOfWork.Procedure<Employee>("GetEmployee", param)
                        .FirstOrDefault();
            }
            catch (Exception)
            {
                return null;
            }
        }

        //public SystemMessage UpdateEmpById(int roleId, int idTable, int id, Employee emp)
        //{


        //    SystemMessage systemMessage = new SystemMessage();
        //    try
        //    {
        //        var param = new DynamicParameters();
        //        param.Add("@id", id);
        //        var checkExisted = UnitOfWork.Procedure<Employee>("GetEmployee", param).FirstOrDefault();
        //        if (checkExisted == null)
        //        {
        //            systemMessage.IsSuccess = false;
        //            systemMessage.Message = "Dữ liệu ko tồn tại !";
        //            return systemMessage;
        //        }
        //        var param1 = new DynamicParameters();
        //        param1.Add("@name ", emp.name);
        //        param1.Add("@id ", id);
        //        param1.Add("@DOB ", emp.DOB);
        //        param1.Add("@Gender", emp.Gender);
        //        param1.Add("@Address ", emp.Address);
        //        param1.Add("@Email", emp.Email);
        //        param1.Add("@Mobile", emp.Mobile);
        //        UnitOfWork.ProcedureExecute("SaveEmployee", param1);
        //        systemMessage.IsSuccess = true;
        //        systemMessage.Message = "Cập nhật thành công";
        //        return systemMessage;
        //    }
        //    catch (Exception e)
        //    {
        //        systemMessage.IsSuccess = false;
        //        systemMessage.Message = e.ToString();
        //        return systemMessage;
        //    }
        //}
        //public SystemMessage AddEmp(int roleId, int idTable, Employee emp)
        //{
        //    SystemMessage systemMessage = new SystemMessage();
        //    try
        //    {
        //        var param1 = new DynamicParameters();
        //        param1.Add("@name ", emp.name);
        //        param1.Add("@DOB ", emp.DOB);
        //        param1.Add("@Gender", emp.Gender);
        //        param1.Add("@Address ", emp.Address);
        //        param1.Add("@Email", emp.Email);
        //        param1.Add("@Mobile", emp.Mobile);
        //        UnitOfWork.ProcedureExecute("InsertEmployee", param1);
        //        systemMessage.IsSuccess = true;
        //        systemMessage.Message = "Thêm mới thành công";
        //        return systemMessage;
        //    }
        //    catch (Exception e)
        //    {
        //        systemMessage.IsSuccess = false;
        //        systemMessage.Message = e.ToString();
        //        return systemMessage;
        //    }
        //}
        public SystemMessage DeleteEmp(int roleId, int idTable, int id)
        {
            SystemMessage systemMessage = new SystemMessage();
            try
            {
                var param = new DynamicParameters();
                param.Add("@id", id);
                var checkExisted = UnitOfWork.Procedure<Employee>("GetEmployee", param).FirstOrDefault();
                if (checkExisted == null)
                {
                    systemMessage.IsSuccess = false;
                    systemMessage.Message = "Dữ liệu ko tồn tại !";
                    return systemMessage;
                }
                else
                {
                    var param1 = new DynamicParameters();
                    param1.Add("@id", id);
                    UnitOfWork.ProcedureExecute("DeleteEmployee", param1);
                    systemMessage.IsSuccess = true;
                    systemMessage.Message = "Xóa nhật thành công";
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

        //public List<EmloyeeDemoViewModel> GetEmloyeeDemo(int pageNumber, int pageSize, string filter, out int total)
        //{
        //    try
        //    {
        //        var param = new DynamicParameters();
        //        param.Add("@FilterField", filter);
        //        param.Add("@OrderBy", "");
        //        param.Add("@PageNumber", pageNumber);
        //        param.Add("@PageSize", pageSize);
        //        param.Add("@Type", 1);
        //        param.Add("@TotalRecord", 0, DbType.Int32, ParameterDirection.InputOutput);
        //        var list = UnitOfWork.Procedure<EmloyeeDemoViewModel>("ListEmployeeDemoPageList", param).ToList();
        //        total = param.GetDataOutput<int>("@TotalRecord");
        //        return list;
        //    }
        //    catch (Exception)
        //    {
        //        total = 0;
        //        return null;
        //    }

        //}
        //public List<EmloyeeDemoViewModel> ExportExcelDemo(string filter)
        //{
        //    int total = 0;
        //    try
        //    {
        //        var param = new DynamicParameters();
        //        param.Add("@FilterField", filter);
        //        param.Add("@OrderBy", "");
        //        param.Add("@PageNumber", 1);
        //        param.Add("@PageSize", 10);
        //        param.Add("@Type", 2);
        //        param.Add("@TotalRecord", 0, DbType.Int32, ParameterDirection.InputOutput);
        //        var list = UnitOfWork.Procedure<EmloyeeDemoViewModel>("ListEmployeeDemoPageList", param).ToList();
        //        total = param.GetDataOutput<int>("@TotalRecord");
        //        return list;
        //    }
        //    catch (Exception)
        //    {
        //        total = 0;
        //        return null;
        //    }
        //}
    }
}