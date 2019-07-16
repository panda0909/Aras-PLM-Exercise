inn = this.getInnovator();
return main(this);
}
//取出目前節點的workflow物件
private Item getWokflowItem(string activty_id){
    Item wflItem = this.newItem("Workflow","get");
    wflItem.setAttribute("select","source_id,source_type");
    Item wflProc = wflItem.createRelatedItem("Workflow Process","get");
    wflProc.setAttribute("select","name");
    Item wflProcAct = wflProc.createRelationship("Workflow Process Activity","get");
    wflProcAct.setAttribute("select","related_id");
    wflProcAct.setProperty("related_id",activty_id);
    wflItem = wflItem.apply();
    return wflItem;
}

//是否包含變更物件
private bool chkAffectItemIsChange(string source_itemtype,string form_id){
    Item DCO_Affected_Item = inn.newItem(source_itemtype+" Affected Item", "get");
    DCO_Affected_Item.setAttribute("select", "source_id,related_id");
    DCO_Affected_Item.setProperty("source_id", form_id);
    DCO_Affected_Item = DCO_Affected_Item.apply();
    
    bool is_change = false;
    for(int i=0;i<DCO_Affected_Item.getItemCount();i++){
        Item temp=DCO_Affected_Item.getItemByIndex(i);
        temp=temp.getItemsByXPath("related_id/Item");
        if(temp.getProperty("action")=="Add")
        {
            // string new_item_id=temp.getProperty("new_item_id");
            // Item ChCtrI = inn.newItem("Change Controlled Item","get");
            // ChCtrI.setAttribute("select","id,classification,itemtype,item_number");
            // ChCtrI.setProperty("id",new_item_id);
            // ChCtrI=ChCtrI.apply();
            // string itm_classfication=ChCtrI.getProperty("classification");
            // string itm_itemtype=ChCtrI.getPropertyAttribute("id","type");
            // string itm_item_number=ChCtrI.getProperty("item_number");
            
            
        }
        else if(temp.getProperty("action")=="Change")
        {
            // string[] action_itm={"affected_id"};
            // for(int j=0;j<action_itm.Count();j++){
            //     string new_item_id=temp.getProperty(action_itm[j]);
            //     Item ChCtrI = inn.newItem("Change Controlled Item","get");
            //     ChCtrI.setProperty("id",new_item_id);
            //     ChCtrI=ChCtrI.apply();
            //     string itm_classfication=ChCtrI.getProperty("classification");
            //     string itm_itemtype=ChCtrI.getPropertyAttribute("id","type");
            //     string itm_item_number=ChCtrI.getProperty("item_number");
                
            // }
            is_change = true;
        }
        else if(temp.getProperty("action")=="Delete")
        {
            // string affected_id=temp.getProperty("affected_id");
            // Item ChCtrI = inn.newItem("Change Controlled Item","get");
            // ChCtrI.setProperty("id",affected_id);
            // ChCtrI=ChCtrI.apply();
            // string itm_classfication=ChCtrI.getProperty("classification");
            // string itm_itemtype=ChCtrI.getPropertyAttribute("id","type");
            // string itm_item_number=ChCtrI.getProperty("item_number");
            
            
        }
    }
    return is_change ;
}

//讀取後續路徑
public Item GetNextPaths(string actID){
    Item path = inn.newItem("Workflow Process Path","get");
    path.setProperty("source_id",actID);
    path = path.apply();
    return path;
}
//讀取路徑物件
public Item GetPath(string pathID){
    Item path = inn.newItem("Workflow Process Path","get");
    path.setID(pathID);
    path = path.apply();
    return path;
}
//更新路徑之Default Path
public Item UpdatePath(string pathID,string is_default){
    Item path = inn.newItem("Workflow Process Path","edit");
    path.setAttribute("where","id='"+pathID+"'");
    path.setProperty("is_default",is_default);
    path = path.apply();
    return path;
}
Innovator inn;
public Item main(Item thisItem){
    string form_source_type="",form_id="";
    string activity_id = thisItem.getID();
    Item wf = getWokflowItem(activity_id);
    if(wf.isError())    return inn.newError("Workflow 讀取錯誤:"+wf.getErrorString());
   
    form_id = wf.getProperty("source_id","");
    form_source_type = wf.getPropertyAttribute("source_type","keyed_name","");
     
    if(form_source_type=="" || form_id=="")
        return inn.newError("Workflow 表單讀取錯誤:source_type='"+form_source_type+"',id='"+form_id+"'");
        
    bool hasChangeItem = chkAffectItemIsChange(form_source_type,form_id);
    
    //有更新的受影響物件
    if(hasChangeItem==true){
        Item nextPaths = GetNextPaths(thisItem.getID());
	    for(int i=0;i<nextPaths.getItemCount();i++){
	        Item path = nextPaths.getItemByIndex(i);
	        string path_id = path.getProperty("id");
	        string path_name = path.getProperty("name","");
	        Item res = inn.newItem();
	        //選擇下次路徑
	        switch(path_name){
	            case "review":
	                res = UpdatePath(path_id,"0");
	            break;
	            case "change":
	                res = UpdatePath(path_id,"1");
	            break;
	        }
	        if(res.isError()==true){
	            return inn.newError("更新路徑錯誤:"+res.ToString());
	        }
	    }
    }else{
        Item nextPaths = GetNextPaths(thisItem.getID());
	    for(int i=0;i<nextPaths.getItemCount();i++){
	        Item path = nextPaths.getItemByIndex(i);
	        string path_id = path.getProperty("id");
	        string path_name = path.getProperty("name","");
	        Item res = inn.newItem();
	        //選擇下次路徑
	        switch(path_name){
	            case "review":
	                res = UpdatePath(path_id,"1");
	            break;
	            case "change":
	                res = UpdatePath(path_id,"0");
	            break;
	        }
	        if(res.isError()==true){
	            return inn.newError("更新路徑錯誤:"+res.ToString());
	        }
	    }
    }
   
return thisItem;    

