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
        /*
        bool login_result = false;
        string url = Request.Form["host"];
        url = url.Replace("/Server", "");
        url = "http://" + url;
        login_result =PLM_Login(url, Request.Form["dbname"], Request.Form["user"], Request.Form["pwd"]);
        */
        //login_result = PLM_Login("http://192.168.0.208/plm", "Panda_NY", "Admin", "innovator");
        //Response.Write(login_result);

        cn_no = Request.Form["cn_no"];
        cn_wo_tmp = Request.Form["cn_wo_tmp"];
        //cn_no = "F-M3P2QQ1D_MPA53081_171013_1";
        //cn_wo_tmp = "[{\"workclass\":\"F- \",\"record\":\"[{\\\"Part_ItemNumber\\\":\\\"F-H4SQ6GE1\\\",\\\"pId\\\":\\\"3E39620BE267425EA56E4D1086CDFC86\\\",\\\"Category\\\":\\\"板金\\\",\\\"PlanInsertCount\\\":2,\\\"UsedInsertCount\\\":1,\\\"CAD_ItemNumber\\\":\\\"E2PADZ01\\\",\\\"CAD_id\\\":\\\"F407A6F610164AF5A155465CE843AE9D\\\",\\\"BOM\\\":[],\\\"Record\\\":[{\\\"Manufacture_ItemNumber\\\":\\\"MPA53081_01_002\\\",\\\"Batch\\\":\\\"2\\\",\\\"Product_ItemNumber\\\":\\\"PVV24062509104AV306T001\\\",\\\"Quantity\\\":1,\\\"Level\\\":\\\"1\\\",\\\"PlanInsertCount\\\":1,\\\"Temp_Now_PlanInsertCount\\\":[1],\\\"WorkOrderList\\\":[{\\\"workorder_item_number\\\":\\\"F-H4SQ6GE1_MPA53081_171013_1\\\",\\\"quantity\\\":1}],\\\"IsUpdate\\\":true},{\\\"Manufacture_ItemNumber\\\":\\\"MPA53081_01_010\\\",\\\"Batch\\\":\\\"1\\\",\\\"Product_ItemNumber\\\":\\\"PVV24062509104AV306T001\\\",\\\"Quantity\\\":1,\\\"Level\\\":\\\"1\\\",\\\"PlanInsertCount\\\":1,\\\"Temp_Now_PlanInsertCount\\\":[1],\\\"WorkOrderList\\\":[{\\\"workorder_item_number\\\":\\\"F-H4SQ6GE1_MPA53081_171013_1\\\",\\\"quantity\\\":1}],\\\"IsUpdate\\\":true}]},{\\\"Part_ItemNumber\\\":\\\"F-M3P2QQ1D\\\",\\\"pId\\\":\\\"5EDBA986082747199D255E41C3918FB8\\\",\\\"Category\\\":\\\"板金\\\",\\\"PlanInsertCount\\\":2,\\\"UsedInsertCount\\\":1,\\\"CAD_ItemNumber\\\":\\\"E2PADZ01\\\",\\\"CAD_id\\\":\\\"F407A6F610164AF5A155465CE843AE9D\\\",\\\"BOM\\\":[],\\\"Record\\\":[{\\\"Manufacture_ItemNumber\\\":\\\"MPA53081_01_002\\\",\\\"Batch\\\":\\\"2\\\",\\\"Product_ItemNumber\\\":\\\"PVV24062509104AV306T001\\\",\\\"Quantity\\\":1,\\\"Level\\\":\\\"11\\\",\\\"PlanInsertCount\\\":1,\\\"Temp_Now_PlanInsertCount\\\":[1],\\\"WorkOrderList\\\":[{\\\"workorder_item_number\\\":\\\"F-M3P2QQ1D_MPA53081_171013_1\\\",\\\"quantity\\\":1}],\\\"IsUpdate\\\":true},{\\\"Manufacture_ItemNumber\\\":\\\"MPA53081_01_010\\\",\\\"Batch\\\":\\\"1\\\",\\\"Product_ItemNumber\\\":\\\"PVV24062509104AV306T001\\\",\\\"Quantity\\\":1,\\\"Level\\\":\\\"11\\\",\\\"PlanInsertCount\\\":1,\\\"Temp_Now_PlanInsertCount\\\":[1],\\\"WorkOrderList\\\":[{\\\"workorder_item_number\\\":\\\"F-M3P2QQ1D_MPA53081_171013_1\\\",\\\"quantity\\\":1}],\\\"IsUpdate\\\":true}]},{\\\"Part_ItemNumber\\\":\\\"F-M3H35999\\\",\\\"pId\\\":\\\"3A03D28E0BDB4FF2AB53384F1D6A2CD2\\\",\\\"Category\\\":\\\"板金\\\",\\\"PlanInsertCount\\\":2,\\\"UsedInsertCount\\\":1,\\\"CAD_ItemNumber\\\":\\\"E2PADZ01\\\",\\\"CAD_id\\\":\\\"F407A6F610164AF5A155465CE843AE9D\\\",\\\"BOM\\\":[],\\\"Record\\\":[{\\\"Manufacture_ItemNumber\\\":\\\"MPA53081_01_002\\\",\\\"Batch\\\":\\\"2\\\",\\\"Product_ItemNumber\\\":\\\"PVV24062509104AV306T001\\\",\\\"Quantity\\\":1,\\\"Level\\\":\\\"13\\\",\\\"PlanInsertCount\\\":1,\\\"Temp_Now_PlanInsertCount\\\":[1],\\\"WorkOrderList\\\":[{\\\"workorder_item_number\\\":\\\"F-M3H35999_MPA53081_171013_1\\\",\\\"quantity\\\":1}],\\\"IsUpdate\\\":true},{\\\"Manufacture_ItemNumber\\\":\\\"MPA53081_01_010\\\",\\\"Batch\\\":\\\"1\\\",\\\"Product_ItemNumber\\\":\\\"PVV24062509104AV306T001\\\",\\\"Quantity\\\":1,\\\"Level\\\":\\\"13\\\",\\\"PlanInsertCount\\\":1,\\\"Temp_Now_PlanInsertCount\\\":[1],\\\"WorkOrderList\\\":[{\\\"workorder_item_number\\\":\\\"F-M3H35999_MPA53081_171013_1\\\",\\\"quantity\\\":1}],\\\"IsUpdate\\\":true}]},{\\\"Part_ItemNumber\\\":\\\"F-M3H35995\\\",\\\"pId\\\":\\\"51ECFA87A5B24F09A680EC3080FF2810\\\",\\\"Category\\\":\\\"板金\\\",\\\"PlanInsertCount\\\":2,\\\"UsedInsertCount\\\":1,\\\"CAD_ItemNumber\\\":\\\"E2PADZ01\\\",\\\"CAD_id\\\":\\\"F407A6F610164AF5A155465CE843AE9D\\\",\\\"BOM\\\":[],\\\"Record\\\":[{\\\"Manufacture_ItemNumber\\\":\\\"MPA53081_01_002\\\",\\\"Batch\\\":\\\"2\\\",\\\"Product_ItemNumber\\\":\\\"PVV24062509104AV306T001\\\",\\\"Quantity\\\":1,\\\"Level\\\":\\\"17\\\",\\\"PlanInsertCount\\\":1,\\\"Temp_Now_PlanInsertCount\\\":[1],\\\"WorkOrderList\\\":[{\\\"workorder_item_number\\\":\\\"F-M3H35995_MPA53081_171013_1\\\",\\\"quantity\\\":1}],\\\"IsUpdate\\\":true},{\\\"Manufacture_ItemNumber\\\":\\\"MPA53081_01_010\\\",\\\"Batch\\\":\\\"1\\\",\\\"Product_ItemNumber\\\":\\\"PVV24062509104AV306T001\\\",\\\"Quantity\\\":1,\\\"Level\\\":\\\"17\\\",\\\"PlanInsertCount\\\":1,\\\"Temp_Now_PlanInsertCount\\\":[1],\\\"WorkOrderList\\\":[{\\\"workorder_item_number\\\":\\\"F-M3H35995_MPA53081_171013_1\\\",\\\"quantity\\\":1}],\\\"IsUpdate\\\":true}]},{\\\"Part_ItemNumber\\\":\\\"F-M3J1J002\\\",\\\"pId\\\":\\\"5239F3BCA86145079E6D69BD2DF3DD3D\\\",\\\"Category\\\":\\\"板金\\\",\\\"PlanInsertCount\\\":2,\\\"UsedInsertCount\\\":1,\\\"CAD_ItemNumber\\\":\\\"E2PADZ01\\\",\\\"CAD_id\\\":\\\"F407A6F610164AF5A155465CE843AE9D\\\",\\\"BOM\\\":[],\\\"Record\\\":[{\\\"Manufacture_ItemNumber\\\":\\\"MPA53081_01_002\\\",\\\"Batch\\\":\\\"2\\\",\\\"Product_ItemNumber\\\":\\\"PVV24062509104AV306T001\\\",\\\"Quantity\\\":1,\\\"Level\\\":\\\"22\\\",\\\"PlanInsertCount\\\":1,\\\"Temp_Now_PlanInsertCount\\\":[1],\\\"WorkOrderList\\\":[{\\\"workorder_item_number\\\":\\\"F-M3J1J002_MPA53081_171013_1\\\",\\\"quantity\\\":1}],\\\"IsUpdate\\\":true},{\\\"Manufacture_ItemNumber\\\":\\\"MPA53081_01_010\\\",\\\"Batch\\\":\\\"1\\\",\\\"Product_ItemNumber\\\":\\\"PVV24062509104AV306T001\\\",\\\"Quantity\\\":1,\\\"Level\\\":\\\"22\\\",\\\"PlanInsertCount\\\":1,\\\"Temp_Now_PlanInsertCount\\\":[1],\\\"WorkOrderList\\\":[{\\\"workorder_item_number\\\":\\\"F-M3J1J002_MPA53081_171013_1\\\",\\\"quantity\\\":1}],\\\"IsUpdate\\\":true}]},{\\\"Part_ItemNumber\\\":\\\"F-M2N2501A\\\",\\\"pId\\\":\\\"5D0B8716739D4109A9181FC37B0EDD7E\\\",\\\"Category\\\":\\\"板金\\\",\\\"PlanInsertCount\\\":0,\\\"UsedInsertCount\\\":0,\\\"CAD_ItemNumber\\\":\\\"E2PADZ01\\\",\\\"CAD_id\\\":\\\"F407A6F610164AF5A155465CE843AE9D\\\",\\\"BOM\\\":[],\\\"Record\\\":null},{\\\"Part_ItemNumber\\\":\\\"F-M2N3501A\\\",\\\"pId\\\":\\\"61F1670F78B044139CDF7F535CA222D1\\\",\\\"Category\\\":\\\"板金\\\",\\\"PlanInsertCount\\\":2,\\\"UsedInsertCount\\\":1,\\\"CAD_ItemNumber\\\":\\\"E2PADZ01\\\",\\\"CAD_id\\\":\\\"F407A6F610164AF5A155465CE843AE9D\\\",\\\"BOM\\\":[],\\\"Record\\\":[{\\\"Manufacture_ItemNumber\\\":\\\"MPA53081_01_002\\\",\\\"Batch\\\":\\\"2\\\",\\\"Product_ItemNumber\\\":\\\"PVV24062509104AV306T001\\\",\\\"Quantity\\\":1,\\\"Level\\\":\\\"26\\\",\\\"PlanInsertCount\\\":1,\\\"Temp_Now_PlanInsertCount\\\":[1],\\\"WorkOrderList\\\":[{\\\"workorder_item_number\\\":\\\"F-M2N3501A_MPA53081_171013_1\\\",\\\"quantity\\\":1}],\\\"IsUpdate\\\":true},{\\\"Manufacture_ItemNumber\\\":\\\"MPA53081_01_010\\\",\\\"Batch\\\":\\\"1\\\",\\\"Product_ItemNumber\\\":\\\"PVV24062509104AV306T001\\\",\\\"Quantity\\\":1,\\\"Level\\\":\\\"26\\\",\\\"PlanInsertCount\\\":1,\\\"Temp_Now_PlanInsertCount\\\":[1],\\\"WorkOrderList\\\":[{\\\"workorder_item_number\\\":\\\"F-M2N3501A_MPA53081_171013_1\\\",\\\"quantity\\\":1}],\\\"IsUpdate\\\":true}]},{\\\"Part_ItemNumber\\\":\\\"F-M2A05C53\\\",\\\"pId\\\":\\\"48282226D53C4137B4C530DF518897A6\\\",\\\"Category\\\":\\\"板金\\\",\\\"PlanInsertCount\\\":2,\\\"UsedInsertCount\\\":1,\\\"CAD_ItemNumber\\\":\\\"E2PADZ01\\\",\\\"CAD_id\\\":\\\"F407A6F610164AF5A155465CE843AE9D\\\",\\\"BOM\\\":[],\\\"Record\\\":[{\\\"Manufacture_ItemNumber\\\":\\\"MPA53081_01_002\\\",\\\"Batch\\\":\\\"2\\\",\\\"Product_ItemNumber\\\":\\\"PVV24062509104AV306T001\\\",\\\"Quantity\\\":1,\\\"Level\\\":\\\"31\\\",\\\"PlanInsertCount\\\":1,\\\"Temp_Now_PlanInsertCount\\\":[1],\\\"WorkOrderList\\\":[{\\\"workorder_item_number\\\":\\\"F-M2A05C53_MPA53081_171013_1\\\",\\\"quantity\\\":1}],\\\"IsUpdate\\\":true},{\\\"Manufacture_ItemNumber\\\":\\\"MPA53081_01_010\\\",\\\"Batch\\\":\\\"1\\\",\\\"Product_ItemNumber\\\":\\\"PVV24062509104AV306T001\\\",\\\"Quantity\\\":1,\\\"Level\\\":\\\"31\\\",\\\"PlanInsertCount\\\":1,\\\"Temp_Now_PlanInsertCount\\\":[1],\\\"WorkOrderList\\\":[{\\\"workorder_item_number\\\":\\\"F-M2A05C53_MPA53081_171013_1\\\",\\\"quantity\\\":1}],\\\"IsUpdate\\\":true}]},{\\\"Part_ItemNumber\\\":\\\"F-H4SKFA01\\\",\\\"pId\\\":\\\"429394AABB3E47849147971B70EB50ED\\\",\\\"Category\\\":\\\"板金\\\",\\\"PlanInsertCount\\\":2,\\\"UsedInsertCount\\\":1,\\\"CAD_ItemNumber\\\":\\\"E2PADZ01\\\",\\\"CAD_id\\\":\\\"F407A6F610164AF5A155465CE843AE9D\\\",\\\"BOM\\\":[],\\\"Record\\\":[{\\\"Manufacture_ItemNumber\\\":\\\"MPA53081_01_002\\\",\\\"Batch\\\":\\\"2\\\",\\\"Product_ItemNumber\\\":\\\"PVV24062509104AV306T001\\\",\\\"Quantity\\\":1,\\\"Level\\\":\\\"34\\\",\\\"PlanInsertCount\\\":1,\\\"Temp_Now_PlanInsertCount\\\":[1],\\\"WorkOrderList\\\":[{\\\"workorder_item_number\\\":\\\"F-H4SKFA01_MPA53081_171013_1\\\",\\\"quantity\\\":1}],\\\"IsUpdate\\\":true},{\\\"Manufacture_ItemNumber\\\":\\\"MPA53081_01_010\\\",\\\"Batch\\\":\\\"1\\\",\\\"Product_ItemNumber\\\":\\\"PVV24062509104AV306T001\\\",\\\"Quantity\\\":1,\\\"Level\\\":\\\"34\\\",\\\"PlanInsertCount\\\":1,\\\"Temp_Now_PlanInsertCount\\\":[1],\\\"WorkOrderList\\\":[{\\\"workorder_item_number\\\":\\\"F-H4SKFA01_MPA53081_171013_1\\\",\\\"quantity\\\":1}],\\\"IsUpdate\\\":true}]},{\\\"Part_ItemNumber\\\":\\\"F-H4SJ5Z03\\\",\\\"pId\\\":\\\"57EC0114D01A4B338E268340D14F8224\\\",\\\"Category\\\":\\\"板金\\\",\\\"PlanInsertCount\\\":2,\\\"UsedInsertCount\\\":1,\\\"CAD_ItemNumber\\\":\\\"E2PADZ01\\\",\\\"CAD_id\\\":\\\"F407A6F610164AF5A155465CE843AE9D\\\",\\\"BOM\\\":[],\\\"Record\\\":[{\\\"Manufacture_ItemNumber\\\":\\\"MPA53081_01_002\\\",\\\"Batch\\\":\\\"2\\\",\\\"Product_ItemNumber\\\":\\\"PVV24062509104AV306T001\\\",\\\"Quantity\\\":1,\\\"Level\\\":\\\"37\\\",\\\"PlanInsertCount\\\":1,\\\"Temp_Now_PlanInsertCount\\\":[1],\\\"WorkOrderList\\\":[{\\\"workorder_item_number\\\":\\\"F-H4SJ5Z03_MPA53081_171013_1\\\",\\\"quantity\\\":1}],\\\"IsUpdate\\\":true},{\\\"Manufacture_ItemNumber\\\":\\\"MPA53081_01_010\\\",\\\"Batch\\\":\\\"1\\\",\\\"Product_ItemNumber\\\":\\\"PVV24062509104AV306T001\\\",\\\"Quantity\\\":1,\\\"Level\\\":\\\"37\\\",\\\"PlanInsertCount\\\":1,\\\"Temp_Now_PlanInsertCount\\\":[1],\\\"WorkOrderList\\\":[{\\\"workorder_item_number\\\":\\\"F-H4SJ5Z03_MPA53081_171013_1\\\",\\\"quantity\\\":1}],\\\"IsUpdate\\\":true}]},{\\\"Part_ItemNumber\\\":\\\"F-M2A2FB52\\\",\\\"pId\\\":\\\"43B168E02F9E4310B159077F538C3CB4\\\",\\\"Category\\\":\\\"板金\\\",\\\"PlanInsertCount\\\":0,\\\"UsedInsertCount\\\":0,\\\"CAD_ItemNumber\\\":\\\"E2PADZ01\\\",\\\"CAD_id\\\":\\\"F407A6F610164AF5A155465CE843AE9D\\\",\\\"BOM\\\":[],\\\"Record\\\":null},{\\\"Part_ItemNumber\\\":\\\"F-M2J00004\\\",\\\"pId\\\":\\\"5EECFC4440FD480EBE8A28A2164EEA1D\\\",\\\"Category\\\":\\\"板金\\\",\\\"PlanInsertCount\\\":2,\\\"UsedInsertCount\\\":1,\\\"CAD_ItemNumber\\\":\\\"E2PADZ01\\\",\\\"CAD_id\\\":\\\"F407A6F610164AF5A155465CE843AE9D\\\",\\\"BOM\\\":[],\\\"Record\\\":[{\\\"Manufacture_ItemNumber\\\":\\\"MPA53081_01_002\\\",\\\"Batch\\\":\\\"2\\\",\\\"Product_ItemNumber\\\":\\\"PVV24062509104AV306T001\\\",\\\"Quantity\\\":1,\\\"Level\\\":\\\"47\\\",\\\"PlanInsertCount\\\":1,\\\"Temp_Now_PlanInsertCount\\\":[1],\\\"WorkOrderList\\\":[{\\\"workorder_item_number\\\":\\\"F-M2J00004_MPA53081_171013_1\\\",\\\"quantity\\\":1}],\\\"IsUpdate\\\":true},{\\\"Manufacture_ItemNumber\\\":\\\"MPA53081_01_010\\\",\\\"Batch\\\":\\\"1\\\",\\\"Product_ItemNumber\\\":\\\"PVV24062509104AV306T001\\\",\\\"Quantity\\\":1,\\\"Level\\\":\\\"47\\\",\\\"PlanInsertCount\\\":1,\\\"Temp_Now_PlanInsertCount\\\":[1],\\\"WorkOrderList\\\":[{\\\"workorder_item_number\\\":\\\"F-M2J00004_MPA53081_171013_1\\\",\\\"quantity\\\":1}],\\\"IsUpdate\\\":true}]},{\\\"Part_ItemNumber\\\":\\\"F-H3SJ014\\\",\\\"pId\\\":\\\"62DBFABD8492456D89674C5D1233CFA9\\\",\\\"Category\\\":\\\"板金\\\",\\\"PlanInsertCount\\\":20,\\\"UsedInsertCount\\\":10,\\\"CAD_ItemNumber\\\":\\\"E2PADZ01\\\",\\\"CAD_id\\\":\\\"F407A6F610164AF5A155465CE843AE9D\\\",\\\"BOM\\\":[],\\\"Record\\\":[{\\\"Manufacture_ItemNumber\\\":\\\"MPA53081_01_002\\\",\\\"Batch\\\":\\\"2\\\",\\\"Product_ItemNumber\\\":\\\"PVV24062509104AV306T001\\\",\\\"Quantity\\\":10,\\\"Level\\\":\\\"49\\\",\\\"PlanInsertCount\\\":10,\\\"Temp_Now_PlanInsertCount\\\":[10],\\\"WorkOrderList\\\":[{\\\"workorder_item_number\\\":\\\"F-H3SJ014_MPA53081_171013_1\\\",\\\"quantity\\\":10}],\\\"IsUpdate\\\":true},{\\\"Manufacture_ItemNumber\\\":\\\"MPA53081_01_010\\\",\\\"Batch\\\":\\\"1\\\",\\\"Product_ItemNumber\\\":\\\"PVV24062509104AV306T001\\\",\\\"Quantity\\\":10,\\\"Level\\\":\\\"49\\\",\\\"PlanInsertCount\\\":10,\\\"Temp_Now_PlanInsertCount\\\":[10],\\\"WorkOrderList\\\":[{\\\"workorder_item_number\\\":\\\"F-H3SJ014_MPA53081_171013_1\\\",\\\"quantity\\\":10}],\\\"IsUpdate\\\":true}]},{\\\"Part_ItemNumber\\\":\\\"F-M2H10053\\\",\\\"pId\\\":\\\"46B76371E3E14EEF9D12CFE6F6F55A05\\\",\\\"Category\\\":\\\"板金\\\",\\\"PlanInsertCount\\\":4,\\\"UsedInsertCount\\\":2,\\\"CAD_ItemNumber\\\":\\\"E2PADZ01\\\",\\\"CAD_id\\\":\\\"F407A6F610164AF5A155465CE843AE9D\\\",\\\"BOM\\\":[],\\\"Record\\\":[{\\\"Manufacture_ItemNumber\\\":\\\"MPA53081_01_002\\\",\\\"Batch\\\":\\\"2\\\",\\\"Product_ItemNumber\\\":\\\"PVV24062509104AV306T001\\\",\\\"Quantity\\\":2,\\\"Level\\\":\\\"53\\\",\\\"PlanInsertCount\\\":2,\\\"Temp_Now_PlanInsertCount\\\":[2],\\\"WorkOrderList\\\":[{\\\"workorder_item_number\\\":\\\"F-M2H10053_MPA53081_171013_1\\\",\\\"quantity\\\":2}],\\\"IsUpdate\\\":true},{\\\"Manufacture_ItemNumber\\\":\\\"MPA53081_01_010\\\",\\\"Batch\\\":\\\"1\\\",\\\"Product_ItemNumber\\\":\\\"PVV24062509104AV306T001\\\",\\\"Quantity\\\":2,\\\"Level\\\":\\\"53\\\",\\\"PlanInsertCount\\\":2,\\\"Temp_Now_PlanInsertCount\\\":[2],\\\"WorkOrderList\\\":[{\\\"workorder_item_number\\\":\\\"F-M2H10053_MPA53081_171013_1\\\",\\\"quantity\\\":2}],\\\"IsUpdate\\\":true}]},{\\\"Part_ItemNumber\\\":\\\"F-HMTL7F03\\\",\\\"pId\\\":\\\"682D22EA8CBC42E5852E81EE161773F5\\\",\\\"Category\\\":\\\"板金\\\",\\\"PlanInsertCount\\\":2,\\\"UsedInsertCount\\\":1,\\\"CAD_ItemNumber\\\":\\\"E2PADZ01\\\",\\\"CAD_id\\\":\\\"F407A6F610164AF5A155465CE843AE9D\\\",\\\"BOM\\\":[],\\\"Record\\\":[{\\\"Manufacture_ItemNumber\\\":\\\"MPA53081_01_002\\\",\\\"Batch\\\":\\\"2\\\",\\\"Product_ItemNumber\\\":\\\"PVV24062509104AV306T001\\\",\\\"Quantity\\\":1,\\\"Level\\\":\\\"65\\\",\\\"PlanInsertCount\\\":1,\\\"Temp_Now_PlanInsertCount\\\":[1],\\\"WorkOrderList\\\":[{\\\"workorder_item_number\\\":\\\"F-HMTL7F03_MPA53081_171013_1\\\",\\\"quantity\\\":1}],\\\"IsUpdate\\\":true},{\\\"Manufacture_ItemNumber\\\":\\\"MPA53081_01_010\\\",\\\"Batch\\\":\\\"1\\\",\\\"Product_ItemNumber\\\":\\\"PVV24062509104AV306T001\\\",\\\"Quantity\\\":1,\\\"Level\\\":\\\"65\\\",\\\"PlanInsertCount\\\":1,\\\"Temp_Now_PlanInsertCount\\\":[1],\\\"WorkOrderList\\\":[{\\\"workorder_item_number\\\":\\\"F-HMTL7F03_MPA53081_171013_1\\\",\\\"quantity\\\":1}],\\\"IsUpdate\\\":true}]},{\\\"Part_ItemNumber\\\":\\\"F-H1SBF018\\\",\\\"pId\\\":\\\"5F76FFD22D7346C2839AFBB2B17A2BFD\\\",\\\"Category\\\":\\\"板金\\\",\\\"PlanInsertCount\\\":0,\\\"UsedInsertCount\\\":0,\\\"CAD_ItemNumber\\\":\\\"E2PADZ01\\\",\\\"CAD_id\\\":\\\"F407A6F610164AF5A155465CE843AE9D\\\",\\\"BOM\\\":[],\\\"Record\\\":null},{\\\"Part_ItemNumber\\\":\\\"F-H3SC00FC\\\",\\\"pId\\\":\\\"60D23EB090884F89ACE198034B79B82A\\\",\\\"Category\\\":\\\"板金\\\",\\\"PlanInsertCount\\\":0,\\\"UsedInsertCount\\\":0,\\\"CAD_ItemNumber\\\":\\\"E2PADZ01\\\",\\\"CAD_id\\\":\\\"F407A6F610164AF5A155465CE843AE9D\\\",\\\"BOM\\\":[],\\\"Record\\\":null},{\\\"Part_ItemNumber\\\":\\\"F-H4Z16103\\\",\\\"pId\\\":\\\"5AF93EA0137D4658A18C13ECF4932C38\\\",\\\"Category\\\":\\\"板金\\\",\\\"PlanInsertCount\\\":2,\\\"UsedInsertCount\\\":1,\\\"CAD_ItemNumber\\\":\\\"E2PADZ01\\\",\\\"CAD_id\\\":\\\"F407A6F610164AF5A155465CE843AE9D\\\",\\\"BOM\\\":[],\\\"Record\\\":[{\\\"Manufacture_ItemNumber\\\":\\\"MPA53081_01_002\\\",\\\"Batch\\\":\\\"2\\\",\\\"Product_ItemNumber\\\":\\\"PVV24062509104AV306T001\\\",\\\"Quantity\\\":1,\\\"Level\\\":\\\"75\\\",\\\"PlanInsertCount\\\":1,\\\"Temp_Now_PlanInsertCount\\\":[1],\\\"WorkOrderList\\\":[{\\\"workorder_item_number\\\":\\\"F-H4Z16103_MPA53081_171013_1\\\",\\\"quantity\\\":1}],\\\"IsUpdate\\\":true},{\\\"Manufacture_ItemNumber\\\":\\\"MPA53081_01_010\\\",\\\"Batch\\\":\\\"1\\\",\\\"Product_ItemNumber\\\":\\\"PVV24062509104AV306T001\\\",\\\"Quantity\\\":1,\\\"Level\\\":\\\"75\\\",\\\"PlanInsertCount\\\":1,\\\"Temp_Now_PlanInsertCount\\\":[1],\\\"WorkOrderList\\\":[{\\\"workorder_item_number\\\":\\\"F-H4Z16103_MPA53081_171013_1\\\",\\\"quantity\\\":1}],\\\"IsUpdate\\\":true}]}]\"}]";
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
