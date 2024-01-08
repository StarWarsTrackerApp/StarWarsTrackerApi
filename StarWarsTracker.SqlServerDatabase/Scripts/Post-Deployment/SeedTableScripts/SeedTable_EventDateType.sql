DROP PROCEDURE IF EXISTS [dbo].[SeedTable_EventDateType] 
GO

CREATE PROCEDURE [dbo].[SeedTable_EventDateType] AS
BEGIN

	PRINT 'Populating records in [dbo].[EventDateType]';

	IF OBJECT_ID('tempdb.dbo.#dbo_SeedEventDateType') IS NOT NULL DROP TABLE #dbo_SeedEventDateType;

	SELECT 
		[Id], [Name] 
	INTO #dbo_SeedEventDateType 
	FROM [dbo].[EventDateType] 
	WHERE 0 = 1;

	INSERT INTO #dbo_SeedEventDateType 
		( [Id], [Name] )
	SELECT 
		  [Id], [Name] 
	FROM 
	(  VALUES
		    (1, 'Definitive')
		  , (2, 'DefinitiveStart')
		  , (3, 'DefinitiveEnd')
		  , (4, 'SpeculativeStart')
		  , (5, 'SpeculativeEnd')		
	) AS v ( [Id], [Name] );

	WITH cte_data as 
		(SELECT 
			[Id], [Name] 
		FROM #dbo_SeedEventDateType)
	MERGE [dbo].[EventDateType] as t
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

	DROP TABLE #dbo_SeedEventDateType;

	PRINT 'Finished Populating records in [dbo].[EventDateType]'
END
GO

EXEC [dbo].[SeedTable_EventDateType];

DROP PROCEDURE IF EXISTS [dbo].[SeedTable_EventDateType] 
GO
