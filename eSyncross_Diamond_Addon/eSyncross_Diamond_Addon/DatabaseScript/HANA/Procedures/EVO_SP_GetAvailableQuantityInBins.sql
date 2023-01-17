CREATE PROCEDURE "EVO_SP_GetAvailableQuantityInBins" (IN WhseCode varchar(8), IN ItemCode varchar(20), IN 
isGroupedBySerial tinyint, IN Project varchar(100)) 
AS 

BEGIN
CREATE LOCAL TEMPORARY COLUMN TABLE "#tempTable" ("BinCode" varchar(50), "ItemCode" varchar(100), "WhseCode" varchar(8), 
        "SerialBatch" varchar(50), "Quantity" float, "BinId" integer, 
    "Project" varchar(100));
    
    
    -- Insert data into temporary table
    INSERT INTO "#tempTable" ("BinCode", "ItemCode", "WhseCode", "SerialBatch", "Quantity", "BinId", "Project") (SELECT
         DISTINCT T4."BinCode", T0."ItemCode", T0."WhsCode" AS "WhseCode", T1."DistNumber" AS "SerialBatch", 
        T0."OnHandQty" AS "Quantity", T0."BinAbs" AS "BinId", T4."U_VO_Project" 
    FROM OBBQ T0 
        INNER JOIN OBTN T1 ON T0."SnBMDAbs" = T1."AbsEntry" AND T0."ItemCode" = T1."ItemCode" 
        INNER JOIN ITL1 T2 ON T1."ItemCode" = T2."ItemCode" AND T1."SysNumber" = T2."SysNumber" 
        INNER JOIN OITL T3 ON T2."LogEntry" = T3."LogEntry" 
        INNER JOIN OBIN T4 ON T0."BinAbs" = T4."AbsEntry" 
    WHERE T3."ManagedBy" = '10000044' AND T0."OnHandQty" > 0 AND T0."WhsCode" = :WhseCode AND
         T0."ItemCode" = :ItemCode AND (:Project = '' OR T4."U_VO_Project" = :Project) 
    UNION ALL 
    SELECT DISTINCT T4."BinCode", T0."ItemCode", T0."WhsCode" AS "WhseCode", T1."DistNumber" AS "SerialBatch", 
        T0."OnHandQty" AS "Quantity", T0."BinAbs" AS "BinId", T4."U_VO_Project" 
    FROM OSBQ T0 
        INNER JOIN OSRN T1 ON T0."SnBMDAbs" = T1."AbsEntry" AND T0."ItemCode" = T1."ItemCode" 
        INNER JOIN ITL1 T2 ON T1."ItemCode" = T2."ItemCode" AND T1."SysNumber" = T2."SysNumber" 
        INNER JOIN OITL T3 ON T2."LogEntry" = T3."LogEntry" 
        INNER JOIN OBIN T4 ON T0."BinAbs" = T4."AbsEntry" 
    WHERE T3."ManagedBy" = '10000045' AND T0."OnHandQty" > 0 AND T0."WhsCode" = :WhseCode AND
         T0."ItemCode" = :ItemCode AND (:Project = '' OR T4."U_VO_Project" = :Project) 
    UNION ALL 
    SELECT T1."BinCode", T0."ItemCode", T0."WhsCode" AS "WhseCode", '' AS "SerialBatch", 
        T0."OnHandQty" AS "Quantity", T0."BinAbs" AS "BinId", T1."U_VO_Project" 
    FROM OIBQ T0 
        INNER JOIN OBIN T1 ON T0."BinAbs" = T1."AbsEntry" 
        INNER JOIN OITM T2 ON T0."ItemCode" = T2."ItemCode" 
    WHERE T0."OnHandQty" > 0 AND T2."ManBtchNum" = 'N' AND T2."ManSerNum" = 'N' AND T0."WhsCode" = :WhseCode AND
         T0."ItemCode" = :ItemCode AND (:Project = '' OR T1."U_VO_Project" = :Project));
    IF (:isGroupedBySerial = 1) THEN 
        SELECT "BinCode" as "Bin Location", "SerialBatch" as "Serial / Batch", "Quantity" AS "Quantity" 
        FROM "#tempTable";
    ELSE 
        SELECT "BinCode" as "Bin Location", SUM("Quantity") AS "Quantity" 
        FROM "#tempTable" 
        GROUP BY "BinCode";
    END IF;
END;
-- Create temporary table