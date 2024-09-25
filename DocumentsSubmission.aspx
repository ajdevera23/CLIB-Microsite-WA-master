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
                                    <td style="display:none;">
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
