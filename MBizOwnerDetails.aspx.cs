using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MBizOwnerDetails : System.Web.UI.Page
{
    TokenRequest token = new TokenRequest();
    GenerateToken generateToken = new GenerateToken();
    GetList getList = new GetList();
    protected void Page_Load(object sender, EventArgs e)
    {

        emailAddress.Attributes["type"] = "email";
        emailAddress.Attributes.Add("pattern", @"^([0-9a-zA-Z]" + //Start with a digit or alphabetical
                                                @"([\+\-_\.][0-9a-zA-Z]+)*" + // No continuous or ending +-_. chars in email
                                                @")+" +
                                                @"@(([0-9a-zA-Z][-\w]*[0-9a-zA-Z]*\.)+[a-zA-Z0-9]{2,17})$");
        
        //contactNumber.Attributes.Add("type", "tel");
        contactNumber.Attributes.Add("pattern", "^[0-9]+$");
        contactNumber.Attributes.Add("maxlength", "11");


        birthDateTextBox.Attributes.Add("type", "date");
        birthDateTextBox.Attributes.Add("required","true");

        birthDateTextBox.Attributes.Add("min", "1900-01-01");
        birthDateTextBox.Attributes.Add("max", DateTime.Now.ToString("yyyy-MM-dd"));

        //Case: 1 When the page is submitted for the first time(First PostBack) and there is file 
        // in FileUpload control but session is Null then Store the values to Session Object as:
        

        if (!IsPostBack)
        {
            //retrieveBranchRecord(); 
            //GetListProvince();
            
            FirstName.Disabled = false;
            LastName.Disabled = false;
            birthDateTextBox.Disabled = false;
        }
    }
    


    protected void btnValidate_Click(object sender, EventArgs e)
    { 
        string clientID = "";
        token.FirstName = FirstName.Value;
        token.LastName = LastName.Value;
        token.DOB = birthDateTextBox.Value;
        token.MiddleName = MiddleName.Value; 
        token.Suffix = Suffix.Value;
        token.ContactNumber = contactNumber.Value;
        token.Email = emailAddress.Value;
        token.Token = generateToken.GenerateTokenAuth();
        TokenRequest InsertClientTran;
        InsertClientTran = getList.TranMBPClient(token);

        TokenRequest GetClientID;
        GetClientID = getList.GetMBPClientID(token);
        clientID = GetClientID.ClientID;

        Session["ClientID"] = clientID;

        Response.Redirect("MBizQuestionnaire.aspx");
        //Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('User succesfully updated.');window.location = 'MBizQuestionnaire.aspx';", true);
        
    }
       
}

  



   

