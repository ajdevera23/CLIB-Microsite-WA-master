using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MBizThankYou : System.Web.UI.Page
{
    TokenRequest token = new TokenRequest();
    GenerateToken generateToken = new GenerateToken();
    BaseResult baseResult = new BaseResult();
    ProcessTransaction processTransaction = new ProcessTransaction();
    GetList getList = new GetList();
    protected void Page_Load(object sender, EventArgs e)
    {
        lblRTN.Text =  Session["ReferenceCode"].ToString();
        string ClientID = Session["ClientID"].ToString();
        token.ClientID = ClientID;
        token.Token = token.Token = generateToken.GenerateTokenAuth();
        TokenRequest retrieveClientRecord;
        retrieveClientRecord = getList.RetrieveMBPClientDetails(token);

        if (!IsPostBack)
        {
            token.Email = retrieveClientRecord.Email;
            token.FirstName = retrieveClientRecord.FirstName;
            token.ReferenceCode = Session["ReferenceCode"].ToString();
            token.GroupMail = Session["GroupMail"].ToString();
            token.Token = generateToken.GenerateTokenAuth();
            baseResult = processTransaction.SendMBPEmail(token);
            
        }
       
    }

    protected void btnContinue_Click(object sender, EventArgs e)
    {
        
        Session.Clear();
        Session.RemoveAll();
        Response.Redirect(ConfigurationManager.AppSettings["MBizOwnerDetails"].Trim());
    }


}

  



   

