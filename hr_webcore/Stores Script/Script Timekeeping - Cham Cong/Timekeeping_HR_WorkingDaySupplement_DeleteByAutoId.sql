USE [ERP_HRM]
GO
/****** Object:  StoredProcedure [dbo].[HR_WorkingDaySupplement_DeleteByAutoId]    Script Date: 12/20/2018 9:15:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[HR_WorkingDaySupplement_DeleteByAutoId]
	@ListAutoID NVARCHAR(MAX) ='3891'
AS
BEGIN
	DELETE dbo.HR_WorkingDaySupplement WHERE AutoID = @ListAutoID
	RETURN 0;
END