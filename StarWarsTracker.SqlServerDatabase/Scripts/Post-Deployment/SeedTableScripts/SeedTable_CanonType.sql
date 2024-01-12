DROP PROCEDURE IF EXISTS [dbo].[SeedTable_CanonType] 
GO

CREATE PROCEDURE [dbo].[SeedTable_CanonType] AS
BEGIN

	PRINT 'Populating records in [dbo].[CanonType]';

	IF OBJECT_ID('tempdb.dbo.#dbo_SeedCanonType') IS NOT NULL DROP TABLE #dbo_SeedCanonType;

	SELECT 
		[Id], [Name] 
	INTO #dbo_SeedCanonType 
	FROM [dbo].[CanonType] 
	WHERE 0 = 1;

	INSERT INTO #dbo_SeedCanonType 
		( [Id], [Name] )
	SELECT 
		  [Id], [Name] 
	FROM 
	(  VALUES
		    (1, 'Strictly Canon')
		  , (2, 'Strictly Legends')
		  , (3, 'Canon And Legends')
	) AS v ( [Id], [Name] );

	WITH cte_data as 
		(SELECT 
			[Id], [Name] 
		FROM #dbo_SeedCanonType)
	MERGE [dbo].[CanonType] as t
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

	DROP TABLE #dbo_SeedCanonType;

	PRINT 'Finished Populating records in [dbo].[CanonType]'
END
GO

EXEC [dbo].[SeedTable_CanonType];

DROP PROCEDURE IF EXISTS [dbo].[SeedTable_CanonType] 
GO
