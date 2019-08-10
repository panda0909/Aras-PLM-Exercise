

// Created:Panda 20181106
// Modified:Panda 20181106
// Modified:Panda 20181106
// Itemtype:ES_TC_WM
// Event_Type:workflow node
// Event:OnActivate
// ByMethodCall:
// Function:發信通知
// 1. 該節點的簽核人員
// 2. 該表單的通知名單人員
// 3. 所有工作任務節點被簽核的執行人員
//========================================
inn = this.getInnovator();
return main(inn);
}
public Item GetWorkFlowItem(string actId){
    //取出目前節點的workflow物件**************************
    Item wflItem = this.newItem("Workflow","get");
    wflItem.setAttribute("select","source_id,source_type");
    Item wflProc = wflItem.createRelatedItem("Workflow Process","get");
    wflProc.setAttribute("select","name");
    Item wflProcAct = wflProc.createRelationship("Workflow Process Activity","get");
    wflProcAct.setAttribute("select","related_id");
    wflProcAct.setProperty("related_id",actId);
    wflItem = wflItem.apply();
    //***************************************************
    return wflItem;
}
public List<string> GetActivityAssignMent(string actId){
    //讀出舊的簽審人員***********************************
    Item old_Assignment = this.newItem("Activity Assignment","get");
    old_Assignment.setProperty("source_id",actId);
    old_Assignment=old_Assignment.apply();
    List<string> old_Asgmt = new List<string>();
    if(old_Assignment.isError()==false){
        for(int i=0; i<old_Assignment.getItemCount();i++){
            Item tmp=old_Assignment.getItemByIndex(i);
            string old_id=tmp.getProperty("related_id");
            if(old_id!="")old_Asgmt.Add(old_id);
        }
    }
    //****************************************************
    return old_Asgmt;
}
public List<string> GetNotificationIdent(string form_type,string form_id){
    string AddAssignmentRelationship="ES_Notification_unit_TC";
    List<string> idents = new List<string>();

    Item contItem = inn.newItem(form_type,"get");
    contItem.setID(form_id);
    contItem.setAttribute("select","item_number,state");
    Item rel = contItem.createRelationship(AddAssignmentRelationship,"get");
    contItem = contItem.apply();
    if(contItem.isError()==false){
        Item affItems = contItem.getItemsByXPath("Relationships/Item/related_id/Item");

        for (int i=0; i<affItems.getItemCount(); i++)
        {
            string identId = "";
            Item itm = affItems.getItemByIndex(i);
            identId = itm.getID();
            if (identId !="" && !idents.Contains(identId) )
            {
              idents.Add(identId);
            }
        }
    }

    return idents;
}
public List<string> GetActivityAllAssignedIden(string form_id){
    List<string> idents = new List<string>();
    string aml = @"<AML><Item action='get' type='Workflow' select='source_id,related_id'>
                    <source_id>{0}</source_id>
                    <related_id>
                        <Item type='Workflow Process' action='get' select='id'>
                        <Relationships>c
                            <Item action='get' type='Workflow Process Activity' select='related_id'>
                            <related_id>
                                <Item type='Activity' action='get' select='id'>
                                <Relationships>
                                    <Item action='get' type='Activity Assignment' select='closed_by'>
                                </Item>
                                </Relationships>
                                </Item>
                            </related_id>
                            </Item>
                        </Relationships>
                        </Item>
                    </related_id>
                    </Item></AML>";
    aml = string.Format(aml,form_id);
    Item closebyIdents = inn.applyAML(aml);
    if(closebyIdents.isError()==false){
        Item parseCloseByIdent = closebyIdents.getItemsByXPath("related_id/Item/Relationships/Item/related_id/Item/Relationships/Item");
        if(parseCloseByIdent.isEmpty()==false){
            for(int i=0;i<parseCloseByIdent.getItemCount();i++){
                Item ident = parseCloseByIdent.getItemByIndex(i);
                string userId = ident.getProperty("closed_by");
                string result = GetIdentityByUserId(userId);
                if(result!=""){
                    idents.Add(result);    
                }
            }
        }
    }
    return idents;
}
public string GetIdentityByUserId(string userId){
    string result = "";
    string aml = @"<AML>
    <Item action='get' type='User' page='1' pagesize='10'>
    <id>{0}</id>
    <Relationships>
        <Item action='get' type='Alias'>
        <related_id>
            <Item type='Identity' action='get'>
            <is_alias>1</is_alias>
            </Item>
        </related_id>
        </Item>
    </Relationships>
    </Item>
    </AML>";

    aml = string.Format(aml,userId);
    Item itm = inn.applyAML(aml);

    if(itm.isError()==false){
        Item parseIdent = itm.getItemsByXPath("Relationships/Item/related_id/Item");
        if(parseIdent.isEmpty()==false){
            Item ident = parseIdent.getItemByIndex(0);
            result = ident.getProperty("id");
        }
    }

    return result;
}
public string GetVarible(string id){
    Item variable = inn.getItemById("Variable",id);
    return variable.getProperty("value","");
}
public string GetItemtypeLabel(string itemtype){
    Item itm = inn.newItem("Itemtype","get");
    itm.setAttribute("select","label_zt");
    itm.setProperty("name",itemtype);
    itm = itm.apply();
    return itm.getProperty("label","","zt");
}
Innovator inn;
string CoErrorString="";
public Item main(Innovator inn){

    string actId=this.getID();
    Item wflItem = GetWorkFlowItem(actId);
    string form_id=wflItem.getProperty("source_id","");
    string form_type=wflItem.getPropertyAttribute("source_type","name","");

    Item workFormItem = inn.getItemById(form_type,form_id);
    if(workFormItem!=null){
        //讀取簽審人員腳色
        List<string> identList =  GetActivityAssignMent(actId);
        //讀取通知人員
        List<string> ident2List = GetNotificationIdent(form_type,form_id);
        //讀取Closeby簽審人員
        List<string> ident3List = GetActivityAllAssignedIden(form_id);

        List<string> combineList = new List<string>();
        combineList.AddRange(identList);
        combineList.AddRange(ident2List);
        combineList.AddRange(ident3List);

        //移除重複
        combineList = combineList.Distinct().ToList<string>();

        //讀取通知物件
        string email_name = "GTO_ReleasedMailNotifacation";
        Item email_msg = this.newItem("EMail Message", "get");
        email_msg.setProperty("name",email_name);
        email_msg = email_msg.apply();

        //更新信件內容
        string body = @"<BODY><font size='+1'>
                        <p>通知您PLM系統中 {0} : {1} {2}  已發行</p>
                        <p><a 
                        href='{3}default.aspx?StartItem={4}:{5}'> 程式設計請登入系統，查看發行物件。</p>
                        </a>
                        </font>
                        <B>
                        {6}
                        </B>
                        </BODY>";
        string itemTypeLabel = GetItemtypeLabel(form_type);
        string item_name = workFormItem.getProperty("name","");
        string item_item_number = workFormItem.getProperty("item_number","");
        string item_id = workFormItem.getProperty("id","");
        string href = GetVarible("EA5368FFAB2642A98EF1D0FA50A2AD72");

        body = string.Format(body,itemTypeLabel,item_item_number,item_name,
                            href,form_type,item_id,
                            "找不到一些构成此電子郵件所需的信息。請與系統管理員聯絡，以確保發件人擁有權限以查看項目。"
                            );

        email_msg.setProperty("body_html",body);
        //發信
        for(int i=0;i<identList.Count();i++){
            Item idenItem = inn.getItemById("Identity",identList[i]);
          if( this.email( email_msg, idenItem ) == false ) {
            //CCO.Utilities.WriteDebug("sendEmailToActiveUsers","error in sendEmailToActiveUsers: " + identList[i]);
          }
        }
        for(int i=0;i<ident2List.Count();i++){
             Item idenItem = inn.getItemById("Identity",ident2List[i]);
          if( this.email( email_msg, idenItem) == false ) {
            //CCO.Utilities.WriteDebug("sendEmailToActiveUsers","error in sendEmailToActiveUsers: " + identList[i]);
          }
        }
    }
//return inn.newError(CoErrorString);
return this;


//return inn.newResult("Emails successfully sent...");
