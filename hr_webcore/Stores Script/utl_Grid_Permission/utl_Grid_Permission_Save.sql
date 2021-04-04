USE [ERP_HRM]
GO
/****** Object:  StoredProcedure [dbo].[utl_Grid_Permission_Save]    Script Date: 4/1/2019 10:21:19 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[utl_Grid_Permission_Save]
@Id							bigint,
@GridId						int,
@PermissionId				int,
@PermissionType				nvarchar(max),
@IsAdd						bit,
@IsEdit						bit,
@IsDelete					bit,
@IsActive					bit,
@IsFilterButton				bit,
@IsExportExcel				bit,
@IsImportExcel				bit,
@IsSubmit					bit,
@IsApproval					bit,
@IsDisApproval				bit,
@IsCopy						bit
AS
BEGIN
DECLARE @Save int
	select @Save = count(*) from ERP_v2_20190327.dbo.utl_Grid_Permission where Id = @Id
	if (@Save = 0)		
		begin
			insert into ERP_v2_20190327.dbo.utl_Grid_Permission (GridId,PermissionId,PermissionType,IsAdd,IsEdit,IsDelete,IsActive,IsFilterButton,IsExportExcel,IsImportExcel,IsSubmit,IsApproval,IsDisApproval,IsCopy)
			 values(@GridId,@PermissionId,@PermissionType,@IsAdd,@IsEdit,@IsDelete,@IsActive,@IsFilterButton,@IsExportExcel,@IsImportExcel,@IsSubmit,@IsApproval,@IsDisApproval,@IsCopy)
		end	
	else
		begin
			UPDATE ERP_v2_20190327.dbo.utl_Grid_Permission
			   SET 
				  GridId=@GridId	
				  ,PermissionId=@PermissionId
				 ,PermissionType = @PermissionType
				 ,IsAdd = @IsAdd					
				 ,IsEdit = @IsEdit	
				 ,IsDelete=@IsDelete
				 ,IsActive=@IsActive
				 ,IsFilterButton=@IsFilterButton
				 ,IsExportExcel=@IsExportExcel
				 ,IsImportExcel=@IsImportExcel
				 ,IsSubmit=@IsSubmit
				 ,IsApproval=@IsApproval
				 ,IsDisApproval=@IsDisApproval
				 ,IsCopy=@IsCopy		
				 WHERE Id = @Id
		end	
END
