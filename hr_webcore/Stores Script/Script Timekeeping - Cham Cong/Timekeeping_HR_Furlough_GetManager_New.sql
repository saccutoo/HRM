USE [ERP_HRM]
GO
/****** Object:  StoredProcedure [dbo].[HR_Furlough_GetManager]    Script Date: 1/11/2019 10:58:16 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 

ALTER PROCEDURE [dbo].[HR_Furlough_GetManager_New]
	@FilterField  						nvarchar(max)	= '  AND StaffID = 175073 ',
	@OrderByField							varchar(100)	= null,
	@PageIndex							int				= 1,				
	@PageSize							int				= 20,	
	@UserId								int				= 121994,
	@LanguageId							int= 5,
	@TotalRecord						int			=0	output,
	@Year INT = 2018
AS
BEGIN
	DECLARE
	@DaysOfMonth varchar(100)
	declare		@WhereSQL				nvarchar(max),
				@Delim					nvarchar(100),
				@StartIndex				bigint,
				@EndIndex				bigint,
				@SQL					nvarchar(max),
							
				@dynamicsql				nvarchar(max),
				@dynamicparamdec		nvarchar(max),
									
				@BrandID int=null
				
		SET @WhereSQL = ' '
		IF @OrderByField = '' OR @OrderByField IS NULL
			BEGIN
				set		@OrderByField	=' a.Fullname ASC'
			END

		set		@FilterField			= isnull(@FilterField,'')
		--1. Calculate to paging
		set		@StartIndex				= @PageSize * @PageIndex
		set		@EndIndex				= @StartIndex + @PageSize

		set		@StartIndex				= ((@PageIndex - 1) * @PageSize) + 1
		set		@EndIndex				= @PageIndex * @PageSize 
		
		IF (@UserId = 1) --Trường hợp là Admin
		BEGIN
			SET @WhereSQL += ' WHERE 1=1 '
		END
		ELSE IF (SELECT RoleID FROM dbo.Sec_Role_User WHERE UserID = CAST(@UserId AS VARCHAR)) = 10 --Trường hợp là HR
		BEGIN
			SET @WhereSQL += ' , dbo.HR_ApprovementConfiguration b WHERE a.StaffID = b.StaffID AND b.HRID '+ CAST(@UserId AS VARCHAR) + ' '
		END
		ELSE IF (SELECT COUNT(AutoID) FROM dbo.HR_ApprovementConfiguration WHERE ManagerID = CAST(@UserId AS VARCHAR)) > 0 --Trường hợp là Quản lý
		BEGIN
			SET @WhereSQL += ' , dbo.HR_ApprovementConfiguration b WHERE a.StaffID = b.StaffID AND b.ManagerID = '+ CAST(@UserId AS VARCHAR) + ' '
		END
		ELSE --Trường hợp là nhân viên
		BEGIN
			SET @WhereSQL += ' WHERE a.StaffID = ' + CAST(@UserId AS VARCHAR) + ' '
		END
		SET @SQL='
				SELECT a.* FROM [dbo].[F_Get_FurloughMonthAll]('+CAST(@UserId AS VARCHAR)+','+CAST(@Year AS VARCHAR)+','+CAST(@LanguageId AS VARCHAR)+') a ' +@WhereSQL+' order by ' + @OrderByField+ 
				' OFFSET  '+ convert(varchar,(@PageIndex - 1) * @PageSize) +' ROWS  FETCH NEXT '+ convert(varchar,@PageSize) +' ROWS ONLY ' 
				  print @SQL
				  EXEC(@SQL)
				--[HR_Furlough_GetManager]
				set	@dynamicsql				=
						N'select @ItemCount = count(b.StaffID)'+
						' from (
									SELECT a.* FROM [dbo].[F_Get_FurloughMonthAll]('+CAST(@UserId AS VARCHAR)+','+CAST(@Year AS VARCHAR)+','+CAST(@LanguageId AS VARCHAR)+') a ' +@WhereSQL+'
						) b '
			set @dynamicparamdec = '@ItemCount int output'
			execute sp_executesql @dynamicsql,@dynamicparamdec,@TotalRecord  output
	
END

