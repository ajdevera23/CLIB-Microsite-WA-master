using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class TermsAndConditions : System.Web.UI.Page
{

    GetList getList = new GetList();
    TokenRequest token = new TokenRequest();
    GenerateToken generateToken = new GenerateToken();
    IList<ProductList> productSet;
    IList<ProductList> imageList;

    ProductProfileRequest productprofilerequest = new ProductProfileRequest();

    string vouchercode; // for the qrcode validation
    protected void Page_Load(object sender, EventArgs e)
    {

        #region GET PRODUCT DETAILS
        vouchercode = ConfigurationManager.AppSettings["CLIBvoucherCode1"] ?? ConfigurationManager.AppSettings["CLIBvoucherCode2"] ?? ConfigurationManager.AppSettings["CLIBvoucherCode"];
        token.Token = generateToken.GenerateTokenAuth();
        #endregion


        if (!IsPostBack)
        {
            Session["pdfBase64"] = null;
            GetProductProfile();


        }
        if (!string.IsNullOrEmpty(Request.QueryString["PART"]) && !string.IsNullOrEmpty(Request.QueryString["PROD"]))

        {

            Session["voucherCode"] = vouchercode;
            Session["referenceCode"] = token.Token = generateToken.GenerateTokenAuth();
            Session["CheckEligibility"] = ConfigurationManager.AppSettings["CheckEligibility"] + "?PART=" + Request.QueryString["PART"] + "&PROD=" + Request.QueryString["PROD"] + "&INTID=" + Request.QueryString["INTID"] + "&PCODE=" + Request.QueryString["PCODE"];
            Session["partnerCode"] = Request.QueryString["PART"];
            Session["productCode"] = Request.QueryString["PROD"];

        }


    }


    public void GetProductProfile()
    {
        try
        {
            // Retrieve the Base64 string representing the PDF
            token.Token = generateToken.GenerateTokenAuth();
            productprofilerequest.Token = token.Token;
            productprofilerequest.PlatformKey = ConfigurationManager.AppSettings["CLIBAPIKey"];
            productprofilerequest.ProductCode = Request.QueryString["PROD"].ToString();

            var returnValue = getList.GetProductProfile(productprofilerequest);

            if (returnValue.Message == "File does not exist")
            {
                Session["IsPDFAvailable"] = "Not Available";
            }
            else
            {
                Session["IsPDFAvailable"] = "Available";
                var base64 = returnValue.Result.ToString();
                Session["pdfBase64"] = base64.ToString();

            }



        }
        catch (Exception ex)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "Swal.fire(`" + ex + "`); ", true);
        }
    }


}