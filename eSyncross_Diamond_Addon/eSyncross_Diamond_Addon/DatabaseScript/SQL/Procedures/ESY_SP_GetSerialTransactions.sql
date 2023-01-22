Create or alter proc ESY_SP_GetSerialTransactions  
@serialNumber nvarchar(50)
as
begin

 Select 
 
 case 
 when T4.DocType = 20 then 'Goods Receipt PO'
 when T4.DocType = 21 then 'Purchase Return'
 when T4.DocType = 15 then 'Sales Delivery'
 when T4.DocType = 16 then 'Sales Return'
 when T4.DocType = 13 then 'Sales Invoice'
 when T4.DocType = 14 then 'Sales Credit Memo'
 when T4.DocType = 18 then 'Purchase Invoice'
 when T4.DocType = 19 then 'Purchase Credit Memo'
 else  ''
 end

 as [Document Type]
 , T4.DocDate [Document Date],T4.DocNum [Document Number],T4.CardCode [BP Code],T4.CardName [BP Name] 
 
 FROM  [dbo].[OSRN] T0   INNER  JOIN [dbo].[ITL1] T3  ON  T3.[ItemCode] = T0.[ItemCode]  AND  T3.[SysNumber] = T0.[SysNumber]   INNER  JOIN [dbo].[OITL] T4  ON  T4.[LogEntry] = T3.[LogEntry]  AND  T4.[ManagedBy] = 10000045  
 WHERE   T0.DistNumber =@serialNumber

end



 