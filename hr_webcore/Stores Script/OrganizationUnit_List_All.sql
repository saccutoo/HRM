USE [ERP_HRM]
GO
/****** Object:  StoredProcedure [dbo].[OrganizationUnit_List]    Script Date: 1/28/2019 9:00:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<honghv>
-- alter date: <14/9/2018>
-- Description:	<Lấy ra list bản ghi>
-- =============================================
CREATE PROCEDURE [dbo].[OrganizationUnit_List_All]
@FilterField		NVARCHAR(MAX) = '',
	@OrderByField			VARCHAR(100) = '',
	@PageIndex			INT = 1,
	@PageSize			INT = 20,
	@TotalRecord int = 0 output,
	@RoleId	int=0,
	@DeptId int=0,
	@LanguageID int =5,
	@UserID	int=0,
	@type int = 1
AS
BEGIN
DECLARE @StartIndex BIGINT, @EndIndex BIGINT, @cSql NVARCHAR(MAX), @cBeginSql NVARCHAR(MAX), @cSqlFrom NVARCHAR(MAX), @WhereSQL NVARCHAR(MAX),@vcWhereStatus NVARCHAR(MAX)
DECLARE @Delim	nvarchar(100)
DECLARE @dynamicparamdec NVARCHAR(2000),@WhereSQLUser nvarchar(max)
set @WhereSQLUser=''
if @RoleId!=11 and @RoleId!=1 
			begin
			set	@Delim				= ' and '
			set	@WhereSQLUser			= @WhereSQLUser + @Delim +  
					'  (   c.OrganizationUnitID  in (    
			 Select  o.OrganizationUnitID from staff s inner join V_OrganizationUnit o on o.DeptChild like ''%,'' +cast (s.OrganizationUnitID as varchar) +'',%''
			 where  s.StaffID='+cast(@UserId as varchar(20))+') ) '
			end


IF(@type = 1)
	begin
	IF (ISNULL(@OrderByField, '') = '' )
		SELECT @OrderByField = ' ORDER BY c.OrganizationUnitID DESC'

	SELECT @PageIndex = ISNULL(@PageIndex, 1), @PageSize = ISNULL(@PageSize, 50)
	SELECT @StartIndex = ((@PageIndex - 1) * @PageSize) + 1
	SELECT @EndIndex = @PageIndex * @PageSize

	set @cSqlFrom = N'SELECT * FROM dbo.F_OrganizationUnit(' + cast(@LanguageID as varchar) +') p where 1=1 '
			+ @WhereSQLUser +iif(@FilterField != '',ISNULL('AND ' + @FilterField, ''),'') 
			+ ' and p.StatusName LIKE N''%ho%'' ORDER BY p.OrderNo '+ ' OFFSET  '+ CONVERT(VARCHAR,(@PageIndex - 1) * @PageSize) 
			+' ROWS  FETCH NEXT '+ CONVERT(VARCHAR,@PageSize) +' ROWS ONLY '
	print (@cSqlFrom)
	exec(@cSqlFrom )
		SELECT  @cSql = ''
	SET @cSql = N'SELECT  @ItemCount = COUNT(d.OrganizationUnitID) FROM (SELECT * FROM dbo.F_OrganizationUnit(5) p where 1=1 '+ @WhereSQLUser +iif(@FilterField != '',ISNULL('AND ' + @FilterField, ''),'')+' and p.StatusName LIKE N''%ho%'') AS d'
	PRINT (@cSql)
	SELECT @dynamicparamdec = N'@ItemCount INT OUTPUT'
	exec sp_executesql @cSql, @dynamicparamdec, @TotalRecord OUTPUT
end
ELSE
BEGIN
	set @cSqlFrom = N'SELECT * FROM dbo.F_OrganizationUnit(' + cast(@LanguageID as varchar) +') p where 1=1'
			+ @WhereSQLUser +iif(@FilterField != '',ISNULL('AND ' + @FilterField, ''),'') 
			+ ' and p.StatusName LIKE N''%ho%'' ORDER BY p.OrderNo '+ ' OFFSET  '+ CONVERT(VARCHAR,(@PageIndex - 1) * @PageSize) 
			+' ROWS  FETCH NEXT '+ CONVERT(VARCHAR,@PageSize) +' ROWS ONLY '
	set @TotalRecord = 0
	exec sp_executesql @cSql
END
	
END

