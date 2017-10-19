using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _XMLRead : System.Web.UI.Page
{
    string error_msg = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            string action = Request.Form["action"];
            string item_number = Request.Form["item_number"];
            string item_xml = Request.Form["xml"];

            switch (action)
            {
                case "delete":
                    string tmp = DeleteWorkOrder(item_number, item_xml);
                    Response.Write(tmp);
                    break;
            }
        }
        catch(Exception ex)
        {
            error_msg=ex.ToString();
        }
        if (error_msg != "")
        {
            Response.Write(error_msg);
        }
    }
    private string DeleteWorkOrder(string cn_no,string cn_wo_tmp)
    {

        string xml = "";
        try
        {
            string url = "http://localhost/plm/Client/customer/TLTC_Nanya/_deleteWorkOrder.aspx";
            
            Encoding myEncoding = Encoding.GetEncoding("utf-8");
            string param = HttpUtility.UrlEncode("cn_no", myEncoding) + "=" + HttpUtility.UrlEncode(cn_no, myEncoding)
            + "&" + HttpUtility.UrlEncode("cn_wo_tmp", myEncoding) + "=" + HttpUtility.UrlEncode(cn_wo_tmp, myEncoding);
            string result_cn_wo_tmp = Post(url, param);
            return result_cn_wo_tmp;
        }catch(Exception ex)
        {
            error_msg = ex.ToString();
            return "";
        }
    }
    public  string Post(string url, string content)
    {
        string result = "";
        try
        {
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
            req.Method = "POST";
            req.ContentType = "application/x-www-form-urlencoded";
            //req.ContentType = "application/json; charset=utf-8";  

            byte[] data = Encoding.UTF8.GetBytes(content);
            req.ContentLength = data.Length;
            using (Stream reqStream = req.GetRequestStream())
            {
                reqStream.Write(data, 0, data.Length);
                reqStream.Close();
            }


            HttpWebResponse resp = (HttpWebResponse)req.GetResponse();
            Stream stream = resp.GetResponseStream();
            //获取响应内容  
            using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
            {
                result = reader.ReadToEnd();
            }
            return result;
        }
        catch(Exception ex)
        {
            error_msg = ex.ToString();
            return result;
        }
        
    }
}