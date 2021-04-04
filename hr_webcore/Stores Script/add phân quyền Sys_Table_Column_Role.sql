USE [ERP_HRM]
GO
-- Add phân quyền 1 bảng
INSERT INTO [dbo].[Sys_Table_Column_Role]
           ([RoleId]
           ,[TableColumnId]
           ,[CreateDate]
           ,[CreateBy]
           ,[isActive]
           ,[Order])

		   select [RoleId]=3,id,[CreateDate]=getdate(),[CreateBy]=1,[isActive]=1,OrderNo from  Sys_Table_Column where tableid=35
      

