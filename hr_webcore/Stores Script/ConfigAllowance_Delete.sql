USE [ERP_HRM]
GO
/****** Object:  StoredProcedure [dbo].[ConfigAllowance_Delete]    Script Date: 1/31/2019 9:58:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[ConfigAllowance_Delete]
	@AllowanceID	int
as

	begin try
		
		delete from dbo.Config_Allowance	where AllowanceID=@AllowanceID 
		 

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
		set @AddlInfo					= '@MenuID=' + convert(varchar, @AllowanceID)

		exec utl_Insert_ErrorLog @ErrorNum, @ErrorMsg, @ErrorProc, 'ConfigAllowance_Delete', 'DEL', @SessionId, @AddlInfo

	end catch




