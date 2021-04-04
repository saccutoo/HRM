USE [ERP_HRM]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Phund
-- alter date: <2019-01-11 11:05:37.987,SELECT GETDATE(),>
-- Description:	Lấy danh sách menu nếu tồn tại trong dbo.Sec_User_Menu
-- =============================================

CREATE PROCEDURE [dbo].[Menu_System_GetList_ExistUserMenu]
AS
BEGIN try
  SELECT MenuId = a.MenuID,a.ControllerID, MenuName=a.Name,MenuName_EN =a.NameEN,
						a.OrderNo, a.CssIconClass, a.ParentID, a.IsActive,a.NameEN,a.Name,
						a.Url,a.TreeLevel,a.HasChild,ActionName =a.ActionName,ParentId=a.ParentID,
						UserID = b.UserID
		 FROM Sec_Menu a 
		 INNER JOIN Sec_User_Menu b ON b.MenuID = a.MenuID
		 WHERE  IsActive=1
END TRY
BEGIN CATCH
		 DECLARE	@ErrorNum				int,
				@ErrorMsg				varchar(200),
				@ErrorProc				varchar(50),
				@SessionId				int,
				@AddlInfo				varchar(MAX)

		 SET @ErrorNum					= ERROR_NUMBER()
		 SET @ErrorMsg					= 'Menu_System_GetList_ExistUserMenu: ' + ERROR_MESSAGE()
		 SET @ErrorProc					= ERROR_PROCEDURE()
		 SET @AddlInfo					= ''

		 EXEC utl_Insert_ErrorLog @ErrorNum, @ErrorMsg, @ErrorProc, 'dbo.Menu_System_GetList_ExistUserMenu', 'GET', @SessionId, @AddlInfo

 END  CATCH

