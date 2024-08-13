using Newtonsoft.Json;
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
    TokenRequest token = new TokenRequest();
    GenerateToken generateToken = new GenerateToken();
    BaseResult baseResult = new BaseResult();
    ProcessTransaction processTransaction = new ProcessTransaction();

    TagInsuraceAsPaidResult tagaspaidresult = new TagInsuraceAsPaidResult();

    GetList getList = new GetList();
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.Cache.SetNoStore();

        if (!IsPostBack)
        {
            Session["FirstPageLoad"] = true;

            SuccessResult();
        }
        else
        {
            redirection();
        }
    }
    protected void btnReturntoProductCategoryPage_Click(object sender, EventArgs e)
    {
        redirection();
    }

    #region SHOW LABEL ON THANK YOU PAGE
    public void SuccessResult()
    {
   
        if (Session["DistributionChannelId"].ToString() == "CL Branch")
        {
            ShowCLBranchLables();
            ShowResponseResults();
        }

        if (Session["PaymentMethod"].ToString() == "GCASH" || Session["PaymentMethod"].ToString() == "PAYMAYA" || Session["PaymentMethod"].ToString() == "GRABPAY" || Session["PaymentMethod"].ToString() == "DD_BPI" || Session["PaymentMethod"].ToString() == "DD_UBP" || Session["PaymentMethod"].ToString() == "CREDIT_CARD")
        {
            TagAsPaid();
        }
    }

    #endregion

    #region REDIRECTION MECHANISM ON EVERY PLATFORM
    public void redirection()
    {
        // Get the base URL from the configuration
        string baseUrl = ConfigurationManager.AppSettings["ProductRegistration"];

        // Check if Session["PartnerValue"] is not null
        if (Session["PartnerValue"] != null)
        {
            // Construct the URL dynamically based on the partner value
            string partnerValue = Session["PartnerValue"].ToString();
            string redirectUrl = baseUrl + "?PART=" + partnerValue;

            // Redirect to the constructed URL
            Response.Redirect(redirectUrl);
        }
        else
        {
            // Handle the case where Session["PartnerValue"] is null
            Response.Redirect(baseUrl);
        }
    }

    #endregion

    #region REFERENCE NUMBER VALIDATION
    private string RefrecenNumber()
    {
        string result = Session["DistributionChannelId"].ToString();

        if (result == "CL Branch")
        {
            result = Session["ReferenceCode"].ToString();
        }
        else
        {
            result = GetCOCNumber();
        }

        return result;
    }
    #endregion

    private string GetCOCNumber()
    {
        string result = Session["cocNumber"].ToString();

        if (!string.IsNullOrEmpty(Session["SummaryFreeInsuranceProductName"].ToString()) &&
            bool.Parse(Session["SummaryIsValidFreeInsurance"].ToString()) == true)
        {
            // Split the COC numbers by comma
            var cocNumbers = result.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

            // Remove the first COC number
            if (cocNumbers.Length > 1)  // Ensure there is more than one COC number
            {
                result = string.Join(",", cocNumbers.Skip(1).ToArray());
            }
            else
            {
                result = Session["cocNumber"].ToString();
            }
        }

        return result;
    }


    #region REDIRECTION METHOD SCRIPT
    private string GetRedirectionScript()
    {
        string script = "";

        // Get the base URL from the configuration
        string baseUrl = ConfigurationManager.AppSettings["ProductRegistration"];

        // Check if Session["PartnerValue"] is not null
        if (Session["PartnerValue"] != null)
        {
            // Retrieve the partner value from session
            string partnerValue = Session["PartnerValue"].ToString();

            // Construct the URL dynamically based on the partner value
            string redirectUrl = baseUrl + "?PART=" + partnerValue;

            // Build the redirection script
            script += "window.location.href = '" + redirectUrl + "';";
        }
        else
        {
            // If Session["PartnerValue"] is null or not found, redirect to the base URL directly
            script += "window.location.href = '" + baseUrl + "';";
        }

        return script;
    }

    #endregion

    #region TAG AS PAID MAIN METHOD

    #region METHOD CALLING TAG AS PAID
    public void TagAsPaid()
    {
        PaymentDetails paymentdetails = new PaymentDetails()
        {
            DatePaid = Session["XenditDatePaid"].ToString(),
            NotificationDate = Session["XenditNotificationDate"].ToString(),
            NumberOfCOCsPaid = long.Parse(Session["XenditNumberOfCOCsPaid"].ToString()),
            ORNumber = Session["XenditORNumber"].ToString(),
            PaymentGatewayFee = double.Parse(Session["XenditPaymentGatewayFee"].ToString()),
            PaymentMethod = Session["XenditPaymentMethod"].ToString(),
            PaymentNotes = Session["XenditPaymentNotes"].ToString(),
            PaymentOption = Session["XenditPaymentOption"].ToString(),
            PaymentOrigin = Session["XenditPaymentOrigin"].ToString(),
            PaymentReferenceNo = Session["XenditPaymentReferenceNo"].ToString(),
            ProductAmount = double.Parse(Session["XenditProductAmount"].ToString()),
            ReferenceNo = Session["XenditReferenceNo"].ToString(),
            TotalAmountPaid = double.Parse(Session["XenditTotalAmountPaid"].ToString()),
            TransactionCheckNumber = Session["XenditORNumber"].ToString(),
        };
        TagInsuranceAsPaidRequest tagaspaidrequest = new TagInsuranceAsPaidRequest()
        {
            Token = generateToken.GenerateTokenAuth(),
            isActive = true,
            PlatformKey = ConfigurationManager.AppSettings["CLIBAPIKey"],
            PaymentDetails = paymentdetails,
            ReferralCode = GetReferralCode(),
        };

        var returnValue = getList.TagInsuranceAsPaid(tagaspaidrequest);
        string message = returnValue.Message;

        if (returnValue.Message == "Transaction successful!")
        {
            string jsonResponse = JsonConvert.SerializeObject(returnValue);
            tagaspaidresult = JsonConvert.DeserializeObject<TagInsuraceAsPaidResult>(jsonResponse);

            ShowOnlinePaymentLabels();
            ShowResponseResults();
            SMSContent(tagaspaidresult);

            if (!string.IsNullOrEmpty(returnValue.Result.FreeInsurance.ToString()) && !string.IsNullOrEmpty(returnValue.Result.FreeInsuranceCOCNumber.ToString()))
            {
                GotoFreeInsuranceDetails(returnValue.Result.FreeInsurance.ToString(), returnValue.Result.FreeInsuranceCOCNumber.ToString());


            }
        }
        else
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "Swal.fire(`" + returnValue.Message + "`); ", true);
            return;
        }
    }


    private void ShowOnlinePaymentLabels()
    {
        lblInsurancePolicyMessage.Text = "Your policy details will be sent to the email address that you have provided";
        lbNameofInsured.Text = "Insured Name:";
        lbProductName.Text = "Product:";
        lblReferenceNumber.Text = "COC Number:";
        lblPremiumText.Text = "Premium:";
    }

    private void ShowResponseResults()
    {
        lblNameOfInsured.Text = Session["firstName"].ToString() + " " + Session["lastName"].ToString();
        lblNameOfProduct.Text = Session["ProductName"].ToString();
        lblRTN.Text = RefrecenNumber();
        lblPremium.Text = Session["formattedPremium"].ToString();
    }

    private void ShowCLBranchLables()
    {
        lblInsurancePolicyMessage.Text = "Your insurance application details have been sent through your registered email. Your application is only valid within 5 days from the date of filing. You may pay at any Cebuana Lhuillier Branch to activate your Insurance Policy.";
        lbNameofInsured.Text = "Name of Insured:";
        lbProductName.Text = "Product:";
        lblReferenceNumber.Text = "Application Reference No.:";
        lblPremiumText.Text = "Premium:";
    }

    private void GotoFreeInsuranceDetails(string FreeInsurance, string FreeInsuranceCOCNumber)
    {
        lbfreeinsurancetitle.Text = "Free Insurance";
        lbfreeinsurancenumbertitle.Text = "COC Number";
        lblFreeInsurance.Text = FreeInsurance;
        lblFreeInsuranceCOCNumber.Text = FreeInsuranceCOCNumber;
    }
    #endregion

    #region GET REFERRAL CODE
    private string GetReferralCode()
    {
        string selectedValue = string.Empty;

        if (Session["ReferralCode"] != null)
        {
            selectedValue = Session["ReferralCode"].ToString();
        }
        else
        {
            selectedValue = null;
        }

        return selectedValue;
    }
    #endregion

    #region GENERATE SMS CONTENT
    public void SMSContent(TagInsuraceAsPaidResult tagaspaidresult)
    {
        try
        {
            string smsproductnamelabel = Session["ProductName"].ToString();
            string smsfirstname = Session["firstName"].ToString();

            decimal premiumStrings = Convert.ToDecimal(Session["Premium"].ToString());
            string premiumString = premiumStrings.ToString();
            string formattedPremium = "Php. " + premiumStrings.ToString("N2");

            string smsReferenceCode = Session["ReferenceCode"].ToString();

            foreach (var cocItems in tagaspaidresult.Result.insuranceCollection)
            {
                string messageContent = "Hi Ka-Cebuana " + smsfirstname + "! Your policy " + smsproductnamelabel + " is now active with COC details: COC No.:" + cocItems.COCNumber + ", Policy Period: " + cocItems.EffectiveDate + " to " + cocItems.TerminationDate + ", T&C apply. For Inquiries, call (02) 7759-9888 or visit https://www.cebuanalhuillier.com/microinsurance.";

                SendSMS(smsReferenceCode, messageContent, cocItems.COCNumber);
            }

            string messageredirection = "Transaction Successfully! We'll redirect you in 5 seconds...";
            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "Swal.fire(`" + messageredirection + "`, '', 'success').then((result) => { if (result.isConfirmed) { setTimeout(function(){ " + GetRedirectionScript() + " }, 5000); }}); ", true);
        }
        catch (Exception ex)
        {
            SystemUtility.EventLog.SaveError(ex.ToString());
            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "Swal.fire(`" + ex + "`); ", true);
            throw;
        }
    }

    #endregion

            #region SMS CONTENT LOOPER
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

    #endregion
}
