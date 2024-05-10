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

public partial class CheckEligibility : System.Web.UI.Page
{
    // Variables
    private string productCode;
    private string partnerCode;
    private string categoryCode;
    private Int64 integrationID;
    private string querystring;
    private string enrollmenturl;
    private string birthdate = "";

    // Instances
    private GenerateToken generateToken = new GenerateToken();
    private GeneralDetails generalDetails = new GeneralDetails();
    private TokenRequest token = new TokenRequest();
    private GetList getList = new GetList();
    private IList<String> partnerList;
    private IList<String> productList;
    private IList<ProductList> productSet;
    private PurchaseRequest purchaserequest = new PurchaseRequest();
    private CheckEligibilityRequest checkeligibilityrequest = new CheckEligibilityRequest();

    private PurchaseRequest purchaseresult = new PurchaseRequest();

    private GetIfCovidRequest getifcovidrequest = new GetIfCovidRequest();
    private GetIfCovidResult getifcovidresult = new GetIfCovidResult();



    private string QpartnerValue;
    private string QproductCode;
    private string QintegrationID;
    private string QproviderCode;


    protected void Page_Load(object sender, EventArgs e)
    {

        QpartnerValue = Request.QueryString["PART"];
        QproductCode = Request.QueryString["PROD"];
        QintegrationID = Request.QueryString["INTID"];
        QproviderCode = Request.QueryString["PCODE"];

        token.Token = generateToken.GenerateTokenAuth();
        partnerList = getList.GetPartnerList(token);
        productList = getList.GetProductList(token);

        //Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1));
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.Cache.SetNoStore();

        string sessionid = "";
        sessionid = HttpContext.Current.Session.SessionID;

     
        if (!IsPostBack)
        {
            IsWithVoucher();
            GetReferenceNumber();

            // IQR CODE
            if (!string.IsNullOrEmpty(Request.QueryString["PART"]) && !string.IsNullOrEmpty(Request.QueryString["PROD"]) && !string.IsNullOrEmpty(Request.QueryString["INTID"]) && !string.IsNullOrEmpty(Request.QueryString["PCODE"]))
            {
                GetIQRDetails();
            }
            // FROM THE INPUT VOUCHER CODE
            else if (!string.IsNullOrEmpty(Session["referenceCode"] as string))
            {
                ClibMicrositeMethods();
            }
            // FROM THE QUERY STRING LIKE 24K, JTJ 
            else if (!string.IsNullOrEmpty(Session["referenceNumber"] as string))
            {
                ClibMicrositeMethods();
            }
        }

        if (!string.IsNullOrEmpty(Request.QueryString["PART"]) && !string.IsNullOrEmpty(Request.QueryString["PROD"]))
        {
            partnerCode = Request.QueryString["PART"];
            productCode = Request.QueryString["PROD"];
            GetPartnerLogo();
            GetProductLogo();
            GetCategoryCode();
            GeneralDetailsIntegrationId();
        }
    }

    private void ClibMicrositeMethods()
    {
        partnerCode = Session["partnerCode"].ToString();
        productCode = Session["productCode"].ToString();
        GetPartnerLogo();
        GetProductLogo();
        GetCategoryCode();
        GeneralDetailsIntegrationId();
    }


    protected void btnCheckEligibility_OnClick(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(firstName.Value) || string.IsNullOrEmpty(lastName.Value) || string.IsNullOrEmpty(birthDateTextBox.Text))
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "Swal.fire('Please fill in all required fields and enter valid information.');", true);
            return;
        }
        DateTime selectedDate;
        if (DateTime.TryParse(birthDateTextBox.Text, out selectedDate))
        {
            DateTime currentDate = DateTime.Now;

            if (selectedDate > currentDate)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "Swal.fire('Birthdate cannot be in the future.');", true);
                birthDateTextBox.Text = null;
                return;
            }
            else if (selectedDate.Year < 1900)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "Swal.fire('Please enter a valid birthdate.');", true);
                birthDateTextBox.Text = null;
                return;
            }
            else if (!System.Text.RegularExpressions.Regex.IsMatch(firstName.Value.Trim(), @"^[A-Za-z0-9-' ]*$") ||
                     !System.Text.RegularExpressions.Regex.IsMatch(middleName.Value.Trim(), @"^[A-Za-z0-9-' ]*$") ||
                     !System.Text.RegularExpressions.Regex.IsMatch(lastName.Value.Trim(), @"^[A-Za-z0-9-' ]*$") ||
                     !System.Text.RegularExpressions.Regex.IsMatch(suffix.Value.Trim(), @"^$|^[A-Za-z0-9-., ]+$"))
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "Swal.fire('You have entered invalid information. Please try again.');", true);
                firstName.Value = null;
                middleName.Value = null;
                lastName.Value = null;
                suffix.Value = null;
                birthDateTextBox.Text = null;
            }

            // If validation passes, proceed with submitting data
            SessionFields();
            CheckAge();
            //PurchaseKYC();
    
         
        }
        else
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "Swal.fire('Invalid birthdate format. Please enter a valid birthdate.');", true);
            birthDateTextBox.Text = null;
        }
    }
    public void GetIQRDetails()
    {
        try
        {   
            token.Token = generateToken.GenerateTokenAuth();
            token.PlatformName = ConfigurationManager.AppSettings["CLIBPlatformName"];
            token.PartnerCode = Request.QueryString["PART"].ToString();
            token.CategoryCode = Request.QueryString["PCODE"].ToString();
            token.ProductCode = Request.QueryString["PROD"].ToString();
            productSet = getList.PopulateProductDetails(token);

            Session["ProductName"] = productSet[0].ProductName.ToString();
            Session["ProductDescription"] = productSet[0].ProductDescription.ToString();
            Session["COCEffectiveDateBasis"] = productSet[0].COCEffectiveDateBasis.ToString();
            Session["CoverageDurationInDays"] = productSet[0].CoverageDurationInDays.ToString();
            Session["Premium"] = productSet[0].SRP.ToString();
            Session["CategoryId"] = productSet[0].CategoryId;
            Session["ProductId"] = productSet[0].ProviderId;
            Session["ProviderId"] = productSet[0].ProviderId;
            Session["PartnerId"] = productSet[0].PartnerId;
            Session["PlatformId"] = productSet[0].PlatformId;


            // FOR TRAVEL SECONDARY PRODUCT CODE
            if (Session["CategoryId"].ToString() == "11")
            {
                Session["SecondaryProductCode"] = Request.QueryString["SPCODE"].ToString();
            }
        }
        catch (Exception)
        {

            throw;
        }

    }
    private void IQRPurchaseParametersKYC()
    {
        try
        {
            token.Token = generateToken.GenerateTokenAuth();
            checkeligibilityrequest.Token = token.Token;
            checkeligibilityrequest.CategoryCode = categoryCode;
            checkeligibilityrequest.FirstName = firstName.Value.Trim();
            checkeligibilityrequest.IntegrationId = Convert.ToInt32(Request.QueryString["INTID"].ToString());
            checkeligibilityrequest.LastName = lastName.Value.Trim();
            checkeligibilityrequest.MiddleName = middleName.Value.Trim();
            checkeligibilityrequest.NumberOfCOCs = 1;
            checkeligibilityrequest.PartnerCode = partnerCode;
            checkeligibilityrequest.PlatformKey = ConfigurationManager.AppSettings["CLIBAPIKey"];
            checkeligibilityrequest.PlatformName = ConfigurationManager.AppSettings["CLIBPlatformName"];
            checkeligibilityrequest.ProductCode = productCode;
            checkeligibilityrequest.ProviderCode = Request.QueryString["PCODE"].ToString();
            checkeligibilityrequest.Birthdate = birthdate;
        }
        catch (Exception)
        {

            throw;
        }

    }

    private void ClibMicrositePurchaseParametersKYC()
    {
        try
        {
            token.Token = generateToken.GenerateTokenAuth();
            checkeligibilityrequest.Token = token.Token;
            checkeligibilityrequest.CategoryCode = Session["categoryCode"].ToString();
            checkeligibilityrequest.FirstName = firstName.Value.Trim();
            checkeligibilityrequest.IntegrationId = Convert.ToInt32(Session["integrationId"].ToString());
            checkeligibilityrequest.LastName = lastName.Value.Trim();
            checkeligibilityrequest.MiddleName = middleName.Value.Trim();
            checkeligibilityrequest.NumberOfCOCs = 1;
            checkeligibilityrequest.PartnerCode = Session["partnerCode"].ToString();
            checkeligibilityrequest.PlatformKey = ConfigurationManager.AppSettings["CLIBAPIKey"];
            checkeligibilityrequest.PlatformName = ConfigurationManager.AppSettings["CLIBPlatformName"];
            checkeligibilityrequest.ProductCode = Session["productCode"].ToString();
            checkeligibilityrequest.ProviderCode = Session["providerCode"].ToString();
            checkeligibilityrequest.Birthdate = birthdate;
            checkeligibilityrequest.ProductName = Session["ProductName"].ToString();
        }
        catch (Exception)
        {

            throw;
        }

    }

    public void PurchaseKYC()
    {
        birthdate = DateTime.Parse(birthDateTextBox.Text.Trim()).ToString("MM/dd/yyyy");

        if (!string.IsNullOrEmpty(QpartnerValue) && !string.IsNullOrEmpty(QproductCode) && !string.IsNullOrEmpty(QintegrationID) && !string.IsNullOrEmpty(QproviderCode))
        {
            IQRPurchaseParametersKYC();
        }
        else
        {
            ClibMicrositePurchaseParametersKYC();
        }

        var returnValue = getList.CheckIfClientExistsIQRNetworld(checkeligibilityrequest);
        string message = returnValue.Message;



        if (message == "Client Can Purchase" || message == "Customer can purchase with Guardian.")
        {
            Session["IsExist"] = "True";
            Session["InsuranceCustomerNumber"] = returnValue.Result[0].InsuranceCustomerNo.ToString();
            RedirectEnrollmentPage();
        }
        else if(message == "Client Not Exist" || message == "Client Not Exist Age Valid.")
        {
            Session["IsExist"] = "False";
            Session["InsuranceCustomerNumber"] = "";
            RedirectEnrollmentPage();
        }
        else if (message == "Client Not Exist and Insured Age Invalid." || message == "Insured age invalid!")
        {
            Session["IsValid"] = message.ToString();
            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "Swal.fire('Sorry you are not eligible for purchase.<br> Your age is not eligible for this product');", true);
        }
        else if (message == "Maximum COC Limit Reached!")
        {
            Session["IsValid"] = message.ToString();
            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "Swal.fire('Sorry, you reached the maximum number of active COC’s for this product.');", true);
        }
        else
        {
            Session["IsValid"] = message.ToString();
            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "Swal.fire(`" + returnValue.Message + "`);", true);
        }
    }

    public void IsWithVoucher()
    {
        if (!string.IsNullOrEmpty(Session["voucherCode"] as string))
        {
            Session["IsWithVoucher"] = "True";
        }
        else
        {
            Session["IsWithVoucher"] = "False";
        }
    }

    public void GetReferenceNumber()
    {
        if (!string.IsNullOrEmpty(Session["referenceCode"] as string))
        {
            string refrencenumbervalue = Session["referenceCode"].ToString();
            Session["fld_ReferenceNumber"] = refrencenumbervalue;
        }
        else
        {
            Session["fld_ReferenceNumber"] = null;
        }
    }
    public void RedirectEnrollmentPage()
    {
        if (!string.IsNullOrEmpty(QpartnerValue) && !string.IsNullOrEmpty(QproductCode) && !string.IsNullOrEmpty(QintegrationID) && !string.IsNullOrEmpty(QproviderCode))
        {
            querystring = "?PART=" + partnerCode + "&PROD=" + productCode + "&INTID=" + Request.QueryString["INTID"] + "&PCODE=" + Request.QueryString["PCODE"];
            enrollmenturl = ConfigurationManager.AppSettings["EnrollmentPage"] + querystring;
            Response.Redirect(enrollmenturl.Trim());
        }
        else
        {
            enrollmenturl = ConfigurationManager.AppSettings["EnrollmentPage2"];
            Response.Redirect(enrollmenturl.Trim());
        }
    }

    public void SessionFields()
    {
        Session["firstName"] = firstName.Value.Trim();
        Session["middleName"] = middleName.Value.Trim();
        Session["lastName"] = lastName.Value.Trim();
        Session["suffix"] = suffix.Value.Trim();
        Session["DateOfBirth"] = birthDateTextBox.Text.Trim();
    }

    public void CheckAge()
    {
        DateTime birthDate;

        if (DateTime.TryParse(birthDateTextBox.Text, out birthDate))
        {
            TimeSpan ageDifference = DateTime.Now.Subtract(birthDate);
            int years = (int)(ageDifference.TotalDays / 365.25); // Taking leap years into account

            //GetIfCovidCoverage(years);

            if (years < 18)
            {
                Session["IsMinor"] = "True";
            }
            else
            {
                Session["IsMinor"] = "False";
            }

            if (!GetIfCovidCoverage(years) && Session["CategoryId"] != null && Session["CategoryId"].ToString() == "11")
            {
                // Handle the case where GetIfCovidCoverage returns false
                return;
            }

            PurchaseKYC();
        }
        else
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "Swal.fire('Invalid Birthdate');", true);
           
        }
    }
    public bool GetIfCovidCoverage(int years)
    {
        try
        {
            if(Session["CategoryId"] != null && Session["CategoryId"].ToString() == "11")
            {
                token.Token = generateToken.GenerateTokenAuth();
                getifcovidrequest.Token = token.Token;
                getifcovidrequest.Age = years;
                getifcovidrequest.PlatformKey = ConfigurationManager.AppSettings["CLIBAPIKey"];
                getifcovidrequest.SecondaryProductCode = Session["SecondaryProductCode"].ToString();

                var returnValue = getList.IQRGetIfCovidCoverage(getifcovidrequest);

                if(returnValue.Message == "Got Product if have Covid")
                {
                    Session["CheckIfCovidCoverage"] = returnValue.Result.ToString();
                    return true;
                }
                else
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "Swal.fire(`" + returnValue.Message + "`);", true);
                    return false;
                }

              
            }

        }
        catch (Exception)
        {

            return false; 
        }

        return false;

    }

    #region Get Partner Logo
    private void GetPartnerLogo()
    {
        if (!string.IsNullOrEmpty(QpartnerValue) && !string.IsNullOrEmpty(QproductCode) && !string.IsNullOrEmpty(QintegrationID) && !string.IsNullOrEmpty(QproviderCode))
        {
            token.PartnerCode = "PJ4";
        }
        else
        {
            token.PartnerCode = partnerCode;
        }
        string partnerImagePath = getList.GetPartnerImagePath(token);
        // partnerImage.Attributes["src"] = ResolveUrl(ConfigurationManager.AppSettings["productImagePath"] + partnerImagePath);
        partnerImage.Style["background-image"] = ResolveUrl(ConfigurationManager.AppSettings["productImagePath"] + partnerImagePath);
    }
    #endregion
    #region Get Product Logo
    private void GetProductLogo()
    {
        token.ProductCode = productCode;
        string productImagePath = getList.GetProductImagePath(token);
        //productImage.Attributes["src"] = ResolveUrl(System.Configuration.ConfigurationManager.AppSettings["productImagePath"] + productImagePath);
        productImage.Style["background-image"] = ResolveUrl(System.Configuration.ConfigurationManager.AppSettings["productImagePath"] + productImagePath);
    }
    #endregion

    #region Get Category Code
    private void GetCategoryCode()
    {
        categoryCode = getList.GetCategory(token);
    }
    #endregion

    private void GeneralDetailsIntegrationId()
    {
        generalDetails.IntegrationId = getList.GetIntegrationId(token);
    }
}
