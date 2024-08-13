using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebCaptchaLib;

public partial class Public_ProductRegistration : System.Web.UI.Page
{
    GenerateToken generateToken = new GenerateToken();
    ProductList product = new ProductList();
    GetList getList = new GetList();
    TokenRequest token = new TokenRequest();

    AffiliateDetailsRequest affiliatedetailsrequest = new AffiliateDetailsRequest();
    AffiliateDetailsResult affiliatedetailsresult = new AffiliateDetailsResult();

    string productCode;
    string partnerCode;
    string referenceCode; //from e-voucher from CLIB
    string referenceNumber; //from Partner VIA url calling
    string vouchercode; // for the qrcode validation
    IList<String> partnerList;
    IList<String> productList;
    IList<ProductList> productSet; // get product by code and integration id


     protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["PART"] != null && Request.QueryString["PROD"] == null && Request.QueryString["REFNUM"] == null)
        {
            string partValue = Request.QueryString["PART"];

            if (!string.IsNullOrEmpty(partValue))
            {

                GetPartNerRedirection(partValue);

                //Session["PartnerValue"] = partValue;
                string vouchercode = ConfigurationManager.AppSettings["CLIBvoucherCode1"] ?? ConfigurationManager.AppSettings["CLIBvoucherCode2"] ?? ConfigurationManager.AppSettings["CLIBvoucherCode"];
                Session["voucherCode"] = vouchercode;
                //Session["SessionExpiration"] = partValue;
                Response.Redirect(ConfigurationManager.AppSettings["ProductCategoryPage"].Trim());
            }
        }
        if (Request.QueryString["PART"] != null && Request.QueryString["PROD"] != null && Request.QueryString["REFNUM"] != null)
        {
            product.ProductCode = Request.QueryString["PROD"];
            partnerCode = Request.QueryString["PART"];
            token.ReferenceNumber = Request.QueryString["REFNUM"];

            token.Token = generateToken.GenerateTokenAuth();
            partnerList = getList.GetPartnerList(token); //change to partner
            productList = getList.GetProductList(token);

            tokenParamsQueryString();

            //ADDED 09/02/21 MBPJR Voucher Code
            if (Request.QueryString["PART"] == "PJ4" && Request.QueryString["PROD"] == "MBPJR")
            {
                if (Request.QueryString["REFNUM"] == "CLINT")
                {
                    Session["GroupMail"] = ConfigurationManager.AppSettings["CLINT"].Trim();
                    Response.Redirect(ConfigurationManager.AppSettings["MBizOwnerDetails"].Trim());
                }
                else if (Request.QueryString["REFNUM"] == "CLEXT")
                {
                    Session["GroupMail"] = ConfigurationManager.AppSettings["CLEXT"].Trim();
                    Response.Redirect(ConfigurationManager.AppSettings["MBizOwnerDetails"].Trim());
                }
                else if (Request.QueryString["REFNUM"] == "CLVIS")
                {
                    Session["GroupMail"] = ConfigurationManager.AppSettings["CLVIS"].Trim();
                    Response.Redirect(ConfigurationManager.AppSettings["MBizOwnerDetails"].Trim());
                }
                else if (Request.QueryString["REFNUM"] == "CLADC")
                {
                    Session["GroupMail"] = ConfigurationManager.AppSettings["CLADC"].Trim();
                    Response.Redirect(ConfigurationManager.AppSettings["MBizOwnerDetails"].Trim());
                }
                else
                {
                    InvalidUrl();
                }
               
                //Response.Redirect("MBizOwnerDetails.aspx");
            }

            if (partnerList.Contains(partnerCode) == false || productList.Contains(product.ProductCode) == false)
            {
                InvalidUrl();
            }
            else
            {
                Session["partnerCode"] = partnerCode;
                Session["productCode"] = product.ProductCode;
                Session["referenceNumber"] = token.ReferenceNumber;
                if (partnerCode == "CFH" && product.ProductCode == "CLNIF")
                {
                    if (token.ReferenceNumber == "CFHWS")
                    {
                        gotoCheckEligibility();
                    }
                    else
                    {
                        InvalidUrl();
                    }
                }
                else if(partnerCode != "CFH" && product.ProductCode == "CLNIF")
                {
                    InvalidUrl();
                }
                else if (partnerCode == "CFH" && product.ProductCode != "CLNIF")
                {
                    InvalidUrl();
                }
                // Partner COde 
                else if (partnerCode == "24K" && product.ProductCode == "CLNF6")
                {
                    if (token.ReferenceNumber == "PROMO")
                    {
                        gotoCheckEligibility();
                    }
                    else
                    {
                        InvalidUrl();
                    }
                }
                else if (partnerCode != "24K" && product.ProductCode == "CLNF6")
                {
                    InvalidUrl();
                }
                else if (partnerCode == "24K" && product.ProductCode != "CLNF6")
                {
                    InvalidUrl();
                }

                //[JEP 04.28.22]: Added TAG for JJ Partner and Product
                else if (partnerCode == "JTJ" && product.ProductCode == "CLNP1")
                {
                    if (token.ReferenceNumber == "CFHWS")
                    {
                        gotoCheckEligibility();
                    }
                    else
                    {
                        InvalidUrl();
                    }
                }
                else if (partnerCode != "JTJ" && product.ProductCode == "CLNP1")
                {
                    InvalidUrl();
                }
                else if (partnerCode == "JTJ" && product.ProductCode != "CLNP1")
                {
                    InvalidUrl();
                }

                else
                {
                    if (token.ReferenceNumber == "CFHWS")
                    {
                        InvalidUrl();
                        //Response.Redirect(ConfigurationManager.AppSettings["EnrollmentPage"].Trim());
                    }
                    else if (token.ReferenceNumber == "PROMO")
                    {
                        InvalidUrl();
                    }
                    else if (getList.ifReferenceNumberExists(token) == true)
                    {
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Reference number is already used.')", true);
                    }
                    else
                    {
                        gotoCheckEligibility();
                    }
                }
              
            }

        }
            //if(Request.QueryString["PART"] != null && Request.QueryString["PROD"] == null && Request.QueryString["REFNUM"] == null)
            //{
            //    if (Request.QueryString["PART"] == "IQR")
            //    {
            //        vouchercode = ConfigurationManager.AppSettings["CLIBvoucherCode1"] ?? ConfigurationManager.AppSettings["CLIBvoucherCode2"] ?? ConfigurationManager.AppSettings["CLIBvoucherCode"];
            //        Session["voucherCode"] = vouchercode;
            //        Response.Redirect(ConfigurationManager.AppSettings["ProductCategoryPage"].Trim());
            //    }
            //}

        if(captchaText.Value=="")
        {
            WebCaptcha.GenerateCaptcha(captchaImage, HttpContext.Current);
        }
    }

    public void GetPartNerRedirection(string partValue)
    {
        Session["PartnerValue"] = partValue;
        Session["SessionExpiration"] = partValue;
    }
    
    public static String GetTimestamp(DateTime value)
    {
        return value.ToString("yyyyMMddHHmm");
    }
    public void Submit_Click(object sender, EventArgs e)
    {
        token.Token = generateToken.GenerateTokenAuth();
        partnerList = getList.GetPartnerList(token);
        productList = getList.GetProductList(token);
        
        if (WebCaptcha.IsCaptchaCorrect(captchaText.Value.Trim(), HttpContext.Current))
        {
            int i = voucherCode.Value.Trim().Length;
            
            if (voucherCode.Value.Length != 0 )
            {
                if (voucherCode.Value.Trim().Length == 12 || voucherCode.Value.Trim().Length == 21 || voucherCode.Value.Trim().Length == 13 || voucherCode.Value.Trim().Length == 15)
                {
                    partnerCode = voucherCode.Value.Substring(0, 3);
                    product.ProductCode = voucherCode.Value.Substring(3, 5);

                    tokenParamsFromVoucher();

                    // REGULAR VOUCHERS - PRODU VOUCHERS
                    if (voucherCode.Value.Length == 12)
                    {
                        token.ReferenceCode = voucherCode.Value.Substring(8, 4);
                    }
                    // BRANCH REFFERAL VOUCHERS 
                    else if (voucherCode.Value.Length == 13)
                    {
                        token.ReferenceCode = voucherCode.Value.Substring(8, 5);
                    }
                    // AGENT REFFERAL VOUCHERS 
                    else if (voucherCode.Value.Length == 21)
                    {
                        token.ReferenceCode = voucherCode.Value.Substring(8, 13);
                    }
                    // REGULAR VOUCHER EXTENDED
                    else if (voucherCode.Value.Length == 15)
                    {
                        token.ReferenceCode = voucherCode.Value.Substring(8, 7);
                    }
                    else
                    {
                        token.ReferenceCode = voucherCode.Value.Substring(8, 12);
                    }
                    if (voucherCode.Value.Length < 12)
                    {
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "Swal.fire('Invalid voucher code. Please check and input correct voucher code.');", true);
                    }
                    else if (voucherCode.Value.Length == 13)
                    {
                         //Jep 042721 Added Client Referral Validation
                        if (partnerCode == "CLH" && product.ProductCode == "REFER")
                        {
                            //string designation = voucherCode.Value.Substring(8, 1);
                            string BranchCode = voucherCode.Value.Substring(8, 5);
                            token.BranchCode = BranchCode;
                            if (getList.CheckIfBranchExists(token) == true)
                            {
                                Session.Clear();
                                string timeStamp;
                                timeStamp = GetTimestamp(DateTime.Now);
                                Session["BranchCode"] = BranchCode;
                                Session["ReferralPartnerCode"] = partnerCode;
                                Session["RTN"] = BranchCode + timeStamp;
                                //Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('"+Session["RTN"].ToString()+ "')",true);

                                Response.Redirect(ConfigurationManager.AppSettings["ClientReferral"].Trim());
                            }
                            else if (getList.CheckIfBranchExists(token) == false)
                            {
                                Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "Swal.fire('Branch code does not exists.');", true);
                            }
                        }
                    }
                    else if (voucherCode.Value.Length == 21)
                    {
                        //AJ 04142024 Added Agent Referral Validation
                        if (partnerCode == "AFL" && product.ProductCode == "REFER")
                        {
                            //string designation = voucherCode.Value.Substring(8, 1);
                            string BranchCode = voucherCode.Value.Substring(8, 5);
                            string affiliateCode = voucherCode.Value.Substring(8, 13);

                            token.BranchCode = BranchCode;
                            Session.Clear();
                            string RTn = GenerateRTN(affiliateCode);
                            Session["BranchCode"] = BranchCode;
                            Session["ReferralPartnerCode"] = partnerCode;
                            Session["RTN"] = RTn;


                             CheckAffiliateDetails(affiliateCode);


                        }
                    }

                    else
                    {
                        if (voucherCode.Value == ConfigurationManager.AppSettings["CLIBvoucherCode"].Trim() || voucherCode.Value == ConfigurationManager.AppSettings["CLIBvoucherCode1"] || voucherCode.Value == ConfigurationManager.AppSettings["CLIBvoucherCode2"])
                        {
                            Session.Clear();
                            Session["voucherCode"] = voucherCode.Value;
                            voucherCode.Value = "";
                            Response.Redirect(ConfigurationManager.AppSettings["MenuPage"].Trim());
                        }
                        if(getList.ifProductCodeBaseOnIntegrationMappingExists(token) == false)
                        {
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "Swal.fire('Invalid voucher code. Please check and input correct voucher code.');", true);
                        }
                        else if (partnerList.Contains(partnerCode) == false || productList.Contains(product.ProductCode) == false || getList.ifReferenceCodeExists(token) == false)
                        {
                            if(getList.ifReferenceCodeIsUsed(token) == true)
                            {
                                Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "Swal.fire('Voucher code is already used. Please input another voucher code.');", true);
                            }
                            else
                            {
                                Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "Swal.fire('Invalid voucher code. Please check and input correct voucher code.');", true);
                            }
                            
                        }
                        else
                        {
                            string partValue = ConfigurationManager.AppSettings["CLIBPlatformName"];
                            GetPartNerRedirection(partValue);
                            gotoCheckEligibility();
                        }
                    }
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "Swal.fire('Please input valid voucher code.');", true);
                }
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "Swal.fire('Please input valid voucher code.');", true);
            }
        }
        else
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "Swal.fire('Invalid captcha. Please try again.');", true);
        }
        WebCaptcha.GenerateCaptcha(captchaImage, HttpContext.Current);
        captchaText.Value = "";
    }
    #region CHECK AFFILIATE DETAILS
    public void CheckAffiliateDetails(string affiliateCode)
    {
        try
        {
            affiliatedetailsrequest.Token = generateToken.GenerateTokenAuth();
            affiliatedetailsrequest.AgentCode = affiliateCode;
            affiliatedetailsrequest.PlatformKey = ConfigurationManager.AppSettings["CLIBAPIKey"];

            var returnValue = getList.GetAffiliateDetails(affiliatedetailsrequest);
            string message = returnValue.Message;

            if (returnValue.Message  == "Agent Already Exist, Got Agent Details.")
            {
                Session["ADCAffiliateCode"] = returnValue.Result[0].AffiliateCode.ToString();
                Session["ADCFirstName"] = returnValue.Result[0].FirstName.ToString();
                Session["ADCLastName"] = returnValue.Result[0].LastName.ToString();

                Response.Redirect(ConfigurationManager.AppSettings["ClientReferral"].Trim());
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "Swal.fire(`" + returnValue.Message + "`); ", true);
            }
        }
        catch (Exception)
        {
    
            throw;
        }
                
    }
    #endregion
    public void InvalidUrl()
    {
        Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "Swal.fire('Invalid URL use case. Please check that you have the correct URL and try again.');", true);
    }

    private void tokenParamsQueryString()
    {
        token.PlatformName = ConfigurationManager.AppSettings["CLIBPlatformName"];
        token.PartnerCode = partnerCode;
        token.ProductCode = product.ProductCode;
    }
    private void tokenParamsFromVoucher()
    {
        token.PlatformName = ConfigurationManager.AppSettings["CLIBPlatformName"];
        token.PartnerCode = voucherCode.Value.Substring(0, 3);
        token.ProductCode = voucherCode.Value.Substring(3, 5);
    }

    public static string GenerateRTN(string affiliateCode)
    {
        // Current date and time
        DateTime now = DateTime.Now;

        // Extract year, month, date, and time components
        string year = now.ToString("yy"); // Last two digits of the year
        string month = now.ToString("MM"); // Month as two digits
        string day = now.ToString("dd"); // Day as two digits
        string time = now.ToString("HHmmss"); // Time as HHmmss

        // Combine parts to form the RTN
        string RTN = string.Format("{0}{1}{2}{3}{4}", affiliateCode, year, month, day, time);
        return RTN;
    }


    private void gotoCheckEligibility()
    {
        Session.Clear();
        productSet = getList.PopulateProductByCodesAndIntegrationID(token);
        Session["partnerCode"] = productSet[0].PartnerCode.ToString();
        Session["productCode"] = productSet[0].ProductCode.ToString();
        Session["integrationId"] = productSet[0].IntegrrationId.ToString();
        Session["categoryCode"] = productSet[0].CategoryCode.ToString();
        Session["providerCode"] = productSet[0].ProviderCode.ToString();
        Session["referenceCode"] = token.ReferenceCode;
        Session["voucherCode"] = voucherCode.Value;
        Session["referenceNumber"] = token.ReferenceNumber;
        Session["ProductName"] = productSet[0].ProductName.ToString();

        voucherCode.Value = "";
        Response.Redirect(ConfigurationManager.AppSettings["CheckEligibility"].Trim());
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
    
    public void generateNewCaptcha_Click(object sender, EventArgs e)
    {
        WebCaptcha.GenerateCaptcha(captchaImage, HttpContext.Current);
        captchaText.Value = "";
    }
}