Create or ALTER VIEW ESY_RIDF_CountersQuery AS

SELECT [SlpCode] AS [counter_code], [SlpName] AS [counter_name] 
            FROM [OSLP] WHERE [SlpCode] != -1 AND [Active] = 'Y'