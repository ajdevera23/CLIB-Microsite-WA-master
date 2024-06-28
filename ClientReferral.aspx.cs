using System;
using System.Collections.Generic;
using System.Configuration;
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

    AgentReferralRequest agentReferralRequest = new AgentReferralRequest();
    ADCClientIfExistResult adcclientifexistresult = new ADCClientIfExistResult();
    ADCClientIfExistRequest adcclientifexistrequest = new ADCClientIfExistRequest();

    GetList getList = new GetList();

    int sessionLength;

    protected void Page_Load(object sender, EventArgs e)
    {
        PageLoadFunctions();

        sessionLength = Session["RTN"].ToString().Length;

        if (!IsPostBack)
        {
           

            lblImageName.Visible = false;
            if (sessionLength == 17)
            {
                CallMethodClientReferral();
            }
            if (sessionLength == 25)
            {
                CallMethodAgentReferral();
            }

        }
        if (IsPostBack)
        {

            string fileName = HiddenFileName.Value;
            if (!string.IsNullOrEmpty(fileName))
            {
                fileLabelText.InnerText = fileName;
            }
            else
            {
                fileLabelText.InnerText = "* Choose a file JPEG, JPG and up to 3MB";
            }
        }
    }

    #region PAGE LOAD FUNCTIONS
    public void PageLoadFunctions()
    {

        try
        {
      
            emailAddress.Attributes["type"] = "email";
            emailAddress.Attributes.Add("pattern", @"^([0-9a-zA-Z]" + //Start with a digit or alphabetical
                                                    @"([\+\-_\.][0-9a-zA-Z]+)*" + // No continuous or ending +-_. chars in email
                                                    @")+" +
                                                    @"@(([0-9a-zA-Z][-\w]*[0-9a-zA-Z]*\.)+[a-zA-Z0-9]{2,17})$");
            cellphoneNo.Attributes.Add("type", "tel");
            cellphoneNo.Attributes.Add("pattern", "^[0-9]+$");
            cellphoneNo.Attributes.Add("maxlength", "11");
            birthDateTextBox.Attributes.Add("type", "date");
            birthDateTextBox.Attributes.Add("required", "true");

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
        }
        catch (Exception ex)
        {
            SystemUtility.EventLog.SaveError(ex.ToString());
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "Swal.fire(`" + ex + "`); ", true);
            throw;
        }


    }

    #endregion

    #region CALL CLIENT REFERRAL METHOD
    public void CallMethodClientReferral()
    {
        try
        {
            retrieveBranchRecord();
            GetListProvince();
            otherDetailsPnl.Visible = false;
            individualPnl.Visible = true;
            groupPnl.Visible = false;
            dpaPnl.Visible = false;
            btnSave.Visible = false;
            FirstName.Disabled = false;
            LastName.Disabled = false;
            birthDateTextBox.Disabled = false;
        }
        catch (Exception ex)
        {
            SystemUtility.EventLog.SaveError(ex.ToString());
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "Swal.fire(`" + ex + "`); ", true);
            throw;
        }

    }

    #endregion

    #region CALL AGENT REFERRAL METHOD
    public void CallMethodAgentReferral()
    {
        try
        {
            rtnLbl.Value = Session["RTN"].ToString();
            fld_AffiliateCode.Value = Session["ADCAffiliateCode"].ToString();
            fld_AffiliateName.Value = Session["ADCFirstName"].ToString() + " " + Session["ADCLastName"].ToString();
            GetListProvince();
            otherDetailsPnl.Visible = false;
            individualPnl.Visible = true;
            groupPnl.Visible = false;
            dpaPnl.Visible = false;
            btnSave.Visible = false;
            FirstName.Disabled = false;
            LastName.Disabled = false;
            birthDateTextBox.Disabled = false;
        }
        catch (Exception ex)
        {
            SystemUtility.EventLog.SaveError(ex.ToString());
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "Swal.fire(`" + ex + "`); ", true);
            throw;
        }

    }

    #endregion

    #region RETRIEVE BRANCH RECORD
    public void retrieveBranchRecord()
    {
        try
        {
            rtnLbl.Value = Session["RTN"].ToString();
            token.BranchCode = Session["BranchCode"].ToString();
            //Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('"+ Session["RTN"].ToString() + "')", true);

            token.Token = generateToken.GenerateTokenAuth();
            TokenRequest retrieveBranchRecord;
            retrieveBranchRecord = getList.RetrieveBranchDetails(token);
            regionLbl.Value = retrieveBranchRecord.Region;
            areaCodeLbl.Value = retrieveBranchRecord.AreaCode;
            branchCodeLbl.Value = retrieveBranchRecord.BranchCode;
            branchNameLbl.Value = retrieveBranchRecord.BranchName;
        }
        catch (Exception ex)
        {
            SystemUtility.EventLog.SaveError(ex.ToString());
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "Swal.fire(`" + ex + "`); ", true);
            throw;
        }


    }

    #endregion

    #region GET LIST PROVINCE
    public void GetListProvince()
    {
        try
        {
            token.Token = generateToken.GenerateTokenAuth();
            IList<String> provList;
            provList = getList.GetListProvince(token);
            DDProvince.DataSource = provList;
            DDProvince.DataBind();
        }
        catch (Exception ex)
        {
            SystemUtility.EventLog.SaveError(ex.ToString());
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "Swal.fire(`" + ex + "`); ", true);
            throw;
        }
    }

    #endregion

    #region GET LIST CITY
    public void GetListCity(string Prov)
    {
        try
        {
            token.Province = Prov;
            token.Token = generateToken.GenerateTokenAuth();
            IList<String> cityList;
            cityList = getList.GetListCity(token);
            DDcity.DataSource = cityList;
            DDcity.DataBind();
        }
        catch (Exception ex)
        {
            SystemUtility.EventLog.SaveError(ex.ToString());
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "Swal.fire(`" + ex + "`); ", true);
            throw;
        }
    }

    #endregion

    protected void DDProvince_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetListCity(DDProvince.SelectedValue);
        //UpdatePanel1.Update();
    }
    protected void RadioButton_CheckedChanged(object sender, EventArgs e)
    {
        if (individualRbtn.Checked == true)
        {
            individualPnl.Visible = true;
            groupPnl.Visible = false;
            txt_clienttype.Value = "INDIVIDUAL";
        }
        else
        {
            individualPnl.Visible = true;
            groupPnl.Visible = true;
            txt_clienttype.Value = "GROUP";
        }
    }
    protected void btnValidate_Click(object sender, EventArgs e)
    {
        try
        {
            if (sessionLength == 17)
            {
                ValidateClientReferral();
            }
            if (sessionLength == 25)
            {
                ValidateAgentReferral();
            }
        }
        catch (Exception ex)
        {
            SystemUtility.EventLog.SaveError(ex.ToString());
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "Swal.fire(`" + ex + "`); ", true);
            throw;
        }

    }

    #region VALIDATE BASIC INFO
    public void ValidateBasicInfo()
    {
        try
        {
            token.FirstName = FirstName.Value;
            token.LastName = LastName.Value;
            token.DOB = birthDateTextBox.Value;
            token.GroupName = fld_GroupName.Value;
            token.GroupContactPerson = ContactPerson.Value;
            token.Token = generateToken.GenerateTokenAuth();
        }
        catch (Exception ex)
        {
            SystemUtility.EventLog.SaveError(ex.ToString());
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "Swal.fire(`" + ex + "`); ", true);
            throw;
        }

    }
    #endregion

    #region VISIBLE INPUT
    public void VisibleInput()
    {
        try
        {
            btnValidate.Visible = false;
            emailAddress.Focus();
            photoTR.Visible = true;
            individualPnl.Visible = true;
            FirstName.Attributes.Add("disabled", "disabled");
            LastName.Attributes.Add("disabled", "disabled");
            birthDateTextBox.Attributes.Add("disabled", "disabled");
            otherDetailsPnl.Visible = true;
            dpaPnl.Visible = true;
            btnSave.Visible = true;
            fld_Address.Value = string.Empty;
            DDProvince.SelectedValue = "Select";
            DDcity.SelectedValue = "Select";
            ZipCode.Value = string.Empty;
            fld_Interests.Value = string.Empty;
            fld_Appointments.Value = string.Empty;
        }
        catch (Exception ex)
        {
            SystemUtility.EventLog.SaveError(ex.ToString());
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "Swal.fire(`" + ex + "`); ", true);
            throw;
        }

    }
    #endregion

    #region VALIDATE AGENT REFERRAL
    public void ValidateAgentReferral()
    {
        try
        {
            string birthdate = "";

            birthdate = DateTime.Parse(birthDateTextBox.Value.Trim()).ToString("MM/dd/yyyy");
            adcclientifexistrequest.Token = generateToken.GenerateTokenAuth();
            adcclientifexistrequest.Birthdate = birthdate;
            adcclientifexistrequest.FirstName = FirstName.Value;
            adcclientifexistrequest.LastName = LastName.Value;
            adcclientifexistrequest.PlatformKey = ConfigurationManager.AppSettings["CLIBAPIKey"];

            var returnValue = getList.GetADCClientIfExist(adcclientifexistrequest);
            string message = returnValue.Message;

            if (message == "Client Not Exist")
            {
                VisibleInput();
            }
            else if (message == "Client Already Exist, Got Client Details")
            {
                VisibleInput();
               
            }
            else if(message == "Client already exists; Client can be referred.")
            {
                VisibleInput();
                string ADCCity = returnValue.Result[0].ADCCity != null ? returnValue.Result[0].ADCCity.ToString() : "";
                string ADCClientId = returnValue.Result[0].ADCClientId != null ? returnValue.Result[0].ADCClientId.ToString() : "";
                DateTime ADCDateOfBirth = returnValue.Result[0].ADCDateOfBirth != null ? returnValue.Result[0].ADCDateOfBirth : DateTime.MinValue;
                string ADCEmailAddress = returnValue.Result[0].ADCEmailAddress != null ? returnValue.Result[0].ADCEmailAddress.ToString() : "";
                string ADCEmployer = returnValue.Result[0].ADCEmployer != null ? returnValue.Result[0].ADCEmployer.ToString() : "";
                string ADCFirstName = returnValue.Result[0].ADCFirstName != null ? returnValue.Result[0].ADCFirstName.ToString() : "";
                string ADCLastName = returnValue.Result[0].ADCLastName != null ? returnValue.Result[0].ADCLastName.ToString() : "";
                string ADCMiddleName = returnValue.Result[0].ADCMiddleName != null ? returnValue.Result[0].ADCMiddleName.ToString() : "";
                string ADCMobileNumber = returnValue.Result[0].ADCMobileNumber != null ? returnValue.Result[0].ADCMobileNumber.ToString() : "";
                string ADCNatureOfWork = returnValue.Result[0].ADCNatureOfWork != null ? returnValue.Result[0].ADCNatureOfWork.ToString() : "";
                string ADCProvince = returnValue.Result[0].ADCProvince != null ? returnValue.Result[0].ADCProvince.ToString() : "";
                string ADCSuffix = returnValue.Result[0].ADCSuffix != null ? returnValue.Result[0].ADCSuffix.ToString() : "";
                string ADCValidIDNumber = returnValue.Result[0].ADCValidIDNumber != null ? returnValue.Result[0].ADCValidIDNumber.ToString() : "";
                string ADCValidIDPresented = returnValue.Result[0].ADCValidIDPresented != null ? returnValue.Result[0].ADCValidIDPresented.ToString() : "";
                string ADCZipCode = returnValue.Result[0].ADCZipCode != null ? returnValue.Result[0].ADCZipCode.ToString() : "";
                string Address = returnValue.Result[0].Address != null ? returnValue.Result[0].Address.ToString() : "";
                string Appointment = returnValue.Result[0].Appointment != null ? returnValue.Result[0].Appointment.ToString() : "";
                string ClientType = returnValue.Result[0].ClientType != null ? returnValue.Result[0].ClientType.ToString() : "";
                string GroupContactPerson = returnValue.Result[0].GroupContactPerson != null ? returnValue.Result[0].GroupContactPerson.ToString() : "";
                string GroupName = returnValue.Result[0].GroupName != null ? returnValue.Result[0].GroupName.ToString() : "";
                string IDPhoto = returnValue.Result[0].IDPhoto != null ? returnValue.Result[0].IDPhoto.ToString() : "";
                string Interests = returnValue.Result[0].Interests != null ? returnValue.Result[0].Interests.ToString() : "";

                PopulateRecordToTextBox(ADCCity, ADCClientId, ADCDateOfBirth, ADCEmailAddress, ADCEmployer, ADCFirstName, ADCLastName, ADCMiddleName, ADCMobileNumber, ADCNatureOfWork, ADCProvince, ADCSuffix, ADCValidIDNumber, ADCValidIDPresented, ADCZipCode, Address, Appointment, ClientType, GroupContactPerson, GroupName, IDPhoto, Interests);



            }
            else if(message == "Client already exists; Client cannot be referred anymore.")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "Swal.fire('Client already exists, Client cannot be referred anymore.');", true);
                return;
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "Swal.fire('Client already exists.');", true);
            }
        }
        catch (Exception ex)
        {

            SystemUtility.EventLog.SaveError(ex.ToString());
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "Swal.fire(`" + ex + "`); ", true);
            throw;
        }

    }
    #endregion

    #region POPULATE TEXT BOX 
    public void PopulateRecordToTextBox(string ADCCity, string ADCClientId, DateTime ADCDateOfBirth, string ADCEmailAddress, string ADCEmployer, string ADCFirstName, string ADCLastName, string ADCMiddleName, string ADCMobileNumber, string ADCNatureOfWork, string ADCProvince, string ADCSuffix, string ADCValidIDNumber, string ADCValidIDPresented, string ADCZipCode, string Address, string Appointment, string ClientType, string GroupContactPerson, string GroupName, string IDPhoto, string Interests)
    {
        ADCCity = ADCCity != null ? ADCCity.ToUpper() : "";
        ADCProvince = ADCProvince != null ? ADCProvince.ToUpper() : "";

        GetListCity(ADCProvince);

        if (DDcity.Items.FindByValue(ADCCity) != null)
        {
          
            DDcity.SelectedValue = ADCCity;
        }
        else
        {
            DDcity.SelectedValue = "Select";
        }
        birthDateTextBox.Value = Convert.ToDateTime(ADCDateOfBirth).ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
        emailAddress.Value = ADCEmailAddress;
        FirstName.Value = ADCFirstName;
        LastName.Value = ADCLastName;
        cellphoneNo.Value = ADCMobileNumber;
        if (DDProvince.Items.FindByValue(ADCProvince) != null)
        {
            DDProvince.SelectedValue = ADCProvince;
        }
        else
        {
            DDProvince.SelectedValue = "Select"; 
        }
        ZipCode.Value = ADCZipCode;
        fld_Address.Value = Address;
        fld_Appointments.Value = Appointment;
        ContactPerson.Value = GroupContactPerson;
        fld_GroupName.Value = GroupName;
        fld_Interests.Value = Interests;
    }
#endregion

    #region VALIDATE CLIENT REFERRAL
    public void ValidateClientReferral()
    {
        try
        {
            ValidateBasicInfo();

            if (groupRbtn.Checked == true)
            {
                if (getList.CheckIfADCClientExists(token) == true)
                {
                    ValidateRetrieveDetailsPerADCCLient();

                    bool checkClientReferralAgingValidation = getList.ClientReferralAgingValidation(token);
                    //if Existing Client
                    if (checkClientReferralAgingValidation == true)
                    {
                        VisibleInput();
                        //Existing Client with more than 90 days aging of client referral tran number (Action: Update Entry, Generate New Reference Number)
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
                        fld_Address.Value = retrieveIndividualRecord.Address;

                        if (DDProvince.Items.FindByValue(retrieveIndividualRecord.Province.ToUpper()) != null)
                        {
                            DDProvince.SelectedValue = retrieveIndividualRecord.Province.ToUpper();
                        }
                        else
                        {
                            DDProvince.SelectedValue = "Select";
                        }

                        GetListCity(retrieveIndividualRecord.Province);

                        if (DDcity.Items.FindByValue(retrieveIndividualRecord.City.ToUpper()) != null)
                        {

                            DDcity.SelectedValue = retrieveIndividualRecord.City.ToUpper();
                        }
                        else
                        {
                            DDcity.SelectedValue = "Select";
                        }


                        ZipCode.Value = retrieveIndividualRecord.ZipCode;
                        fld_Interests.Value = retrieveIndividualRecord.Interests;
                        fld_Appointments.Value = retrieveIndividualRecord.Appointments;


                    }
                    //Existing Client with less than 90 days aging of client referral tran number Action: No Action Needed. Collapse all panel)
                    else if (checkClientReferralAgingValidation == false)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "Swal.fire('Client already exists.');", true);
                        LastName.Value = string.Empty;
                        FirstName.Value = string.Empty;
                        birthDateTextBox.Value = string.Empty;
                        fld_GroupName.Value = string.Empty;
                        ContactPerson.Value = string.Empty;
                    }
                }
                else
                {
                    VisibleInput();
                }
            }
            else if (individualRbtn.Checked == true)
            {

                //if Existing Client
                if (getList.CheckIfADCClientExists(token) == true)
                {
                    ValidateRetrieveDetailsPerADCCLient();

                    bool checkClientReferralAgingValidation = getList.ClientReferralAgingValidation(token);

                    //Existing Client with more than 90 days aging of client referral tran number (Action: Update Entry, Generate New Reference Number)
                    if (checkClientReferralAgingValidation == true)
                    {
                        VisibleInput();

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
                        fld_Address.Value = retrieveIndividualRecord.Address;

                        if (DDProvince.Items.FindByValue(retrieveIndividualRecord.Province.ToUpper()) != null)
                        {
                            DDProvince.SelectedValue = retrieveIndividualRecord.Province.ToUpper();
                        }
                        else
                        {
                            DDProvince.SelectedValue = "Select";
                        }

                        GetListCity(retrieveIndividualRecord.Province);

                        if (DDcity.Items.FindByValue(retrieveIndividualRecord.City.ToUpper()) != null)
                        {

                            DDcity.SelectedValue = retrieveIndividualRecord.City.ToUpper();
                        }
                        else
                        {
                            DDcity.SelectedValue = "Select";
                        }

                        ZipCode.Value = retrieveIndividualRecord.ZipCode;
                        fld_Interests.Value = retrieveIndividualRecord.Interests;
                        fld_Appointments.Value = retrieveIndividualRecord.Appointments;

                    }
                    //Existing Client with less than 90 days aging of client referral tran number Action: No Action Needed. Collapse all panel)
                    else if (checkClientReferralAgingValidation == false)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "Swal.fire('Client already exists.');", true);
                        LastName.Value = string.Empty;
                        FirstName.Value = string.Empty;
                        birthDateTextBox.Value = string.Empty;
                    }

                }
                //New Client    
                else
                {
                    VisibleInput();
                }
            }
        }
        catch (Exception ex)
        {
            SystemUtility.EventLog.SaveError(ex.ToString());
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "Swal.fire(`" + ex + "`); ", true);
            throw;
        }

    }
    #endregion

    #region VALIDATE RETRIEVE DETAILS PER CLIENT
    public void ValidateRetrieveDetailsPerADCCLient()
    {
        token.Token = generateToken.GenerateTokenAuth();
        TokenRequest retrieveDetailsPerADCClientList;
        retrieveDetailsPerADCClientList = getList.RetrieveDetailsPerADCClient(token);
        //Getting of Client ID
        string ID = "";
        ID = retrieveDetailsPerADCClientList.ClientID;
        token.Token = generateToken.GenerateTokenAuth();
        token.ClientID = ID;
    }
    #endregion

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (sessionLength == 17)
            {
                SaveClientReferral();
            }
            if (sessionLength == 25)
            {
                SaveAgentReferral();
            }
        }
        catch (Exception ex) 
        {
            SystemUtility.EventLog.SaveError(ex.ToString());
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "Swal.fire(`" + ex + "`); ", true);
            throw;
        }
    }

    #region SAVE AGENT REFERRAL
    public void SaveAgentReferral()
    {
        try
        {
            FieldValidation();
        }
        catch (Exception ex) 
        {
            SystemUtility.EventLog.SaveError(ex.ToString());
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "Swal.fire(`" + ex + "`); ", true);
            throw;
        }
    }
    #endregion

    #region SAVE CLIENT REFERRAL
    public void SaveClientReferral()
    {
        try
        {
            ValidateBasicInfo();
            FieldValidation();
        }
        catch (Exception)
        {

            throw;
        }



    }
    #endregion

    #region PROCESS SAVING INFORMATION
    public void SavingInformation()
    {
        try
        {
            if (sessionLength == 17)
            {
                SavingClientReferralInformation();
            }
            if (sessionLength == 25)
            {
                SavingAgentReferralInformation();
            }
        }
        catch (Exception)
        {

            throw;
        }
    }
    #endregion

    #region FIELD VALIDATION
    public void FieldValidation()
    {
        if (string.IsNullOrEmpty(emailAddress.Value))
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "Swal.fire('Email Address is required.'); ", true);
            return;
        }
        if (string.IsNullOrEmpty(cellphoneNo.Value))
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "Swal.fire('Cell Phone Number is required.'); ", true);
            return;
        }
        if (string.IsNullOrEmpty(fld_Address.Value))
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "Swal.fire('Address is required.'); ", true);
            return;
        }
        if (DDProvince.SelectedValue == "Select")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "Swal.fire('Province is required.'); ", true);
            return;
        }
        if (DDcity.SelectedValue == "Select")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "Swal.fire('City is required.'); ", true);
            return;
        }
        if (string.IsNullOrEmpty(ZipCode.Value))
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "Swal.fire('Zip Code is required.'); ", true);
            return;
        }
        if (string.IsNullOrEmpty(fld_Interests.Value))
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "Swal.fire('Interests is required.'); ", true);
            return;
        }
        if (string.IsNullOrEmpty(fld_Appointments.Value))
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "Swal.fire('Appointments is required.'); ", true);
            return;
        }
        //if (!PhotoUpload.HasFile)
        //{
        //    string ex = "No file uploaded.";
        //    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "Swal.fire(`" + ex + "`); ", true);
        //    return;
        //}
        if (dataPrivacyCheckbox.Checked == false)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "Swal.fire('Please read and check our Data Privacy policy before proceeding.');", true);
            return;
        }
        else
        {
            SavingInformation();
        }
    }
    #endregion

    #region SAVING OF AGENT REFERRAL MODULE
    public void SavingAgentReferralInformation()
    {
        try
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
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "Swal.fire(`" + strMessage + "`); ", true);

                        clearphotoupload();
                    }
                    else
                    {
                        if (fileSizeActual <= fileSizeLimit)
                        {
                            string birthdate = "";

                            birthdate = DateTime.Parse(birthDateTextBox.Value.Trim()).ToString("MM/dd/yyyy");

                            agentReferralRequest.Token = generateToken.GenerateTokenAuth();
                            agentReferralRequest.ADCCity = DDcity.SelectedValue.ToString();
                            agentReferralRequest.ADCEmailAddress = emailAddress.Value.ToString();
                            agentReferralRequest.ADCMobileNumber = cellphoneNo.Value.ToString();

                            //PhotoSaving
                            using (Stream s = PhotoUpload.PostedFile.InputStream)
                            {
                                using (BinaryReader br = new BinaryReader(s))
                                {
                                    byte[] Databytes = br.ReadBytes((Int32)s.Length);
                                    agentReferralRequest.ADCPhoto = Convert.ToBase64String(Databytes);

                                }
                            }
                            agentReferralRequest.ADCProvince = DDProvince.SelectedValue.ToString();
                            agentReferralRequest.ADCZipCode = ZipCode.Value.ToString();
                            agentReferralRequest.Address = fld_Address.Value.ToString();
                            agentReferralRequest.Appointment = fld_Appointments.Value.ToString();
                            agentReferralRequest.AreaCode = null;
                            agentReferralRequest.Birthdate = birthdate;
                            agentReferralRequest.BranchCode = null;
                            agentReferralRequest.BranchName = null;
                            agentReferralRequest.ClientType = txt_clienttype.Value.ToString();
                            agentReferralRequest.CreatedBy = null;
                            agentReferralRequest.FirstName = FirstName.Value.ToString();
                            agentReferralRequest.GroupContactPerson = ContactPerson.Value.ToString();
                            agentReferralRequest.GroupName = fld_GroupName.Value.ToString();
                            agentReferralRequest.Interests = fld_Interests.Value.ToString();
                            agentReferralRequest.LastName = LastName.Value.ToString();
                            agentReferralRequest.PlatformKey = ConfigurationManager.AppSettings["CLIBAPIKey"];
                            agentReferralRequest.RTNCode = rtnLbl.Value.ToString();
                            agentReferralRequest.Region = null;


                            var returnValue = getList.IQRSaveClientReferral(agentReferralRequest);
                            string message = returnValue.Message;

                            if (returnValue.Message == "Client Referral Data Saved successfully")
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", @"
                                            Swal.fire({
                                                title: 'Transaction Successfully Created',
                                                showConfirmButton: false,
                                                timer: 5000
                                            }).then(() => {
                                                window.location = 'ClientReferral.aspx';
                                            });
                                        ", true);

                                clearphotoupload();
                            }
                            else
                            {
                                Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "Swal.fire(`" + returnValue.Message + "`); ", true);
                                clearphotoupload();
                                return;
                            }
                        }
                        else if (fileSizeActual > fileSizeLimit)
                        {
                            strMessage = "Allowed attachment file size is up to 3MB per file.";
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "Swal.fire(`" + strMessage + "`); ", true);
                            clearphotoupload();
                        }
                    }
                }
                else
                {
                    strMessage = @"Invalid file format. Allowed formats are as follows: \n";
                    strMessage += "1. Image (*.jpg, *.jpeg)";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "Swal.fire(`" + strMessage + "`);", true);
                    clearphotoupload();
                }
            }
            else
            {
                strMessage = "Photo is required.";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "Swal.fire(`" + strMessage + "`);", true);
                clearphotoupload();
            }


        }
        catch (Exception ex)
        {
            SystemUtility.EventLog.SaveError(ex.ToString());
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "Swal.fire(`" + ex + "`); ", true);
            throw;
        }
    }
    #endregion


    public void clearphotoupload()
    {
        lblImageName.Visible = false;
        lblImageName.Text = null;
        HiddenFileName.Value = null;
        Session["FileUpload1"] = null;
        fileLabelText.InnerText = "* Choose a file JPEG, JPG and up to 3MB";
    }

    #region SAVING OF CLIENT REFERRAL MODULE
    public void SavingClientReferralInformation()
    {
        if (groupRbtn.Checked == true)
        {
            try
            {
                GroupSaving();
            }
            catch (Exception ex)
            {
                SystemUtility.EventLog.SaveError(ex.ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "Swal.fire(`" + ex + "`); ", true);
                throw;
            }
        }
        else if (individualRbtn.Checked == true)
        {
            try
            {
               IndividualSaving();
            }
            catch (Exception ex)
            {
                SystemUtility.EventLog.SaveError(ex.ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "Swal.fire(`" + ex + "`); ", true);
                throw;
            }
        }
    }
    #endregion

    #region GROUP SAVING
    public void GroupSaving()
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

                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "Swal.fire(`" + strMessage + "`); ", true);

                        clearphotoupload();
                    }
                    else
                    {
                        if (fileSizeActual <= fileSizeLimit)
                        {
                            //ADC Client Tran
                            token.Type = "1";
                            token.GroupContactPerson = ContactPerson.Value;
                            token.GroupName = fld_GroupName.Value;
                            token.FirstName = FirstName.Value;
                            token.LastName = LastName.Value;
                            token.DOB = birthDateTextBox.Value;
                            token.Address = fld_Address.Value;
                            token.City = DDcity.SelectedValue.ToString();
                            token.Province = DDProvince.SelectedValue.ToString();
                            token.ZipCode = ZipCode.Value;
                            token.Email = emailAddress.Value;
                            token.ContactNumber = cellphoneNo.Value;
                            token.Region = regionLbl.Value.ToString();
                            token.AreaCode = areaCodeLbl.Value.ToString();
                            token.BranchCode = branchCodeLbl.Value.ToString();
                            token.BranchName = branchNameLbl.Value.ToString();
                            token.AgentID = branchCodeLbl.Value.ToString();
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
                            token.ReferenceCode = rtnLbl.Value.ToString();
                            token.Interests = fld_Interests.Value;
                            token.Appointments = fld_Appointments.Value;
                            token.BranchCode = branchCodeLbl.Value.ToString();
                            token.Token = generateToken.GenerateTokenAuth();
                            TokenRequest clientReferralTran;
                            clientReferralTran = getList.ClientReferralTran(token);

                            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", @"
                                            Swal.fire({
                                                title: 'Transaction Successfully Created',
                                                showConfirmButton: false,
                                                timer: 5000
                                            }).then(() => {
                                                window.location = 'ClientReferral.aspx';
                                            });
                                        ", true);

                            clearphotoupload();
                        }
                        else if (fileSizeActual > fileSizeLimit)
                        {
                            strMessage = "Allowed attachment file size is up to 3MB per file.";

                            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "Swal.fire(`" + strMessage + "`); ", true);

                            clearphotoupload();
    
                        }
                    }
                }
                else
                {
                    strMessage = @"Invalid file format. Allowed formats are as follows: \n";
                    strMessage += "1. Image (*.jpg, *.jpeg)";

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "Swal.fire(`" + strMessage + "`); ", true);

                    clearphotoupload();
    
                }

                //ClientSideScript.Alert("Client is successfully added.");
            }
            else
            {
                strMessage = "Photo ID is required.";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "Swal.fire(`" + strMessage + "`); ", true);

                clearphotoupload();

            }
        }
        else
        {
            token.Type = "2";
            token.FirstName = FirstName.Value;
            token.LastName = LastName.Value;
            token.DOB = birthDateTextBox.Value;


            token.Address = fld_Address.Value;
            token.City = DDcity.SelectedValue.ToString();
            token.Province = DDProvince.SelectedValue.ToString();
            token.ZipCode = ZipCode.Value;
            token.Email = emailAddress.Value;
            token.ContactNumber = cellphoneNo.Value;
            token.Region = regionLbl.Value.ToString();
            token.AreaCode = areaCodeLbl.Value.ToString();
            token.BranchCode = branchCodeLbl.Value.ToString();
            token.BranchName = branchNameLbl.Value.ToString();
            token.AgentID = branchCodeLbl.Value.ToString();
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
            token.ReferenceCode = rtnLbl.Value.ToString();
            token.Interests = fld_Interests.Value;
            token.Appointments = fld_Appointments.Value;
            token.BranchCode = branchCodeLbl.Value.ToString();
            token.Token = generateToken.GenerateTokenAuth();
            TokenRequest clientReferralTran;
            clientReferralTran = getList.ClientReferralTran(token);


            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", @"
                                            Swal.fire({
                                                title: 'Transaction Successfully Created',
                                                showConfirmButton: false,
                                                timer: 5000
                                            }).then(() => {
                                                window.location = 'ClientReferral.aspx';
                                            });
                                        ", true);

            clearphotoupload();
        }
    }

    #endregion

    #region INDIVIDUAL SAVING
    public void IndividualSaving()
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
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "Swal.fire(`" + strMessage + "`); ", true);
                        clearphotoupload();
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

                            token.Address = fld_Address.Value;
                            token.City = DDcity.SelectedValue.ToString();
                            token.Province = DDProvince.SelectedValue.ToString();
                            token.ZipCode = ZipCode.Value;
                            token.Email = emailAddress.Value;
                            token.ContactNumber = cellphoneNo.Value;
                            token.Region = regionLbl.Value.ToString();
                            token.AreaCode = areaCodeLbl.Value.ToString();
                            token.BranchCode = branchCodeLbl.Value.ToString();
                            token.BranchName = branchNameLbl.Value.ToString();
                            token.AgentID = branchCodeLbl.Value.ToString();
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
                            token.ReferenceCode = rtnLbl.Value.ToString();
                            token.Interests = fld_Interests.Value;
                            token.Appointments = fld_Appointments.Value;
                            token.BranchCode = branchCodeLbl.Value.ToString();
                            token.Token = generateToken.GenerateTokenAuth();
                            TokenRequest clientReferralTran;
                            clientReferralTran = getList.ClientReferralTran(token);



                            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", @"
                                            Swal.fire({
                                                title: 'Transaction Successfully Created',
                                                showConfirmButton: false,
                                                timer: 5000
                                            }).then(() => {
                                                window.location = 'ClientReferral.aspx';
                                            });
                                        ", true);

                            clearphotoupload();
                        }
                        else if (fileSizeActual > fileSizeLimit)
                        {
                            strMessage = "Allowed attachment file size is up to 3MB per file.";
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "Swal.fire(`" + strMessage + "`); ", true);

                            clearphotoupload();
                        }
                    }
                }
                else
                {
                    strMessage = @"Invalid file format. Allowed formats are as follows: \n";
                    strMessage += "1. Image (*.jpg, *.jpeg)";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "Swal.fire(`" + strMessage + "`); ", true);


                    clearphotoupload();
                }

                //ClientSideScript.Alert("Client is successfully added.");
            }
            else
            {
                strMessage = "Photo is required.";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "Swal.fire(`" + strMessage + "`); ", true);

                clearphotoupload();
            }


        }
        else
        {
            token.Type = "2";
            token.FirstName = FirstName.Value;
            token.LastName = LastName.Value;
            token.DOB = birthDateTextBox.Value;


            token.Address = fld_Address.Value;
            token.City = DDcity.SelectedValue.ToString();
            token.Province = DDProvince.SelectedValue.ToString();
            token.ZipCode = ZipCode.Value;
            token.Email = emailAddress.Value;
            token.ContactNumber = cellphoneNo.Value;
            token.Region = regionLbl.Value.ToString();
            token.AreaCode = areaCodeLbl.Value.ToString();
            token.BranchCode = branchCodeLbl.Value.ToString();
            token.BranchName = branchNameLbl.Value.ToString();
            token.AgentID = branchCodeLbl.Value.ToString();
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
            token.ReferenceCode = rtnLbl.Value.ToString();
            token.Interests = fld_Interests.Value;
            token.Appointments = fld_Appointments.Value;
            token.BranchCode = branchCodeLbl.Value.ToString();
            token.Token = generateToken.GenerateTokenAuth();
            TokenRequest clientReferralTran;
            clientReferralTran = getList.ClientReferralTran(token);

            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", @"
                                            Swal.fire({
                                                title: 'Transaction Successfully Created',
                                                showConfirmButton: false,
                                                timer: 5000
                                            }).then(() => {
                                                window.location = 'ClientReferral.aspx';
                                            });
                                        ", true);

            clearphotoupload();
        }
    }

    #endregion

    protected void dataPrivacyCheckbox_CheckedChanged(object sender, EventArgs e)
    {
        //if (dataPrivacyCheckbox.Checked)
        //{
        //    btnSave.Visible = true;
         
        //}
        //else
        //{
        //    btnSave.Visible = false;
          
        //}
    }

}