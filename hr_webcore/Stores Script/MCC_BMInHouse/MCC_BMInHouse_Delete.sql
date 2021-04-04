USE [ERP_HRM]
GO
/****** Object:  StoredProcedure [dbo].[Province_Delete]    Script Date: 3/28/2019 3:21:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[MCC_BMInHouse_Delete]
	@Id		int
as

	begin try
		delete from	ERP_V2.dbo.MCC_BMInHouse where Id = @Id
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

		exec utl_Insert_ErrorLog @ErrorNum, @ErrorMsg, @ErrorProc, 'Province_Delete', 'DEL', @SessionId, @AddlInfo

	end catch




