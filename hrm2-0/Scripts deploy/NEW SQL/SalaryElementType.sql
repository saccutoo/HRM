USE [Hrm2.0]
GO

/****** Object:  UserDefinedTableType [dbo].[SalaryElementType]    Script Date: 2/23/2020 3:36:55 PM ******/
CREATE TYPE [dbo].[SalaryElementType] AS TABLE(
	[Id] [BIGINT] NULL,
	[SalaryTypeId] [BIGINT] NULL,
	[SalaryElementId] [BIGINT] NULL,
	[IsShowSalary] [BIT] NULL,
	[IsShowPayslip] [BIT] NULL,
	[IsEdit] [BIT] NULL,
	[OrderNo] [INT] NULL
)
GO


