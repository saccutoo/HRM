USE [ERP_HRM]
GO
/****** Object:  StoredProcedure [dbo].[Staff_GetALL]    Script Date: 3/29/2019 1:18:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[Sys_Table_GetList]
@RoleID		int
AS
BEGIN
	select a.*,b.* from Sys_Table_Role_Action a,Sys_Table b where a.RoleId=@RoleID and a.TableId=b.Id 
END	
