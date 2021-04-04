USE [Hrm2.0]
GO
/****** Object:  StoredProcedure [dbo].[SalaryElement_Get_GetSalaryElementBySalaryTypeId]    Script Date: 2/22/2020 1:31:48 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE[dbo].[SalaryType_Get_GetSalarytypeMapperBySalaryTypeId]
	 @Id BIGINT,
	 @DbName NVARCHAR(100)
AS
 
BEGIN
	 
	DECLARE @ExeQuery NVARCHAR(MAX), @params NVARCHAR(MAX)	   				
															 
	SET @ExeQuery = N'USE [Hrm_'+ @DbName + ']' + N'				
					SELECT 
						CSM.Id,
						CSM.TypeId,
						CSM.DataId
					FROM Config_Salarytype_Mapper CSM
					WHERE CSM.IsDeleted=0 AND CSM.SalarytypeId=@Id								
			'
		SET @params ='@Id BIGINT'
		EXEC sp_executesql @ExeQuery,@params,@Id
	
END
