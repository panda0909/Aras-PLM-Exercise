with [bom_new] as (
select pbom.SOURCE_ID as tr_sid,pbom.RELATED_ID as tr_rid,p.ITEM_NUMBER as parent,pr.ITEM_NUMBER as son ,pr.NAME,pr.CN_SHORT_NAME ,pbom.QUANTITY,pr.CN_MTL_SPEC_NO,1 as level from [innovator].[PART_BOM] as pbom
inner join [innovator].[part] as p on pbom.SOURCE_ID=p.id
inner join [innovator].[part] as pr on pbom.RELATED_ID=pr.id
where p.id='500C62E3C2C54830A6341384DB3A71C9'
UNION ALL
select pbom2.SOURCE_ID as tr_sid,pbom2.RELATED_ID as tr_rid,p.ITEM_NUMBER as parent,pr.ITEM_NUMBER as son,pr.NAME,pr.CN_SHORT_NAME,pbom2.QUANTITY,pr.CN_MTL_SPEC_NO,level + 1
from [innovator].[PART_BOM] as pbom2
inner join [bom_new] as bnew on bnew.tr_rid=pbom2.SOURCE_ID
inner join [innovator].part as p on pbom2.SOURCE_ID =p.id
inner join [innovator].[part] as pr on pbom2.RELATED_ID=pr.id
)
select rank() OVER (ORDER by ta.tr_sid,ta.tr_rid ) as rank,* from [bom_new] as ta
order by rank
