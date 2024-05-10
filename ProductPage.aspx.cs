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

public partial class Public_ProductPage : System.Web.UI.Page
{
    GetList getList = new GetList();
    TokenRequest token = new TokenRequest();
    GenerateToken generateToken = new GenerateToken();
    IList<ProductList> productSet;
    IList<ProductList> imageList;
    string voucherCode;
    protected void Page_Load(object sender, EventArgs e)
    {
        token.PlatformName = ConfigurationManager.AppSettings["CLIBPlatformName"];
        token.PartnerCode = Request.QueryString["PART"].ToString();

        Session["PartnerValue"] = Request.QueryString["PART"].ToString();


        if (!string.IsNullOrEmpty(Request.QueryString["b"]) || !string.IsNullOrEmpty(Request.QueryString["a"]) || !string.IsNullOrEmpty(Request.QueryString["c"]))
        {
            //if (!string.IsNullOrEmpty(Session["voucherCode"] as string))
            //{

            //}

            categoryLabel.Text = HttpUtility.HtmlEncode(Request.QueryString["b"].ToString());
            token.CategoryCode = Request.QueryString["a"];


            Session["CategoryCodeForHidingPrice"] = token.CategoryCode.ToString();

            if (token.CategoryCode.Length > 4 || System.Text.RegularExpressions.Regex.IsMatch(token.CategoryCode, @"[^a-zA-Z0-9]"))
            {
                Response.Redirect(ConfigurationManager.AppSettings["ProductRegistrationIQR"].Trim());
            }
            else
            {

                token.Token = generateToken.GenerateTokenAuth();
                productSet = getList.PopulateProductGridView(token);

                string imagePath = ConfigurationManager.AppSettings["productImagePath"] + Request.QueryString["c"];
                categoryImage.Style["background-image"] = "url('" + ResolveUrl(imagePath) + "')";

                var finalProductSet2 = LoadImage(productSet);

                productGridView.DataSource = finalProductSet2;
                productGridView.DataBind();

                if (finalProductSet2.Count == 0)
                {
                    label1.InnerText = "No products to show under this category at the moment.";
                    StringBuilder html = new StringBuilder();
                }
            }

        }


    }


    private IList<ProductList> LoadImage(IList<ProductList> list)
    {
        StringBuilder html = new StringBuilder();
        StringBuilder html1 = new StringBuilder();
        StringBuilder html2 = new StringBuilder();
        StringBuilder INTID = new StringBuilder();
        StringBuilder PCODE = new StringBuilder();
        StringBuilder PART = new StringBuilder();
        StringBuilder PROD = new StringBuilder();


        //html.AppendLine("<a href='" + ConfigurationManager.AppSettings["EnrollmentPage"] + "?PART=PJ4&PROD=");
        html.AppendLine("<a href='" + ConfigurationManager.AppSettings["ProductDetails"]);

        html1.AppendLine("'><img src='" + ConfigurationManager.AppSettings["productImagePath"]);
        html2.AppendLine("'></a>");
        INTID.AppendLine("&INTID=");
        PCODE.AppendLine("&PCODE=");
        //PART.AppendLine("?PART=" + ConfigurationManager.AppSettings["IQR"]);
        PART.AppendLine("?PART=" + Session["PartnerValue"].ToString());
        PROD.AppendLine("&PROD=");

        foreach (var item in list)
        {
            item.IconPath = html.ToString() + PART.ToString() + PROD.ToString() + item.ProductCode + INTID.ToString() + item.IntegrrationId + PCODE.ToString() + item.ProviderCode + html1.ToString() + item.IconPath + html2.ToString();

        }

        return list;
    }
    private string ProductCategoryTable(IList<String> list)
    {
        string returnValue="";
        int item = 0;
        StringBuilder htmlTable = new StringBuilder();
        htmlTable.AppendLine("<table class='table-responsive'>");
        
        for (int j = 0; j < list.Count/2 ; j++)
        {
            htmlTable.AppendLine("<tr>");
            
            for (int i = 0; i < 2; i++)
            {
                htmlTable.AppendLine("<td>");
                htmlTable.AppendLine(list.ElementAt(item));
                htmlTable.AppendLine("</td>");
                item++;
            }
            htmlTable.AppendLine("</tr>");
        }
            
        htmlTable.AppendLine("</table>");
        returnValue = htmlTable.ToString();
        return returnValue;
    }

    protected void productGridView_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            HyperLink lnkMore = (HyperLink)e.Row.FindControl("link");
            Label lbl = (Label)e.Row.FindControl("Label1");
            lnkMore.NavigateUrl = "~/Company.aspx?cmp=" + lbl.Text;
        }
    }

}