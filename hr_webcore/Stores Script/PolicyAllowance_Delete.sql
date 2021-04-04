USE [ERP_HRM]
GO
/****** Object:  StoredProcedure [dbo].[PolicyAllowance_Delete]    Script Date: 2/13/2019 11:09:07 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[PolicyAllowance_Delete]
	@AutoID	int
as

	begin try
		
		delete from dbo.PolicyAllowance	where AutoID=@AutoID
		 

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
		set @AddlInfo					= '@MenuID=' + convert(varchar, @AutoID)

		exec utl_Insert_ErrorLog @ErrorNum, @ErrorMsg, @ErrorProc, 'PolicyAllowance_Delete', 'DEL', @SessionId, @AddlInfo

	end catch




