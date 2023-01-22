Create or alter proc ESY_SP_GetSerialDetail  
@serialNumber nvarchar(50)
as
begin

 Select
T0.Status,
T0.WhsCode [CurrentLocation],
T2.ItmsGrpNam [TaggingDefinition],
'' [Category],
'' [Design],
'' [Style],
'' [DesignGroup],
'' [SubCategory],
'' [Brand],
'' [Occation] 
from OSRI T0
LEFT JOIN OITM T1 ON T0.ItemCode = T1.ItemCode
LEFT JOIN OITB T2 On T1.ItmsGrpCod = T2.ItmsGrpCod

Where IntrSerial = @serialNumber 

end



 