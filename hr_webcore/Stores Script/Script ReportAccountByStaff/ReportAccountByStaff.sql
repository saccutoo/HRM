USE ERP_HRM 
GO
IF EXISTS ( SELECT 1
  FROM sys.procedures
  WHERE name = 'ReportAccountByStaff' )
 BEGIN	
 DROP PROCEDURE [dbo].[ReportAccountByStaff]
 END
GO

/*
***************************************************************************
	-- Author:			TrinhNN
	-- Description:		Get List data for Report Account By Staff
	-- Date				15/01/2019

***************************************************************************
*/

CREATE PROCEDURE [dbo].[ReportAccountByStaff]
	@FilterField  					NVARCHAR(max)	= NULL , 
	@FromDate  						DATETIME		= NULL , 
	@ToDate  						DATETIME		= NULL , 
	@ReportType						INT=0,					
	@RoleID							NVARCHAR(max),			
	@CurrencyID						INT=0,
	@OrderByField   				NVARCHAR(max)	= NULL ,
	@PageNumber						INT				= NULL ,
	@PageSize						INT				= NULL , 
	@LanguageID						INT				= NULL ,
	@UserId							BIGINT			= NULL , 
	@OrganizationUnitId				INT				= NULL , 
	@TotalRecord					INT				OUTPUT , 
	@Total1							MONEY			= 0		OUTPUT ,
	@Total2							MONEY			= 0		OUTPUT ,
	@Total3							MONEY			= 0		OUTPUT ,
	@Total4							MONEY			= 0		OUTPUT ,
	@Total5							MONEY			= 0		OUTPUT ,
	@Total6							MONEY			= 0		OUTPUT ,
	--@Total7							MONEY			= 0		OUTPUT ,
	--@Total8							MONEY			= 0		OUTPUT ,
	--@Total9							MONEY			= 0		OUTPUT ,
	@Total10						MONEY			= 0		OUTPUT ,
	@Total11						MONEY			= 0		OUTPUT ,
	@Total12						MONEY			= 0		OUTPUT ,
	@Total13						MONEY			= 0		OUTPUT ,
	@Total14						MONEY			= 0		OUTPUT ,
	@Total15						MONEY			= 0		OUTPUT ,
	@Total16						MONEY			= 0		OUTPUT ,
	@Total17						MONEY			= 0		OUTPUT ,
	@Total18						MONEY			= 0		OUTPUT 
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
		IF (@ReportType='1')  -- phòng ban
		BEGIN
			set @View='F_ReportAccountByStaff_OrganizationUnit(' + cast( @LanguageID  as varchar) + ','''+cast( convert(varchar, @FromDate, 23)  as varchar) + ''','''+cast( convert(varchar, @ToDate, 23)  as varchar) + ''','''+cast( @CurrencyID  as varchar)+''','+cast( @SpendingCorresponding1Account  as varchar)+','+cast( @MarginCorresponding1Account  as varchar)+','''+cast( convert(varchar, @FromDate, 23)  as varchar) +''')'
			set @SQLselect='OrganizationUnitName, VIP	, Advanced	, [Standard], Substandard, Invalid , VIPS	, AdvancedS	, [StandardS], FeeAmount, Margin, AccountsConvertedBySpending, AccountsConvertedByMargin, TotalAccountsConverted, RateACBSPerTAC, RateOfMarginFeePerTotalMargin'
		end
		else  -- nhân viên
		BEGIN
			set @View='F_ReportAccountByStaff_Staff(' + cast( @LanguageID  as varchar) + ','''+cast( convert(varchar, @FromDate, 23)  as varchar) + ''','''+cast( convert(varchar, @ToDate, 23)  as varchar) + ''','''+cast( @CurrencyID  as varchar)+''','+cast( @SpendingCorresponding1Account  as varchar)+','+cast( @MarginCorresponding1Account  as varchar)+','''+cast( convert(varchar, @FromDate, 23)  as varchar) +''')'
			set @SQLselect='OrganizationUnitName, StaffName, VIP	, Advanced , Substandard, Invalid , [Standard], VIPS	, AdvancedS	, [StandardS], FeeAmount, Margin, AccountsConvertedBySpending, AccountsConvertedByMargin, TotalAccountsConverted, RateACBSPerTAC, RateOfMarginFeePerTotalMargin'
		end
		-------------------------------------------
		IF @FilterField <> '' 
			SET @WhereSQL = 'WHERE '+@FilterField+' '
		ELSE 
			SET @WhereSQL = 'WHERE 1= 1'
		PRINT 'where sql: '+ @WhereSQL
		SET @SQL = 'select * from '+@View + ' p '+@WhereSQL+' order by p.OrganizationUnitName ' +
					' offset '     + convert(varchar,@OffsetSize) + ' rows ' +
					' fetch next ' + convert(varchar,@PageSize)   + ' rows only' 
     	
		PRINT '----view--'
		PRINT @SQL
		EXEC (@SQL)
		-- Tính tổng
		set	@dynamicsql				=
		'select @ItemCount	= count(OrganizationUnitName)
			,@sum1=ISNULL(sum(VIP),0)
			,@sum2=ISNULL(sum(Advanced),0)
			,@sum3=ISNULL(sum(Standard),0) 
			,@sum4=ISNULL(sum(VIPS),0)
			,@sum5=ISNULL(sum(AdvancedS),0)
			,@sum6=ISNULL(sum(StandardS),0)'+
			--,@sum7=ISNULL(sum(VIPC),0)
			--,@sum8=ISNULL(sum(AdvancedC),0)
			--,@sum9=ISNULL(sum(StandardC),0)
			',@sum10=ISNULL(sum(FeeAmount),0)
			,@sum11=ISNULL(sum(Margin),0)
			,@sum12=ISNULL(sum(AccountsConvertedBySpending),0)
			,@sum13=ISNULL(sum(AccountsConvertedByMargin),0)
			,@sum14=ISNULL(sum(TotalAccountsConverted),0)
			,@sum15=ISNULL(100*sum(AccountsConvertedBySpending),0)/(IIF(ISNULL(sum(AccountsConvertedBySpending),0)+ISNULL(sum(AccountsConvertedByMargin),0)!=0,ISNULL(sum(AccountsConvertedBySpending),0)+ISNULL(sum(AccountsConvertedByMargin),0),1))
			,@sum16=ISNULL(100*sum(p.Margin),0)/IIF(ISNULL(sum(p.Margin),0)+ISNULL(sum(p.FeeAmount),0)!=0,ISNULL(sum(p.Margin),0)+ISNULL(sum(p.FeeAmount),0),1)
			,@sum17=ISNULL(sum(Substandard),0)
			,@sum18=ISNULL(sum(Invalid),0)

		'	+
		' from (select *
				from '+ @View +') p ' + @WhereSQL +''
		-------------------------------------------
		PRINT '@dynamicsql'
		PRINT @dynamicsql
		  set @dynamicparamdec = N'@ItemCount int output,@sum1 money output,@sum2 money output,@sum3 money output,@sum4 money output
 ,@sum5 money output,@sum6 money output'+
 --,@sum7 money output,@sum8 money output,@sum9 money output
 ',@sum10 money output,@sum11 money output,@sum12 money output,@sum13 money output,@sum14 money output,@sum15 money output,@sum16 money output
 ,@sum17 money output,@sum18 money output';
 PRINT 'hihi'
 PRINT @dynamicsql

 PRINT @dynamicparamdec
		execute sp_executesql @dynamicsql,@dynamicparamdec,@ItemCount=@TotalRecord OUTPUT
		,@sum1=@Total1 OUTPUT
		,@sum2=@Total2 OUTPUT
		,@sum3=@Total3 OUTPUT 
		,@sum4=@Total4 OUTPUT
		,@sum5=@Total5 OUTPUT 
		,@sum6=@Total6 OUTPUT
		--,@sum7=@Total7 OUTPUT 
		--,@sum8=@Total8 OUTPUT
		--,@sum9=@Total9 OUTPUT 
		,@sum10=@Total10 OUTPUT
		,@sum11=@Total11 OUTPUT 
		,@sum12=@Total12 OUTPUT
		,@sum13=@Total13 OUTPUT
		,@sum14=@Total14 OUTPUT 
		,@sum15=@Total15 OUTPUT
		,@sum16=@Total16 OUTPUT
		,@sum17=@Total17 OUTPUT
		,@sum18=@Total18 OUTPUT 
 
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
