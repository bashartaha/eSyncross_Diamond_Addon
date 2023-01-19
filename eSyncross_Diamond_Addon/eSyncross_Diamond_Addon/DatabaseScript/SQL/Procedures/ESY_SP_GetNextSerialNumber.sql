Create or alter proc ESY_SP_GetNextSerialNumber  
@itemCode nvarchar(50)
as
begin

declare @serialnumber nvarchar(100);
Select @serialnumber= max (DistNumber) from OSRN Where itemcode = @itemCode


select SUBSTRING(@serialnumber,3,LEN(@serialnumber)-2) +1;
end



 