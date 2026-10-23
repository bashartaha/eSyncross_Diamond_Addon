CREATE OR ALTER   proc [dbo].[ESY_SP_GetSerialNumbersList]
@docNum nvarchar(50)
as
begin


SELECT 'Y' as [Select] , T0.SuppSerial AS 'Serial Number' ,T1.ItemCode AS 'Item Code', T1.Dscription as 'Item Description'
FROM   [OSRI] T0  
INNER JOIN  [PDN1] T1 ON T0.BaseEntry = T1.DocEntry and T0.BaseLinNum = T1.LineNum and T0.ItemCode = T1.ItemCode
INNER JOIN [OPDN] T2 ON T1.DocEntry = T2.DocEntry
WHERE T0.BaseType = 20  and T2.DocNum  =@docNum

End