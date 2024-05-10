using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Public_ThankYouPage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            lblNameOfProduct.Text = Session["ProductName"].ToString();
            lbCOCNumber.Text = Session["cocNumber"].ToString();
        }
        else
        {
            Session.Clear();
            Session.RemoveAll();
            Response.Redirect(ConfigurationManager.AppSettings["ProductRegistration"].Trim());
        }

    }
    protected void returnBtn_Click(object sender, EventArgs e)
    {
        Session.Clear();
        Session.RemoveAll();
        Response.Redirect(ConfigurationManager.AppSettings["ProductCategoryPage"]);
    }
}