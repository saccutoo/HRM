IF EXISTS
(	SELECT	*
	FROM	[sys].[tables]	t
	JOIN	[sys].[schemas]	s
		ON	t.[schema_id] = s.[schema_id]
	WHERE	t.[name] = N'Schedule'
		AND	t.[type] = 'U'
		AND	s.[name] = N'dbo'
)
BEGIN
	IF NOT EXISTS(SELECT 1 FROM sys.columns 
				  WHERE Name = N'HasLinkages'
				  AND Object_ID = Object_ID(N'dbo.Schedule'))
	BEGIN
		ALTER TABLE [dbo].[Schedule]
		ADD [HasLinkages] BIT NULL
	END


END