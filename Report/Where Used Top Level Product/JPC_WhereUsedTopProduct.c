    inn = this.getInnovator();
    return main(this);
}
private string ConvertNumber(string qStr){
    Decimal de;
    Decimal.TryParse(qStr,System.Globalization.NumberStyles.Any,null,out de);
    return de.ToString();
}
private Item GetAllItems(string part_id)
{
    Item callframe = inn.newItem("SQL", "SQL PROCESS");
    callframe.setProperty("name", "JPC_WhereUsedTopLevelBOM");
    callframe.setProperty("PROCESS", "CALL");
    callframe.setProperty("ARG1", part_id);
    
    return callframe.apply();
}
Innovator inn;
private Item main(Item thisItem){
    Aras.Server.Security.Identity plmIdentity = Aras.Server.Security.Identity.GetByName("Innovator Admin");
    Boolean PermissionWasSet = Aras.Server.Security.Permissions.GrantIdentity(plmIdentity);
    Item result=inn.newResult("");
    try{
        string id = thisItem.getID();
        Item partItem = inn.getItemById("Part",id);
        Item report = inn.newItem("Report","get");
        report.setProperty("name","JPC Where Used Top Product");
        report = report.apply();
        XmlNode resultNode = result.dom.SelectSingleNode("//Result");
        if(report.isError()==false){
            Item part = GetAllItems(id);
            if(part.isError()==false){
                for(int i=0;i<part.getItemCount();i++){
                    Item itmPrt = part.getItemByIndex(i);
                    Item findPrt = inn.newItem("Part","get");
                    findPrt.setProperty("item_number",itmPrt.getProperty("item_number",""));
                    findPrt = findPrt.apply();
                    if(findPrt.isError()==false){
                        XmlNode sourceItemNode;
                        sourceItemNode = resultNode.OwnerDocument.ImportNode(findPrt.node, true);
                        resultNode.AppendChild(sourceItemNode);
                    }
                }
            }
        }

    }catch(Exception ex){

    }
    
    if (PermissionWasSet) Aras.Server.Security.Permissions.RevokeIdentity(plmIdentity);
    return result;