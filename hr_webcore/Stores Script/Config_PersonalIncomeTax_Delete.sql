USE [ERP_HRM]
GO
/****** Object:  StoredProcedure [dbo].[Config_PersonalIncomeTax_Delete]    Script Date: 1/31/2019 8:37:11 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[Config_PersonalIncomeTax_Delete]
	@ID	int
as

	begin try
		
		delete from dbo.Config_PersonalIncomeTax	where ID=@ID 
		 

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
		set @AddlInfo					= '@MenuID=' + convert(varchar, @ID)

		exec utl_Insert_ErrorLog @ErrorNum, @ErrorMsg, @ErrorProc, 'Config_PersonalIncomeTax_Delete', 'DEL', @SessionId, @AddlInfo

	end catch




