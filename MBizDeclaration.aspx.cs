using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MBizDeclaration : System.Web.UI.Page
{
    TokenRequest token = new TokenRequest();
    GenerateToken generateToken = new GenerateToken();
    GetList getList = new GetList();
    public string strMessage;
    protected void Page_Load(object sender, EventArgs e)
    {
        //Case: 1 When the page is submitted for the first time(First PostBack) and there is file 
        // in FileUpload control but session is Null then Store the values to Session Object as:
        if (Session["FileUpload1"] == null && PhotoUpload.HasFile)
        {
            Session["FileUpload1"] = PhotoUpload;
            lblImageName.Text = PhotoUpload.FileName;
            lblImageName.Visible = true;
        }
        // Case 2: On Next PostBack Session has value but FileUpload control is
        // Blank due to PostBack then return the values from session to FileUpload as:
        else if (Session["FileUpload1"] != null && (!PhotoUpload.HasFile))
        {
            PhotoUpload = (FileUpload)Session["FileUpload1"];
            lblImageName.Text = PhotoUpload.FileName;
            lblImageName.Visible = true;
        }
        // Case 3: When there is value in Session but user want to change the file then
        // In this case we need to change the file in session object also as:
        else if (PhotoUpload.HasFile)
        {
            Session["FileUpload1"] = PhotoUpload;
            lblImageName.Text = PhotoUpload.FileName;
            lblImageName.Visible = true;
        }
        else
        {
            lblImageName.Visible = false;
        }
            
    }

   



    protected void certifyCheckbox_CheckedChanged(object sender, EventArgs e)
    {

    }
    protected void btnNext_Click(object sender, EventArgs e)
    {
        if (certifyCheckbox.Checked == true)
        {
            if (PhotoUpload.HasFile)
            {
                string fileExt = Path.GetExtension(PhotoUpload.FileName).ToLower();
                string strMessage = "";
                int fileLength = PhotoUpload.FileName.Length;
                int fileSizeLimit = 3000000;
                int fileSizeActual = PhotoUpload.PostedFile.ContentLength;

                if (fileExt == ".jpg" || fileExt == ".jpeg")
                {
                    if (fileLength > 50)
                    {
                        strMessage = "File name is too long. File name should NOT be more than 50 characters ";
                        strMessage += "(including spaces and file extension).";
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('" + strMessage + "')", true);

                        Session["FileUpload1"] = null;
                    }
                    else
                    {
                        if (fileSizeActual <= fileSizeLimit)
                        {
                            token.ClientID = Session["ClientID"].ToString();   
                            //PhotoSaving
                            using (Stream s = PhotoUpload.PostedFile.InputStream)
                            {
                                using (BinaryReader br = new BinaryReader(s))
                                {
                                    byte[] Databytes = br.ReadBytes((Int32)s.Length);
                                    token.Photo = Convert.ToBase64String(Databytes);
                                    token.ReferenceCode = Session["ReferenceCode"].ToString();
                                    token.AttachmentCategory = "MBP Valid ID";
                                    token.Token = generateToken.GenerateTokenAuth();
                                    
                                    TokenRequest InsertPhoto;
                                    InsertPhoto = getList.MBPInsertClientPhoto(token);

                                }
                            }
                            

                            Response.Redirect(ConfigurationManager.AppSettings["MBizThankYou"].Trim());
                            lblImageName.Visible = false;
                        }
                        else if (fileSizeActual > fileSizeLimit)
                        {
                            strMessage = "Allowed attachment file size is up to 3MB per file.";
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('" + strMessage + "')", true);

                            Session["FileUpload1"] = null;
                            lblImageName.Visible = false;
                        }                        
                    }
                }
                else
                {
                    strMessage = @"Invalid file format. Allowed formats are as follows: \n";
                    strMessage += "1. Image (*.jpg, *.jpeg)";
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('" + strMessage + "')", true);

                    Session["FileUpload1"] = null;
                    lblImageName.Visible = false;
                }

            }
            else
            {
                strMessage = "Photo is required.";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('" + strMessage + "')", true);
                lblImageName.Visible = false;
            }
        }
        else
        {
            strMessage = @"You need to accept the terms and condition.";            
            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('" + strMessage + "')", true);
        }
        

        

    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect(ConfigurationManager.AppSettings["MBizPACoverage"].Trim());

    }
}

  



   

