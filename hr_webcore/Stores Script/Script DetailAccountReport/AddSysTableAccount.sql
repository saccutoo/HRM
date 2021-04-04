INSERT INTO Sys_Table
    (TableName,
	LinkGetData,
	DataEditUrl,
	EditUrl,
	AddUrl,
	DeleteUrl,
	ExcelUrl,
	PageSize,
	PageSizeList,
	IdTable,
	ClassTable,
	PopupWidth,
	PopupHeight,
	IsActive,
	ShowTotal,
	CustomHtml,
	[Param],
	CreateDate,
	CreateBy)
VALUES
    (N'Báo cáo chi tiết tài khoản','/DetailAccountReport/TableServerSideGetDataAccount',NULL,NULL,NULL,NULL,NULL,10,'5,10,20,50,100',NULL,NULL,'90%','70%',1,NULL,NULL,NULL,'2019-01-11 00:00:00.000',1),
    (N'Báo cáo chi tiết phí thuê','/DetailAccountReport/TableServerSideGetDataPayment',NULL,NULL,NULL,NULL,NULL,10,'5,10,20,50,100',NULL,NULL,'90%','70%',1,NULL,NULL,NULL,'2019-01-11 00:00:00.000',1),
	(N'Báo cáo chi tiết thuê/refer','/DetailAccountReport/TableServerSideGetDataPaymentRefer',NULL,NULL,NULL,NULL,NULL,10,'5,10,20,50,100',NULL,NULL,'90%','70%',1,NULL,NULL,NULL,'2019-01-11 00:00:00.000',1)
    