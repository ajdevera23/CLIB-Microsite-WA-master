<%@ Page Title="" Language="C#" MasterPageFile="~/ClientReferral.master" AutoEventWireup="true" CodeFile="ClientReferral.aspx.cs" Inherits="ClientReferral" MaintainScrollPositionOnPostback="true" %>

<asp:Content ID="Header" ContentPlaceHolderID="HeaderContent" runat="Server">
        <link href="Style/clientReferral.css" rel="stylesheet" />
    <link href="JScript/Plugins/Referral/bootstrap.min.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="ClientReferralPageMain" ContentPlaceHolderID="BodyContent" runat="server">
<div class="main-container">
    <form id="adcForm" runat="server">

<%-- <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
 <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
 <ContentTemplate>--%>
 <div class="modal fade" id="selectionModal" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true" data-bs-backdrop="static" data-bs-keyboard="false">
 <div class="modal-dialog">
     <div class="modal-content">
         <div class="modal-body">
             <center>
                  <span class="h5 mb-5">Select Client Type</span>
             </center>
           <br />
    <div class="row g-2">
<div class="col-md-6">
    <div class="card text-center clickable" onclick="document.getElementById('<%= individualRbtn.ClientID %>').click()">
        <div class="image">
            <svg xmlns="http://www.w3.org/2000/svg" width="52" height="52" viewBox="0 0 512 512" fill="#DC4033">
                <path d="M332.64,64.58C313.18,43.57,286,32,256,32c-30.16,0-57.43,11.5-76.8,32.38-19.58,21.11-29.12,49.8-26.88,80.78C156.76,206.28,203.27,256,256,256s99.16-49.71,103.67-110.82C361.94,114.48,352.34,85.85,332.64,64.58Z"/>
                <path d="M432,480H80A31,31,0,0,1,55.8,468.87c-6.5-7.77-9.12-18.38-7.18-29.11C57.06,392.94,83.4,353.61,124.8,326c36.78-24.51,83.37-38,131.2-38s94.42,13.5,131.2,38c41.4,27.6,67.74,66.93,76.18,113.75,1.94,10.73-.68,21.34-7.18,29.11A31,31,0,0,1,432,480Z"/>
            </svg>
            <br />
            <asp:RadioButton ID="individualRbtn" runat="server" Text="&nbsp; INDIVIDUAL" GroupName="client" OnCheckedChanged="RadioButton_CheckedChanged" AutoPostBack="True" data-bs-dismiss="modal" CssClass="hidden-radio" />
        </div>
        <span class="fw-bold">INDIVIDUAL</span>
    </div>
</div>
<div class="col-md-6">
    <div class="card text-center clickable" onclick="document.getElementById('<%= groupRbtn.ClientID %>').click()">
        <div class="image">
            <svg xmlns="http://www.w3.org/2000/svg" width="52" height="52" viewBox="0 0 16 16" fill="#DC4033">
                <g>
                    <g>
                        <circle cx="6" cy="6" r="3"/>
                        <path d="M11.825,12.121a9.049,9.049,0,0,0-11.651,0A.491.491,0,0,0,0,12.5v1.381A1.032,1.032,0,0,0,1,15H11a1,1,0,0,0,1-1V12.643A.61.61,0,0,0,11.825,12.121Z"/>
                    </g>
                    <g>
                        <path d="M10,6a3.966,3.966,0,0,1-.125.987c.042,0,.082.013.125.013A3,3,0,1,0,7.528,2.306,4,4,0,0,1,10,6Z"/>
                        <path d="M15.825,10.121A9.037,9.037,0,0,0,9.44,8.02,4.048,4.048,0,0,1,8.292,9.269a10.036,10.036,0,0,1,4.213,2.116A1.457,1.457,0,0,1,13,12.5V13h2a1,1,0,0,0,1-1V10.5A.5.5,0,0,0,15.825,10.121Z"/>
                    </g>
                </g>
            </svg>
            <br />
            <asp:RadioButton ID="groupRbtn" runat="server" Text="&nbsp; GROUP" GroupName="client" OnCheckedChanged="RadioButton_CheckedChanged" AutoPostBack="True" data-bs-dismiss="modal" CssClass="hidden-radio" />
        </div>
        <span class="fw-bold">GROUP</span>
    </div>
</div>

    </div>
       </div>
     </div>
    </div>
 </div>
 <div class="section section-background bg-theme-color-light overlay-dark overlay-opacity-8 bg-cover lazy" data-loaded="true">
<div class="container">

<div class="row text-center-md text-center-xs justify-content-start">
<div class="col-12 col-lg-6 mb-5 text-white aos-init aos-animate" data-aos="fade-in" data-aos-delay="0" data-aos-offset="0">

<h1 class="display-4 fw-bold mb-0" >
<span class="d-inline-block fw-normal">
<span class="h5 fw-normal">
 <img src="Images/clib-logo.png" style="height: 70px;"/>
</span>
<span class="h6">
    <br>
    Hi Ka Cebuana!
</span>
<br>
 Welcome to the <span class="text-danger"><br>Client Referral Module</span>
</span>
</h1>

    <!-- <p class="h3 fw-normal mb-0">
    Your home, one click away
    </p> -->
</div>
  
<div class="col-12 col-lg-6 text-align-end text-center-md text-center-xs aos-init aos-animate" data-aos="fade-in" data-aos-delay="50" data-aos-offset="0">
<div class="d-inline-block bg-white shadow-primary-xs rounded p-4 p-md-5 w-100 max-w-450 text-align-start mt-3">

<div class="row gutters-xs">
<div class="col-12 col-lg-6">
<div class="form-floating mb-3">
    <input runat="server" placeholder="Referral Transaction Number:" id="rtnLbl" type="text" value="" class="form-control" disabled="disabled" style="background-color:#242147; color:#FFF; font-weight:600;"/>
    <label for="s_address">Referral Transaction Number:</label>
</div>
</div>
 <div class="col-12 col-lg-6">
 <div class="form-floating mb-3">
 <input runat="server" placeholder="Client Type" id="txt_clienttype" type="text" value="" class="form-control" disabled="disabled" style="background-color:#242147; color:#FFF; font-weight:600;"/>
 <label for="s_address">Client Type:</label>
</div>
</div>
</div>
<% if (Session["ReferralPartnerCode"].ToString() == "CLH") { %> 
<div class="row gutters-xs">
    <div class="col-12 col-lg-6">
    <div class="form-floating mb-3">
        <input runat="server"  placeholder="Region" id="regionLbl" type="text" value="" class="form-control" disabled="disabled"/>
        <label for="s_address">Region:</label>
    </div>
    </div>
    <div class="col-12 col-lg-6">
        <div class="form-floating mb-3">
            <input runat="server" placeholder="Area" id="areaCodeLbl" type="text" value="" class="form-control" disabled="disabled"/>
            <label for="s_address">Area Code:</label>
        </div>
    </div>
</div>
<div class="row gutters-xs">
    <div class="col-12 col-lg-6">
    <div class="form-floating mb-3">
        <input runat="server"  placeholder="Branch Code" id="branchCodeLbl" type="text" value="" class="form-control" disabled="disabled"/>
        <label for="s_address">Branch Code:</label>
    </div>
    </div>
    <div class="col-12 col-lg-6">
        <div class="form-floating mb-3">
            <input  runat="server"   placeholder="Branch Name" id="branchNameLbl" type="text" value="" class="form-control" disabled="disabled"/>
            <label for="s_address">Branch Name:</label>
        </div>
    </div>
</div>
 <% } %> 
<% if (Session["ReferralPartnerCode"].ToString() == "AFL") { %> 
<div class="row gutters-xs">
    <div class="col-12 col-lg-12">
    <div class="form-floating mb-3">
        <input runat="server"  placeholder="Affiliate Code" id="fld_AffiliateCode" type="text" value="" class="form-control" disabled="disabled"/>
        <label for="s_address">Affiliate Code:</label>
    </div>
    </div>
    <div class="col-12 col-lg-12">
        <div class="form-floating mb-3">
            <input runat="server" placeholder="Affiliate Name" id="fld_AffiliateName" type="text" value="" class="form-control" disabled="disabled"/>
            <label for="s_address">Affiliate Name:</label>
        </div>
    </div>
</div>
 <% } %> 
<asp:Panel ID="groupPnl" runat="server">
<div class="form-floating mb-3">
<input type="text" id="fld_GroupName" class="form-control" onpaste="return false" onkeypress="return characterAndNumbers(event)" runat="server" maxlength="100" minlength="1" required="required" />
<label for="s_address"> Group / Business Name:</label>
</div>
<div class="form-floating mb-3">
<input type="text" id="ContactPerson" class="form-control" onpaste="return false" onkeypress="return characterAndNumbers(event)" runat="server" maxlength="100" minlength="1" required="required" />
<label for="s_address">  Contact Person Designation:</label>
</div>

</asp:Panel>
<asp:Panel ID="individualPnl" runat="server">
<div class="form-floating mb-3">

     <input type="text" placeholder="Last Name" id="LastName"  class="form-control" onpaste="return false" runat="server"  maxlength="20" minlength="1" onkeypress="return characterAndNumbers(event)" onkeydown="return /[A-Za-z0-9-' ]/.test(event.key)" onchange="javascript:ValidateLastName();" title="Last Name" required="required"/>
<label for="s_address">Last Name</label>
</div>
<div class="form-floating mb-3">
   <input type="text" placeholder="First Name" id="FirstName" class="form-control" onpaste="return false" runat="server"  maxlength="75" minlength="1" onkeypress="return characterAndNumbers(event)" onkeydown="return /[A-Za-z0-9-' ]/.test(event.key)" onchange="javascript:ValidateFirstName();" title="First Name" required="required" />
  <label for="s_address">First Name</label>
</div>
</asp:Panel>
<div class="form-floating mb-3">
  <input id="birthDateTextBox" placeholder="Date Of Birth"  runat="server" class="form-control"   min="1753-01-01" max="9999-12-31"/>
    <label for="s_address">Date Of Birth</label>
</div>
<asp:Button ID="btnValidate" class="btn w-100 btn-lg btn-danger bg-gradient-danger" runat="server" Text="Validate" OnClick="btnValidate_Click"  Enabled="true" />
<asp:Panel ID="otherDetailsPnl" runat="server"> 
<div class="form-floating mb-3">
 <input type="text"  placeholder="Email Address" runat="server" class="form-control" id="emailAddress" onkeypress="return emailCheck(event)" maxlength="50" minlength="5" onchange="javascript:ValidateEmail();"/>
    <label for="s_address"> <span class="text-required">* Email Address:</span></label>
</div>
<div class="form-floating mb-3">
    <input type="text" placeholder="* Cellphone Number:" runat="server" class="form-control" id="cellphoneNo" onpaste="return false" value='09' data-initial='09' maxlength='11' minlength="11" pattern="[0-9]*" title="Contact Number" oninput="javascript: if (this.value.length > this.maxLength) this.value = this.value.slice(0, this.maxLength);" onkeypress="return validNumeric()" onchange="javascript:validateContactNumber();"/>
    <label for="s_address"> <span class="text-required">* Cellphone Number:</span></label>
</div>
<div class="form-floating mb-3">
     <input type="text" placeholder="* Address Building / Street Name:" id="fld_Address" class="form-control" onpaste="return false" runat="server" onkeypress="return characterAndNumbers(event)" maxlength="120" minlength="10"/>
    <label for="s_address"> <span class="text-required">* Address Building / Street Name:</span></label>
</div>
<div class="form-floating mb-3">
    <asp:DropDownList ID="DDProvince" CssClass="form-control selectpicker" runat="server" OnSelectedIndexChanged="DDProvince_SelectedIndexChanged" AutoPostBack="True"></asp:DropDownList>
    <label for="DDProvince">* Province:</label>
</div>
<div class="form-floating mb-3">
    <asp:DropDownList ID="DDcity" CssClass="form-control selectpicker" runat="server"></asp:DropDownList>
    <label for="DDcity"><span class="text-required">* City:</span></label>
</div>
<div class="form-floating mb-3">
     <input type="text" placeholder="* Zip Code:"  id="ZipCode" class="form-control" onpaste="return false" runat="server" onkeypress="return characterAndNumbers(event)" maxlength="4" />
    <label for="s_address"> <span class="text-required">* Zip Code:</span></label>
</div>

<div class="form-floating mb-3">
 <input type="text" placeholder="* Interested with following products:"  id="fld_Interests" class="form-control" maxlength="250" minlength="1" runat="server" />
    <label for="s_address"> <span class="text-required">* Interested with following products:</span></label>
</div>

<div class="form-floating mb-3">
       <input type="text" placeholder="* Preferred appointment time and Notes:"  id="fld_Appointments" class="form-control" maxlength="250" minlength="1" runat="server" />
    <label for="s_address"> <span class="text-required">* Preferred appointment time and Notes:</span></label>
</div>

<%--<div class="mb-3"  id="photoTR" runat="server">
      <asp:FileUpload ID="PhotoUpload" runat="server" />
            <asp:Label ID="lblImageName" runat="server"></asp:Label>
    <label for="s_address"> <span class="text-required">* Photo:</span></label>
</div>--%>

<%--    <div class="mb-3">
        <table>
                    <tr id="photoTR" runat="server">
                        <td style="width: 300px;">
                            <span class="text-required">*</span>Photo:
                        <br />
                        </td>
                        <td style="width: 300px;">
                            <asp:FileUpload ID="PhotoUpload" runat="server"  AutoPostBack="True"/>
                            <asp:Label ID="lblImageName" runat="server"></asp:Label>
                        </td>
                    </tr>
        </table>
    </div>
    <br /><br />--%>
<asp:Panel ID="fileUploadPanel" runat="server">
    <div class="mb-3">
        <table style="width:100%;">
            <tr id="photoTR" runat="server">
                <td>
                    <div class="file-upload" id="fileUploadDiv">
                        <span id="fileLabelText" runat="server">* Choose a file JPEG, JPG and up tp 3MB</span>
                        <asp:FileUpload ID="PhotoUpload" runat="server" OnChange="updateFileName()" accept=".jpg,.jpeg" />
                    </div>
                    <asp:Label ID="lblImageName" runat="server"></asp:Label>
                                     <!-- Hidden field to store the file name -->
                    <asp:HiddenField ID="HiddenFileName" runat="server" />
                </td>
            </tr>
        </table>
    </div>
    <br /><br />
</asp:Panel>

</asp:Panel>
        <div id="dpaView" align="center">
        <asp:Panel ID="dpaPnl" runat="server"  align="center" >
                    <div id="dataPrivacyFormHead" class="tab-pane show active fade" align="left" style="margin-left: 5%;">
                        <h6 style="color: black;" align="center">Data Privacy Agreement & Client Consent Declaration </h6>
                    </div>
                    <div id="dataPrivacyFormBody" class="tab-pane show active fade">
                        <div>
                            <div>
                                <div align="center" style="margin-left: 6%">
                                 
                                    <%--<input type="checkbox" id="dataPrivacyCheckbox" class="form-check-input ml-1" runat="server"  />--%>
                                    <label class="form-check-label ml-4" for="dataPrivacyCheckbox" style="font-size:12px;">   <asp:CheckBox ID="dataPrivacyCheckbox" CssClass="form-check-input ml-1" runat="server" OnCheckedChanged="dataPrivacyCheckbox_CheckedChanged" /> I hereby acknowledge that I have read, understand, and agree to the <a href="DataPrivacy.aspx" target="_blank">Data Privacy Policy</a> of Cebuana Lhuillier.</label><br />
                                </div>
                            </div>

                        </div>
                    </div>
                    &nbsp
                </asp:Panel>
            <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" class="btn w-100 btn-lg btn-danger bg-gradient-danger" Width="300" Height="50" />
        </div>
    <br />
<footer class="pb-1">
      <p style="text-align:center; color:#808080; font-weight:400;">© 2021 - Cebuana Lhuillier</p>  
</footer>

</div>
</div>
</div>
</div>
</div>
<%-- </ContentTemplate>

           <Triggers>
            <asp:PostBackTrigger ControlID="btnSave" />
        </Triggers>
</asp:UpdatePanel>--%>

  </form>
</div>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ScriptContent" runat="Server">
    <script src="JScript/Plugins/Referral/bootstrap.bundle.min.js"></script>
    <script>


        $(document).ready(function () {
            //--SHOW MODAL----///
            var clientType = document.getElementById("<%=txt_clienttype.ClientID%>").value
            if (!clientType) {
                $('#selectionModal').modal('show');
            }
            //-------------------------------- KEY UP FOR CONTACT NUMBER 09 --------------------///
            $("#<%=cellphoneNo.ClientID%>").on("keyup", function () {
                var value = $(this).val();
                $(this).val($(this).data("initial") + value.substring(2));
            });
        });

        function updateFileName() {
            var fileInput = document.getElementById('<%= PhotoUpload.ClientID %>');
            var fileLabelText = document.getElementById('<%= fileLabelText.ClientID %>');
            var hiddenFileNameField = document.getElementById('<%= HiddenFileName.ClientID %>');

            if (fileInput.files.length > 0) {
                var fileName = fileInput.files[0].name;
                fileLabelText.textContent = fileName;
                hiddenFileNameField.value = fileName; // Store the file name in the hidden field
            } else {
                fileLabelText.textContent = "* Choose a file JPEG, JPG and up to 3MB";
                hiddenFileNameField.value = ""; // Clear the hidden field if no file is selected
            }
        }

        function text_changed(txtObj) {

            var birthDate = new Date(txtObj.value);
            var birthDateYear = birthDate.getFullYear();
            var dateNow = new Date();
            var dateNowYear = dateNow.getFullYear();
            var age = dateNowYear - birthDateYear;
            var elem = document.getElementById("infoForm_guardianDiv");

            if (birthDateYear.value < 1753 || birthDate.value > 9999) {
                alert("Please input correct year");
                return true;   
            }
            if (age < 18) {
                elem.style.display = "block";
            }
            else {
                elem.style.display = "none";
                document.getElementById("infoForm_guardianBirthDate").value = "";

            }
        }
        function character(e) {
            isIE = document.all ? 1 : 0
            keyEntry = !isIE ? e.which : event.keyCode;
            if (((keyEntry >= '65') && (keyEntry <= '90')) || ((keyEntry >= '97') && (keyEntry <= '122')) || (keyEntry == '46') || (keyEntry == '32') || keyEntry == '45' || keyEntry == '188' || (keyEntry == '191'))
                return true;
            else {
                return false;
            }
        }

        function characterAndNumbers(e) {
            var keyEntry = e.which || e.keyCode;

            // Allow numbers (48-57), uppercase letters (65-90), lowercase letters (97-122), period (46), space (32), hyphen (45), comma (188), and single quote (39)
            if (
                (keyEntry >= 48 && keyEntry <= 57) ||
                (keyEntry >= 65 && keyEntry <= 90) ||
                (keyEntry >= 97 && keyEntry <= 122) ||
                keyEntry == 46 ||
                keyEntry == 32 ||
                keyEntry == 45 ||
                keyEntry == 188 ||
                keyEntry == 39
            ) {
                return true;
            } else {
                return false;
            }
        }
        function emailCheck(e) {
            isIE = document.all ? 1 : 0;
            keyEntry = !isIE ? e.which : event.keyCode;
            if (
                (keyEntry >= "48" && keyEntry <= "57") ||
                (keyEntry >= "64" && keyEntry <= "90") ||
                (keyEntry >= "97" && keyEntry <= "122") ||
                keyEntry == "46" ||
                keyEntry == "32" ||
                keyEntry == "45" ||
                keyEntry == "186" ||
                keyEntry == "189" ||
                keyEntry == "95"
            ) {
                return true;
            } else {
                return false;
            }
        }
        //-------------------------------- VALID EAMIL ADDRESS --------------------///
        function ValidateEmail() {
            var mailtextbox = document.getElementById("<%=emailAddress.ClientID%>").value;
            var mailformat = /^(([^<>()[\]\\.,;:\s@"]+(\.[^<>()[\]\\.,;:\s@"]+)*)|.(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
            if (mailtextbox.match(mailformat)) {
                console.log("VALID EMAIL ADDRESS");
                return true;
            }
            else
            {
                Swal.fire('You have entered an invalid email address. Please try again');
                document.getElementById("<%=emailAddress.ClientID%>").value = "";
                return false;
            }
        }
        //-------------------------------- VALIDATE CONTACT NUMBER --------------------///
        function validateContactNumber() {
            var phone = document.getElementById("<%=cellphoneNo.ClientID%>").value;
    var re = /^(09|\+639)\d{11}$/;
    var contanctnumberlenght = phone.replace(/[^0-9]/g, "").length;
    if (phone.match(re)) {
        console.log("VALID ID NUMBER");
        return true;
    }
    if (contanctnumberlenght < 11)
    {
       Swal.fire('Please enter a valid PH Number');
        document.getElementById("<%=cellphoneNo.ClientID%>").value = "09";
        return true;
    }
    else {
        console.log("VALID PH NUMBER");
    }

        }
        //-------------------------------- VALID NUMERIC --------------------///
        function validNumeric() {
            var charCode = (event.which) ? event.which : event.keyCode;
            if (charCode >= 48 && charCode <= 57) {
                return true;
            } else {
                return false;
            }
        }
       //-------------------------------- VALID FIRST NAME --------------------///
        function ValidateFirstName() {

            var nametextbox = document.getElementById("<%=FirstName.ClientID%>").value;
            var nameformat = /^[A-Za-z0-9-' ]+$/;

            if (nametextbox.match(nameformat)) {
                console.log("VALID FIRST NAME");
                return true;
            }
            else
            {
                Swal.fire('You have entered invalid information. Please try again.');
                        document.getElementById("<%=FirstName.ClientID%>").value = "";
                        return false;
                    }
        }
       //-------------------------------- VALID LAST NAME --------------------///
        function ValidateLastName() {

            var nametextbox = document.getElementById("<%=LastName.ClientID%>").value;
            var nameformat = /^[A-Za-z0-9-' ]+$/;
            
            if (nametextbox.match(nameformat)) {
                console.log("VALID LAST NAME");
                return true;
            }
            else
            {
               Swal.fire('You have entered invalid information. Please try again.');
                document.getElementById("<%=LastName.ClientID%>").value = "";
                return false;
            }
        }

        document.addEventListener('DOMContentLoaded', function () {
            document.querySelectorAll('.clickable').forEach(function (card) {
                card.addEventListener('click', function () {
                    let radioBtn = card.querySelector('input[type="radio"]');
                    radioBtn.checked = true;

                    // Remove active class from all cards
                    document.querySelectorAll('.clickable').forEach(function (card) {
                        card.classList.remove('active');
                    });

                    // Add active class to clicked card
                    card.classList.add('active');
                });
            });
        });



    </script>


</asp:Content>

