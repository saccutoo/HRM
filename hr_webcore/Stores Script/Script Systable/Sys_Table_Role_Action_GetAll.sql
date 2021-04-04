USE [ERP_HRM]
GO
/****** Object:  StoredProcedure [dbo].[Sys_Table_Role_Action_GetAll]    Script Date: 1/12/2019 9:31:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<phucnd>
-- alter date: <2019-01-12 11:17:34.777>
-- Description:	<Description,,>
-- =============================================

CREATE PROCEDURE [dbo].[Sys_Table_Role_Action_GetAll]
AS
BEGIN
	BEGIN
		SELECT * from dbo.Sys_Table_Role_Action ORDER BY TableId
	END
END

