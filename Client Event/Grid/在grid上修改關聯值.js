var prt = typeof(parent.document.item) == "object" ? parent.document : parent.parent;
var tmpThisItem = typeof(parent.document.thisItem) == "object" ? parent.document.thisItem : parent.thisItem;
var NewRels = prt.item.selectNodes("Relationships/Item[@type='" + relationshipTypeName + "' and @isTemp='1']");
if (!NewRels || NewRels.length < 2) 
    top.aras.getItemRelationshipsEx(prt.item, relationshipTypeName);

var maxVal = 0;
var rels = tmpThisItem.getRelationships(relationshipTypeName);
var count = rels.getItemCount();
for (var i = 0; i < count; i++) {
    var rel = rels.getItemByIndex(i);
    var sort_order = parseInt(rel.getProperty("sort_order"));
    if (sort_order > maxVal) {
        maxVal = sort_order;
    }
}


var thisRel = tmpThisItem.getItemsByXPath("//Item[@id='" + relationshipID + "']").getItemByIndex(0);
thisRel.setProperty("sort_order", maxVal + 1);
thisRel.setProperty("quantity", maxVal + 1);
grid.items_Experimental.set(relationshipID, "value", "sort_order_D", maxVal + 1);
grid.items_Experimental.set(relationshipID, "value", "quantity_D", 1000);
