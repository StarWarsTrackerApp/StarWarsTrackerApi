DROP PROCEDURE IF EXISTS [dbo].[SeedTable_LogLevel] 
GO

CREATE PROCEDURE [dbo].[SeedTable_LogLevel] AS
BEGIN

	PRINT 'Populating records in [dbo].[LogLevel]';

	IF OBJECT_ID('tempdb.dbo.#dbo_SeedLogLevel') IS NOT NULL DROP TABLE #dbo_SeedLogLevel;

	SELECT 
		[Id], [Name] 
	INTO #dbo_SeedLogLevel 
	FROM [dbo].[LogLevel] 
	WHERE 0 = 1;

	INSERT INTO #dbo_SeedLogLevel 
		( [Id], [Name] )
	SELECT 
		  [Id], [Name] 
	FROM 
	(  VALUES
		    (0, 'Trace')
		  , (1, 'Debug')
		  , (2, 'Info')
		  , (3, 'Warning')
		  , (4, 'Error')
		  , (5, 'Critical')
	) AS v ( [Id], [Name] );

	WITH cte_data as 
		(SELECT 
			[Id], [Name] 
		FROM #dbo_SeedLogLevel)
	MERGE [dbo].[LogLevel] as t
		USING cte_data as s ON t.[Id] = s.[Id]
		WHEN NOT MATCHED BY TARGET THEN
			INSERT 
				([Id], [Name])
			VALUES 
				(s.[Id], s.[Name])
		WHEN MATCHED 
			THEN UPDATE SET 
				[Name] = s.[Name]
	;

	DROP TABLE #dbo_SeedLogLevel;

	PRINT 'Finished Populating records in [dbo].[LogLevel]'
END
GO

EXEC [dbo].[SeedTable_LogLevel];

DROP PROCEDURE IF EXISTS [dbo].[SeedTable_LogLevel] 
GO
