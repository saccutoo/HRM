USE [ERP_HRM]
GO
/****** Object:  StoredProcedure [dbo].[utl_Role_Permission_Delete]    Script Date: 4/1/2019 4:42:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[utl_Role_Permission_Delete]
	@Id	bigint
as

	begin try
		
		delete from ERP_v2_20190327.dbo.utl_Role_Permission where  Id=@Id		
	end try

	begin catch

		declare	@ErrorNum				int,
				@ErrorMsg				varchar(200),
				@ErrorProc				varchar(50),
				@SessionId				int,
				@AddlInfo				varchar(max)

		set @ErrorNum					= error_number()
		set @ErrorMsg					= 'DeleteSec_StaffMarginLevel: ' + error_message()
		set @ErrorProc					= error_procedure()
		set @AddlInfo					= '@MenuID=' + convert(varchar, @Id)

		exec utl_Insert_ErrorLog @ErrorNum, @ErrorMsg, @ErrorProc, 'StaffPlan_Delete', 'DEL', @SessionId, @AddlInfo

	end catch




