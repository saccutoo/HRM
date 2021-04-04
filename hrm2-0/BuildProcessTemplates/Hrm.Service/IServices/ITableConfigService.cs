namespace Hrm.Service
{
    public partial interface ITableConfigService : IBaseService
    {
        string GetTableConfig(long TableId);
        string GetTableConfigByTableName(string tableName);
        string SaveUserTableConfig(string tableName, string queryData, string filterData, string configData, string isFilter);
    }
}
