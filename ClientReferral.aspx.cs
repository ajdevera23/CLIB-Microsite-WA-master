using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ClientReferral : System.Web.UI.Page
{
    TokenRequest token = new TokenRequest();
    GenerateToken generateToken = new GenerateToken();
    GetList getList = new GetList();
    protected void Page_Load(object sender, EventArgs e)
    {
        emailAddress.Attributes["type"] = "email";
        emailAddress.Attributes.Add("pattern",  @"^([0-9a-zA-Z]" + //Start with a digit or alphabetical
                                                @"([\+\-_\.][0-9a-zA-Z]+)*" + // No continuous or ending +-_. chars in email
                                                @")+" +
                                                @"@(([0-9a-zA-Z][-\w]*[0-9a-zA-Z]*\.)+[a-zA-Z0-9]{2,17})$");
        cellphoneNo.Attributes.Add("type", "tel");
        cellphoneNo.Attributes.Add("pattern", "^[0-9]+$");
        cellphoneNo.Attributes.Add("maxlength", "11");
        birthDateTextBox.Attributes.Add("type", "date");
        birthDateTextBox.Attributes.Add("required","true");

        birthDateTextBox.Attributes.Add("min", "1900-01-01");
        birthDateTextBox.Attributes.Add("max", DateTime.Now.ToString("yyyy-MM-dd"));

        //Case: 1 When the page is submitted for the first time(First PostBack) and there is file 
        // in FileUpload control but session is Null then Store the values to Session Object as:
        if (Session["FileUpload1"] == null && PhotoUpload.HasFile)
        {
            Session["FileUpload1"] = PhotoUpload;
            lblImageName.Text = PhotoUpload.FileName;
        }
        // Case 2: On Next PostBack Session has value but FileUpload control is
        // Blank due to PostBack then return the values from session to FileUpload as:
        else if (Session["FileUpload1"] != null && (!PhotoUpload.HasFile))
        {
            PhotoUpload = (FileUpload)Session["FileUpload1"];
            lblImageName.Text = PhotoUpload.FileName;
        }
        // Case 3: When there is value in Session but user want to change the file then
        // In this case we need to change the file in session object also as:
        else if (PhotoUpload.HasFile)
        {
            Session["FileUpload1"] = PhotoUpload;
            lblImageName.Text = PhotoUpload.FileName;
        }

        if (!IsPostBack)
        {
            retrieveBranchRecord(); 
            GetListProvince();
            otherDetailsPnl.Visible = false;
            individualPnl.Visible = true;
            groupPnl.Visible = false;
            dpaPnl.Visible = false;
            btnSave.Enabled = false;
            FirstName.Disabled = false;
            LastName.Disabled = false;
            birthDateTextBox.Disabled = false;
        }
    }
    public void retrieveBranchRecord()
    {
        rtnLbl.Text = Session["RTN"].ToString();
        token.BranchCode = Session["BranchCode"].ToString();
        //Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('"+ Session["RTN"].ToString() + "')", true);

        token.Token = generateToken.GenerateTokenAuth();
        TokenRequest retrieveBranchRecord;
        retrieveBranchRecord = getList.RetrieveBranchDetails(token);
        regionLbl.Text = retrieveBranchRecord.Region;
        areaCodeLbl.Text = retrieveBranchRecord.AreaCode;
        branchCodeLbl.Text = retrieveBranchRecord.BranchCode;
        branchNameLbl.Text = retrieveBranchRecord.BranchName;

    }

    //public void retrieveIndividualRecord()
    //{
    //    token.FirstName = FirstName.Value;
    //    token.LastName = LastName.Value;
    //    token.DOB = birthDateTextBox.Text.ToString();
    //    token.Token = generateToken.GenerateTokenAuth();

    //    //branchNameLbl.Text = retrieveIndividualRecord.BranchName;

    //}
    public void GetListProvince()
    {
        token.Token = generateToken.GenerateTokenAuth();
        IList<String> provList;
        provList = getList.GetListProvince(token);
        DDProvince.DataSource = provList;
        DDProvince.DataBind();

    }
    public void GetListCity(string Prov)
    {
        token.Province = Prov;
        token.Token = generateToken.GenerateTokenAuth();
        IList<String> cityList;
        cityList = getList.GetListCity(token);
        DDcity.DataSource = cityList;
        DDcity.DataBind();

    }
    protected void DDProvince_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetListCity(DDProvince.SelectedValue);
    }

    protected void RadioButton_CheckedChanged(object sender, EventArgs e)
    {
        if (individualRbtn.Checked == true)
        {
            individualPnl.Visible = true;
            groupPnl.Visible = false;
        }
        else
        {
            individualPnl.Visible = true;
            groupPnl.Visible = true;
        }
    }

    protected void btnValidate_Click(object sender, EventArgs e)
    {
        token.FirstName = FirstName.Value;
        token.LastName = LastName.Value;
        token.DOB = birthDateTextBox.Value;
        token.GroupName = GroupName.Value;
        token.GroupContactPerson = ContactPerson.Value;
        token.Token = generateToken.GenerateTokenAuth();



        if (groupRbtn.Checked == true)
        {
            if (getList.CheckIfADCClientExists(token) == true)
            {
               
                //if Existing Client
                if (getList.ClientReferralAgingValidation(token) == true)
                {
                    token.Token = generateToken.GenerateTokenAuth();
                    TokenRequest retrieveDetailsPerADCClientList;
                    retrieveDetailsPerADCClientList = getList.RetrieveDetailsPerADCClient(token);
                    //Getting of Client ID
                    string ID = "";
                    ID = retrieveDetailsPerADCClientList.ClientID;
                    token.Token = generateToken.GenerateTokenAuth();
                    token.ClientID = ID;

                    //Existing Client with more than 90 days aging of client referral tran number (Action: Update Entry, Generate New Reference Number)
                    if (getList.ClientReferralAgingValidation(token) == true)
                    {
                        //Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Client already exists with 91 days lapsed.')", true);

                        individualPnl.Visible = true;
                        otherDetailsPnl.Visible = true;

                        TokenRequest retrieveIndividualRecord;
                        retrieveIndividualRecord = getList.RetrieveDetailsPerADCClient(token);
                        FirstName.Value = retrieveIndividualRecord.FirstName;
                        LastName.Value = retrieveIndividualRecord.LastName;

                        birthDateTextBox.Value = Convert.ToDateTime(retrieveIndividualRecord.DOB).ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
                        cellphoneNo.Value = retrieveIndividualRecord.ContactNumber;
                        emailAddress.Value = retrieveIndividualRecord.Email;
                        Address.Value = retrieveIndividualRecord.Address;
                        DDProvince.SelectedValue = retrieveIndividualRecord.Province;
                        GetListCity(retrieveIndividualRecord.Province);
                        DDcity.SelectedValue = retrieveIndividualRecord.City;
                        ZipCode.Value = retrieveIndividualRecord.ZipCode;
                        Interests.Value = retrieveIndividualRecord.Interests;
                        Appointments.Value = retrieveIndividualRecord.Appointments;
                        PhotoUpload.Visible = false;
                        photoTR.Visible = false;
                        btnSave.Enabled = true;
                        groupRbtn.Checked = true;
                        FirstName.Disabled = true;
                        LastName.Disabled = true;
                        birthDateTextBox.Disabled = true;

                    }
                    //Existing Client with less than 90 days aging of client referral tran number Action: No Action Needed. Collapse all panel)
                    else if (getList.ClientReferralAgingValidation(token) == false)
                    {
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Client already exists.');window.location = 'ClientReferral.aspx';", true);
                        LastName.Value = string.Empty;
                        FirstName.Value = string.Empty;
                        birthDateTextBox.Value = string.Empty;
                        GroupName.Value = string.Empty;
                        ContactPerson.Value = string.Empty;
                    }

                }
            }
            else
            {
                individualPnl.Visible = true;
                otherDetailsPnl.Visible = true;
                dpaPnl.Visible = true;
                Address.Value = string.Empty;
                DDProvince.SelectedValue = "Select";
                DDcity.SelectedValue = "Select";
                ZipCode.Value = string.Empty;
                Interests.Value = string.Empty;
                Appointments.Value = string.Empty;
            }
        }
        else if (individualRbtn.Checked == true)
        {
            
            //if Existing Client
            if (getList.CheckIfADCClientExists(token) == true)
            {
                token.Token = generateToken.GenerateTokenAuth();
                TokenRequest retrieveDetailsPerADCClientList;
                retrieveDetailsPerADCClientList = getList.RetrieveDetailsPerADCClient(token);
                //Getting of Client ID
                string ID = "";
                ID = retrieveDetailsPerADCClientList.ClientID;
                token.Token = generateToken.GenerateTokenAuth();
                token.ClientID = ID;

                //Existing Client with more than 90 days aging of client referral tran number (Action: Update Entry, Generate New Reference Number)
                if (getList.ClientReferralAgingValidation(token) == true)
                {
                    //Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Client already exists with 91 days lapsed.')", true);

                    individualPnl.Visible = true;
                    otherDetailsPnl.Visible = true;
                    TokenRequest retrieveIndividualRecord;
                    retrieveIndividualRecord = getList.RetrieveDetailsPerADCClient(token);
                    FirstName.Value = retrieveIndividualRecord.FirstName;
                    LastName.Value = retrieveIndividualRecord.LastName;

                    birthDateTextBox.Value = Convert.ToDateTime(retrieveIndividualRecord.DOB).ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
                    cellphoneNo.Value = retrieveIndividualRecord.ContactNumber;
                    emailAddress.Value = retrieveIndividualRecord.Email;
                    Address.Value = retrieveIndividualRecord.Address;
                    DDProvince.SelectedValue = retrieveIndividualRecord.Province;
                    GetListCity(retrieveIndividualRecord.Province);
                    DDcity.SelectedValue = retrieveIndividualRecord.City;
                    ZipCode.Value = retrieveIndividualRecord.ZipCode;
                    Interests.Value = retrieveIndividualRecord.Interests;
                    Appointments.Value = retrieveIndividualRecord.Appointments;
                  
                 
                    PhotoUpload.Visible = false;
                    photoTR.Visible = false;
                    btnSave.Enabled = true;
                    individualRbtn.Checked = true;
                    FirstName.Disabled = true;
                    LastName.Disabled = true;
                    birthDateTextBox.Disabled = true;
                }
                //Existing Client with less than 90 days aging of client referral tran number Action: No Action Needed. Collapse all panel)
                else if (getList.ClientReferralAgingValidation(token) == false)
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Client already exists.');window.location = 'ClientReferral.aspx';", true);
                    LastName.Value = string.Empty;
                    FirstName.Value = string.Empty;
                    birthDateTextBox.Value = string.Empty;
                }

            }
            //New Client    
            else
            {
                photoTR.Visible = true;
                individualPnl.Visible = true;
                otherDetailsPnl.Visible = true;
                dpaPnl.Visible = true;
                Address.Value = string.Empty;
                DDProvince.SelectedValue = "Select";
                DDcity.SelectedValue = "Select";
                ZipCode.Value = string.Empty;
                Interests.Value = string.Empty;
                Appointments.Value = string.Empty;
            }
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        token.FirstName = FirstName.Value;
        token.LastName = LastName.Value;
        token.DOB = birthDateTextBox.Value;
        token.GroupName = GroupName.Value;
        token.GroupContactPerson = ContactPerson.Value;
        token.Token = generateToken.GenerateTokenAuth();
        
        
        
        if(DDProvince.SelectedValue == "Select")
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Province is required.')", true);
        }
        else
        {
            if(DDcity.SelectedValue == "Select")
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('City is required.')", true);
            }
            else
            {
                if (groupRbtn.Checked == true)
                {
                    try
                    {
                        //new user
                        if (getList.CheckIfADCClientExists(token) == false)
                        {
                            string fileExt = Path.GetExtension(PhotoUpload.FileName).ToLower();
                            string strMessage = "";
                            int fileLength = PhotoUpload.FileName.Length;
                            int fileSizeLimit = 3000000;
                            int fileSizeActual = PhotoUpload.PostedFile.ContentLength;

                            if (PhotoUpload.HasFile)
                            {

                                if (fileExt == ".jpg" || fileExt == ".jpeg")
                                {

                                    if (fileLength > 50)
                                    {

                                        strMessage = "File name is too long. File name should NOT be more than 50 characters ";
                                        strMessage += "(including spaces and file extension).";
                                        Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('" + strMessage + "')", true);

                                        Session["FileUpload1"] = null;
                                    }
                                    else
                                    {
                                        if (fileSizeActual <= fileSizeLimit)
                                        {
                                            //ADC Client Tran
                                            token.Type = "1";
                                            token.GroupContactPerson = ContactPerson.Value;
                                            token.GroupName = GroupName.Value;
                                            token.FirstName = FirstName.Value;
                                            token.LastName = LastName.Value;
                                            token.DOB = birthDateTextBox.Value;
                                            token.Address = Address.Value;
                                            token.City = DDcity.SelectedValue.ToString();
                                            token.Province = DDProvince.SelectedValue.ToString();
                                            token.ZipCode = ZipCode.Value;
                                            token.Email = emailAddress.Value;
                                            token.ContactNumber = cellphoneNo.Value;
                                            token.Region = regionLbl.Text;
                                            token.AreaCode = areaCodeLbl.Text;
                                            token.BranchCode = branchCodeLbl.Text;
                                            token.BranchName = branchNameLbl.Text;
                                            token.AgentID = branchCodeLbl.Text;
                                            //PhotoSaving
                                            using (Stream s = PhotoUpload.PostedFile.InputStream)
                                            {
                                                using (BinaryReader br = new BinaryReader(s))
                                                {
                                                    byte[] Databytes = br.ReadBytes((Int32)s.Length);
                                                    token.Photo = Convert.ToBase64String(Databytes);

                                                }
                                            }
                                            token.Token = generateToken.GenerateTokenAuth();
                                            TokenRequest ClientReferralTran;
                                            ClientReferralTran = getList.ClientReferralGroupTran(token);

                                            //Getting of Client ID
                                            string ID = "";
                                            token.Token = generateToken.GenerateTokenAuth();
                                            TokenRequest retrieveDetailsPerADCClientList;
                                            retrieveDetailsPerADCClientList = getList.RetrieveDetailsPerADCClient(token);
                                            ID = retrieveDetailsPerADCClientList.ClientID;


                                            //Client Referral Tran
                                            token.ClientID = ID;
                                            token.ReferenceCode = rtnLbl.Text;
                                            token.Interests = Interests.Value;
                                            token.Appointments = Appointments.Value;
                                            token.BranchCode = branchCodeLbl.Text;
                                            token.Token = generateToken.GenerateTokenAuth();
                                            TokenRequest clientReferralTran;
                                            clientReferralTran = getList.ClientReferralTran(token);



                                            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Transaction Successfully Created');window.location = 'ClientReferral.aspx';", true);

                                            Session["FileUpload1"] = null;
                                            lblImageName.Visible = false;
                                        }
                                        else if (fileSizeActual > fileSizeLimit)
                                        {
                                            strMessage = "Allowed attachment file size is up to 3MB per file.";
                                            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('" + strMessage + "')", true);

                                            Session["FileUpload1"] = null;
                                            lblImageName.Visible = false;
                                        }
                                    }
                                }
                                else
                                {
                                    strMessage = @"Invalid file format. Allowed formats are as follows: \n";
                                    strMessage += "1. Image (*.jpg, *.jpeg)";
                                    Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('" + strMessage + "')", true);

                                    Session["FileUpload1"] = null;
                                    lblImageName.Visible = false;
                                }

                                //ClientSideScript.Alert("Client is successfully added.");
                            }
                            else
                            {
                                strMessage = "Photo ID is required.";
                                Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('" + strMessage + "')", true);

                            }
                        }
                        else
                        {
                            token.Type = "2";
                            token.FirstName = FirstName.Value;
                            token.LastName = LastName.Value;
                            token.DOB = birthDateTextBox.Value;


                            token.Address = Address.Value;
                            token.City = DDcity.SelectedValue.ToString();
                            token.Province = DDProvince.SelectedValue.ToString();
                            token.ZipCode = ZipCode.Value;
                            token.Email = emailAddress.Value;
                            token.ContactNumber = cellphoneNo.Value;
                            token.Region = regionLbl.Text;
                            token.AreaCode = areaCodeLbl.Text;
                            token.BranchCode = branchCodeLbl.Text;
                            token.BranchName = branchNameLbl.Text;
                            token.AgentID = branchCodeLbl.Text;
                            token.Token = generateToken.GenerateTokenAuth();
                            TokenRequest ClientReferralTran;

                            ClientReferralTran = getList.ClientReferralGroupTran(token);

                            //Getting of Client ID
                            string ID = "";
                            token.Token = generateToken.GenerateTokenAuth();
                            TokenRequest retrieveDetailsPerADCClientList;
                            retrieveDetailsPerADCClientList = getList.RetrieveDetailsPerADCClient(token);
                            ID = retrieveDetailsPerADCClientList.ClientID;

                            //Client Referral Tran
                            token.ClientID = ID;
                            token.ReferenceCode = rtnLbl.Text;
                            token.Interests = Interests.Value;
                            token.Appointments = Appointments.Value;
                            token.BranchCode = branchCodeLbl.Text;
                            token.Token = generateToken.GenerateTokenAuth();
                            TokenRequest clientReferralTran;
                            clientReferralTran = getList.ClientReferralTran(token);

                            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Transaction Successfully Updated');window.location = 'ClientReferral.aspx';", true);
                        }
                    }
                    catch (Exception error)
                    {
                        throw error;
                    }
                }
                else if (individualRbtn.Checked == true)
                {

                    try
                    {
                        //new user
                        if (getList.CheckIfADCClientExists(token) == false)  /*&& getList.ClientReferralAgingValidation(token) == true*/
                        {
                            string fileExt = Path.GetExtension(PhotoUpload.FileName).ToLower();
                            string strMessage = "";
                            int fileLength = PhotoUpload.FileName.Length;
                            int fileSizeLimit = 3000000;
                            int fileSizeActual = PhotoUpload.PostedFile.ContentLength;
                            if (PhotoUpload.HasFile)
                            {

                                if (fileExt == ".jpg" || fileExt == ".jpeg")
                                {

                                    if (fileLength > 50)
                                    {

                                        strMessage = "File name is too long. File name should NOT be more than 50 characters ";
                                        strMessage += "(including spaces and file extension).";
                                        Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('"+strMessage+"')", true);
                                        Session["FileUpload1"] = null;
                                    }
                                    else
                                    {
                                        if (fileSizeActual <= fileSizeLimit)
                                        {
                                            //ADC Client Tran
                                            token.Type = "1";
                                            token.FirstName = FirstName.Value;
                                            token.LastName = LastName.Value;
                                            token.DOB = birthDateTextBox.Value;

                                            token.Address = Address.Value;
                                            token.City = DDcity.SelectedValue.ToString();
                                            token.Province = DDProvince.SelectedValue.ToString();
                                            token.ZipCode = ZipCode.Value;
                                            token.Email = emailAddress.Value;
                                            token.ContactNumber = cellphoneNo.Value;
                                            token.Region = regionLbl.Text;
                                            token.AreaCode = areaCodeLbl.Text;
                                            token.BranchCode = branchCodeLbl.Text;
                                            token.BranchName = branchNameLbl.Text;
                                            token.AgentID = branchCodeLbl.Text;
                                            //PhotoSaving
                                            using (Stream s = PhotoUpload.PostedFile.InputStream)
                                            {
                                                using (BinaryReader br = new BinaryReader(s))
                                                {
                                                    byte[] Databytes = br.ReadBytes((Int32)s.Length);
                                                    token.Photo = Convert.ToBase64String(Databytes);

                                                }
                                            }
                                            token.Token = generateToken.GenerateTokenAuth();
                                            TokenRequest ClientReferralTran;
                                            ClientReferralTran = getList.ClientReferralIndividualTran(token);

                                            //Getting of Client ID
                                            string ID = "";
                                            token.Token = generateToken.GenerateTokenAuth();
                                            TokenRequest retrieveDetailsPerADCClientList;
                                            retrieveDetailsPerADCClientList = getList.RetrieveDetailsPerADCClient(token);
                                            ID = retrieveDetailsPerADCClientList.ClientID;


                                            //Client Referral Tran
                                            token.ClientID = ID;
                                            token.ReferenceCode = rtnLbl.Text;
                                            token.Interests = Interests.Value;
                                            token.Appointments = Appointments.Value;
                                            token.BranchCode = branchCodeLbl.Text;
                                            token.Token = generateToken.GenerateTokenAuth();
                                            TokenRequest clientReferralTran;
                                            clientReferralTran = getList.ClientReferralTran(token);



                                            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Transaction Successfully Created');window.location = 'ClientReferral.aspx';", true);


                                            Session["FileUpload1"] = null;
                                            lblImageName.Visible = false;
                                        }
                                        else if (fileSizeActual > fileSizeLimit)
                                        {
                                            strMessage = "Allowed attachment file size is up to 3MB per file.";
                                            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('" + strMessage + "')", true);



                                            Session["FileUpload1"] = null;
                                            lblImageName.Visible = false;
                                        }
                                    }
                                }
                                else
                                {
                                    strMessage = @"Invalid file format. Allowed formats are as follows: \n";
                                    strMessage += "1. Image (*.jpg, *.jpeg)";
                                    Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('" + strMessage + "')", true);



                                    lblImageName.Visible = false;
                                    Session["FileUpload1"] = null;
                                }

                                //ClientSideScript.Alert("Client is successfully added.");
                            }
                            else
                            {
                                strMessage = "Photo is required.";
                                Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('" + strMessage + "')", true);
                                lblImageName.Visible = false;
                                Session["FileUpload1"] = null;
                            }


                        }
                        else
                        {
                            token.Type = "2";
                            token.FirstName = FirstName.Value;
                            token.LastName = LastName.Value;
                            token.DOB = birthDateTextBox.Value;


                            token.Address = Address.Value;
                            token.City = DDcity.SelectedValue.ToString();
                            token.Province = DDProvince.SelectedValue.ToString();
                            token.ZipCode = ZipCode.Value;
                            token.Email = emailAddress.Value;
                            token.ContactNumber = cellphoneNo.Value;
                            token.Region = regionLbl.Text;
                            token.AreaCode = areaCodeLbl.Text;
                            token.BranchCode = branchCodeLbl.Text;
                            token.BranchName = branchNameLbl.Text;
                            token.AgentID = branchCodeLbl.Text;
                            token.Token = generateToken.GenerateTokenAuth();
                            TokenRequest ClientReferralTran;

                            ClientReferralTran = getList.ClientReferralIndividualTran(token);

                            //Getting of Client ID
                            string ID = "";
                            token.Token = generateToken.GenerateTokenAuth();
                            TokenRequest retrieveDetailsPerADCClientList;
                            retrieveDetailsPerADCClientList = getList.RetrieveDetailsPerADCClient(token);
                            ID = retrieveDetailsPerADCClientList.ClientID;

                            //Client Referral Tran
                            token.ClientID = ID;
                            token.ReferenceCode = rtnLbl.Text;
                            token.Interests = Interests.Value;
                            token.Appointments = Appointments.Value;
                            token.BranchCode = branchCodeLbl.Text;
                            token.Token = generateToken.GenerateTokenAuth();
                            TokenRequest clientReferralTran;
                            clientReferralTran = getList.ClientReferralTran(token);

                            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Transaction Successfully updated');window.location = 'ClientReferral.aspx';", true);


                        }


                    }
                    catch (Exception error)
                    {
                        throw error;
                    }


                }
            }
        }
       
        
    }



    protected void dataPrivacyCheckbox_CheckedChanged(object sender, EventArgs e)
    {
        if (dataPrivacyCheckbox.Checked)
        {
            btnSave.Enabled = true;
        }
        else
        {
            btnSave.Enabled = false;
        }
    }

}