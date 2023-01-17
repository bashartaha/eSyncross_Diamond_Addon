  CREATE or alter PROCEDURE [dbo].EVO_SP_LowQuantityLocation
(
  @DocEntry INT,
   @FilterProject nvarchar(100),
  @IncludeFreeStock nvarchar(1)
)
AS
BEGIN
   SELECT ROW_NUMBER()over(order by A1.ItemCode) rowNum,
         ROW_NUMBER()over(partition by A1.ItemCode order by A1.ItemCode) AltSortCod, 
         Items.ItemCode,
         Items.ItemName,
         A1.WhseCode,
         A1.BinCode,
		 A1.BinId,
         A1.SerialBatch,
         A1.Pick [Pick Quantity]
  FROM 
  (
    SELECT Quantities.*, 
           Lines.U_PickInvQuantity PalletQuantity,
           SUM(Quantities.Quantity) OVER (PARTITION BY Quantities.ItemCode ORDER BY Quantities.Quantity , Quantities.ItemCode, Quantities.SerialBatch, Quantities.BinId) as available,
           CASE
             WHEN Lines.U_PickInvQuantity - SUM(Quantities.Quantity) OVER (PARTITION BY Quantities.ItemCode ORDER BY Quantities.Quantity , Quantities.ItemCode, Quantities.SerialBatch, Quantities.BinId) > 0 THEN Quantities.Quantity
             ELSE Quantities.Quantity + (Lines.U_PickInvQuantity - SUM(Quantities.Quantity) OVER (PARTITION BY Quantities.ItemCode ORDER BY Quantities.Quantity , Quantities.ItemCode, Quantities.SerialBatch, Quantities.BinId))
           END AS 'Pick'
    FROM [@EVO_OPICK] Header 
    INNER JOIN [@EVO_PICK1] Lines ON Header.DocEntry = Lines.DocEntry
    INNER JOIN EVO_VW_SnBAvailableQtyinBins Quantities ON Lines.U_ItemCode = Quantities.ItemCode 
                                                      AND Header.U_WhseCode = Quantities.WhseCode
    WHERE Header.DocEntry =@DocEntry and (@FilterProject='' or Quantities.U_VO_Project=@FilterProject )
  ) A1 
  INNER JOIN OITM Items ON A1.ItemCode = Items.ItemCode
  WHERE Pick > 0;
END
