<%@ Page Title="" Language="C#" MasterPageFile="~/Claims.master" AutoEventWireup="true" CodeFile="DocumentsSubmission.aspx.cs" Inherits="ClientReferral" MaintainScrollPositionOnPostback="true" %> <asp:Content ContentPlaceHolderID="infoForm" runat="server">
    <link href="Style/ClaimsAI/documentsubmission.css" rel="stylesheet" />
  <form id="enrollmentForm" class="container body-content container-enrollment" method="post" autocomplete="off" runat="server" novalidate>
    <div class="container">
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
            <asp:TextBox ID="firstName" runat="server" CssClass="form-control" Placeholder="Insured's First Name" onkeypress="return characterAndNumbers(event)" Maxlength="75" Minlength="1" onkeydown="return /[A-Za-z0-9-' ]/.test(event.key)"></asp:TextBox>
          </div>
          <div class="form-group col-md-4">
            <label for="last-name">Insured's Last Name</label>
            <asp:TextBox ID="lastName" runat="server" CssClass="form-control" Placeholder="Insured's Last Name" onkeypress="return characterAndNumbers(event)" Maxlength="20" Minlength="1" onkeydown="return /[A-Za-z0-9-' ]/.test(event.key)"></asp:TextBox>
          </div>
          <p class="note">Note: Input Claims Reference Number provided by Cebuana Lhuillier Branch Personnel or CLIB Claims Division.</p>
          <asp:Button ID="ValidateButton" runat="server" Text="Validate" CssClass="validate-btn" OnClick="ValidateButton_Click" />
        </div>
      </div>
    </div>
  </div>
<% if (Session["EnableClaims"] != null && (bool)Session["EnableClaims"] == true) { %>
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
             <asp:DropDownList runat="server" class="form-control" onpaste="return false" ID="natureofclaimDropdownlist" OnSelectedIndexChanged="natureofclaimDropdownlist_SelectedIndexChanged"  AutoPostBack="True"></asp:DropDownList>
             </div>
   
            <div class="table-container">
                <table>
                    <thead id="tableHeader" runat="server" style="display:none;">
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
                                    <td>
                                        <asp:HiddenField ID="hiddenBenefitCode" runat="server" Value='<%# Eval("BenefitCode") %>' />
                                    </td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </tbody>
                </table>
                <asp:Panel ID="documentContainer" runat="server"></asp:Panel>
            </div>

        <div class="uploader-container">
          <div class="button-container">
            <button class="button hover-blue">
              <svg class="icon" xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24" style="fill: #60a5fa;transform: ;msFilter:;">
                <path d="M13.707 2.293A.996.996 0 0 0 13 2H6c-1.103 0-2 .897-2 2v16c0 1.103.897 2 2 2h12c1.103 0 2-.897 2-2V9a.996.996 0 0 0-.293-.707l-6-6zM6 4h6.586L18 9.414l.002 9.174-2.568-2.568c.35-.595.566-1.281.566-2.02 0-2.206-1.794-4-4-4s-4 1.794-4 4 1.794 4 4 4c.739 0 1.425-.216 2.02-.566L16.586 20H6V4zm6 12c-1.103 0-2-.897-2-2s.897-2 2-2 2 .897 2 2-.897 2-2 2z"></path>
              </svg> Open File: 
            </button>
            <button class="button hover-cyan">
              <svg class="icon" xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24" style="fill: #06b6d4;transform: ;msFilter:;">
                <path d="M13 19v-4h3l-4-5-4 5h3v4z"></path>
                <path d="M7 19h2v-2H7c-1.654 0-3-1.346-3-3 0-1.404 1.199-2.756 2.673-3.015l.581-.102.192-.558C8.149 8.274 9.895 7 12 7c2.757 0 5 2.243 5 5v1h1c1.103 0 2 .897 2 2s-.897 2-2 2h-3v2h3c2.206 0 4-1.794 4-4a4.01 4.01 0 0 0-3.056-3.888C18.507 7.67 15.56 5 12 5 9.244 5 6.85 6.611 5.757 9.15 3.609 9.792 2 11.82 2 14c0 2.757 2.243 5 5 5z"></path>
              </svg> Upload </button>
            <button class="button hover-yellow">
              <svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24" style="fill: #facc14;transform: ;msFilter:;">
                <path d="M18.948 11.112C18.511 7.67 15.563 5 12.004 5c-2.756 0-5.15 1.611-6.243 4.15-2.148.642-3.757 2.67-3.757 4.85 0 2.757 2.243 5 5 5h1v-2h-1c-1.654 0-3-1.346-3-3 0-1.404 1.199-2.757 2.673-3.016l.581-.102.192-.558C8.153 8.273 9.898 7 12.004 7c2.757 0 5 2.243 5 5v1h1c1.103 0 2 .897 2 2s-.897 2-2 2h-2v2h2c2.206 0 4-1.794 4-4a4.008 4.008 0 0 0-3.056-3.888z"></path>
                <path d="M13.004 14v-4h-2v4h-3l4 5 4-5z"></path>
              </svg> Download </button>
            <button class="button hover-orange">
              <svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24" style="fill: #fb923c;transform: ;msFilter:;">
                <path d="M12 9a3.02 3.02 0 0 0-3 3c0 1.642 1.358 3 3 3 1.641 0 3-1.358 3-3 0-1.641-1.359-3-3-3z"></path>
                <path d="M12 5c-7.633 0-9.927 6.617-9.948 6.684L1.946 12l.105.316C2.073 12.383 4.367 19 12 19s9.927-6.617 9.948-6.684l.106-.316-.105-.316C21.927 11.617 19.633 5 12 5zm0 12c-5.351 0-7.424-3.846-7.926-5C4.578 10.842 6.652 7 12 7c5.351 0 7.424 3.846 7.926 5-.504 1.158-2.578 5-7.926 5z"></path>
              </svg> Show 
            </button>
          </div>
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
                  <asp:Image ID="captchaImage" runat="server" Style="width:300px;"/>
                  <br />
                  <asp:LinkButton ID="generateNewCaptcha" runat="server" onclick="generateNewCaptcha_Click" Font-Size="Small" Font-Italic="true">Generate new code</asp:LinkButton>
                  <br />
                  <div class="col-md-4 mb-3">
                    <input type="text" class="form-control" id="captchaText" placeholder="Please answer Captcha Text" runat="server" style="text-align:center;" />
                  </div>

                 <asp:Button ID="btn_Submit" runat="server"  Text="Submit" CssClass="validate-btn"/>
                    <br /><br />
                </div>
            </div>
          </div>
      </div>
    </div>
  </div>
<%} %>
</div>
    </div>
  </form>
<script src="JScript/ClaimsAI/documentsubmission.js"></script>
</asp:Content>
