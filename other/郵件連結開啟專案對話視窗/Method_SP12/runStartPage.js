var itemID = inArgs.itemID;
var itemTypeName = inArgs.itemTypeName;
var itemVersionModificator = inArgs.versionModificator;

//---------------Panda Code----------------//
var tmpAras = aras;
var ActivityType_Nm = 'Activity2';
var ActivityID = itemID;
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

var getItemByModificator = {
	default: function(itemID, itemTypeName) {
		return aras.getItemById(itemTypeName, itemID);
	},
	released: function(itemID, itemTypeName) {
		var itemAml = aras.newIOMItem(itemTypeName, 'get');
		itemAml.setAttribute('queryType', 'Released');
		itemAml.setAttribute('queryDate', ArasModules.intl.date.toIS0Format(new Date()));
		itemAml.setProperty('config_id', itemID);
		var result = itemAml.apply();
		return result.node;
	},
	current: function(itemID, itemTypeName) {
		var itemAml = aras.newIOMItem(itemTypeName, 'get');
		itemAml.setAttribute('queryType', 'Latest');
		itemAml.setProperty('config_id', itemID);
		var result = itemAml.apply();
		return result.node;
	}
};

var gottenItem;
if (getItemByModificator[itemVersionModificator]) {
	gottenItem = getItemByModificator[itemVersionModificator](itemID, itemTypeName);
} else {
	gottenItem = getItemByModificator.default(itemID, itemTypeName);
}

if (!gottenItem) {
	return Promise.reject(aras.getResource('', 'deep_link.error_open_item'));
}

var keyedName = aras.getItemProperty(gottenItem, 'keyed_name');
if (aras.uiFindWindowEx(gottenItem.id)) {
	return Promise.resolve(aras.getResource('', 'deep_link.item_alredy_is_opened', itemTypeName + ' ' + keyedName));
}
//---------------Panda Code----------------//
if(itemTypeName === ActivityType_Nm){
    var acwFormNd = aras.getFormForDisplay("Activity Completion Worksheet", "by-name", false);
	if (acwFormNd && acwFormNd.node) {
		var w = parseInt(aras.getItemProperty(acwFormNd.node, "width"));
		var h = parseInt(aras.getItemProperty(acwFormNd.node, "height"));
	}
	var topWindow = aras.getMostTopWindowWithAras(window);
	topWindow.item = getActivityItem();
	//Open Activity Completion Form
	params = [window, itemID];
	params.aras = aras;
	params.resultHandler = populateActivityGrid;
	params.dialogHeight = h ? h +  20/*statusbar*/ + 31 /*toolbar*/ + 28 /*title bar*/ + 25 /*safe-interval between form and relationships*/ : 600;
	params.dialogWidth = w || 700;
	params.resizable = true;
	params.content = "../Solutions/Project/scripts/ActivityCompletionWorksheet/ACWDialog.html";

	var actNd = aras.getItemById("Activity2", params[1], 0);
	if (actNd) {
		var dialog = window.parent.ArasModules.Dialog.show("iframe", params);
		
		if (!dialog || !dialog.then) {
        	return Promise.reject(aras.getResource('', 'deep_link.error_open_item'));
        }
        
        return dialog.then(function() {
        	var win = aras.uiFindAndSetFocusWindowEx(gottenItem.id);
        	return aras.getResource('', 'deep_link.item_is_opened', itemTypeName + ' ' + keyedName);
        });
	}
}else{
    var result = aras.uiShowItemEx(gottenItem, 'tab view', !Boolean(window.arasTabs));

    if (!result || !result.then) {
    	return Promise.reject(aras.getResource('', 'deep_link.error_open_item'));
    }
    
    return result.then(function() {
    	var win = aras.uiFindAndSetFocusWindowEx(gottenItem.id);
    	return aras.getResource('', 'deep_link.item_is_opened', itemTypeName + ' ' + keyedName);
    });
}
//---------------Panda Code End----------------//
