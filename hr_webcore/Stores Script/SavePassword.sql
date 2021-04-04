USE [ERP_HRM]
GO
/****** Object:  StoredProcedure [dbo].[Province_Save]    Script Date: 3/11/2019 3:37:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[SavePassword]
@UserID			int,
@Password		nvarchar(200)
AS
BEGIN
	begin
			UPDATE  NovaonAD.dbo.Sec_User
			   SET 
				  Password=@Password							
				 WHERE UserID = @UserID
		end	
END
