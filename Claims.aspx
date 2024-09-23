<%@ Page Title="" Language="C#" MasterPageFile="~/Claims.master" AutoEventWireup="true" CodeFile="Claims.aspx.cs" Inherits="ClientReferral" MaintainScrollPositionOnPostback="true" %>

<asp:Content ContentPlaceHolderID="infoForm" runat="server">
    <link href="Style/ClaimsAI/claims.css" rel="stylesheet" />
    <form id="enrollmentForm" class="container body-content container-enrollment" method="post" autocomplete="off" runat="server" novalidate>
        <div class="container my-2">
            <div class="text-center mt-2">
                <h1>Welcome to Cebuana Lhuillier Insurance Brokers Online</h1>
                <p class="lead">Self-Service Claims Management System</p>
                <p>Please select your service:</p>
                <br />
                <div class="row justify-content-center">
                    
                <asp:HiddenField ID="hiddenDocSubmissionUrl" runat="server" />
                <div class="card" onclick="redirectToDocumentsSubmission()">
                    <div class="card-content">
                        <div class="card-bottom">
                            <p style="margin-top:200px;">Claims Document Submission</p>
                            <svg xmlns="http://www.w3.org/2000/svg" width="42" height="42" viewBox="0 0 24 24" style="fill: rgba(0, 0, 0, 1);transform: ;msFilter:;">
                                <path d="M10.707 17.707 16.414 12l-5.707-5.707-1.414 1.414L13.586 12l-4.293 4.293z"></path>
                            </svg>
                        </div>
                    </div>
                    <div class="card-image">
                        <img src="Images/Claims/documentsub-icon.png" style="margin-top:-30px; width:150px;" />
                    </div>
                </div>
                </div>
            </div>
        </div>
    </form>
<script type="text/javascript">
    function redirectToDocumentsSubmission() {
        // Get the URL from the hidden field
        var docSubmissionUrl = document.getElementById('<%= hiddenDocSubmissionUrl.ClientID %>').value;
        window.location.href = docSubmissionUrl;
    }
</script>
</asp:Content>
