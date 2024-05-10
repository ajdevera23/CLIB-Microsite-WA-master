/*CHANGE LOG:
20201201 - addition of guardian details*/

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json.Linq;
using InsuranceWS;
using Newtonsoft.Json;
using System.Globalization;
using System.Web.UI.HtmlControls;
using WebCaptchaLib;
using System.Text;
//using waCLIBGInsureProject.Models;

public partial class EnrollmentPage2 : System.Web.UI.Page
{
    #region Initialization

    string productCode;
    string partnerCode;
    string orderId;
    string referenceCode;
    string voucherCode;
    string referenceNumber;
    string categoryCode;
    BeneficiaryCollection beneficiaryCollection = new BeneficiaryCollection();
    CustomerDetails customerDetails = new CustomerDetails();
    GeneralDetails generalDetails = new GeneralDetails();
    GuardianDetails guardianDetails = new GuardianDetails();
    ProcessTransactionRequest processTransactionRequest = new ProcessTransactionRequest();
    ProcessTransaction processTransaction = new ProcessTransaction();
    ProcessTransactionResult processTransactionResult = new ProcessTransactionResult();
    GenerateToken generateToken = new GenerateToken();
    VerifyFields verifyFields = new VerifyFields();
    BaseResult baseResult = new BaseResult();
    TokenRequest token = new TokenRequest();
    GetList getList = new GetList();
    BaseResult guardianVerifyFields = new BaseResult();
    IList<String> partnerList;
    IList<String> productList;


    PurchaseRequest purchaserequest = new PurchaseRequest();
    PurchaseRequest purchaseresult = new PurchaseRequest();

    PurchaseResult purchaseResult = new PurchaseResult();

    Nationality Nationality = new Nationality();
   

    private SelectionListRequest selectionlistrequest = new SelectionListRequest();
    private SelectionResult selectionlistresult = new SelectionResult();

    private DependentRequest dependentrequest = new DependentRequest();
    private DependentResult dependentresult = new DependentResult();

    List<DependentCollection> dependentCollectionList = new List<DependentCollection>();



    bool firstnamehasvalue = true;
    bool lastnamehasvalue = true;
    bool birthdayhasvalue = true;
    bool allhasvalue = false;

    #endregion

    #region Page_Load

    protected void Page_Load(object sender, EventArgs e)
    {
        
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.Cache.SetNoStore();

        token.Token = generateToken.GenerateTokenAuth();
        partnerList = getList.GetPartnerList(token);
        productList = getList.GetProductList(token);

        sessionfields();

        if (!IsPostBack)
        {
            
            generateCaptcha();
            postload();

            if (Session["IsExist"].ToString() == "True")
            {
                GetListProvince();
                GetListRelation();
            }
            if (Session["IsExist"].ToString() == "False" || Session["IsExist"].ToString() == "True")
            {
                GetListGender();
                GetListCivilStatus();
                GetListProvince();
                GetListValidID();
                GetListRelation();
                GetListSourceOfFunds();
                GetNationalities();
                GetListNatureOfWork();
            }
            if(Session["categoryCode"].ToString() == "PRI")
            {
                FirePropertyField();
            }
            if (Session["categoryCode"].ToString() == "FMI")
            {
                FamilyPlusField();
            }

        }
        if (IsPostBack)
        {
            if (DD_TypeOfDependents.SelectedValue != "0" && Session["categoryCode"].ToString() == "FMI")
            {
                InitializeDynamicControls();
            }

        }

        submitBtn.ServerClick += SubmitBtn_Click;
    }

    #endregion

    #region POST LOAD
    public void postload()
    {
        if (!string.IsNullOrEmpty(Session["productCode"] as string))
        {
            if (!string.IsNullOrEmpty(Session["partnerCode"] as string))
            {
                partnerCode = Session["partnerCode"].ToString();
                productCode = Session["productCode"].ToString();

                if (!string.IsNullOrEmpty(Session["referenceCode"] as string) && !string.IsNullOrEmpty(Session["voucherCode"] as string))
                {

                    token.ReferenceCode = Session["referenceCode"].ToString();
                    voucherCode = Session["voucherCode"].ToString();

                    if (getList.ifReferenceCodeExists(token) == false)
                    {
                        if (voucherCode != ConfigurationManager.AppSettings["CLIBvoucherCode"] || voucherCode != ConfigurationManager.AppSettings["CLIBvoucherCode1"]
                            || voucherCode != ConfigurationManager.AppSettings["CLIBvoucherCode2"])
                        {
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Voucher code is already used. Please enter another voucher code.'); window.location='" + ConfigurationManager.AppSettings["ProductRegistration"] + "';", true);

                        }
                        else
                        {
                            partnerCode = Request.QueryString["PART"];
                            productCode = Request.QueryString["PROD"];

                            #region Verify PartnerCode and ProductCode if via URL query

                            if (System.Text.RegularExpressions.Regex.IsMatch(partnerCode, @"[^a-zA-Z0-9]"))
                            {
                                Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('" + "Invalid URL." + "'); window.location='" + ConfigurationManager.AppSettings["ProductRegistration"] + "';", true);
                            }
                            else if (System.Text.RegularExpressions.Regex.IsMatch(productCode, @"[^a-zA-Z0-9]"))
                            {
                                Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('" + "Invalid URL." + "'); window.location='" + ConfigurationManager.AppSettings["ProductRegistration"] + "';", true);
                            }
                            else if (partnerList.Contains(partnerCode) == false || productList.Contains(productCode) == false)
                            {
                                Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('" + "Invalid URL." + "'); window.location='" + ConfigurationManager.AppSettings["ProductRegistration"] + "';", true);
                            }
                            #endregion
                        }
                    }
                }
                else
                {
                    token.ReferenceNumber = Session["referenceNumber"].ToString();
                    if (token.ReferenceNumber == "CFHWS")
                    {
                        //do nothing 
                    }
                    else if (token.ReferenceNumber == "PROMO")
                    {
                        //do nothing 
                    }
                    else if (getList.ifReferenceNumberExists(token) == true)
                    {
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Reference number is already used.'); window.location='" + ConfigurationManager.AppSettings["ProductRegistration"] + "';", true);
                    }
                }
            }
            else
            {
                Response.Redirect(ConfigurationManager.AppSettings["ProductRegistration"].Trim(), false);
            }
        }
        else if (!string.IsNullOrEmpty(Session["voucherCode"] as string))
        {
            voucherCode = Session["voucherCode"] as string;
            token.ReferenceCode = ConfigurationManager.AppSettings["CLIBrefCode"];
            if (voucherCode == ConfigurationManager.AppSettings["CLIBvoucherCode"] || voucherCode == ConfigurationManager.AppSettings["CLIBvoucherCode1"] || voucherCode == ConfigurationManager.AppSettings["CLIBvoucherCode2"])
            {
                if (!string.IsNullOrEmpty(Request.QueryString["PART"]) && !string.IsNullOrEmpty(Request.QueryString["PROD"]))
                {
                    partnerCode = Request.QueryString["PART"];
                    productCode = Request.QueryString["PROD"];

                    #region Verify PartnerCode and ProductCode if via URL query
                    if (System.Text.RegularExpressions.Regex.IsMatch(partnerCode, @"[^a-zA-Z0-9]"))
                    {
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('" + "Invalid URL." + "'); window.location='" + ConfigurationManager.AppSettings["ProductRegistration"] + "';", true);
                    }
                    else if (System.Text.RegularExpressions.Regex.IsMatch(productCode, @"[^a-zA-Z0-9]"))
                    {
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('" + "Invalid URL." + "'); window.location='" + ConfigurationManager.AppSettings["ProductRegistration"] + "';", true);
                    }
                    else if (partnerList.Contains(partnerCode) == false || productList.Contains(productCode) == false)
                    {
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('" + "Invalid URL." + "'); window.location='" + ConfigurationManager.AppSettings["ProductRegistration"] + "';", true);
                    }
                    #endregion
                }
                else
                {
                    Page.Response.Redirect(Page.Request.Url.ToString(), false);
                }
            }
            else
            {
                Page.Response.Redirect(Page.Request.Url.ToString(), false);
            }
        }
        else
        {
            Response.Redirect(ConfigurationManager.AppSettings["ProductRegistration"].Trim(), false);
        }
        #region Get Partner Logo
        token.PartnerCode = partnerCode;
        string partnerImagePath = getList.GetPartnerImagePath(token);

        partnerImage.Style["background-image"] = ResolveUrl(ConfigurationManager.AppSettings["productImagePath"] + partnerImagePath);
        //partnerImage.Attributes["src"] = ResolveUrl(ConfigurationManager.AppSettings["productImagePath"] + partnerImagePath);
        #endregion

        #region Get Product Logo
        token.ProductCode = productCode;
        string productImagePath = getList.GetProductImagePath(token);
        //productImage.Attributes["src"] = ResolveUrl(System.Configuration.ConfigurationManager.AppSettings["productImagePath"] + productImagePath);
        productImage.Style["background-image"] = ResolveUrl(System.Configuration.ConfigurationManager.AppSettings["productImagePath"] + productImagePath);
        #endregion

        #region Get Category Code
        categoryCode = getList.GetCategory(token);

        if (categoryCode == ConfigurationManager.AppSettings["VHI"])
        {
            vehicleInfoBody.Visible = true;
            vehicleInfoHead.Visible = true;
        }
        else
        {
            vehicleInfoBody.Visible = false;
            vehicleInfoHead.Visible = false;
        }
        #endregion


     
    }
    #endregion

    #region GENERATE CAPTCHA
    public void generateCaptcha()
    {
        #region Captcha
        if (captchaText.Value == "")
        {
            WebCaptcha.GenerateCaptcha(captchaImage, HttpContext.Current);
        }
        #endregion

    }
    #endregion

    #region SESSION FIELD FROM CHECK ELIGIBILITY PAGE
    public void sessionfields()
    {
        firstName.Value = Session["firstName"].ToString();
        middleName.Value = Session["middleName"].ToString();
        lastName.Value = Session["lastName"].ToString();
        suffix.Value = Session["suffix"].ToString();
        birthDateTextBox.Text = Session["DateOfBirth"].ToString();
    }
    #endregion
   
    #region FIRE PROPERTY FIELD
    public void FirePropertyField()
    {
        if (Session["categoryCode"].ToString() == "PRI")
        {
            GetOwnershipStatus();
            GetNatureOfOccupancy();
            GetTypeOfHome();
            GetTypeExteriorWalls();
            GetTypeRoof();
            GetTypeInnerPartitions();
            GetTypeBeams();
            GetTypeColumns();
            GetListProvinceFireProperty();
            GetPreviousLossStatus();
            GetIsThePropertyMortgaged();
        }

    }
    #endregion

    #region FAMILY PLUS FIELD
    public void FamilyPlusField()
    {
        if (Session["categoryCode"].ToString() == "FMI")
        {
            GetTyoeOfDependents();
        }
    }
    #endregion


    #region SubmitBtn_Click
    protected void SubmitBtn_Click(object sender, EventArgs e)
     {
        try
        {
            if (WebCaptcha.IsCaptchaCorrect(captchaText.Value.Trim(), HttpContext.Current))
            {
                SavingInformationValidation();
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "Swal.fire('Invalid captcha. Please try again.');", true);
            }
        }
        catch (FormatException ex)
        {
            SystemUtility.EventLog.SaveError(ex.ToString());
            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "Swal.fire('An unexpected error occured. Please contact CLIB and try again later.');", true);
        }
        catch (Exception ex)
        {
            SystemUtility.EventLog.SaveError(ex.ToString());
            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "Swal.fire('An unexpected error occured. Please contact CLIB and try again later.');", true);
        }
    }
    #endregion

    #region SavingInformationValidation
    public void SavingInformationValidation()
    {
        populatedatafromtextboxes();

        #region ADDRESS FIELDS
        if (string.IsNullOrEmpty(presentAddress.Text))
        {
            ClientScript.RegisterStartupScript(this.GetType(), "alert", "Swal.fire('Please Enter Address.');", true);
            return;
        }
        if (DDProvince.SelectedValue == "Select" || string.IsNullOrEmpty(DDProvince.SelectedValue))
        {
            ClientScript.RegisterStartupScript(this.GetType(), "alert", "Swal.fire('Please Select Province.');", true);
            return;
        }
        if (DDcity.SelectedValue == "Select" || string.IsNullOrEmpty(DDcity.SelectedValue))
        {
            ClientScript.RegisterStartupScript(this.GetType(), "alert", "Swal.fire('Please Select City.');", true);
            return;
        }
        if (string.IsNullOrEmpty(fld_Baranggay.Text))
        {
            ClientScript.RegisterStartupScript(this.GetType(), "alert", "Swal.fire('Please Enter Barangay.');", true);
            return;
        }
        if (string.IsNullOrEmpty(fld_ZipCode.Text))
        {
            ClientScript.RegisterStartupScript(this.GetType(), "alert", "Swal.fire('Please Enter Postal Code.');", true);
            return;
        }
        #endregion

        #region CONTACT INFORMATION FIELDS
        if (string.IsNullOrEmpty(contactNumber.Text) || contactNumber.Text.Length == 2)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "alert", "Swal.fire('Please Enter Contact No.');", true);
            return;
        }
        if (string.IsNullOrEmpty(emailAddress.Text))
        {
            ClientScript.RegisterStartupScript(this.GetType(), "alert", "Swal.fire('Please Enter Email Address.');", true);
            return;
        }
        #endregion

        #region IDENTIFICATION FIELDS
        if ((gender.SelectedValue == "Select" || string.IsNullOrEmpty(gender.SelectedValue)) && (Session["IsExist"].ToString() == "False" || Session["IsExist"].ToString() == "True"))
        {
            ClientScript.RegisterStartupScript(this.GetType(), "alert", "Swal.fire('Please Select Gender.');", true);
            return;
        }
        if ((string.IsNullOrEmpty(placeOfBirth.Text)) && (Session["IsExist"].ToString() == "False" || Session["IsExist"].ToString() == "True"))
        {
            ClientScript.RegisterStartupScript(this.GetType(), "alert", "Swal.fire('Please Enter Place of Birth.');", true);
            return;
        }
        if ((DDNationality.SelectedValue == "Select") && (Session["IsExist"].ToString() == "False" || Session["IsExist"].ToString() == "True"))
        {
            ClientScript.RegisterStartupScript(this.GetType(), "alert", "Swal.fire('Please Select Nationality.');", true);
            return;
        }
        if ((civilStatus.SelectedValue == "Select" || string.IsNullOrEmpty(civilStatus.SelectedValue)) && (Session["IsExist"].ToString() == "False" || Session["IsExist"].ToString() == "True"))
        {
            ClientScript.RegisterStartupScript(this.GetType(), "alert", "Swal.fire('Please Select Civil Status.');", true);
            return;
        }
        if ((DDSourceOfFunds.SelectedValue == "Select" || string.IsNullOrEmpty(DDSourceOfFunds.SelectedValue)) && (Session["IsExist"].ToString() == "False" || Session["IsExist"].ToString() == "True"))
        {
            ClientScript.RegisterStartupScript(this.GetType(), "alert", "Swal.fire('Please Select Source of Funds.');", true);
            return;
        }
        if ((validIdDropDownList.SelectedValue == "Select" || string.IsNullOrEmpty(validIdDropDownList.SelectedValue)) && (Session["IsExist"].ToString() == "False" || Session["IsExist"].ToString() == "True"))
        {
            ClientScript.RegisterStartupScript(this.GetType(), "alert", "Swal.fire('Please Select Valid ID.');", true);
            return;
        }
        if ((string.IsNullOrEmpty(idNumber.Text)) && (Session["IsExist"].ToString() == "False" || Session["IsExist"].ToString() == "True"))
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "Swal.fire('Please Enter ID No.');", true);
            return;
        }

        if ((fld_NatureOfWork.SelectedValue == "Select" || string.IsNullOrEmpty(fld_NatureOfWork.SelectedValue)) && (Session["IsExist"].ToString() == "False" || Session["IsExist"].ToString() == "True"))
        {
            ClientScript.RegisterStartupScript(this.GetType(), "alert", "Swal.fire('Please Select Nature of Work.');", true);
            return;
        }
        #endregion

        #region BENEFICIARY FIELDS
        if (string.IsNullOrEmpty(beneFullname.Text) && Session["categoryCode"].ToString() != "PRI")
        {
            ClientScript.RegisterStartupScript(this.GetType(), "alert", "Swal.fire('Please Enter Beneficiary Full Name.');", true);
            return;
        }
        if (relationshipDropDownList.SelectedValue == "Select" && Session["categoryCode"].ToString() != "PRI")
        {
            ClientScript.RegisterStartupScript(this.GetType(), "alert", "Swal.fire('Please Select Relationship to the Insured.');", true);
            return;
        }
        #endregion

        #region GUARDIAN FIELDS
        if (string.IsNullOrEmpty(guardianFullName.Text) && Session["IsMinor"].ToString() == "True" && Session["categoryCode"].ToString() != "PRI")
        {
            ClientScript.RegisterStartupScript(this.GetType(), "alert", "Swal.fire('Please Enter Guardian Full Name.');", true);
            return;
        }
        if (string.IsNullOrEmpty(guardianBirthDate.Text) && Session["IsMinor"].ToString() == "True" && Session["categoryCode"].ToString() != "PRI")
        {
            ClientScript.RegisterStartupScript(this.GetType(), "alert", "Swal.fire('Please Enter Guardian Birthdate.');", true);
            return;
        }
        if (guardianRelationshipDropDownList.SelectedValue == "Select" && Session["IsMinor"].ToString() == "True" && Session["categoryCode"].ToString() != "PRI")
        {
            ClientScript.RegisterStartupScript(this.GetType(), "alert", "Swal.fire('Please Select Guardian Relationship.');", true);
            return;
        }
        #endregion

        #region FIRE PROPERTY FIELDS 

        if (string.IsNullOrEmpty(fld_PropertyAddress.Text) && Session["categoryCode"].ToString() == "PRI")
        {
            ClientScript.RegisterStartupScript(this.GetType(), "alert", "Swal.fire('Please Enter Full Address of Property to be insured.');", true);
            return;
        }
        if (PDDLProvince.SelectedValue == "Select" && Session["categoryCode"].ToString() == "PRI")
        {
            ClientScript.RegisterStartupScript(this.GetType(), "alert", "Swal.fire('Please Select Province.');", true);
            return;
        }
        if (PDDLCity.SelectedValue == "Select" && Session["categoryCode"].ToString() == "PRI")
        {
            ClientScript.RegisterStartupScript(this.GetType(), "alert", "Swal.fire('Please Select City.');", true);
            return;
        }
        if (string.IsNullOrEmpty(fld_NameOfResident.Text) && Session["categoryCode"].ToString() == "PRI")
        {
            ClientScript.RegisterStartupScript(this.GetType(), "alert", "Swal.fire('Please Enter Name of Resident.');", true);
            return;
        }
        if (DD_OwnershipStatus.SelectedValue == "Select" && Session["categoryCode"].ToString() == "PRI")
        {
            ClientScript.RegisterStartupScript(this.GetType(), "alert", "Swal.fire('Please Select Ownership Status.');", true);
            return;
        }
        if (string.IsNullOrEmpty(fld_PropertyAge.Text) && Session["categoryCode"].ToString() == "PRI")
        {
            ClientScript.RegisterStartupScript(this.GetType(), "alert", "Swal.fire('Please Enter Age of Home.');", true);
            return;
        }
        if (string.IsNullOrEmpty(fld_FloorArea.Text) && Session["categoryCode"].ToString() == "PRI")
        {
            ClientScript.RegisterStartupScript(this.GetType(), "alert", "Swal.fire('Please Enter Floor area.');", true);
            return;
        }
        if (string.IsNullOrEmpty(fld_NoOfStoreys.Text) && Session["categoryCode"].ToString() == "PRI")
        {
            ClientScript.RegisterStartupScript(this.GetType(), "alert", "Swal.fire('Please Enter No. of Storeys.');", true);
            return;
        }
        if (DD_IsThePropertyMortgaged.SelectedValue == "Select" && Session["categoryCode"].ToString() == "PRI")
        {
            ClientScript.RegisterStartupScript(this.GetType(), "alert", "Swal.fire('Please Select property Mortgaged.');", true);
            return;
        }
        if (DD_IsThePropertyMortgaged.SelectedValue == "Yes" && string.IsNullOrEmpty(fld_MortgageeName.Text) && Session["categoryCode"].ToString() == "PRI")
        {
            ClientScript.RegisterStartupScript(this.GetType(), "alert", "Swal.fire('Please Enter Name of Mortgagee.');", true);
            return;
        }
        if (DD_NatureOfOccupancy.SelectedValue == "Select" && Session["categoryCode"].ToString() == "PRI")
        {
            ClientScript.RegisterStartupScript(this.GetType(), "alert", "Swal.fire('Please Select Nature of Occupancy.');", true);
            return;
        }
        if (DD_TypeOfHome.SelectedValue == "Select" && Session["categoryCode"].ToString() == "PRI")
        {
            ClientScript.RegisterStartupScript(this.GetType(), "alert", "Swal.fire('Please Select Type of Home.');", true);
            return;
        }
        if (DD_TypeExteriorWalls.SelectedValue == "Select" && Session["categoryCode"].ToString() == "PRI")
        {
            ClientScript.RegisterStartupScript(this.GetType(), "alert", "Swal.fire('Please Select Type of Exterior Walls.');", true);
            return;
        }
        if (DD_TypeExteriorWalls.Text == "Others: Please Specify" && string.IsNullOrEmpty(otherInputTypeExteriorWalls.Text))
        {
            ClientScript.RegisterStartupScript(this.GetType(), "alert", "Swal.fire('Please Specify Type of Exterior Walls');", true);
            return;
        }
        if (DD_TypeRoof.SelectedValue == "Select" && Session["categoryCode"].ToString() == "PRI")
        {
            ClientScript.RegisterStartupScript(this.GetType(), "alert", "Swal.fire('Please Select Type of Roofs.');", true);
            return;
        }
        if (DD_TypeRoof.Text == "Others: Please Specify" && string.IsNullOrEmpty(otherInputTypeRoof.Text))
        {
            ClientScript.RegisterStartupScript(this.GetType(), "alert", "Swal.fire('Please Specify Type of Roof');", true);
            return;
        }
        if (DD_TypeInnerPartitions.SelectedValue == "Select" && Session["categoryCode"].ToString() == "PRI")
        {
            ClientScript.RegisterStartupScript(this.GetType(), "alert", "Swal.fire('Please Select Type of Inner Partitions.');", true);
            return;
        }
        if (DD_TypeInnerPartitions.Text == "Others: Please Specify" && string.IsNullOrEmpty(otherInputTypeInnerPartitions.Text))
        {
            ClientScript.RegisterStartupScript(this.GetType(), "alert", "Swal.fire('Please Specify Type of Inner Partitions');", true);
            return;
        }
        if (DD_TypeBeams.SelectedValue == "Select" && Session["categoryCode"].ToString() == "PRI")
        {
            ClientScript.RegisterStartupScript(this.GetType(), "alert", "Swal.fire('Please Select Type of Beams.');", true);
            return;
        }
        if (DD_TypeBeams.Text == "Others: Please Specify" && string.IsNullOrEmpty(otherInputTypeBeams.Text))
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "Swal.fire('Please Specify Type of Beams');", true);
            return;
        }
        if (DD_TypeColumns.SelectedValue == "Select" && Session["categoryCode"].ToString() == "PRI")
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "Swal.fire('Please Select Type of Columns.');", true);
            return;
        }
        if (DD_TypeColumns.Text == "Others: Please Specify" && string.IsNullOrEmpty(otherInputTypeColumns.Text))
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "Swal.fire('Please Specify Type of Columns');", true);
            return;
        }
        if (string.IsNullOrEmpty(fld_BoundaryFront.Text) && Session["categoryCode"].ToString() == "PRI")
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "Swal.fire('Please Enter Front Boundary.');", true);
            return;
        }
        if (string.IsNullOrEmpty(fld_BoundaryRear.Text) && Session["categoryCode"].ToString() == "PRI")
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "Swal.fire('Please Enter Rear Boundary.');", true);
            return;
        }
        if (string.IsNullOrEmpty(fld_BoundaryLeft.Text) && Session["categoryCode"].ToString() == "PRI")
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "Swal.fire('Please Enter Left Boundary.');", true);
            return;
        }
        if (string.IsNullOrEmpty(fld_BoundaryRight.Text) && Session["categoryCode"].ToString() == "PRI")
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "Swal.fire('Please Enter Right Boundary.');", true);
            return;
        }
        if (DD_IsThereAnyPreviousLoss.SelectedValue == "Select" && Session["categoryCode"].ToString() == "PRI")
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "Swal.fire('Please Select any previous loss.');", true);
            return;
        }
        if (DD_IsThereAnyPreviousLoss.SelectedValue == "Yes" && string.IsNullOrEmpty(fld_Remarks.Text) && Session["categoryCode"].ToString() == "PRI")
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "Swal.fire('Please indicate date and detail of incident.');", true);
            return;
        }
        if (string.IsNullOrEmpty(fld_BodyOfWaterDistance.Text) && Session["categoryCode"].ToString() == "PRI")
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "Swal.fire('Please Enter Distance from bodies of water.');", true);
            return;
        }
        #endregion

        #region FAMILY FIELDS
        if (firstnamehasvalue == false && Session["categoryCode"].ToString() == "FMI")
        {
            ClientScript.RegisterStartupScript(this.GetType(), "alert", "Swal.fire('Please fill in all the required information for the dependent fields.');", true);
            return;
        }
        if (lastnamehasvalue == false && Session["categoryCode"].ToString() == "FMI")
        {
            ClientScript.RegisterStartupScript(this.GetType(), "alert", "Swal.fire('Please fill in all the required information for the dependent fields.');", true);
            return;
        }
        if (birthdayhasvalue == false && Session["categoryCode"].ToString() == "FMI")
        {
            ClientScript.RegisterStartupScript(this.GetType(), "alert", "Swal.fire('Please fill in all the required information for the dependent fields.');", true);
            return;
        }
        if (allhasvalue == false && Session["categoryCode"].ToString() == "FMI")
        {
            ClientScript.RegisterStartupScript(this.GetType(), "alert", "Swal.fire('Atleast 1 dependent is required');", true);
            return;
        }
        #endregion

        #region DATA PRIVACY / TERMS AND CONDITION
        if (dataPrivacy1Checkbox.Checked == false || dataPrivacy2Checkbox.Checked == false)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "Swal.fire('Please read and check our Data Privacy policy and Terms and conditions before proceeding.');", true);
            return;
        }
        if (!System.Text.RegularExpressions.Regex.IsMatch(firstName.Value.Trim(), @"^[A-Za-z0-9-' ]*$") ||
                 !System.Text.RegularExpressions.Regex.IsMatch(middleName.Value.Trim(), @"^[A-Za-z0-9-' ]*$") ||
                 !System.Text.RegularExpressions.Regex.IsMatch(lastName.Value.Trim(), @"^[A-Za-z0-9-' ]*$") ||
                 !System.Text.RegularExpressions.Regex.IsMatch(suffix.Value.Trim(), @"^$|^[A-Za-z0-9-., ]+$"))
        {
            ClientScript.RegisterStartupScript(this.GetType(), "alert", "Swal.fire('You have entered invalid information. Please try again.');", true);
            return;
        }
        #endregion

        #region SAVING INFORMATION / PURCHASE KYC
        else
        {
            PurchaseSavingInformation();
        }
        #endregion
    }
    #endregion

    #region CLIB MICROSITE SAVING CLIENT KYC
    public void PurchaseSavingInformation()
    {
        #region SAVING CLIENT INFORMATION

        #region CLIENT KYC INITIALIZATION
                string birthdate = "";
                string guardianbirthdate = "";
                string guardianrelationship = "";
                string beneficiaryrelationship = "";
                string natureofwork = "";
                string bodyOfWaterDistanceText = fld_BodyOfWaterDistance.Text.Trim();
                string floorAreaText = fld_FloorArea.Text.Trim();
                string noOfStoreysText = fld_NoOfStoreys.Text.Trim();
                string propertyAgeText = fld_PropertyAge.Text.Trim();

                Session["ProductName"].ToString();

                #region ISMINOR

                if (Session["IsMinor"].ToString() == "True" && Session["categoryCode"].ToString() != "PRI")
                        {
                            guardianbirthdate = DateTime.Parse(guardianBirthDate.Text.Trim()).ToString("MM/dd/yyyy");
                            guardianrelationship = guardianRelationshipDropDownList.SelectedValue.Trim();
                        }
                        else
                        {
                            if (guardianRelationshipDropDownList.SelectedValue == "Select")
                            {
                                guardianrelationship = null;
                            }

                            guardianBirthDate.Text = "";
                        }
                #endregion

                #region NATURE OF WORK
                if (fld_NatureOfWork.SelectedValue == "Select")
                {
                    natureofwork = null;
                }
                else
                {
                    natureofwork = fld_NatureOfWork.SelectedValue.ToString();
                }
                #endregion

                #region RELATIONSHIP EMPTY STRING
                if (relationshipDropDownList.SelectedValue == "Select")
                    {
                        beneficiaryrelationship = null;
                    }
                    else
                    {
                        beneficiaryrelationship = relationshipDropDownList.SelectedValue.ToString();
                    }

                    birthdate = DateTime.Parse(birthDateTextBox.Text.Trim()).ToString("MM/dd/yyyy");
                #endregion
        #endregion


        if (Session["categoryCode"].ToString() == "FMI")
        {
            #region MICROSITE FAMILY PRODUCTS
            FamilyRequest familyRequest = new FamilyRequest();
            token.Token = generateToken.GenerateTokenAuth();
            familyRequest.Token = token.Token;
            familyRequest.Address = presentAddress.Text.ToString();
            familyRequest.Barangay = fld_Baranggay.Text.ToString();
            familyRequest.BeneficiaryName = beneFullname.Text.ToString();
            familyRequest.BeneficiaryRelationship = beneficiaryrelationship;
            familyRequest.Birthdate = birthdate;
            familyRequest.CategoryCode = Session["categoryCode"].ToString(); ;
            familyRequest.City = DDcity.SelectedValue.ToString();
            familyRequest.CivilStatus = civilStatus.SelectedValue.ToString();
            familyRequest.DateTimeFormat = "MM/dd/yyyy";

            List<DependentCollection> deps = new List<DependentCollection>();
            familyRequest.DependentCollection = deps;

            for (int i = 0; i < dependentCollectionList.Count; i++)
            {
                if (!string.IsNullOrEmpty(dependentCollectionList[i].DependentFirstName) &&
                    !string.IsNullOrEmpty(dependentCollectionList[i].DependentLastName) &&
                    !string.IsNullOrEmpty(dependentCollectionList[i].DependentBirth))
                {
                    DependentCollection dep = new DependentCollection();

                    dep.DependentFirstName = dependentCollectionList[i].DependentFirstName;
                    dep.DependentLastName = dependentCollectionList[i].DependentLastName;
                    dep.DependentName = dependentCollectionList[i].DependentName;
                    dep.DependentBirth = dependentCollectionList[i].DependentBirth;
                    dep.DependentRelationship = dependentCollectionList[i].DependentRelationship;
                    dep.FirstnameID = dependentCollectionList[i].FirstnameID;
                    dep.LastnameID = dependentCollectionList[i].LastnameID;
                    dep.BirthID = dependentCollectionList[i].BirthID;


                    familyRequest.DependentCollection.Add(dep);

                }
            }

            familyRequest.EmailAddress = emailAddress.Text.ToString();
            familyRequest.EmployerBusinessName = fld_EmployerBusinessName.Text.ToString();
            familyRequest.FirstName = firstName.Value.ToString();
            familyRequest.Gender = gender.SelectedValue.ToString();
            familyRequest.GuardianBirthday = guardianbirthdate;
            familyRequest.GuardianContactNo = "";
            familyRequest.GuardianName = guardianFullName.Text.ToString();
            familyRequest.GuardianRelationship = guardianrelationship;
            familyRequest.IDNumber = idNumber.Text.ToString();
            familyRequest.IntegrationId = Convert.ToInt32(Session["integrationId"].ToString());
            familyRequest.IsExist = Convert.ToBoolean(Session["IsExist"].ToString());
            familyRequest.IsWithVoucher = Convert.ToBoolean(Session["IsWithVoucher"].ToString());
            familyRequest.LastName = lastName.Value.ToString();
            familyRequest.MiddleName = middleName.Value.ToString();
            familyRequest.MobileNumber = contactNumber.Text.ToString();
            familyRequest.Nationality = DDNationality.SelectedValue.ToString();
            familyRequest.NatureOfWork = natureofwork;
            familyRequest.NumberOfCOCs = 1;
            familyRequest.Occupation = fld_Occupation.Text.ToString();
            familyRequest.PartnerCode = Session["partnerCode"].ToString();
            familyRequest.PlaceOfBirth = placeOfBirth.Text.ToString();
            familyRequest.PlatformName = ConfigurationManager.AppSettings["CLIBPlatformName"];
            familyRequest.PlatformKey = ConfigurationManager.AppSettings["CLIBAPIKey"];
            familyRequest.ProductCode = Session["productCode"].ToString();
            familyRequest.ProviderCode = Session["providerCode"].ToString();
            familyRequest.Province = DDProvince.SelectedValue.ToString();
            familyRequest.ReferenceNumber = Session["voucherCode"].ToString();
            familyRequest.SourceOfFunds = DDSourceOfFunds.SelectedValue.ToString();
            familyRequest.Suffix = suffix.Value.ToString();
            familyRequest.TinId = fld_TinId.Text.ToString();
            familyRequest.UserId = ConfigurationManager.AppSettings["CLIBPlatformName"];
            familyRequest.ValidID = validIdDropDownList.SelectedValue.ToString();
            familyRequest.VoucherCode = Session["referenceCode"] != null ? Session["referenceCode"].ToString() : "";
            familyRequest.ZipCode = fld_ZipCode.Text.ToString();


            var returnValue = getList.IQRPurchaseFamily(familyRequest);
            string message = returnValue.Message;

            if (returnValue.Message == "Transaction Successful")
            {

                Session["cocNumber"] = returnValue.Result[0].COCNumber.ToString();

                string jsonResponse = JsonConvert.SerializeObject(returnValue);
                purchaseResult = JsonConvert.DeserializeObject<PurchaseResult>(jsonResponse);

                SMSContent(purchaseResult);


                Response.Redirect(ConfigurationManager.AppSettings["ThankYouPage"].Trim(), false);
                ClearFields(enrollmentForm);
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "Swal.fire(`" + returnValue.Message + "`); ", true);
            }

            #endregion

        }
        else
        {
            #region MICROSITE PRODUCTS & FIRE PROPERTY
            FirePropertyRequest firepropertyrequest = new FirePropertyRequest();
            token.Token = generateToken.GenerateTokenAuth();
            firepropertyrequest.Token = token.Token;
            firepropertyrequest.Address = presentAddress.Text.ToString();
            firepropertyrequest.Barangay = fld_Baranggay.Text.ToString();
            firepropertyrequest.BeneficiaryName = beneFullname.Text.ToString();
            firepropertyrequest.BeneficiaryRelationship = beneficiaryrelationship;
            firepropertyrequest.Birthdate = birthdate;
            firepropertyrequest.BodyOfWaterDistance = string.IsNullOrEmpty(bodyOfWaterDistanceText) ? 0 : Convert.ToDecimal(bodyOfWaterDistanceText);
            firepropertyrequest.BoundaryFront = fld_BoundaryFront.Text.ToString();
            firepropertyrequest.BoundaryLeft = fld_BoundaryLeft.Text.ToString();
            firepropertyrequest.BoundaryRear = fld_BoundaryRear.Text.ToString();
            firepropertyrequest.BoundaryRight = fld_BoundaryRight.Text.ToString();
            firepropertyrequest.CategoryCode = Session["categoryCode"].ToString();
            firepropertyrequest.City = DDcity.SelectedValue.Trim();
            firepropertyrequest.CivilStatus = civilStatus.SelectedValue.ToString();
            firepropertyrequest.DateTimeFormat = "MM/dd/yyyy";
            firepropertyrequest.EmailAddress = emailAddress.Text.ToString();
            firepropertyrequest.EmployerBusinessName = fld_EmployerBusinessName.Text.ToString();
            firepropertyrequest.FirstName = firstName.Value.ToString();
            firepropertyrequest.FloorArea = string.IsNullOrEmpty(floorAreaText) ? 0 : Convert.ToDecimal(floorAreaText);
            firepropertyrequest.Gender = gender.SelectedValue.ToString();
            firepropertyrequest.GuardianBirthday = guardianbirthdate;
            firepropertyrequest.GuardianContactNo = "";
            firepropertyrequest.GuardianName = guardianFullName.Text.ToString();
            firepropertyrequest.GuardianRelationship = guardianrelationship;
            firepropertyrequest.IDNumber = idNumber.Text.ToString();
            firepropertyrequest.IntegrationId = Convert.ToInt32(Session["integrationId"].ToString());
            firepropertyrequest.IsExist = Convert.ToBoolean(Session["IsExist"].ToString());
            firepropertyrequest.IsWithVoucher = Convert.ToBoolean(Session["IsWithVoucher"].ToString());
            firepropertyrequest.LastName = lastName.Value.ToString();
            firepropertyrequest.LossDetails = fld_Remarks.Text.ToString();
            firepropertyrequest.MiddleName = middleName.Value.ToString();
            firepropertyrequest.MobileNumber = contactNumber.Text.ToString();
            firepropertyrequest.MortgageeName = fld_MortgageeName.Text.ToString();
            firepropertyrequest.NameOfResident = fld_NameOfResident.Text.ToString();
            firepropertyrequest.Nationality = DDNationality.SelectedValue.ToString();
            firepropertyrequest.NatureOfWork = natureofwork;
            firepropertyrequest.NatureOfOccupancy = DD_NatureOfOccupancy.SelectedValue.ToString();
            firepropertyrequest.NoOfStoreys = string.IsNullOrEmpty(noOfStoreysText) ? 0 : Convert.ToInt64(noOfStoreysText);
            firepropertyrequest.NumberOfCOCs = 1;
            firepropertyrequest.Occupation = fld_Occupation.Text.ToString();
            firepropertyrequest.OwnershipStatus = DD_OwnershipStatus.SelectedValue.ToString();
            firepropertyrequest.PartnerCode = Session["partnerCode"].ToString();
            firepropertyrequest.PlaceOfBirth = placeOfBirth.Text.ToString();
            firepropertyrequest.PlatformKey = ConfigurationManager.AppSettings["CLIBAPIKey"];
            firepropertyrequest.PlatformName = ConfigurationManager.AppSettings["CLIBPlatformName"];
            firepropertyrequest.PreviousLossStatus = DD_IsThereAnyPreviousLoss.SelectedValue.ToString();
            firepropertyrequest.ProductCode = Session["productCode"].ToString();
            firepropertyrequest.PropertyAddress = fld_PropertyAddress.Text.ToString();
            firepropertyrequest.PropertyProvince = PDDLProvince.SelectedValue.ToString();
            firepropertyrequest.PropertyCity = PDDLCity.SelectedValue.ToString();
            firepropertyrequest.PropertyAge = string.IsNullOrEmpty(propertyAgeText) ? 0 : Convert.ToInt64(propertyAgeText);
            firepropertyrequest.PropertyMortgageStatus = DD_IsThePropertyMortgaged.SelectedValue.ToString();
            firepropertyrequest.ProviderCode = Session["providerCode"].ToString();
            firepropertyrequest.Province = DDProvince.SelectedValue.ToString();
            firepropertyrequest.ReferenceNumber = Session["voucherCode"].ToString();
            firepropertyrequest.SourceOfFunds = DDSourceOfFunds.SelectedValue.ToString();
            firepropertyrequest.Suffix = suffix.Value.Trim();
            firepropertyrequest.TinId = fld_TinId.Text.ToString();
            firepropertyrequest.TypeBeams = GetTypeBeamsSelectedValue();
            firepropertyrequest.TypeColumns = GetTypeColumnsSelectedValue();
            firepropertyrequest.TypeExteriorWalls = GetTypeExteriorWallsSelectedValue();
            firepropertyrequest.TypeInnerPartitions = GetTypeInnerPartitionsSelectedValue();
            firepropertyrequest.TypeOfHome = DD_TypeOfHome.SelectedValue.ToString();
            firepropertyrequest.TypeRoof = GetTypeRoofSelectedValue();
            firepropertyrequest.UserId = ConfigurationManager.AppSettings["CLIBPlatformName"];
            firepropertyrequest.ValidID = validIdDropDownList.SelectedValue.Trim();
            firepropertyrequest.VoucherCode = Session["referenceCode"] != null ? Session["referenceCode"].ToString() : "";
            firepropertyrequest.ZipCode = fld_ZipCode.Text.ToString();

            var returnValue = getList.IQRPurchase(firepropertyrequest);
            string message = returnValue.Message;

            if (returnValue.Message == "Transaction Successful")
            {

                Session["cocNumber"] = returnValue.Result[0].COCNumber.ToString();

                string jsonResponse = JsonConvert.SerializeObject(returnValue);
                purchaseResult = JsonConvert.DeserializeObject<PurchaseResult>(jsonResponse);

                SMSContent(purchaseResult);


                Response.Redirect(ConfigurationManager.AppSettings["ThankYouPage"].Trim(), false);
                ClearFields(enrollmentForm);
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "Swal.fire(`" + returnValue.Message + "`); ", true);
            }

            #endregion
        }


        #endregion
    }
    #endregion


    #region SEND SMS
    public void SMSContent(PurchaseResult purchaseResult)
    {
        try
        {
            string smsproductnamelabel = Session["ProductName"].ToString();
            string smsfirstname = firstName.Value.ToString();

            string smsReferenceCode = Session["voucherCode"].ToString();

            foreach (var cocItems in purchaseResult.Result)
            {
                string messageContent = "Hi Ka-Cebuana " + smsfirstname + "! Your policy " + smsproductnamelabel + " is now active with COC details: COC No.:" + cocItems.COCNumber + ", Policy Period: " + cocItems.EffectiveDate + " to " + cocItems.TerminationDate + ", T&C apply. For Inquiries, call (02) 7759-9888 or visit https://www.cebuanalhuillier.com/microinsurance.";

                SendSMS(smsReferenceCode, messageContent, cocItems.COCNumber);

            }

        }
        catch (Exception ex)
        {
            SystemUtility.EventLog.SaveError(ex.ToString());
            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "Swal.fire(`" + ex + "`); ", true);
            throw;
        }
    }


    public void SendSMS(string smsReferenceCode, string messageContent, string cocNumber)
    {
        SMSRequest smsRequest = new SMSRequest();
        SendSMSDetails sendsmsdetails = new SendSMSDetails();

        smsRequest.Token = generateToken.GenerateTokenAuth();
        smsRequest.CocNumber = cocNumber;
        smsRequest.PlatformKey = ConfigurationManager.AppSettings["CLIBAPIKey"].ToString();
        smsRequest.ReferenceNumber = smsReferenceCode;

        sendsmsdetails.Content = messageContent;
        sendsmsdetails.IsInternational = false;
        sendsmsdetails.MobileNumber = contactNumber.Text.ToString();

        smsRequest.SendSMSDetails = sendsmsdetails;

        var returnValue = getList.SendSMS(smsRequest);
        string message = returnValue.Message;
    }
    #endregion

    #region FAMILY DYNAMIC UI
    protected void DD_TypeOfDependents_SelectedIndexChanged(object sender, EventArgs e)
    {
        InitializeDynamicControls();
    }
    private void InitializeDynamicControls()
    {
        try
        {
            ClearDynamicControls();
            string selectedValue = DD_TypeOfDependents.SelectedItem.ToString().ToUpper();
            string selectedIndex = DD_TypeOfDependents.SelectedValue;

            #region GET TYPE OF DIPENDENT
            token.Token = generateToken.GenerateTokenAuth();
            dependentrequest.Token = token.Token;
            dependentrequest.PlatformKey = ConfigurationManager.AppSettings["CLIBAPIKey"];
            dependentrequest.ProductCode = Session["productCode"].ToString();
            dependentrequest.TypeOfDependents = selectedIndex;

            var returnValue = getList.FamilyDependentUI(dependentrequest);
            #endregion

            // Serialize the DependentResult object to JSON
            string json = Newtonsoft.Json.JsonConvert.SerializeObject(returnValue);

            // Deserialize the JSON data
            var rootDependent = Newtonsoft.Json.JsonConvert.DeserializeObject<RootDependent>(json);

            // Access the dependents from the rootDependent object
            var dependents = rootDependent.Result;

            // Split the selectedValue into an array of relationships
            string[] relationships = selectedValue.Split(new string[] { " AND " }, StringSplitOptions.None);

            int countIndex = 1;
            foreach (var relationship in relationships)
            {
                var count = dependents.Where(x => x.Relationship.ToUpper() == relationship).FirstOrDefault().RelationshipCount;
                GenerateDependentControls(relationship, count, countIndex);  // Pass dependentCollectionList here
                countIndex++;
            }
        }
        catch (Exception ex)
        {

        }
    }

    private void ClearDynamicControls()
    {
        // Clear any existing dynamic controls
        relationshipDropdownContainer.Controls.Clear();
    }

    private void GenerateDependentControls(string relationshipName, int relationshipCount, int countIndex)
    {
        // Normalize the relationshipName to NormalCase
        relationshipName = System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(relationshipName.ToLower());

        // Create a div element to represent the category (e.g., "Parent" or "Siblings")
        HtmlGenericControl categoryDiv = new HtmlGenericControl("div");
        categoryDiv.Attributes["class"] = "mb-3"; // Add Bootstrap margin class

        Label lblCategoryName = new Label();
        lblCategoryName.Text = "Name Of " + relationshipName + "<br />"; // Add line break
        lblCategoryName.ID = "lbl_CategoryName" + countIndex; // No need for unique ID here
        lblCategoryName.CssClass = "h6"; // Add Bootstrap class for category heading

        categoryDiv.Controls.Add(lblCategoryName);

        // Create a div element for each dependent within the category
        for (int i = 1; i <= relationshipCount; i++)
        {
            HtmlGenericControl dependentDiv = new HtmlGenericControl("div");
            dependentDiv.Attributes["class"] = "row mb-2"; // Add Bootstrap classes for a row and margin

            Label lblDependentNumber = new Label();
            lblDependentNumber.Text = i.ToString() + ". ";
            lblDependentNumber.CssClass = "col-md-1"; // Adjust the column width and add Bootstrap class for bold text

            TextBox txtFirstName = new TextBox();
            txtFirstName.ID = "txt_FirstName_" + countIndex + i; // No need for unique ID here
            txtFirstName.CssClass = "col-md-4 form-control"; // Adjust the column width and add your custom CSS class here
            txtFirstName.Attributes.Add("placeholder", "First Name");
            txtFirstName.Attributes.Add("onkeydown", "return /[A-Za-z-' ]/i.test(event.key)"); // Add regex validation for First Name

            // Set minimum and maximum length
            txtFirstName.MaxLength = 75; // Maximum length
            txtFirstName.Attributes.Add("minlength", "1"); // Minimum length

            TextBox txtLastName = new TextBox();
            txtLastName.ID = "txt_LastName_" + countIndex + i; // No need for unique ID here
            txtLastName.CssClass = "col-md-4 form-control"; // Adjust the column width and add your custom CSS class here
            txtLastName.Attributes.Add("placeholder", "Last Name");
            txtLastName.Attributes.Add("onkeydown", "return /[A-Za-z-' ]/i.test(event.key)"); // Add regex validation for Last Name

            // Set minimum and maximum length
            txtLastName.MaxLength = 20; // Maximum length
            txtLastName.Attributes.Add("minlength", "1"); // Minimum length

            TextBox datePicker = new TextBox();
            datePicker.ID = "txt_DateOfBirth_" + countIndex + i;
            datePicker.CssClass = "col-md-3 form-control";
            datePicker.Attributes["type"] = "text";
            datePicker.Attributes["placeholder"] = "Select Date of Birth";
            datePicker.Attributes.Add("data-datepicker", "true");
            datePicker.Attributes["onchange"] = "validateDate(this);";

            // Add an onload attribute to remove the readonly attribute when the page loads
            datePicker.Attributes["onload"] = "this.removeAttribute('readonly');";

            // Add the dependentDiv to the categoryDiv
            categoryDiv.Controls.Add(dependentDiv);

            dependentDiv.Controls.Add(lblDependentNumber);
            dependentDiv.Controls.Add(txtFirstName);
            dependentDiv.Controls.Add(txtLastName);
            dependentDiv.Controls.Add(datePicker);

            DependentCollection dependent = new DependentCollection();
            dependent.FirstnameID = txtFirstName;
            dependent.LastnameID = txtLastName;
            dependent.BirthID = datePicker;
            dependent.DependentRelationship = relationshipName;

            dependentCollectionList.Add(dependent);



        }
        relationshipDropdownContainer.Controls.Add(categoryDiv);
    }


    #region GET TYPE OF DEPENDENTS
    public void GetTyoeOfDependents()
    {
        if (DD_TypeOfDependents == null)
        {
            DD_TypeOfDependents = new DropDownList();
        }

        DD_TypeOfDependents.Items.Clear();
        DD_TypeOfDependents.Items.Insert(0, new ListItem("Select", "0"));
        DD_TypeOfDependents.Items.Insert(1, new ListItem("Spouse and Children", "Spouse And Children"));
        DD_TypeOfDependents.Items.Insert(2, new ListItem("Parent and Children", "Parent And Children"));
        DD_TypeOfDependents.Items.Insert(3, new ListItem("Parent and Sibling", "Parent And Sibling"));

        DD_TypeOfDependents.Attributes["onchange"] = "applyGlowAnimation()";
    }

    #endregion

    protected void populatedatafromtextboxes()
    {
        if (Session["categoryCode"].ToString() == "FMI")
        {
            foreach (DependentCollection dependent in dependentCollectionList)
            {
                string firstName = ((TextBox)dependent.FirstnameID).Text.Trim();
                string lastName = ((TextBox)dependent.LastnameID).Text.Trim();
                string birthDate = ((TextBox)dependent.BirthID).Text.Trim();

                // Check if firstname has value
                if ((!string.IsNullOrEmpty(firstName) && string.IsNullOrEmpty(lastName) && string.IsNullOrEmpty(birthDate)) || (string.IsNullOrEmpty(firstName) && !string.IsNullOrEmpty(lastName) && !string.IsNullOrEmpty(birthDate)))
                {
                    firstnamehasvalue = false; // If any field is empty, set the flag to false
                }
                // Check if lastname has value
                if ((string.IsNullOrEmpty(firstName) && !string.IsNullOrEmpty(lastName) && string.IsNullOrEmpty(birthDate)) || (!string.IsNullOrEmpty(firstName) && string.IsNullOrEmpty(lastName) && !string.IsNullOrEmpty(birthDate)))
                {
                    lastnamehasvalue = false;
                }
                // Check if lastname has value
                if ((string.IsNullOrEmpty(firstName) && string.IsNullOrEmpty(lastName) && !string.IsNullOrEmpty(birthDate)) || (!string.IsNullOrEmpty(firstName) && !string.IsNullOrEmpty(lastName) && string.IsNullOrEmpty(birthDate)))
                {
                    birthdayhasvalue = false;
                }

                if (!string.IsNullOrEmpty(firstName) && !string.IsNullOrEmpty(lastName) && !string.IsNullOrEmpty(birthDate))
                {
                    dependent.DependentFirstName = firstName;
                    dependent.DependentLastName = lastName;
                    dependent.DependentBirth = birthDate;
                    dependent.DependentName = firstName + " " + lastName;

                    allhasvalue = true;
                }
            }
        }
    }


    #endregion

    #region SELECTION REQUEST METHOD FOR IMS SELECTION LIST
    public void SelectionRequestMethod()
    {
        token.Token = generateToken.GenerateTokenAuth();
        selectionlistrequest.Token = token.Token;
        selectionlistrequest.PlatformKey = ConfigurationManager.AppSettings["CEBOTGAPIkey"];
    }
    #endregion

    #region GET PREVIOUS LOSS STATUS
    public void GetPreviousLossStatus()
    {
        DD_IsThereAnyPreviousLoss.Items.Clear();
        DD_IsThereAnyPreviousLoss.Items.Insert(0, new ListItem("Select", "Select"));
        DD_IsThereAnyPreviousLoss.Items.Insert(1, new ListItem("Yes", "Yes"));
        DD_IsThereAnyPreviousLoss.Items.Insert(2, new ListItem("No", "No"));

    }
    #endregion

    #region IS THE PROPERTY MORTGAGED
    public void GetIsThePropertyMortgaged()
    {
        DD_IsThePropertyMortgaged.Items.Clear();
        DD_IsThePropertyMortgaged.Items.Insert(0, new ListItem("Select", "Select"));
        DD_IsThePropertyMortgaged.Items.Insert(1, new ListItem("Yes", "Yes"));
        DD_IsThePropertyMortgaged.Items.Insert(2, new ListItem("No", "No"));
    }
    #endregion

    #region GET OWNERSHIP STATUS
    public void GetOwnershipStatus()
    {
        SelectionRequestMethod();
        selectionlistrequest.DefinitionId = 21;
        var returnValue = getList.IMSSelectionList(selectionlistrequest);
        DD_OwnershipStatus.Items.Clear();
        DD_OwnershipStatus.Items.Insert(0, new ListItem("Select", "Select"));
        foreach (var item in returnValue.Result)
        {
            DD_OwnershipStatus.Items.Add(new ListItem(item.DisplayText, item.DisplayText));
        }

        DD_OwnershipStatus.DataBind();
    }
    #endregion

    #region GET NATURE OF OCCUPANCY
    public void GetNatureOfOccupancy()
    {
        SelectionRequestMethod();
        selectionlistrequest.DefinitionId = 22;
        var returnValue = getList.IMSSelectionList(selectionlistrequest);
        DD_NatureOfOccupancy.Items.Clear();
        DD_NatureOfOccupancy.Items.Insert(0, new ListItem("Select", "Select"));
        foreach (var item in returnValue.Result)
        {
            DD_NatureOfOccupancy.Items.Add(new ListItem(item.DisplayText, item.DisplayText));
        }

        DD_NatureOfOccupancy.DataBind();
    }
    #endregion

    #region GET TYPE OF HOME
    public void GetTypeOfHome()
    {
        SelectionRequestMethod();
        selectionlistrequest.DefinitionId = 23;
        var returnValue = getList.IMSSelectionList(selectionlistrequest);
        DD_TypeOfHome.Items.Clear();
        DD_TypeOfHome.Items.Insert(0, new ListItem("Select", "Select"));
        foreach (var item in returnValue.Result)
        {
            DD_TypeOfHome.Items.Add(new ListItem(item.DisplayText, item.DisplayText));
        }

        DD_TypeOfHome.DataBind();
    }
    #endregion

    #region GET TYPE OF EXTERIOR WALLS
    public void GetTypeExteriorWalls()
    {
        SelectionRequestMethod();
        selectionlistrequest.DefinitionId = 24;
        var returnValue = getList.IMSSelectionList(selectionlistrequest);
        DD_TypeExteriorWalls.Items.Clear(); 
        DD_TypeExteriorWalls.Items.Insert(0, new ListItem("Select", "Select"));
        foreach (var item in returnValue.Result)
        {
            DD_TypeExteriorWalls.Items.Add(new ListItem(item.DisplayText, item.DisplayText));
        }
   
        DD_TypeExteriorWalls.DataBind();
    }
    private string GetTypeExteriorWallsSelectedValue()
    {
        string selectedValue = DD_TypeExteriorWalls.SelectedValue;

        if (selectedValue == "Others: Please Specify")
        {
            selectedValue = otherInputTypeExteriorWalls.Text;
        }

        return selectedValue;
    }
    protected void DD_TypeExteriorWalls_SelectedIndexChanged(object sender, EventArgs e)
    {
        string selectedValue = DD_TypeExteriorWalls.SelectedValue;
        DD_TypeExteriorWalls.CssClass = "form-control";

        if (selectedValue == "Others: Please Specify")
        {
            otherInputTypeExteriorWalls.Style["display"] = "inline";
            otherInputTypeExteriorWalls.CssClass = "form-control fade-in";
            otherInputTypeExteriorWalls.Text = "";
            otherInputTypeExteriorWalls.Attributes["placeholder"] = "Please Specify Type of Exterior Walls";
        }
        else
        {
            otherInputTypeExteriorWalls.Text = "";
            otherInputTypeExteriorWalls.Style["display"] = "none";
            otherInputTypeExteriorWalls.Attributes.Remove("placeholder");

        }
    }
    #endregion

    #region GET TYPE OF ROOF
    public void GetTypeRoof()
    {
        SelectionRequestMethod();
        selectionlistrequest.DefinitionId = 25;
        var returnValue = getList.IMSSelectionList(selectionlistrequest);
        DD_TypeRoof.Items.Clear();
        DD_TypeRoof.Items.Insert(0, new ListItem("Select", "Select"));
        foreach (var item in returnValue.Result)
        {
            DD_TypeRoof.Items.Add(new ListItem(item.DisplayText, item.DisplayText));
        }

        DD_TypeRoof.DataBind();
    }
    private string GetTypeRoofSelectedValue()
    {
        string selectedValue = DD_TypeRoof.SelectedValue;

        if (selectedValue == "Others: Please Specify")
        {
            selectedValue = otherInputTypeRoof.Text;
        }

        return selectedValue;
    }
    protected void DD_TypeRoof_SelectedIndexChanged(object sender, EventArgs e)
    {
        string selectedValue = DD_TypeRoof.SelectedValue;

        DD_TypeRoof.CssClass = "form-control";

        if (selectedValue == "Others: Please Specify")
        {
            otherInputTypeRoof.Style["display"] = "inline";
            otherInputTypeRoof.CssClass = "form-control fade-in";
            otherInputTypeRoof.Text = "";
            otherInputTypeRoof.Attributes["placeholder"] = "Please Specify Type of Roof";
        }
        else
        {
            otherInputTypeRoof.Text = "";
            otherInputTypeRoof.Style["display"] = "none";
            otherInputTypeRoof.Attributes.Remove("placeholder");
        }

    }
    #endregion

    #region GET TYPE OF INNER PARTITIONS
    public void GetTypeInnerPartitions()
    {
        SelectionRequestMethod();
        selectionlistrequest.DefinitionId = 26;
        var returnValue = getList.IMSSelectionList(selectionlistrequest);
        DD_TypeInnerPartitions.Items.Clear();
        DD_TypeInnerPartitions.Items.Insert(0, new ListItem("Select", "Select"));
        foreach (var item in returnValue.Result)
        {
            DD_TypeInnerPartitions.Items.Add(new ListItem(item.DisplayText, item.DisplayText));
        }
        DD_TypeInnerPartitions.DataBind();
    }
    private string GetTypeInnerPartitionsSelectedValue()
    {
        string selectedValue = DD_TypeInnerPartitions.SelectedValue;

        if (selectedValue == "Others: Please Specify")
        {
            selectedValue = otherInputTypeInnerPartitions.Text;
        }
        return selectedValue;
    }
    protected void DD_TypeInnerPartitions_SelectedIndexChanged(object sender, EventArgs e)
    {
        string selectedValue = DD_TypeInnerPartitions.SelectedValue;

        DD_TypeInnerPartitions.CssClass = "form-control";

        if (selectedValue == "Others: Please Specify")
        {
            otherInputTypeInnerPartitions.Style["display"] = "inline";
            otherInputTypeInnerPartitions.CssClass = "form-control fade-in";
            otherInputTypeInnerPartitions.Text = "";
            otherInputTypeInnerPartitions.Attributes["placeholder"] = "Please Specify Type of Inner Partitions";
        }
        else
        {
            otherInputTypeInnerPartitions.Text = "";
            otherInputTypeInnerPartitions.Style["display"] = "none";
            otherInputTypeInnerPartitions.Attributes.Remove("placeholder");
        }
    }
    #endregion

    #region GET TYPE OF BEAMS
    public void GetTypeBeams()
    {
        SelectionRequestMethod();
        selectionlistrequest.DefinitionId = 27;
        var returnValue = getList.IMSSelectionList(selectionlistrequest);
        DD_TypeBeams.Items.Clear();
        DD_TypeBeams.Items.Insert(0, new ListItem("Select", "Select"));
        foreach (var item in returnValue.Result)
        {
            DD_TypeBeams.Items.Add(new ListItem(item.DisplayText, item.DisplayText));
        }

        DD_TypeBeams.DataBind();
    }
    private string GetTypeBeamsSelectedValue()
    {
        string selectedValue = DD_TypeBeams.SelectedValue;

        if (selectedValue == "Others: Please Specify")
        {
            selectedValue = otherInputTypeBeams.Text;
        }
        return selectedValue;
    }

    protected void DD_TypeBeams_SelectedIndexChanged(object sender, EventArgs e)
    {
        string selectedValue = DD_TypeBeams.SelectedValue;

        DD_TypeBeams.CssClass = "form-control";

        if (selectedValue == "Others: Please Specify")
        {
            otherInputTypeBeams.Style["display"] = "inline";
            otherInputTypeBeams.CssClass = "form-control fade-in";
            otherInputTypeBeams.Text = "";
            otherInputTypeBeams.Attributes["placeholder"] = "Please Specify Type of Beams";
        }
        else
        {
            otherInputTypeBeams.Text = "";
            otherInputTypeBeams.Style["display"] = "none";
            otherInputTypeBeams.Attributes.Remove("placeholder");
        }
    }
    #endregion

    #region GET TYPE OF COLUMNS
    public void GetTypeColumns()
    {
        SelectionRequestMethod();
        selectionlistrequest.DefinitionId = 28;
        var returnValue = getList.IMSSelectionList(selectionlistrequest);
        DD_TypeColumns.Items.Clear();
        DD_TypeColumns.Items.Insert(0, new ListItem("Select", "Select"));
        foreach (var item in returnValue.Result)
        {
            DD_TypeColumns.Items.Add(new ListItem(item.DisplayText, item.DisplayText));
        }

        DD_TypeColumns.DataBind();
    }

    private string GetTypeColumnsSelectedValue()
    {
        string selectedValue = DD_TypeColumns.SelectedValue;

        if (selectedValue == "Others: Please Specify")
        {
            selectedValue = otherInputTypeColumns.Text;
        }
        return selectedValue;
    }
    protected void DD_TypeColumns_SelectedIndexChanged(object sender, EventArgs e)
    {
        string selectedValue = DD_TypeColumns.SelectedValue;
        DD_TypeColumns.CssClass = "form-control";

        if (selectedValue == "Others: Please Specify")
        {
            otherInputTypeColumns.Style["display"] = "inline";
            otherInputTypeColumns.CssClass = "form-control fade-in";
            otherInputTypeColumns.Text = "";
            otherInputTypeColumns.Attributes["placeholder"] = "Please Specify Type of Columns";
        }
        else
        {
            otherInputTypeColumns.Text = "";
            otherInputTypeColumns.Style["display"] = "none";
            otherInputTypeColumns.Attributes.Remove("placeholder");
        }

    }

    #endregion

    #region GET FIRE PROPERTY PROVINCE
    public void GetListProvinceFireProperty()
    {
        token.Token = generateToken.GenerateTokenAuth();
        IList<String> provList;
        provList = getList.GetListProvince(token);
        PDDLProvince.DataSource = provList;
        PDDLProvince.DataBind();

    }
    #endregion

    #region GET FIRE PROPERTY CITY LIST
    public void GetListCityFireProperty(string Prov)
    {
        token.Province = Prov;
        token.Token = generateToken.GenerateTokenAuth();
        IList<String> cityList;
        cityList = getList.GetListCity(token);
        PDDLCity.DataSource = cityList;
        PDDLCity.DataBind();

        // Add the CSS class to trigger the animation
        PDDLCity.CssClass = "form-control fade-in";
    }
    #endregion

    #region GET PROVINCE
    public void GetListProvince()
    {
 
        token.Token = generateToken.GenerateTokenAuth();
        IList<string> provList;
        provList = getList.GetListProvince(token);
        DDProvince.Items.Add(new ListItem("Select", "Select"));
        DDProvince.DataSource = provList;
        DDProvince.DataBind();

    }
    #endregion

    #region GET CITY LIST

    public void GetListCity(string Prov)
    {
        token.Province = Prov;
        token.Token = generateToken.GenerateTokenAuth();
        IList<String> cityList;
        cityList = getList.GetListCity(token);
        DDcity.DataSource = cityList;
        DDcity.DataBind();
        // Add the CSS class to trigger the animation
        DDcity.CssClass = "form-control fade-in";
    }
    #endregion

    #region GET SOURCE OF FUNDS
    public void GetListSourceOfFunds()
    {
        token.Token = generateToken.GenerateTokenAuth();
        IList<String> sourceofFUndsList;
        sourceofFUndsList = getList.GetListSourceOfFunds(token);
        DDSourceOfFunds.DataSource = sourceofFUndsList;
        DDSourceOfFunds.DataBind();
    }
    #endregion

    #region GET NATURE OF WORK
    public void GetListNatureOfWork()
    {
        token.Token = generateToken.GenerateTokenAuth();
        IList<String> natureList;
        natureList = getList.GetListNatureOfWork(token);
        fld_NatureOfWork.DataSource = natureList;
        fld_NatureOfWork.DataBind();
    }
    #endregion

    #region GET CIVIL STATUS LIST
    public void GetListCivilStatus()
    {
        token.Token = generateToken.GenerateTokenAuth();
        IList<String> civilStatList;
        civilStatList = getList.GetListCivilStatus(token);
        civilStatus.DataSource = civilStatList;
        civilStatus.DataBind();

    }
    #endregion

    #region GET NATIONALITY
    private void GetNationalities()
    {
        string[] nationalities = {
                "Select","Filipino", "Afghan", "Albanian", "Algerian", "American", "Andorran", "Angolan", "Antiguans", "Argentinean", "Armenian",
                "Australian", "Austrian", "Azerbaijani", "Bahamian", "Bahraini", "Bangladeshi", "Barbadian", "Barbudans", "Batswana", "Belarusian",
                "Belgian", "Belizean", "Beninese", "Bhutanese", "Bolivian", "Bosnian", "Brazilian", "British", "Bruneian", "Bulgarian", "Burkinabe",
                "Burmese", "Burundian", "Cambodian", "Cameroonian", "Canadian", "Cape Verdean", "Central African", "Chadian", "Chilean", "Chinese",
                "Colombian", "Comoran", "Congolese", "Costa Rican", "Croatian", "Cuban", "Cypriot", "Czech", "Danish", "Djibouti", "Dominican", "Dutch",
                "East Timorese", "Ecuadorean", "Egyptian", "Emirian", "Equatorial Guinean", "Eritrean", "Estonian", "Ethiopian", "Fijian", "Finnish",
                "French", "Gabonese", "Gambian", "Georgian", "German", "Ghanaian", "Greek", "Grenadian", "Guatemalan", "Guinea-Bissauan", "Guinean",
                "Guyanese", "Haitian", "Herzegovinian", "Honduran", "Hungarian", "Icelander", "Indian", "Indonesian", "Iranian", "Iraqi", "Irish",
                "Israeli", "Italian", "Ivorian", "Jamaican", "Japanese", "Jordanian", "Kazakhstani", "Kenyan", "Kittian And Nevisian", "Kuwaiti",
                "Kyrgyz", "Laotian", "Latvian", "Lebanese", "Liberian", "Libyan", "Liechtensteiner", "Lithuanian", "Luxembourger", "Macedonian",
                "Malagasy", "Malawian", "Malaysian", "Maldivan", "Malian", "Maltese", "Marshallese", "Mauritanian", "Mauritian", "Mexican", "Micronesian",
                "Moldovan", "Monacan", "Mongolian", "Moroccan", "Mosotho", "Motswana", "Mozambican", "Namibian", "Nauruan", "Nepalese", "New Zealander",
                "Ni-Vanuatu", "Nicaraguan", "Nigerien", "North Korean", "Northern Irish", "Norwegian", "Omani", "Pakistani", "Palauan", "Panamanian",
                "Papua New Guinean", "Paraguayan", "Peruvian", "Polish", "Portuguese", "Qatari", "Romanian", "Russian", "Rwandan", "Saint Lucian",
                "Salvadoran", "Samoan", "San Marinese", "Sao Tomean", "Saudi", "Scottish", "Senegalese", "Serbian", "Seychellois", "Sierra Leonean",
                "Singaporean", "Slovakian", "Slovenian", "Solomon Islander", "Somali", "South African", "South Korean", "Spanish", "Sri Lankan",
                "Sudanese", "Surinamer", "Swazi", "Swedish", "Swiss", "Syrian", "Taiwanese", "Tajik", "Tanzanian", "Thai", "Togolese", "Tongan",
                "Trinidadian Or Tobagonian", "Tunisian", "Turkish", "Tuvaluan", "Ugandan", "Ukrainian", "Uruguayan", "Uzbekistani", "Venezuelan",
                "Vietnamese", "Welsh", "Yemenite", "Zambian", "Zimbabwean"
                };

        // Add nationalities to the DropDownList
        DDNationality.DataSource = nationalities;
        DDNationality.DataBind();

    }
    #endregion

    #region GET GENDER LIST
    public void GetListGender()
    {
        token.Token = generateToken.GenerateTokenAuth();
        IList<String> genderList;
        genderList = getList.GetListGender(token);

        // Create a dictionary to map displayed text to underlying values
        Dictionary<string, string> genderMap = new Dictionary<string, string>();
        genderMap.Add("Male", "M");
        genderMap.Add("Female", "F");

        // Modify the gender list to display "Male" or "Female" and store "M" or "F"
        List<ListItem> modifiedList = new List<ListItem>();
        foreach (var item in genderList)
        {
            if (genderMap.ContainsKey(item))
                modifiedList.Add(new ListItem(item, genderMap[item]));
            else
                modifiedList.Add(new ListItem("Select", "")); // Handle other cases if necessary
        }

        // Bind the modified gender list to the dropdown
        gender.DataSource = modifiedList;
        gender.DataTextField = "Text";
        gender.DataValueField = "Value";
        gender.DataBind();
    }
    #endregion

    #region GET VALID ID
    public void GetListValidID()
    {
        token.Token = generateToken.GenerateTokenAuth();
        IList<String> validIDList;
        validIDList = getList.GetListValidID(token);
        validIdDropDownList.DataSource = validIDList;
        validIdDropDownList.DataBind();

    }
    #endregion

    #region GET RELATIONSHIP
    public void GetListRelation()
    {
        token.Token = generateToken.GenerateTokenAuth();
        IList<String> relList;
        relList = getList.GetListRelation(token);
        relationshipDropDownList.DataSource = relList;
        relationshipDropDownList.DataBind();

        guardianRelationshipDropDownList.DataSource = relList;
        guardianRelationshipDropDownList.DataBind();

    }
    #endregion

    #region Save Details
    public void PopulateData()
    {
        processTransactionRequest = SaveDetails();
        ClearFields(enrollmentForm);
        baseResult = verifyFields.VerifyTransactionFields(processTransactionRequest);
        guardianVerifyFields = verifyFields.IsValidGuardian(processTransactionRequest);


        if (guardianVerifyFields.ResultStatus == ResultType.Success)
        {
            baseResult = verifyFields.VerifyTransactionFields(processTransactionRequest);

            if (baseResult.ResultStatus == ResultType.Success)
            {
                processTransactionResult = processTransaction.ProcessTransactionDetails(processTransactionRequest);

                if (processTransactionResult.ResultStatus == ResultType.Success)
                {
                    #region Send COC to Client

                    token.ContactNumber = processTransactionRequest.CustomerDetails.MobileNumber;
                    token.Email = processTransactionRequest.CustomerDetails.EmailAddress;
                    token.FirstName = processTransactionRequest.CustomerDetails.FirstName;
                    token.MiddleName = processTransactionRequest.CustomerDetails.MiddleName;
                    token.LastName = processTransactionRequest.CustomerDetails.LastName;
                    token.ProductName = productCode;
                    token.COCNumber = processTransactionResult.InsuranceTransactionCollection[0].CocNumber;
                    token.EffectiveDateTime = processTransactionResult.InsuranceTransactionCollection[0].EffectiveDate.ToString();
                    token.TerminationDate = processTransactionResult.InsuranceTransactionCollection[0].TerminationDate.ToString();

                    baseResult = processTransaction.SendCOCToClient(token);

                    #endregion

                    if (baseResult.ResultStatus == ResultType.Success)
                    {
                        Session.Clear();
                        Session["success"] = "sucess";
                        Session["partnerCode"] = partnerCode;
                        Response.Redirect(ConfigurationManager.AppSettings["ThankYouPage"].Trim(), false);
                    }
                    else
                    {
                        SystemUtility.EventLog.SaveError(baseResult.Message);
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "Swal.fire('" + "An error has occured while sending your COC details. Please contact insurance_support@pjlhuillier.com for assistance." + "'); window.location='" + ConfigurationManager.AppSettings["ProductRegistration"] + "';", true);
                    }
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "Swal.fire('" + processTransactionResult.Message + " Please try again.');", true);
                }
            }
            else
            {
                SystemUtility.EventLog.SaveError(baseResult.Message);
                Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "Swal.fire('" + baseResult.Message + "');", true);
            }
        }
        else
        {
            SystemUtility.EventLog.SaveError(guardianVerifyFields.Message);
            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "Swal.fire('" + guardianVerifyFields.Message + "');", true);
        }
    }

    public ProcessTransactionRequest SaveDetails()
    {

        token.Token = generateToken.GenerateTokenAuth();
        DateTime oDate = DateTime.Parse(birthDateTextBox.Text);

        #region Beneficiary

        beneficiaryCollection.BeneficiaryName = beneFullname.Text.Trim();
        beneficiaryCollection.BeneficiaryRelationship = relationshipDropDownList.SelectedItem.Text;

        #endregion

        #region Customer Details

        customerDetails.FirstName = firstName.Value.Trim();
        customerDetails.MiddleName = middleName.Value.Trim();
        customerDetails.LastName = lastName.Value.Trim();
        customerDetails.Address = presentAddress.Text.Trim();
        customerDetails.Birthdate = oDate.ToString(ConfigurationManager.AppSettings["DateTimeFormat"]);
        customerDetails.PlaceOfBirth = placeOfBirth.Text.Trim();
        customerDetails.SourceOfFunds = DDSourceOfFunds.SelectedItem.Text;
        customerDetails.Gender = gender.SelectedItem.Text;
        customerDetails.CivilStatus = civilStatus.SelectedItem.Text;


        customerDetails.MobileNumber = contactNumber.Text.Trim();
        customerDetails.EmailAddress = emailAddress.Text.Trim();
        customerDetails.ValidIDPresented = validIdDropDownList.SelectedItem.Text;
        customerDetails.ValidIDNumber = idNumber.Text.Trim();


        //customerDetails.SourceOfFunds = sourceOfFunds.Text.Trim();
        // customerDetails.Gender = isRadioChecked(maleRadioButton, femaleRadioButton);
        //customerDetails.CivilStatus = isStatusRadioChecked(singleCivilStatus, widowedCivilStatus, marriedCivilStatus, separatedCivilStatus);
        #endregion

        #region Guardian Details

        if ((DateTime.Now.Year - oDate.Year) < 18)
        {
            DateTime gDate = DateTime.Parse(guardianBirthDate.Text);
            guardianDetails.GuardianBirthday = gDate.ToString(ConfigurationManager.AppSettings["DateTimeFormat"]);
            guardianDetails.GuardianName = guardianFullName.Text.Trim();
            guardianDetails.GuardianRelationship = guardianRelationshipDropDownList.SelectedItem.Text;
            //guardianDetails.GuardianContactNo = guardianContactTextBox.Text.Trim();
        }

        #endregion

        #region General Details

        generalDetails.ReferenceNo = voucherCode; //modified 11 / 25 / 21 JEH: changed from token.ReferenceNumber to voucherCode
        generalDetails.DateTimeFormat = ConfigurationManager.AppSettings["DateTimeFormat"];
        generalDetails.IntegrationId = getList.GetIntegrationId(token);
        generalDetails.IsPaid = "true";
        generalDetails.NumberOfCOCs = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["NumberOfCOCs"]);
        generalDetails.ForRenewal = "false"; //for coordination c/o Maam Ai
        generalDetails.PlatformAPI = ConfigurationManager.AppSettings["PlatformAPI"];
        generalDetails.ReferenceCode = voucherCode;

        #endregion

        List<BeneficiaryCollection> beneList = new List<BeneficiaryCollection>();
        beneList.Add(beneficiaryCollection);

        processTransactionRequest.BeneficiaryCollection = beneList;
        processTransactionRequest.CustomerDetails = customerDetails;
        processTransactionRequest.GeneralDetails = generalDetails;

        if (guardianBirthDate.Text != "")
        {
            processTransactionRequest.GuardianDetails = guardianDetails;
        }

        processTransactionRequest.Token = token.Token;


        return processTransactionRequest;

    }
    #endregion

    #region Check Radio Button
    //public string isRadioChecked(RadioButton radioButton1, RadioButton radioButton2)
    //{
    //    bool isChecked = radioButton1.Checked;
    //    string value = "";
    //    if (isChecked == true)
    //    {
    //        value = radioButton1.Text;
    //    }
    //    else if (radioButton2.Checked == true)
    //    {
    //        value = radioButton2.Text;
    //    }
    //    else
    //    {
    //        value = "";
    //    }

    //    return value;
    //}
    //public string isStatusRadioChecked(RadioButton radioButton1, RadioButton radioButton2, RadioButton radioButton3, RadioButton radioButton4)
    //{
    //    //bool isChecked = radioButton1.Checked;
    //    string value = "";
    //    if (radioButton1.Checked == true)
    //    {
    //        value = radioButton1.Text;
    //    }
    //    else if (radioButton2.Checked == true)
    //    {
    //        value = radioButton2.Text;
    //    }
    //    else if (radioButton3.Checked == true)
    //    {
    //        value = radioButton3.Text;
    //    }
    //    else if (radioButton4.Checked == true)
    //    {
    //        value = radioButton4.Text;
    //    }
    //    else
    //    {
    //        value = "";
    //    }

    //    return value;
    //}
    #endregion

    #region Check Fields


    public bool validateFields()
    {
        bool isValid = false;

        return isValid;
    }

    private void ClearFields(Control form)
    {
        foreach (var control in this.Controls)
        {
            var textbox = control as TextBox;
            if (textbox != null)
                textbox.Text = string.Empty;
        }
    }
    #endregion

    #region Captcha
    public void generateNewCaptcha_Click(object sender, EventArgs e)
    {
        WebCaptcha.GenerateCaptcha(captchaImage, HttpContext.Current);
        captchaText.Value = "";
    }
    #endregion

    protected void DDProvince_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetListCity(DDProvince.SelectedValue);
        UpdatePanel1.Update();
        
    }
    protected void PDDLProvince_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetListCityFireProperty(PDDLProvince.SelectedValue);
        UpdatePanel1.Update();

    }
    protected void DD_IsThePropertyMortgaged_SelectedIndexChanged(object sender, EventArgs e)
    {
        DD_IsThePropertyMortgaged.CssClass = "form-control";

        if (DD_IsThePropertyMortgaged.SelectedValue == "Yes")
        {
            fld_MortgageeName.Visible = true;
            spanmortgagee.Visible = true;
        }
        else
        {
            fld_MortgageeName.Visible = false;
            spanmortgagee.Visible = false;
            fld_MortgageeName.Text = "";
        }
    }
    protected void DD_IsThereAnyPreviousLoss_SelectedIndexChanged(object sender, EventArgs e)
    {
        string selectedValue = DD_IsThereAnyPreviousLoss.SelectedValue;

        DD_IsThereAnyPreviousLoss.CssClass = "form-control";

        if (selectedValue == "Yes")
        {
            spandetailsofincident.Visible = true;
            fld_Remarks.Style["display"] = "inline";
            fld_Remarks.CssClass = "form-control fade-in";
            fld_Remarks.Text = "";
            fld_Remarks.Attributes["placeholder"] = "Please indicate date and detail of incident";
        }
        else
        {
            spandetailsofincident.Visible = false;
            fld_Remarks.Style["display"] = "none";
            fld_Remarks.Attributes.Remove("placeholder");
            fld_Remarks.Text = "";
        }
    }

}