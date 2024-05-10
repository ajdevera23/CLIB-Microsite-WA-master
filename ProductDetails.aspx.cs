using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;


public partial class ProductDetails : System.Web.UI.Page
{
    GetList getList = new GetList();
    TokenRequest token = new TokenRequest();
    GenerateToken generateToken = new GenerateToken();
    IList<ProductList> productSet;
    IList<ProductList> imageList;

    SecondaryProductRequest secondaryproductrequest = new SecondaryProductRequest();
    ProductProfileRequest productprofilerequest = new ProductProfileRequest();
    string vouchercode; // for the qrcode validation
    protected string placeholderHtml { get; set; }

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

            if (Request.QueryString["PROD"].ToString() == "FPGDT" || Request.QueryString["PROD"] == "FPGIT")
            {
                GetSecondaryProducts();
            }
          
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



    #region GET SECONDARY PRODUCTS
    public void GetSecondaryProducts()
    {
        try
        {
            token.Token = generateToken.GenerateTokenAuth();
            secondaryproductrequest.Token = token.Token;
            secondaryproductrequest.PartnerCode = Request.QueryString["PART"].ToString();
            secondaryproductrequest.PlatformId = 9;
            secondaryproductrequest.PlatformKey = ConfigurationManager.AppSettings["CLIBAPIKey"];
            secondaryproductrequest.PrimaryProductCode = Request.QueryString["PROD"].ToString();

            var returnValue = getList.IQRSecondaryProduct(secondaryproductrequest);
            var secondaryProducts = returnValue.Result;
            var productHtmlBuilder = new StringBuilder();

            foreach (var product in secondaryProducts)
            {
                var hrefValue = ConfigurationManager.AppSettings["CheckEligibility"] +
                                "?PART=" + Request.QueryString["PART"].ToString() +  
                                "&PROD=" + Request.QueryString["PROD"].ToString() + 
                                "&INTID=" + product.IntegrationId + 
                                "&PCODE=" + product.ProviderCode + 
                                "&SPCODE=" + product.ProductCode;


                productHtmlBuilder.AppendFormat(
                    @"<div class='d-style btn-brc-tp border-2 bgc-white btn-outline-blue btn-h-outline-blue btn-a-outline-blue w-100 my-2 py-3 shadow-sm'>
            <div class='row align-items-center'>
                <div class='col-12 col-md-4'>
                    <h4 class='pt-3 text-170 text-600 text-primary-d1 letter-spacing text-center'>
                        {0}
                    </h4>
                </div>
                <ul class='list-unstyled mb-0 col-12 col-md-4 text-dark-l1 text-90 text-center my-4 my-md-0'>
                    <li>
                        <div class='text-secondary-d1 text-110'>
                            <i class='fa fa-check text-success-m2 text-110 mr-2 mt-1'></i>
                            <span class='ml-n15 align-text-bottom'>₱</span><span class='text-180'>{1:N0}</span> / Plan
                        </div>
                    </li>
                </ul>
                <div class='col-12 col-md-4 text-center'>
                    <a href='{2}' class='f-n-hover btn btn-info btn-raised px-4 py-25 w-75 text-600'>Get Started</a>
                </div>
            </div>
        </div>",
                    product.ProductName,
                    product.CoreBenefitAmount,
                    hrefValue);
            }

            placeholderHtml = productHtmlBuilder.ToString();
        }
        catch (Exception)
        {

            throw;
        }

    }
    #endregion

}