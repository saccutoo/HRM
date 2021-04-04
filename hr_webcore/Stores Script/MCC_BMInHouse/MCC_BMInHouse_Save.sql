USE [ERP_HRM]
GO
/****** Object:  StoredProcedure [dbo].[Config_Allowance_Save]    Script Date: 3/28/2019 3:12:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[MCC_BMInHouse_Save]
@Id						bigint,
@CustomerId				bigint,
@BM_MCCId				bigint,
@AccountType			int,
@IsPartner				bit,
@Type					int
AS
BEGIN
DECLARE @MCC_BMInHouse_Save int
	select @MCC_BMInHouse_Save = count(*) from ERP_v2.dbo.MCC_BMInHouse where Id = @Id
	if (@MCC_BMInHouse_Save = 0)		
		begin
			insert into ERP_v2.dbo.MCC_BMInHouse (CustomerId,BM_MCCId,AccountType,IsPartner,[Type]) values(@CustomerId,@BM_MCCId,@AccountType,@IsPartner,@Type)
		end	
	else
		begin
			UPDATE  ERP_v2.dbo.MCC_BMInHouse
			   SET 
				  CustomerId=@CustomerId
				  ,BM_MCCId=@BM_MCCId
				 ,AccountType = @AccountType
				 ,IsPartner = @IsPartner					
				 ,[Type] = @Type			
				 WHERE Id = @Id
		end	
END
