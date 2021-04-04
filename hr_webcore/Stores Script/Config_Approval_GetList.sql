USE [ERP_HRM]
GO
/****** Object:  StoredProcedure [dbo].[HR_WorkingDayMachine_GetList]    Script Date: 1/25/2019 2:21:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[Config_Approval_GetList]
	@Type int=0
AS
BEGIN
	select * from dbo.Config_Allowance 
END	
