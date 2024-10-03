using System;
using System.Collections.Generic;
using System.Configuration;
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
    string hiddenFieldValue = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {

        Session["pdfBase64"] = null;
        if (!IsPostBack)
        {
            SessionDisable();
            generateCaptcha();
        }
        else
        {

            hiddenFieldValue = Request.Form["save_me_name"];

            if(hiddenFieldValue == "sawakas")
            {
                SaveMe();
            }

            // Rebuild the document container on postback
            if (Request.Files.Count > 0)
            {
                HttpPostedFile uploadedFile = Request.Files[0];
                if (uploadedFile != null && uploadedFile.ContentLength > 0)
                {
                    string filePath = Server.MapPath("~/UploadedFiles/") + uploadedFile.FileName;
                    uploadedFile.SaveAs(filePath);

                    Response.Write("File uploaded successfully!");
                }
            }

            foreach (string benefitCode in SelectedBenefitCodes)
            {
                if (Session[benefitCode] != null)
                {
                    // Retrieve the stored documents from session
                    List<GetDocumentBasedOnBenefit> documents = (List<GetDocumentBasedOnBenefit>)Session[benefitCode];

                    // Display the documents for this benefit code
                    DisplayDocuments(documents, benefitCode);
                }
            }
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
                ReadOnlyTextFields();
                Session["BenefitCoverageId"] = returnValue.Result[0].BenefitCoverageId.ToString();
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "Swal.fire(`" + message + "`); ", true);
                DisplayValidateButton();
                SessionDisable();
                ClearAllFields();

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
    public void ReadOnlyTextFields()
    {
        referenceNumber.ReadOnly = true;
        firstName.ReadOnly = true;
        lastName.ReadOnly = true;
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
                ClearAllDocuments();
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

    #region  SelectedBenefitCodes
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
    #endregion

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
            getDocumentBasedOnBenefitRequest.ClaimsReferenceNumber = referenceNumber.Text.ToString();
            getDocumentBasedOnBenefitRequest.PlatformKey = ConfigurationManager.AppSettings["CLIBAPIKey"];

            var returnValue = getList.GetDocumentBasedOnBenefitRequest(getDocumentBasedOnBenefitRequest);
            string message = returnValue.Message;

            if (returnValue.ResultStatus == 0 && returnValue.Result != null && returnValue.Result.Count > 0)
            {
                // Retrieve or initialize document list from session
                List<GetDocumentBasedOnBenefit> currentDocuments = Session[benefitCode] as List<GetDocumentBasedOnBenefit> ?? new List<GetDocumentBasedOnBenefit>();

                // Append the new documents
                currentDocuments.AddRange(returnValue.Result);

                // Store the updated list in Session
                Session[benefitCode] = currentDocuments;

                // Call DisplayDocuments to render them in the UI
                DisplayDocuments(currentDocuments, benefitCode);
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

    private void DisplayDocuments(List<GetDocumentBasedOnBenefit> documents, string benefitCode)
    {

        foreach (var document in documents)
        {
            // Perform counter check to see if ClaimsDocumentsName already exists in the container
            if (!documentContainer.Controls.Cast<Control>().Any(c => c is Literal && ((Literal)c).Text.Contains(document.ClaimsDocumentsName)))
            {
                // Add the documents to a Literal or Placeholder control with a unique ID based on BenefitCode
                Literal litDocuments = new Literal();
                litDocuments.Text = "<div class='col-md-12 mt-3 mb-3'>" +
                        "<div id='doc_" + benefitCode + "'>" +
                            "<b>" + document.ClaimsDocumentsName + "</b> <br>File: " +
                            "<span id=\"file_name_" + document.ClaimsDocumentsId + "\" style='color:#f39c12;'>" + document.FileName + "</span>" +
                            //"<input type=\"file\" id=\"file_upload_" + document.ClaimsDocumentsId + "\" accept=\".jpg,.jpeg,.png,.pdf\" onchange="UploadFile(123)" hidden />" +
                            "<input type=\"file\" id=\"file_upload_" + document.ClaimsDocumentsId + "\" name=\"file_upload_" + document.ClaimsDocumentsId + "\" accept=\".jpg,.jpeg,.png,.pdf\" hidden />" +
                            "<input type=\"hidden\" name=\"documentId\" value=" + document.ClaimsDocumentsId + " />" +
                        "</div>" +

                        "<button type='button' id='btn_upload_" + document.ClaimsDocumentsId + "' data-value='" + document.ClaimsDocumentsId + " 'class='button' style='margin-inline-end: 5px;' " + ">" +
                        "<svg class='icon' xmlns='http://www.w3.org/2000/svg' width='20' height='20' viewBox='0 0 24 24' style='fill: #00263E;'>" +
                        "<path d='M13 19v-4h3l-4-5-4 5h3v4z'></path>" +
                        "<path d='M7 19h2v-2H7c-1.654 0-3-1.346-3-3 0-1.404 1.199-2.756 2.673-3.015l.581-.102.192-.558C8.149 8.274 9.895 7 12 7c2.757 0 5 2.243 5 5v1h1c1.103 0 2 .897 2 2s-.897 2-2 2h-3v2h3c2.206 0 4-1.794 4-4a4.01 4.01 0 0 0-3.056-3.888C18.507 7.67 15.56 5 12 5 9.244 5 6.85 6.611 5.757 9.15 3.609 9.792 2 11.82 2 14c0 2.757 2.243 5 5 5z'></path>" +
                        "</svg> Upload" +
                        "</button>" +

                        "<button type=\"button\" id=\"btn_download_" + document.ClaimsDocumentsId + "\"style =\"margin-inline-end: 5px\" class=\"button\"" + (!string.IsNullOrEmpty(document.FileName) ? "" : "disabled") +
                        " onclick='DownloadDocument(" + document.ClaimsDocumentsId + ")'>" +
                            "<svg id=\"dl_" + document.ClaimsDocumentsId + "\" xmlns=\"http://www.w3.org/2000/svg\" width=\"20\" height=\"20\" viewBox=\"0 0 24 24\" style =\"" + (!string.IsNullOrEmpty(document.FileName) ? "fill: #00263E;transform: ;msFilter:;" : "fill: gray; opacity: 0.5;") + "\">" +
                            "<path id=\"path1_dl_" + document.ClaimsDocumentsId + "\" d=\"M18.948 11.112C18.511 7.67 15.563 5 12.004 5c-2.756 0-5.15 1.611-6.243 4.15-2.148.642-3.757 2.67-3.757 4.85 0 2.757 2.243 5 5 5h1v-2h-1c-1.654 0-3-1.346-3-3 0-1.404 1.199-2.757 2.673-3.016l.581-.102.192-.558C8.153 8.273 9.898 7 12.004 7c2.757 0 5 2.243 5 5v1h1c1.103 0 2 .897 2 2s-.897 2-2 2h-2v2h2c2.206 0 4-1.794 4-4a4.008 4.008 0 0 0-3.056-3.888z\"></path>" +
                            "<path id=\"path2_dl_" + document.ClaimsDocumentsId + "\" d=\"M13.004 14v-4h-2v4h-3l4 5 4-5z\"></path>" +
                            "</svg> Download " +
                        "</button>" +

                            // Button to show document (with documentId being passed to JavaScript)
                            "<button AutoPostBack=\"true\" type=\"button\" id=\"btn_show_" + document.ClaimsDocumentsId +
                            "\" class=\"button\"" + (!string.IsNullOrEmpty(document.FileName) ? "" : "disabled") +
                            " onclick='showDocument(" + document.ClaimsDocumentsId + ")'>" +
                            "<svg id=\"mata_" + document.ClaimsDocumentsId + "\" xmlns=\"http://www.w3.org/2000/svg\" width=\"20\" height=\"20\" viewBox=\"0 0 24 24\" style=\"" +
                            (!string.IsNullOrEmpty(document.FileName) ? "fill: #00263E;" : "fill: gray; opacity: 0.5;") + "\">" +
                            "<path id=\"path1_" + document.ClaimsDocumentsId + "\" d=\"M12 9a3.02 3.02 0 0 0-3 3c0 1.642 1.358 3 3 3 1.641 0 3-1.358 3-3 0-1.641-1.359-3-3-3z\"></path>" +
                            "<path id=\"path2_" + document.ClaimsDocumentsId + "\" d=\"M12 5c-7.633 0-9.927 6.617-9.948 6.684L1.946 12l.105.316C2.073 12.383 4.367 19 12 19s9.927-6.617 9.948-6.684l.106-.316-.105-.316C21.927 11.617 19.633 5 12 5zm0 12c-5.351 0-7.424-3.846-7.926-5C4.578 10.842 6.652 7 12 7c5.351 0 7.424 3.846 7.926 5-.504 1.158-2.578 5-7.926 5z\"></path>" +
                            "</svg> Show " +
                            "</button>" +
                    "</div>";


                documentContainer.Controls.Add(litDocuments); // Assuming documentContainer is a placeholder in your UI


            }
        }
    }
    protected void btnHiddenShow_Click(object sender, EventArgs e)
    {
        // Get the document ID from the hidden field
        int documentId = int.Parse(hiddenDocumentId.Value);
        // Call your existing C# logic to retrieve or display the document
        GetExistingDocuments(documentId);
    }


    public void FailedList()
    {
        try
        {

        }
        catch (Exception ex)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "Swal.fire(`" + ex + "`); ", true);
            throw;
        }
    }

    public void InitialListUpload()
    {
        try
        {

        }
        catch (Exception ex)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "Swal.fire(`" + ex + "`); ", true);
            throw;
        }
    }

    public void SaveClaimsRequirementsRequest(HttpPostedFile httpPostedFile, long documentId)
    {
        try
        {
            SaveClaimsRequirementsRequest saveClaimsRequirementsRequest = new SaveClaimsRequirementsRequest();
            token.Token = generateToken.GenerateTokenAuth();

            string fileName = System.IO.Path.GetFileName(httpPostedFile.FileName);
            string fileType = System.IO.Path.GetExtension(httpPostedFile.FileName);

            string base64data = Base64Encoding(httpPostedFile);

            saveClaimsRequirementsRequest.Token = token.Token;
            saveClaimsRequirementsRequest.ClaimsDocumentsId = documentId;
            saveClaimsRequirementsRequest.ClaimsReferenceNumber = referenceNumber.Text.ToString();
            saveClaimsRequirementsRequest.FileLocation = "Not Applicable";
            saveClaimsRequirementsRequest.FileName = fileName;
            saveClaimsRequirementsRequest.FileType = fileType;
            saveClaimsRequirementsRequest.PlatformKey = ConfigurationManager.AppSettings["CLIBAPIKey"]; ;
            saveClaimsRequirementsRequest.CreatedBy = "";
            saveClaimsRequirementsRequest.FileData = base64data; // ITO UNG BASE 64

            var returnValue = getList.SaveClaimsRequirementsRequest(saveClaimsRequirementsRequest);
            string message = returnValue.Message;


            if (returnValue.ResultStatus == 0 && returnValue.Result != null)
            {
                hiddenFieldValue = string.Empty;
                Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "Swal.fire(`" + message + "`); ", true);
            }
            else
            {
                hiddenFieldValue = string.Empty;
                Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "Swal.fire(`" + message + "`); ", true);
            }

        }
        catch (Exception ex)
        {
            hiddenFieldValue = string.Empty;
            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "Swal.fire(`" + ex + "`); ", true);
            throw;
        }
    }

    private void ClearAllDocuments()
    {
        // Clear all the controls in the documentContainer
        documentContainer.Controls.Clear();
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


    #region OPEN FILE DIALOG
    protected void btnHiddenUpload_Click(object sender, EventArgs e)
    {
        int documentId = int.Parse(hiddenDocumentId.Value);

    }
    public void OpenFileDialogFile()
    {
        string script = @"
        document.querySelectorAll('[id^=""file_upload_""]').forEach(function(fileInput) {
            fileInput.addEventListener('change', function(event) {
                var id = event.target.id.split('_').pop(); // Extract the id from the input element
                var fileName = event.target.value.split('\\').pop(); // Get the file name
                var file = event.target.files[0]; // Get the selected file

                var maxSize = 3 * 1024 * 1024; // 3MB file size limit
                var allowedTypes = ['image/jpeg', 'image/png', 'image/gif', 'application/pdf']; // Allowed file types

                if (file) {
                    // Check if the file type is allowed
                    if (!allowedTypes.includes(file.type)) {
                        alert('Invalid file type. Please select a JPG, PNG, GIF, or PDF.');
                        event.target.value = ''; // Clear file input
                        document.getElementById('file_name_' + id).textContent = ''; // Clear displayed file name
                        document.getElementById('previewContainer').style.display = 'none'; // Hide preview container
                        selectedFile = null; // Clear selected file
                        return;
                    }

                    // Check if the file size exceeds the maximum limit
                    if (file.size > maxSize) {
                        alert('File size exceeds 3MB. Please select a smaller file.');
                        event.target.value = ''; // Clear file input
                        document.getElementById('file_name_' + id).textContent = ''; // Clear displayed file name
                        document.getElementById('previewContainer').style.display = 'none'; // Hide preview container
                        selectedFile = null; // Clear selected file
                        return;
                    }

                    // If everything is okay, store the selected file for later preview
                    document.getElementById('file_name_' + id).textContent = fileName;
                    selectedFile = file; // Store the file for the modal preview

                    // Optional: Show a success message
                    alert('File selected: ' + fileName);
                } else {
                    document.getElementById('previewContainer').style.display = 'none';
                    selectedFile = null; // Clear selected file
                }
            });
        });
    ";

        ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowModal", script, true);
    }
    #endregion

    #region GetExistingDocuments
    public void GetExistingDocuments(int ClaimsDocumentID)
    {
        try
        {
            GetExistingDocumentsRequest getExistingDocumentsRequest = new GetExistingDocumentsRequest();

            token.Token = generateToken.GenerateTokenAuth();
            getExistingDocumentsRequest.Token = token.Token;
            getExistingDocumentsRequest.ClaimsDocumentsId = ClaimsDocumentID;
            getExistingDocumentsRequest.ClaimsReferenceNumber = referenceNumber.Text.ToString();
            getExistingDocumentsRequest.PlatformKey = ConfigurationManager.AppSettings["CLIBAPIKey"];

            var returnValue = getList.GetExistingDocuments(getExistingDocumentsRequest);
            string message = returnValue.Message;

            if (returnValue.ResultStatus == 0 && returnValue.Result != null && returnValue.Result.Count > 0)
            {
                Session["pdfBase64"] = returnValue.Result[0].FileData.ToString();
                Session["FileName"] = returnValue.Result[0].FileName.ToString();
                Session["FileType"] = returnValue.Result[0].FileType.ToString();
                Session["FileLocation"] = returnValue.Result[0].FileLocation.ToString();



                if (Session["pdfBase64"] != null)
                {
                    string script = @"
                        var base64PDF = '" + Session["pdfBase64"] + @"';
                        if (base64PDF !== null && base64PDF.trim() !== '') {
                            var modalfilepreview = new bootstrap.Modal(document.getElementById('myModal'));
                            document.getElementById('filePreview').src = 'data:application/pdf;base64,' + base64PDF;
                            modalfilepreview.show();
                        }";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowModal", script, true);
                }

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

    #region CLICK DOWNLOAD DOCUMENT
    protected void btnDownloadDocument_Click(object sender, EventArgs e)
    {
        int documentId = int.Parse(hiddenDocumentId.Value);
        GetExistingDocuments(documentId);

        // Example Base64 string (replace with your actual Base64 data)
        string base64FileString = Session["pdfBase64"].ToString();
        // File details
        string fileName = Session["FileName"].ToString();  // Specify your file name
        string contentType = Session["FileType"].ToString();  // Specify your file type
        // Trigger the file download
        DownloadBase64File(base64FileString, fileName, contentType);
    }
    protected void DownloadBase64File(string base64String, string fileName, string contentType)
    {
        // Convert Base64 string to byte array
        byte[] fileBytes = Convert.FromBase64String(base64String);

        // Clear the response
        Response.Clear();

        // Set the content type
        Response.ContentType = contentType;

        // Set the content disposition for downloading the file
        Response.AddHeader("Content-Disposition", string.Format("attachment; filename={0}", fileName));

        // Write the file bytes to the response output stream
        Response.BinaryWrite(fileBytes);

        // End the response
        Response.End();
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
        if (string.IsNullOrEmpty(referenceNumber.Text.ToString()))
        {
            ClientScript.RegisterStartupScript(this.GetType(), "alert", "Swal.fire('Please Enter Claim Reference Number.');", true);
            return;
        }
        if (string.IsNullOrEmpty(firstName.Text.ToString()))
        {
            ClientScript.RegisterStartupScript(this.GetType(), "alert", "Swal.fire('Please Enter First Name.');", true);

            return;
        }
        if (string.IsNullOrEmpty(lastName.Text.ToString()))
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
        UpdatePanel1.Update();
    }

    //protected void btn_Submit_Click(object sender, EventArgs e)
    //{

    //    List<long> documentIds = new List<long>();

    //    foreach (string key in Request.Form.Keys)
    //    {
    //        if (key == "documentId")
    //        {
    //            string documentIdValues = Request.Form[key];
    //            string[] documentIdArray = documentIdValues.Split(',');
    //            foreach (string documentIdValue in documentIdArray)
    //            {
    //                long documentId;
    //                if (long.TryParse(documentIdValue, out documentId))
    //                {
    //                    documentIds.Add(documentId);
    //                }
    //            }
    //        }
    //    }

    //    foreach (string key in Request.Form)
    //    {
    //        string value = Request.Form[key];
    //    }

    //    foreach (var documentId in documentIds)
    //    {
    //        string fileUploadControlId = "file_upload_" + documentId;

    //        // Find the FileUpload control in documentContainer.Controls
    //        FileUpload fileUploadControl = documentContainer.FindControl(fileUploadControlId) as FileUpload;

    //        // Check if the control exists and has a file
    //        if (fileUploadControl != null && fileUploadControl.HasFile)
    //        {
    //            // Process the uploaded file
    //            SaveClaimsRequirementsRequest(fileUploadControl.PostedFile, documentId);
    //        }
    //    }
    //}

    protected void SaveMe()
    {
        List<long> documentIds = new List<long>();

        foreach (string key in Request.Form.Keys)
        {
            if (key == "documentId")
            {
                string documentIdValues = Request.Form[key];
                string[] documentIdArray = documentIdValues.Split(',');
                foreach (string documentIdValue in documentIdArray)
                {
                    long documentId;
                    if (long.TryParse(documentIdValue, out documentId))
                    {
                        documentIds.Add(documentId);
                    }
                }
            }
        }
        foreach (var documentId in documentIds)
        {
            string fileInputName = "file_upload_" + documentId;
            HttpPostedFile uploadedFile = Request.Files[fileInputName];

            if (uploadedFile != null && uploadedFile.ContentLength > 0)
            {
                SaveClaimsRequirementsRequest(uploadedFile, documentId);
            }
        }
        hiddenFieldValue = string.Empty;
    }


    public string Base64Encoding(HttpPostedFile httpPostedFile)
    {
        using (var binaryReader = new System.IO.BinaryReader(httpPostedFile.InputStream))
        {
            byte[] fileBytes = binaryReader.ReadBytes(httpPostedFile.ContentLength);

            string base64String = Convert.ToBase64String(fileBytes);

            return base64String;
        }

    }
}