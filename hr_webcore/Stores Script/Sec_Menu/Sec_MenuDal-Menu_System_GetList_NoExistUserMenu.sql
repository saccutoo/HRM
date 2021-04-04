USE [ERP_HRM]
GO
/****** Object:  StoredProcedure [dbo].[Menu_System_GetList_NoExistUserMenu]    Script Date: 1/11/2019 3:05:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[Menu_System_GetList_NoExistUserMenu]
AS
BEGIN try
  SELECT distinct MenuId = a.MenuID,a.ControllerID, MenuName=a.Name,MenuName_EN =a.NameEN,
						a.OrderNo, a.CssIconClass, a.ParentID, a.IsActive,a.NameEN,a.Name,
						a.Url,a.TreeLevel,a.HasChild,ActionName =a.ActionName,ParentId=a.ParentID,
						c.UserID
			from Sec_Menu a INNER JOIN Sec_Role_Menu b ON a.MenuID=b.MenuID
			INNER JOIN Sec_Role_User c ON b.RoleID=c.RoleID
			WHERE IsActive=1
END TRY
BEGIN CATCH
		 DECLARE	@ErrorNum				int,
				@ErrorMsg				varchar(200),
				@ErrorProc				varchar(50),
				@SessionId				int,
				@AddlInfo				varchar(MAX)

		 SET @ErrorNum					= ERROR_NUMBER()
		 SET @ErrorMsg					= 'Menu_System_GetList_NoExistUserMenu: ' + ERROR_MESSAGE()
		 SET @ErrorProc					= ERROR_PROCEDURE()
		 SET @AddlInfo					= ''

		 EXEC utl_Insert_ErrorLog @ErrorNum, @ErrorMsg, @ErrorProc, 'dbo.Menu_System_GetList_NoExistUserMenu', 'GET', @SessionId, @AddlInfo

 END  CATCH