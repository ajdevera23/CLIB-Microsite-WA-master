using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebCaptchaLib;

public partial class ClientReferral : System.Web.UI.Page
{
    TokenRequest token = new TokenRequest();
    GenerateToken generateToken = new GenerateToken();
    GetList getList = new GetList();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            SessionDisable();
            generateCaptcha();
        }
    }
    #region GET VALIDATE CLAIMS
    public void GetValidateClaims()
    {
        try
        {
            GetClaimsIfExistRequest getclaimsexistrequest = new GetClaimsIfExistRequest();
            token.Token = generateToken.GenerateTokenAuth();
            getclaimsexistrequest.Token = token.Token;
            getclaimsexistrequest.ClaimsReferenceNumber = referenceNumber.Text.ToString();
            getclaimsexistrequest.FirstName = firstName.Text.ToString();
            getclaimsexistrequest.LastName = lastName.Text.ToString();
            getclaimsexistrequest.PlatformKey = ConfigurationManager.AppSettings["CLIBAPIKey"];
            var returnValue = getList.GetClaimsIfExists(getclaimsexistrequest);
            string message = returnValue.Message;

            if (returnValue.ResultStatus == 0)
            {
                SessionEnable();
                GetNatureOfClaims();
                HideValidateButton();
               Session["BenefitCoverageId"] =  returnValue.Result[0].BenefitCoverageId.ToString();
            }
            else
            {
                DisplayValidateButton();
                SessionDisable();
                ClearAllFields();
                Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "Swal.fire(`" + message + "`); ", true);
            }
        }
        catch (Exception ex)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "Swal.fire(`" + ex + "`); ", true);
            ClearAllFields();
            throw;
        }


    }
    #endregion
    public void DisplayValidateButton()
    {
        ValidateButton.Style["display"] = "block";
    }
    public void HideValidateButton()
    {
        ValidateButton.Style["display"] = "none";
    }
    public void SessionEnable()
    {
        Session["EnableClaims"] = true;
    }
    public void SessionDisable()
    {
        Session["EnableClaims"] = false;
    }
    #region GET NATURE OF CLAIMS
    public void GetNatureOfClaims()
    {
        try
        {
            GetNatureofClaimRequest getnatureofclaimrequest = new GetNatureofClaimRequest();
            token.Token = generateToken.GenerateTokenAuth();
            getnatureofclaimrequest.Token = token.Token;
            getnatureofclaimrequest.PlatformKey = ConfigurationManager.AppSettings["CLIBAPIKey"];
            var returnValue = getList.GetNatureofClaimRequest(getnatureofclaimrequest);
            string message = returnValue.Message;

            if (returnValue.ResultStatus == 0 && returnValue.Result != null)
            {
                natureofclaimDropdownlist.Items.Clear();
                natureofclaimDropdownlist.Items.Insert(0, new ListItem("Select", "Select"));
                ListItem newItem = new ListItem(returnValue.Result.NatureOfClaim, returnValue.Result.NatureOfClaim);
                natureofclaimDropdownlist.Items.Add(newItem);
                Session["NatureOfClaim"] = returnValue.Result.NatureOfClaim.ToString();
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "Swal.fire(`" + message + "`); ", true);
            }

        }
        catch (Exception ex)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "Swal.fire(`" + ex + "`); ", true);
            throw;
        }
    }
    #endregion

    #region CLEAR FIELDS
    public void ClearAllFields()
    {
        referenceNumber.Text = "";
        firstName.Text = "";
        lastName.Text = "";
    }
    #endregion

    #region Captcha
    public void generateNewCaptcha_Click(object sender, EventArgs e)
    {
        WebCaptcha.GenerateCaptcha(captchaImage, HttpContext.Current);
        captchaText.Value = "";
    }
    #endregion


    private List<string> selectedBenefitCodes = new List<string>();

    #region GetDocumentsForSelectedBenefits
    public void GetDocumentsForSelectedBenefits()

        documentContainer.Controls.Clear();

        foreach (string benefitCode in selectedBenefitCodes)
        {
       
            GetDocumentBasedOnBenefitRequest(benefitCode);
        }
    }
    #endregion

    #region GetDocumentBasedOnBenefitRequest
    public void GetDocumentBasedOnBenefitRequest(string benefitCode)
    {
        try
        {
            GetDocumentBasedOnBenefitRequest getDocumentBasedOnBenefitRequest = new GetDocumentBasedOnBenefitRequest();
            token.Token = generateToken.GenerateTokenAuth();
            getDocumentBasedOnBenefitRequest.Token = token.Token;
            getDocumentBasedOnBenefitRequest.BenefitCode = benefitCode;
            getDocumentBasedOnBenefitRequest.PlatformKey = ConfigurationManager.AppSettings["CLIBAPIKey"];

            var returnValue = getList.GetDocumentBasedOnBenefitRequest(getDocumentBasedOnBenefitRequest);
            string message = returnValue.Message;

            if (returnValue.ResultStatus == 0 && returnValue.Result != null && returnValue.Result.Count > 0)
            {
       
                DisplayDocuments(returnValue.Result, benefitCode);
            }
            else
            {
     
            }
        }
        catch (Exception ex)
        {
     
            ClientScript.RegisterStartupScript(this.GetType(), "alert", "Swal.fire('Error: " + ex.Message + "');", true);
        }
    }
    #endregion


    private void DisplayDocuments(List<GetDocumentBasedOnBenefit> documents, string benefitCode)
    {
        foreach (var document in documents)
        {
            Literal litDocuments = new Literal();
            litDocuments.Text = "<div id='doc_" + benefitCode + "'>Document Name: " + document.ClaimsDocumentsName + "</div>";
            documentContainer.Controls.Add(litDocuments); // Assuming documentContainer is a placeholder in your UI
        }
    }

    private void ClearDocuments(string benefitCode)
    {

        List<Control> controlsToRemove = new List<Control>();

        foreach (Control control in documentContainer.Controls)
        {

            Literal literal = control as Literal;
            if (literal != null && literal.Text.Contains("id='doc_" + benefitCode + "'"))
            {
                controlsToRemove.Add(literal);
            }
        }
        foreach (Control controlToRemove in controlsToRemove)
        {
            documentContainer.Controls.Remove(controlToRemove);
        }
    }


    protected void chkBenefit_CheckedChanged(object sender, EventArgs e)
    {

        CheckBox chkBenefit = (CheckBox)sender;
        RepeaterItem item = (RepeaterItem)chkBenefit.NamingContainer;

        string benefitCode = ((HiddenField)item.FindControl("hiddenBenefitCode")).Value;

       
        bool isChecked = chkBenefit.Checked;

        if (isChecked)
        {
       
            if (!selectedBenefitCodes.Contains(benefitCode))
            {
                selectedBenefitCodes.Add(benefitCode);
            }
        }
        else
        {

            if (selectedBenefitCodes.Contains(benefitCode))
            {
                selectedBenefitCodes.Remove(benefitCode);
                ClearDocuments(benefitCode);
            }
        }

        GetDocumentsForSelectedBenefits();
    }



    #region GetBenefitByNatureOfClaimRequest
    public void GetBenefitByNatureOfClaimRequest(string NatureOfClaim)
    {
        try
        {
            GetBenefitByNatureOfClaimRequest getBenefitByNatureOfClaimRequest = new GetBenefitByNatureOfClaimRequest();
            token.Token = generateToken.GenerateTokenAuth();
            getBenefitByNatureOfClaimRequest.Token = token.Token;
            getBenefitByNatureOfClaimRequest.BenefitCoverageId = Convert.ToInt32(Session["BenefitCoverageId"]);
            getBenefitByNatureOfClaimRequest.PlatformKey = ConfigurationManager.AppSettings["CLIBAPIKey"];
            getBenefitByNatureOfClaimRequest.NatureOfClaim = NatureOfClaim;

            var returnValue = getList.GetBenefitByNatureOfClaimRequest(getBenefitByNatureOfClaimRequest);
            string message = returnValue.Message;

            if (returnValue.ResultStatus == 0 && returnValue.Result != null && returnValue.Result.Count > 0)
            {
                // Show the header if data is available
                tableHeader.Style["display"] = "table-header-group";
                // Bind the JSON result to the Repeater
                rptBenefits.DataSource = returnValue.Result;
                rptBenefits.DataBind();
            }
            else
            {
                // Hide the header if no data
                tableHeader.Style["display"] = "none";
                // Clear the Repeater table by setting the DataSource to null and binding it
                rptBenefits.DataSource = null;
                rptBenefits.DataBind();
            }

        }
        catch (Exception ex)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "Swal.fire(`" + ex + "`); ", true);
            throw;
        }

    }
    #endregion

    public void generateCaptcha()
    {
        #region Captcha
        if (captchaText.Value == "")
        {
            WebCaptcha.GenerateCaptcha(captchaImage, HttpContext.Current);
        }
        #endregion
    }
    protected void ValidateButton_Click(object sender, EventArgs e)
    {
        if(string.IsNullOrEmpty(referenceNumber.Text.ToString()))
        {
            ClientScript.RegisterStartupScript(this.GetType(), "alert", "Swal.fire('Please Enter Claim Reference Number.');", true);
            return;
        }
        if(string.IsNullOrEmpty(firstName.Text.ToString()))
        {
            ClientScript.RegisterStartupScript(this.GetType(), "alert", "Swal.fire('Please Enter First Name.');", true);

            return;
        }
        if(string.IsNullOrEmpty(lastName.Text.ToString()))
        {
            ClientScript.RegisterStartupScript(this.GetType(), "alert", "Swal.fire('Please Enter Last Name.');", true);

            return;
        }
        else
        {
            GetValidateClaims();
        }
          
    }
    protected void natureofclaimDropdownlist_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetBenefitByNatureOfClaimRequest(natureofclaimDropdownlist.SelectedValue);
    }
}