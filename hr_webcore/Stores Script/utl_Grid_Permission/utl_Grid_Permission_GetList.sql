USE [ERP_HRM]
GO
/****** Object:  StoredProcedure [dbo].[utl_Control_Permission_GetList]    Script Date: 4/1/2019 8:35:17 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[utl_Grid_Permission_GetList]
@FilterField		NVARCHAR(MAX) = '',
	@OrderBy			VARCHAR(100) = '',
	@PageNumber			INT = 1,
	@PageSize			INT = 20,
	@Language			INT,
	@TotalRecord int = 0 output,
	@type int = 1
AS
BEGIN
DECLARE @StartIndex BIGINT, @EndIndex BIGINT, @cSql NVARCHAR(MAX), @cBeginSql NVARCHAR(MAX), @cSqlFrom NVARCHAR(MAX), @WhereSQL NVARCHAR(MAX),@vcWhereStatus NVARCHAR(MAX)
DECLARE @Delim	nvarchar(100)
DECLARE @dynamicparamdec NVARCHAR(2000)
IF(@type = 1)
	begin
	IF (ISNULL(@OrderBy, '') = '' )
		SELECT @OrderBy = ' ORDER BY AutoID DESC'

	SELECT @PageNumber = ISNULL(@PageNumber, 1), @PageSize = ISNULL(@PageSize, 50)
	SELECT @StartIndex = ((@PageNumber - 1) * @PageSize) + 1
	SELECT @EndIndex = @PageNumber * @PageSize

	set @cSqlFrom =   N'SELECT a.*,GridName=(select iif('+CAST(@Language as nvarchar)+'=5,Note,TableName) from Sys_Table where id=a.GridId )
				,iif(a.PermissionType like N''%User%'',(select iif('+CAST(@Language as nvarchar)+'=5,FullName,FullNameEN) from staff where a.PermissionId=UserID),(select iif('+CAST(@Language as nvarchar)+'=5,Name,NameEN) from Sec_Role  where a.PermissionId=RoleID)) as PermissionName
				FROM ERP_v2_20190327.dbo.utl_Grid_Permission a'

	SELECT @cSql = @cSqlFrom + iif(@FilterField != '',ISNULL(+ ' WHERE ' + @FilterField, ''),'') + ' order by a.Id desc  '+ ' OFFSET  '+ CONVERT(VARCHAR,(@PageNumber - 1) * @PageSize) +' ROWS  FETCH NEXT ' + CONVERT(VARCHAR,@PageSize) +' ROWS ONLY '

	print (@cSql)
	exec(@cSql )
		SELECT  @cSql = ''
	SELECT @cSql = N'SELECT @TotalRecord = COUNT(a.Id) from ( '+ @cSqlFrom + +' '+iif(@FilterField != '',ISNULL('WHERE ' + @FilterField, ''),'')+' ) a'
	print (@cSql)
	SELECT @dynamicparamdec = N'@TotalRecord INT OUTPUT'
	exec sp_executesql @cSql, @dynamicparamdec, @TotalRecord OUTPUT
end
ELSE
BEGIN
	set @cSqlFrom =   N'SELECT a.*,GridName=(select iif('+CAST(@Language as nvarchar)+'=5,Note,TableName) from Sys_Table where id=a.GridId )
				,iif(a.PermissionType like N''%User%'',(select iif('+CAST(@Language as nvarchar)+'=5,FullName,FullNameEN) from staff where a.PermissionId=UserID),(select iif('+CAST(@Language as nvarchar)+'=5,Name,NameEN) from Sec_Role  where a.PermissionId=RoleID)) as PermissionName
				FROM ERP_v2_20190327.dbo.utl_Grid_Permission a'
	SELECT @cSql = @cSqlFrom +' '+iif(@FilterField != '',ISNULL('AND ' + @FilterField, ''),'') 
	set @TotalRecord = 0
	exec sp_executesql @cSql
END
	
END

