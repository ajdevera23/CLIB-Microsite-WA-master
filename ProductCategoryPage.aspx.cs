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

public partial class Public_ProductCategoryPage : System.Web.UI.Page
{
    GetList getList = new GetList();
    TokenRequest token = new TokenRequest();
    GenerateToken generateToken = new GenerateToken();
    IList<CategoryResult> productCategorySet;
    IList<CategoryResult> imageList;
    string voucherCode;
    protected void Page_Load(object sender, EventArgs e)

    {
        Exception ex = Server.GetLastError();
        if (ex != null)
        {
            // Log the error for debugging purposes
            // You can log it to a file, database, or other storage.

            // Display a user-friendly error message to the user
            Response.Redirect(ConfigurationManager.AppSettings["TermsAndConditions"].Trim());
        }

        if (!string.IsNullOrEmpty(Session["voucherCode"] as string))
        {
            voucherCode = Session["voucherCode"].ToString();

            token.PlatformName = ConfigurationManager.AppSettings["CLIBPlatformName"];
            token.ProductCode = GetPartnerValue();


            if (voucherCode == ConfigurationManager.AppSettings["CLIBvoucherCode"]
                || voucherCode == ConfigurationManager.AppSettings["CLIBvoucherCode1"]
                || voucherCode == ConfigurationManager.AppSettings["CLIBvoucherCode2"])
            {

                token.Token = generateToken.GenerateTokenAuth();
                productCategorySet = getList.PopulateProductCategoryGridView(token);
                imageList = LoadImage(productCategorySet);
                table.Text = (ProductCategoryTable(imageList));


                if(imageList.Count == 0)
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "Swal.fire({ title: 'No products to show under this category at the moment.', icon: 'warning', showCancelButton: false, confirmButtonColor: '#3085d6', confirmButtonText: 'OK' }).then(function(result) { if (result.value) { window.location.href = 'ProductRegistration.aspx'; } });", true);

                }

            }

        }


    }

    private string GetPartnerValue()
    {
        try
        {
            // Get the value of the query string parameter "PART"
            string partParam = Request.QueryString["PART"];

            // If the PART parameter is found and not empty, return its value
            if (!string.IsNullOrEmpty(partParam))
            {
                return partParam;
            }
            // If the PART parameter is not found or empty, use default logic
            else if (Session["PartnerValue"] != null)
            {
                // If the session partner value is available, return it
                return Session["PartnerValue"].ToString();
            }
            else
            {
                // Use default partner value if neither query string nor session partner value is available
                return "DefaultPartnerValue";
            }
        }
        catch (NullReferenceException)
        {
            // Use default partner value if an exception occurs during retrieval
            return "DefaultPartnerValue";
        }
    }

    private IList<CategoryResult> LoadImage(IList<CategoryResult> list)
    {
        //list = list.Select(r => string.Concat("<a href='" + ConfigurationManager.AppSettings["ProductPage"] +"?a="+r+"'><img src='"+ConfigurationManager.AppSettings["productImagePath"], r, "' ></a>")).ToList();
        StringBuilder html = new StringBuilder();
        StringBuilder html1 = new StringBuilder();
        StringBuilder html2 = new StringBuilder();
        StringBuilder html3 = new StringBuilder();
        StringBuilder html4 = new StringBuilder();

        html.AppendLine("<a href='" + ConfigurationManager.AppSettings["ProductPage"] + "?PART=" + Session["PartnerValue"].ToString() + "&a=");
        html1.AppendLine("&b=");
        html4.AppendLine("&c=");
        html3.AppendLine("'><img src='" + ConfigurationManager.AppSettings["productImagePath"]);
        html2.AppendLine("' ></a>");
        
        foreach (var item in    list)
        {
            item.IconPath = html.ToString() + item.CategoryCode + html1.ToString() + item.CategoryName + html4.ToString() + item.IconPath + html3.ToString()
                + item.IconPath + html2.ToString();
        }
        
        return list;
    }
    private string ProductCategoryTable(IList<CategoryResult> list)
    {
        string returnValue="";
        int item = 0;
        StringBuilder htmlTable = new StringBuilder();
        htmlTable.AppendLine("<table class='table-responsive'>");
        int count = (((list.Count - 1) / 2) + 1);
        for (int j = 0; j < count ; j++)
        {
            htmlTable.AppendLine("<tr>");
            
            for (int i = 0; i < 2; i++)
            {
                if (item < list.Count)
                {
                    if (item == list.Count-1)
                    {
                        htmlTable.AppendLine("<td align='center'>");
                    }
                    else
                    {
                        htmlTable.AppendLine("<td>");
                    }
                    htmlTable.AppendLine(list.ElementAt(item).IconPath);
                    htmlTable.AppendLine("</td>");
                }
                    item++;
            }
            htmlTable.AppendLine("</tr>");
        }
            
        htmlTable.AppendLine("</table>");
        returnValue = htmlTable.ToString();
        return returnValue;
    }
    
}