CREATE or alter   PROCEDURE EVO_SP_GetAvailableQuantityInBins ( 
    @WhseCode VARCHAR(8),
    @ItemCode VARCHAR(20),
    @isGroupedBySerial BIT,
    @Project VARCHAR(100)
) 
AS
BEGIN
    -- Create temporary table
   CREATE TABLE #tempTable (
        BinCode VARCHAR(50),
        ItemCode VARCHAR(100),
        WhseCode VARCHAR(8),
        SerialBatch VARCHAR(50),
        Quantity FLOAT,
        BinId INT,
		Project VARCHAR(100),
    );


    -- Insert data into temporary table
    INSERT INTO #tempTable (BinCode, ItemCode, WhseCode, SerialBatch, Quantity, BinId,Project)
   SELECT DISTINCT T4."BinCode", T0."ItemCode", T0."WhsCode" AS "WhseCode", 
    T1."DistNumber" AS "SerialBatch", T0."OnHandQty" AS "Quantity", T0."BinAbs" AS "BinId" ,T4.U_VO_Project
FROM OBBQ T0 
    INNER JOIN OBTN T1 ON T0."SnBMDAbs" = T1."AbsEntry" AND T0."ItemCode" = T1."ItemCode" 
    INNER JOIN ITL1 T2 ON T1."ItemCode" = T2."ItemCode" AND T1."SysNumber" = T2."SysNumber" 
    INNER JOIN OITL T3 ON T2."LogEntry" = T3."LogEntry" 
    INNER JOIN OBIN T4 ON T0."BinAbs" = T4."AbsEntry" 
WHERE T3."ManagedBy" = '10000044' AND T0."OnHandQty" > 0  AND T0."WhsCode" = @WhseCode AND T0."ItemCode" = @ItemCode
and (@Project = '' or T4.U_VO_Project = @Project)
UNION ALL 
SELECT DISTINCT T4."BinCode", T0."ItemCode", T0."WhsCode" AS "WhseCode", T1."DistNumber" AS "SerialBatch", 
    T0."OnHandQty" AS "Quantity", T0."BinAbs" AS "BinId" ,T4.U_VO_Project
FROM OSBQ T0 
    INNER JOIN OSRN T1 ON T0."SnBMDAbs" = T1."AbsEntry" AND T0."ItemCode" = T1."ItemCode" 
    INNER JOIN ITL1 T2 ON T1."ItemCode" = T2."ItemCode" AND T1."SysNumber" = T2."SysNumber" 
    INNER JOIN OITL T3 ON T2."LogEntry" = T3."LogEntry" 
    INNER JOIN OBIN T4 ON T0."BinAbs" = T4."AbsEntry" 
WHERE T3."ManagedBy" = '10000045' AND T0."OnHandQty" > 0  AND T0."WhsCode" = @WhseCode AND T0."ItemCode" = @ItemCode
and (@Project = '' or T4.U_VO_Project = @Project)
UNION ALL 
SELECT T1."BinCode", T0."ItemCode", T0."WhsCode" AS "WhseCode", '' AS "SerialBatch", T0."OnHandQty" AS "Quantity", 
    T0."BinAbs" AS "BinId" ,T1.U_VO_Project
FROM OIBQ T0 
    INNER JOIN OBIN T1 ON T0."BinAbs" = T1."AbsEntry" 
    INNER JOIN OITM T2 ON T0."ItemCode" = T2."ItemCode" 
WHERE T0."OnHandQty" > 0 AND T2."ManBtchNum" = 'N' AND T2."ManSerNum" = 'N'  AND T0."WhsCode" = @WhseCode AND T0."ItemCode" = @ItemCode
and (@Project = '' or T1.U_VO_Project = @Project)

 
 if(@isGroupedBySerial =1)
 begin
 Select BinCode [Bin Location],SerialBatch [Serial / Batch],Quantity Quantity from #tempTable 
 end
 else
 begin
 Select BinCode [Bin Location],Sum(Quantity) Quantity from #tempTable Group by BinCode
 end

END;
