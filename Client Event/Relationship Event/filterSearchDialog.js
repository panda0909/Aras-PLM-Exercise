var retObj = new Object();
debugger;
var members=inArgs.itemContext.selectNodes("Relationships/Item[@type='Project Team']");
var criteria="";
for(var i=0;i<members.length;i++){
    criteria=criteria+members[i].selectSingleNode("related_id/Item[@type='Identity']").getElementsByTagName("name")[0].text+"|";
}
retObj["name"] = { filterValue:criteria, isFilterFixed:true };
return retObj;
