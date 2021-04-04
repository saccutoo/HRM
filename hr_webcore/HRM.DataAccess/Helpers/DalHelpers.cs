using HRM.DataAccess.Entity.UserDefinedType;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRM.DataAccess.Helpers
{
    public static class DalHelpers
    {
        public static DataTable ToUserDefinedDataTable<T>(this IEnumerable<T> values) where T : IUserDefinedType
        {
            var table = new DataTable();

            var properties = typeof(T).GetProperties();
            foreach (var prop in properties)
                table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);

            if (values != null)
            {
                foreach (var value in values)
                {
                    var newRow = table.NewRow();
                    foreach (var prop in properties)
                    {
                        if (table.Columns.Contains(prop.Name))
                            newRow[prop.Name] = prop.GetValue(value, null) ?? DBNull.Value;
                    }
                    table.Rows.Add(newRow);
                }
            }
            return table;
        }
    }
}
