Create or alter proc ESY_SP_GetNextSerialNumber  
@tag nvarchar(50)
as
begin

if exists(Select top 1 itemcode from OSRN where substring(DistNumber,1,2)  = @tag)
begin
declare @currentSerialnumber nvarchar(100);
Select @currentSerialnumber= max (DistNumber) from OSRN  where substring(DistNumber,1,2)  = @tag

set @currentSerialnumber =SUBSTRING(@currentSerialnumber,3,LEN(@currentSerialnumber)-2);
 
Select  cast ( @currentSerialnumber as int) ;

 
	 
end
else
begin

select  0

end



end
 