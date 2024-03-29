USE [Hrm_System]
GO
/****** Object:  User [erpnhung3]    Script Date: 2/21/2020 8:22:34 PM ******/
CREATE USER [erpnhung3] FOR LOGIN [erpnhung3] WITH DEFAULT_SCHEMA=[dbo]
GO
/****** Object:  User [Erpnhung30]    Script Date: 2/21/2020 8:22:34 PM ******/
CREATE USER [Erpnhung30] WITHOUT LOGIN WITH DEFAULT_SCHEMA=[dbo]
GO
/****** Object:  User [erpnhung5]    Script Date: 2/21/2020 8:22:34 PM ******/
CREATE USER [erpnhung5] FOR LOGIN [erpnhung5] WITH DEFAULT_SCHEMA=[dbo]
GO
/****** Object:  User [tech01_admin]    Script Date: 2/21/2020 8:22:34 PM ******/
CREATE USER [tech01_admin] WITHOUT LOGIN WITH DEFAULT_SCHEMA=[dbo]
GO
/****** Object:  User [tech01_datnd]    Script Date: 2/21/2020 8:22:34 PM ******/
CREATE USER [tech01_datnd] WITHOUT LOGIN WITH DEFAULT_SCHEMA=[dbo]
GO
ALTER ROLE [db_owner] ADD MEMBER [erpnhung3]
GO
ALTER ROLE [db_owner] ADD MEMBER [Erpnhung30]
GO
ALTER ROLE [db_owner] ADD MEMBER [erpnhung5]
GO
ALTER ROLE [db_owner] ADD MEMBER [tech01_admin]
GO
ALTER ROLE [db_owner] ADD MEMBER [tech01_datnd]
GO
/****** Object:  UserDefinedTableType [dbo].[Data_Staff_ContractType]    Script Date: 2/21/2020 8:22:34 PM ******/
CREATE TYPE [dbo].[Data_Staff_ContractType] AS TABLE(
	[Id] [bigint] NULL,
	[StaffId] [bigint] NULL,
	[ContractTypeId] [bigint] NULL,
	[ContractTime] [bigint] NULL,
	[ContractCode] [nvarchar](250) NULL,
	[Name] [nvarchar](max) NULL,
	[ContractStartDate] [date] NULL,
	[ContractEndDate] [date] NULL,
	[ContractNote] [nvarchar](max) NULL,
	[ContractStatus] [bigint] NULL,
	[CreateBy] [bigint] NULL,
	[CreateDate] [datetime] NULL,
	[ModifiedBy] [bigint] NULL,
	[ModifiedDate] [datetime] NULL
)
GO
/****** Object:  UserDefinedTableType [dbo].[Data_Staff_InformationType]    Script Date: 2/21/2020 8:22:34 PM ******/
CREATE TYPE [dbo].[Data_Staff_InformationType] AS TABLE(
	[Id] [bigint] NULL,
	[StaffCode] [nvarchar](250) NULL,
	[Name] [nvarchar](250) NULL,
	[Birthday] [date] NULL,
	[GenderId] [bigint] NULL,
	[CurrentWorkingProcessId] [bigint] NULL,
	[Email] [nvarchar](max) NULL,
	[Phone] [nvarchar](max) NULL,
	[PhoneCompany] [nvarchar](max) NULL,
	[Skype] [nvarchar](250) NULL,
	[IdentityNumber] [nvarchar](100) NULL,
	[IdIssuedDate] [date] NULL,
	[IdIssuedBy] [nvarchar](250) NULL,
	[TaxId] [nvarchar](100) NULL,
	[TaxDate] [date] NULL,
	[TaxBy] [nvarchar](250) NULL,
	[UserName] [nvarchar](max) NULL,
	[BankNumber] [nvarchar](max) NULL,
	[BankName] [nvarchar](max) NULL,
	[BankBranch] [nvarchar](max) NULL,
	[Address] [nvarchar](max) NULL,
	[ContactAddress] [nvarchar](max) NULL,
	[NationalId] [bigint] NULL,
	[ProvinceId] [bigint] NULL,
	[Domicile] [nvarchar](max) NULL,
	[ProvinceDomicile] [bigint] NULL,
	[NationalDomicile] [bigint] NULL,
	[LinkFacebook] [nvarchar](max) NULL,
	[ImageLink] [nvarchar](max) NULL,
	[EmailCompany] [nvarchar](max) NULL,
	[PresentationContactName] [nvarchar](max) NULL,
	[PresentationContactPhone] [nvarchar](max) NULL,
	[Note] [nvarchar](max) NULL,
	[IsSendCheckList] [bit] NULL,
	[SendCheckListDate] [datetime] NULL,
	[SendCheckListBy] [datetime] NULL,
	[TimeLateLimit] [int] NULL,
	[TimeLeaveEarlyLimit] [int] NULL,
	[TimekeepingForm] [bigint] NULL,
	[HRAdditionId] [bigint] NULL,
	[AdditionManagerId] [bigint] NULL,
	[IsDeleted] [bit] NULL,
	[EthnicityId] [bigint] NULL,
	[MaritalStatus] [bigint] NULL,
	[IsActivated] [bit] NULL,
	[Status] [bigint] NULL,
	[Password] [nvarchar](max) NULL
)
GO
/****** Object:  UserDefinedTableType [dbo].[Data_Staff_WorkingProcessType]    Script Date: 2/21/2020 8:22:34 PM ******/
CREATE TYPE [dbo].[Data_Staff_WorkingProcessType] AS TABLE(
	[Id] [bigint] NULL,
	[StaffId] [bigint] NULL,
	[WorkingprocessTypeId] [bigint] NULL,
	[DecisionNo] [nvarchar](50) NULL,
	[WorkingStatus] [bigint] NULL,
	[DecisionDate] [datetime] NULL,
	[StartDate] [datetime] NULL,
	[EndDate] [datetime] NULL,
	[ContractId] [bigint] NULL,
	[OrganizationId] [bigint] NULL,
	[OfficeId] [bigint] NULL,
	[PositionId] [bigint] NULL,
	[ClassificationId] [bigint] NULL,
	[StaffLevelId] [bigint] NULL,
	[ManagerId] [bigint] NULL,
	[HRManagerId] [bigint] NULL,
	[HRAdditionId] [bigint] NULL,
	[AdditionManagerId] [bigint] NULL,
	[PolicyId] [bigint] NULL,
	[CurrencyId] [bigint] NULL,
	[BasicPay] [decimal](18, 4) NULL,
	[EfficiencyBonus] [decimal](18, 4) NULL,
	[PaymentForm] [bigint] NULL,
	[PaymentMethod] [bigint] NULL,
	[SalaryNote] [nvarchar](max) NULL,
	[Description] [nvarchar](max) NULL,
	[Note] [nvarchar](max) NULL,
	[IsDeleted] [bit] NULL,
	[Status] [bigint] NULL,
	[ShiftId] [bigint] NULL
)
GO
/****** Object:  UserDefinedTableType [dbo].[Data_StaffRoleType]    Script Date: 2/21/2020 8:22:34 PM ******/
CREATE TYPE [dbo].[Data_StaffRoleType] AS TABLE(
	[Id] [bigint] NULL,
	[RoleId] [bigint] NULL,
	[StaffId] [bigint] NULL,
	[Description] [nvarchar](max) NULL,
	[IsDeleted] [bit] NULL
)
GO
/****** Object:  UserDefinedFunction [dbo].[F_GetTime]    Script Date: 2/21/2020 8:22:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE FUNCTION [dbo].[F_GetTime]
(
	@FirstDate Datetime,
	@SecondDate DATETIME,
	@ThirdDate Datetime,
	@FouthDate DATETIME,
	@Direction BIT
	)
RETURNS DATETIME
AS 
BEGIN
	DECLARE @result DATETIME
	IF (@Direction = 0)
	BEGIN
		IF (ISNULL(@FirstDate, '1900-01-02') BETWEEN ISNULL(@ThirdDate, '1900-01-01') AND ISNULL(@FouthDate, '2099-12-31')) SET @result = @FirstDate
		ELSE
		IF (ISNULL(@ThirdDate, '1900-01-02') BETWEEN ISNULL(@FirstDate, '1900-01-01') AND ISNULL(@SecondDate, '2099-12-31')) SET @result = @ThirdDate
		ELSE SET @result = '1900-01-01'
	END
	ELSE
	IF (@Direction = 1)
	BEGIN
		IF (ISNULL(@SecondDate, '2099-12-30') BETWEEN ISNULL(@ThirdDate, '1900-01-01') AND ISNULL(@FouthDate, '2099-12-31')) SET @result = @SecondDate
		ELSE
		IF (ISNULL(@FouthDate, '2099-12-30') BETWEEN ISNULL(@FirstDate, '1900-01-01') AND ISNULL(@SecondDate, '2099-12-31')) SET @result = @FouthDate
		ELSE SET @result = '2099-12-31'
	END

	RETURN @result  
END
GO
/****** Object:  UserDefinedFunction [dbo].[F_Workingday_Calculation]    Script Date: 2/21/2020 8:22:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[F_Workingday_Calculation]
(
	@Date			DATETIME,
	@CheckIn		DATETIME,
	@CheckOut		DATETIME,
	@StaffId		BIGINT
)
RETURNS @Result TABLE(Workingday FLOAT, Overtime FLOAT, Late DATETIME, Early DATETIME)
AS
BEGIN
	IF @CheckIn IS NULL OR @CheckOut IS NULL
	BEGIN
		INSERT INTO @Result
		        ( Workingday, Overtime, Late, Early )
		VALUES  ( 0, -- Workingday - float
				  0, -- Overtime - float
		          CAST((CAST(CAST(@Date AS DATE) AS NVARCHAR) + ' 00:00:00.000') AS DATE), -- Late - datetime
		          CAST((CAST(CAST(@Date AS DATE) AS NVARCHAR) + ' 00:00:00.000') AS DATE)  -- Early - datetime
		          )
	END
	BEGIN
		DECLARE @CheckinTime TIME(0) = CAST(@CheckIn AS TIME(0))
		DECLARE @CheckoutTime TIME(0) = CAST(@CheckOut AS TIME(0))
		DECLARE @Workingday FLOAT
		DECLARE @WorkingdayOvertime FLOAT
		DECLARE @Late FLOAT
		DECLARE @Early FLOAT
		DECLARE @ShiftId BIGINT = (SELECT TOP 1 ShiftId 
									FROM dbo.Data_Staff_WorkingProcess 
									WHERE StaffId = @StaffId AND IsDeleted = 0
									ORDER BY StartDate DESC)

		SELECT 
			--@Workingday = SUM(IIF(ISNULL(DWSD.SalaryRatio, 0) = 0, TotalTime, 0))
			@Workingday = SUM(IIF(DWSD.IsOverTime=0, TotalTime, 0))

			--,@WorkingdayOvertime = SUM(IIF(ISNULL(DWSD.SalaryRatio, 0) != 0, 
			--									 IIF(CAST(DATEDIFF(
			--												MINUTE, 
			--												IIF(@CheckinTime <= DWSD.StartTime, DWSD.StartTime, @CheckinTime), 
			--												IIF(@CheckoutTime >= DWSD.EndTime, DWSD.EndTime, @CheckoutTime)
			--												) AS FLOAT) >= DWSD.OvertimeMinRequire, 
			--											CAST(DATEDIFF(
			--												MINUTE, 
			--												IIF(@CheckinTime <= DWSD.StartTime, DWSD.StartTime, @CheckinTime), 
			--												IIF(@CheckoutTime >= DWSD.EndTime, DWSD.EndTime, @CheckoutTime)
			--												) AS FLOAT), 
			--											0) /
			--									 CAST(DATEDIFF(MINUTE, DWSD.StartTime, DWSD.EndTime) AS FLOAT) * TotalTime * DWSD.SalaryRatio,
			--										 0))
			,@WorkingdayOvertime = SUM(IIF(DWSD.IsOverTime=1, 
												 IIF(CAST(DATEDIFF(
															MINUTE, 
															IIF(@CheckinTime <= DWSD.StartTime, DWSD.StartTime, @CheckinTime), 
															IIF(@CheckoutTime >= DWSD.EndTime, DWSD.EndTime, @CheckoutTime)
															) AS FLOAT) >= DWSD.OvertimeMinRequire, 
														CAST(DATEDIFF(
															MINUTE, 
															IIF(@CheckinTime <= DWSD.StartTime, DWSD.StartTime, @CheckinTime), 
															IIF(@CheckoutTime >= DWSD.EndTime, DWSD.EndTime, @CheckoutTime)
															) AS FLOAT), 
														0) /
												 CAST(DATEDIFF(MINUTE, DWSD.StartTime, DWSD.EndTime) AS FLOAT) * TotalTime * DWSD.SalaryRatio,
													 0))
		FROM dbo.Data_Workingday_Shift_Detail DWSD
		WHERE DWSD.ShiftId = @ShiftId AND DATEDIFF(MINUTE, DWSD.StartTime, @CheckoutTime) > 0 AND DWSD.EndTime > @CheckinTime

		SELECT 
			@Late = ISNULL(SUM(IIF(@CheckinTime >= DWSD.StartTime AND DWSD.TotalTime > 0, CAST(DATEDIFF(MINUTE, DWSD.StartTime, @CheckinTime) AS FLOAT), 0)), 0),
			@Early = ISNULL(SUM(IIF(@CheckoutTime <= DWSD.EndTime AND DWSD.TotalTime > 0, CAST(DATEDIFF(MINUTE, @CheckoutTime, DWSD.EndTime) AS FLOAT), 0)), 0)
		FROM dbo.Data_Workingday_Shift_Detail DWSD
		WHERE DWSD.ShiftId = @ShiftId AND DWSD.SalaryRatio = 0
		AND DATEDIFF(MINUTE, DWSD.StartTime, @CheckoutTime) > 0 AND DWSD.EndTime > @CheckinTime

		INSERT INTO @Result
		        ( Workingday, Overtime, Late, Early )
		VALUES  ( 
					@Workingday, -- Workingday - float
					@WorkingdayOvertime,
					CAST(
						CAST(CAST(@Date AS DATE) AS NVARCHAR) 
						+ ' ' 
						+ CAST(CAST(DATEADD(MINUTE, @Late, 0) AS TIME(0)) AS NVARCHAR)
						 AS DATETIME),
					CAST(
						CAST(CAST(@Date AS DATE) AS NVARCHAR) 
						+ ' ' 
						+ CAST(CAST(DATEADD(MINUTE, @Early, 0) AS TIME(0)) AS NVARCHAR)
						 AS DATETIME)
		          
		          )
	END
	RETURN
END
GO
/****** Object:  UserDefinedFunction [dbo].[splitstring]    Script Date: 2/21/2020 8:22:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[splitstring] ( @stringToSplit VARCHAR(MAX), @dau NVARCHAR(10) )
RETURNS
 @returnList TABLE ([Name] [nvarchar] (500))
AS
BEGIN

 DECLARE @name NVARCHAR(255)
 DECLARE @pos INT

 WHILE CHARINDEX(@dau, @stringToSplit) > 0
 BEGIN
  SELECT @pos  = CHARINDEX(@dau, @stringToSplit)  
  SELECT @name = SUBSTRING(@stringToSplit, 1, @pos-1)

  INSERT INTO @returnList 
  SELECT @name

  SELECT @stringToSplit = SUBSTRING(@stringToSplit, @pos+1, LEN(@stringToSplit)-@pos)
 END

 INSERT INTO @returnList
 SELECT @stringToSplit

 RETURN
END

GO
/****** Object:  Table [dbo].[Config_AnnualLeave]    Script Date: 2/21/2020 8:22:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Config_AnnualLeave](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[AnnualLeave] [decimal](18, 2) NULL,
	[AnnualLeaveSeniority] [decimal](18, 2) NULL,
	[NumberOfLeaveGranted] [decimal](18, 2) NULL,
	[HandlingAnnualLeaveBacklog] [bigint] NULL,
	[DateRemoveSurplusAnnualLeave] [datetime] NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Config_Insurance]    Script Date: 2/21/2020 8:22:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Config_Insurance](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[FromDate] [datetime] NULL,
	[ToDate] [datetime] NULL,
	[Note] [nvarchar](2000) NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedBy] [bigint] NULL,
	[UpdatedDate] [datetime] NULL,
	[UpdatedBy] [bigint] NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Config_InsuranceDetail]    Script Date: 2/21/2020 8:22:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Config_InsuranceDetail](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[InsuranceId] [bigint] NULL,
	[Code] [nvarchar](50) NULL,
	[Name] [nvarchar](2000) NULL,
	[RateCompany] [decimal](18, 5) NULL,
	[RatePersonal] [decimal](18, 5) NULL,
 CONSTRAINT [PK_Config_Insurance] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Config_KPI]    Script Date: 2/21/2020 8:22:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Config_KPI](
	[Id] [bigint] NOT NULL,
	[Code] [nvarchar](50) NOT NULL,
	[Name] [nvarchar](500) NOT NULL,
	[TypeId] [bigint] NOT NULL,
	[DataTypeId] [bigint] NOT NULL,
	[Formula] [nvarchar](max) NULL,
	[DataFormat] [bigint] NULL,
	[OrderNo] [bigint] NULL,
	[Description] [nvarchar](max) NULL,
	[Value] [decimal](18, 2) NULL,
	[IsEdit] [bit] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[CreatedBy] [int] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[UpdatedBy] [int] NULL,
	[UpdatedDate] [datetime] NULL,
 CONSTRAINT [PK_System_KPI] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Config_PersonalInComeTax]    Script Date: 2/21/2020 8:22:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Config_PersonalInComeTax](
	[Id] [bigint] NOT NULL,
	[Description] [nvarchar](500) NULL,
	[Deductionitself] [decimal](18, 5) NULL,
	[TardinessReduction] [decimal](18, 5) NULL,
	[StartDate] [datetime] NOT NULL,
	[EndDate] [datetime] NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedBy] [int] NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_Config_PersonalInComeTax] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Config_PersonalIncomeTaxDetail]    Script Date: 2/21/2020 8:22:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Config_PersonalIncomeTaxDetail](
	[Id] [bigint] NOT NULL,
	[PersonalIncomeTaxId] [bigint] NOT NULL,
	[FromAmount] [decimal](18, 5) NULL,
	[ToAmount] [decimal](18, 5) NULL,
	[Tax] [decimal](18, 5) NULL,
	[ProgressiveAmount] [decimal](18, 5) NULL,
	[SubtractAmount] [decimal](18, 5) NULL,
 CONSTRAINT [PK_Config_PersonalIncomeTaxDetail] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Config_Salary_Element]    Script Date: 2/21/2020 8:22:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Config_Salary_Element](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Code] [nvarchar](50) NOT NULL,
	[Name] [nvarchar](500) NOT NULL,
	[TypeId] [bigint] NOT NULL,
	[DataTypeId] [bigint] NOT NULL,
	[Formula] [nvarchar](max) NULL,
	[DataFormat] [bigint] NULL,
	[Css] [nvarchar](50) NULL,
	[OrderNo] [bigint] NULL,
	[Description] [nvarchar](max) NULL,
	[Value] [decimal](18, 5) NULL,
	[TypeLevel] [int] NOT NULL CONSTRAINT [DF_System_Salary_Element_TypeLevel]  DEFAULT ((2)),
	[IsEdit] [bit] NOT NULL CONSTRAINT [DF_System_Salary_Element_IsEdit]  DEFAULT ((0)),
	[IsDeleted] [bit] NOT NULL CONSTRAINT [DF_System_Salary_Element_IsDeleted]  DEFAULT ((0)),
	[CreatedBy] [int] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[UpdatedBy] [int] NULL,
	[UpdatedDate] [datetime] NULL,
 CONSTRAINT [PK_System_Salary_Element] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [UCCode] UNIQUE NONCLUSTERED 
(
	[Code] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Config_Salary_Type]    Script Date: 2/21/2020 8:22:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Config_Salary_Type](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](500) NOT NULL,
	[StatusId] [bigint] NOT NULL,
	[Description] [nvarchar](500) NULL,
	[IsAutoLock] [bit] NOT NULL,
	[DayLock] [int] NULL,
	[MaximumDay] [int] NULL,
	[IsDeleted] [bit] NOT NULL,
	[CreatedBy] [int] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[UpdatedBy] [int] NULL,
	[UpdatedDate] [datetime] NULL,
 CONSTRAINT [PK_System_Salary_Type] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Config_Salarytype_Element]    Script Date: 2/21/2020 8:22:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Config_Salarytype_Element](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[SalaryTypeId] [bigint] NOT NULL,
	[SalaryElementId] [bigint] NOT NULL,
	[IsEdit] [bit] NULL,
	[IsShowSalary] [bit] NOT NULL,
	[IsShowPayslip] [bit] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_System_Salarytype_Element] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Config_Salarytype_Mapper]    Script Date: 2/21/2020 8:22:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Config_Salarytype_Mapper](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[SalarytypeId] [bigint] NULL,
	[TypeId] [bigint] NULL,
	[DataId] [bigint] NULL,
	[CreatedBy] [int] NULL,
	[CreatedDate] [datetime] NULL,
	[UpdatedBy] [int] NULL,
	[UpdatedDate] [datetime] NULL,
	[IsDeleted] [bit] NULL,
 CONSTRAINT [PK_System_Salarytype_Mapper] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Data_Attachment]    Script Date: 2/21/2020 8:22:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Data_Attachment](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[FileName] [nvarchar](max) NULL,
	[DisplayFileName] [nvarchar](max) NULL,
	[FileExtension] [nvarchar](50) NULL,
	[FileSize] [float] NULL,
	[Name] [nvarchar](max) NULL,
	[Description] [nvarchar](max) NULL,
	[RecordId] [bigint] NULL,
	[DataType] [nvarchar](200) NULL,
	[IsDeleted] [bit] NULL,
	[CreatedDate] [datetime] NULL,
 CONSTRAINT [PK_Data_Attachment] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Data_Checklist]    Script Date: 2/21/2020 8:22:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Data_Checklist](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](500) NOT NULL,
	[Note] [nvarchar](max) NOT NULL,
	[CreatedBy] [int] NULL,
	[CreatedDate] [datetime] NULL,
	[UpdatedBy] [int] NULL,
	[UpdatedDate] [datetime] NULL,
	[IsDeleted] [bit] NULL,
 CONSTRAINT [PK_Data_CheckList] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Data_ChecklistAssign]    Script Date: 2/21/2020 8:22:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Data_ChecklistAssign](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[ChecklistDetailId] [bigint] NOT NULL,
	[AssigneeId] [bigint] NULL,
	[AssigneeTypeId] [bigint] NULL,
	[TypeViewId] [bigint] NULL,
	[Status] [int] NULL,
	[ParentId] [bigint] NULL,
	[CreatedBy] [int] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[UpdatedBy] [int] NULL,
	[UpdatedDate] [datetime] NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_System_ChecklistAsign] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Data_ChecklistDetail]    Script Date: 2/21/2020 8:22:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Data_ChecklistDetail](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[ChecklistId] [bigint] NOT NULL,
	[Name] [nvarchar](2000) NULL,
	[ColumnLink] [nvarchar](50) NULL,
	[ChecklistDetailTypeId] [bigint] NULL,
	[TypeControlId] [bigint] NULL,
	[IsDefault] [bit] NULL,
	[IsSendMail] [bit] NULL,
	[MailTemplateId] [bigint] NULL,
	[Note] [nvarchar](max) NULL,
	[ParentId] [bigint] NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_System_CheckListDetail] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Data_Column_Validation]    Script Date: 2/21/2020 8:22:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Data_Column_Validation](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[ColumnId] [bigint] NULL,
	[IsRequired] [bit] NULL,
	[MaxLength] [int] NULL,
	[BasicValidationTypeId] [bigint] NULL,
	[IsDeleted] [bit] NULL,
 CONSTRAINT [PK_Data_Column_Validation] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Data_LocalizedData]    Script Date: 2/21/2020 8:22:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Data_LocalizedData](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[DataId] [bigint] NULL,
	[DataType] [nvarchar](max) NULL,
	[LanguageId] [bigint] NULL,
	[PropertyName] [nvarchar](max) NULL,
	[PropertyValue] [nvarchar](500) NULL,
	[IsDefault] [bit] NULL,
	[CreatedBy] [bigint] NULL,
	[CreatedDate] [datetime] NULL,
	[UpdatedBy] [bigint] NULL,
	[UpdatedDate] [datetime] NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_Data_LoclizedMasterData] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Data_Menu_Permission]    Script Date: 2/21/2020 8:22:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Data_Menu_Permission](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[MenuId] [bigint] NULL,
	[PermissionId] [bigint] NULL,
	[IsDeleted] [bit] NULL,
 CONSTRAINT [PK_Data_Menu_Permission] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Data_Organization]    Script Date: 2/21/2020 8:22:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Data_Organization](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[OrganizationName] [nvarchar](max) NULL,
	[OrganizationCode] [nvarchar](max) NULL,
	[OrderNo] [int] NULL,
	[ParentId] [bigint] NOT NULL,
	[OrganizationType] [bigint] NOT NULL,
	[Status] [bigint] NULL,
	[Email] [nvarchar](128) NULL,
	[Phone] [nvarchar](50) NULL,
	[Branch] [bigint] NULL,
	[CurrencyTypeID] [bigint] NULL,
	[CreatedBy] [bigint] NULL,
	[CreatedDate] [datetime] NULL,
	[UpdatedBy] [bigint] NULL,
	[UpdateDate] [datetime] NULL,
	[IsDeleted] [bit] NULL,
 CONSTRAINT [PK_Organization] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Data_Payroll]    Script Date: 2/21/2020 8:22:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Data_Payroll](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[SalaryTypeId] [bigint] NULL,
	[Name] [nvarchar](500) NULL,
	[Description] [nvarchar](500) NULL,
	[Month] [int] NULL,
	[Year] [int] NULL,
	[Status] [bigint] NOT NULL,
	[TotalStaff] [int] NULL,
	[TotalAmount] [decimal](18, 4) NULL,
	[OrganizationId] [bigint] NOT NULL,
	[CreatedBy] [bigint] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[UpdatedBy] [bigint] NULL,
	[UpdatedDate] [datetime] NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_Data_Payroll] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Data_PayrollHistory]    Script Date: 2/21/2020 8:22:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Data_PayrollHistory](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[PayrollId] [bigint] NOT NULL,
	[Status] [bigint] NULL,
	[CreatedBy] [int] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
 CONSTRAINT [PK_Data_PayrollHistory] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Data_Permission]    Script Date: 2/21/2020 8:22:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Data_Permission](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[PermissionName] [nvarchar](max) NULL,
	[IsDeleted] [bit] NULL,
 CONSTRAINT [PK_Data_Permission] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Data_Pipeline]    Script Date: 2/21/2020 8:22:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Data_Pipeline](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[MenuId] [bigint] NULL,
	[Name] [nvarchar](max) NULL,
	[Description] [nvarchar](max) NULL,
	[IsDefault] [bit] NULL,
	[CreatedBy] [int] NULL,
	[CreatedDate] [datetime] NULL,
	[UpdatedBy] [int] NULL,
	[UpdatedDate] [datetime] NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_System_Pipeline] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Data_PipelineMapper]    Script Date: 2/21/2020 8:22:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Data_PipelineMapper](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[PipelineId] [bigint] NOT NULL,
	[PipelineStepId] [bigint] NULL,
 CONSTRAINT [PK_Data_PipelineMapper] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Data_PipelineStep]    Script Date: 2/21/2020 8:22:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Data_PipelineStep](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[PositionId] [int] NULL,
	[PipelineName] [nvarchar](1000) NOT NULL,
	[PipelineSymbol] [nvarchar](50) NULL,
	[PipelineRule] [nvarchar](100) NULL,
	[Description] [nvarchar](max) NULL,
	[OrderNo] [int] NULL,
	[IsDefault] [bit] NULL,
	[CreatedBy] [int] NULL,
	[CreatedDate] [datetime] NULL,
	[UpdatedBy] [int] NULL,
	[UpdatedDate] [datetime] NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_Data_PipeLine] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Data_Role]    Script Date: 2/21/2020 8:22:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Data_Role](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](1000) NOT NULL,
	[Description] [nvarchar](max) NULL,
	[IsDeleted] [bit] NULL CONSTRAINT [DF__System_Ro__IsDel__2AEB3533]  DEFAULT ((0)),
 CONSTRAINT [PK__System_R__3214EC07F6107FC4] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Data_Salary]    Script Date: 2/21/2020 8:22:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Data_Salary](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Month] [int] NOT NULL,
	[Year] [int] NOT NULL,
	[PayrollId] [bigint] NULL,
	[StaffId] [bigint] NOT NULL,
	[WorkingProcessId] [bigint] NOT NULL,
	[BasicSalary] [decimal](18, 5) NOT NULL,
	[Bonus] [decimal](18, 5) NOT NULL,
	[TotalSalary] [decimal](18, 5) NOT NULL,
	[TotalIncome] [decimal](18, 5) NOT NULL,
	[TotalReduction] [decimal](18, 5) NOT NULL,
	[Netsalary] [decimal](18, 5) NOT NULL,
	[CurentcyId] [bigint] NOT NULL,
	[CreatedBy] [bigint] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[UpdatedBy] [bigint] NULL,
	[UpdatedDate] [datetime] NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_Salary] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Data_SalaryDetail]    Script Date: 2/21/2020 8:22:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Data_SalaryDetail](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[SalaryId] [bigint] NOT NULL,
	[SalaryElementId] [bigint] NOT NULL,
	[Amount] [decimal](18, 5) NULL,
	[TypeLevel] [int] NOT NULL,
	[DataTypeId] [bigint] NOT NULL,
	[Formula] [nvarchar](max) NULL,
	[Description] [nvarchar](max) NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_Data_SalaryDetail] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Data_Setting_Email]    Script Date: 2/21/2020 8:22:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Data_Setting_Email](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NULL,
	[Title] [nvarchar](max) NULL,
	[ContentTemplate] [nvarchar](max) NULL,
	[MailTo] [nvarchar](max) NULL,
	[MailCc] [nvarchar](max) NULL,
	[MailBcc] [nvarchar](max) NULL,
	[Note] [nvarchar](max) NULL,
	[IsMailWelcomeKit] [bit] NULL,
	[CreatedBy] [bigint] NULL,
	[CreatedDate] [datetime] NOT NULL,
	[UpdatedBy] [bigint] NULL,
	[UpdatedDate] [datetime] NULL,
	[IsDeleted] [bit] NULL,
 CONSTRAINT [PK_Data_Setting_Email] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Data_Staff_Allowance]    Script Date: 2/21/2020 8:22:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Data_Staff_Allowance](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[WorkingProcessId] [bigint] NULL,
	[StaffId] [bigint] NULL,
	[Name] [nvarchar](max) NULL,
	[AllowanceType] [bigint] NULL,
	[Status] [bigint] NULL,
	[Amount] [decimal](18, 4) NULL,
	[CurrencyId] [bigint] NULL,
	[Content] [nvarchar](max) NULL,
	[Note] [nvarchar](max) NULL,
	[StartDate] [datetime] NULL,
	[EndDate] [datetime] NULL,
	[TaxExemption] [bit] NULL,
	[CreatedBy] [bigint] NULL,
	[CreatedDate] [datetime] NULL,
	[UpdatedBy] [bigint] NULL,
	[UpdatedDate] [datetime] NULL,
	[IsDeleted] [bit] NULL,
 CONSTRAINT [PK_Data_Staff_Allowance] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Data_Staff_Benefits]    Script Date: 2/21/2020 8:22:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Data_Staff_Benefits](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[WorkingProcessId] [bigint] NULL,
	[StaffId] [bigint] NULL,
	[BenefitType] [bigint] NULL,
	[Amount] [decimal](18, 4) NULL,
	[CurrencyId] [bigint] NULL,
	[Content] [nvarchar](max) NULL,
	[Note] [nvarchar](max) NULL,
	[Status] [bigint] NULL,
	[StartDate] [datetime] NULL,
	[EndDate] [datetime] NULL,
	[CreatedBy] [bigint] NULL,
	[CreatedDate] [datetime] NULL,
	[UpdatedBy] [bigint] NULL,
	[UpdatedDate] [datetime] NULL,
	[IsDeleted] [bit] NULL,
 CONSTRAINT [PK_Data_Staff_Benefits] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Data_Staff_BoardingProcess]    Script Date: 2/21/2020 8:22:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Data_Staff_BoardingProcess](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[StaffId] [bigint] NULL,
	[IsSendWelcomeKit] [bit] NULL,
	[SendWelcomeKitBy] [bigint] NULL,
	[LastSendWelcomeKitDate] [datetime] NULL,
	[IsSendChecklist] [bit] NULL,
	[CurrentChecklistId] [bigint] NULL,
	[SendChecklistBy] [bigint] NULL,
	[SendChecklistDate] [datetime] NULL,
	[OnboardingDate] [datetime] NULL,
	[OffboardingDate] [datetime] NULL,
	[Note] [nvarchar](max) NULL,
	[IsDeleted] [bit] NULL,
 CONSTRAINT [PK_Data_Staff_BoardingProcess] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Data_Staff_Bonus_Discipline]    Script Date: 2/21/2020 8:22:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Data_Staff_Bonus_Discipline](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[StaffId] [bigint] NULL,
	[TypeId] [bigint] NULL,
	[GroupId] [bigint] NULL,
	[DecisionNo] [nvarchar](max) NULL,
	[Content] [nvarchar](max) NULL,
	[ActionId] [bigint] NULL,
	[Amount] [decimal](18, 4) NULL,
	[CurrencyId] [bigint] NULL,
	[SignDate] [datetime] NULL,
	[ApplyDate] [datetime] NULL,
	[Note] [nvarchar](max) NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedBy] [bigint] NULL,
	[UpdatedBy] [bigint] NULL,
	[UpdatedDate] [datetime] NULL,
	[IsDeleted] [bit] NULL,
	[Status] [bigint] NULL,
	[PaySamePeriod] [bit] NULL,
 CONSTRAINT [PK_Data_Staff_Bonus_Discipline] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Data_Staff_Certificate]    Script Date: 2/21/2020 8:22:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Data_Staff_Certificate](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[StaffId] [bigint] NULL,
	[TypeId] [bigint] NULL,
	[Name] [nvarchar](500) NULL,
	[IssuedDate] [datetime] NULL,
	[FromDate] [date] NULL,
	[ToDate] [date] NULL,
	[RankId] [bigint] NULL,
	[IssuedBy] [nvarchar](1000) NULL,
	[Point] [decimal](18, 2) NULL,
	[Note] [nvarchar](max) NULL,
	[IsDeleted] [bit] NULL,
 CONSTRAINT [PK_Data_Staff_Certificate] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Data_Staff_ChecklistDetail]    Script Date: 2/21/2020 8:22:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Data_Staff_ChecklistDetail](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[StaffId] [bigint] NOT NULL,
	[ChecklistId] [bigint] NULL,
	[ChecklistDetailId] [bigint] NOT NULL,
	[ChecklistDetailTypeId] [bigint] NULL,
	[TypeControlId] [bigint] NULL,
	[IsDefault] [bit] NULL,
	[Name] [nvarchar](max) NULL,
	[ColumnLink] [nvarchar](max) NULL,
	[Value] [nvarchar](max) NULL,
	[Note] [nvarchar](max) NULL,
	[AssigneeId] [bigint] NULL,
	[IsFinish] [bit] NOT NULL,
	[DurationDate] [datetime] NULL,
	[ParentId] [bigint] NULL,
	[CreatedBy] [int] NULL,
	[CreatedDate] [datetime] NULL,
	[UpdatedBy] [int] NULL,
	[UpdatedDate] [datetime] NULL,
	[IsDeleted] [bit] NULL,
 CONSTRAINT [PK_Data_Staff_ChecklistDetail] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Data_Staff_Contract]    Script Date: 2/21/2020 8:22:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Data_Staff_Contract](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[StaffId] [bigint] NULL,
	[ContractTypeId] [bigint] NULL,
	[ContractTime] [bigint] NULL,
	[ContractCode] [nvarchar](250) NULL,
	[Name] [nvarchar](max) NULL,
	[ContractStartDate] [date] NULL,
	[ContractEndDate] [date] NULL,
	[ContractNote] [nvarchar](max) NULL,
	[ContractStatus] [bigint] NULL,
	[CreateBy] [bigint] NULL,
	[CreateDate] [datetime] NULL,
	[ModifiedBy] [bigint] NULL,
	[ModifiedDate] [datetime] NULL,
 CONSTRAINT [PK__Data_Sta__3214EC07DEB4891F] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Data_Staff_Experience]    Script Date: 2/21/2020 8:22:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Data_Staff_Experience](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[StaffId] [bigint] NOT NULL,
	[FromDate] [date] NULL,
	[ToDate] [date] NULL,
	[OfficePositionID] [bigint] NULL,
	[OfficeRoleID] [bigint] NULL,
	[CompanyName] [nvarchar](max) NULL,
	[Salary] [decimal](18, 4) NULL,
	[CurrencyId] [bigint] NULL,
	[Note] [nvarchar](max) NULL,
	[IsDeleted] [bit] NULL,
 CONSTRAINT [PK_Data_Staff_Experience] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Data_Staff_HealthInsurance]    Script Date: 2/21/2020 8:22:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Data_Staff_HealthInsurance](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[StaffId] [bigint] NOT NULL,
	[InsuranceCode] [nvarchar](250) NULL,
	[InsuranceNumber] [nvarchar](250) NULL,
	[TypeId] [bigint] NULL,
	[PlaceHealthCare] [nvarchar](max) NULL,
	[IssuedDate] [date] NULL,
	[StartDate] [date] NULL,
	[EndDate] [date] NULL,
	[Status] [bigint] NULL,
	[Note] [nvarchar](max) NULL,
	[CreatedBy] [bigint] NULL,
	[CreatedDate] [datetime] NULL,
	[UpdatedBy] [bigint] NULL,
	[UpdatedDate] [datetime] NULL,
	[IsDeleted] [bit] NULL,
 CONSTRAINT [PK__Data_Sta__3214EC0788AB18A2] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Data_Staff_Information]    Script Date: 2/21/2020 8:22:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Data_Staff_Information](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[StaffCode] [nvarchar](250) NULL,
	[Name] [nvarchar](250) NULL,
	[Birthday] [date] NULL,
	[GenderId] [bigint] NULL,
	[CurrentWorkingProcessId] [bigint] NULL,
	[Email] [nvarchar](max) NULL,
	[Phone] [nvarchar](max) NULL,
	[PhoneCompany] [nvarchar](max) NULL,
	[Skype] [nvarchar](250) NULL,
	[IdentityNumber] [nvarchar](100) NULL,
	[IdIssuedDate] [date] NULL,
	[IdIssuedBy] [nvarchar](250) NULL,
	[TaxId] [nvarchar](100) NULL,
	[TaxDate] [date] NULL,
	[TaxBy] [nvarchar](250) NULL,
	[UserName] [nvarchar](max) NULL,
	[BankNumber] [nvarchar](max) NULL,
	[BankAccount] [nvarchar](250) NULL,
	[BankName] [nvarchar](max) NULL,
	[BankBranch] [nvarchar](max) NULL,
	[Address] [nvarchar](max) NULL,
	[ContactAddress] [nvarchar](max) NULL,
	[NationalId] [bigint] NULL,
	[ProvinceId] [bigint] NULL,
	[Domicile] [nvarchar](max) NULL,
	[ProvinceDomicile] [bigint] NULL,
	[NationalDomicile] [bigint] NULL,
	[LinkFacebook] [nvarchar](max) NULL,
	[ImageLink] [nvarchar](max) NULL,
	[EmailCompany] [nvarchar](max) NULL,
	[PresentationContactName] [nvarchar](max) NULL,
	[PresentationContactPhone] [nvarchar](max) NULL,
	[Note] [nvarchar](max) NULL,
	[IsSendCheckList] [bit] NULL,
	[SendCheckListDate] [datetime] NULL,
	[SendCheckListBy] [datetime] NULL,
	[TimeLateLimit] [int] NULL,
	[TimeLeaveEarlyLimit] [int] NULL,
	[TimekeepingForm] [bigint] NULL,
	[HRAdditionId] [bigint] NULL,
	[AdditionManagerId] [bigint] NULL,
	[IsDeleted] [bit] NULL,
	[EthnicityId] [bigint] NULL,
	[MaritalStatus] [bigint] NULL,
	[CurrencyId] [bigint] NULL,
	[IsActivated] [bit] NULL CONSTRAINT [DF_Data_Staff_Information_IsActivated]  DEFAULT ((1)),
 CONSTRAINT [PK_Data_Staff_Information] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Data_Staff_Parent]    Script Date: 2/21/2020 8:22:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Data_Staff_Parent](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[StaffId] [int] NOT NULL,
	[ParentId] [int] NOT NULL,
	[Type] [int] NULL,
	[FromDate] [datetime] NULL,
	[ToDate] [datetime] NULL,
 CONSTRAINT [PK_Data_Staff_Parent] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Data_Staff_ParentTest]    Script Date: 2/21/2020 8:22:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Data_Staff_ParentTest](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[StaffId] [int] NOT NULL,
	[ParentId] [int] NOT NULL,
	[Type] [int] NULL,
	[FromDate] [datetime] NULL,
	[ToDate] [datetime] NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Data_Staff_PipelineStatus]    Script Date: 2/21/2020 8:22:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Data_Staff_PipelineStatus](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[StaffId] [bigint] NULL,
	[PipelineId] [bigint] NULL,
	[PipelineStepId] [bigint] NULL,
	[Date] [datetime] NULL,
	[Note] [nvarchar](max) NULL,
	[IsDeleted] [bit] NULL,
 CONSTRAINT [PK_Data_Staff_PipeLineStatus] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Data_Staff_Relationships]    Script Date: 2/21/2020 8:22:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Data_Staff_Relationships](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[StaffId] [bigint] NULL,
	[RelationshipId] [bigint] NULL,
	[Name] [nvarchar](250) NULL,
	[BirthDay] [date] NULL,
	[Phone] [nvarchar](250) NULL,
	[Status] [bigint] NULL,
	[IsEmergency] [bit] NULL,
	[Deduction] [bit] NULL,
	[DeductionCode] [nvarchar](50) NULL,
	[DeductionFrom] [datetime] NULL,
	[DeductionTo] [datetime] NULL,
	[Note] [nvarchar](max) NULL,
	[CreatedDate] [datetime] NULL CONSTRAINT [DF_Data_Staff_Relationships_CreatedDate]  DEFAULT (getdate()),
	[CreatedBy] [bigint] NULL,
	[UpdatedBy] [bigint] NULL,
	[UpdatedDate] [datetime] NULL,
	[IsDeleted] [bit] NULL CONSTRAINT [DF_Data_Staff_Relationships_IsDeleted]  DEFAULT ((0)),
 CONSTRAINT [PK_Data_Staff_Relationships] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Data_Staff_Role_Permission]    Script Date: 2/21/2020 8:22:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Data_Staff_Role_Permission](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[DataId] [bigint] NULL,
	[DataTypeId] [bigint] NULL,
	[MenuPermissionId] [bigint] NULL,
	[IsDeleted] [bit] NULL,
 CONSTRAINT [PK_Data_Staff_Role_Permission] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Data_Staff_Salary_WorkingDay]    Script Date: 2/21/2020 8:22:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Data_Staff_Salary_WorkingDay](
	[Id] [bigint] NOT NULL,
	[StaffId] [bigint] NOT NULL,
	[WorkingProcessId] [bigint] NOT NULL,
	[Month] [int] NOT NULL,
	[Year] [int] NOT NULL,
	[Standardworkingday] [decimal](18, 5) NULL,
	[Workingday] [decimal](18, 5) NULL,
	[TotalHourDuration] [decimal](18, 5) NULL,
	[OverTime] [decimal](18, 5) NULL,
	[WorkingdayOfficial] [decimal](18, 5) NULL,
	[WorkingdayProbation] [decimal](18, 5) NOT NULL,
	[WorkingdayProbationExtension] [decimal](18, 5) NULL,
	[BasicSalary] [decimal](18, 5) NULL,
	[Bonus] [decimal](18, 5) NULL,
	[CreatedBy] [int] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[UpdatedBy] [int] NULL,
	[UpdatedDate] [datetime] NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_Data_Staff_Salary_WorkingDay] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Data_Staff_Salary_WorkingDayAlowence]    Script Date: 2/21/2020 8:22:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Data_Staff_Salary_WorkingDayAlowence](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[AllowanceId] [bigint] NOT NULL,
	[StaffId] [bigint] NOT NULL,
	[WorkingProcessId] [bigint] NOT NULL,
	[Month] [int] NOT NULL,
	[Year] [int] NOT NULL,
	[BasicSalary] [float] NULL,
	[Bonus] [float] NULL,
	[Standardworkingday] [float] NULL,
	[Workingday] [float] NULL,
	[TotalHourDuration] [decimal](18, 5) NULL,
	[OverTime] [decimal](18, 5) NULL,
	[WorkingdayOfficial] [decimal](18, 5) NULL,
	[WorkingdayProbation] [decimal](18, 5) NOT NULL,
	[WorkingdayProbationExtension] [decimal](18, 5) NULL,
	[AllowanceAmount] [decimal](18, 5) NULL,
	[CreatedBy] [bigint] NULL,
	[CreatedDate] [datetime] NULL,
	[UpdatedBy] [bigint] NULL,
	[UpdatedDate] [datetime] NULL,
	[IsDeleted] [bit] NULL,
 CONSTRAINT [PK_Data_Staff_Salary_WorkingDayAlowence] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Data_Staff_SocialInsurance]    Script Date: 2/21/2020 8:22:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Data_Staff_SocialInsurance](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[StaffId] [bigint] NULL,
	[TypeId] [bigint] NULL,
	[InsuranceCode] [nvarchar](250) NULL,
	[InsuranceNumber] [nvarchar](250) NULL,
	[FamilyCode] [nvarchar](250) NULL,
	[PlaceHold] [nvarchar](max) NULL,
	[UnionAmount] [decimal](18, 4) NULL,
	[CurrencyId] [bigint] NULL,
	[DateReturn] [date] NULL,
	[Status] [bigint] NULL,
	[RateCompany] [float] NULL,
	[RatePerson] [float] NULL,
	[MonthStart] [date] NULL,
	[StartDate] [date] NULL,
	[EndDate] [date] NULL,
	[Note] [nvarchar](max) NULL,
 CONSTRAINT [PK_Data_Staff_SocialInsurance] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Data_Staff_SocialInsuranceDetail]    Script Date: 2/21/2020 8:22:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Data_Staff_SocialInsuranceDetail](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[InsuranceId] [bigint] NOT NULL,
	[StaffId] [bigint] NOT NULL,
	[FromMonth] [date] NULL,
	[ToMonth] [date] NULL,
	[Status] [bigint] NOT NULL,
	[DateReturn] [datetime] NULL,
	[PlaceHealthCare] [nvarchar](max) NULL,
	[Regime] [nvarchar](250) NULL,
	[Note] [nvarchar](max) NULL,
	[UnionAmount] [decimal](18, 4) NULL,
	[BasicSalary] [decimal](18, 4) NULL,
	[RateCompany] [float] NULL,
	[RatePerson] [float] NULL,
	[ApproveStatus] [bigint] NULL,
 CONSTRAINT [PK_Data_Staff_SocialInsuranceDetail] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Data_Staff_WorkingDay_Machine]    Script Date: 2/21/2020 8:22:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Data_Staff_WorkingDay_Machine](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[StaffId] [bigint] NULL,
	[WorkingDayMachineId] [bigint] NULL,
	[SSN] [nvarchar](250) NULL,
	[Note] [nvarchar](max) NULL,
	[CreatedBy] [bigint] NULL,
	[CreatedDate] [datetime] NULL,
	[UpdatedBy] [bigint] NULL,
	[UpdatedDate] [datetime] NULL,
	[IsDeleted] [bit] NULL,
 CONSTRAINT [PK__Data_Sta__3214EC07C4A8039E] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Data_Staff_WorkingProcess]    Script Date: 2/21/2020 8:22:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Data_Staff_WorkingProcess](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[StaffId] [bigint] NOT NULL,
	[WorkingprocessTypeId] [bigint] NULL,
	[DecisionNo] [nvarchar](50) NULL,
	[WorkingStatus] [bigint] NULL,
	[DecisionDate] [datetime] NULL,
	[StartDate] [datetime] NULL,
	[EndDate] [datetime] NULL,
	[ContractId] [bigint] NULL,
	[OrganizationId] [bigint] NULL,
	[OfficeId] [bigint] NULL,
	[PositionId] [bigint] NULL,
	[ClassificationId] [bigint] NULL,
	[StaffLevelId] [bigint] NULL,
	[ManagerId] [bigint] NULL,
	[HRManagerId] [bigint] NULL,
	[PolicyId] [bigint] NULL,
	[CurrencyId] [bigint] NULL,
	[BasicPay] [decimal](18, 4) NULL,
	[EfficiencyBonus] [decimal](18, 4) NULL,
	[PaymentForm] [bigint] NULL,
	[PaymentMethod] [bigint] NULL,
	[SalaryNote] [nvarchar](max) NULL,
	[Description] [nvarchar](max) NULL,
	[Note] [nvarchar](max) NULL,
	[IsDeleted] [bit] NULL CONSTRAINT [DF_Data_Staff_WorkingProcess_IsDeleted]  DEFAULT ((0)),
	[Status] [bigint] NULL,
	[ShiftId] [bigint] NULL,
	[CreatedDate] [datetime] NULL CONSTRAINT [DF_Data_Staff_WorkingProcess_CreatedDate]  DEFAULT (getdate()),
	[CreatedBy] [bigint] NULL,
	[UpdatedDate] [datetime] NULL,
	[UpdatedBy] [bigint] NULL,
 CONSTRAINT [PK_Data_Staff_WorkingProcess] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Data_Staff_WorkingProcess20200210]    Script Date: 2/21/2020 8:22:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Data_Staff_WorkingProcess20200210](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[StaffId] [bigint] NOT NULL,
	[WorkingprocessTypeId] [bigint] NULL,
	[DecisionNo] [nvarchar](50) NULL,
	[WorkingStatus] [bigint] NULL,
	[DecisionDate] [datetime] NULL,
	[StartDate] [datetime] NULL,
	[EndDate] [datetime] NULL,
	[ContractId] [bigint] NULL,
	[OrganizationId] [bigint] NULL,
	[OfficeId] [bigint] NULL,
	[PositionId] [bigint] NULL,
	[ClassificationId] [bigint] NULL,
	[StaffLevelId] [bigint] NULL,
	[ManagerId] [bigint] NULL,
	[HRManagerId] [bigint] NULL,
	[PolicyId] [bigint] NULL,
	[CurrencyId] [bigint] NULL,
	[BasicPay] [decimal](18, 4) NULL,
	[EfficiencyBonus] [decimal](18, 4) NULL,
	[PaymentForm] [bigint] NULL,
	[PaymentMethod] [bigint] NULL,
	[SalaryNote] [nvarchar](max) NULL,
	[Description] [nvarchar](max) NULL,
	[Note] [nvarchar](max) NULL,
	[IsDeleted] [bit] NULL,
	[Status] [bigint] NULL,
	[ShiftId] [bigint] NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedBy] [bigint] NULL,
	[UpdatedDate] [datetime] NULL,
	[UpdatedBy] [bigint] NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Data_Staff_WorkingProcess20200218]    Script Date: 2/21/2020 8:22:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Data_Staff_WorkingProcess20200218](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[StaffId] [bigint] NOT NULL,
	[WorkingprocessTypeId] [bigint] NULL,
	[DecisionNo] [nvarchar](50) NULL,
	[WorkingStatus] [bigint] NULL,
	[DecisionDate] [datetime] NULL,
	[StartDate] [datetime] NULL,
	[EndDate] [datetime] NULL,
	[ContractId] [bigint] NULL,
	[OrganizationId] [bigint] NULL,
	[OfficeId] [bigint] NULL,
	[PositionId] [bigint] NULL,
	[ClassificationId] [bigint] NULL,
	[StaffLevelId] [bigint] NULL,
	[ManagerId] [bigint] NULL,
	[HRManagerId] [bigint] NULL,
	[PolicyId] [bigint] NULL,
	[CurrencyId] [bigint] NULL,
	[BasicPay] [decimal](18, 4) NULL,
	[EfficiencyBonus] [decimal](18, 4) NULL,
	[PaymentForm] [bigint] NULL,
	[PaymentMethod] [bigint] NULL,
	[SalaryNote] [nvarchar](max) NULL,
	[Description] [nvarchar](max) NULL,
	[Note] [nvarchar](max) NULL,
	[IsDeleted] [bit] NULL,
	[Status] [bigint] NULL,
	[ShiftId] [bigint] NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedBy] [bigint] NULL,
	[UpdatedDate] [datetime] NULL,
	[UpdatedBy] [bigint] NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Data_StaffParent_WorkingProcess]    Script Date: 2/21/2020 8:22:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Data_StaffParent_WorkingProcess](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[StaffId] [bigint] NOT NULL,
	[ParentId] [bigint] NULL,
	[Type] [bigint] NULL,
	[FromDate] [datetime] NULL,
	[ToDate] [datetime] NULL,
	[WorkingProcessId] [bigint] NULL,
 CONSTRAINT [PK_StaffParentWorkingProcess] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Data_StaffRole]    Script Date: 2/21/2020 8:22:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Data_StaffRole](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[RoleId] [bigint] NOT NULL,
	[StaffId] [bigint] NOT NULL,
	[Description] [nvarchar](max) NULL,
	[IsDeleted] [bit] NULL DEFAULT ((0)),
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Data_User]    Script Date: 2/21/2020 8:22:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Data_User](
	[Id] [bigint] NOT NULL,
	[StaffId] [bigint] NULL,
	[UserName] [nvarchar](max) NULL,
	[DisplayName] [nvarchar](max) NULL,
	[Password] [nvarchar](max) NULL,
	[UserType] [int] NULL,
	[Status] [int] NULL,
	[LanguageId] [bigint] NULL,
	[IsActivate] [bit] NULL,
	[IsLocked] [bit] NULL,
	[LockedDate] [datetime] NULL,
	[LockedBy] [int] NULL,
	[ForgotCode] [nvarchar](50) NULL,
	[ForgotExpired] [datetime] NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedBy] [bigint] NULL,
	[UpdatedDate] [datetime] NULL,
	[UpdatedBy] [bigint] NULL,
	[IsDeleted] [bit] NULL CONSTRAINT [DF_Data_User_IsDeleted]  DEFAULT ((0)),
 CONSTRAINT [PK_Data_User] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Data_Workingday_AnnualLeave]    Script Date: 2/21/2020 8:22:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Data_Workingday_AnnualLeave](
	[AutoId] [bigint] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NULL,
	[StaffId] [bigint] NOT NULL,
	[AnnualLeave] [decimal](18, 2) NULL,
	[TypeId] [bigint] NULL,
	[RegimeId] [bigint] NULL,
	[PeriodApplyId] [bigint] NULL,
	[Status] [bigint] NULL,
	[DateApply] [datetime] NULL,
	[ExpiryDate] [datetime] NULL,
	[CreatedDate] [datetime] NOT NULL,
	[CreatedBy] [bigint] NOT NULL,
	[Note] [nvarchar](max) NULL,
 CONSTRAINT [PK_Workingday_AnnualLeave] PRIMARY KEY CLUSTERED 
(
	[AutoId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Data_Workingday_AnnualLeave_Staff_Mapper]    Script Date: 2/21/2020 8:22:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Data_Workingday_AnnualLeave_Staff_Mapper](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[StaffId] [bigint] NULL,
	[AnnualLeaveId] [bigint] NULL,
	[IsDeleted] [bit] NULL,
 CONSTRAINT [PK_Data_Workingday_AnnualLeave_Staff_Mapper] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Data_Workingday_CheckInOut]    Script Date: 2/21/2020 8:22:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Data_Workingday_CheckInOut](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[StaffId] [bigint] NULL,
	[WorkingDayMachineId] [bigint] NULL,
	[SSN] [bigint] NULL,
	[CheckTime] [datetime] NULL,
	[CheckType] [nvarchar](50) NULL,
	[TypeHolidayId] [bigint] NULL,
	[Emotion] [nvarchar](max) NULL,
	[ImageUrl] [nvarchar](max) NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedBy] [bigint] NULL,
 CONSTRAINT [PK_Data_Workingday_CheckInOut] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Data_Workingday_Holiday]    Script Date: 2/21/2020 8:22:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Data_Workingday_Holiday](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NULL,
	[ClassifyId] [bigint] NULL,
	[IsAnnualLeave] [bit] NOT NULL CONSTRAINT [DF_Data_Workingday_Holiday_IsAnnualLeave]  DEFAULT ((0)),
	[SalaryRegimeId] [bigint] NULL,
	[DesistRegimeId] [bigint] NULL,
	[OvertimeRate] [float] NULL,
	[FromDate] [date] NULL,
	[ToDate] [date] NULL,
	[Note] [nvarchar](max) NULL,
	[CreatedBy] [bigint] NULL,
	[CreatedDate] [datetime] NULL,
	[IsDeleted] [bit] NOT NULL CONSTRAINT [DF_Data_Workingday_Holiday_IsDeleted]  DEFAULT ((0)),
 CONSTRAINT [PK_Data_WorkingDay_Holiday] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Data_Workingday_HolidayShift]    Script Date: 2/21/2020 8:22:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Data_Workingday_HolidayShift](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[HolidayId] [bigint] NULL,
	[ShiftId] [bigint] NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_Workingday_HolidayShift] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Data_WorkingDay_Machine]    Script Date: 2/21/2020 8:22:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Data_WorkingDay_Machine](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](250) NULL,
	[DatabaseName] [varchar](250) NULL,
	[DeviceId] [bigint] NULL,
	[Place] [nvarchar](200) NULL,
	[Port] [bigint] NULL,
	[Ip] [varchar](50) NULL,
	[Password] [nvarchar](500) NULL,
	[CreatedBy] [bigint] NULL,
	[CreatedDate] [datetime] NULL,
	[UpdatedBy] [bigint] NULL,
	[UpdatedDate] [datetime] NULL,
	[IsDeleted] [bit] NULL,
 CONSTRAINT [PK__HR_Worki__3214EC07082C81EE] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Data_Workingday_Period]    Script Date: 2/21/2020 8:22:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Data_Workingday_Period](
	[AutoId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](500) NULL,
	[FromDay] [int] NULL,
	[Today] [int] NULL,
	[Status] [bigint] NULL,
	[Note] [nvarchar](max) NULL,
	[IsDeleted] [bit] NULL,
	[IsDefault] [bit] NULL,
	[MaximumEdition] [int] NULL,
	[CreatedBy] [int] NULL,
	[CreatedDate] [datetime] NULL,
 CONSTRAINT [PK_Workingday_period] PRIMARY KEY CLUSTERED 
(
	[AutoId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Data_Workingday_Shift]    Script Date: 2/21/2020 8:22:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Data_Workingday_Shift](
	[ShiftId] [bigint] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[PeriodId] [bigint] NOT NULL,
	[DatabaseName] [varchar](50) NOT NULL,
	[IsOverNight] [bit] NOT NULL,
	[LateAllowed] [decimal](18, 2) NULL,
	[EarlyAllowed] [decimal](18, 2) NULL,
	[ToShift] [time](7) NULL,
	[FromShift] [time](7) NULL,
	[WorkId] [bigint] NULL,
	[Status] [bigint] NULL,
	[IsDefault] [bit] NULL,
	[IsDeleted] [bit] NULL CONSTRAINT [DF_Data_Workingday_Shift_IsDeleted]  DEFAULT ((0)),
 CONSTRAINT [PK_Workingday_Shift] PRIMARY KEY CLUSTERED 
(
	[ShiftId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Data_Workingday_Shift_Detail]    Script Date: 2/21/2020 8:22:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Data_Workingday_Shift_Detail](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[ShiftId] [bigint] NOT NULL,
	[StartTime] [time](7) NOT NULL,
	[EndTime] [time](7) NOT NULL,
	[TotalTime] [decimal](18, 2) NULL,
	[StartDate] [date] NOT NULL,
	[EndDate] [date] NULL,
	[Status] [int] NOT NULL,
	[OvertimeMinRequire] [decimal](18, 2) NULL,
	[SalaryRatio] [decimal](18, 2) NULL,
	[IsLunchBreak] [bit] NULL,
	[IsOverTime] [bit] NULL,
	[T2] [bit] NOT NULL,
	[T3] [bit] NOT NULL,
	[T4] [bit] NOT NULL,
	[T5] [bit] NOT NULL,
	[T6] [bit] NOT NULL,
	[T7] [bit] NOT NULL,
	[CN] [bit] NOT NULL,
	[IsDeleted] [bit] NULL,
 CONSTRAINT [PK_Workingday_Shift_Detail] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Data_Workingday_Shift_Mapper]    Script Date: 2/21/2020 8:22:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Data_Workingday_Shift_Mapper](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[ShiftId] [bigint] NOT NULL,
	[Month] [int] NOT NULL,
	[Year] [int] NOT NULL,
	[WorkId] [bigint] NULL,
	[StandardWorkingday] [decimal](18, 2) NOT NULL,
	[StartDate] [date] NULL,
	[EndDate] [date] NULL,
 CONSTRAINT [PK_Data_Workingday_Shiff_Mapper] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Data_WorkingDay_Summary]    Script Date: 2/21/2020 8:22:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Data_WorkingDay_Summary](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[StaffId] [bigint] NULL,
	[ShiftId] [bigint] NULL,
	[WorkId] [bigint] NULL,
	[Date] [date] NULL,
	[StartTime] [datetime] NULL,
	[EndTime] [datetime] NULL,
	[CheckIn] [datetime] NULL,
	[CheckOut] [datetime] NULL,
	[LateDuration] [datetime] NULL,
	[EarlyDuration] [datetime] NULL,
	[Workingday] [decimal](6, 2) NULL,
	[WorkingdayOvertime] [decimal](6, 2) NULL,
	[WorkingdaySupplement] [decimal](6, 2) NULL,
	[WorkingdayAnnualLeave] [decimal](6, 2) NULL,
	[WorkingdayUnpaidLeave] [decimal](6, 2) NULL,
	[OtherWorkingday] [decimal](6, 2) NULL,
	[TotalWorkingday] [decimal](6, 2) NULL,
	[TypeHolidayId] [bigint] NULL,
	[WorkingdayTypeId] [bigint] NULL,
 CONSTRAINT [PK_WorkingDay_Summary] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Data_WorkingDay_Supplement]    Script Date: 2/21/2020 8:22:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Data_WorkingDay_Supplement](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[StaffId] [bigint] NULL,
	[SupplementTypeId] [bigint] NULL,
	[Date] [date] NULL,
	[StartTime] [datetime] NULL,
	[EndTime] [datetime] NULL,
	[MissingTimeDuration] [datetime] NULL,
	[OvertimeRate] [decimal](5, 2) NULL,
	[ReasonTypeId] [bigint] NULL,
	[Note] [nvarchar](max) NULL,
	[IsCompleted] [bit] NULL,
	[CreatedBy] [bigint] NULL,
	[CreatedDate] [datetime] NULL,
	[UpdatedBy] [bigint] NULL,
	[UpdatedDate] [datetime] NULL,
	[IsDeleted] [bit] NULL,
 CONSTRAINT [PK_WorkingDay_Supplement] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Data_Workingday_Supplement_Approval]    Script Date: 2/21/2020 8:22:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Data_Workingday_Supplement_Approval](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[RequestId] [bigint] NULL,
	[RequestStatusId] [bigint] NULL,
	[ApprovedBy] [bigint] NULL,
	[ApprovedDate] [datetime] NULL,
	[Note] [nvarchar](max) NULL,
	[IsDeleted] [bit] NULL,
 CONSTRAINT [PK_Data_Workingday_Supplement_Approval] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Data_Workingday_Supplement_Configuration]    Script Date: 2/21/2020 8:22:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Data_Workingday_Supplement_Configuration](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[RequesterId] [bigint] NULL,
	[RequesterTypeId] [bigint] NULL,
	[CurrentRequestStatusId] [bigint] NULL,
	[NextRequestStatusId] [bigint] NULL,
	[RequestActionId] [bigint] NULL,
	[IsLastStep] [bigint] NULL,
	[IsDeleted] [bit] NULL,
 CONSTRAINT [PK_Data_Workingday_Supplement_Configuration] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Data_Workingday_Supplement_Configuration_Exception]    Script Date: 2/21/2020 8:22:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Data_Workingday_Supplement_Configuration_Exception](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[StaffId] [bigint] NULL,
	[OrganizationId] [bigint] NULL,
	[PreApprovalStatus] [bigint] NULL,
	[ApprovedBy] [nvarchar](max) NULL,
	[Note] [nvarchar](500) NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedBy] [bigint] NULL,
	[IsDeleted] [bit] NULL,
 CONSTRAINT [PK_Data_Supplement_Configuration_Exception] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Data_WorkingDay_WorkDay]    Script Date: 2/21/2020 8:22:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Data_WorkingDay_WorkDay](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[PeriodID] [bigint] NULL,
	[Monday] [bit] NULL,
	[Tuesday] [bit] NULL,
	[Wednesday] [bit] NULL,
	[Thursday] [bit] NULL,
	[Friday] [bit] NULL,
	[Saturday] [bit] NULL,
	[Sunday] [bit] NULL,
	[OrderWeek] [bigint] NULL,
	[IsDeleted] [bit] NULL,
 CONSTRAINT [PK_Data_WorkingDay_WorkDay] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[System_Language]    Script Date: 2/21/2020 8:22:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[System_Language](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[LanguageCulture] [nvarchar](20) NOT NULL,
	[ImageName] [nvarchar](50) NULL,
	[DisplayOrder] [int] NOT NULL,
	[IsDeleted] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[System_LocaleStringResource]    Script Date: 2/21/2020 8:22:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[System_LocaleStringResource](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[LanguageId] [int] NOT NULL,
	[ResourceName] [nvarchar](200) NOT NULL,
	[ResourceValue] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK__LocaleSt__3214EC0799187FBA] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[System_MasterData]    Script Date: 2/21/2020 8:22:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[System_MasterData](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Code] [nvarchar](200) NOT NULL,
	[Name] [nvarchar](1000) NULL,
	[Value] [nvarchar](1000) NULL,
	[GroupId] [bigint] NULL,
	[Color] [nvarchar](20) NULL,
	[Description] [nvarchar](max) NULL,
	[OrderNo] [bigint] NULL,
	[IsActive] [bit] NOT NULL CONSTRAINT [DF_System_MasterData_IsActive]  DEFAULT ((1)),
	[IsDisbleEditing] [bit] NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_MasterData] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[System_Menu]    Script Date: 2/21/2020 8:22:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[System_Menu](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[MenuName] [nvarchar](max) NULL,
	[RouteUrl] [nvarchar](max) NULL,
	[Icon] [nvarchar](max) NULL,
	[OrderNo] [int] NULL,
	[ParentId] [bigint] NULL,
	[IsIncludeMenu] [bit] NULL,
	[Searchable] [bit] NULL,
	[MenuPositionId] [bigint] NULL,
	[CreatedBy] [bigint] NULL,
	[CreatedDate] [datetime] NULL,
	[UpdatedBy] [bigint] NULL,
	[UpdatedDate] [datetime] NULL,
	[IsDeleted] [bit] NULL,
	[MenuGroupType] [int] NULL,
 CONSTRAINT [PK_Menu] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[System_MultipleLanguageConfiguration]    Script Date: 2/21/2020 8:22:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[System_MultipleLanguageConfiguration](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[DataType] [nvarchar](max) NULL,
	[Property] [nvarchar](max) NULL,
	[IsDeleted] [bit] NULL,
 CONSTRAINT [PK_System_MultipleLanguageConfiguration] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[System_Permission_Condition]    Script Date: 2/21/2020 8:22:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[System_Permission_Condition](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[MenuId] [bigint] NOT NULL,
	[PermissionId] [bigint] NOT NULL,
	[PermissionTypeId] [int] NOT NULL,
	[Condition] [nvarchar](max) NOT NULL,
	[Delim] [nvarchar](50) NOT NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_System_Permission_Condition] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[System_Table]    Script Date: 2/21/2020 8:22:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[System_Table](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[TableName] [nvarchar](max) NULL,
	[SqlTableName] [nvarchar](max) NULL,
	[CreatedBy] [bigint] NULL,
	[CreatedDate] [datetime] NULL,
	[UpdatedBy] [bigint] NULL,
	[UpdatedDate] [datetime] NULL,
	[IsDeleted] [bit] NULL,
	[SqlAlias] [nvarchar](100) NULL,
	[Description] [nvarchar](max) NULL,
 CONSTRAINT [PK_Table] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[System_Table_Column]    Script Date: 2/21/2020 8:22:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[System_Table_Column](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[TableId] [bigint] NULL,
	[ColumnName] [nvarchar](max) NULL,
	[ColumnDataTypeId] [bigint] NULL,
	[SqlData] [nvarchar](max) NULL,
	[OrderNo] [int] NULL,
	[IsUpdatingField] [bit] NULL,
	[IsDeleted] [bit] NULL,
	[IsLocked] [bit] NULL,
	[IsFilter] [bit] NULL CONSTRAINT [DF_System_Table_Column_IsFilter]  DEFAULT ((1)),
	[OriginalColumnName] [nvarchar](250) NULL,
	[OriginalAliasTableName] [nvarchar](250) NULL,
 CONSTRAINT [PK_Data_Table_Column] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[System_TableConfigDefault]    Script Date: 2/21/2020 8:22:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[System_TableConfigDefault](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[TableId] [bigint] NULL,
	[ConfigData] [nvarchar](max) NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedBy] [bigint] NULL,
	[UpdatedDate] [datetime] NULL,
	[UpdatedBy] [bigint] NULL,
	[IsDeleted] [bit] NULL,
 CONSTRAINT [PK_TableConfigDefault] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[User_TableConfig]    Script Date: 2/21/2020 8:22:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User_TableConfig](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[UserId] [bigint] NULL,
	[TableId] [bigint] NULL,
	[ConfigData] [nvarchar](max) NULL,
	[QueryData] [nvarchar](max) NULL,
	[FilterData] [nvarchar](max) NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedBy] [bigint] NULL,
	[UpdatedDate] [datetime] NULL,
	[UpdatedBy] [bigint] NULL,
	[IsDeleted] [bit] NULL,
 CONSTRAINT [PK_TableConfig] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
ALTER TABLE [dbo].[Config_PersonalInComeTax] ADD  CONSTRAINT [DF_Config_PersonalInComeTax_IsDeleted]  DEFAULT ((0)) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[Data_Salary] ADD  CONSTRAINT [DF_Salary_BasicSalary]  DEFAULT ((0)) FOR [BasicSalary]
GO
ALTER TABLE [dbo].[Data_Salary] ADD  CONSTRAINT [DF_Salary_Bonus]  DEFAULT ((0)) FOR [Bonus]
GO
ALTER TABLE [dbo].[Data_Salary] ADD  CONSTRAINT [DF_Salary_TotalSalary]  DEFAULT ((0)) FOR [TotalSalary]
GO
ALTER TABLE [dbo].[Data_Salary] ADD  CONSTRAINT [DF_Salary_TotalIncome]  DEFAULT ((0)) FOR [TotalIncome]
GO
ALTER TABLE [dbo].[Data_Salary] ADD  CONSTRAINT [DF_Salary_TotalReduction]  DEFAULT ((0)) FOR [TotalReduction]
GO
ALTER TABLE [dbo].[Data_Salary] ADD  CONSTRAINT [DF_Salary_Netsalary]  DEFAULT ((0)) FOR [Netsalary]
GO
ALTER TABLE [dbo].[Data_SalaryDetail] ADD  CONSTRAINT [DF_Data_SalaryDetail_TypeLevel]  DEFAULT ((2)) FOR [TypeLevel]
GO
/****** Object:  StoredProcedure [dbo].[Dependency_Organization_Get_GetOrganizationById]    Script Date: 2/21/2020 8:22:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE[dbo].[Dependency_Organization_Get_GetOrganizationById]
AS 
BEGIN	
	SELECT 
			[Id],
			[OrganizationName],
			[OrganizationCode],
			[OrderNo],
			[ParentId],
			[OrganizationType],
			[Status],
			[Email],
			[Phone],
			[Branch],
			[CurrencyTypeID],
			[CreatedBy],
			[CreatedDate],
			[UpdatedBy],
			[UpdateDate],
			[IsDeleted]
	  FROM [dbo].[Data_Organization]
END

GO
/****** Object:  StoredProcedure [dbo].[Dependency_Organization_Get_GetParentOrganization]    Script Date: 2/21/2020 8:22:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE[dbo].[Dependency_Organization_Get_GetParentOrganization]
AS 
BEGIN	
	SELECT 
			[Id],
			[OrganizationName],
			[OrganizationCode],
			[OrderNo],
			[ParentId],
			[OrganizationType],
			[Status],
			[Email],
			[Phone],
			[Branch],
			[CurrencyTypeID],
			[CreatedBy],
			[CreatedDate],
			[UpdatedBy],
			[UpdateDate],
			[IsDeleted]
	  FROM [dbo].[Data_Organization]
END

GO
/****** Object:  StoredProcedure [dbo].[Dependency_Pipeline_Get_GetPipeline]    Script Date: 2/21/2020 8:22:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE[dbo].[Dependency_Pipeline_Get_GetPipeline]
AS 
BEGIN	
	SELECT [Id]
	      ,[MenuId]
	      ,[Name]
	      ,[Description]
	      ,[IsDefault]
	      ,[CreatedBy]
	      ,[CreatedDate]
	      ,[UpdatedBy]
	      ,[UpdatedDate]
	      ,[IsDeleted]
	  FROM [dbo].[Data_Pipeline];
	  SELECT [Id]
	      ,[MenuName]
	      ,[RouteUrl]
	      ,[Icon]
	      ,[OrderNo]
	      ,[ParentId]
	      ,[CreatedBy]
	      ,[CreatedDate]
	      ,[UpdatedBy]
	      ,[UpdatedDate]
	      ,[IsDeleted]
	  FROM [dbo].[System_Menu];
	  SELECT [Id]
      ,[PipelineId]
      ,[PipelineStepId]
  FROM [dbo].[Data_PipelineMapper];
  SELECT [Id]
      ,[PositionId]
      ,[PipelineName]
      ,[PipelineSymbol]
      ,[PipelineRule]
      ,[Description]
      ,[OrderNo]
      ,[IsDefault]
      ,[CreatedBy]
      ,[CreatedDate]
      ,[UpdatedBy]
      ,[UpdatedDate]
      ,[IsDeleted]
  FROM [dbo].[Data_PipelineStep];
  SELECT [Id]
      ,[Name]
      ,[Value]
      ,[GroupId]
      ,[Color]
      ,[Description]
      ,[OrderNo]
      ,[IsActive]
      ,[IsDeleted]
  FROM [dbo].[System_MasterData];
END

GO
/****** Object:  StoredProcedure [dbo].[Dependency_Pipeline_Get_GetPipelineById]    Script Date: 2/21/2020 8:22:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE[dbo].[Dependency_Pipeline_Get_GetPipelineById]
AS 
BEGIN	
	SELECT [Id]
	      ,[MenuId]
	      ,[Name]
	      ,[Description]
	      ,[IsDefault]
	      ,[CreatedBy]
	      ,[CreatedDate]
	      ,[UpdatedBy]
	      ,[UpdatedDate]
	      ,[IsDeleted]
	  FROM [dbo].[Data_Pipeline]	 
END

GO
/****** Object:  StoredProcedure [dbo].[Dependency_Pipeline_Get_GetPipelineStepByMenuName]    Script Date: 2/21/2020 8:22:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE[dbo].[Dependency_Pipeline_Get_GetPipelineStepByMenuName]
AS 
BEGIN	
	SELECT [Id]
	      ,[MenuId]
	      ,[Name]
	      ,[Description]
	      ,[IsDefault]
	      ,[CreatedBy]
	      ,[CreatedDate]
	      ,[UpdatedBy]
	      ,[UpdatedDate]
	      ,[IsDeleted]
	  FROM [dbo].[Data_Pipeline];
	  SELECT
	      	[Id],
			[MenuName]
			[RouteUrl],
			[Icon],
			[OrderNo],
			[ParentId],
			[IsIncludeMenu],
			[Searchable],
			[MenuPositionId],
			[CreatedBy],
			[CreatedDate],
			[UpdatedBy],
			[UpdatedDate],
			[IsDeleted]
	  FROM [dbo].[System_Menu];
	  SELECT [Id]
      ,[PipelineId]
      ,[PipelineStepId]
  FROM [dbo].[Data_PipelineMapper];
  SELECT [Id]
      ,[PositionId]
      ,[PipelineName]
      ,[PipelineSymbol]
      ,[PipelineRule]
      ,[Description]
      ,[OrderNo]
      ,[IsDefault]
      ,[CreatedBy]
      ,[CreatedDate]
      ,[UpdatedBy]
      ,[UpdatedDate]
      ,[IsDeleted]
  FROM [dbo].[Data_PipelineStep];
  SELECT [Id]
      ,[Name]
      ,[Value]
      ,[GroupId]
      ,[Color]
      ,[Description]
      ,[OrderNo]
      ,[IsActive]
      ,[IsDeleted]
  FROM [dbo].[System_MasterData];
  SELECT [Id],
		[StaffId],
		[PipelineId],
		[PipelineStepId],
		[Date],
		[Note],
		[IsDeleted]
  FROM [dbo].[Data_Staff_PipelineStatus]
END

GO
/****** Object:  StoredProcedure [dbo].[Dependency_Pipeline_Get_GetPipelineStepByPipelineId]    Script Date: 2/21/2020 8:22:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE[dbo].[Dependency_Pipeline_Get_GetPipelineStepByPipelineId]
AS 
BEGIN	
	SELECT [Id]
	      ,[MenuId]
	      ,[Name]
	      ,[Description]
	      ,[IsDefault]
	      ,[CreatedBy]
	      ,[CreatedDate]
	      ,[UpdatedBy]
	      ,[UpdatedDate]
	      ,[IsDeleted]
	  FROM [dbo].[Data_Pipeline];
	  SELECT [Id]
	      ,[MenuName]
	      ,[RouteUrl]
	      ,[Icon]
	      ,[OrderNo]
	      ,[ParentId]
	      ,[CreatedBy]
	      ,[CreatedDate]
	      ,[UpdatedBy]
	      ,[UpdatedDate]
	      ,[IsDeleted]
	  FROM [dbo].[System_Menu];
	  SELECT [Id]
      ,[PipelineId]
      ,[PipelineStepId]
  FROM [dbo].[Data_PipelineMapper];
  SELECT [Id]
      ,[PositionId]
      ,[PipelineName]
      ,[PipelineSymbol]
      ,[PipelineRule]
      ,[Description]
      ,[OrderNo]
      ,[IsDefault]
      ,[CreatedBy]
      ,[CreatedDate]
      ,[UpdatedBy]
      ,[UpdatedDate]
      ,[IsDeleted]
  FROM [dbo].[Data_PipelineStep];
  SELECT [Id]
      ,[Name]
      ,[Value]
      ,[GroupId]
      ,[Color]
      ,[Description]
      ,[OrderNo]
      ,[IsActive]
      ,[IsDeleted]
  FROM [dbo].[System_MasterData];
END

GO
/****** Object:  StoredProcedure [dbo].[Dependency_Role_Get_GetRole]    Script Date: 2/21/2020 8:22:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE[dbo].[Dependency_Role_Get_GetRole]
AS 
BEGIN	
	SELECT 
		[Id],
		[Name],
		[Description]
	  FROM [dbo].[Data_Role]
END

GO
/****** Object:  StoredProcedure [dbo].[Dependency_Staff_Get_GetPipelineStepStaffByMenuName]    Script Date: 2/21/2020 8:22:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE[dbo].[Dependency_Staff_Get_GetPipelineStepStaffByMenuName]
AS 
BEGIN	
	SELECT [Id]
      ,[StaffCode]
      ,[Name]
      ,[Birthday]
      ,[GenderId]
      ,[Email]
      ,[Phone]
      ,[IdentityNumber]
      ,[IdIssuedDate]
      ,[IdIssuedBy]
      ,[TaxId]
      ,[TaxDate]
      ,[TaxBy]
      ,[UserName]
      ,[BankNumber]
      ,[BankName]
      ,[Address]
      ,[ContactAddress]
      ,[NationalId]
      ,[LinkFacebook]
      ,[ImageLink]
      ,[EmailCompany]
      ,[PresentationContactName]
      ,[PresentationContactPhone]
      ,[Note]
      ,[IsSendCheckList]
      ,[SendCheckListDate]
      ,[SendCheckListBy]
      ,[IsDeleted]
  FROM [dbo].[Data_Staff_Information];
  SELECT [Id]
      ,[StaffId]
      ,[IsSendChecklist]
      ,[SendChecklistBy]
      ,[SendChecklistDate]
      ,[OnboardingDate]
      ,[OffboardingDate]
      ,[Note]
      ,[IsDeleted]
  FROM [dbo].[Data_Staff_BoardingProcess];
  SELECT [Id]
      ,[StaffId]
      ,[PipelineId]
      ,[PipelineStepId]
      ,[CreatedBy]
      ,[CreatedDate]
      ,[UpdatedBy]
      ,[UpdatedDate]
      ,[Note]
      ,[IsDeleted]
  FROM [dbo].[Data_Staff_PipelineStatus];
  SELECT [Id]
      ,[MenuId]
      ,[Name]
      ,[Description]
      ,[IsDefault]
      ,[CreatedBy]
      ,[CreatedDate]
      ,[UpdatedBy]
      ,[UpdatedDate]
      ,[IsDeleted]
  FROM [dbo].[Data_Pipeline];
  SELECT [Id]
      ,[MenuName]
      ,[RouteUrl]
      ,[Icon]
      ,[OrderNo]
      ,[ParentId]
      ,[CreatedBy]
      ,[CreatedDate]
      ,[UpdatedBy]
      ,[UpdatedDate]
      ,[IsDeleted]
  FROM [dbo].[System_Menu];

END

GO
/****** Object:  StoredProcedure [dbo].[Dependency_Staff_Get_GetStaffByOrganizationId]    Script Date: 2/21/2020 8:22:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE[dbo].[Dependency_Staff_Get_GetStaffByOrganizationId]
AS 
BEGIN	
	SELECT [Id],
			[StaffId],
			[WorkingprocessTypeId],
			[DecisionNo],
			[WorkingStatus],
			[DecisionDate],
			[StartDate],
			[EndDate],
			[ContractId],
			[OrganizationId],
			[OfficeId],
			[PositionId],
			[ClassificationId],
			[StaffLevelId],
			[ManagerId],
			[HRManagerId],
			[HRAdditionId],
			[AdditionManagerId],
			[PolicyId] [BIGINT],
			[CurrencyId] [BIGINT],
			[BasicPay],
			[EfficiencyBonus],
			[Description],
			[Note],
			[IsDeleted],
			[Status],
			[ShiftId] ,
			[CreatedDate],
			[CreatedBy],
			[UpdatedDate],
			[UpdatedBy]
	  FROM [dbo].[Data_Staff_WorkingProcess];
	  
	  SELECT
			[Id],
			[StaffCode],
			[Name],
			[Birthday],
			[GenderId],
			[Email],
			[Phone],
			[PhoneCompany],
			[Skype],
			[IdentityNumber],
			[IdIssuedDate],
			[IdIssuedBy],
			[TaxId],
			[TaxDate],
			[TaxBy],
			[UserName],
			[BankNumber],
			[BankName],
			[BankBranch],
			[Address],
			[ContactAddress],
			[NationalId],
			[LinkFacebook],
			[ImageLink],
			[EmailCompany],
			[PresentationContactName],
			[PresentationContactPhone],
			[Note],
			[IsSendCheckList],
			[SendCheckListDate],
			[SendCheckListBy],
			[IsDeleted],
			[EthnicityId] ,
			[MaritalStatus]
	  FROM	[dbo].[Data_Staff_Information]
END

GO
/****** Object:  StoredProcedure [dbo].[Dependency_System_Full_GetLanguage]    Script Date: 2/21/2020 8:22:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE[dbo].[Dependency_System_Full_GetLanguage]
AS 
BEGIN	
	SELECT 
		Id,
		Name,
		LanguageCulture,
		ImageName
	FROM dbo.System_Language;
END

GO
/****** Object:  StoredProcedure [dbo].[Dependency_System_Full_GetPermission]    Script Date: 2/21/2020 8:22:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE[dbo].[Dependency_System_Full_GetPermission]
AS 
BEGIN	
	SELECT 
		[Id],
		[DataId],
		[DataTypeId],
		[MenuPermissionId],
		[IsDeleted]
	FROM [dbo].[Data_Staff_Role_Permission];
	SELECT 
		[Id],
		[MenuId],
		[PermissionId],
		[IsDeleted]
	FROM [dbo].[Data_Menu_Permission];
	SELECT 
		[Id],
		[MenuName],
		[ParentId],
		[IsIncludeMenu],
		[MenuPositionID],
		[IsDeleted]
	FROM [dbo].[System_Menu];
	SELECT 
		[Id],
		[PermissionName],
		[IsDeleted]
	FROM [dbo].[Data_Permission];
	SELECT 
		[Id],
		[StaffId],
		[ParentId],
		[Type],
		[FromDate],
		[ToDate]
	FROM [dbo].[Data_Staff_Parent];
	SELECT 
		[Id],
		[RoleId],
		[StaffId],
		[IsDeleted]
	FROM [dbo].[Data_StaffRole];
	SELECT 
		[Id],
		[Name],
		[GroupId],
		[IsActive],
		[IsDeleted]
	FROM [dbo].[System_MasterData]
END
GO
/****** Object:  StoredProcedure [dbo].[Dependency_System_Full_GetResources]    Script Date: 2/21/2020 8:22:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE[dbo].[Dependency_System_Full_GetResources]
AS 
BEGIN	
	SELECT 
		[LanguageId] ,
	    [ResourceName] ,
	    [ResourceValue]
	FROM dbo.System_LocaleStringResource;
END
GO
/****** Object:  StoredProcedure [dbo].[Dependency_System_Get_GetLocalizedData]    Script Date: 2/21/2020 8:22:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE[dbo].[Dependency_System_Get_GetLocalizedData]
AS 
BEGIN	
	SELECT 
		[DataId],
		[DataType],
		[PropertyName],
		[PropertyValue],
		[IsDeleted]			
	FROM dbo.Data_LocalizedData;
END

GO
/****** Object:  StoredProcedure [dbo].[Dependency_System_Get_GetMenu]    Script Date: 2/21/2020 8:22:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE[dbo].[Dependency_System_Get_GetMenu]
AS 
BEGIN	
	SELECT 
		[Id],
		[MenuName],
		[RouteUrl],
		[Icon],
		[ParentId],
		[IsIncludeMenu]			
	FROM dbo.System_Menu;
END

GO
/****** Object:  StoredProcedure [dbo].[Dependency_System_Get_GetMultipleLanguageConfiguration]    Script Date: 2/21/2020 8:22:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE[dbo].[Dependency_System_Get_GetMultipleLanguageConfiguration]
AS 
BEGIN	
	SELECT 
		[DataType] ,
	    [Property]
	FROM dbo.System_MultipleLanguageConfiguration
END
GO
/****** Object:  StoredProcedure [dbo].[Dependency_System_Get_GetTable]    Script Date: 2/21/2020 8:22:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE[dbo].[Dependency_System_Get_GetTable]
AS 
BEGIN	
	SELECT 
		[Id],
		[TableName],
		[CreatedBy],
		[CreatedDate],
		[UpdatedBy],
		[UpdatedDate],
		[IsDeleted]
	FROM dbo.System_Table;
END
GO
/****** Object:  StoredProcedure [dbo].[Dependency_System_Get_GetTableColumn]    Script Date: 2/21/2020 8:22:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATe PROCEDURE[dbo].[Dependency_System_Get_GetTableColumn]
AS 
BEGIN	
	SELECT 
		[Id],
		[TableId],
		[ColumnName],
		[OrderNo],
		[IsDeleted]
	  FROM [dbo].[System_Table_Column]
END

GO
/****** Object:  StoredProcedure [dbo].[Dependency_System_Get_MasterData]    Script Date: 2/21/2020 8:22:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE[dbo].[Dependency_System_Get_MasterData]
AS 
BEGIN	
	SELECT 
		[Id],
		[GroupId],
		[Name],
		[Value],
		[Description],
		[OrderNo],
		[IsActive],
		[IsDeleted]
	FROM dbo.System_MasterData
END
GO
/****** Object:  StoredProcedure [dbo].[Dependency_System_Get_MasterDataByGroupId]    Script Date: 2/21/2020 8:22:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE[dbo].[Dependency_System_Get_MasterDataByGroupId]
AS 
BEGIN	
	SELECT 
		[Id],
		[GroupId],
		[Name],
		[Value],
		[Description],
		[OrderNo],
		[IsActive],
		[IsDeleted],
		[ColorId]
	FROM dbo.System_MasterData
END
GO
/****** Object:  StoredProcedure [dbo].[Dependency_System_Get_MasterDataByName]    Script Date: 2/21/2020 8:22:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE[dbo].[Dependency_System_Get_MasterDataByName]
AS 
BEGIN	
	SELECT 
		[Id],
		[GroupId],
		[Name],
		[Value],
		[Description],
		[OrderNo],
		[IsActive],
		[IsDeleted],
		[ColorId]
	FROM dbo.System_MasterData
END
GO
/****** Object:  StoredProcedure [dbo].[Dependency_System_Get_MasterDataColor]    Script Date: 2/21/2020 8:22:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE[dbo].[Dependency_System_Get_MasterDataColor]
AS 
BEGIN	
	SELECT 
		[Id],
		[Color],
		[IsDeleted]
	FROM dbo.System_MasterData
END
GO
/****** Object:  StoredProcedure [dbo].[Dependency_System_Get_TableConfigDefault]    Script Date: 2/21/2020 8:22:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE[dbo].[Dependency_System_Get_TableConfigDefault]
AS 
BEGIN	
	SELECT 
		[Id],
		[TableId],
		[ConfigData],
		[IsDeleted]
	FROM dbo.System_TableConfigDefault;
END
GO
/****** Object:  StoredProcedure [dbo].[Nhung]    Script Date: 2/21/2020 8:22:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Nhung] 
AS
BEGIN
	Declare @fromDate datetime2;
Declare @toDate datetime2;

Set @fromDate = '2012-01-01';
Set @toDate = '2012-12-01';

With dt As
(
Select @fromDate As [TheDate]
Union All
Select DateAdd(month, 1, TheDate) From dt Where [TheDate] < @toDate
)
Select
    dt.TheDate
    
From
    dt
END

GO
/****** Object:  StoredProcedure [dbo].[SqlQueryNotificationStoredProcedure-1fb034a0-6c9e-4eed-a50e-8ae0a05037e9]    Script Date: 2/21/2020 8:22:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SqlQueryNotificationStoredProcedure-1fb034a0-6c9e-4eed-a50e-8ae0a05037e9] AS BEGIN BEGIN TRANSACTION; RECEIVE TOP(0) conversation_handle FROM [SqlQueryNotificationService-1fb034a0-6c9e-4eed-a50e-8ae0a05037e9]; IF (SELECT COUNT(*) FROM [SqlQueryNotificationService-1fb034a0-6c9e-4eed-a50e-8ae0a05037e9] WHERE message_type_name = 'http://schemas.microsoft.com/SQL/ServiceBroker/DialogTimer') > 0 BEGIN if ((SELECT COUNT(*) FROM sys.services WHERE name = 'SqlQueryNotificationService-1fb034a0-6c9e-4eed-a50e-8ae0a05037e9') > 0)   DROP SERVICE [SqlQueryNotificationService-1fb034a0-6c9e-4eed-a50e-8ae0a05037e9]; if (OBJECT_ID('SqlQueryNotificationService-1fb034a0-6c9e-4eed-a50e-8ae0a05037e9', 'SQ') IS NOT NULL)   DROP QUEUE [SqlQueryNotificationService-1fb034a0-6c9e-4eed-a50e-8ae0a05037e9]; DROP PROCEDURE [SqlQueryNotificationStoredProcedure-1fb034a0-6c9e-4eed-a50e-8ae0a05037e9]; END COMMIT TRANSACTION; END
GO
/****** Object:  StoredProcedure [dbo].[SqlQueryNotificationStoredProcedure-292f1c03-190c-4e38-833f-03e371b106d7]    Script Date: 2/21/2020 8:22:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SqlQueryNotificationStoredProcedure-292f1c03-190c-4e38-833f-03e371b106d7] AS BEGIN BEGIN TRANSACTION; RECEIVE TOP(0) conversation_handle FROM [SqlQueryNotificationService-292f1c03-190c-4e38-833f-03e371b106d7]; IF (SELECT COUNT(*) FROM [SqlQueryNotificationService-292f1c03-190c-4e38-833f-03e371b106d7] WHERE message_type_name = 'http://schemas.microsoft.com/SQL/ServiceBroker/DialogTimer') > 0 BEGIN if ((SELECT COUNT(*) FROM sys.services WHERE name = 'SqlQueryNotificationService-292f1c03-190c-4e38-833f-03e371b106d7') > 0)   DROP SERVICE [SqlQueryNotificationService-292f1c03-190c-4e38-833f-03e371b106d7]; if (OBJECT_ID('SqlQueryNotificationService-292f1c03-190c-4e38-833f-03e371b106d7', 'SQ') IS NOT NULL)   DROP QUEUE [SqlQueryNotificationService-292f1c03-190c-4e38-833f-03e371b106d7]; DROP PROCEDURE [SqlQueryNotificationStoredProcedure-292f1c03-190c-4e38-833f-03e371b106d7]; END COMMIT TRANSACTION; END
GO
/****** Object:  StoredProcedure [dbo].[SqlQueryNotificationStoredProcedure-3105338e-87ad-4db6-a2f2-57fabc97d1b7]    Script Date: 2/21/2020 8:22:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SqlQueryNotificationStoredProcedure-3105338e-87ad-4db6-a2f2-57fabc97d1b7] AS BEGIN BEGIN TRANSACTION; RECEIVE TOP(0) conversation_handle FROM [SqlQueryNotificationService-3105338e-87ad-4db6-a2f2-57fabc97d1b7]; IF (SELECT COUNT(*) FROM [SqlQueryNotificationService-3105338e-87ad-4db6-a2f2-57fabc97d1b7] WHERE message_type_name = 'http://schemas.microsoft.com/SQL/ServiceBroker/DialogTimer') > 0 BEGIN if ((SELECT COUNT(*) FROM sys.services WHERE name = 'SqlQueryNotificationService-3105338e-87ad-4db6-a2f2-57fabc97d1b7') > 0)   DROP SERVICE [SqlQueryNotificationService-3105338e-87ad-4db6-a2f2-57fabc97d1b7]; if (OBJECT_ID('SqlQueryNotificationService-3105338e-87ad-4db6-a2f2-57fabc97d1b7', 'SQ') IS NOT NULL)   DROP QUEUE [SqlQueryNotificationService-3105338e-87ad-4db6-a2f2-57fabc97d1b7]; DROP PROCEDURE [SqlQueryNotificationStoredProcedure-3105338e-87ad-4db6-a2f2-57fabc97d1b7]; END COMMIT TRANSACTION; END
GO
/****** Object:  StoredProcedure [dbo].[SqlQueryNotificationStoredProcedure-3cf3c9f7-c1aa-4227-a67b-b429fac4f1b0]    Script Date: 2/21/2020 8:22:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SqlQueryNotificationStoredProcedure-3cf3c9f7-c1aa-4227-a67b-b429fac4f1b0] AS BEGIN BEGIN TRANSACTION; RECEIVE TOP(0) conversation_handle FROM [SqlQueryNotificationService-3cf3c9f7-c1aa-4227-a67b-b429fac4f1b0]; IF (SELECT COUNT(*) FROM [SqlQueryNotificationService-3cf3c9f7-c1aa-4227-a67b-b429fac4f1b0] WHERE message_type_name = 'http://schemas.microsoft.com/SQL/ServiceBroker/DialogTimer') > 0 BEGIN if ((SELECT COUNT(*) FROM sys.services WHERE name = 'SqlQueryNotificationService-3cf3c9f7-c1aa-4227-a67b-b429fac4f1b0') > 0)   DROP SERVICE [SqlQueryNotificationService-3cf3c9f7-c1aa-4227-a67b-b429fac4f1b0]; if (OBJECT_ID('SqlQueryNotificationService-3cf3c9f7-c1aa-4227-a67b-b429fac4f1b0', 'SQ') IS NOT NULL)   DROP QUEUE [SqlQueryNotificationService-3cf3c9f7-c1aa-4227-a67b-b429fac4f1b0]; DROP PROCEDURE [SqlQueryNotificationStoredProcedure-3cf3c9f7-c1aa-4227-a67b-b429fac4f1b0]; END COMMIT TRANSACTION; END
GO
/****** Object:  StoredProcedure [dbo].[SqlQueryNotificationStoredProcedure-58eaa230-95d3-4b32-9d05-0419b8249037]    Script Date: 2/21/2020 8:22:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SqlQueryNotificationStoredProcedure-58eaa230-95d3-4b32-9d05-0419b8249037] AS BEGIN BEGIN TRANSACTION; RECEIVE TOP(0) conversation_handle FROM [SqlQueryNotificationService-58eaa230-95d3-4b32-9d05-0419b8249037]; IF (SELECT COUNT(*) FROM [SqlQueryNotificationService-58eaa230-95d3-4b32-9d05-0419b8249037] WHERE message_type_name = 'http://schemas.microsoft.com/SQL/ServiceBroker/DialogTimer') > 0 BEGIN if ((SELECT COUNT(*) FROM sys.services WHERE name = 'SqlQueryNotificationService-58eaa230-95d3-4b32-9d05-0419b8249037') > 0)   DROP SERVICE [SqlQueryNotificationService-58eaa230-95d3-4b32-9d05-0419b8249037]; if (OBJECT_ID('SqlQueryNotificationService-58eaa230-95d3-4b32-9d05-0419b8249037', 'SQ') IS NOT NULL)   DROP QUEUE [SqlQueryNotificationService-58eaa230-95d3-4b32-9d05-0419b8249037]; DROP PROCEDURE [SqlQueryNotificationStoredProcedure-58eaa230-95d3-4b32-9d05-0419b8249037]; END COMMIT TRANSACTION; END
GO
/****** Object:  StoredProcedure [dbo].[SqlQueryNotificationStoredProcedure-7ff35acb-bc9f-4ed3-bb3e-1f35c8d5c98b]    Script Date: 2/21/2020 8:22:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SqlQueryNotificationStoredProcedure-7ff35acb-bc9f-4ed3-bb3e-1f35c8d5c98b] AS BEGIN BEGIN TRANSACTION; RECEIVE TOP(0) conversation_handle FROM [SqlQueryNotificationService-7ff35acb-bc9f-4ed3-bb3e-1f35c8d5c98b]; IF (SELECT COUNT(*) FROM [SqlQueryNotificationService-7ff35acb-bc9f-4ed3-bb3e-1f35c8d5c98b] WHERE message_type_name = 'http://schemas.microsoft.com/SQL/ServiceBroker/DialogTimer') > 0 BEGIN if ((SELECT COUNT(*) FROM sys.services WHERE name = 'SqlQueryNotificationService-7ff35acb-bc9f-4ed3-bb3e-1f35c8d5c98b') > 0)   DROP SERVICE [SqlQueryNotificationService-7ff35acb-bc9f-4ed3-bb3e-1f35c8d5c98b]; if (OBJECT_ID('SqlQueryNotificationService-7ff35acb-bc9f-4ed3-bb3e-1f35c8d5c98b', 'SQ') IS NOT NULL)   DROP QUEUE [SqlQueryNotificationService-7ff35acb-bc9f-4ed3-bb3e-1f35c8d5c98b]; DROP PROCEDURE [SqlQueryNotificationStoredProcedure-7ff35acb-bc9f-4ed3-bb3e-1f35c8d5c98b]; END COMMIT TRANSACTION; END
GO
/****** Object:  StoredProcedure [dbo].[SqlQueryNotificationStoredProcedure-8bf1c67f-aca4-44f5-99b5-4cb38ace71b2]    Script Date: 2/21/2020 8:22:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SqlQueryNotificationStoredProcedure-8bf1c67f-aca4-44f5-99b5-4cb38ace71b2] AS BEGIN BEGIN TRANSACTION; RECEIVE TOP(0) conversation_handle FROM [SqlQueryNotificationService-8bf1c67f-aca4-44f5-99b5-4cb38ace71b2]; IF (SELECT COUNT(*) FROM [SqlQueryNotificationService-8bf1c67f-aca4-44f5-99b5-4cb38ace71b2] WHERE message_type_name = 'http://schemas.microsoft.com/SQL/ServiceBroker/DialogTimer') > 0 BEGIN if ((SELECT COUNT(*) FROM sys.services WHERE name = 'SqlQueryNotificationService-8bf1c67f-aca4-44f5-99b5-4cb38ace71b2') > 0)   DROP SERVICE [SqlQueryNotificationService-8bf1c67f-aca4-44f5-99b5-4cb38ace71b2]; if (OBJECT_ID('SqlQueryNotificationService-8bf1c67f-aca4-44f5-99b5-4cb38ace71b2', 'SQ') IS NOT NULL)   DROP QUEUE [SqlQueryNotificationService-8bf1c67f-aca4-44f5-99b5-4cb38ace71b2]; DROP PROCEDURE [SqlQueryNotificationStoredProcedure-8bf1c67f-aca4-44f5-99b5-4cb38ace71b2]; END COMMIT TRANSACTION; END
GO
/****** Object:  StoredProcedure [dbo].[SqlQueryNotificationStoredProcedure-9b444eea-7710-4b72-86ae-5afb6fadc887]    Script Date: 2/21/2020 8:22:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SqlQueryNotificationStoredProcedure-9b444eea-7710-4b72-86ae-5afb6fadc887] AS BEGIN BEGIN TRANSACTION; RECEIVE TOP(0) conversation_handle FROM [SqlQueryNotificationService-9b444eea-7710-4b72-86ae-5afb6fadc887]; IF (SELECT COUNT(*) FROM [SqlQueryNotificationService-9b444eea-7710-4b72-86ae-5afb6fadc887] WHERE message_type_name = 'http://schemas.microsoft.com/SQL/ServiceBroker/DialogTimer') > 0 BEGIN if ((SELECT COUNT(*) FROM sys.services WHERE name = 'SqlQueryNotificationService-9b444eea-7710-4b72-86ae-5afb6fadc887') > 0)   DROP SERVICE [SqlQueryNotificationService-9b444eea-7710-4b72-86ae-5afb6fadc887]; if (OBJECT_ID('SqlQueryNotificationService-9b444eea-7710-4b72-86ae-5afb6fadc887', 'SQ') IS NOT NULL)   DROP QUEUE [SqlQueryNotificationService-9b444eea-7710-4b72-86ae-5afb6fadc887]; DROP PROCEDURE [SqlQueryNotificationStoredProcedure-9b444eea-7710-4b72-86ae-5afb6fadc887]; END COMMIT TRANSACTION; END
GO
/****** Object:  StoredProcedure [dbo].[SqlQueryNotificationStoredProcedure-9dd4a657-ad01-48c1-9391-a7b34f305659]    Script Date: 2/21/2020 8:22:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SqlQueryNotificationStoredProcedure-9dd4a657-ad01-48c1-9391-a7b34f305659] AS BEGIN BEGIN TRANSACTION; RECEIVE TOP(0) conversation_handle FROM [SqlQueryNotificationService-9dd4a657-ad01-48c1-9391-a7b34f305659]; IF (SELECT COUNT(*) FROM [SqlQueryNotificationService-9dd4a657-ad01-48c1-9391-a7b34f305659] WHERE message_type_name = 'http://schemas.microsoft.com/SQL/ServiceBroker/DialogTimer') > 0 BEGIN if ((SELECT COUNT(*) FROM sys.services WHERE name = 'SqlQueryNotificationService-9dd4a657-ad01-48c1-9391-a7b34f305659') > 0)   DROP SERVICE [SqlQueryNotificationService-9dd4a657-ad01-48c1-9391-a7b34f305659]; if (OBJECT_ID('SqlQueryNotificationService-9dd4a657-ad01-48c1-9391-a7b34f305659', 'SQ') IS NOT NULL)   DROP QUEUE [SqlQueryNotificationService-9dd4a657-ad01-48c1-9391-a7b34f305659]; DROP PROCEDURE [SqlQueryNotificationStoredProcedure-9dd4a657-ad01-48c1-9391-a7b34f305659]; END COMMIT TRANSACTION; END
GO
/****** Object:  StoredProcedure [dbo].[SqlQueryNotificationStoredProcedure-a9103c61-e067-4947-95a7-1f1e7651f91c]    Script Date: 2/21/2020 8:22:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SqlQueryNotificationStoredProcedure-a9103c61-e067-4947-95a7-1f1e7651f91c] AS BEGIN BEGIN TRANSACTION; RECEIVE TOP(0) conversation_handle FROM [SqlQueryNotificationService-a9103c61-e067-4947-95a7-1f1e7651f91c]; IF (SELECT COUNT(*) FROM [SqlQueryNotificationService-a9103c61-e067-4947-95a7-1f1e7651f91c] WHERE message_type_name = 'http://schemas.microsoft.com/SQL/ServiceBroker/DialogTimer') > 0 BEGIN if ((SELECT COUNT(*) FROM sys.services WHERE name = 'SqlQueryNotificationService-a9103c61-e067-4947-95a7-1f1e7651f91c') > 0)   DROP SERVICE [SqlQueryNotificationService-a9103c61-e067-4947-95a7-1f1e7651f91c]; if (OBJECT_ID('SqlQueryNotificationService-a9103c61-e067-4947-95a7-1f1e7651f91c', 'SQ') IS NOT NULL)   DROP QUEUE [SqlQueryNotificationService-a9103c61-e067-4947-95a7-1f1e7651f91c]; DROP PROCEDURE [SqlQueryNotificationStoredProcedure-a9103c61-e067-4947-95a7-1f1e7651f91c]; END COMMIT TRANSACTION; END
GO
/****** Object:  StoredProcedure [dbo].[SqlQueryNotificationStoredProcedure-f5457087-2cf4-45f3-95d0-2a476ce14b48]    Script Date: 2/21/2020 8:22:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SqlQueryNotificationStoredProcedure-f5457087-2cf4-45f3-95d0-2a476ce14b48] AS BEGIN BEGIN TRANSACTION; RECEIVE TOP(0) conversation_handle FROM [SqlQueryNotificationService-f5457087-2cf4-45f3-95d0-2a476ce14b48]; IF (SELECT COUNT(*) FROM [SqlQueryNotificationService-f5457087-2cf4-45f3-95d0-2a476ce14b48] WHERE message_type_name = 'http://schemas.microsoft.com/SQL/ServiceBroker/DialogTimer') > 0 BEGIN if ((SELECT COUNT(*) FROM sys.services WHERE name = 'SqlQueryNotificationService-f5457087-2cf4-45f3-95d0-2a476ce14b48') > 0)   DROP SERVICE [SqlQueryNotificationService-f5457087-2cf4-45f3-95d0-2a476ce14b48]; if (OBJECT_ID('SqlQueryNotificationService-f5457087-2cf4-45f3-95d0-2a476ce14b48', 'SQ') IS NOT NULL)   DROP QUEUE [SqlQueryNotificationService-f5457087-2cf4-45f3-95d0-2a476ce14b48]; DROP PROCEDURE [SqlQueryNotificationStoredProcedure-f5457087-2cf4-45f3-95d0-2a476ce14b48]; END COMMIT TRANSACTION; END
GO
/****** Object:  StoredProcedure [dbo].[System_AddNewWorkingProcessPermission]    Script Date: 2/21/2020 8:22:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[System_AddNewWorkingProcessPermission]
	@StaffID BIGINT = 0,
	@FromDate DATETIME,
	@ToDate DATETIME
AS
BEGIN
-- GET LIST STAFF BY STAFF ID
	;WITH Temp(StaffId, ParentId, FromDate, ToDate, Type)
	AS 
	(
		SELECT S.StaffId, S.ManagerID, dbo.F_GetTime(s.StartDate, s.EndDate, @FromDate, @ToDate, 0), dbo.F_GetTime(s.StartDate, s.EndDate, @FromDate, @ToDate, 1), 1
		FROM dbo.Data_Staff_WorkingProcess S
		INNER JOIN dbo.Data_Staff_Information SS ON SS.Id = S.StaffId
		WHERE s.ManagerID = @StaffId AND S.StaffID <> S.ManagerID 
			AND (
					ISNULL(dbo.F_GetTime(s.StartDate, s.EndDate, @FromDate, @ToDate, 0), '1900-01-02') <> '1900-01-01' AND
					ISNULL(dbo.F_GetTime(s.StartDate, s.EndDate, @FromDate, @ToDate, 1), '2099-12-30') <> '2099-12-31'
				)
		UNION ALL
	    SELECT S.StaffId, @StaffId, dbo.F_GetTime(s.StartDate, s.EndDate, SA.FromDate, SA.ToDate, 0), dbo.F_GetTime(s.StartDate, s.EndDate, SA.FromDate, SA.ToDate, 1), 0
		FROM Temp SA, dbo.Data_Staff_WorkingProcess S  
		WHERE SA.StaffId = S.ManagerId AND S.StaffId <> S.ManagerId AND (
					ISNULL(dbo.F_GetTime(s.StartDate, s.EndDate, SA.FromDate, SA.ToDate, 0), '1900-01-02') <> '1900-01-01' AND
					ISNULL(dbo.F_GetTime(s.StartDate, s.EndDate, SA.FromDate, SA.ToDate, 1), '2099-12-30') <> '2099-12-31'
				)
	)
		INSERT INTO dbo.Data_Staff_ParentTest(StaffID,ParentId,Type,FromDate, Todate)
		SELECT WP.StaffID, WP.ParentId, 0, Wp.FromDate, Wp.ToDate FROM Temp WP
		LEFT JOIN dbo.Data_Staff_ParentTest SP ON ISNULL(SP.StaffId, 0) =  ISNULL(WP.StaffID, 0) 
									AND ISNULL(SP.ParentId, 0) = ISNULL(WP.ParentId, 0) 
									AND ISNULL(Sp.FromDate, '1900-01-01') = ISNULL(WP.FromDate, '1900-01-01') 
									AND ISNULL(Sp.ToDate, '1900-01-01') = ISNULL(WP.ToDate, '1900-01-01')									
		WHERE SP.StaffId IS NULL
	--INSERT INTO dbo.Data_Staff_Parent
	--SELECT 
	--	@StaffID, Name, 1, @FromDate, @ToDate, @WorkingProcessId 
	--FROM dbo.splitstring((SELECT TOP 1 ISNULL(HRIDs, '') FROM dbo.WorkingProcess WHERE StaffID = @StaffID AND WPID = @WorkingProcessId), ',') WP							
	--WHERE NOT EXISTS (SELECT TOP 1 * FROM dbo.StaffParent SP WHERE ISNULL(SP.StaffId, 0) =  ISNULL(@StaffID, 0) 
	--								AND ISNULL(SP.ParentId, 0) = CAST(ISNULL(WP.Name, '0') AS INT)
	--								AND SP.WorkingProcessId = @WorkingProcessId)
	--								AND WP.Name <> 'undefined'

	--HR
	;WITH TempHr(StaffId, ParentId, FromDate, ToDate, Type)
	AS 
	(
		SELECT S.StaffId, S.HRManagerID, dbo.F_GetTime(s.StartDate, s.EndDate, @FromDate, @ToDate, 0), dbo.F_GetTime(s.StartDate, s.EndDate, @FromDate, @ToDate, 1), 2
		FROM dbo.Data_Staff_WorkingProcess S
		INNER JOIN dbo.Data_Staff_Information SS ON SS.Id = S.StaffId
		WHERE s.HRManagerID = @StaffId AND S.StaffID <> S.HRManagerID
			AND (
					ISNULL(dbo.F_GetTime(s.StartDate, s.EndDate, @FromDate, @ToDate, 0), '1900-01-02') <> '1900-01-01' AND
					ISNULL(dbo.F_GetTime(s.StartDate, s.EndDate, @FromDate, @ToDate, 1), '2099-12-30') <> '2099-12-31'
				)
		UNION ALL
	    SELECT S.StaffId, @StaffId, dbo.F_GetTime(s.StartDate, s.EndDate, SA.FromDate, SA.ToDate, 0), dbo.F_GetTime(s.StartDate, s.EndDate, SA.FromDate, SA.ToDate, 1), 2
		FROM TempHr SA, dbo.Data_Staff_WorkingProcess S  
		WHERE SA.StaffId = S.HRManagerID AND S.StaffId <> S.HRManagerID AND (
					ISNULL(dbo.F_GetTime(s.StartDate, s.EndDate, SA.FromDate, SA.ToDate, 0), '1900-01-02') <> '1900-01-01' AND
					ISNULL(dbo.F_GetTime(s.StartDate, s.EndDate, SA.FromDate, SA.ToDate, 1), '2099-12-30') <> '2099-12-31'
				)
	)
		INSERT INTO dbo.Data_Staff_ParentTest(StaffID,ParentId,Type,FromDate, Todate)
		SELECT WP.StaffID, WP.ParentId, 1, Wp.FromDate, Wp.ToDate FROM TempHr WP
		LEFT JOIN dbo.Data_Staff_ParentTest SP ON ISNULL(SP.StaffId, 0) =  ISNULL(WP.StaffID, 0) 
									AND ISNULL(SP.ParentId, 0) = ISNULL(WP.ParentId, 0) 
									AND ISNULL(Sp.FromDate, '1900-01-01') = ISNULL(WP.FromDate, '1900-01-01') 
									AND ISNULL(Sp.ToDate, '1900-01-01') = ISNULL(WP.ToDate, '1900-01-01')									
		WHERE SP.StaffId IS NULL
END
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Thu nhập từ' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Config_PersonalIncomeTaxDetail', @level2type=N'COLUMN',@level2name=N'FromAmount'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Thu nhập đến' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Config_PersonalIncomeTaxDetail', @level2type=N'COLUMN',@level2name=N'ToAmount'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Thuế suất' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Config_PersonalIncomeTaxDetail', @level2type=N'COLUMN',@level2name=N'Tax'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Lũy kế' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Config_PersonalIncomeTaxDetail', @level2type=N'COLUMN',@level2name=N'ProgressiveAmount'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Số tiền trừ' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Config_PersonalIncomeTaxDetail', @level2type=N'COLUMN',@level2name=N'SubtractAmount'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Loại Cấp Độ:0 Hằng số, 1: lookup giá trị từ bảng khác : 2 công thức tính' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Config_Salary_Element', @level2type=N'COLUMN',@level2name=N'TypeLevel'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Loại (phòng ban hay nhân viên)' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Config_Salarytype_Mapper', @level2type=N'COLUMN',@level2name=N'TypeId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Id bản ghi đính kèm' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Data_Attachment', @level2type=N'COLUMN',@level2name=N'RecordId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'bảng kết nối đính kèm' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Data_Attachment', @level2type=N'COLUMN',@level2name=N'DataType'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Id của nhóm nhiệm vụ hoặc là nhiệm vụ chi tiết' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Data_ChecklistAssign', @level2type=N'COLUMN',@level2name=N'ChecklistDetailId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'phòng ban hoặc người được giao' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Data_ChecklistAssign', @level2type=N'COLUMN',@level2name=N'AssigneeId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Loại nhận giao (phòng ban hoặc cá nhân)' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Data_ChecklistAssign', @level2type=N'COLUMN',@level2name=N'AssigneeTypeId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Loại view' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Data_ChecklistAssign', @level2type=N'COLUMN',@level2name=N'TypeViewId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Trạng thái' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Data_ChecklistAssign', @level2type=N'COLUMN',@level2name=N'Status'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Tên cột cập nhật thông tin' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Data_ChecklistDetail', @level2type=N'COLUMN',@level2name=N'ColumnLink'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Loại nhiệm vụ (nhóm hoặc nhiệm vụ)' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Data_ChecklistDetail', @level2type=N'COLUMN',@level2name=N'ChecklistDetailTypeId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Loại check list (hộp chọn, cập nhật thông tin, nhập văn bản, nhập ngày, đính kèm tệp)' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Data_ChecklistDetail', @level2type=N'COLUMN',@level2name=N'TypeControlId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Mặc định' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Data_PipelineStep', @level2type=N'COLUMN',@level2name=N'IsDefault'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Loại Cấp Độ:0 Hằng số, 1: lookup giá trị từ bảng khác : 2 công thức tính' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Data_SalaryDetail', @level2type=N'COLUMN',@level2name=N'TypeLevel'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Loại khen thưởng kỷ luật: 0: khen thưởng, 1: kỷ luật' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Data_Staff_Bonus_Discipline', @level2type=N'COLUMN',@level2name=N'TypeId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Loại chứng nhận' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Data_Staff_Certificate', @level2type=N'COLUMN',@level2name=N'TypeId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Tên chứng nhận' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Data_Staff_Certificate', @level2type=N'COLUMN',@level2name=N'Name'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'từ ngày' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Data_Staff_Certificate', @level2type=N'COLUMN',@level2name=N'FromDate'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Đến ngày' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Data_Staff_Certificate', @level2type=N'COLUMN',@level2name=N'ToDate'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Xếp loại' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Data_Staff_Certificate', @level2type=N'COLUMN',@level2name=N'RankId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Nơi cấp' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Data_Staff_Certificate', @level2type=N'COLUMN',@level2name=N'IssuedBy'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Điểm số' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Data_Staff_Certificate', @level2type=N'COLUMN',@level2name=N'Point'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Trangj' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Data_Staff_ChecklistDetail', @level2type=N'COLUMN',@level2name=N'IsFinish'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Dân tộc' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Data_Staff_Information', @level2type=N'COLUMN',@level2name=N'NationalId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'số quyết định' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Data_Staff_WorkingProcess', @level2type=N'COLUMN',@level2name=N'DecisionNo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'trạng thái vào làm' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Data_Staff_WorkingProcess', @level2type=N'COLUMN',@level2name=N'WorkingStatus'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'văn phòng' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Data_Staff_WorkingProcess', @level2type=N'COLUMN',@level2name=N'OfficeId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'1: is submenu group,  2: is submenu' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'System_Menu', @level2type=N'COLUMN',@level2name=N'MenuGroupType'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Loai Role (1: Role; 2: user)' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'System_Permission_Condition', @level2type=N'COLUMN',@level2name=N'PermissionTypeId'
GO
