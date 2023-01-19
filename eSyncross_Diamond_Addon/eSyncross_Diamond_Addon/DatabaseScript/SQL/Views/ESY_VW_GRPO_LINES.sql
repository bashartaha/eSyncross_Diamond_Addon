CREATE or alter   VIEW ESY_VW_GRPO_LINES AS
Select 
TaggingDefinition,
StockPoint,
Description,
Alias,
JewelryType,
Category,
SubCategory,
TagCurrency,
TagCurrencyExRate,
SUM(CostPrice) as CostPrice,
Avg(MarkUpPercent) MarkUpPercent,
SUM(DiamondWt) DiamondWt,
SUM(DiamondWeightSub1) DiamondWeightSub1,
SUM(DiamondWeightSub2) DiamondWeightSub2,
TaggingLine1,
TaggingLine2,
sum(GoldWeight) GoldWeight

 from ESY_GRPO

 Group By
 TaggingDefinition,
StockPoint,
Description,
Alias,
JewelryType,
Category,
SubCategory,
TagCurrency,
TagCurrencyExRate, 
TaggingLine1,
TaggingLine2