if (!aras['setup_query'].StartItem) {
	return;
}
var arr = aras['setup_query'].StartItem.split(':');
var type = arr[0];
var id = arr[1];
if (!type || !id) {
	return;
}
//---------------Panda Code----------------//
var tmpAras = aras;
var ActivityType_Nm = 'Activity2';
var ActivityID = id;
function getItemFromServer(amlQuery, tmpAras) {
	var tmpRes = tmpAras.soapSend("ApplyItem", amlQuery);
	if (tmpRes.getFaultCode() != 0 || (tmpRes = tmpRes.getResult().selectSingleNode("Item")) == null) {
		tmpAras.AlertError(tmpRes);
	}
	return tmpRes;
}
function getActivityItem() {
	return  getItemFromServer("<Item type='" + ActivityType_Nm + "' id='" + ActivityID + "' action='get'/>", aras);
}
function populateActivityGrid() {
    
}
//---------------Panda Code End----------------//
if(type === ActivityType_Nm){
    var acwFormNd = aras.getFormForDisplay("Activity Completion Worksheet", "by-name", false);
	if (acwFormNd && acwFormNd.node) {
		var w = parseInt(aras.getItemProperty(acwFormNd.node, "width"));
		var h = parseInt(aras.getItemProperty(acwFormNd.node, "height"));
	}
	var topWindow = aras.getMostTopWindowWithAras(window);
	topWindow.item = getActivityItem();
	//Open Activity Completion Form
	params = [window, ActivityID];
	params.aras = aras;
	params.resultHandler = populateActivityGrid;
	params.dialogHeight = h ? h +  20/*statusbar*/ + 31 /*toolbar*/ + 28 /*title bar*/ + 25 /*safe-interval between form and relationships*/ : 600;
	params.dialogWidth = w || 700;
	params.resizable = true;
	params.content = "../Solutions/Project/scripts/ActivityCompletionWorksheet/ACWDialog.html";

	var actNd = aras.getItemById("Activity2", params[1], 0);
	if (actNd) {
		var dialog = window.parent.ArasModules.Dialog.show("iframe", params);
	}
}else{
    var itm = aras.getItemById(type, id, 0);
    if (itm) {
    	aras.uiShowItemEx(itm, undefined);
    }
}
//---------------Panda Code End----------------//
