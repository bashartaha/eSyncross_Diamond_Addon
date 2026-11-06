Create or alter view ESY_RIDF_AvailableStockQuery as

SELECT
    T0.[DistNumber] AS [SRNo],
    ISNULL(T2.[ItemName], '') AS [ItemDescription],
    ISNULL(T2.[U_ESY_Category], '') AS [CategoryName],
    ISNULL(T2.[U_ESY_JewelleryType], '') AS [JewelryTypeName],
    ISNULL(T0.[U_StockPoint], '') AS [StockPointName],
    ISNULL(T3.[WhsName], '') AS [LocationName],
    ISNULL(T0.[U_TotalWeight], 0) AS [GrossWeight],
    ISNULL(T0.[U_RubyWt], 0) AS [RubyWeight],
    ISNULL(T0.[U_SaphireWt], 0) AS [SaphireWeight],
    ISNULL(T0.[U_EmraldWt], 0) AS [EmaraldWeight],
    ISNULL(T0.[U_OtherStoneWt], 0) AS [OtherStoneWeight],
    ISNULL(T0.[U_DiamondWt], 0) AS [Carat],
    ISNULL(T0.[U_TagPrice], 0) AS [TagPrice],
    T0.[InDate] AS [InwardDate],
    ISNULL(T2.[U_ESY_Design], '') AS [DesignName],
    ISNULL(T2.[U_ESY_Brand], '') AS [BrandName],
    T2.[ItemCode] AS [Alias]
FROM [OSRN] T0
INNER JOIN [OSRQ] T1
    ON T1.[ItemCode] = T0.[ItemCode]
   AND T1.[SysNumber] = T0.[SysNumber]
INNER JOIN [OITM] T2
    ON T2.[ItemCode] = T0.[ItemCode]
LEFT JOIN [OWHS] T3
    ON T3.[WhsCode] = T1.[WhsCode]
WHERE T1.[Quantity] > 0
 