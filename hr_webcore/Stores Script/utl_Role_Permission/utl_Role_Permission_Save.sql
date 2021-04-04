USE [ERP_HRM]
GO
/****** Object:  StoredProcedure [dbo].[utl_Role_Permission_Save]    Script Date: 4/1/2019 4:39:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[utl_Role_Permission_Save]
@Id							bigint,
@GridId						int,
@PermissionId				int,
@PermissionType				nvarchar(max),
@Condition					nvarchar(max),
@Delim						nchar(10)
AS
BEGIN
DECLARE @Save int
	select @Save = count(*) from ERP_v2_20190327.dbo.utl_Role_Permission where Id = @Id
	if (@Save = 0)		
		begin
			insert into ERP_v2_20190327.dbo.utl_Role_Permission (GridId,PermissionId,PermissionType,Condition,Delim)
			 values(@GridId,@PermissionId,@PermissionType,@Condition,@Delim)
		end	
	else
		begin
			UPDATE ERP_v2_20190327.dbo.utl_Role_Permission
			   SET 
				  GridId=@GridId	
				  ,PermissionId=@PermissionId
				 ,PermissionType = @PermissionType
				 ,Condition = @Condition					
				 ,Delim = @Delim				 	
				 WHERE Id = @Id
		end	
END
