USE [ERP_HRM]
GO
/****** Object:  StoredProcedure [dbo].[Globallist_GetsWhereParentIDnotTree]    Script Date: 1/12/2019 11:58:31 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 
 
/*
***************************************************************************
	-- Author:			Phund
	-- Description:		Globallist_GetAll
	-- Date				PIC					alter record
	-- 2018/10/09                  
***************************************************************************
*/

CREATE procedure [dbo].Globallist_GetAll
as
begin try
	 SELECT       
	[GlobalListID]
      ,[Name]
      ,[NameEN]
      ,[Value]
      ,[ValueEN]
      ,[IsActive]
      ,[Descriptions]
      ,[ParentID]
      ,[TreeLevel]
      ,[HasChild]
      ,[OrderNo]
      ,[CreatedDate]
      ,[CreatedBy]
      ,[ModifiedDate]
      ,[ModifiedBy]
	  
     FROM     dbo.GlobalList AS p WITH (nolock)
	  order by [OrderNo] 
	 
					
	end try

	begin catch

		declare	@ErrorNum				int,
				@ErrorMsg				varchar(200),
				@ErrorProc				varchar(50),
				@SessionId				int,
				@AddlInfo				varchar(max)

		set @ErrorNum					= error_number()
		set @ErrorMsg					= 'Globallist_GetsWhereParentID: ' + error_message()
		set @ErrorProc					= error_procedure()
		set @AddlInfo					= ''

		exec utl_Insert_ErrorLog @ErrorNum, @ErrorMsg, @ErrorProc, 'dbo.Globallist', 'GET', @SessionId, @AddlInfo

	end catch






