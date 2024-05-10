using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
public partial class Error : System.Web.UI.Page
{

    ProcessTransaction processTransaction = new ProcessTransaction();
    TokenRequest token = new TokenRequest();
    GenerateToken generateToken = new GenerateToken();
    BaseResult result = new BaseResult();
    GetList getList = new GetList();
    string voucherCode;
    protected void Page_Load(object sender, EventArgs e)
    {
        //if (!string.IsNullOrEmpty(Session["voucherCode"] as string))
        //{
            
        //}
        //else if (!string.IsNullOrEmpty(Session["referenceNumber"] as string))
        //{
            
        //}
        //else
        //{
        //    Response.Redirect(ConfigurationManager.AppSettings["ProductRegistration"].Trim());
        //}
    }
}