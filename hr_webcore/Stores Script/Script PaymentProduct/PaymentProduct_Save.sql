USE [ERP_HRM]
GO
/****** Object:  StoredProcedure [dbo].[WorkingProcess_Save]    Script Date: 09/01/2019 4:02:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


--region [dbo].[usp_InsertUpdateWorkingProcess]

------------------------------------------------------------------------------------------------------------------------
-- Tác giả:     thanhbt
-- Tên thủ tục: [dbo].[usp_InsertUpdatePaymentProduct]
-- Ngày tạo:    2019-01-09
------------------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE [dbo].[PaymentProduct_Save]
			@AutoID	int=null
		   ,@CustomerID int=NULL
           ,@StaffID int=NULL
           ,@Status int=NULL
           ,@PaymentDate datetime=NULL
           ,@Amount float=NULL
           ,@ProductID int=NULL
           ,@CreatedBy int=NULL
           ,@CreatedDate datetime=NULL
           ,@OrganizationUnitID int=NULL
    
AS
 
BEGIN TRY
print (@AutoID)
    IF @AutoID>0
    begin
	
UPDATE [dbo].[PaymentProduct]
   SET [StaffID] = @StaffID
      ,[CustomerID] = @CustomerID
     ,[Amount] = @Amount
     ,[PaymentDate] = @PaymentDate
     ,[Status] = @Status
     ,[ProductID] = @ProductID
     ,[CreatedBy] = @CreatedBy
     ,[CreatedDate] = @CreatedDate
     ,[OrganizationUnitID] = @OrganizationUnitID
  
 WHERE
		AutoID = @AutoID
     print(5)
    END
	else
	begin
INSERT INTO [dbo].[PaymentProduct]
           ([StaffID]
           ,[CustomerID]
           ,[Amount]
           ,[PaymentDate]
           ,[Status]
           ,[ProductID]
           ,[CreatedBy]
           ,[CreatedDate]
           ,[OrganizationUnitID]
           )
     VALUES
           (@StaffID
           ,@CustomerID
           ,@Amount
           ,@PaymentDate
           ,@Status
           ,@ProductID
           ,@CreatedBy
           ,@CreatedDate
           ,@OrganizationUnitID
            )
		   set	 @AutoID	= scope_Identity()
		  
	end
       

END TRY
BEGIN CATCH

	DECLARE	@ErrorNum 	int,
			@ErrorMsg 	varchar(200),
			@ErrorProc 	varchar(50),
			@SessionId 	int,
			@AddlInfo 	varchar(MAX)

	SET @ErrorNum 	= error_number()
	SET @ErrorMsg 	= '[dbo].[PaymentProduct_Save]: ' + error_message()
	SET @ErrorProc 	= error_procedure()
	SET @AddlInfo 	= 'INSERT OR UPDATE FAIL: ' + GETDATE()

	EXEC utl_Insert_ErrorLog @ErrorNum, @ErrorMsg, @ErrorProc, 'PaymentProduct', 'IOU', @SessionId, @AddlInfo

END CATCH

--endregion


