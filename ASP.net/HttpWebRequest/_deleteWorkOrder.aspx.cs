using Aras.IOM;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using System.Web.UI;
using System.Web.UI.WebControls;
/*Json.NET相關的命名空間*/
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections;

public partial class _deleteWorkOrder : System.Web.UI.Page
{
    static Innovator CoInnovator;
    private string cn_no="";
    private string cn_wo_tmp = "";
    public class WorkOrderList
    {
        public string workorder_item_number { get; set; }
        public int quantity { get; set; }
    }

    public class Record
    {
        public string Manufacture_ItemNumber { get; set; }
        public string Batch { get; set; }
        public string Product_ItemNumber { get; set; }
        public int Quantity { get; set; }
        public string Level { get; set; }
        public int PlanInsertCount { get; set; }
        public List<int> Temp_Now_PlanInsertCount { get; set; }
        public List<WorkOrderList> WorkOrderList { get; set; }
        public bool IsUpdate { get; set; }
    }

    public class RootObject
    {
        public string Part_ItemNumber { get; set; }
        public string pId { get; set; }
        public string Category { get; set; }
        public int PlanInsertCount { get; set; }
        public int UsedInsertCount { get; set; }
        public string CAD_ItemNumber { get; set; }
        public string CAD_id { get; set; }
        public List<object> BOM { get; set; }
        public List<Record> Record { get; set; }
    }
    public class RootNode
    {
        public string workclass { get; set; }
        public string record { get; set; }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        

        cn_no = Request.Form["cn_no"];
        cn_wo_tmp = Request.Form["cn_wo_tmp"];
        
        if (cn_wo_tmp == "")
        {
            Response.Write("Error:暫存檔空值");
        }
        if (cn_no == "")
        {
            Response.Write("Error:編號空值");
        }
        
        Response.Write(RunDeleteRecord());

    }
    
    #region "公用函式"
    public Boolean PLM_Login(string url, string db, string user, string pw)
    {

        try
        {
            //Debugger.Break();
            HttpServerConnection cnx = IomFactory.CreateHttpServerConnection(url, db, user, pw);
            Item login_result = cnx.Login();
            if (!login_result.isError())
            {
                CoInnovator = IomFactory.CreateInnovator(cnx);
            }
            return true;
        }
        catch
        {
            return false;
        }

    }
    #endregion

    #region "私有函式"
    private string RunDeleteRecord()
    {
        string json_result = "";
        string cn_no_first_char = "";
        int flag_count = 0;
        try
        {
            cn_no_first_char = cn_no.Substring(0,1);

            List<RootNode> root = JsonConvert.DeserializeObject<List<RootNode>>(cn_wo_tmp);
            for(int rootCount = 0; rootCount < root.Count(); rootCount++)
            {
                if (root[rootCount].workclass.IndexOf(cn_no_first_char) > -1)
                {
                    switch (cn_no_first_char)
                    {
                        case "F":
                        case "D":
                        case "B":
                        case "L":
                        case "K":
                            json_result =RunDeleteModel_F(root[rootCount].record);
                            break;
                    }
                    root[rootCount].record = json_result;
                    break;
                }
            }
            json_result= JsonConvert.SerializeObject(root);
            return json_result;
        }catch(Exception ex)
        {
            return json_result;
        }
    }
    private string RunDeleteModel_F(string record)
    {
        string result = "";
        try
        {
            List<RootObject> root = JsonConvert.DeserializeObject<List<RootObject>>(record);
            for(int rootCount = 0; rootCount < root.Count(); rootCount++)
            {
                List<Record> record_list = root[rootCount].Record;
                if (record_list == null) continue;
                for(int recordCount = 0; recordCount < record_list.Count(); recordCount++)
                {
                    int PlanInsesrtCount = record_list[recordCount].PlanInsertCount;
                    List<WorkOrderList> worklist = record_list[recordCount].WorkOrderList;
                    if (worklist == null) continue;
                    for (int woCount = 0; woCount < worklist.Count(); woCount++)
                    {
                        if (worklist[woCount].workorder_item_number == cn_no)
                        {
                            PlanInsesrtCount = PlanInsesrtCount - worklist[woCount].quantity;
                            worklist[woCount].quantity=0;
                            worklist.RemoveAt(woCount);
                        }
                    }

                    record_list[recordCount].PlanInsertCount = PlanInsesrtCount;
                    record_list[recordCount].WorkOrderList = worklist;
                }
                root[rootCount].Record = record_list;
            }
            result = JsonConvert.SerializeObject(root);
            return result;
        }catch(Exception ex)
        {
            return "";
        }
    }
    private string RunDeleteModel_GH(string record)
    {
        string result = "";
        try
        {
            List<RootObject> root = JsonConvert.DeserializeObject<List<RootObject>>(record);
            for (int rootCount = 0; rootCount < root.Count(); rootCount++)
            {
                
            }
            result = JsonConvert.SerializeObject(root);
            return result;
        }
        catch (Exception ex)
        {
            return "";
        }
    }
    #endregion 
}
