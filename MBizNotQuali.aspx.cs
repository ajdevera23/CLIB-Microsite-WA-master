using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MBizNotQuali : System.Web.UI.Page
{
    TokenRequest token = new TokenRequest();
    GenerateToken generateToken = new GenerateToken();
    GetList getList = new GetList();
    protected void Page_Load(object sender, EventArgs e)
    {
        emailLbl.Text = Session["GroupMail"].ToString();
    }

    protected void btnContinue_Click(object sender, EventArgs e)
    {
        Response.Redirect(ConfigurationManager.AppSettings["MBizOwnerDetails"].Trim());
    }


}

  



   

