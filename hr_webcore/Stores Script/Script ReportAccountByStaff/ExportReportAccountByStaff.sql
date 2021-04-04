USE ERP_HRM 
GO
IF EXISTS ( SELECT 1
  FROM sys.procedures
  WHERE name = 'ExportReportAccountByStaff' )
 BEGIN	
 DROP PROCEDURE [dbo].[ExportReportAccountByStaff]
 END
GO

/*
***************************************************************************
	-- Author:			TrinhNN
	-- Description:		Get List data for Report Account By Staff to export excel
	-- Date				15/01/2018

***************************************************************************
*/

CREATE PROCEDURE [dbo].[ExportReportAccountByStaff]
	@FilterField  					NVARCHAR(max)	= NULL , 
	@FromDate  						DATETIME		= NULL , 
	@ToDate  						DATETIME		= NULL , 
	@ReportType						INT=0,					
	@RoleID							NVARCHAR(max),			
	@CurrencyID						INT=0,
	@OrderByField   				NVARCHAR(max)	= NULL ,
	@PageNumber						INT				= 1 ,
	@PageSize						INT				= 10 , 
	@LanguageID						INT				= NULL ,
	@UserId							BIGINT			= NULL , 
	@OrganizationUnitId				INT				= NULL , 
	@TotalRecord					INT				OUTPUT 
AS
BEGIN TRY
	declare	@WhereSQL				NVARCHAR(max),
			@WhereSQLUser			NVARCHAR(max),
			@Delim					VARCHAR(100),
			@SQL					NVARCHAR(max),
			
			@dynamicsql				NVARCHAR(max),
			@dynamicparamdec		NVARCHAR(max),
			@SearchResult			BIGINT,

			@OrderBySQL				VARCHAR(max),
			@OffsetSize				INT, 
			@Level					INT,
			@SQLselect nvarchar(max),
			@ColSQL					nvarchar(max),
			@View					varchar(max),
			@SpendingCorresponding1Account  INT =1,
			@MarginCorresponding1Account	INT =1


	set		@FilterField			= isnull(@FilterField,'')
	set		@WhereSQL				= ''
	set		@WhereSQLUser			= ''
	set		@Delim					= ' WHERE '
	set		@OffsetSize				=	(@PageNumber-1) * @PageSize 
	

	DECLARE	@LastDOM VARCHAR(19)
	SET		@SpendingCorresponding1Account =(SELECT TOP 1 Value FROM dbo.Globallist WHERE GlobalListID = 3402)
	SET		@MarginCorresponding1Account =(SELECT TOP 1 Value FROM dbo.Globallist WHERE GlobalListID = 3403)

	
	SET @FromDate =  CONVERT(CHAR(10),@FromDate,121) + ' 00:00:00:000'
	SET @ToDate = CONVERT(CHAR(10),@ToDate,121) + ' 23:59:59:000'
	set @LastDOM = (select dateadd(s,-1,dateadd(mm,datediff(m,0,@FromDate)+1,0))) 
	PRINT @FromDate
	Create TABLE #Result(
				StaffName				NVARCHAR(max),
				OrganizationUnitName	NVARCHAR(max),
				VIP						FLOAT,
				Advanced				FLOAT,
				[Standard]				FLOAT,
				Substandard				FLOAT,
				Invalid					FLOAT,
				VIPS					FLOAT,
				AdvancedS				FLOAT,
				StandardS				FLOAT,
				FeeAmount				FLOAT,
				Margin					FLOAT,
				AccountsConvertedBySpending FLOAT,
				AccountsConvertedByMargin FLOAT,
				TotalAccountsConverted FLOAT,
				RateACBSPerTAC FLOAT ,
				RateOfMarginFeePerTotalMargin FLOAT
			)
		IF @FilterField <> '' 
			SET @WhereSQL = 'WHERE '+@FilterField+' '
		ELSE 
			SET @WhereSQL = 'WHERE 1= 1'


		IF (@ReportType='1')  -- phòng ban
		BEGIN
			SET @View = 'INSERT INTO #Result
			        ( StaffName ,
			          OrganizationUnitName ,
			          VIP ,
			          Advanced ,
			          Standard ,
			          Substandard ,
			          Invalid ,
			          VIPS ,
			          AdvancedS ,
			          StandardS ,
			          FeeAmount ,
			          Margin ,
			          AccountsConvertedBySpending ,
			          AccountsConvertedByMargin ,
			          TotalAccountsConverted ,
			          RateACBSPerTAC ,
			          RateOfMarginFeePerTotalMargin
			        )
			SELECT '''' ,
			          OrganizationUnitName ,
			          VIP ,
			          Advanced ,
			          Standard ,
			          Substandard ,
			          Invalid ,
			          VIPS ,
			          AdvancedS ,
			          StandardS ,
			          FeeAmount ,
			          Margin ,
			          AccountsConvertedBySpending ,
			          AccountsConvertedByMargin ,
			          TotalAccountsConverted ,
			          RateACBSPerTAC ,
			          RateOfMarginFeePerTotalMargin
			from
			F_ReportAccountByStaff_OrganizationUnit(' + cast( @LanguageID  as varchar) + ','''+cast( convert(varchar, @FromDate, 23)  as varchar) + ''','''+cast( convert(varchar, @ToDate, 23)  as varchar) + ''','''+cast( @CurrencyID  as varchar)+''','+cast( @SpendingCorresponding1Account  as varchar)+','+cast( @MarginCorresponding1Account  as varchar)+','''+cast( convert(varchar, @FromDate, 23)  as varchar) +''') '
			+@WhereSQL
			--set @View='F_ReportAccountByStaff_OrganizationUnit(' + cast( @LanguageID  as varchar) + ','''+cast( convert(varchar, @FromDate, 23)  as varchar) + ''','''+cast( convert(varchar, @ToDate, 23)  as varchar) + ''','''+cast( @CurrencyID  as varchar)+''','+cast( @SpendingCorresponding1Account  as varchar)+','+cast( @MarginCorresponding1Account  as varchar)+','''+cast( convert(varchar, @FromDate, 23)  as varchar) +''')'
			--set @SQLselect='OrganizationUnitName, VIP	, Advanced	, [Standard], Substandard, Invalid , VIPS	, AdvancedS	, [StandardS], FeeAmount, Margin, AccountsConvertedBySpending, AccountsConvertedByMargin, TotalAccountsConverted, RateACBSPerTAC, RateOfMarginFeePerTotalMargin'
		end
		else  -- nhân viên
		BEGIN
		SET @View = 'INSERT INTO #Result
			        ( StaffName ,
			          OrganizationUnitName ,
			          VIP ,
			          Advanced ,
			          Standard ,
			          Substandard ,
			          Invalid ,
			          VIPS ,
			          AdvancedS ,
			          StandardS ,
			          FeeAmount ,
			          Margin ,
			          AccountsConvertedBySpending ,
			          AccountsConvertedByMargin ,
			          TotalAccountsConverted ,
			          RateACBSPerTAC ,
			          RateOfMarginFeePerTotalMargin
			        )
			SELECT StaffName ,
			          OrganizationUnitName ,
			          VIP ,
			          Advanced ,
			          Standard ,
			          Substandard ,
			          Invalid ,
			          VIPS ,
			          AdvancedS ,
			          StandardS ,
			          FeeAmount ,
			          Margin ,
			          AccountsConvertedBySpending ,
			          AccountsConvertedByMargin ,
			          TotalAccountsConverted ,
			          RateACBSPerTAC ,
			          RateOfMarginFeePerTotalMargin
			from
			F_ReportAccountByStaff_Staff(' + cast( @LanguageID  as varchar) + ','''+cast( convert(varchar, @FromDate, 23)  as varchar) + ''','''+cast( convert(varchar, @ToDate, 23)  as varchar) + ''','''+cast( @CurrencyID  as varchar)+''','+cast( @SpendingCorresponding1Account  as varchar)+','+cast( @MarginCorresponding1Account  as varchar)+','''+cast( convert(varchar, @FromDate, 23)  as varchar) +''') '
			+@WhereSQL
			--set @View='F_ReportAccountByStaff_Staff(' + cast( @LanguageID  as varchar) + ','''+cast( convert(varchar, @FromDate, 23)  as varchar) + ''','''+cast( convert(varchar, @ToDate, 23)  as varchar) + ''','''+cast( @CurrencyID  as varchar)+''','+cast( @SpendingCorresponding1Account  as varchar)+','+cast( @MarginCorresponding1Account  as varchar)+','''+cast( convert(varchar, @FromDate, 23)  as varchar) +''')'
			--set @SQLselect='OrganizationUnitName, StaffName, VIP	, Advanced , Substandard, Invalid , [Standard], VIPS	, AdvancedS	, [StandardS], FeeAmount, Margin, AccountsConvertedBySpending, AccountsConvertedByMargin, TotalAccountsConverted, RateACBSPerTAC, RateOfMarginFeePerTotalMargin'
		end
		-------------------------------------------
		
		--PRINT 'where sql: '+ @WhereSQL
		--SET @SQL = 'select * from '+@View + ' p '+@WhereSQL+' order by p.OrganizationUnitName ' +
		--			' offset '     + convert(varchar,@OffsetSize) + ' rows ' +
		--			' fetch next ' + convert(varchar,@PageSize)   + ' rows only' 
     	
		--PRINT '----view--'
		PRINT @View
		EXEC (@View)
		SELECT * FROM #Result r WHERE 1=1
		order by r.OrganizationUnitName
		-- Tính tổng
		set	@dynamicsql				=
		'select @ItemCount	= count(OrganizationUnitName)
		'	+
		' from (select *
				from #result) p'
		-------------------------------------------
		PRINT '@dynamicsql'
		PRINT @dynamicsql
		  set @dynamicparamdec = N'@ItemCount int output';
 PRINT 'hihi'
 PRINT @dynamicsql

 PRINT @dynamicparamdec
		execute sp_executesql @dynamicsql,@dynamicparamdec,@ItemCount=@TotalRecord OUTPUT
	print ('@TotalRecord' + @TotalRecord)


		 

END TRY

BEGIN CATCH
	declare	@ErrorNum				int,
	@ErrorMsg				varchar(200),
	@ErrorProc				varchar(50),
	@SessionId				int,
	@AddlInfo				varchar(max)

	set @ErrorNum					= error_number()
	set @ErrorMsg					= 'ReportsImplementation: ' + error_message()
	set @ErrorProc					= error_procedure()
	set @AddlInfo					= '@FilterField=' + @FilterField

	exec utl_Insert_ErrorLog @ErrorNum, @ErrorMsg, @ErrorProc, 'dbo.ReportsImplementation', 'GET', @SessionId, @AddlInfo
END CATCH
