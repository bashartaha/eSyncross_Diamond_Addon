
CREATE or alter   VIEW [dbo].[EVO_VW_SnBAvailableQtyinBins] AS

  SELECT DISTINCT T4."BinCode", T0."ItemCode", T0."WhsCode" AS "WhseCode", 
    T1."DistNumber" AS "SerialBatch", T0."OnHandQty" AS "Quantity", T0."BinAbs" AS "BinId" ,T4.U_VO_Project,T4.AltSortCod
FROM OBBQ T0 
    INNER JOIN OBTN T1 ON T0."SnBMDAbs" = T1."AbsEntry" AND T0."ItemCode" = T1."ItemCode" 
    INNER JOIN ITL1 T2 ON T1."ItemCode" = T2."ItemCode" AND T1."SysNumber" = T2."SysNumber" 
    INNER JOIN OITL T3 ON T2."LogEntry" = T3."LogEntry" 
    INNER JOIN OBIN T4 ON T0."BinAbs" = T4."AbsEntry" 
WHERE T3."ManagedBy" = '10000044' AND T0."OnHandQty" > 0 
union all
SELECT DISTINCT T4."BinCode", T0."ItemCode", T0."WhsCode" AS "WhseCode", T1."DistNumber" AS "SerialBatch", 
    T0."OnHandQty" AS "Quantity", T0."BinAbs" AS "BinId" ,T4.U_VO_Project,T4.AltSortCod
FROM OSBQ T0 
    INNER JOIN OSRN T1 ON T0."SnBMDAbs" = T1."AbsEntry" AND T0."ItemCode" = T1."ItemCode" 
    INNER JOIN ITL1 T2 ON T1."ItemCode" = T2."ItemCode" AND T1."SysNumber" = T2."SysNumber" 
    INNER JOIN OITL T3 ON T2."LogEntry" = T3."LogEntry" 
    INNER JOIN OBIN T4 ON T0."BinAbs" = T4."AbsEntry" 
WHERE T3."ManagedBy" = '10000045' AND T0."OnHandQty" > 0
union all

SELECT T1."BinCode", T0."ItemCode", T0."WhsCode" AS "WhseCode", '' AS "SerialBatch", T0."OnHandQty" AS "Quantity", 
    T0."BinAbs" AS "BinId" ,T1.U_VO_Project,T1.AltSortCod
FROM OIBQ T0 
    INNER JOIN OBIN T1 ON T0."BinAbs" = T1."AbsEntry" 
    INNER JOIN OITM T2 ON T0."ItemCode" = T2."ItemCode" 
WHERE T0."OnHandQty" > 0 AND T2."ManBtchNum" = 'N' AND T2."ManSerNum" = 'N'

