Create or alter proc ESY_SP_GetSerialDetail  
@serialNumber nvarchar(50)
as
begin

 Select
T0.Status,
T0.WhsCode [CurrentLocation],
T2.ItmsGrpNam [TaggingDefinition],
T1.U_ESY_Category [Category],
T1.U_ESY_Design [Design],
T1.U_ESY_Style [Style],
'' [DesignGroup],
T1.U_ESY_SubCategory [SubCategory],
T1.U_ESY_Brand [Brand],
T1.U_ESY_Occasion [Occation] 
from OSRI T0
LEFT JOIN OITM T1 ON T0.ItemCode = T1.ItemCode
LEFT JOIN OITB T2 On T1.ItmsGrpCod = T2.ItmsGrpCod

Where IntrSerial = @serialNumber 

end


