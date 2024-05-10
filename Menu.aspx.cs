using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Menu : System.Web.UI.Page
{
    ProcessTransaction processTransaction = new ProcessTransaction();
    TokenRequest token = new TokenRequest();
    GenerateToken generateToken = new GenerateToken();
    BaseResult result = new BaseResult();
    GetList getList = new GetList();
    string voucherCode;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Session["voucherCode"] as string))
        {
            voucherCode = Session["voucherCode"].ToString();

            if (voucherCode == ConfigurationManager.AppSettings["CLIBvoucherCode"]
                || voucherCode == ConfigurationManager.AppSettings["CLIBvoucherCode1"]
                || voucherCode == ConfigurationManager.AppSettings["CLIBvoucherCode2"])
            {


            }
            else
            {
                Response.Redirect(ConfigurationManager.AppSettings["ProductRegistration"].Trim());
            }
        }
        else
        {
            Response.Redirect(ConfigurationManager.AppSettings["ProductRegistration"].Trim());
        }
    }
}