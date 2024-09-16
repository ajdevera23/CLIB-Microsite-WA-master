<%@ Page Title="" Language="C#" MasterPageFile="~/Claims.master" AutoEventWireup="true" CodeFile="DocumentsSubmission.aspx.cs" Inherits="ClientReferral" MaintainScrollPositionOnPostback="true" %> <asp:Content ContentPlaceHolderID="infoForm" runat="server">
  <link href="Style/ClaimsAI/documentsubmission.css" rel="stylesheet" />
  <form id="enrollmentForm" class="container body-content container-enrollment" method="post" autocomplete="off" runat="server" novalidate>
    <div class="container">
      <div class="form-header">
        <h3 style="padding:3px;">Claims Information</h3>
         <svg xmlns="http://www.w3.org/2000/svg" width="44" height="44" viewBox="0 0 24 24" style="fill: #fff;transform: ;msFilter:;"><path d="M16.293 9.293 12 13.586 7.707 9.293l-1.414 1.414L12 16.414l5.707-5.707z"></path></svg>
      </div>
      <form action="#" class="claims-form">
        <div class="row col-md-12 mt-4">
          <div class="form-group col-md-4">
            <label for="reference-number">Claims Reference Number</label>
            <input type="text" id="reference-number" placeholder="Claims Reference Number">
          </div>
          <div class="form-group col-md-4">
            <label for="first-name">Insured's First Name</label>
            <input type="text" id="first-name" placeholder="Insured's First Name">
          </div>
          <div class="form-group col-md-4">
            <label for="last-name">Insured's Last Name</label>
            <input type="text" id="last-name" placeholder="Insured's Last Name">
          </div>
        </div>
        <p class="note">Note: Input Claims Reference Number provided by Cebuana Lhuillier Branch Personnel or CLIB Claims Division.</p>
        <button type="submit" class="validate-btn">Validate</button>

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
      </form>
    </div>
  </form>
</asp:Content>