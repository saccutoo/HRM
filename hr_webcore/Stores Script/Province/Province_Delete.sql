USE [ERP_HRM]
GO
/****** Object:  StoredProcedure [dbo].[Province_Delete]    Script Date: 2/27/2019 2:08:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[Province_Delete]
	@ProvinceID		int
as

	begin try
		delete from	Province where ProvinceID = @ProvinceID
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
		set @AddlInfo					= '@MenuID=' + convert(varchar, @ProvinceID)

		exec utl_Insert_ErrorLog @ErrorNum, @ErrorMsg, @ErrorProc, 'Province_Delete', 'DEL', @SessionId, @AddlInfo

	end catch




