using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class MBizDetails : System.Web.UI.Page
{
    public static ArrayList files = new ArrayList();
    TokenRequest token = new TokenRequest();
    GenerateToken generateToken = new GenerateToken();
    GetList getList = new GetList();
    protected void Page_Load(object sender, EventArgs e)
    {
        startDateTextBox.Attributes.Add("type", "date");
        startDateTextBox.Attributes.Add("required", "true");

        startDateTextBox.Attributes.Add("min", "1900-01-01");
        startDateTextBox.Attributes.Add("max", DateTime.Now.ToString("yyyy-MM-dd"));

        postalCode.Attributes.Add("pattern", "^[0-9]+$");      
        //FileUpload1.Attributes["style"] = "display:none";




        if (!IsPostBack)
        {
            GetListProvince();
        }
    }
   


   
    public void GetListProvince()
    {
        token.Token = generateToken.GenerateTokenAuth();
        IList<String> provList;
        provList = getList.GetListProvince(token);
        DDProvince.DataSource = provList;
        DDProvince.DataBind();

    }
    
    public void GetListCity(string Prov)
    {
        token.Province = Prov;
        token.Token = generateToken.GenerateTokenAuth();
        IList<String> cityList;
        cityList = getList.GetListCity(token);
        DDcity.DataSource = cityList;
        DDcity.DataBind();
    }
  

    protected void btnNext_Click(object sender, EventArgs e)
    {
        if(DDOwnership.SelectedIndex == 0)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Owner Property is required.')", true);
        }
        else
        {
            if (DDProvince.SelectedValue == "Select")
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Province is required.')", true);
            }
            else
            {
                if (DDcity.SelectedValue == "Select")
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('City is required.')", true);
                }
                else
                {
                    if (ten.Checked == true)
                    {
                        token.InsuredAmount = "10000";
                    }
                    else if (twenty.Checked == true)
                    {
                        token.InsuredAmount = "25000";
                    }
                    else if (fifty.Checked == true)
                    {
                        token.InsuredAmount = "50000";
                    }
                    token.BusinessName = businessName.Value;
                    token.StartOfBusiness = startDateTextBox.Value;
                    token.BusinessType = businessType.Value;
                    token.Address = businessAddress.Value;
                    token.Province = DDProvince.SelectedValue;
                    token.City = DDcity.SelectedValue;
                    token.ZipCode = postalCode.Value;
                    token.PropOwner = DDOwnership.SelectedValue;

                    token.ClientID = Session["ClientID"].ToString();
                    token.ReferenceCode = Session["ReferenceCode"].ToString();

                    token.Token = generateToken.GenerateTokenAuth();
                    TokenRequest InsertBusinessDetailsTran;
                    InsertBusinessDetailsTran = getList.MBPBusinessDetailsTran(token);

                    HttpFileCollection _HttpFileCollection = Request.Files;

                    if (_HttpFileCollection.Count > 2)
                    {
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Maximum of 2 photos should be allowed per application');", true);
                    }
                    else
                    {
                        for (int i = 0; i < _HttpFileCollection.Count; i++)
                        {
                            HttpPostedFile _HttpPostedFile = _HttpFileCollection[i];
                            bool hasError = false;
                            bool hasUploaded = false;
                            string extension = System.IO.Path.GetExtension(_HttpPostedFile.FileName).ToLower();
                            if (extension == ".jpg")
                            {

                                int fileSizeLimit = 3000000;
                                int fileSizeActual = _HttpPostedFile.ContentLength;

                                if (fileSizeActual <= fileSizeLimit)
                                {
                                    using (Stream s = _HttpPostedFile.InputStream)
                                    {
                                        using (BinaryReader br = new BinaryReader(s))
                                        {
                                            byte[] Databytes = br.ReadBytes((Int32)s.Length);
                                            token.AttachmentCategory = "MBP Property Image";
                                            token.Photo = Convert.ToBase64String(Databytes);
                                            TokenRequest InsertAttachment;
                                            InsertAttachment = getList.MBPInsertAttachments(token);

                                            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Business Details has succesfully updated.');window.location = 'MBizOwnerDetails2.aspx';", true);
                                        }
                                    }
                                }
                                else if (fileSizeActual > fileSizeLimit)
                                {
                                    Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Allowed attachment file size is up to 3MB per file.');", true);

                                    hasError = true;
                                }
                            }
                            else
                            {
                                hasError = true;
                                Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Invalid file format on item " + _HttpPostedFile.FileName + ". Allowed format is only: \\n 1. Image (*.jpg).');", true);

                                //ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Info", "alert('Invalid file format on item " + _HttpPostedFile.FileName + ". Allowed format is only: \n 1. Image (*.jpg)');", true);
                            }
                        }
                    }

                }
            }
        }

    }
    protected void btnPrevious_Click(object sender, EventArgs e)
    {
        Response.Redirect(ConfigurationManager.AppSettings["MBizQuestionnaire"].Trim());
    }
    protected void DDProvince_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetListCity(DDProvince.SelectedValue);
    }
    
       
}

  



   

