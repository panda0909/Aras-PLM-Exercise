<%@ Page Language="C#" AutoEventWireup="true" CodeFile="_deleteWorkOrder.aspx.cs" Inherits="_deleteWorkOrder" %>

<%--<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="application/json; charset=utf-8"/>
    <script type="text/javascript" src="javascript/jquery/jquery-3.1.0.min.js"></script>
    <script>
        var cn_wo_tmp_json = null;
        var action_class = null;
        var record_json = null;
        function checkVal(key, val) {
            if (typeof val === 'string') {
                try { return JSON.parse(val); } catch (e) { }
            }
            return val;
        }
        function RunDeleteFModel(cn_no_first) {
            cn_wo_tmp_json = JSON.parse(cn_wo_tmp, checkVal);
            for (var i = 0; i < cn_wo_tmp_json.length; i++) {
                if (cn_wo_tmp_json[i].workclass.indexOf(cn_no_first) > -1) {
                    record_json = cn_wo_tmp_json[i].record;
                    action_class = i;
                }
            }
            var change_flag = false;
            var result_plan = 0;
            if (record_json === null) {
            } else {
                if (record_json.length > 0) {
                    for (var i = 0; i < record_json.length; i++) {
                        if (record_json[i].Record != null) {
                            var record_json_obj = record_json[i].Record;
                            for (var j = 0; j < record_json_obj.length; j++) {
                                var quantity = record_json_obj[j].Quantity;
                                var use_plan = record_json_obj[j].PlanInsertCount;
                                var worklist = record_json_obj[j].WorkOrderList;
                                var Manufacture_ItemNumber = record_json_obj[j].Manufacture_ItemNumber;
                                var Batch = record_json_obj[j].Batch;
                                if (worklist != null) {
                                    for (var k = 0; k < worklist.length; k++) {
                                        if (worklist[k].workorder_item_number == cn_no) {
                                            use_plan = use_plan - worklist[k].quantity;
                                            worklist[k].quantity = 0;
                                            record_json_obj[j].PlanInsertCount = use_plan;
                                            result_plan = use_plan;
                                            change_flag = true;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

            }

            if (change_flag === true) {
                for (var i = 0; i < cn_wo_tmp_json.length; i++) {
                    if (i != action_class) {
                        var tmp_record_json = cn_wo_tmp_json[i].record;
                        cn_wo_tmp_json[i].record = JSON.stringify(tmp_record_json);
                    }
                }
                cn_wo_tmp_json[action_class].record = JSON.stringify(record_json);
                //document.getElementById("json").innerHTML=JSON.stringify(cn_wo_tmp_json).replace("<","&gt");
                //document.write(JSON.stringify(cn_wo_tmp_json));
                //$.ajax({
                //        type: "POST",
                //        url: "_deleteWorkOrder.aspx/CreateJSONTxt",
                //        data: '{text:' + JSON.stringify(record_json) + '}',
                //        async: false,
                //    contentType: "application/json; charset=utf-8",
                //    dataType: "json",
                //    success: function (response) {
                //        //xmlDoc = $.parseXML(response.d);
                //        console.log(response);
                //    },
                //    failure: function (response) {
                //        return null;
                //    }
                //});
            }
        }
    </script>
       <%
//Response.Write("<script>RunTest();</script>");
Response.Write("<script>var cn_no='"+Request.Form["cn_no"]+"';</script>");
Response.Write("<script>var cn_wo_tmp='"+Request.Form["cn_wo_tmp"]+"';</script>");
  %>
    

</head>
<body>
    <div>
        <script type="text/javascript"> 
           //var cn_no_first=cn_no.substring(0,1);
           // //alert(cn_no_first);
           // switch(cn_no_first){
           //     case "F":
           //     case "D":
           //     case "B":
           //         RunDeleteFModel(cn_no_first);
           //         break;

           // }
           
           
                    
                    
        </script>
    </div>
    
</body>
</html>--%>




