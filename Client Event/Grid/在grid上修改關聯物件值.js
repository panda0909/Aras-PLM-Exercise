var thisRel = tmpThisItem.getItemsByXPath("//Item[@id='" + relationshipID + "']").getItemByIndex(0);
thisRel.setProperty("sort_order", maxVal + 1);

grid.items_Experimental.set(relationshipID, "value", "sort_order_D", maxVal + 1);

var related=thisRel.getRelatedItem();
related.setProperty("classification","Software");
thisRel.setRelatedItem(related);
grid.items_Experimental.set(relationshipID, "value", "classification_R", "Software");
console.log(thisRel);
