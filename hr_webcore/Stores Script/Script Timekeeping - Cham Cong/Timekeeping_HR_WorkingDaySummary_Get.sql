USE [ERP_HRM]
GO
/****** Object:  StoredProcedure [dbo].[HR_WorkingDaySummary_Get]    Script Date: 1/11/2019 12:01:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[HR_WorkingDaySummary_Get]
	@Month	int,
	@Year int,
	 @UserId INT = 232821,
	 @TotalDay FLOAT = null OUT,
	 @TotalFurlough FLOAT = NULL OUT,
	 @TotalOvertime FLOAT = NULL OUT,
	@TotalWorkingDaySupplement FLOAT = NULL OUT,
	@TotalWorkingDayLeave FLOAT = NULL OUT,
	@SumTotalWorkingDay FLOAT = NULL OUT,
	 @TotalHourLate VARCHAR(10)= null OUT,
	 @TotalHourEarly  VARCHAR(10)= null OUT,
	  @TotalHour  VARCHAR(10)= null OUT
AS
BEGIN
	DECLARE @SQL NVARCHAR(MAX)
	SET @SQL='SELECT * FROM dbo.F_HR_WorkingDaySummary_1('+CAST(@Month AS VARCHAR)+','+CAST(@Year AS VARCHAR)+')' + ' where StaffID = ' + CAST(@UserId AS VARCHAR) +' ORDER BY Date DESC '
	EXEC (@SQL)
	SELECT @TotalDay = SUM(ISNULL(WorkingDay,0))
	,@TotalFurlough= SUM(ISNULL(Furlough,0))
	,@TotalOvertime = SUM(ISNULL(Overtime,0))
	,@TotalWorkingDaySupplement = SUM(ISNULL(WorkingDaySupplement,0))
	,@TotalWorkingDayLeave = SUM(ISNULL(WorkingDayLeave,0))
	,@SumTotalWorkingDay = SUM(ISNULL(TotalWorkingDay,0))
	,@TotalHourLate = convert(VARCHAR(5),CAST(DATEADD(ss,SUM(SecondLateDuration),0) as time))
	,@TotalHourEarly = convert(VARCHAR(5),CAST(DATEADD(ss,SUM(SecondEarlyDuration),0) as time))
	,@TotalHour =  convert(VARCHAR(5),CAST(DATEADD(ss,SUM(SecondLateDuration)+SUM(SecondEarlyDuration),0) as time))
	 FROM dbo.F_HR_WorkingDaySummary_1(CAST(@Month AS VARCHAR),CAST(@Year AS VARCHAR)) where StaffID = @UserId
END