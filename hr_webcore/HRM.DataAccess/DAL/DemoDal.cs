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
    public class DemoDal: BaseDal<ADOProvider>
    {
        public List<Employee> GetEmployees()
        {
            try
            {
                var list = UnitOfWork.Procedure<Employee>("GetAllEmployee").ToList();
                return list;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
