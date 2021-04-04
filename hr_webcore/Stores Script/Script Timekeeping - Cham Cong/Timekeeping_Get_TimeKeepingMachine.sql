USE [ERP_HRM]
GO
/****** Object:  StoredProcedure [dbo].[Get_TimeKeepingMachine]    Script Date: 1/21/2019 4:10:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER procedure [dbo].[Get_TimeKeepingMachine]
	 @Month int,
	 @Year int,
	 @UserId INT =22878,
	 @TotalDay FLOAT = null OUT,
	 @TotalHourLate VARCHAR(10)= null OUT,
	 @TotalHourEarly  VARCHAR(10)= null OUT
AS
BEGIN
	DECLARE @BranchId INT
	DECLARE @Place INT
	DECLARE @DATABASENAME NVARCHAR(50)
	DECLARE @FromDate1 NVARCHAR(50)
	DECLARE @ToDate1 NVARCHAR(50)
	DECLARE @FromDate DATETIME
	DECLARE @ToDate DATETIME
	DECLARE @Month1 int
	DECLARE @Year1 int,@StatusSat int,@WorkingDayMachineID int
	SET @FromDate1 = (SELECT c.DateFromNumber FROM HR_WorkingDayConfig c Where c.WorkingDayMachineID = (SELECT TOP 1 WorkingDayMachineID FROM HR_IDMapping WHERE StaffID = 503130 ORDER BY WorkingDayMachineDate))
	SET @ToDate1 = (SELECT c.DateToNumber FROM HR_WorkingDayConfig c Where c.WorkingDayMachineID = (SELECT TOP 1 WorkingDayMachineID FROM HR_IDMapping WHERE StaffID = 503130 ORDER BY WorkingDayMachineDate))
	IF @Month = 1
	BEGIN
		SET @Month1 = 12
		SET @Year1 = @Year - 1
	END
	ELSE
	BEGIN
		SET @Month1 = @Month - 1
		SET @Year1 = @Year
	END
	SET @FromDate1 = CAST( @Year1 AS nvarchar(50))+ '/' + CAST( @Month1 AS nvarchar(50)) + '/' + @FromDate1
	SET @ToDate1 = CAST( @Year AS nvarchar(50))+ '/' + CAST( @Month AS nvarchar(50)) + '/' + @ToDate1
	PRINT @FromDate1
	PRINT @ToDate1
	DECLARE @Work1 NVARCHAR(50)
	DECLARE @Work2 NVARCHAR(50)
	DECLARE @DayOfWeekFomat BIT
	SET @FromDate =  convert(nvarchar(20),@FromDate1 +' 01:00',113)
	SET @ToDate =  convert(nvarchar(20),@ToDate1 +' 23:30',113)


	SELECT @DATABASENAME = a.DatabaseName
	 ,@DayOfWeekFomat = DayOfWeekFomat,@StatusSat=StatusSat,@WorkingDayMachineID=a.WorkingDayMachineID
	  FROM HR_WorkingDayMachine a
	  INNER JOIN HR_WorkingDay w ON a.WorkingDayMachineID = w.WorkingDayMachineID
	INNER JOIN HR_IDMapping b ON a.WorkingDayMachineID = b.WorkingDayMachineID
	WHERE b.StaffID = @UserId
	 
		
		-- TAO BANG TAM VA LUU DU LIEU TU MAY CHAM CONG VAO BANG TAM
	  DECLARE @TblTimeChecking TABLE (UserIdSSN INT,SSN NVARCHAR(20),CheckTime DATETIME, HourCheck VARCHAR(50)) 
	    
	
	  DECLARE @sql NVARCHAR(4000)
	  SET @sql = '
	  SELECT  uc.UserId,u.SSN , cc.CHECKTIME,
  		CONVERT(NVARCHAR(50),(cc.CHECKTIME),108)
	  FROM staff u
	  INNER JOIN HR_IDMapping m ON u.StaffID  = m.StaffID
	  INNER JOIN '+@DATABASENAME+'.dbo.[USERINFO] uc ON u.SSN = uc.BADGENUMBER
	  INNER JOIN '+@DATABASENAME+'.dbo.CHECKINOUT cc ON uc.USERID = cc.USERID
	  WHERE u.UserId = '+ CAST(@UserId as NVARCHAR(50)) +' AND cc.CHECKTIME >='''+CAST( @FromDate AS nvarchar(50))+''' AND cc.CHECKTIME  <='''+CAST( @ToDate  AS nvarchar(50))+'''
	  ORDER BY cc.CHECKTIME '
	  print @sql

	  INSERT @TblTimeChecking (UserIdSSN,SSN,CheckTime, HourCheck)
	  EXEC(@sql)

	-- Select * from @TblTimeChecking

	  DECLARE @TblTimeCheckingErp TABLE (
				  DayOfWeeks NVARCHAR(10)
				, CheckTime DATETIME
				, HourCheckIn NVARCHAR(50)
				, HourCheckOut NVARCHAR(50)
				, DayWork FLOAT
				, HourLate NVARCHAR(50)
				, HourEarly NVARCHAR(50)
				,DayNumber INT
				)

--IF (SELECT COUNT(UserIdSSN) FROM @TblTimeChecking)>0 -- Check Bảng Chấm Công Có Dữ Liệu Đổ Về
--BEGIN


WHILE @FromDate<=@ToDate
BEGIN
	DECLARE @DayWeek INT = datepart(dw,@FromDate) -- Trong tuần
	DECLARE @weekOfMonth INT  = (datediff(week, dateadd(week, datediff(week, 0, dateadd(month, datediff(month, 0, @FromDate), 0)), 0), @FromDate - 1) + 1) -- Tuần trong tháng
	print 'Ngày'
	print @DayWeek
	print @weekOfMonth

			----------- TÍNH THỜI GIAN ĐI MUỘN VỀ SỚM --------------------------------
			BEGIN 
			DECLARE @MorningHourEnd NVARCHAR(10)
			DECLARE @AfernoonHourStart NVARCHAR(10)
			SELECT    @MorningHourEnd=w.MorningHourEnd , @AfernoonHourStart = w.AfernoonHourStart
			FROM HR_WorkingDayMachine a
			  INNER JOIN HR_WorkingDay w ON a.WorkingDayMachineID = w.WorkingDayMachineID
			  INNER JOIN HR_IDMapping b ON a.WorkingDayMachineID = b.WorkingDayMachineID
			 WHERE b.StaffID = @UserId AND @FromDate BETWEEN ISNULL(w.StartDate, cast(cast(datepart(year,getdate()) as char(4)) + '/' + cast(IIF((datepart(month,getdate()) -1)=0,12,(datepart(month,getdate()) -1)) as char(2))+ '/28' as datetime)) AND ISNULL(w.EndDate,GETDATE())
			 --[Get_TimeKeeping]
		DECLARE @HourCheckIn VARCHAR(10), @HourCheckOut VARCHAR(10),@DayWork FLOAT = 0, @HourLate NVARCHAR(50) = '00:00',@HourEarly NVARCHAR(50) = '00:00'

		 IF @DayWeek = 7 and @StatusSat=0 AND  (select count(AutoID) from HR_WorkingDayMachineSatList a where a.[Day]=@FromDate and WorkingDayMachineID=@WorkingDayMachineID and isnull(IsFullday,0)=0 )>0 -- THỨ 7 TUẦN 2
			BEGIN
				IF(SELECT COUNT(*) FROM @TblTimeChecking WHERE  CAST(CheckTime as DATE) = CAST( @FromDate AS DATE))>1
				BEGIN
					SELECT @HourCheckIn = MIN(HourCheck)
					  ,@HourCheckOut = IIF(DATEDIFF(MINUTE,MIN(HourCheck),MAX(HourCheck))>=120 , MAX(HourCheck),NULL) 
				 FROM @TblTimeChecking
				 WHERE CAST(CheckTime as DATE) = CAST( @FromDate AS DATE)
				END
				ELSE
				BEGIN
					DECLARE @HourCheckSub VARCHAR(50)
					SET @HourCheckSub = NULL
					SELECT @HourCheckSub = MIN(HourCheck) FROM @TblTimeChecking
					 WHERE CAST(CheckTime as DATE) = CAST( @FromDate AS DATE)
					IF CAST(DATEDIFF(MINUTE,'12:00:00',@HourCheckSub) AS INT)>= 0    
					BEGIN
						
						SET @HourCheckOut = @HourCheckSub
						SET @HourCheckIn = NULL
					END
					ELSE
					BEGIN IF CAST(DATEDIFF(MINUTE,'12:00:00',@HourCheckSub) AS INT)<0   
						SET @HourCheckOut = NULL
						SET @HourCheckIn = @HourCheckSub
						SET @HourCheckSub = NULL
					END
					print 'Tinh thu 7 tuan 2'
					print @HourCheckIn
				END
			END
			ELSE
			BEGIN
				SELECT @HourCheckIn = MIN(HourCheck)
			  ,@HourCheckOut = IIF(DATEDIFF(MINUTE,MIN(HourCheck),MAX(HourCheck))>=120 , MAX(HourCheck),NULL) 
				 FROM @TblTimeChecking
				 WHERE CAST(CheckTime as DATE) = CAST( @FromDate AS DATE)
			END



		

		 IF DATEDIFF(MINUTE,@HourCheckIn,@MorningHourEnd)<=0  AND @HourCheckOut IS NULL AND @FromDate < GETDATE()
		 BEGIN
			SET @HourCheckOut = @HourCheckIn
			SET @HourCheckIn  = NULL
		 END
		 IF (DATEDIFF(MINUTE,@HourCheckIn,@MorningHourEnd) < 0 AND  DATEDIFF(MINUTE,@HourCheckIn,@AfernoonHourStart)> 0 AND @HourCheckOut IS NULL)
			OR (DATEDIFF(MINUTE,@HourCheckOut,@MorningHourEnd) < 0 AND  DATEDIFF(MINUTE,@HourCheckOut,@AfernoonHourStart)> 0 AND @HourCheckIn IS NULL)
			BEGIN
				SET @HourCheckOut = NULL
				SET @HourCheckIn  = NULL
			END
		
		END
		    -------------KẾT THÚC TÍNH THỜI GIAN ĐI MUỘN VỀ SỚM -----------------------------------
			print '--------'
			print (@FromDate)
			print (@HourCheckIn)
			print (@HourCheckOut)
			print (@UserId)

			print (CONVERT(DateTIme,CONVERT(NVARCHAR(11),@FromDate,102),102))
				--[Get_TimeKeeping]
				print '1111111111111111111111111'
				print (CONVERT(DateTIme,CONVERT(NVARCHAR(11),@FromDate,102),102))
				print @HourCheckIn
				print @HourCheckOut
				print @UserId
	SELECT @DayWork=DayWork ,@HourLate=HourLate ,@HourEarly=HourEarly FROM dbo.Get_TimeWork (CONVERT(DateTIme,CONVERT(NVARCHAR(11),@FromDate,102),102),@HourCheckIn, IIF(@HourCheckSub IS NOT NULL,'12:00:00',@HourCheckOut), @UserId)

	
	print(@FromDate)
	
	print @DayWeek
	print @weekOfMonth
	 IF @DayWeek = 7 and @StatusSat=0 AND  (select count(AutoID) from HR_WorkingDayMachineSatList a where a.[Day]=@FromDate and WorkingDayMachineID=@WorkingDayMachineID and isnull(IsFullday,0)=0 )>0 -- THỨ 7 TUẦN 2
	 print 'Thu 7 tuan 2'
		BEGIN
		if (@DayWork>0.5)
		BEGIN
			SET @DayWork = 0.5
			SET @HourEarly = '00:00'
		END

		IF (SELECT CAST(DATEDIFF(MINUTE,'12:00:00',@HourCheckSub) AS INT)) >= 0
		BEGIN
			SET @HourEarly = '00:00'
		END
	END


	print '1--------'
			print (@DayWork)
	--SELECT * FROM dbo.Get_TimeWork (@FromDate,@HourCheckIn, @HourCheckOut, @UserId)
	INSERT INTO @TblTimeCheckingErp
	VALUES(
	iif(@DayOfWeekFomat = 1
			, DATENAME(weekday, @FromDate)
			, iif (datepart(dw,@FromDate)=1,'CN','T.'+ cast (datepart(dw,@FromDate) as varchar))
		)		
		,@FromDate
		,@HourCheckIn
		,IIF(@HourCheckSub IS NOT NULL,@HourCheckSub,@HourCheckOut)
		,@DayWork
		,IIF(@HourLate IS NOT NULL,@HourLate,'00:00')
		,IIF(@HourEarly IS NOT NULL,@HourEarly,'00:00')
		,datepart(dw,@FromDate)
	) 
SET @FromDate=DATEADD(day,1,@FromDate)
SET @HourCheckSub = NULL
SET @HourCheckIn = NULL
SET @HourCheckOut = NULL
END
	


	-------------------------------
	 SELECT  @TotalDay = SUM(DayWork)
	 , @TotalHourLate = CONVERT(VARCHAR(5), DATEADD(ms, SUM(DATEDIFF(ms, '00:00:00.000', HourLate)), '00:00:00.000'),108)
	 ,@TotalHourEarly = CONVERT(VARCHAR(5), DATEADD(ms, SUM(DATEDIFF(ms, '00:00:00.000', HourEarly)), '00:00:00.000'),108)
	  FROM @TblTimeCheckingErp

	 print N'TỔNG KẾT QUẢ'
		PRINT @TotalDay
		print @TotalHourLate
		print @TotalHourEarly

	Select *  from   @TblTimeCheckingErp ORDER BY CheckTime DESC


	--[Get_TimeKeeping]
--END
--ELSE
--BEGIN
--	WHILE @FromDate<=@ToDate
--	BEGIN
--		INSERT INTO @TblTimeCheckingErp
--		VALUES(
--		iif(@DayOfWeekFomat = 1
--				, DATENAME(weekday, @FromDate)
--				, iif (datepart(dw,@FromDate)=1,'CN','T.'+ cast (datepart(dw,@FromDate) as varchar))
--			)		
--			,''
--			,''
--			,''
--			,0
--			,'00:00'
--			,'00:00'
--			,datepart(dw,@FromDate)
--		) 
--	SET @FromDate=DATEADD(day,1,@FromDate)
--	END
--	 SET  @TotalDay = 0
--	 SET @TotalHourLate =   CONVERT(VARCHAR(5),  '00:00:00.000',108)
--	 SET @TotalHourEarly = CONVERT(VARCHAR(5),  '00:00:00.000',108)
--	 Select *  from   @TblTimeCheckingErp ORDER BY CheckTime DESC
--END	
END






