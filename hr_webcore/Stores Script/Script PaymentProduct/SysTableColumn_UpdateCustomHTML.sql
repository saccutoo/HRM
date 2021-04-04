UPDATE  Sys_Table_Column

SET CustomHTML = '<a ng-click="tableEdit(tblData,tblInfo)"><span class="{{tblColumn.Class}}" ng-bind="formatData(tblColumn.Type,tblData[tblColumn.ColumnName])"></span></a>'
WHERE ID=505