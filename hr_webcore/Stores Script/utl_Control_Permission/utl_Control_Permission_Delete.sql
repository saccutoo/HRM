USE [ERP_HRM]
GO
/****** Object:  StoredProcedure [dbo].[StaffPlan_Delete]    Script Date: 3/29/2019 4:56:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREate PROCEDURE [dbo].[utl_Control_Permission_Delete]
	@ControlId	bigint
as

	begin try
		
		delete from ERP_v2_20190327.dbo.utl_Control_Permission where  ControlId=@ControlId		
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
		set @AddlInfo					= '@MenuID=' + convert(varchar, @ControlId)

		exec utl_Insert_ErrorLog @ErrorNum, @ErrorMsg, @ErrorProc, 'StaffPlan_Delete', 'DEL', @SessionId, @AddlInfo

	end catch




