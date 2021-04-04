using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hrm.Framework.Models
{
    public class TableViewModel : ControlModel
    {
        public TableViewModel()
        {
            Fields = new List<FieldModel>();
            ButtonHeader = new List<ButtonHeaderModel>();
            ListTableColumns = new List<TableColumnModel>();
        }
        #region control config
        public bool ShowAddButton { get; set; }
        public bool ShowImportExcelButton { get; set; }
        public bool ShowExportExcelButton { get; set; }
        #endregion
        #region main table config
        public List<FieldModel> Fields { get; set; }
        public List<ButtonHeaderModel> ButtonHeader { get; set; }
        public List<TableColumnModel> ListTableColumns { get; set; }
        #endregion
        #region footer
        public int CurrentPage { get; set; }
        public List<dynamic> lstItemsPerPage { get; set; }
        public int ItemsPerPage { get; set; }
        public int TotalPage { get; set; }
        public int TotalRecord { get; set; }
        #endregion
        #region Data
        public string TableName { get; set; }
        public string TableDataUrl { get; set; }
        public string TableReloadConfigUrl { get; set; }
        public string TableConfigName { get; set; }
        public List<dynamic> TableData { get; set; }
        public List<PipelineStepModel> PipelineData { get; set; }
        public string FilterConfig { get; set; }
        public int ViewType { get; set; } = 0;
        public dynamic Total { get; set; } = string.Empty;
        public string BackgroundColor { get; set; } = string.Empty;
        #endregion
    }
    public class SectionModel : ControlModel
    {
        public string SectionName { get; set; }
        public int Position { get; set; }
        public List<FieldModel> Fields { get; set; }
    }
    public class FieldModel : ControlModel
    {
        public string FieldName { get; set; }
        public string FieldTitle { get; set; }
        public string DataFormat { get; set; }
        public string Alignment { get; set; }
        public string Width { get; set; }
        public string ExternalLink { get; set; }
        public string EventClick { get; set; }
        public string Sorting { get; set; }
        public string Presentation { get; set; }
        public List<PresentationDataModel> PresentationData { get; set; }
        public string ShowHeader { get; set; }
        public string Font { get; set; }
        public int Position { get; set; }
        public bool Visible { get; set; }
        public string ActionSave { get; set; }
        public string ActionDelete { get; set; }
        public string ActionAproval { get; set; }
        public string ActionNoAproval { get; set; }
        public string TotalField { get; set; } = string.Empty;
        public bool IsShowTotal { get; set; } = false;
    }
    public class PresentationDataModel
    {
        public string ValueField { get; set; }
        public string DataType { get; set; }
        public string DisplayField { get; set; }
        public bool IsMultiLanguage { get; set; }
    }
    public class ButtonHeaderModel
    {
        public string ButtonName { get; set; }
        public string ButtonClass { get; set; }
        public string EventButton { get; set; }
    }
}