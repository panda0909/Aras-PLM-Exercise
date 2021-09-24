inn = this.getInnovator();
Item res = main(this);
return res;
}
private void Log(string msg)
{
    Item log = inn.newItem("JPC_Method_Log", "add");
    log.setProperty("jpc_run_method", "JPC_WF_AssignmentMetrix");
    log.setProperty("jpc_method_event", "workflow");
    log.setProperty("jpc_log", msg);
    log = log.apply();
}
private Item GetWF(string actId){
    //取出目前節點的workflow物件**************************
    Item wflItem = this.newItem("Workflow","get");
    wflItem.setAttribute("select","source_id,source_type,related_id");
    Item wflProc = wflItem.createRelatedItem("Workflow Process","get");
    wflProc.setAttribute("select","name,copied_from_string");
    Item wflProcAct = wflProc.createRelationship("Workflow Process Activity","get");
    wflProcAct.setAttribute("select","related_id");
    wflProcAct.setProperty("related_id",actId);
    wflItem = wflItem.apply();
    //***************************************************
    return wflItem;
}
private Item GetFormByWF(Item wflItem){
    string form_id = wflItem.getProperty("source_id");
    string source_type =  wflItem.getPropertyAttribute("source_type","name","");
    Item formItm = inn.newItem(source_type,"get");
    formItm.setProperty("id",form_id);
    formItm = formItm.apply();
    return formItm;
}
private Item GetWFActSetting(string wf_id,string act_name){
    string aml=@"<AML>
            <Item action='get' type='Assignment Activity'>
                <itm_workflow_map>
                <Item type='Workflow Map' action='get'>
                    <id>{0}</id>
                </Item>
                </itm_workflow_map>
                <itm_workflow_map_activity>
                <Item type='Workflow Map Activity' action='get'>
                    <related_id>
                    <Item type='Activity Template' action='get'>
                        <name>{1}</name>
                    </Item>
                    </related_id>
                </Item>
                </itm_workflow_map_activity>
            </Item>
            </AML>";
    aml = string.Format(aml,wf_id,act_name);
    Item res = inn.applyAML(aml);
    return res;
}
private Item GetAssignmentSetting(string assign_act_id){
    string aml = @"<AML>
                <Item action='get' type='Assignment'>
                <source_id>{0}</source_id>
                </Item>
                </AML>";
    aml = string.Format(aml,assign_act_id);
    Item itm = inn.applyAML(aml);
    return itm;
}
private Item GetChangePathSetting(string assign_act_id){
    string aml = @"<AML>
                <Item action='get' type='JPC_ChangePath' orderBy='sort_order'>
                <source_id>{0}</source_id>
                </Item>
                </AML>";
    aml = string.Format(aml,assign_act_id);
    Item itm = inn.applyAML(aml);
    return itm;
}
private Item GetCheckFormSetting(string assign_act_id){
    string aml = @"<AML>
                <Item action='get' type='JPC_CheckForm'>
                <source_id>{0}</source_id>
                </Item>
                </AML>";
    aml = string.Format(aml,assign_act_id);
    Item itm = inn.applyAML(aml);
    return itm;
}
private Item GetAddIdentityByField(Item formItm,string prop1){
    string prop1_id = formItm.getProperty(prop1,"");
    string prop1_type = formItm.getPropertyAttribute(prop1,"type","");
    Item prop1_item = inn.newItem(prop1_type,"get");
         prop1_item.setProperty("id",prop1_id);
         prop1_item = prop1_item.apply();
    if(prop1_item.isError()){
        return null;
    }else{
        return prop1_item;
    }
}
private Item GetAddIdentityByItemField(Item formItm,string prop1,string prop2){
    string prop1_id = formItm.getProperty(prop1,"");
    string prop1_type = formItm.getPropertyAttribute(prop1,"type","");
    Item prop1_item = inn.newItem(prop1_type,"get");
         prop1_item.setProperty("id",prop1_id);
         prop1_item = prop1_item.apply();
    if(prop1_item.isError()){
        return inn.newError(prop1+":空值");
    }else{
        Item prop2_item = inn.newItem("Identity","get");
        prop2_item.setProperty("id",prop1_item.getProperty(prop2,""));
        prop2_item = prop2_item.apply();
        if(prop2_item==null){
            return null;
        }else if(prop2_item.isError()==true){
            return null;
        }
        else{
            return prop2_item;
        }
    }
}
private bool CheckPropValue(Item formItm,string field_name,string condition,string field_value){
    string form_field_value = formItm.getProperty(field_name,"");
    double num = 0.0,field_value_D = 0.1;
    double number = 0.0;
    if(Double.TryParse(form_field_value,out number)==true){
        num = double.Parse(form_field_value);
    }
    if(double.TryParse(field_value,out number)==true){
        field_value_D = double.Parse(field_value);
    }
    switch(condition){
        case ">":
            if(num > field_value_D) return true;
        break;
        case "<":
            if(num < field_value_D) return true;
        break;
        case "<>":
            if(num != field_value_D) return true;
        break;
        case ">=":
            if(num >= field_value_D) return true;
        break;
        case "<=":
            if(num <= field_value_D) return true;
        break;
        case "=":
            if(num == field_value_D || form_field_value==field_value) return true;
        break;
        case "like":
            if(form_field_value.Contains(field_value)==true) return true;
        break;
        case "is not null":
            if(form_field_value != "") return true;
        break;
    }

    return false;
}

private string CheckPropValueFormat(Item formItm,string field_name,string condition,string field_value){
    string form_field_value = formItm.getProperty(field_name,"");
    
    switch(condition){
        case ">":
            return "'" + form_field_value + "' > '" + field_value + "'";
        case "<":
            return "'" + form_field_value + "' < '" + field_value + "'";
        case "<>":
           return "'" + form_field_value + "' <> '" + field_value + "'";
        case ">=":
            return "'" + form_field_value + "' >= '" + field_value + "'";
        case "<=":
            return "'" + form_field_value + "' <= '" + field_value + "'";
        case "=":
            return "'" + form_field_value + "' = '" + field_value + "'";
        case "like":
            return "CHARINDEX('"+field_value+"','"+form_field_value+"') > 0";
        case "is not null":
            return "Len('" + form_field_value + "') > 0";
    }
    return "";
}
private bool CheckAddedAssignment(string actId,string identId){
    Item newActAssignment = inn.newItem("Activity Assignment","get");
        newActAssignment.setProperty("source_id",actId);
        newActAssignment.setProperty("related_id",identId);
        newActAssignment = newActAssignment.apply();
        if(newActAssignment.isError()==true){
            //找不到
            return false;
        }else{
            //找到
            return true;
        }
}
private bool CheckMoreCriteria(Item formItm,string assignSetID){
    bool result = true;
    Item res = inn.newItem("Assignment_Criteria","get");
    res.setProperty("source_id",assignSetID);
    res = res.apply();
    if(res.isError()==false){
        for(int i=0;i<res.getItemCount();i++){
            Item itm = res.getItemByIndex(i);
            string jpc_criteria = itm.getProperty("jpc_criteria","");
            string jpc_condition = itm.getProperty("jpc_condition","");
            string jpc_cond_value = itm.getProperty("jpc_cond_value","");
            
            bool check = CheckPropValue(formItm,jpc_criteria,jpc_condition,jpc_cond_value);
            if(check==false){
                result = false;
            }
        }
    }else{
        //沒有設定條件
        return true;
    }

    return result;
}
private bool CheckMoreCriteriaPath(Item formItm,string assignSetID){
    bool result = true;
    Item res = inn.newItem("JPC_ChangePath_MoreCriteria","get");
    res.setProperty("source_id",assignSetID);
    res = res.apply();
    if(res.isError()==false){
        for(int i=0;i<res.getItemCount();i++){
            Item itm = res.getItemByIndex(i);
            string jpc_criteria = itm.getProperty("jpc_criteria","");
            string jpc_condition = itm.getProperty("jpc_condition","");
            string jpc_cond_value = itm.getProperty("jpc_cond_value","");
            
            bool check = CheckPropValue(formItm,jpc_criteria,jpc_condition,jpc_cond_value);
            if(check==false){
                result = false;
            }
        }
    }else{
        //沒有設定條件
        return true;
    }

    return result;
}
private bool CheckMoreCriteriaCheckForm(Item formItm,string assignSetID){
    bool result = true;
    Item res = inn.newItem("JPC_CheckForm_MoreCriteria","get");
    res.setProperty("source_id",assignSetID);
    res = res.apply();
    if(res.isError()==false){
        string format_str = "";
        for(int i=0;i<res.getItemCount();i++){
            Item itm = res.getItemByIndex(i);
            string jpc_criteria = itm.getProperty("jpc_criteria","");
            string jpc_condition = itm.getProperty("jpc_condition","");
            string jpc_cond_value = itm.getProperty("jpc_cond_value","");
            string jpc_and_or_condition = itm.getProperty("jpc_and_or_condition","");
            string jpc_left_reg =  itm.getProperty("jpc_left_reg","");
            string jpc_right_reg =  itm.getProperty("jpc_right_reg","");
            bool check = CheckPropValue(formItm,jpc_criteria,jpc_condition,jpc_cond_value);
            format_str += (jpc_left_reg + CheckPropValueFormat(formItm,jpc_criteria,jpc_condition,jpc_cond_value) + " "  + jpc_right_reg + " " + jpc_and_or_condition + " ").Replace("&", "&amp;").Replace("<", "&lt;").Replace(">", "&gt;");
            
        }
        string sql = @"select top 1 (CASE WHEN (    
                        {0} )   THEN 'T' ELSE 'F' END) as r
                        from innovator.[itemtype]";
        sql = string.Format(sql,format_str);
        Item sqlResult = inn.applySQL(sql);
        if(sqlResult.isError()==false){
            if(sqlResult.getProperty("r","")=="T"){
                result = true;
            }else{
                result = false;
            }
        }
        return result;
    }else{
        //沒有設定條件
        return true;
    }
    return result;
}
public bool CheckPropValueFormatSQL(Item formItm,string field_name,string condition,string field_value)
{
    bool result = true;
    string format_str = "";
    string jpc_left_reg =  "(";
    string jpc_right_reg =  ")";
    
    format_str += (jpc_left_reg + CheckPropValueFormat(formItm,field_name,condition,field_value) + " "  + jpc_right_reg + " ").Replace("&", "&amp;").Replace("<", "&lt;");

    string sql = @"select top 1 (CASE WHEN (    
                    {0} )   THEN 'T' ELSE 'F' END) as r
                    from innovator.[itemtype]";
    sql = string.Format(sql,format_str);
    
        Item sqlResult = inn.applySQL(sql);
        if(sqlResult.isError()==false){
            if(sqlResult.getProperty("r","")=="T"){
                result = true;
            }else{
                result = false;
            }
        }
    
    return result;
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
string error_msg="";
private Item main(Item thisItem){
    Item wflItem = GetWF(thisItem.getID());
    Item formItm = GetFormByWF(wflItem);
    Item wflProcessItem = wflItem.getRelatedItem();
    string wfCopyFromStringId = wflProcessItem.getProperty("copied_from_string","");
    //return inn.newError(wfCopyFromStringId);
    string act_name = thisItem.getProperty("name","");
    Item itmWF = inn.newItem("Workflow Map","get");
        itmWF.setProperty("id",wfCopyFromStringId);
        itmWF = itmWF.apply();
        if(itmWF.isError()==false){
            string config_id = itmWF.getProperty("config_id","");
            itmWF = inn.newItem("Workflow Map","get");
            itmWF.setProperty("config_id",config_id);
            itmWF = itmWF.apply();
            wfCopyFromStringId = itmWF.getID();
        }
    Item AssignmentActSetting = GetWFActSetting(wfCopyFromStringId,act_name);
    //return inn.newError(AssignmentActSetting.ToString());
    if(AssignmentActSetting.isError()==false){
        Item AssignAct = AssignmentActSetting.getItemByIndex(0);
        //-----------增加簽核人員-----------//
        Item AssignSettingItms = GetAssignmentSetting(AssignAct.getID());
        
        for(int i=0;i<AssignSettingItms.getItemCount();i++){
            Item assign_set = AssignSettingItms.getItemByIndex(i);
            string assignment_type = assign_set.getProperty("assignment_type","");
            string parameter1 = assign_set.getProperty("parameter","");
            string parameter2 = assign_set.getProperty("parameter2","");
            string jpc_method = assign_set.getPropertyAttribute("jpc_method","keyed_name","");
            string jpc_condition_property = assign_set.getProperty("jpc_condition_property","");
            string jpc_cretiria = assign_set.getProperty("jpc_cretiria","");
            string jpc_condition_value = assign_set.getProperty("jpc_condition_value","");
            string is_required = assign_set.getProperty("is_required","");
            string for_all_members = assign_set.getProperty("for_all_members","");
            string voting_weight = assign_set.getProperty("voting_weight","");
            //return inn.newError(jpc_method);
            Item newIdentity = inn.newItem();
            string body = "<form_id>{0}</form_id><form_type>{1}</form_type><p1>{2}</p1><p2>{3}</p2>";
            
            switch(assignment_type){
                case "Identity":
                    //固定的使用者
                    newIdentity = inn.newItem("Identity","get");
                    newIdentity.setProperty("id",assign_set.getProperty("assignee",""));
                    newIdentity = newIdentity.apply();
                break;
                case "Field":
                    //變數1=Identity欄位
                    newIdentity = GetAddIdentityByField(formItm,parameter1);
                    
                break;
                case "Item Field":
                    //變數1=Identity欄位,變數2=變數1欄位的Identity欄位
                    newIdentity = GetAddIdentityByItemField(formItm,parameter1,parameter2);
                    if(newIdentity==null){
                        body = string.Format(body,formItm.getID(),formItm.getAttribute("type",""),parameter1,parameter2);
                        if(jpc_method!="")
                        {
                            newIdentity = inn.applyMethod(jpc_method,body);
                        }
                    }else if(newIdentity.isError()==true)
                    {
                        
                        body = string.Format(body,formItm.getID(),formItm.getAttribute("type",""),parameter1,parameter2);
                        if(jpc_method!=""){
                            newIdentity = inn.applyMethod(jpc_method,body);
                        }
                    }
                break;
            }
            
            //---------  新增簽核人員----------//
            if(newIdentity==null) continue;
            
            try{
                if(jpc_condition_property!="" && jpc_cretiria!="" && jpc_condition_value!="")
                {
                    bool check = false;
                    try{
                       check = CheckPropValue(formItm,jpc_condition_property,jpc_cretiria,jpc_condition_value);
                    }catch(Exception ex){
                        return inn.newError("Check:"+ex.ToString());
                    }
                    
                    if(check==true)
                    {
                        bool checkMoreCond = false;
                        try{
                            checkMoreCond = CheckMoreCriteria(formItm,assign_set.getID());
                            
                        }catch(Exception ex){
                            return inn.newError("Check2:"+ex.ToString());
                        }
                        if(checkMoreCond==false){
                            //判斷更多條件，False則不繼續新增下去
                            continue;
                        }
                        try{
                            if(newIdentity.isError()==false && newIdentity.getResult().ToString().Trim()!=""){
                                if(CheckAddedAssignment(thisItem.getID(),newIdentity.getID())==false)
                                {
                                    Item newActAssignment = inn.newItem("Activity Assignment","add");
                                    newActAssignment.setProperty("source_id",thisItem.getID());
                                    newActAssignment.setProperty("related_id",newIdentity.getID());
                                    newActAssignment.setProperty("is_required",is_required);
                                    newActAssignment.setProperty("for_all_members",for_all_members);
                                    newActAssignment.setProperty("voting_weight",voting_weight);
                                    newActAssignment = newActAssignment.apply();
                                    //return inn.newError(check.ToString());
                                }
                            }
                        }catch(Exception ex){
                            return inn.newError("error2:"+newIdentity.ToString());
                        }
                    }
                }else{
                    if(newIdentity.isError()==false && newIdentity.ToString().Trim()!=""){
                        if(CheckAddedAssignment(thisItem.getID(),newIdentity.getID())==false)
                        {
                            if(newIdentity.isError()==false){
                                Item newActAssignment = inn.newItem("Activity Assignment","add");
                                newActAssignment.setProperty("source_id",thisItem.getID());
                                newActAssignment.setProperty("related_id",newIdentity.getID());
                                newActAssignment.setProperty("is_required",is_required);
                                newActAssignment.setProperty("for_all_members",for_all_members);
                                newActAssignment.setProperty("voting_weight",voting_weight);
                                newActAssignment = newActAssignment.apply();
                            }
                        }
                    }
                }
            }catch(Exception ex){
                return inn.newError("新增簽核人員錯誤:"+error_msg);
            }
            
        }
        //---------------------------------//
        //-----------選擇路徑--------------//
        Item ChangePathItems = GetChangePathSetting(AssignAct.getID());
        //return inn.newError(ChangePathItems.ToString());
        if(ChangePathItems.isError()==false){
            //return inn.newError(ChangePathItems.getItemCount().ToString());
            for(int i=0;i<ChangePathItems.getItemCount();i++){
                Item checkItm = ChangePathItems.getItemByIndex(i);
                string path_name = checkItm.getProperty("jpc_path_name","");
                string jpc_criteria = checkItm.getProperty("jpc_criteria","");
                string jpc_condition = checkItm.getProperty("jpc_condition","");
                string jpc_cond_value = checkItm.getProperty("jpc_cond_value","");
                //return inn.newError(path_name);
                bool check = CheckPropValueFormatSQL(formItm,jpc_criteria,jpc_condition,jpc_cond_value);
                
                //return inn.newError(check.ToString()+","+jpc_cond_value);
                if(check==true)
                {
                    //return inn.newError(path_name);
                    bool check_more = CheckMoreCriteriaPath(formItm,checkItm.getID());
                    //return inn.newError(check_more.ToString());
                    if(check_more){
                        Item nextPaths = GetNextPaths(thisItem.getID());
                        for(int p=0;p<nextPaths.getItemCount();p++){
                            Item nPath = nextPaths.getItemByIndex(p);
                            if(nPath.getProperty("name","")==path_name){
                                UpdatePath(nPath.getID(),"1");
                            }else{
                                UpdatePath(nPath.getID(),"0");
                            }
                        }
                        break;
                    }
                }
            }
        }
        //---------------------------------//
        //-----------檢查表單欄位--------------//
        Item CheckFormItems = GetCheckFormSetting(AssignAct.getID());
        if(CheckFormItems.isError()==false){
            //return inn.newError(ChangePathItems.getItemCount().ToString());
            for(int i=0;i<CheckFormItems.getItemCount();i++){
                Item checkItm = CheckFormItems.getItemByIndex(i);
                string jpc_criteria = checkItm.getProperty("jpc_criteria","");
                string jpc_condition = checkItm.getProperty("jpc_condition","");
                string jpc_cond_value = checkItm.getProperty("jpc_cond_value","");
                string jpc_relationship_name = checkItm.getProperty("jpc_relationship_name","");
                string jpc_replace_method = checkItm.getProperty("jpc_replace_method","");
                string jpc_message = checkItm.getProperty("jpc_message","");

                bool check = CheckPropValue(formItm,jpc_criteria,jpc_condition,jpc_cond_value);
                if(check==true)
                {
                    bool check_more = CheckMoreCriteriaCheckForm(formItm,checkItm.getID());
                    if(check_more){
                        return inn.newError(jpc_message);
                    }
                }
            }
            //return inn.newError(error_msg);
        }
        //--------------------------------//
    }else{
        //return AssignmentActSetting;
    }
    
return thisItem;