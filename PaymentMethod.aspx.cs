using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ConfirmationPage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        Session["PartnerValue"] = "EQR";
        Session["VoucherCode"] = "CLIBPRODU001";
        // Redirect to the official link after setting the session
    }
    protected void Button1_Click(object sender, EventArgs e)
      
    {
       
        Response.Redirect("http://localhost:50774/ProductPage.aspx?a=TRI&b=Travel%20Insurance&c=400x100-Travel_Cat_Icon.jpg");
    }
}