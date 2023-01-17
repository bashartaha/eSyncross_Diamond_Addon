  Create or alter  proc EVO_SP_GetSalesOrderLinesForPicklist @orderEntry int
 as
 
 SELECT T0."ItemCode", T0."Dscription", T0."Quantity", T0."OpenQty", T0."InvQty", T0."OpenInvQty",
  T0."WhsCode", T0."ObjType", T0."DocEntry", T0."LineNum", T0."UomCode", T0."UomEntry", T1."IUoMEntry",
   T1."InvntryUom", T0."NumPerMsr", T2."OnHand" 
   FROM RDR1 T0 INNER JOIN OITM T1 ON T0."ItemCode" = T1."ItemCode" 
   INNER JOIN OITW T2 ON T0."ItemCode" = T2."ItemCode" AND T0."WhsCode" = T2."WhsCode" 
   WHERE T0."DocEntry" = @orderEntry AND T0."OpenQty" > 0