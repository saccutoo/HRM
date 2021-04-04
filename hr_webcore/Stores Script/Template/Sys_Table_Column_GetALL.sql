USE [ERP_HRM]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[Sys_Table_Column_GetALL]
	
AS
BEGIN
	select * from dbo.Sys_Table_Column where TableId=24
END	
