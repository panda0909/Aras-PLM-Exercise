CREATE PROCEDURE JPC_WhereUsedTopLevelBOM (@PART_ID CHAR(32))
AS
BEGIN
  with [bom_new] as (
select pbom.SOURCE_ID as tr_sid,pbom.RELATED_ID as tr_rid,p.ITEM_NUMBER as parent,pr.ITEM_NUMBER as son ,pr.NAME,1 as level from [innovator].[PART_BOM] as pbom
inner join [innovator].[part] as p on pbom.SOURCE_ID=p.id
inner join [innovator].[part] as pr on pbom.RELATED_ID=pr.id
where pr.id=@PART_ID and p.IS_CURRENT = '1'

UNION ALL
select pbom2.SOURCE_ID as tr_sid,pbom2.RELATED_ID as tr_rid,p.ITEM_NUMBER as parent,pr.ITEM_NUMBER as son,pr.NAME,level + 1
from [innovator].[PART_BOM] as pbom2
inner join [bom_new] as bnew on bnew.tr_sid=pbom2.RELATED_ID
inner join [innovator].part as p on pbom2.SOURCE_ID =p.id
inner join [innovator].[part] as pr on pbom2.RELATED_ID=pr.id
where p.IS_CURRENT='1'
)

select ta.parent as item_number from [bom_new] as ta where ta.parent like 'P%'
END