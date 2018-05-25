/*
example youtube url : https://youtu.be/Cnw9_74_NB4
*/
var inn = new Innovator();

console.log("-----------start------------");

var prt = typeof(parent.document.item) == "object" ? parent.document : parent.parent;
var tmpThisItem = typeof(parent.document.thisItem) == "object" ? parent.document.thisItem : parent.thisItem;



var thisRel = tmpThisItem.getItemsByXPath("//Item[@id='" + relationshipID + "']").getItemByIndex(0);
try{
thisRel.setProperty("cn_1", "1");
thisRel.setProperty("cn_2", "1000");
}catch(e){
    console.log(e);
}
console.log(thisRel);
grid.items_Experimental.set(relationshipID, "value", "cn_1_D", "1");
grid.items_Experimental.set(relationshipID, "value", "cn_2_D", "1000");