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

    private List<string> SelectedBenefitCodes
    {
        get
        {
            // Return the list from ViewState if available, otherwise initialize a new list
            return ViewState["SelectedBenefitCodes"] as List<string> ?? new List<string>();
        }
        set
        {
            // Store the list in ViewState
            ViewState["SelectedBenefitCodes"] = value;
        }
    }


    #region GetDocumentsForSelectedBenefits
    public void GetDocumentsForSelectedBenefits()
    {
        // Loop through the selected benefit codes and append documents for each
        foreach (string benefitCode in SelectedBenefitCodes)
        {
            // Fetch and display documents for the benefit
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
                // Display or append the documents in the UI if the request is successful
                DisplayDocuments(returnValue.Result, benefitCode);
            }
            else
            {
                // Optional: Handle the case when no documents are found
                // Example: Show a message to the user
            }
        }
        catch (Exception ex)
        {
            // Handle the exception
            ClientScript.RegisterStartupScript(this.GetType(), "alert", "Swal.fire('Error: " + ex.Message + "');", true);
        }
    }
    #endregion

    // Method to append documents to the UI
    private void DisplayDocuments(List<GetDocumentBasedOnBenefit> documents, string benefitCode)
    {
        foreach (var document in documents)
        {
            // Add the documents to a Literal or Placeholder control with a unique ID based on BenefitCode
            Literal litDocuments = new Literal();
            litDocuments.Text = "<div id='doc_" + benefitCode + "'>Document Name: " + document.ClaimsDocumentsName + "</div>";
            documentContainer.Controls.Add(litDocuments); // Assuming documentContainer is a placeholder in your UI
        }
    }

    // Method to clear specific documents from the UI based on BenefitCode
    private void ClearDocuments(string benefitCode)
    {
        // Prepare a list to store the controls to be removed
        List<Control> controlsToRemove = new List<Control>();

        foreach (Control control in documentContainer.Controls)
        {
            // Check if the control is a Literal and has the corresponding BenefitCode in its ID
            Literal literal = control as Literal;
            if (literal != null && literal.Text.Contains("id='doc_" + benefitCode + "'"))
            {
                controlsToRemove.Add(literal);
            }
        }

        // Remove all the controls related to the unchecked BenefitCode
        foreach (Control controlToRemove in controlsToRemove)
        {
            documentContainer.Controls.Remove(controlToRemove);
        }
    }

    // Event handler when a checkbox is checked or unchecked
    protected void chkBenefit_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox chkBenefit = (CheckBox)sender;
        RepeaterItem item = (RepeaterItem)chkBenefit.NamingContainer;

        string benefitCode = ((HiddenField)item.FindControl("hiddenBenefitCode")).Value;
        bool isChecked = chkBenefit.Checked;

        // Access the persisted list
        List<string> selectedCodes = SelectedBenefitCodes;

        if (isChecked)
        {
            if (!selectedCodes.Contains(benefitCode))
            {
                selectedCodes.Add(benefitCode);
            }
        }
        else
        {
            if (selectedCodes.Contains(benefitCode))
            {
                selectedCodes.Remove(benefitCode);
                ClearDocuments(benefitCode);
            }
        }

        // Update the ViewState-stored list
        SelectedBenefitCodes = selectedCodes;

        // Append or clear documents based on the current list
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