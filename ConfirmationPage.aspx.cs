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
                if (Session["DistributionChannelId"].ToString() == "CL Branch")
                {
                    lblInsurancePolicyMessage.Text = "Your insurance application details have been sent through your registered email. Your application is only valid within 5 days from the date of filing. You may pay at any Cebuana Lhuillier Branch to activate your Insurance Policy.";
                    lbNameofInsured.Text = "Name of Insured:";
                    lbProductName.Text = "Product:";
                    lblReferenceNumber.Text = "Application Reference No.:";
                    lblPremiumText.Text = "Premium:";

                }
                if (Session["PaymentMethod"].ToString() == "GCASH" || Session["PaymentMethod"].ToString() == "PAYMAYA" || Session["PaymentMethod"].ToString() == "GRABPAY" || Session["PaymentMethod"].ToString() == "DD_BPI" || Session["PaymentMethod"].ToString() == "DD_UBP" || Session["PaymentMethod"].ToString() == "CREDIT_CARD")
                {
                    TagAsPaid();
                    lblInsurancePolicyMessage.Text = "Your policy details will be sent to the email address that you have provided";
                    lbNameofInsured.Text = "Insured Name:";
                    lbProductName.Text = "Product:";
                    lblReferenceNumber.Text = "COC Number:";
                    lblPremiumText.Text = "Premium:";

                }
                if (bool.Parse(Session["SummaryIsValidFreeInsurance"].ToString()) == false && !string.IsNullOrEmpty(Session["SummaryFreeInsuranceProductName"].ToString()))
                {
                    FreeInsurance.Text = Session["FreeInsurance"].ToString();
                    FreeInsuranceCOCNumber.Text = Session["FreeInsuranceCOCNumber"].ToString();

                }


                lblNameOfInsured.Text = Session["firstName"].ToString() + " " + Session["lastName"].ToString();
                lblNameOfProduct.Text = Session["ProductName"].ToString();
                lblRTN.Text = RefrecenNumber();
                lblPremium.Text = Session["formattedPremium"].ToString();
 

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

    private string RefrecenNumber()
    {
        string result = Session["DistributionChannelId"].ToString();

        if(result == "CL Branch")
        {
            result = Session["ReferenceCode"].ToString();
        }
        else
        {
            result = Session["cocNumber"].ToString();
        }

        return result;
    }

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



    #region TAG AS PAID
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

        if(returnValue.Message == "Transaction successful!")
        {
            string jsonResponse = JsonConvert.SerializeObject(returnValue);
            tagaspaidresult = JsonConvert.DeserializeObject<TagInsuraceAsPaidResult>(jsonResponse);

            SMSContent(tagaspaidresult);

            if (bool.Parse(Session["SummaryIsValidFreeInsurance"].ToString()) == false && !string.IsNullOrEmpty(Session["SummaryFreeInsuranceProductName"].ToString()))
            {
                Session["FreeInsurance"] = returnValue.Result[0].FreeInsurance.ToString();
                Session["FreeInsuranceCOCNumber"] = returnValue.Result[0].FreeInsuranceCOCNumber.ToString();
            }
        }
        else
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "Swal.fire(`" + returnValue.Message + "`); ", true);
            return;
        }

    }

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


                foreach (var cocItems in tagaspaidresult.Result)
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

}

#endregion

