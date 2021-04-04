USE [ERP_HRM]
GO
/****** Object:  StoredProcedure [dbo].[sec_User_Authenticated_List]    Script Date: 1/11/2019 11:39:12 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 
 

/*
***************************************************************************
	-- Author:			Phucnd
	-- Description:		Get Authenticated list
***************************************************************************
*/

ALTER procedure[dbo].[sec_User_Authenticated_List] 
as
 
	begin try
	declare	@WhereSQL				nvarchar(max),
	@ViewPlan					nvarchar(max),
	@SQL					nvarchar(max)  
		set @SQL=' select	u.UserID,u.UserName,u.Email,u.IsActivated,u.LockedBy,u.Status,a.BranchId
		,OriginalUserId=u.userId
        ,LoginUserId=u.userId,Password= u.Password,
		u.DisplayName,o.CompanyType,
		a.OrganizationUnitID,StaffLevelID=a.StaffLevel,a.MCCs,a.BMs,
		RoleId=(select top 1 s.RoleID from Sec_Role_User s where s.UserID=u.UserID order by s.RoleID)
		,OrganizationUnitName=(select top 1 iif(u.CurrentLanguageID=4,s.NameEN,s.name)
		 from [ERP_HRM].[dbo].OrganizationUnit s where s.OrganizationUnitID=a.OrganizationUnitID)
		,a.OfficePositionID
		,CurrencyTypeID = ISNULL(o.CurrencyTypeID,194)
		from	dbo.sec_User u (nolock) left join [ERP_HRM].[dbo].staff a on u.UserID=a.StaffID
		left join [ERP_HRM].[dbo].OrganizationUnit o on a.OrganizationUnitID=o.OrganizationUnitID
		where  u.IsActivated = 1 '
			print (@SQL)
		 exec(@SQL)		 
			 
				--,ClientLimit=isnull((select cl.ClientLimit from ConfigClientLimit cl where cl.StaffLevel=isnull(a.StaffLevel,1)),0) +' + @ViewPlan+
	end try

	
	begin catch

		declare	@ErrorNum				int,
				@ErrorMsg				varchar(200),
				@ErrorProc				varchar(50),
				@SessionId				int,
				@AddlInfo				varchar(max)

		set @ErrorNum					= error_number()
		set @ErrorMsg					= 'sec_Get_UserAuthenticated_list: ' + error_message()
		set @ErrorProc					= error_procedure()
		set @AddlInfo					= ''

		exec utl_Insert_ErrorLog @ErrorNum, @ErrorMsg, @ErrorProc, 'sec_User', 'GET', @SessionId, @AddlInfo

	end catch
