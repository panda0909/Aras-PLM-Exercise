/*取得所有的Aras BOM*/
var GetPartBOMByAll=function(part_id)
{
    var inn = new Innovator();
    try
    {
        var aml = "<AML><Item type='Part' select='item_number,cn_tag' action='GetItemRepeatConfig' id='"+part_id+"'><Relationships><Item type='Part BOM' select='related_id,quantity' repeatProp='related_id' repeatTimes='10'/></Relationships></Item></AML>";
        var result_string = "";
        var result = inn.applyAML(aml);
        var related_BOM = result.getItemsByXPath("Relationships//Item");
        if (related_BOM.isError() == false)
        {
            for (var i = 0; i < related_BOM.getItemCount(); i++)
            {
                var related_bom_itm = related_BOM.getItemByIndex(i);
                var related_itm = related_bom_itm.getRelatedItem();
                
                if (related_itm != null) {
                    if (related_itm.getProperty("cn_tag", "") == 0) {
                        var new_bom = inn.newItem("w bom", "add");
                        new_bom.setProperty("cn_materialu", related_bom_itm.getProperty("quantity", ""));
                        new_bom.setProperty("cn_material", related_itm.getProperty("id", ""));
                        result_string += new_bom.ToString();
                    }
                    
                    //result_string += Recursive(related_itm);
                }
                
            }
            console.log(result_string);
            return result_string;
        }
        return "";
    }catch( ex)
    {
       
    }
}
var Recursive = function (parent) {
    var inn = NanyaJSAras.Innovator;
    var result = "";
    try
    {
        var related_BOM = parent.getItemsByXPath("Relationships//Item");
        if (related_BOM.isError() == false)
        {
            for (var i = 0; i < related_BOM.getItemCount(); i++)
            {
                var related_bom_itm = related_BOM.getItemByIndex(i);
                var related_itm = related_bom_itm.getRelatedItem();
                if (related_itm != null) {
                    
                    var new_bom = inn.newItem("w bom", "add");
                    new_bom.setProperty("cn_materialu", related_bom_itm.getProperty("quantity", ""));
                    new_bom.setProperty("cn_material", related_itm.getProperty("id", ""));
                    result += new_bom.ToString();
                }
               
                //result += Recursive(related_itm);
            }
        }
        return result;
    }
    catch ( ex)
    {
        return "";
    }
}
