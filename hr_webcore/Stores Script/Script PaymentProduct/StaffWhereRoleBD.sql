USE [ERP_HRM]
GO
/****** Object:  StoredProcedure [dbo].[StaffWhereRoleBD]    Script Date: 15/01/2019 10:19:00 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		thanhbt
-- alter date: 2019/01/09
-- Description:	lấy thông tin staff theo role BD(3,29)
-- =============================================
ALTER PROCEDURE [dbo].[StaffWhereRoleBD] 
	-- Add the parameters for the stored procedure here\
	
as

	begin try
	select *,FullName=s.Fullname +'_'+ o.Name 
	from Staff s
	left join Sec_Role_User sr ON s.UserID=sr.UserID
	inner join Organizationunit o on s.OrganizationUnitID=o.OrganizationUnitID
	where sr.RoleID=3 OR sr.RoleID=29
	
		 

		
	end try

	begin catch
	
		declare	@ErrorNum				int,
				@ErrorMsg				varchar(200),
				@ErrorProc				varchar(50),
				@SessionId				int,
				@AddlInfo				varchar(max)

		set @ErrorNum					= error_number()
		set @ErrorMsg					= 'StaffWhereRoleBD: ' + error_message()
		set @ErrorProc					= error_procedure()
		--set @AddlInfo					= '@StaffID=' + convert(varchar, @StaffID)

		exec utl_Insert_ErrorLog @ErrorNum, @ErrorMsg, @ErrorProc, 'StaffWhereRoleBD', 'SWRBD', @SessionId, @AddlInfo

	end catch




