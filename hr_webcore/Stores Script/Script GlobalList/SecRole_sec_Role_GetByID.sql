USE [ERP_HRM]
GO
/****** Object:  StoredProcedure [dbo].[sec_Role_GetByID]    Script Date: 12/24/2018 10:24:34 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<honghv>
-- alter date: <14/09/2018>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [dbo].[sec_Role_GetByID]
	@id  int = 0
AS
BEGIN
	select * from NovaonAD.dbo.Sec_Role where RoleID=@id
END

