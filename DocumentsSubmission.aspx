<%@ Page Title="" Language="C#" MasterPageFile="~/Claims.master" AutoEventWireup="true" CodeFile="DocumentsSubmission.aspx.cs" Inherits="ClientReferral" MaintainScrollPositionOnPostback="true" %>

<asp:Content ContentPlaceHolderID="infoForm" runat="server">
    <link href="Style/ClaimsAI/documentsubmission.css" rel="stylesheet" />
    <form id="enrollmentForm" class="container body-content container-enrollment" method="post" autocomplete="On" runat="server" enctype="multipart/form-data" novalidate>
        <input type="hidden" name="docId" id="docIdInput" value="">
        <input type="hidden" id="param_for_saving" name="param_for_saving" autocomplete="off" />
        <asp:HiddenField ID="hiddenDocumentId" runat="server" />
        <asp:Button ID="btnHiddenShow" runat="server" OnClick="btnHiddenShow_Click" Style="display: none;" />
        <asp:Button ID="btnDownloadDocument" runat="server" OnClick="btnDownloadDocument_Click" Style="display: none;" />
        <asp:Button ID="btnHiddenUpload" runat="server" OnClick="btnHiddenUpload_Click" Style="display: none;" />


        <div class="container">
            <div class="overlay-page">
                <div class="spinner-page">
                    <img src="Images/CebLogAnima.gif" />
                </div>
            </div>
            <div class="app-accordion" id="accordian1id" app-accordian>
                <!-- Accordion Item 1 -->
                <div class="app-collapse" app-collapse>
                    <div class="collapse-header-button" aria-expanded="true" aria-controls="sect1" id="collapse1id" indipendente="false" app-collapse-header-btn>
                        <span class="collapse-title">
                            <b>Claims Information</b>
                            <svg xmlns="http://www.w3.org/2000/svg" fill="#fff" viewBox="0 0 156 82" class="collapse-icon">
                                <path class="icon-path-line" />
                            </svg>
                        </span>
                    </div>
                    <div id="sect1" role="region" aria-labelledby="collapse1id" class="collapse-panel app-collapse-expanded">
                        <div class="collapse-panel-inner-wrapper content-text">
                            <!-- Content for Claims Information -->
                            <div class="row col-md-12 mt-4">
                                <div class="form-group col-md-4">
                                    <label for="reference-number">Claims Reference Number</label>
                                    <asp:TextBox ID="referenceNumber" runat="server" CssClass="form-control" Placeholder="Claims Reference Number"></asp:TextBox>
                                </div>
                                <div class="form-group col-md-4">
                                    <label for="first-name">Insured's First Name</label>
                                    <asp:TextBox ID="firstName" runat="server" CssClass="form-control" Placeholder="Insured's First Name" onkeypress="return characterAndNumbers(event)" MaxLength="75" Minlength="1" onkeydown="return /[A-Za-z0-9-' ]/.test(event.key)"></asp:TextBox>
                                </div>
                                <div class="form-group col-md-4">
                                    <label for="last-name">Insured's Last Name</label>
                                    <asp:TextBox ID="lastName" runat="server" CssClass="form-control" Placeholder="Insured's Last Name" onkeypress="return characterAndNumbers(event)" MaxLength="20" Minlength="1" onkeydown="return /[A-Za-z0-9-' ]/.test(event.key)"></asp:TextBox>
                                </div>
                                <p class="note">Note: Input Claims Reference Number provided by Cebuana Lhuillier Branch Personnel or CLIB Claims Division.</p>
                                <asp:Button ID="ValidateButton" runat="server" Text="Validate" CssClass="validate-btn" OnClick="ValidateButton_Click" />
                            </div>
                        </div>
                    </div>
                </div>
                <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <% if (Session["EnableClaims"] != null && (bool)Session["EnableClaims"] == true)
                            { %>
                        <!-- Accordion Item 2 -->
                        <div class="app-collapse" app-collapse>
                            <div class="collapse-header-button" aria-expanded="true" aria-controls="sect2" id="collapse2id" indipendente="false" app-collapse-header-btn>
                                <span class="collapse-title">
                                    <b>Claims Benefit and Requirement</b>
                                    <svg xmlns="http://www.w3.org/2000/svg" fill="#fff" viewBox="0 0 156 82" class="collapse-icon">
                                        <path class="icon-path-line" />
                                    </svg>
                                </span>
                            </div>
                            <div id="sect2" role="region" aria-labelledby="collapse2id" class="collapse-panel app-collapse-expanded">
                                <div class="collapse-panel-inner-wrapper content-text">
                                    <br />
                                    <div class="form-group natureofclaim">
                                        <label for="nature-of-claim">Nature Of Claim</label>
                                        <asp:DropDownList runat="server" class="form-control" onpaste="return false" ID="natureofclaimDropdownlist" OnSelectedIndexChanged="natureofclaimDropdownlist_SelectedIndexChanged" AutoPostBack="True"></asp:DropDownList>
                                    </div>

                                    <div class="table-container">
                                        <table>
                                            <thead id="tableHeader" runat="server" style="display: none;">
                                                <tr>
                                                    <th></th>
                                                    <th>Benefits</th>
                                                    <th>Coverage Amount</th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <asp:Repeater ID="rptBenefits" runat="server">
                                                    <ItemTemplate>
                                                        <tr>
                                                            <td>
                                                                <asp:CheckBox ID="chkBenefit" runat="server" AutoPostBack="true" OnCheckedChanged="chkBenefit_CheckedChanged" />
                                                            </td>
                                                            <td><%# Eval("Benefit") %></td>
                                                            <td><%# "PHP " + String.Format("{0:N2}", Eval("CoverageAmount")) %></td>
                                                            <td style="display: none;">
                                                                <asp:HiddenField ID="hiddenBenefitCode" runat="server" Value='<%# Eval("BenefitCode") %>' />
                                                            </td>
                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                            </tbody>
                                        </table>
                                        <asp:Panel ID="documentContainer" runat="server"></asp:Panel>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <!-- Accordion Item 3 -->
                        <div class="app-collapse" app-collapse>
                            <div class="collapse-header-button" aria-expanded="true" aria-controls="sect3" id="collapse3id" indipendente="false" app-collapse-header-btn>
                                <span class="collapse-title">
                                    <b>Data Privacy Agreement and Client Consent Declaration</b>
                                    <svg xmlns="http://www.w3.org/2000/svg" fill="#fff" viewBox="0 0 156 82" class="collapse-icon">
                                        <path class="icon-path-line" />
                                    </svg>
                                </span>
                            </div>
                            <div id="sect3" role="region" aria-labelledby="collapse3id" class="collapse-panel app-collapse-expanded">
                                <div class="collapse-panel-inner-wrapper content-text">
                                    <br />
                                    <div class="row">
                                        <div class="col-sm-10 offset-sm-1" align="center">
                                            <input type="checkbox" id="dataPrivacy1Checkbox" class="form-check-input ml-1" runat="server" />
                                            <label class="form-check-label ml-4" for="dataPrivacyCheckbox">I hereby acknowledge that I have read, understand, and agree to the <a href="https://www.cebuanalhuillier.com/privacypolicy/" target="_blank">Data Privacy Policy</a> of Cebuana Lhuillier. </label>
                                            <div id="main">
                                                <br />
                                                <asp:Image ID="captchaImage" runat="server" Style="width: 300px;" />
                                                <br />
                                                <asp:LinkButton ID="generateNewCaptcha" runat="server" OnClick="generateNewCaptcha_Click" Font-Size="Small" Font-Italic="true">Generate new code</asp:LinkButton>
                                                <br />
                                                <div class="col-md-4 mb-3">
                                                    <input type="text" class="form-control" id="captchaText" placeholder="Please answer Captcha Text" runat="server" style="text-align: center;" />
                                                </div>

                                                <%--<asp:Button ID="btn_Submit" AutoPostBack="true" runat="server" Text="Submit" CssClass="validate-btn" OnClick="btn_Submit_Click" />--%>
                                                <button type="button" id="btn_Submit" class="validate-btn">Submit</button>
                                                <%--<button type="button" id="btn_Submit" class="btn btn-primary">Submit</button>--%>
                                                <br />
                                                <br />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <%} %>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btnHiddenShow" EventName="Click" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </div>
    </form>
    <div class="modal fade" id="myModal" data-bs-backdrop="static" data-bs-keyboard="false" tabindex="-1" role="dialog" aria-labelledby="modalLabel" aria-hidden="true">
        <div class="modal-dialog modal-lg" role="document">
            <div class="modal-content">
                <div class="modal-body">
                    <span class="close" data-bs-dismiss="modal">X</span>
                    <div class="content">
                        <div class="zoom-buttons mb-4">
                            <div class="preview-container" id="previewContainer">
                                <h5>File Preview:</h5>
                                <img id="filePreview" alt="Image Preview" />
                                <iframe id="pdfPreview" width="100%" height="500px"></iframe>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <script>

        function showSpinner() {
            $(".overlay-page").show();
        }

        function hideSpinner() {
            $(".overlay-page").hide();
        }

        Sys.WebForms.PageRequestManager.getInstance().add_initializeRequest(function () {
            showSpinner();
        });

        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(function () {
            hideSpinner();
        });

        function showDocument(documentId) {
            let selectedFile = null;
            document.getElementById('<%= hiddenDocumentId.ClientID %>').value = documentId;
            var fileUploadControl = document.getElementById('file_upload_' + documentId);
            var file = fileUploadControl.files[0];
            var myModal = new bootstrap.Modal(document.getElementById('myModal'));

            if (file) {
                var fileType = file.type;
                if (fileType === 'application/pdf') {
                    pdfPreview.style.display = "block";
                    filePreview.style.display = "none";

                    const fileURL = URL.createObjectURL(file);
                    pdfPreview.src = fileURL;
                    myModal.show();

                    return;
                }
                else if (fileType === 'image/jpeg' || fileType === 'image/png' || fileType === 'image/gif') {
                    filePreview.style.display = "block";
                    pdfPreview.style.display = "none";

                    const reader = new FileReader();
                    reader.onload = function (e) {
                        filePreview.src = e.target.result;
                    };
                    reader.readAsDataURL(file);

                    myModal.show();

                    return;
                }
            }

            // Trigger the hidden button to perform a server-side postback
            document.getElementById('<%= btnHiddenShow.ClientID %>').click();
        }

        function DownloadDocument(documentId) {

            document.getElementById('<%= hiddenDocumentId.ClientID %>').value = documentId;
            document.getElementById('<%= btnDownloadDocument.ClientID %>').click();
            showSpinner();

            // Delay hiding the spinner to simulate loading

        }



        var btncheckeligibility = document.querySelector('.validate-btn');

        btncheckeligibility.addEventListener('click', function () {
            //$(this).html('<span class="loading-spinner"></span> Validating...').attr({disabled: true})
            Swal.fire({
                title: 'Please wait while we process your details...',
                showConfirmButton: false,
                allowOutsideClick: false,
                allowEscapeKey: false
            });
        });
    </script>
</asp:Content>
