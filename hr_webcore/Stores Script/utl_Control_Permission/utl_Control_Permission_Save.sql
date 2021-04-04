USE [ERP_HRM]
GO
/****** Object:  StoredProcedure [dbo].[MCC_BMInHouse_Save]    Script Date: 3/29/2019 4:50:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create PROCEDURE [dbo].[utl_Control_Permission_Save]
@ControlId					bigint,
@GridColumnId				int,
@PermissionId				int,
@PermissionType				nvarchar(max),
@IsActive					bit,
@CustomHtml					nvarchar(max),
@DisplayOrder				int,
@DataCondition				nvarchar(max)
AS
BEGIN
DECLARE @Save int
	select @Save = count(*) from ERP_v2_20190327.dbo.utl_Control_Permission where ControlId = @ControlId
	if (@Save = 0)		
		begin
			insert into ERP_v2_20190327.dbo.utl_Control_Permission (GridColumnId,PermissionId,PermissionType,IsActive,CustomHtml,DisplayOrder,DataCondition)
			 values(@GridColumnId,@PermissionId,@PermissionType,@IsActive,@CustomHtml,@DisplayOrder,@DataCondition)
		end	
	else
		begin
			UPDATE  ERP_v2_20190327.dbo.utl_Control_Permission
			   SET 
				  GridColumnId=@GridColumnId
				  ,PermissionId=@PermissionId
				 ,PermissionType = @PermissionType
				 ,IsActive = @IsActive					
				 ,CustomHtml = @CustomHtml	
				 ,DisplayOrder=@DisplayOrder
				 ,DataCondition=@DataCondition		
				 WHERE ControlId = @ControlId
		end	
END
