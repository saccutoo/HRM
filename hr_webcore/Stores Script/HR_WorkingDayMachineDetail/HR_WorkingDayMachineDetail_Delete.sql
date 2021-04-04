USE [ERP_HRM]
GO
/****** Object:  StoredProcedure [dbo].[PolicyAllowance_Delete]    Script Date: 2/25/2019 4:32:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[HR_WorkingDayMachineDetail_Delete]
	@AutoID	int
as

	begin try
		
		delete from dbo.HR_WorkingDayMarchineDetail	where AutoID=@AutoID
		 

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
		set @AddlInfo					= '@AutoID=' + convert(varchar, @AutoID)

		exec utl_Insert_ErrorLog @ErrorNum, @ErrorMsg, @ErrorProc, 'HR_WorkingDayMachineDetail_Delete', 'DEL', @SessionId, @AddlInfo

	end catch




