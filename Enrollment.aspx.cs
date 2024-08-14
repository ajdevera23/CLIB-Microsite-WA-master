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

public partial class Enrollment : System.Web.UI.Page
{
    #region Initialization

    string productCode;
    string partnerCode;
    string orderId;
    string referenceCode;
    string voucherCode;
    string referenceNumber;
    string categoryCode;
    public string ID = "";
    string referenceno = "";
  

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

    PurchaseRequest purchaserequest = new PurchaseRequest();
    PurchaseRequest purchaseresult = new PurchaseRequest();

    PurchaseResult purchaseResult = new PurchaseResult();

    Nationality Nationality = new Nationality();

    SMSRequest smsrequest = new SMSRequest();
    SMSResults smsresults = new SMSResults();

    private SelectionListRequest selectionlistrequest = new SelectionListRequest();
    private SelectionResult selectionlistresult = new SelectionResult();

    private DestinationTypeRequest destinationtyperequest = new DestinationTypeRequest();
    private DestinationTypeResult destinationtyperesult = new DestinationTypeResult();

    private OptionalCoverageRequest optionalcoveragerequest = new OptionalCoverageRequest();
    private OptionalCoverageResult optionalcoverageresult = new OptionalCoverageResult();

    private DependentRequest dependentrequest = new DependentRequest();
    private DependentResult dependentresult = new DependentResult();


    List<DependentCollection> dependentCollectionList = new List<DependentCollection>();
    List<DependentCollections> dependentCollectionLists = new List<DependentCollections>();


    private ReferralCodeRequest referralcoderequest = new ReferralCodeRequest();
    private ReferralCodeResult referralcoderesult = new ReferralCodeResult();


    bool firstnamehasvalue = true;
    bool lastnamehasvalue = true;
    bool birthdayhasvalue = true;
    bool allhasvalue = false;
  
    //protected string numberOfDays { get; set; }
    //protected string travelFrom { get; set; }
    //protected string travelTo { get; set; }


    IList<String> partnerList;
    IList<String> productList;
    #endregion

    #region Page_Load
    protected void Page_Load(object sender, EventArgs e)
    {
        Session["PartnerValue"] = Request.QueryString["PART"].ToString();
        Session["ValidateSuccessfully"] = "False";
        PageLoadEnableReferralElements();
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.Cache.SetNoStore();
      
        string sessionid = "";

        sessionid = HttpContext.Current.Session.SessionID;

        token.Token = generateToken.GenerateTokenAuth();
        partnerList = getList.GetPartnerList(token); 
        productList = getList.GetProductList(token);

        postload();
        sessionfields();

        if (!IsPostBack)
        {
          
            Session["PaymentMethod"] = "False";
 

            generateCaptcha();
            if (Session["IsExist"].ToString() == "True")
            {
                GetListProvince();
                GetListRelation();
            }
            if(Session["IsExist"].ToString() == "False" || Session["IsExist"].ToString() == "True")
            {
                GetListGender();
                GetListCivilStatus();
                GetNationalities();
                GetListProvince();
                GetListValidID();
                GetListRelation();
                GetListSourceOfFunds();
                GetListNatureOfWork();
            }
            // FIRE PROPERTY CATEGORY CODE IS PRI AND THE CATEGORY ID IS 6
            if (Session["CategoryId"].ToString() == "6")
            {
                FirePropertyField();
            }
            // FAMILY PLUS CODE IS FMI AND THE CATEGORY ID IS 10
            if (Session["CategoryId"].ToString() == "10")
            {
                FamilyPlusField();
            }
            // TRAVEL INSURANCE IS TRI AND THE CATEOGRY ID IS 11
            if(Session["CategoryId"].ToString() == "11")
            {
                GetDestinationType();
                GetOptionalCoverage();
                GetTravelOrigin();
                ScriptManager.RegisterStartupScript(this, GetType(), "ClearLocalStorage", "localStorage.removeItem('selectedPlan');", true);
            }

        }

        if (IsPostBack)
        {
            if (DD_TypeOfDependents.SelectedValue != "0" && Session["CategoryId"].ToString() == "10")
            {
                InitializeDynamicControls();

            }
        }

        submitBtn.ServerClick += SubmitBtn_Click;
        btnPaymentMethod.ServerClick += SubmitBtn_PaymentMethod;
        goBackStep1.ServerClick += GoBackto_Step1;
        gotoFinalStep.ServerClick += Goto_FinalStep;
        btnGotoPaymentSummary.ServerClick += Goto_PaymentSummary;
        btnGotoStepPamentOptions.ServerClick += Goto_PaymentOptions;
        //Button1.ServerClick += Button_1;
    }

    public void postload()
    {
        partnerCode = Request.QueryString["PART"];
        productCode = Request.QueryString["PROD"];

        #region Get Partner Logo
        token.PartnerCode = "PJ4";
        string partnerImagePath = getList.GetPartnerImagePath(token);
        //partnerImage.Attributes["src"] = ResolveUrl(ConfigurationManager.AppSettings["productImagePath"] + partnerImagePath);
        partnerImage.Style["background-image"] = ResolveUrl(ConfigurationManager.AppSettings["productImagePath"] + partnerImagePath);
        #endregion

        #region Get Product Logo
        token.ProductCode = productCode;
        string productImagePath = getList.GetProductImagePath(token);
        //productImage.Attributes["src"] = ResolveUrl(System.Configuration.ConfigurationManager.AppSettings["productImagePath"] + productImagePath);
        productImage.Style["background-image"] = ResolveUrl(System.Configuration.ConfigurationManager.AppSettings["productImagePath"] + productImagePath);
        #endregion

        #region Get Category Code
        categoryCode = getList.GetCategory(token);
        #endregion

        #region Show Guardian div
        if (guardianBirthDate.Text != "")
        {
            if ((DateTime.Now.Year - DateTime.Parse(guardianBirthDate.Text).Year) < 18)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "Swal.fire('Please enter valid guardian birthdate');", true);
            }
        }
        #endregion

    }

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

    #region SESSION FIELDS FROM CHECK ELIGIBILITY PAGE
    public void sessionfields()
    {
        try
        {
           firstName.Value = Session["firstName"].ToString();
            middleName.Value = Session["middleName"].ToString();
            lastName.Value = Session["lastName"].ToString();
            suffix.Value = Session["suffix"].ToString();
            birthDateTextBox.Text = Session["DateOfBirth"].ToString();

            Session["TermsAndConditions"] = ConfigurationManager.AppSettings["TermsAndConditions"] + "?PART=" + Request.QueryString["PART"] + "&PROD=" + Request.QueryString["PROD"] + "&INTID=" + Request.QueryString["INTID"] + "&PCODE=" + Request.QueryString["PCODE"];
            Session["partnerCode"] = Request.QueryString["PART"];
            Session["productCode"] = Request.QueryString["PROD"];
        }
        catch (Exception ex)
        {
            SystemUtility.EventLog.SaveError(ex.ToString());
            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "Swal.fire(`" + ex + "`); ", true);
            throw;
        }

    }
    #endregion

    #region FIRE PROPERTY FIELD
    public void FirePropertyField()
    {
        try
        {
            if (Session["CategoryId"].ToString() == "6")
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
        catch (Exception ex)
        {
            SystemUtility.EventLog.SaveError(ex.ToString());
            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "Swal.fire(`" + ex + "`); ", true);
            throw;
        }

    }
    #endregion

    #region FAMILY PLUS FIELD
    public void FamilyPlusField()
    {
        try
        {
            if (Session["CategoryId"].ToString() == "10")
            {
                GetTyoeOfDependents();
            }
        }
        catch (Exception ex) 
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "Swal.fire(`" + ex + "`); ", true);
            throw;
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
            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "Swal.fire(`" + ex + "`); ", true);
        }
        catch (Exception ex)
            {
            SystemUtility.EventLog.SaveError(ex.ToString());
            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "Swal.fire('An unexpected error occured. Please contact CLIB and try again later.');", true);
        }
        

    }
    #endregion

    #region btnPaymentMethod
    protected void SubmitBtn_PaymentMethod(object sender, EventArgs e)
    {
        string selectedPayment = Request.Form["payment-option"];

        if(selectedPayment == "Cebuana Lhuillier Branch")
        {
            Session["PaymentMethod"] = "CL Branch";
            int currentValue = Convert.ToInt32(numberInput.Text);
            DisplayPreview(currentValue);
        }
        if(selectedPayment == "Online Payment")
        {
            Session["PaymentMethod"] = "CLDigital";
        }
    }
    #endregion

    #region gobacktoStep1
    protected void GoBackto_Step1(object sender, EventArgs e)
    {
        numberInput.Text = "1";

        if(Session["PaymentMethod"].ToString() == "CL Branch")
        {
            Session["PaymentMethod"] = "False";
            SavingInformationValidation();
        }
        else
        {
            Session["PaymentMethod"] = "CLDigital";
        }
    }
    #endregion

    #region Goto_FinalStep
    protected void Goto_FinalStep(object sender, EventArgs e)
    {
        PurchaseSavingInformation();
     
    }
    #endregion

    #region Goto_PaymentSummary
    protected void Goto_PaymentSummary(object sender, EventArgs e)
    {
        string selectedPayment = Request.Form["payment.payment_method"];

        if(selectedPayment == null)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "Swal.fire('Please Choose Payment Method to Proceed');", true);
            return;
        }
        else
        {
            Session["PaymentMethod"] = selectedPayment;
            int currentValue = Convert.ToInt32(numberInput.Text);
            DisplayPreview(currentValue);
        }

    }
    #endregion

    #region GotoPaymentOptions
    protected void Goto_PaymentOptions(object sender, EventArgs e)
    {
        Session["PaymentMethod"] = "False";
        SavingInformationValidation();
    }
    #endregion

    #region OPEN PAYMENT CHANNEL
    public void OpenPaymentChannel()
    {
        populatedatafromtextboxesfieldvalidations();

        #region CLIENT KYC INITIALIZATION

                string birthdate = "";
                string guardianbirthdate = "";
                string petbirthdate = "";
                string guardianrelationship = "";
                string beneficiaryrelationship = "";
                string natureofwork = "";
                string bodyOfWaterDistanceText = fld_BodyOfWaterDistance.Text.Trim();
                string floorAreaText = fld_FloorArea.Text.Trim();
                string noOfStoreysText = fld_NoOfStoreys.Text.Trim();
                string propertyAgeText = fld_PropertyAge.Text.Trim();

                string message = "";

                #region ISMINOR
                if (Session["IsMinor"].ToString() == "True" && Session["CategoryId"].ToString() != "6")
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

        #region PET BIRTHDATE

                if(Session["CategoryId"].ToString() == "8")
                {
                    petbirthdate = DateTime.Parse(fld_PetBirthdate.Text.Trim()).ToString("MM/dd/yyyy");
                }
                else
                {
                    petbirthdate = "";
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

                birthdate = DateTime.Parse(birthDateTextBox.Text.Trim()).ToString("MM/dd/yyyy");
 

        #endregion

        //string feildvalidationpetbirthdate = DateTime.Parse(fld_PetBirthdate.Text.Trim()).ToString("MM/dd/yyyy");

        FieldValidationRequest fieldvalidationrequest = new FieldValidationRequest();

            fieldvalidationrequest.Token = generateToken.GenerateTokenAuth();
            fieldvalidationrequest.Address = presentAddress.Text.ToString();
            fieldvalidationrequest.Barangay = fld_Baranggay.Text.ToString();
            fieldvalidationrequest.BeneficiaryName = beneFullname.Text.ToString();
            fieldvalidationrequest.BeneficiaryRelationship = relationshipDropDownList.SelectedValue.ToString();
            fieldvalidationrequest.Birthdate = birthdate;
            fieldvalidationrequest.BodyOfWaterDistance = string.IsNullOrEmpty(bodyOfWaterDistanceText) ? 0 : Convert.ToDecimal(bodyOfWaterDistanceText);
            fieldvalidationrequest.BookingReferenceNumber = fld_BookingReferenceNo.Text.ToString();
            fieldvalidationrequest.BoundaryFront = fld_BoundaryFront.Text.ToString();
            fieldvalidationrequest.BoundaryLeft = fld_BoundaryLeft.Text.ToString();
            fieldvalidationrequest.BoundaryRear = fld_BoundaryRear.Text.ToString();
            fieldvalidationrequest.BoundaryRight = fld_BoundaryRight.Text.ToString();
            fieldvalidationrequest.CategoryCode = categoryCode;
            fieldvalidationrequest.City = DDcity.SelectedValue.ToString();
            fieldvalidationrequest.CivilStatus = civilStatus.SelectedValue.ToString();
            fieldvalidationrequest.DateTimeFormat = "MM/dd/yyyy";

            List<DependentCollections> deps = new List<DependentCollections>();
            fieldvalidationrequest.DependentCollection = deps;

            for (int i = 0; i < dependentCollectionLists.Count; i++)
                {
                    if (!string.IsNullOrEmpty(dependentCollectionLists[i].DependentFirstName) &&
                        !string.IsNullOrEmpty(dependentCollectionLists[i].DependentLastName) &&
                        !string.IsNullOrEmpty(dependentCollectionLists[i].DependentBirth))
                    {
                        DependentCollections dep = new DependentCollections();

                        dep.DependentFirstName = dependentCollectionLists[i].DependentFirstName;
                        dep.DependentLastName = dependentCollectionLists[i].DependentLastName;
                        dep.DependentName = dependentCollectionLists[i].DependentName;
                        dep.DependentBirth = dependentCollectionLists[i].DependentBirth;
                        dep.DependentRelationship = dependentCollectionLists[i].DependentRelationship;
                        dep.FirstnameID = dependentCollectionLists[i].FirstnameID;
                        dep.LastnameID = dependentCollectionLists[i].LastnameID;
                        dep.BirthID = dependentCollectionLists[i].BirthID;


                        fieldvalidationrequest.DependentCollection.Add(dep);

                    }
                }
         
            fieldvalidationrequest.Destination = fld_Destination.SelectedValue.ToString();
            fieldvalidationrequest.EmailAddress = emailAddress.Text.ToString();
            fieldvalidationrequest.EmployerBusinessName = fld_EmployerBusinessName.Text.ToString();
            fieldvalidationrequest.FirstName = firstName.Value.ToString();
            fieldvalidationrequest.FloorArea = string.IsNullOrEmpty(floorAreaText) ? 0 : Convert.ToDecimal(floorAreaText);
            fieldvalidationrequest.Gender = gender.SelectedValue.ToString();
            fieldvalidationrequest.GuardianBirthday = guardianbirthdate;
            fieldvalidationrequest.GuardianContactNo = "";
            fieldvalidationrequest.GuardianName = guardianFullName.Text.ToString();
            fieldvalidationrequest.GuardianRelationship = guardianrelationship;
            fieldvalidationrequest.IDNumber = idNumber.Text.ToString();
            fieldvalidationrequest.IntegrationId = Convert.ToInt32(Request.QueryString["INTID"].ToString());
            fieldvalidationrequest.IsCovidCoverage = ConvertToBoolean(rbCovidCoverage.SelectedValue);
            fieldvalidationrequest.IsExist = Convert.ToBoolean(Session["IsExist"].ToString());
            fieldvalidationrequest.IsWithVoucher = Convert.ToBoolean("False");
            fieldvalidationrequest.LastName = lastName.Value.ToString();
            fieldvalidationrequest.MiddleName = middleName.Value.ToString();
            fieldvalidationrequest.MobileNumber = contactNumber.Text.ToString();
            fieldvalidationrequest.MortgageeName = fld_MortgageeName.Text.ToString();
            fieldvalidationrequest.NameOfResident = fld_NameOfResident.Text.ToString();
            fieldvalidationrequest.Nationality = DDNationality.SelectedValue.ToString();
            fieldvalidationrequest.NatureOfOccupancy = DD_NatureOfOccupancy.SelectedValue.ToString();
            fieldvalidationrequest.NatureOfWork = natureofwork;
            fieldvalidationrequest.NoOfStoreys = string.IsNullOrEmpty(noOfStoreysText) ? 0 : Convert.ToInt64(noOfStoreysText);
            fieldvalidationrequest.NumberOfCOCs = long.Parse(NumberOfCOCs());
            fieldvalidationrequest.Occupation = fld_Occupation.Text.ToString();
            fieldvalidationrequest.OwnershipStatus = DD_OwnershipStatus.SelectedValue.ToString();
            fieldvalidationrequest.PassportNumber = fld_PassportNo.Text.ToString();
            fieldvalidationrequest.PlaceOfBirth = placeOfBirth.Text.ToString();
            fieldvalidationrequest.PartnerCode = Request.QueryString["PART"].ToString();
            fieldvalidationrequest.PetName = fld_PetName.Text.ToString();
            fieldvalidationrequest.PetBreed = fld_Breed.Text.ToString();
            fieldvalidationrequest.PetColor = fld_Color.Text.ToString();
            fieldvalidationrequest.PetGender = fld_Gender.SelectedValue.ToString();
            fieldvalidationrequest.PetBirth = petbirthdate;
            fieldvalidationrequest.PetAgeYear = GetPetYearAge();
            fieldvalidationrequest.PetAgeMonth = GetPetMonthAge();
            fieldvalidationrequest.PetPedigree = fld_PedigreeCertificateNo.Text.ToString();
            fieldvalidationrequest.PetRFID = fld_RFIDNo.Text.ToString();
            fieldvalidationrequest.PetYearlyTreatment = fld_PetYearlyTreatment.SelectedValue.ToString();
            fieldvalidationrequest.PetYearlyTreatmentDetails = fld_PetYearlyTreatmentDetails.Text.ToString();
            fieldvalidationrequest.PetHistory = fld_PetHistory.SelectedValue.ToString();
            fieldvalidationrequest.PetHistoryDetails = fld_PetHistoryDetails.Text.ToString();
            fieldvalidationrequest.PetVitamins = fld_PetVitamins.SelectedValue.ToString();
            fieldvalidationrequest.PetVitaminsDetails = fld_PetVitaminsDetails.Text.ToString();
            fieldvalidationrequest.PetCategory = fld_PetCategory.SelectedValue.ToString();
            fieldvalidationrequest.PhysicalDeformity = fld_PhysicalDeformity.SelectedValue.ToString();
            fieldvalidationrequest.PhysicalDeformityDetails = fld_PhysicalDeformityDetails.Text.ToString();
            fieldvalidationrequest.PreExistingIllness = fld_PreExistingIllness.SelectedValue.ToString();
            fieldvalidationrequest.PreExistingIllnessDetails = fld_PreExistingIllnessDetails.Text.ToString();
            fieldvalidationrequest.PlatformKey = ConfigurationManager.AppSettings["CLIBAPIKey"];
            fieldvalidationrequest.PlatformName = ConfigurationManager.AppSettings["CLIBPlatformName"];
            fieldvalidationrequest.ProviderCode = Request.QueryString["PCODE"].ToString();
            fieldvalidationrequest.PreviousLossStatus = DD_IsThereAnyPreviousLoss.Text.ToString();
            fieldvalidationrequest.ProductCode = Request.QueryString["PROD"].ToString();
            fieldvalidationrequest.PropertyAddress = fld_PropertyAddress.Text.ToString();
            fieldvalidationrequest.PropertyAge = string.IsNullOrEmpty(propertyAgeText) ? 0 : Convert.ToInt64(propertyAgeText);
            fieldvalidationrequest.PropertyCity = PDDLCity.SelectedValue.ToString();
            fieldvalidationrequest.PropertyMortgageStatus = DD_IsThePropertyMortgaged.SelectedValue.ToString();
            fieldvalidationrequest.PropertyProvince = PDDLProvince.SelectedValue.ToString();
            fieldvalidationrequest.Province = DDProvince.SelectedValue.ToString();
            fieldvalidationrequest.PurposeOfTravel = GetSelectedPlanValue();
            fieldvalidationrequest.ReferenceNumber = "";
            fieldvalidationrequest.SecondaryProductCode = SecondaryProduct();
            fieldvalidationrequest.SourceOfFunds = DDSourceOfFunds.SelectedValue.ToString();
            fieldvalidationrequest.Suffix = suffix.Value.ToString();
            fieldvalidationrequest.TinId = fld_TinId.Text.ToString();
            fieldvalidationrequest.TravelDurationDays = TravelDurationDays();
            fieldvalidationrequest.TravelDurationFrom = travelFrom.Text.ToString();
            fieldvalidationrequest.TravelDurationTo = travelTo.Text.ToString();
            fieldvalidationrequest.TypeBeams = GetTypeBeamsSelectedValue();
            fieldvalidationrequest.TypeColumns = GetTypeColumnsSelectedValue();
            fieldvalidationrequest.TypeExteriorWalls = GetTypeExteriorWallsSelectedValue();
            fieldvalidationrequest.TypeInnerPartitions = GetTypeInnerPartitionsSelectedValue();
            fieldvalidationrequest.TypeOfHome = DD_TypeOfHome.SelectedValue.ToString();
            fieldvalidationrequest.TypeRoof = GetTypeRoofSelectedValue();
            fieldvalidationrequest.TravelOrigin = fld_Origin.SelectedValue.ToString();
            fieldvalidationrequest.UserId = Request.QueryString["PART"].ToString();
            fieldvalidationrequest.ValidID = validIdDropDownList.SelectedValue.ToString();
            fieldvalidationrequest.VisaType = fld_VisaType.Text.ToString();
            fieldvalidationrequest.VoucherCode = "";
            fieldvalidationrequest.ZipCode = fld_ZipCode.Text.ToString();
        

            var returnValue = getList.FieldValidation(fieldvalidationrequest);
            message = returnValue.Message;

            if(returnValue.Message == "Validation Successful")
            {
                Session["ValidateSuccessfully"] = "True";
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "Swal.fire(`" + returnValue.Message + "`); ", true);
                return;
            }


    }
    #endregion

    #region GET PET MONTH AGE
    public int GetPetMonthAge()
    {
        int monthAge = 0;

        if (Session["CategoryId"].ToString() == "8")
        {
            // Assuming fld_PetBirthdate is a DateTime object or can be parsed to DateTime
            DateTime petBirthdate = DateTime.Parse(fld_PetBirthdate.Text.Trim());

            // Calculate the difference in months between the birthdate and the current date
            int monthsDiff = (DateTime.Now.Year - petBirthdate.Year) * 12 + DateTime.Now.Month - petBirthdate.Month;

            // Adjust month age if the current day of the month is less than the birthdate's day
            if (DateTime.Now.Day < petBirthdate.Day)
            {
                monthsDiff--;
            }

            monthAge = monthsDiff;
        }
        else
        {
            monthAge = 0;
        }


        return monthAge;
    }
    #endregion

    #region GET PET YEAR AGE
    public int GetPetYearAge()
    {
        int yearAge = 0;
        if (Session["CategoryId"].ToString() == "8")
        {
            // Assuming fld_PetBirthdate is a DateTime object or can be parsed to DateTime
            DateTime petBirthdate = DateTime.Parse(fld_PetBirthdate.Text.Trim());

            // Calculate the difference in years between the birthdate and the current date
            int yearsDiff = DateTime.Now.Year - petBirthdate.Year;

            // Adjust year age if the current month is earlier than the birthdate's month,
            // or if the current month is the same as the birthdate's month but the current day
            // is earlier than the birthdate's day.
            if (DateTime.Now.Month < petBirthdate.Month ||
                (DateTime.Now.Month == petBirthdate.Month && DateTime.Now.Day < petBirthdate.Day))
            {
                yearsDiff--;
            }

            yearAge = yearsDiff;
        }
        else
        {
            yearAge = 0;
        }
       
        return yearAge;
    }

    #endregion

    #region SECONDARY PRODUCT
    private string SecondaryProduct()
    {
        string results = Session["CategoryId"].ToString();
        if (Session["CategoryId"].ToString() == "11")
        {
            results = Session["SecondaryProductCode"].ToString();
        }
        else
        {
            results = "";
        }

        return results;
    }
    #endregion

    #region TRAVEL DURATION DAYS
    private int TravelDurationDays()
    {
        int travelduration = 0; // Set default value to 0

        if (!string.IsNullOrEmpty(numberOfDays.Text))
        {
            int result;
            if (int.TryParse(numberOfDays.Text, out result))
            {
                travelduration = result;
            }
        }

        return travelduration;
    }
    #endregion

    #region SavingInformationValidation
    public void SavingInformationValidation()
    {
        try
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

            #region CONTACT INFOMATION FIELDS

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
            if (string.IsNullOrEmpty(beneFullname.Text) && Session["CategoryId"].ToString() != "6")
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "Swal.fire('Please Enter Beneficiary Full Name.');", true);
                return;
            }
            if (relationshipDropDownList.SelectedValue == "Select" && Session["CategoryId"].ToString() != "6")
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "Swal.fire('Please Select Relationship to the Insured.');", true);
                return;
            }
            #endregion

            #region GUARDIAN FIELDS

            if (string.IsNullOrEmpty(guardianFullName.Text) && Session["IsMinor"].ToString() == "True" && Session["CategoryId"].ToString() != "6")
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "Swal.fire('Please Enter Guardian Full Name.');", true);
                return;
            }
            if (string.IsNullOrEmpty(guardianBirthDate.Text) && Session["IsMinor"].ToString() == "True" && Session["CategoryId"].ToString() != "6")
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "Swal.fire('Please Enter Guardian Birthdate.');", true);
                return;
            }
            if (guardianRelationshipDropDownList.SelectedValue == "Select" && Session["IsMinor"].ToString() == "True" && Session["CategoryId"].ToString() != "6")
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "Swal.fire('Please Select Guardian Relationship.');", true);
                return;
            }
            #endregion

            #region FIRE PROPERTY FIELDS 

            if (string.IsNullOrEmpty(fld_PropertyAddress.Text) && Session["CategoryId"].ToString() == "6")
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "Swal.fire('Please Enter Full Address of Property to be insured.');", true);
                return;
            }
            if (PDDLProvince.SelectedValue == "Select" && Session["CategoryId"].ToString() == "6")
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "Swal.fire('Please Select Province.');", true);
                return;
            }
            if (PDDLCity.SelectedValue == "Select" && Session["CategoryId"].ToString() == "6")
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "Swal.fire('Please Select City.');", true);
                return;
            }
            if (string.IsNullOrEmpty(fld_NameOfResident.Text) && Session["CategoryId"].ToString() == "6")
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "Swal.fire('Please Enter Name of Resident.');", true);
                return;
            }
            if (DD_OwnershipStatus.SelectedValue == "Select" && Session["CategoryId"].ToString() == "6")
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "Swal.fire('Please Select Ownership Status.');", true);
                return;
            }
            if (string.IsNullOrEmpty(fld_PropertyAge.Text) && Session["CategoryId"].ToString() == "6")
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "Swal.fire('Please Enter Age of Home.');", true);
                return;
            }
            if (string.IsNullOrEmpty(fld_FloorArea.Text) && Session["CategoryId"].ToString() == "6")
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "Swal.fire('Please Enter Floor area.');", true);
                return;
            }
            if (string.IsNullOrEmpty(fld_NoOfStoreys.Text) && Session["CategoryId"].ToString() == "6")
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "Swal.fire('Please Enter No. of Storeys.');", true);
                return;
            }
            if (DD_IsThePropertyMortgaged.SelectedValue == "Select" && Session["CategoryId"].ToString() == "6")
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "Swal.fire('Please Select property Mortgaged.');", true);
                return;
            }
            if (DD_IsThePropertyMortgaged.SelectedValue == "Yes" && string.IsNullOrEmpty(fld_MortgageeName.Text) && Session["CategoryId"].ToString() == "6")
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "Swal.fire('Please Enter Name of Mortgagee.');", true);
                return;
            }
            if (DD_NatureOfOccupancy.SelectedValue == "Select" && Session["CategoryId"].ToString() == "6")
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "Swal.fire('Please Select Nature of Occupancy.');", true);
                return;
            }
            if (DD_TypeOfHome.SelectedValue == "Select" && Session["CategoryId"].ToString() == "6")
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "Swal.fire('Please Select Type of Home.');", true);
                return;
            }
            if (DD_TypeExteriorWalls.SelectedValue == "Select" && Session["CategoryId"].ToString() == "6")
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "Swal.fire('Please Select Type of Exterior Walls.');", true);
                return;
            }
            if (DD_TypeExteriorWalls.Text == "Others: Please Specify" && string.IsNullOrEmpty(otherInputTypeExteriorWalls.Text))
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "Swal.fire('Please Specify Type of Exterior Walls');", true);
                return;
            }
            if (DD_TypeRoof.SelectedValue == "Select" && Session["CategoryId"].ToString() == "6")
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "Swal.fire('Please Select Type of Roofs.');", true);
                return;
            }
            if (DD_TypeRoof.Text == "Others: Please Specify" && string.IsNullOrEmpty(otherInputTypeRoof.Text))
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "Swal.fire('Please Specify Type of Roof');", true);
                return;
            }
            if (DD_TypeInnerPartitions.SelectedValue == "Select" && Session["CategoryId"].ToString() == "6")
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "Swal.fire('Please Select Type of Inner Partitions.');", true);
                return;
            }
            if (DD_TypeInnerPartitions.Text == "Others: Please Specify" && string.IsNullOrEmpty(otherInputTypeInnerPartitions.Text))
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "Swal.fire('Please Specify Type Inner of Partitions');", true);
                return;
            }
            if (DD_TypeBeams.SelectedValue == "Select" && Session["CategoryId"].ToString() == "6")
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "Swal.fire('Please Select Type of Beams.');", true);
                return;
            }
            if (DD_TypeBeams.Text == "Others: Please Specify" && string.IsNullOrEmpty(otherInputTypeBeams.Text))
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "Swal.fire('Please Specify Type of Beams');", true);
                return;
            }
            if (DD_TypeColumns.SelectedValue == "Select" && Session["CategoryId"].ToString() == "6")
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "Swal.fire('Please Select Type of Columns.');", true);
                return;
            }
            if (DD_TypeColumns.Text == "Others: Please Specify" && string.IsNullOrEmpty(otherInputTypeColumns.Text))
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "Swal.fire('Please Specify Type of Columns');", true);
                return;
            }
            if (string.IsNullOrEmpty(fld_BoundaryFront.Text) && Session["CategoryId"].ToString() == "6")
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "Swal.fire('Please Enter Front Boundary.');", true);
                return;
            }
            if (string.IsNullOrEmpty(fld_BoundaryRear.Text) && Session["CategoryId"].ToString() == "6")
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "Swal.fire('Please Enter Rear Boundary.');", true);
                return;
            }
            if (string.IsNullOrEmpty(fld_BoundaryLeft.Text) && Session["CategoryId"].ToString() == "6")
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "Swal.fire('Please Enter Left Boundary.');", true);
                return;
            }
            if (string.IsNullOrEmpty(fld_BoundaryRight.Text) && Session["CategoryId"].ToString() == "6")
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "Swal.fire('Please Enter Right Boundary.');", true);
                return;
            }
            if (DD_IsThereAnyPreviousLoss.SelectedValue == "Select" && Session["CategoryId"].ToString() == "6")
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "Swal.fire('Please Select any previous loss.');", true);
                return;
            }
            if (DD_IsThereAnyPreviousLoss.SelectedValue == "Yes" && string.IsNullOrEmpty(fld_Remarks.Text) && Session["CategoryId"].ToString() == "6")
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "Swal.fire('Please indicate date and detail of incident.');", true);
                return;
            }
            if (string.IsNullOrEmpty(fld_BodyOfWaterDistance.Text) && Session["CategoryId"].ToString() == "6")
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "Swal.fire('Please Enter Distance from bodies of water.');", true);
                return;
            }
            if (DD_TypeOfDependents.SelectedValue == "0")
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "Swal.fire('Please select type of dependent');", true);
                return;
            }
            #endregion

            #region FAMILY FIELDS
            if (firstnamehasvalue == false && Session["CategoryId"].ToString() == "10")
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "Swal.fire('Please fill in all the required information for the dependent fields.');", true);
                return;
            }
            if (lastnamehasvalue == false && Session["CategoryId"].ToString() == "10")
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "Swal.fire('Please fill in all the required information for the dependent fields.');", true);
                return;
            }
            if (birthdayhasvalue == false && Session["CategoryId"].ToString() == "10")
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "Swal.fire('Please fill in all the required information for the dependent fields.');", true);
                return;
            }
            if (allhasvalue == false && Session["CategoryId"].ToString() == "10")
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "Swal.fire('At least 1 dependent is required.');", true);
                return;
            }
            #endregion

            #region TRAVEL FIELDS

            if (fld_Origin.SelectedValue == "Select" && Session["CategoryId"].ToString() == "11")
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "Swal.fire('Please Select Travel Origin.');", true);
                return;
            }

            if (fld_Destination.SelectedValue == "Select" && Session["CategoryId"].ToString() == "11")
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "Swal.fire('Please Select Travel Destination.');", true);
                return;
            }
            if (fld_VisaType.SelectedValue == "Select" && Session["ProductCode"].ToString() == "FPGIT" && Session["CategoryId"].ToString() == "11")
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "Swal.fire('Please Select Travel Visa Type.');", true);
                return;
            }
            if (!basicRadio.Checked && !completeRadio.Checked && Session["CategoryId"].ToString() == "11")
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "Swal.fire('Please select at least one purpose of your travel: business or leisure.');", true);
                return;
            }
            if (string.IsNullOrEmpty(travelFrom.Text.ToString()) && Session["CategoryId"].ToString() == "11")
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "Swal.fire('Please Select Date Travel From.');", true);
                return;
            }
            if (string.IsNullOrEmpty(travelTo.Text.ToString()) && Session["CategoryId"].ToString() == "11")
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "Swal.fire('Please Select Date Travel To.');", true);
                return;
            }
            if (string.IsNullOrEmpty(numberOfDays.Text.ToString()) && Session["CategoryId"].ToString() == "11")
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "Swal.fire('Please Select Date Travel From and Travel to to calculate the number travel days.');", true);
                return;
            }
            if (string.IsNullOrEmpty(fld_BookingReferenceNo.Text.ToString()) && Session["CategoryId"].ToString() == "11")
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "Swal.fire('Please Enter Booking Reference Number');", true);
                return;
            }
            if (string.IsNullOrEmpty(fld_PassportNo.Text.ToString()) && Session["ProductCode"].ToString() == "FPGIT" && Session["CategoryId"].ToString() == "11")
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "Swal.fire('Please Enter Passport Number');", true);
                return;
            }
            if (rbCovidCoverage.SelectedItem == null && Session["CategoryId"].ToString() == "11" && Session["CheckIfCovidCoverage"].ToString() == "True")
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "Swal.fire('Please choose at least one option for Covid-19 coverage.');", true);
                return;
            }
            if (Session["CategoryId"].ToString() == "11" && Session["DisplayCoverageModule"].ToString() == "True")
            {
                foreach (RepeaterItem item in repeaterQuestions.Items)
                {
                    RadioButtonList rblYesNo = item.FindControl("rblYesNo") as RadioButtonList;

                    if (string.IsNullOrEmpty(rblYesNo.SelectedValue) && Session["CategoryId"].ToString() == "11")
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "alert", "Swal.fire('Please answer all questions in the Optional Coverage Section.');", true);
                        return;
                    }
                }
            }
            #endregion

            #region PET FIELDS
            if (string.IsNullOrEmpty(fld_PhysicalDeformity.SelectedValue)  && Session["CategoryId"].ToString() == "8")
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "Swal.fire('Please Select physical deformity.');", true);
                return;
            }
            if (fld_PhysicalDeformity.SelectedValue == "Yes" && string.IsNullOrEmpty(fld_PhysicalDeformityDetails.Text) && Session["CategoryId"].ToString() == "8")
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "Swal.fire('Please specify physical deformity details.');", true);
                return;
            }
            if (string.IsNullOrEmpty(fld_PreExistingIllness.SelectedValue) && Session["CategoryId"].ToString() == "8")
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "Swal.fire('Please Select pre-existing illnesses.');", true);
                return;
            }
            if (fld_PreExistingIllness.SelectedValue == "Yes" && string.IsNullOrEmpty(fld_PreExistingIllnessDetails.Text) && Session["CategoryId"].ToString() == "8")
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "Swal.fire('Please specify pre-existing illnesses details');", true);
                return;
            }
            if (string.IsNullOrEmpty(fld_PetCategory.SelectedValue) && Session["CategoryId"].ToString() == "8")
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "Swal.fire('Please Select Type of Pet.');", true);
                return;
            }
            if (string.IsNullOrEmpty(fld_Gender.SelectedValue) && Session["CategoryId"].ToString() == "8")
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "Swal.fire('Please Select Pet Gender.');", true);
                return;
            }
            if(string.IsNullOrEmpty(fld_PetName.Text) && Session["CategoryId"].ToString() == "8")
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "Swal.fire('Please Enter Pet Name.');", true);
                return;
            }
            if (string.IsNullOrEmpty(fld_Breed.Text) && Session["CategoryId"].ToString() == "8")
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "Swal.fire('Please Enter Pet Breed.');", true);
                return;
            }
            if (string.IsNullOrEmpty(fld_Color.Text) && Session["CategoryId"].ToString() == "8")
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "Swal.fire('Please Enter Pet Color.');", true);
                return;
            }
            if (string.IsNullOrEmpty(fld_PetBirthdate.Text) && Session["CategoryId"].ToString() == "8")
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "Swal.fire('Please Select Pet Date of Birth.');", true);
                return;
            }
            if (string.IsNullOrEmpty(fld_PetYearlyTreatment.SelectedValue) && Session["CategoryId"].ToString() == "8")
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "Swal.fire('Please Select Pet Yearly Treatment Details.');", true);
                return;
            }
            if (fld_PetYearlyTreatment.SelectedValue == "Yes" && string.IsNullOrEmpty(fld_PetYearlyTreatmentDetails.Text) && Session["CategoryId"].ToString() == "8")
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "Swal.fire('Please specify Pet Yearly Treatment Details.');", true);
                return;
            }
            if (string.IsNullOrEmpty(fld_PetHistory.SelectedValue) && Session["CategoryId"].ToString() == "8")
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "Swal.fire('Please Select Pet History.');", true);
                return;
            }
            if (fld_PetHistory.SelectedValue == "Yes" && string.IsNullOrEmpty(fld_PetHistoryDetails.Text) && Session["CategoryId"].ToString() == "8")
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "Swal.fire('Please specify Pet History Details');", true);
                return;
            }
            if (string.IsNullOrEmpty(fld_PetVitamins.SelectedValue) && Session["CategoryId"].ToString() == "8")
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "Swal.fire('Please Select Pet Vitamins.');", true);
                return;
            }
            if (fld_PetVitamins.SelectedValue == "Yes" && string.IsNullOrEmpty(fld_PetVitaminsDetails.Text) && Session["CategoryId"].ToString() == "8")
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "Swal.fire('Please specify Pet Vitamins / Medication Details.');", true);
                return;
            }
            #endregion

            #region DATA PRIVACY / TERMS AND CONDITION
            if (dataPrivacy1Checkbox.Checked == false || dataPrivacy2Checkbox.Checked == false)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "Swal.fire('Please read and check our Data Privacy policy and Terms and conditions before proceeding.');", true);
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
                if (baseResult.ResultStatus == ResultType.Success)
                {
                    //PurchaseSavingInformation();
                    OpenPaymentChannel();
                    //DispalyPreview();
                }

            }
            #endregion
        }
        catch (Exception ex)
        {
            SystemUtility.EventLog.SaveError(ex.ToString());
            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "Swal.fire(`" + ex + "`); ", true);
            throw;
        }

    }
    #endregion

    #region GENERATION OF REFERENCE NUMBER
    public void GenerateReferenceNumber()
    {
        string yearIQR = DateTime.Now.Year.ToString().Substring(2);      
        string monthIQR = DateTime.Now.Month.ToString("00");
        string dateIQR = DateTime.Now.Day.ToString("00");
        string timeIQR = DateTime.Now.ToString("HHmm");                   
        string secondsIQR = DateTime.Now.Second.ToString("00");         
        string millisecondsIQR = DateTime.Now.Millisecond.ToString("00");

        partnerCode = partnerCode.PadRight(3, '0').Substring(0, 3);
        yearIQR = yearIQR.PadRight(2, '0').Substring(0, 2);
        monthIQR = monthIQR.PadRight(2, '0').Substring(0, 2);
        dateIQR = dateIQR.PadRight(2, '0').Substring(0, 2);
        timeIQR = timeIQR.PadRight(4, '0').Substring(0, 4);
        secondsIQR = secondsIQR.PadRight(2, '0').Substring(0, 2);
        millisecondsIQR = millisecondsIQR.PadRight(2, '0').Substring(0, 2);

        referenceno = partnerCode + yearIQR + monthIQR + dateIQR + timeIQR + secondsIQR + millisecondsIQR;
    }
    #endregion

    #region IQR CODE SAVING CLIENT KYC
    public void PurchaseSavingInformation()
    {
        populatedatafromtextboxes();

        #region SAVING CLIENT INFORMATION

            #region GENERATE REFERENCE NUMBER
            GenerateReferenceNumber();
            #endregion

              #region GENERATE EMAIL DETAILS
             string refence17digits = referenceno.ToString().Substring(0, 17);

            Session["ReferenceCode"] = refence17digits;
            Session["firstName"] = firstName.Value.ToString();
            Session["middleName"] = middleName.Value.ToString();
            Session["lastName"] = lastName.Value.ToString();
            Session["suffix"] = suffix.Value.ToString();
            Session["dateOfBirth"] = birthDateTextBox.Text.ToString();
            Session["contactNumber"] = contactNumber.Text.Trim();
            Session["emailAddress"] = emailAddress.Text.ToString();
            string issuedate = DateTime.Now.ToString("MM/dd/yyyy");
            Session["issueDate"] = issuedate.ToString();
            Session["DeadLineOfPayment"] = DateTime.Now.AddDays(5).ToString("MM/dd/yyyy");
            Session["ProductName"].ToString();
            Session["ProductDescription"].ToString();
            Session["Premium"].ToString();
            Session["success"] = "sucess";
            Session["partnerCode"] = partnerCode;
            Session["Adddress"] = presentAddress.Text.ToString();
            Session["City"] = DDcity.SelectedValue.ToString();
            Session["Province"] = DDProvince.SelectedValue.ToString();
            Session["PostalCode"] = fld_ZipCode.Text.ToString();
            #endregion

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

            string message = "";

            #region ISMINOR
            if (Session["IsMinor"].ToString() == "True" && Session["CategoryId"].ToString() != "6")
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

            #region RELATIONSHIP EMPTY STRING
            if (relationshipDropDownList.SelectedValue == "Select")
            {
                beneficiaryrelationship = null;
            }
            else
            {
                beneficiaryrelationship = relationshipDropDownList.SelectedValue.ToString();
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

            birthdate = DateTime.Parse(birthDateTextBox.Text.Trim()).ToString("MM/dd/yyyy");

            #endregion

            if (Session["CategoryId"].ToString() == "8")
            {

              string purchasepetbirthdate = DateTime.Parse(fld_PetBirthdate.Text.Trim()).ToString("MM/dd/yyyy");
                 #region IQR PET PRODUCTS
                 HealthDeclarationRequest petrequest = new HealthDeclarationRequest();
                token.Token = generateToken.GenerateTokenAuth();
                petrequest.Token = token.Token;
                petrequest.Address = presentAddress.Text.ToString();
                petrequest.Barangay = fld_Baranggay.Text.ToString();
                petrequest.BeneficiaryName = beneFullname.Text.ToString();
                petrequest.BeneficiaryRelationship = beneficiaryrelationship;
                petrequest.Birthdate = birthdate;
                petrequest.CategoryCode = categoryCode;
                petrequest.City = DDcity.SelectedValue.ToString();
                petrequest.CivilStatus = civilStatus.SelectedValue.ToString();
                petrequest.DateTimeFormat = "MM/dd/yyyy";
                petrequest.EmailAddress = emailAddress.Text.ToString();
                petrequest.EmployerBusinessName = fld_EmployerBusinessName.Text.ToString();
                petrequest.FirstName = firstName.Value.ToString();
                petrequest.Gender = gender.SelectedValue.ToString();
                petrequest.GuardianBirthday = guardianbirthdate;
                petrequest.GuardianContactNo = "";
                petrequest.GuardianName = guardianFullName.Text.ToString();
                petrequest.GuardianRelationship = guardianrelationship;
                petrequest.IDNumber = idNumber.Text.ToString();
                petrequest.IntegrationId = Convert.ToInt32(Request.QueryString["INTID"].ToString());
                petrequest.IsExist = Convert.ToBoolean(Session["IsExist"].ToString());
                petrequest.IsWithVoucher = Convert.ToBoolean("False");
                petrequest.LastName = lastName.Value.ToString();
                petrequest.MiddleName = middleName.Value.ToString();
                petrequest.MobileNumber = contactNumber.Text.ToString();
                petrequest.Nationality = DDNationality.SelectedValue.ToString();
                petrequest.NatureOfWork = natureofwork;
                petrequest.NumberOfCOCs = long.Parse(numberInput.Text);
                petrequest.Occupation = fld_Occupation.Text.ToString();
                petrequest.PartnerCode = Request.QueryString["PART"].ToString();
                petrequest.PetAgeMonth = GetPetMonthAge();
                petrequest.PetAgeYear = GetPetYearAge();
                petrequest.PetBirth = purchasepetbirthdate;
                petrequest.PetBreed = fld_Breed.Text.ToString();
                petrequest.PetCategory = fld_PetCategory.SelectedValue.ToString();
                petrequest.PetColor = fld_Color.Text.ToString();
                petrequest.PetGender = fld_Gender.Text.ToString();
                petrequest.PetHistory = fld_PetHistory.SelectedValue.ToString();
                petrequest.PetHistoryDetails = fld_PetHistoryDetails.Text.ToString();
                petrequest.PetName = fld_PetName.Text.ToString();
                petrequest.PetPedigree = fld_PedigreeCertificateNo.Text.ToString();
                petrequest.PetRFID = fld_RFIDNo.Text.ToString();
                petrequest.PetVitamins = fld_PetVitamins.SelectedValue.ToString();
                petrequest.PetVitaminsDetails = fld_PetVitaminsDetails.Text.ToString();
                petrequest.PetYearlyTreatment = fld_PetYearlyTreatment.SelectedValue.ToString();
                petrequest.PetYearlyTreatmentDetails = fld_PetYearlyTreatmentDetails.Text.ToString();
                petrequest.PhysicalDeformity = fld_PetHistory.SelectedValue.ToString();
                petrequest.PhysicalDeformityDetails = fld_PetHistoryDetails.Text.ToString();
                petrequest.PlaceOfBirth = placeOfBirth.Text.ToString();
                petrequest.PlatformKey = ConfigurationManager.AppSettings["CLIBAPIKey"];
                petrequest.PlatformName = ConfigurationManager.AppSettings["CLIBPlatformName"];
                petrequest.PreExistingIllness = fld_PreExistingIllness.SelectedValue.ToString();
                petrequest.PreExistingIllnessDetails = fld_PreExistingIllnessDetails.Text.ToString();
                petrequest.ProductCode = Request.QueryString["PROD"].ToString();
                petrequest.ProviderCode = Request.QueryString["PCODE"].ToString();
                petrequest.Province = DDProvince.SelectedValue.ToString();
                petrequest.ReferenceNumber = Session["ReferenceCode"].ToString();
                petrequest.SourceOfFunds = DDSourceOfFunds.SelectedValue.ToString();
                petrequest.Suffix = suffix.Value.ToString();
                petrequest.TinId = fld_TinId.Text.ToString();
                petrequest.UserId = Request.QueryString["PART"].ToString();
                petrequest.ValidID = validIdDropDownList.SelectedValue.ToString();
                petrequest.VoucherCode = "";
                petrequest.ZipCode = fld_ZipCode.Text.ToString();
                petrequest.AgentCode = GetAgentCode();
                petrequest.ReferralCode = GetReferralCode();
                petrequest.Remarks = RemarkDiscountFormat(GetReferralCode());


                var returnValue = getList.IQRPurchasePet(petrequest);
                message = returnValue.Message;

                if (returnValue.Message == "Transaction Successful")
                {
                #region SEND EMAIL NOTFICIATION

                    var cocNumbers = new StringBuilder();
                    foreach (var result in returnValue.Result)
                    {
                        cocNumbers.Append(result.COCNumber);
                        cocNumbers.Append(", ");
                        // Add a separator between COCNumbers
                    }
                    // Remove the last ", " separator if there are COCNumbers present
                    if (returnValue.Result.Count > 0)
                    {
                        cocNumbers.Remove(cocNumbers.Length - 2, 2);
                    }
                    Session["cocNumber"] = cocNumbers.ToString();
                    Session["cocNumber"] = returnValue.Result[0].COCNumber.ToString();
                    Session["COCEffectiveDateBasis"] = returnValue.Result[0].EffectiveDate.ToString();
                    Session["Premium"] = returnValue.Result[0].Premium;
                    Session["TerminationDate"] = returnValue.Result[0].TerminationDate.ToString();



                    SMSContent();
                    EmailContent();
                    #endregion
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "Swal.fire(`" + returnValue.Message + "`); ", true);
                    fld_ReferralCode.Text = null;
                    EnableReferralElements();
                    return;
                }

            #endregion
            }
            if (Session["CategoryId"].ToString() == "10")
            {
                 #region IQR FAMILY PRODUCTS
                FamilyRequest familyRequest = new FamilyRequest();
                token.Token = generateToken.GenerateTokenAuth();
                familyRequest.Token = token.Token;
                familyRequest.Address = presentAddress.Text.ToString();
                familyRequest.Barangay = fld_Baranggay.Text.ToString();
                familyRequest.BeneficiaryName = beneFullname.Text.ToString();
                familyRequest.BeneficiaryRelationship = beneficiaryrelationship;
                familyRequest.Birthdate = birthdate;
                familyRequest.CategoryCode = categoryCode;
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
                familyRequest.IntegrationId = Convert.ToInt32(Request.QueryString["INTID"].ToString());
                familyRequest.IsExist = Convert.ToBoolean(Session["IsExist"].ToString());
                familyRequest.IsWithVoucher = Convert.ToBoolean("False");
                familyRequest.LastName = lastName.Value.ToString();
                familyRequest.MiddleName = middleName.Value.ToString();
                familyRequest.MobileNumber = contactNumber.Text.ToString();
                familyRequest.Nationality = DDNationality.SelectedValue.ToString();
                familyRequest.NatureOfWork = natureofwork;
                familyRequest.NumberOfCOCs = long.Parse(numberInput.Text);
                familyRequest.Occupation = fld_Occupation.Text.ToString();
                familyRequest.PartnerCode = Request.QueryString["PART"].ToString();
                familyRequest.PlaceOfBirth = placeOfBirth.Text.ToString();
                familyRequest.PlatformName = ConfigurationManager.AppSettings["CLIBPlatformName"];
                familyRequest.PlatformKey = ConfigurationManager.AppSettings["CLIBAPIKey"];
                familyRequest.ProductCode = Request.QueryString["PROD"].ToString();
                familyRequest.ProviderCode = Request.QueryString["PCODE"].ToString();
                familyRequest.Province = DDProvince.SelectedValue.ToString();
                familyRequest.ReferenceNumber = Session["ReferenceCode"].ToString();
                familyRequest.SourceOfFunds = DDSourceOfFunds.SelectedValue.ToString();
                familyRequest.Suffix = suffix.Value.ToString();
                familyRequest.TinId = fld_TinId.Text.ToString();
                familyRequest.UserId = Request.QueryString["PART"].ToString();
                familyRequest.ValidID = validIdDropDownList.SelectedValue.ToString();
                familyRequest.VoucherCode = "";
                familyRequest.ZipCode = fld_ZipCode.Text.ToString();
                familyRequest.AgentCode = GetAgentCode();
                familyRequest.ReferralCode = GetReferralCode();
                familyRequest.Remarks = RemarkDiscountFormat(GetReferralCode());


                var returnValue = getList.IQRPurchaseFamily(familyRequest);
                message = returnValue.Message;

                if (returnValue.Message == "Transaction Successful")
                {
                #region SEND EMAIL NOTFICIATION

                    var cocNumbers = new StringBuilder();
                    foreach (var result in returnValue.Result)
                    {
                        cocNumbers.Append(result.COCNumber);
                        cocNumbers.Append(", ");
                        // Add a separator between COCNumbers
                    }
                    // Remove the last ", " separator if there are COCNumbers present
                    if (returnValue.Result.Count > 0)
                    {
                        cocNumbers.Remove(cocNumbers.Length - 2, 2);
                    }
                    Session["cocNumber"] = cocNumbers.ToString();
                    Session["COCEffectiveDateBasis"] = returnValue.Result[0].EffectiveDate.ToString();
                    Session["Premium"] = returnValue.Result[0].Premium;
                    Session["TerminationDate"] = returnValue.Result[0].TerminationDate.ToString();

                    SMSContent();
                    EmailContent();

                    #endregion
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "Swal.fire(`" + returnValue.Message + "`); ", true);
                    fld_ReferralCode.Text = null; 
                    EnableReferralElements();
                    return;
                }
                #endregion
            }
            if (Session["CategoryId"].ToString() == "11")
            {
                 #region IQR TRAVEL PRODUCTS
                List<OptionalCoverageCollection> optionalCoverageList = new List<OptionalCoverageCollection>();
                TravelRequest travelrequest = new TravelRequest();
                token.Token = generateToken.GenerateTokenAuth();
                travelrequest.Token = token.Token;
                travelrequest.Address = presentAddress.Text.ToString();
                travelrequest.Barangay = fld_Baranggay.Text.ToString();
                travelrequest.BeneficiaryName = beneFullname.Text.ToString();
                travelrequest.BeneficiaryRelationship = beneficiaryrelationship;
                travelrequest.Birthdate = birthdate;
                travelrequest.BookingReferenceNumber = fld_BookingReferenceNo.Text.ToString();
                travelrequest.CategoryCode = categoryCode;
                travelrequest.City = DDcity.SelectedValue.ToString();
                travelrequest.CivilStatus = civilStatus.SelectedValue.ToString();
                travelrequest.DateTimeFormat = "MM/dd/yyyy";
                travelrequest.Destination = fld_Destination.SelectedValue.ToString();
                travelrequest.EmailAddress = emailAddress.Text.ToString();
                travelrequest.EmployerBusinessName = fld_EmployerBusinessName.Text.ToString();
                travelrequest.FirstName = firstName.Value.ToString();
                travelrequest.Gender = gender.SelectedValue.ToString();
                travelrequest.GuardianBirthday = guardianbirthdate;
                travelrequest.GuardianContactNo = "";
                travelrequest.GuardianName = guardianFullName.Text.ToString();
                travelrequest.GuardianRelationship = guardianrelationship;
                travelrequest.IDNumber = idNumber.Text.ToString();
                travelrequest.IntegrationId = Convert.ToInt32(Request.QueryString["INTID"].ToString());
                travelrequest.IsCovidCoverage = ConvertToBoolean(rbCovidCoverage.SelectedValue);
                travelrequest.IsExist = Convert.ToBoolean(Session["IsExist"].ToString());
                travelrequest.IsWithVoucher = Convert.ToBoolean("False");
                travelrequest.LastName = lastName.Value.ToString();
                travelrequest.MiddleName = middleName.Value.ToString();
                travelrequest.MobileNumber = contactNumber.Text.ToString();
                travelrequest.Nationality = DDNationality.SelectedValue.ToString();
                travelrequest.NatureOfWork = natureofwork;
                travelrequest.NumberOfCOCs = long.Parse(numberInput.Text);
                travelrequest.TravelOrigin = fld_Origin.SelectedValue.ToString();
                travelrequest.Occupation = fld_Occupation.Text.ToString();

                foreach (RepeaterItem item in repeaterQuestions.Items)
                {
                    RadioButtonList rblYesNo = item.FindControl("rblYesNo") as RadioButtonList;
                    HiddenField hdnQuestionNo = item.FindControl("hdnQuestionNo") as HiddenField;
                    int questionNo = Convert.ToInt32(hdnQuestionNo.Value);

                    Console.WriteLine("QuestionNo: " + questionNo);

                    OptionalCoverageCollection optionalCoverage = new OptionalCoverageCollection
                    {
                        Answer = rblYesNo.SelectedValue == "Yes",
                        QuestionNo = questionNo
                    };

                    optionalCoverageList.Add(optionalCoverage);
                }

                travelrequest.OptionalCoverageCollections = optionalCoverageList;
                travelrequest.PartnerCode = Request.QueryString["PART"].ToString();
                travelrequest.PassportNumber = fld_PassportNo.Text.ToString();
                travelrequest.PlaceOfBirth = placeOfBirth.Text.ToString();
                travelrequest.PlatformKey = ConfigurationManager.AppSettings["CLIBAPIKey"];
                travelrequest.PlatformName = ConfigurationManager.AppSettings["CLIBPlatformName"];
                travelrequest.ProductCode = Request.QueryString["PROD"].ToString();
                travelrequest.ProviderCode = Request.QueryString["PCODE"].ToString();
                travelrequest.Province = DDProvince.SelectedValue.ToString();
                travelrequest.PurposeOfTravel = GetSelectedPlanValue();
                travelrequest.ReferenceNumber = Session["ReferenceCode"].ToString();
                travelrequest.SecondaryProductCode = Session["SecondaryProductCode"].ToString();
                travelrequest.SourceOfFunds = DDSourceOfFunds.SelectedValue.ToString();
                travelrequest.Suffix = suffix.Value.ToString();
                travelrequest.TinId = fld_TinId.Text.ToString();
                travelrequest.TravelDurationDays = Convert.ToInt32(numberOfDays.Text.ToString());
                travelrequest.TravelDurationFrom = travelFrom.Text.ToString();
                travelrequest.TravelDurationTo = travelTo.Text.ToString();
                travelrequest.UserId = Request.QueryString["PART"].ToString();
                travelrequest.ValidID = validIdDropDownList.SelectedValue.ToString();
                travelrequest.VisaType = fld_VisaType.Text.ToString();
                travelrequest.VoucherCode = "";
                travelrequest.ZipCode = fld_ZipCode.Text.ToString();

                travelrequest.AgentCode = GetAgentCode();
                travelrequest.ReferralCode = GetReferralCode();
                travelrequest.Remarks = RemarkDiscountFormat(GetReferralCode());


            var returnValue = getList.IQRPurchaseTravel(travelrequest);
                message = returnValue.Message;

                if (returnValue.Message == "Transaction Successful")
                {
                #region SEND EMAIL NOTFICIATION

                    var cocNumbers = new StringBuilder();
                    foreach (var result in returnValue.Result)
                    {
                        cocNumbers.Append(result.COCNumber);
                        cocNumbers.Append(", ");
                        // Add a separator between COCNumbers
                    }
                    // Remove the last ", " separator if there are COCNumbers present
                    if (returnValue.Result.Count > 0)
                    {
                        cocNumbers.Remove(cocNumbers.Length - 2, 2);
                    }

                    Session["cocNumber"] = cocNumbers.ToString();
                    Session["COCEffectiveDateBasis"] = returnValue.Result[0].EffectiveDate.ToString();
                    Session["Premium"] = returnValue.Result[0].Premium;
                    Session["TerminationDate"] = returnValue.Result[0].TerminationDate.ToString();
                   
                    SMSContent();
                    EmailContent();
                    #endregion
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "Swal.fire(`" + returnValue.Message + "`); ", true);
                    fld_ReferralCode.Text = null;
                    EnableReferralElements();
                    return;
                }

                #endregion
            }
            if (Session["CategoryId"].ToString() != "10" && Session["CategoryId"].ToString() != "11" && Session["CategoryId"].ToString() != "8")
            {
                 #region IQR PRODUCTS & FIRE PROPERTY
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
                    firepropertyrequest.CategoryCode = categoryCode;
                    firepropertyrequest.City = DDcity.SelectedValue.ToString();
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
                    firepropertyrequest.IntegrationId = Convert.ToInt32(Request.QueryString["INTID"].ToString());
                    firepropertyrequest.IsExist = Convert.ToBoolean(Session["IsExist"].ToString());
                    firepropertyrequest.IsWithVoucher = Convert.ToBoolean("False");
                    firepropertyrequest.LastName = lastName.Value.ToString();
                    firepropertyrequest.LossDetails = fld_Remarks.Text.ToString();
                    firepropertyrequest.MiddleName = middleName.Value.ToString();
                    firepropertyrequest.MobileNumber = contactNumber.Text.ToString();
                    firepropertyrequest.MortgageeName = fld_MortgageeName.Text.ToString();
                    firepropertyrequest.NameOfResident = fld_NameOfResident.Text.ToString();
                    firepropertyrequest.Nationality = DDNationality.SelectedValue.ToString();
                    firepropertyrequest.NatureOfOccupancy = DD_NatureOfOccupancy.SelectedValue.ToString();
                    firepropertyrequest.NatureOfWork = natureofwork;
                    firepropertyrequest.NoOfStoreys = string.IsNullOrEmpty(noOfStoreysText) ? 0 : Convert.ToInt64(noOfStoreysText);
                    firepropertyrequest.NumberOfCOCs = long.Parse(numberInput.Text);
                    firepropertyrequest.Occupation = fld_Occupation.Text.ToString();
                    firepropertyrequest.OwnershipStatus = DD_OwnershipStatus.SelectedValue.ToString();
                    firepropertyrequest.PartnerCode = Request.QueryString["PART"].ToString();
                    firepropertyrequest.PlaceOfBirth = placeOfBirth.Text.ToString();
                    firepropertyrequest.PlatformKey = ConfigurationManager.AppSettings["CLIBAPIKey"];
                    firepropertyrequest.PlatformName = ConfigurationManager.AppSettings["CLIBPlatformName"];
                    firepropertyrequest.PreviousLossStatus = DD_IsThereAnyPreviousLoss.Text.ToString();
                    firepropertyrequest.ProductCode = Request.QueryString["PROD"].ToString();
                    firepropertyrequest.PropertyAddress = fld_PropertyAddress.Text.ToString();
                    firepropertyrequest.PropertyProvince = PDDLProvince.SelectedValue.ToString();
                    firepropertyrequest.PropertyCity = PDDLCity.SelectedValue.ToString();
                    firepropertyrequest.PropertyAge = string.IsNullOrEmpty(propertyAgeText) ? 0 : Convert.ToInt64(propertyAgeText);
                    firepropertyrequest.PropertyMortgageStatus = DD_IsThePropertyMortgaged.SelectedValue.ToString();
                    firepropertyrequest.ProviderCode = Request.QueryString["PCODE"].ToString();
                    firepropertyrequest.Province = DDProvince.SelectedValue.ToString();
                    firepropertyrequest.ReferenceNumber = Session["ReferenceCode"].ToString();
                    firepropertyrequest.SourceOfFunds = DDSourceOfFunds.SelectedValue.ToString();
                    firepropertyrequest.Suffix = suffix.Value.ToString();
                    firepropertyrequest.TinId = fld_TinId.Text.ToString();
                    firepropertyrequest.TypeBeams = GetTypeBeamsSelectedValue();
                    firepropertyrequest.TypeColumns = GetTypeColumnsSelectedValue();
                    firepropertyrequest.TypeExteriorWalls = GetTypeExteriorWallsSelectedValue();
                    firepropertyrequest.TypeInnerPartitions = GetTypeInnerPartitionsSelectedValue();
                    firepropertyrequest.TypeOfHome = DD_TypeOfHome.SelectedValue.ToString();
                    firepropertyrequest.TypeRoof = GetTypeRoofSelectedValue();
                    firepropertyrequest.UserId = Request.QueryString["PART"].ToString();
                    firepropertyrequest.ValidID = validIdDropDownList.SelectedValue.ToString();
                    firepropertyrequest.VoucherCode = "";
                    firepropertyrequest.ZipCode = fld_ZipCode.Text.ToString();

                    firepropertyrequest.AgentCode = GetAgentCode();
                    firepropertyrequest.ReferralCode = GetReferralCode();
                    firepropertyrequest.Remarks = RemarkDiscountFormat(GetReferralCode());

                    var returnValue = getList.IQRPurchase(firepropertyrequest);
                    message = returnValue.Message;

                    if (returnValue.Message == "Transaction Successful")
                    {
                        #region SEND EMAIL NOTFICIATION
             
                                #region
                                    var cocNumbers = new StringBuilder();
                                    foreach (var result in returnValue.Result)
                                    {
                                        cocNumbers.Append(result.COCNumber);
                                        cocNumbers.Append(", ");
                                        // Add a separator between COCNumbers
                                    }
                                    // Remove the last ", " separator if there are COCNumbers present
                                    if (returnValue.Result.Count > 0)
                                    {
                                        cocNumbers.Remove(cocNumbers.Length - 2, 2);
                                    }

                                    Session["cocNumber"] = cocNumbers.ToString();
                                    Session["COCEffectiveDateBasis"] = returnValue.Result[0].EffectiveDate.ToString();
                                    Session["Premium"] = returnValue.Result[0].Premium;
                                    Session["TerminationDate"] = returnValue.Result[0].TerminationDate.ToString();
                                #endregion

                        SMSContent();
                        EmailContent();
                        #endregion
                    }
                    else
                    {
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "Swal.fire(`" + returnValue.Message + "`); ", true);
                        fld_ReferralCode.Text = null;
                        EnableReferralElements();
                        return;
                    }

            }
           #endregion
      #endregion
      }
    #endregion

    #region GET AGENT CODE
    private string GetAgentCode()
    {
        string selectedValue = null;

        if (!string.IsNullOrEmpty(fld_ReferralCode.Text))
        {
            if (Session["SummaryAgentCode"] != null && Session["PaymentMethod"].ToString() != "CL Branch")
            {
                selectedValue = !string.IsNullOrEmpty(Session["SummaryAgentCode"].ToString())
                       ? Session["SummaryAgentCode"].ToString()
                       : null;
            }
            else
            {
                selectedValue = null;
            }
        }
        return selectedValue;
    }
    #endregion

    #region GET REFERRAL CODE
    private string GetReferralCode()
    {
        string selectedValue = null;

        if (!string.IsNullOrEmpty(fld_ReferralCode.Text))
        {
            if (Session["ReferralCode"] != null && Session["PaymentMethod"].ToString() != "CL Branch")
            {
                selectedValue = !string.IsNullOrEmpty(Session["ReferralCode"].ToString())
                    ? Session["ReferralCode"].ToString()
                    : null;
            }
            else
            {
                selectedValue = null;
            }
        }
        return selectedValue;
    }
    #endregion

    #region REMARK DISCOUNT FORMAT
    public string RemarkDiscountFormat(string referralcode)
    {
        string formattedDiscount = null;

        if (!string.IsNullOrEmpty(fld_ReferralCode.Text))
        {
            if (Session["PaymentMethod"].ToString() != "CL Branch" && bool.Parse(Session["SummaryIsValidFreeInsurance"].ToString()) == false && (!string.IsNullOrEmpty(Session["SummaryTotalDiscount"].ToString()) || Session["SummaryTotalDiscount"].ToString() != "0.00"))
            {
                formattedDiscount = "Discount of Php " + Session["SummaryTotalDiscount"].ToString() + " from Referral Code " + referralcode + "";
            }
            if (Session["PaymentMethod"].ToString() != "CL Branch" && bool.Parse(Session["SummaryIsValidFreeInsurance"].ToString()) == true && (!string.IsNullOrEmpty(Session["SummaryTotalDiscount"].ToString()) || Session["SummaryTotalDiscount"].ToString() != "0.00"))
            {
                formattedDiscount = "Discount of Php " + Session["SummaryTotalDiscount"].ToString() + " from Referral Code " + referralcode + " Free Insurance";
            }
        }
        return formattedDiscount;
    }
    #endregion
        
    #region COC NUMBER
    private string NumberOfCOCs()
    {
        try
        {
            long numberOfCoc = 0;
            if (Request.QueryString["PROD"].ToString() == "CLCRA")
            {
                numberOfCoc = 4;
                Session["cocNumberList"] = 4;
            }
            else
            {
                numberOfCoc = 1;
                Session["cocNumberList"] = 1;
            }
            return numberOfCoc.ToString();
        }
        catch (Exception ex)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "Swal.fire(`" + ex + "`); ", true);
            throw;
        }


    }
    #endregion

    #region GET NUMBER OF COC

    private string GetAvailebleNumberOfCOCs()
    {
        try
        {
            long availablenumber = 0;

            AvailableCOCRequest numberofavailablecocrequest = new AvailableCOCRequest();

            token.Token = generateToken.GenerateTokenAuth();
            numberofavailablecocrequest.Token = token.Token;
            numberofavailablecocrequest.CategoryCode = categoryCode;
            numberofavailablecocrequest.InsuranceCustomerNo = Session["InsuranceCustomerNumber"].ToString();
            numberofavailablecocrequest.IntegrationId = Convert.ToInt32(Request.QueryString["INTID"].ToString());
            numberofavailablecocrequest.NumberOfCOCs = 0;
            numberofavailablecocrequest.PartnerCode = Request.QueryString["PART"].ToString();
            numberofavailablecocrequest.PlatformKey = ConfigurationManager.AppSettings["CLIBAPIKey"];
            numberofavailablecocrequest.PlatformName = ConfigurationManager.AppSettings["CLIBPlatformName"];
            numberofavailablecocrequest.ProductCode = Request.QueryString["PROD"].ToString();
            numberofavailablecocrequest.ProviderCode = Request.QueryString["PCODE"].ToString();

            var returnValue = getList.NumberOfAvailableCOC(numberofavailablecocrequest);


            if(Session["CategoryId"].ToString() == "8")
            {
                Session["AvailableCOCs"] = 1;
            }
            else
            {
                Session["AvailableCOCs"] = returnValue.Result.AvailableCOCs.ToString();
            }
         

            availablenumber = Convert.ToInt32(Session["AvailableCOCs"].ToString());

            return availablenumber.ToString();

        }
        catch (Exception ex)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "Swal.fire(`" + ex + "`); ", true);
            throw;
        }
    }

    #endregion

    #region INCREMENT/DECREMENT COC COUNT
    protected void btnPlus_Click(object sender, EventArgs e)
    {
        int availableCOCs = Convert.ToInt32(Session["AvailableCOCs"]);
        int currentValue = Convert.ToInt32(numberInput.Text);
        decimal premiumamount = Convert.ToDecimal(Session["ViewPremium"]);

    

        if (currentValue < availableCOCs && Session["CategoryId"].ToString() != "8")
        {
            numberInput.Text = (currentValue + 1).ToString();

            // Parse the value from ViewSummaryTotalAmount
            decimal totalAmount = premiumamount;

            // Multiply the parsed value by the amount to increment
            totalAmount *= currentValue + 1; // Increment the current value by 1

            DisplayPreview(currentValue + 1);

            //// Update the ViewSummaryTotalAmount label with the new calculated value
            //ViewSummaryTotalAmount.Text = "PHP. " + totalAmount.ToString("N2"); ;
        }
        else
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "Swal.fire('You have reached the maximum COC limit.');", true);
            // Optionally, you can show a message indicating the maximum limit has been reached.
            // For example: Response.Write("You have reached the maximum limit.");
        }
    }

    protected void btnMinus_Click(object sender, EventArgs e)
    {
        int currentValue = Convert.ToInt32(numberInput.Text);
        decimal premiumamount = Convert.ToDecimal(Session["ViewPremium"]);

        // Ensure the currentValue doesn't go below 1
        if (currentValue > 1)
        {
            numberInput.Text = (currentValue - 1).ToString();

            // Parse the value from ViewSummaryTotalAmount
            decimal totalAmount = premiumamount;

            // Multiply the parsed value by the amount to decrement
            totalAmount *= currentValue - 1; // Decrement the current value by 1

            DisplayPreview(currentValue - 1);

            //// Update the ViewSummaryTotalAmount label with the new calculated value
            //ViewSummaryTotalAmount.Text = "PHP. " + totalAmount.ToString("N2");
        }
        else
        {
            // Display a message indicating that the minimum value is 1
            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "Swal.fire('Minimum COC limit is 1');", true);
        }
    }
    #endregion

    #region PaymentMethodType
    private string PaymentMethodType()
    {
        try
        {
            string paymentmethodtype = Session["PaymentMethod"].ToString();
            if (Session["PaymentMethod"].ToString() == "GCASH" || Session["PaymentMethod"].ToString() == "PAYMAYA" || Session["PaymentMethod"].ToString() == "GRABPAY")
            {
                paymentmethodtype = "eWallet";
            }

            if(Session["PaymentMethod"].ToString() == "CREDIT_CARD")
            {
                paymentmethodtype = "Credit Card";
            }

            if (Session["PaymentMethod"].ToString() == "DD_BPI" || Session["PaymentMethod"].ToString() == "DD_UBP")
            {
                paymentmethodtype = "Bank Transfer";
            }

            return paymentmethodtype;
        }
        catch (Exception ex)
        {
            SystemUtility.EventLog.SaveError(ex.ToString());
            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "Swal.fire(`" + ex + "`); ", true);
            throw;
        }

    }

    #endregion

    #region DISPLAY PREVIEW
    public void DisplayPreview(int currentValue)
    {
        GetAvailebleNumberOfCOCs();
        try
        {
            DisplayPaymentSummaryRequest displayPreviewRequest = new DisplayPaymentSummaryRequest();
            List<OptionalCoverageCollections> optionalCoverageList = new List<OptionalCoverageCollections>();

            string birthdate = "";
            birthdate = DateTime.Parse(birthDateTextBox.Text.Trim()).ToString("MM/dd/yyyy");

            displayPreviewRequest.Token = generateToken.GenerateTokenAuth();
            displayPreviewRequest.Birthdate = birthdate;
            displayPreviewRequest.CategoryCode = categoryCode;
            displayPreviewRequest.IntegrationId = Convert.ToInt32(Request.QueryString["INTID"].ToString());
            displayPreviewRequest.NumberOfCOCs = currentValue;
            displayPreviewRequest.ReferralCode = fld_ReferralCode.Text.ToString();

            if (Session["CategoryId"].ToString() == "11")
            {
                displayPreviewRequest.IsCovidCoverage = ConvertToBoolean(rbCovidCoverage.SelectedValue);
                foreach (RepeaterItem item in repeaterQuestions.Items)
                {
                    RadioButtonList rblYesNo = item.FindControl("rblYesNo") as RadioButtonList;
                    HiddenField hdnQuestionNo = item.FindControl("hdnQuestionNo") as HiddenField;
                    int questionNo = Convert.ToInt32(hdnQuestionNo.Value);

                    OptionalCoverageCollections optionalCoverage = new OptionalCoverageCollections
                    {
                        Answer = rblYesNo.SelectedValue == "Yes",
                        QuestionNo = questionNo
                    };

                    optionalCoverageList.Add(optionalCoverage);
                }
                displayPreviewRequest.OptionalCoverageCollections = optionalCoverageList;
                displayPreviewRequest.SecondaryProductCode = Session["SecondaryProductCode"].ToString();
                displayPreviewRequest.TravelDurationDays = Convert.ToInt32(numberOfDays.Text.ToString());
                displayPreviewRequest.TravelDurationFrom = travelFrom.Text.ToString();
                displayPreviewRequest.TravelDurationTo = travelTo.Text.ToString();
            }
            else
            {
                displayPreviewRequest.IsCovidCoverage = false;
                displayPreviewRequest.OptionalCoverageCollections = null;
                displayPreviewRequest.SecondaryProductCode = "";
                displayPreviewRequest.TravelDurationDays = 0;
                displayPreviewRequest.TravelDurationFrom = "";
                displayPreviewRequest.TravelDurationTo = "";
            }

            displayPreviewRequest.PartnerCode = Request.QueryString["PART"].ToString();
            displayPreviewRequest.PaymentChannel = Session["PaymentMethod"].ToString();
            displayPreviewRequest.PlatformKey = ConfigurationManager.AppSettings["CLIBAPIKey"];
            displayPreviewRequest.PlatformName = ConfigurationManager.AppSettings["CLIBPlatformName"];
            displayPreviewRequest.ProductCode = Request.QueryString["PROD"].ToString();
            displayPreviewRequest.ProviderCode = Request.QueryString["PCODE"].ToString();

            var returnValue = getList.DisplayPaymentSummary(displayPreviewRequest);
            string message = returnValue.Message;
            decimal currentpremium = decimal.Parse(returnValue.Result[0].CurrentPremium.ToString());
            decimal premium = decimal.Parse(returnValue.Result[0].Premium.ToString());
            decimal convenienceFee = decimal.Parse(returnValue.Result[0].ConvenienceFee.ToString());

            decimal totalPremium = premium;
            decimal totalAmount = premium + convenienceFee;



            #region REFERRAL ADJUSTMENT
            decimal totalPremiumWithReferral = 0;
            decimal totalAmountWithReferral;
            decimal totalDiscountPhp = 0;
            decimal totalDiscountPercent = 0;
            decimal totalPremiumWithDiscount = 0;

            if (!string.IsNullOrEmpty(fld_ReferralCode.Text.ToString()))
            {
                decimal discountPhp = decimal.Parse(Session["SummaryDiscountPHP"].ToString());
                decimal discountPercent = decimal.Parse(Session["SummaryDiscountPercent"].ToString());

                totalDiscountPercent = (discountPercent * currentpremium) / 100;    // COMPUTEVDISCOUNT PERCENT TO PESO
                totalDiscountPhp = (discountPhp + totalDiscountPercent);                    // COMPUTE TOTAL PHP DISCOUNT
                totalPremiumWithReferral = currentpremium - totalDiscountPhp;                 
                totalAmountWithReferral = (totalPremiumWithReferral * currentValue) + convenienceFee;
                totalPremiumWithDiscount = (totalPremiumWithReferral * currentValue);

                Session["SummaryTotalDiscount"] = totalDiscountPhp.ToString("N2");
            }
            else
            {
                totalPremiumWithDiscount = totalPremium;
                totalAmountWithReferral = totalAmount;
            }
            #endregion

         
            Session["SummaryProductName"] = returnValue.Result[0].ProductName.ToString();
            Session["SummaryPremium"] = premium;
            Session["SummaryCurrentPremium"] = currentpremium;
            Session["SummaryConvinienceFee"] = convenienceFee;
            Session["SummaryTotalPremium"] = totalPremiumWithDiscount.ToString("N2");         
            Session["SummaryTotalAmount"] = "Php " + totalAmountWithReferral.ToString("N2");
            Session["SummaryTotalAmounXendit"] = totalAmountWithReferral.ToString();


            SummaryPremiumPerCOC.Text = totalPremiumWithReferral.ToString("N2");
            SummaryDiscountPercentValue.Text = "- "+ decimal.Parse(totalDiscountPercent.ToString("N2"))+"";
            SummaryProductName.Text = Session["SummaryProductName"].ToString();
            SummaryPremium.Text = currentpremium.ToString("N2");
            SummaryConvinienceFee.Text = convenienceFee.ToString("N2");
            SummaryTotalPremium.Text = Session["SummaryTotalPremium"].ToString();
            SummaryTotalAmount.Text = Session["SummaryTotalAmount"].ToString();
            lblPaymentChannel.Text = "(" + Session["PaymentMethod"].ToString() + ")";
            lblAvailableCOCs.Text = Session["AvailableCOCs"].ToString();
        }
        catch (Exception ex)
        {
            SystemUtility.EventLog.SaveError(ex.ToString());
            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "Swal.fire(`" + ex + "`); ", true);
            throw;
        }
    }


    #endregion

    #region EMAIL CONTENT
    public void EmailContent()
        {
     
                decimal premiumStrings = Convert.ToDecimal(Session["Premium"].ToString());
                string premiumString = premiumStrings.ToString();
                string formattedPremium = "Php " + premiumStrings.ToString("N2");

                string distributionChannelId = Session["PaymentMethod"].ToString();

                Session["DistributionChannelId"] = distributionChannelId;

                token.Email = Session["emailAddress"].ToString();
                token.FirstName = Session["firstName"].ToString();
                token.ReferenceCode = Session["ReferenceCode"].ToString();
                token.GroupMail = "CLIBMarketing@pjlhuillier.com";
                token.COCNumber = Session["cocNumber"].ToString();
                token.MiddleName = Session["middleName"].ToString();
                token.LastName = Session["lastName"].ToString();
                token.Suffix = Session["suffix"].ToString();
                token.DOB = DateTime.Parse(Session["dateOfBirth"].ToString()).ToString("MM/dd/yyyy");
                token.ContactNumber = Session["contactNumber"].ToString();
                token.IssueDateTime = Session["issueDate"].ToString();
                token.EffectiveDateTime = Session["COCEffectiveDateBasis"].ToString();
                token.TerminationDate = Session["TerminationDate"].ToString();
                token.ProductName = Session["ProductName"].ToString();
                token.ProductDescription = Session["ProductDescription"].ToString();
                token.Premium = formattedPremium;
                token.DeadlineOfPayment = Session["DeadLineOfPayment"].ToString();
                Session["formattedPremium"] = formattedPremium;

                if (Session["PaymentMethod"].ToString() == "CL Branch")
                {

                    token.Token = generateToken.GenerateTokenAuth();
                    baseResult = processTransaction.SendIQREmail(token);

                    #region REDIRECTING TO THANK YOU PAGE OF IQR
                    Response.Redirect(ConfigurationManager.AppSettings["ConfirmationPage"].Trim(), false);
                    ClearFields(enrollmentForm);
                    #endregion
                }
                else
                {
                    XenditCreateInvoice();
                }
         
    }
        #endregion

    #region XenditCreateInvoice
        public void XenditCreateInvoice()
        {
 
                string streetline = Session["City"].ToString() + ", " + Session["Province"].ToString();
                List<string> paymentMethods = new List<string>() { Session["PaymentMethod"].ToString() };

                List<CIAddress> ciAddress = new List<CIAddress>()
                {
                    new CIAddress
                    {
                        country = "PHILIPPINES",
                        city =  Session["City"].ToString(),
                        postal_code = Session["PostalCode"].ToString(),
                        state = Session["Province"].ToString() ,
                        street_line1 = streetline,
                        street_line2 = streetline
                    }
                };

                CICustomer ciCustomer = new CICustomer()
                {
                    given_names = Session["firstName"].ToString(),
                    surname = Session["lastName"].ToString(),
                    email = Session["emailAddress"].ToString(),
                    mobile_number = Session["contactNumber"].ToString(),
                    addresses = ciAddress,
                };

                CreateInvoiceRequest createinvoice = new CreateInvoiceRequest()
                {
                    external_id = Session["ReferenceCode"].ToString(),
                    description = Session["ProductName"].ToString(),
                    currency = "PHP",
                    amount =  decimal.Parse(Session["SummaryTotalAmounXendit"].ToString()),
                    customer = ciCustomer,
                    payment_methods = paymentMethods,
                    success_redirect_url = ConfigurationManager.AppSettings["ConfirmationPage"].Trim(),
                    failure_redirect_url = ConfigurationManager.AppSettings["ErrorPage"].Trim()
                };

                var returnValue = getList.XenditCreateInvoice(createinvoice);
                string message = returnValue.invoice_url;

                #region SESSION FIELDS FOR XENDIT

                Session["XenditDatePaid"] = DateTime.Parse(returnValue.created.ToString()).ToString("MM/dd/yyyy");
                Session["XenditNotificationDate"] = DateTime.Parse(returnValue.updated.ToString()).ToString("MM/dd/yyyy"); ;
                Session["XenditNumberOfCOCsPaid"] = long.Parse(numberInput.Text);
                Session["XenditORNumber"] = returnValue.id.ToString();
                Session["XenditPaymentGatewayFee"] = Session["SummaryConvinienceFee"];
                Session["XenditPaymentMethod"] = PaymentMethodType();
                Session["XenditPaymentNotes"] = RemarkDiscountFormat(GetReferralCode());
                Session["XenditPaymentOption"] = Session["PaymentMethod"].ToString();
                Session["XenditPaymentOrigin"] = Session["PaymentMethod"].ToString();
                Session["XenditPaymentReferenceNo"] = Session["ReferenceCode"].ToString();
                Session["XenditProductAmount"] = Session["SummaryPremium"];
                Session["XenditReferenceNo"] = Session["ReferenceCode"].ToString();
                Session["XenditTotalAmountPaid"] = Session["SummaryTotalAmounXendit"].ToString();
                Session["XenditTransactionCheckNumber"] = Session["ReferenceCode"].ToString();

                #endregion

            if (returnValue.error_code == null)
                {
                    Response.Redirect(returnValue.invoice_url);
                    Context.ApplicationInstance.CompleteRequest();
                }
                else
                {
                    SystemUtility.EventLog.SaveError(returnValue.error_code.ToString());
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "Swal.fire(`" + returnValue.error_code + "`); ", true);
           
                }
        }

    #endregion

    #region SEND SMS
    public void SMSContent()
    {
        try
        {
            if (Session["PaymentMethod"].ToString() == "CL Branch")
            {

                string smsproductnamelabel = Session["ProductName"].ToString();
                string smsfirstname = Session["firstName"].ToString();

                decimal premiumStrings = Convert.ToDecimal(Session["Premium"].ToString());
                string premiumString = premiumStrings.ToString();
                string formattedPremium = "Php " + premiumStrings.ToString("N2");

                string smsReferenceCode = Session["ReferenceCode"].ToString();

                string messageContent = "Hi, Ka-Cebuana " + smsfirstname + "! Thank you for enrolling to " + smsproductnamelabel + ". To activate your insurance, you may pay the premium of " + formattedPremium + " at any Cebuana Lhuillier Branch using the Ref Code  " + smsReferenceCode + "  .Terms and Conditions apply. For Inquiries please call 0968-856-5459. Thank You";

                string cocNumber = Session["cocNumber"].ToString();

                SendSMS(smsReferenceCode, messageContent, cocNumber);
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
        sendsmsdetails.MobileNumber = Session["contactNumber"].ToString();

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
            dependentrequest.ProductCode = Request.QueryString["PROD"].ToString();
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
            SystemUtility.EventLog.SaveError(ex.ToString());
            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "Swal.fire(`" + ex + "`); ", true);
        }
    }

    private void ClearDynamicControls()
    {
        // Clear any existing dynamic controls
        relationshipDropdownContainer.Controls.Clear();
    }

    private void GenerateDependentControls(string relationshipName, int relationshipCount, int countIndex)
    {
        try
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

                DependentCollections dependents = new DependentCollections();
                dependents.FirstnameID = txtFirstName;
                dependents.LastnameID = txtLastName;
                dependents.BirthID = datePicker;
                dependents.DependentRelationship = relationshipName;

                dependentCollectionLists.Add(dependents);

            }
            relationshipDropdownContainer.Controls.Add(categoryDiv);
        }
        catch (Exception ex)
        {
            SystemUtility.EventLog.SaveError(ex.ToString());
            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "Swal.fire(`" + ex + "`); ", true);

            throw;
        }

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
        try
        {
            if (Session["CategoryId"].ToString() == "10")
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
        catch (Exception ex) 
        {
            SystemUtility.EventLog.SaveError(ex.ToString());
            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "Swal.fire(`" + ex + "`); ", true);
            throw;
        }
       
    }


    protected void populatedatafromtextboxesfieldvalidations()
    {
        try
        {
            if (Session["CategoryId"].ToString() == "10")
            {
                foreach (DependentCollections dependent in dependentCollectionLists)
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
        catch (Exception ex)
        {
            SystemUtility.EventLog.SaveError(ex.ToString());
            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "Swal.fire(`" + ex + "`); ", true);
            throw;
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

    #region GET PROVINCE LIST

    public void GetListProvince()
        {
            token.Token = generateToken.GenerateTokenAuth();
            IList<String> provList;
            provList = getList.GetListProvince(token);
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

    #region Get Value for Dropdown List
    public void GetListValidID()
    {
        token.Token = generateToken.GenerateTokenAuth();
        IList<String> validIDList;
        validIDList = getList.GetListValidID(token);
        validIdDropDownList.DataSource = validIDList;
        validIdDropDownList.DataBind();

    }
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

    #region GET TRAVEL ORIGIN
    public void GetTravelOrigin()
    {
        try
        {
           
            if(GetDestinationTypeValue() == "International")
            {
                fld_Origin.Items.Insert(0, new ListItem("Philippines", "Philippines"));
                fld_Origin.Enabled = false;
                fld_Origin.CssClass = "form-control";
            }
            else
            {
                fld_Origin.Items.Insert(0, new ListItem("Select", "Select"));
                fld_Origin.Enabled = true;
            }


        }
        catch (Exception ex)
        {
            SystemUtility.EventLog.SaveError(ex.ToString());
            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "Swal.fire(`" + ex + "`); ", true);
            throw;
        }
    }

    #endregion

    #region GET DESTINATION TYPE
    public void GetDestinationType()
    {
        try
        {
            token.Token = generateToken.GenerateTokenAuth();
            destinationtyperequest.Token = token.Token;
            destinationtyperequest.DestinationType = GetDestinationTypeValue();
            destinationtyperequest.PlatformKey = ConfigurationManager.AppSettings["CLIBAPIKey"];

            var returnValue = getList.IQRDestinationType(destinationtyperequest);
            fld_Destination.Items.Clear();
            fld_VisaType.Items.Clear();

            fld_Destination.Items.Insert(0, new ListItem("Select", "Select"));

            if (Session["ProductCode"].ToString() == "FPGIT")
            {
                fld_VisaType.Items.Insert(0, new ListItem("Select", "Select"));
            }

            Dictionary<string, string> destinationToVisaTypeMap = new Dictionary<string, string>();

            foreach (var item in returnValue.Result)
            {
                fld_Origin.Items.Add(new ListItem(item.Destination, item.Destination));
                fld_Destination.Items.Add(new ListItem(item.Destination, item.Destination));
                fld_VisaType.Items.Add(new ListItem(item.VisaType, item.VisaType));

                destinationToVisaTypeMap[item.Destination] = item.VisaType;
            }

            ViewState["DestinationToVisaTypeMap"] = destinationToVisaTypeMap;
            fld_Destination.DataBind();
            if (Session["ProductCode"].ToString() == "FPGIT")
            {
                fld_VisaType.DataBind();
            }
        }
        catch (Exception ex)
        {
            SystemUtility.EventLog.SaveError(ex.ToString());
            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "Swal.fire(`" + ex + "`); ", true);
            throw;
        }
    }
    protected void updateVisaTypes()
    {
        Dictionary<string, string> destinationToVisaTypeMap = ViewState["DestinationToVisaTypeMap"] as Dictionary<string, string>;

        if (destinationToVisaTypeMap != null)
        {
            string selectedDestination = fld_Destination.SelectedValue;
            string correspondingVisaType = destinationToVisaTypeMap.ContainsKey(selectedDestination)
                ? destinationToVisaTypeMap[selectedDestination]
                : "Select"; 

    
            fld_VisaType.SelectedValue = correspondingVisaType;


            // Add the CSS class to trigger the animation
            fld_VisaType.CssClass = "form-control fade-in";
        }
    }

    private string GetDestinationTypeValue()

    {
        string selectedValue = Session["ProductCode"].ToString();

        if (selectedValue == "FPGDT")
        {
            selectedValue = "Domestic";
            fld_VisaType.Visible = false;
            fld_PassportNo.Visible = false;
            lbl_VisaType.Visible = false;
            lb_PassPortNo.Visible = false;



        }
        if(selectedValue == "FPGIT")
        {
            selectedValue = "International";
            fld_VisaType.Visible = true;
            fld_PassportNo.Visible = true;
            lbl_VisaType.Visible = true;
            lb_PassPortNo.Visible = true;

        }
        return selectedValue;
    }

    #endregion

    #region GET VALUE FOR TYPE OF TRAVEL

    private string GetSelectedPlanValue()
    {
        if (basicRadio.Checked)
        {
            return "Leisure";
        }
        else if (completeRadio.Checked)
        {
            return "Business";
        }

        return string.Empty; 
    }
    #endregion

    #region CONVERT TO BOOLEAN
    private bool ConvertToBoolean(string value)
    {
        bool result;
        if (bool.TryParse(value, out result))
        {
            return result;
        }
        else
        {
            // Handle the case where the conversion failed, maybe log an error or display a message.
            // For now, we'll set a default value (false) in case of failure.
            return false;
        }
    }

    #endregion

    #region GET OPTIONAL COVERAGE
    public void GetOptionalCoverage()
    {
        try
        {
            token.Token = generateToken.GenerateTokenAuth();
            optionalcoveragerequest.Token = token.Token;
            optionalcoveragerequest.PlatformKey = ConfigurationManager.AppSettings["CLIBAPIKey"];
            optionalcoveragerequest.ProductCode = Session["SecondaryProductCode"].ToString();


            var returnValue = getList.IQROptionalCoverage(optionalcoveragerequest);

            if (returnValue.Message == "Got Optional Coverage")
            {
                Session["DisplayCoverageModule"] = "True";
                repeaterQuestions.DataSource = returnValue.Result;
                repeaterQuestions.DataBind();

            }
            else
            {
                Session["DisplayCoverageModule"] = "False";
            }
        }
        catch (Exception)
        {

            throw;
        }

    }

    #endregion

    #region Check Fields
    public bool validateFields()
    {
        bool isValid = false;

        return isValid;
    }


    private void ClearFields(Control form)
    {
        foreach(var control in this.Controls)
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

    #region GET REFERRAL DETAILS
    public void GetReferralDetails(string referralcodevalue)
    {
        string birthdate = DateTime.Parse(birthDateTextBox.Text.Trim()).ToString("MM/dd/yyyy");
        double summaryPremiumValue = double.Parse(SummaryPremium.Text);

        int AvailableCOCs = int.Parse(lblAvailableCOCs.Text);

        token.Token = generateToken.GenerateTokenAuth();
        referralcoderequest.Token = token.Token;
        referralcoderequest.IntegrationId = Convert.ToInt32(Request.QueryString["INTID"].ToString());
        referralcoderequest.PlatformKey = ConfigurationManager.AppSettings["CLIBAPIKey"];
        referralcoderequest.Premium = summaryPremiumValue;
        referralcoderequest.ReferralCode = referralcodevalue;
        referralcoderequest.BirthDate = birthdate;
        referralcoderequest.FirstName = firstName.Value.ToString();
        referralcoderequest.LastName = lastName.Value.ToString();
        referralcoderequest.MiddleName = middleName.Value.ToString();

        var returnValue = getList.GetReferralDetails(referralcoderequest);
        string message = returnValue.Message;

        bool DiscountPhphasvalue = false;
        bool DiscountPercenthasvalue = false;

        int SetCOCC = 0;

        if (returnValue.ResultStatus == 1)
        {
            fld_ReferralCode.Text = null;
            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "Swal.fire(`" + returnValue.Message + "`); ", true);
            return;
        }
        if (returnValue.ResultStatus == 0)
        {
            SetCOCC = returnValue.Result.SetCOC;

            if (SetCOCC > AvailableCOCs)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "Swal.fire('You have reached the maximum COC Limit.'); ", true);
                EnableReferralElements();
                return;
            }
            if (returnValue.Message == "Referral details retrieved successfully.")
            {
                Session["SummaryAgentCode"] = returnValue.Result.AgentCode.ToString();
                Session["SummaryDiscountPHP"] = returnValue.Result.DiscountPHP;
                Session["SummaryDiscountPercent"] = returnValue.Result.DiscountPercent;
                Session["SummaryDiscountedPremium"] = returnValue.Result.DiscountedPremium;
                Session["SummaryFreeInsuranceProductName"] = returnValue.Result.FreeInsuranceProductName;
                Session["SummaryIsValidFreeInsurance"] = returnValue.Result.IsValidFreeInsurance;
                Session["ReferralCode"] = fld_ReferralCode.Text.ToString();

                SummaryPremiumPerCOC.Text = Session["SummaryDiscountedPremium"].ToString();

                if (!string.IsNullOrEmpty(Session["SummaryFreeInsuranceProductName"].ToString()))
                {
                    SummaryFreeInsurance.Text = "Free Insurance (" + Session["SummaryFreeInsuranceProductName"].ToString() + ")";
                }
                if (!string.IsNullOrEmpty(Session["SummaryDiscountPHP"].ToString()))
                {
                    SummaryDiscountAmount.Text = "Discount (Php. " + decimal.Parse(Session["SummaryDiscountPHP"].ToString()) + ")";
                    SummaryDiscountAmountValue.Text = "- " + decimal.Parse(Session["SummaryDiscountPHP"].ToString()).ToString("N2") + "";
                }
                if (!string.IsNullOrEmpty(Session["SummaryDiscountPercent"].ToString()))
                {
                    SummaryDiscountPercent.Text = "Discount (" + decimal.Parse(Session["SummaryDiscountPercent"].ToString()) + " %)";
                }
                if (bool.Parse(Session["SummaryIsValidFreeInsurance"].ToString()) == false && !string.IsNullOrEmpty(Session["SummaryFreeInsuranceProductName"].ToString()))
                {
                    string script = @"
                        Swal.fire({
                            title: 'You have reached maximum COC limit for your free insurance (" + Session["SummaryFreeInsuranceProductName"].ToString() + @"). Do you wish to proceed?',
                            showCancelButton: true,
                            confirmButtonText: 'CONTINUE',
                            cancelButtonText: 'CANCEL',
                            reverseButtons: true
                        }).then((result) => {
                            if (result.isDismissed) {
                                document.getElementById('" + btnEnableReferralElements.ClientID + @"').click();
                            }
                        });";
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", script, true);
                }

                if (SetCOCC > 0)
                {
                    DisableReferralElements();
                    numberInput.Text = SetCOCC.ToString();
                    DisplayPreview(SetCOCC);
                }

                if (SetCOCC == 0)
                {
                    DisplayPreview(int.Parse(numberInput.Text));
                }
                return;

            }
        }
        else
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "Swal.fire(`" + returnValue.Message + "`); ", true);
            return;
        }

    }
    #endregion

    #region DISABLE REFERRAL ELEMENTS
    public void DisableReferralElements()
    {
        fld_ReferralCode.Attributes["readonly"] = "readonly"; // Set readonly instead of disabled
        btnMinus.Attributes["disabled"] = "disabled";
        btnPlus.Attributes["disabled"] = "disabled";
        numberInput.Attributes["readonly"] = "readonly";
        btnApply.Attributes["disabled"] = "disabled";

        fld_ReferralCode.CssClass += " disabled-element";
        btnMinus.CssClass += " disabled-element";
        btnPlus.CssClass += " disabled-element";
        numberInput.CssClass += " disabled-element";
        btnApply.CssClass += " disabled-element";
    }
    #endregion

    #region ENABLE REFERRAL ELEMENTS
    public void EnableReferralElements()
    {
        int currentValue = Convert.ToInt32(numberInput.Text);
        DisplayPreview(currentValue);

        fld_ReferralCode.Attributes.Remove("readonly"); // Remove readonly instead of disabled
        btnMinus.Attributes.Remove("disabled");
        btnPlus.Attributes.Remove("disabled");
        numberInput.Attributes.Remove("readonly");
        btnApply.Attributes.Remove("disabled");

        fld_ReferralCode.CssClass = fld_ReferralCode.CssClass.Replace("disabled-element", "").Trim();
        btnMinus.CssClass = btnMinus.CssClass.Replace("disabled-element", "").Trim();
        btnPlus.CssClass = btnPlus.CssClass.Replace("disabled-element", "").Trim();
        numberInput.CssClass = numberInput.CssClass.Replace("disabled-element", "").Trim();
        btnApply.CssClass = btnApply.CssClass.Replace("disabled-element", "").Trim();

        fld_ReferralCode.Enabled = true;
        btnMinus.Enabled = true;
        btnPlus.Enabled = true;
        numberInput.Enabled = true;
        btnApply.Enabled = true;
        fld_ReferralCode.Text = null;
    }
    #endregion

    #region PAGE LOAD ENABLE REFERRAL ELEMENTS
    public void PageLoadEnableReferralElements()
    {
        fld_ReferralCode.Attributes.Remove("readonly"); // Remove readonly instead of disabled
        btnMinus.Attributes.Remove("disabled");
        btnPlus.Attributes.Remove("disabled");
        numberInput.Attributes.Remove("readonly");
        btnApply.Attributes.Remove("disabled");

        fld_ReferralCode.CssClass = fld_ReferralCode.CssClass.Replace("disabled-element", "").Trim();
        btnMinus.CssClass = btnMinus.CssClass.Replace("disabled-element", "").Trim();
        btnPlus.CssClass = btnPlus.CssClass.Replace("disabled-element", "").Trim();
        numberInput.CssClass = numberInput.CssClass.Replace("disabled-element", "").Trim();
        btnApply.CssClass = btnApply.CssClass.Replace("disabled-element", "").Trim();

        fld_ReferralCode.Enabled = true;
        btnMinus.Enabled = true;
        btnPlus.Enabled = true;
        numberInput.Enabled = true;
        btnApply.Enabled = true;
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

    protected void fld_Destination_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (Session["ProductCode"].ToString() == "FPGIT")
        {
            updateVisaTypes();
        }  
    }

    protected void btnApply_Click(object sender, EventArgs e)
    {
        GetReferralDetails(fld_ReferralCode.Text.ToString());
    }

    protected void EnableReferralElements_Click(object sender, EventArgs e)
    {
        EnableReferralElements();
    }

    protected void btnClear_Click(object sender, EventArgs e)
    {
        fld_ReferralCode.Text = null;
        EnableReferralElements();
    }





}