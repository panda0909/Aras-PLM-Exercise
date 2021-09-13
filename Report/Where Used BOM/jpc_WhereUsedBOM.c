    inn = this.getInnovator();
    return main(this);
}
private string ConvertNumber(string qStr){
    Decimal de;
    Decimal.TryParse(qStr,System.Globalization.NumberStyles.Any,null,out de);
    return de.ToString();
}

Innovator inn;
private Item main(Item thisItem){
    //Item partItem = inn.getItemById("Part","AC1E3ABE601D494986F4615B1A211F2B");
    Item partItem = inn.getItemById("Part",thisItem.getID());
    Item report = inn.newItem("Report","get");
    report.setProperty("name","JPC Where Used Parent");
    report = report.apply();
    Item result=inn.newItem();
    if(report.isError()==false){
        string aml = @"<AML>
                        <Item action='get' type='Part BOM' select='sort_order,quantity,source_id(item_number,name),related_id,cn_attrition_rate,cn_bom_note,reference_designator'>
                            <source_id>
                            <Item type='Part' action='get' select='id,item_number,name,cn_lifecycle,cn_revision,cn_part_note,classification,release_date,cn_factory,unit'>
                            <is_current>1</is_current>
                            </Item>
                            </source_id>
                            <related_id>
                            <Item type='Part' action='get' select='id,item_number'>
                                <item_number>{0}</item_number>
                            </Item>
                            </related_id>
                        </Item>
                    </AML>";
        aml = string.Format(aml,partItem.getProperty("item_number",""));
        Item part = inn.applyAML(aml);
        if(part.isError()==false){
            result = part;
        }
        

        // aml = @"<AML>
        //         <Item action='get' type='Part BOM' select='sort_order,quantity,source_id(item_number,name),related_id,cn_attrition_rate,cn_bom_note,reference_designator'>
        //             <source_id>
        //             <Item type='Part' action='get' select='id,item_number,name,cn_lifecycle,cn_revision,cn_part_note,classification,release_date,cn_factory,unit'>
        //                 <is_current>1</is_current>
        //             </Item>
        //             </source_id>
        //             <Relationships>
        //             <Item action='get' type='BOM Substitute'>
        //                 <related_id>
        //                 <Item type='Part' action='get'>
        //                     <item_number>{0}</item_number>
        //                 </Item>
        //                 </related_id>
        //             </Item>
        //             </Relationships>
        //         </Item>
        //         </AML>";
        // aml = string.Format(aml,partItem.getProperty("item_number",""));
        // part = inn.applyAML(aml);
        // if(part.isError()==false){
        //     for(int i=0;i<part.getItemCount();i++){
        //         Item itm = part.getItemByIndex(i);
        //         result.appendItem(itm);
        //     }
        // }
    }
    return result;