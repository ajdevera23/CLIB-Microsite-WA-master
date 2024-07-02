﻿<%@ Page Language="C#" MasterPageFile="~/Enrollment.Master" AutoEventWireup="true" CodeFile="Enrollment.aspx.cs" Inherits="Enrollment" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link type="text/css" href="../Style/enrollment.css" rel="stylesheet" />
     <link href="Style/paymentmethod.css" rel="stylesheet" />
    <link href="JScript/Plugins/Datepicker/datetimepicker_ajax_libs_jqueryui_1.11.2_jquery-ui.css" rel="stylesheet" />
</asp:Content>
<asp:Content ContentPlaceHolderID="productLogo" runat="server">
    <%--  <img src="../Images/ProductLogoSample.png" runat="server" id="productImage" />--%>
 <div id="productImage" runat="server" class="image-container-through-div"></div>
</asp:Content>
<asp:Content ContentPlaceHolderID="partnerLogo" runat="server">
    <%--  <img src="../Images/ProductLogoSample.png" runat="server" id="partnerImage" />--%>
<div id="partnerImage" runat="server" class="image-container-through-div"></div>
</asp:Content>

<asp:Content ContentPlaceHolderID="infoForm" runat="server">
    <a href="javascript:" id="return-to-top"><i class="icon-svg" aria-hidden="true">
  <!-- Replace the following SVG code with your desired chevron-up SVG -->
  <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24" width="24" height="24">
    <path fill="currentColor" d="M12 8.293 6.354 14.354a.5.5 0 0 1-.708-.708l6-6a.5.5 0 0 1 .708 0l6 6a.5.5 0 0 1-.708.708L12 8.293z" />
  </svg>
</i></a>
  <form id="enrollmentForm" class="body-content container-enrollment" autocomplete="off" runat="server">
    <div class="accordion">
      <div class="accordion-item">
        <button class="accordion-header active" id="basicinformation" type="button">
          <strong>
            <svg xmlns="http://www.w3.org/2000/svg" width="35" height="35" style="position:absolute;">
              <rect id="Rectangle_73" data-name="Rectangle 73" width="35" height="35" rx="9" fill="#f7a424" opacity="0.1" />
              <path id="Icon_ionic-md-person" data-name="Icon ionic-md-person" d="M14.7,14.7A5.1,5.1,0,1,0,9.6,9.6,5.113,5.113,0,0,0,14.7,14.7Zm0,2.549c-3.378,0-10.2,1.721-10.2,5.1v2.549H24.894V22.345C24.894,18.967,18.075,17.246,14.7,17.246Z" transform="translate(2.803 2.803)" fill="#f7a424" />
            </svg>
            <b style="margin-left:50px;">Insured Information</b>
          </strong>
          <i class="fas fa-angle-right"></i>
        </button>
        <div class="accordion-body active">
          <div class="row col-md-12">
            <div class="col-md-3 mb-3">
              <span class="text-required">*</span> First Name: <br />
              <input type="text" id="firstName" class="form-control" onpaste="return false" placeholder="First Name" runat="server" onkeypress="return characterAndNumbers(event)" Maxlength="75" Minlength="1" onkeydown="return /[A-Za-z0-9-' ]/.test(event.key)" readonly="readonly" />
            </div>
            <div class="col-md-3 mb-3"> Middle Name: <br />
              <input type="text" id="middleName" class="form-control" onpaste="return false" placeholder="Middle Name" runat="server" onkeypress="return characterAndNumbers(event)" Maxlength="20" Minlength="1" onkeydown="return /[A-Za-z0-9-' ]/.test(event.key)" readonly="readonly" />
            </div>
            <div class="col-md-3 mb-3">
              <span class="text-required">*</span>Last Name: <br />
              <input type="text" id="lastName" class="form-control" onpaste="return false" placeholder="Last Name" runat="server" onkeypress="return characterAndNumbers(event)" Maxlength="20" Minlength="1" onkeydown="return /[A-Za-z0-9-' ]/.test(event.key)" readonly="readonly" />
            </div>
            <div class="col-md-3 mb-3"> Suffix: <br />
              <input type="text" id="suffix" class="form-control" onpaste="return false" placeholder="Suffix" runat="server" Maxlength="5" Minlength="1" onkeypress="return validateSuffix(event)" readonly="readonly" />
            </div>
          </div>
          <div class="row col-md-12">
            <div class="col-md-4 mb-3">
              <span class="text-required">*</span>Date of Birth: <br />
              <asp:TextBox runat="server" class="form-control" onkeypress="return text_changed(this);" onchange="this.onkeypress();" oninput="this.onkeypress();" onpaste="return false" type="date" ID="birthDateTextBox" ReadOnly="true"></asp:TextBox>
            </div>
            <div class="col-md-6">
              <br />
              <input type="button" ID="btnCheckEligibility" value="CHECK ELIGIBILITY" style="width:100%; top:100px; border:3px solid #000000; font-weight:900; background-color:#A6A6A6 !important" class="btn btn-info" disabled />
            </div>
          </div>
        </div>
      </div>
 <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
 <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
 <ContentTemplate>
      <div class="accordion-item">
        <button class="accordion-header active" type="button">
          <strong>
            <svg xmlns="http://www.w3.org/2000/svg" width="35" height="35" style="position:absolute;">
              <rect id="Rectangle_73" data-name="Rectangle 73" width="35" height="35" rx="9" fill="#11cdef" opacity="0.1" />
              <g id="Icon_ionic-ios-home" data-name="Icon ionic-ios-home" transform="translate(3.933 3.928)">
                <path id="Path_29" data-name="Path 29" d="M13.672,7.3a.158.158,0,0,0-.213,0L6.814,13.005a.2.2,0,0,0-.064.142v10.52a.089.089,0,0,0,.085.091h4.6a.089.089,0,0,0,.085-.091v-6.4a.089.089,0,0,1,.085-.091h3.919a.089.089,0,0,1,.085.091v6.4a.089.089,0,0,0,.085.091h4.6a.089.089,0,0,0,.085-.091V13.147a.19.19,0,0,0-.064-.142Z" fill="#11cdef" />
                <path id="Path_30" data-name="Path 30" d="M23.5,14.358,14.238,3.713a.836.836,0,0,0-1.343,0l-4.033,4.7V5.449c0-.077-.044-.141-.1-.141H5.825c-.054,0-.1.063-.1.141v6.483L3.634,14.4a1.173,1.173,0,0,0-.26.717,1.263,1.263,0,0,0,.2.752.6.6,0,0,0,.485.288.577.577,0,0,0,.431-.218L13.5,5.534a.071.071,0,0,1,.064-.028.1.1,0,0,1,.064.028L22.641,15.9a.577.577,0,0,0,.431.218.6.6,0,0,0,.485-.288,1.231,1.231,0,0,0,.206-.752A1.176,1.176,0,0,0,23.5,14.358Z" transform="translate(0)" fill="#11cdef" />
              </g>
            </svg>
            <b style="margin-left:50px;">Address</b>
          </strong>
          <i class="fas fa-angle-right"></i>
        </button>
        <div class="accordion-body active">
          <div class="row col-md-12">
            <div class="col-md-12 mb-3"">
			<span class=" text-required">*</span>Address: <br />
              <asp:TextBox type="text" class="form-control" onpaste="return false" placeholder="Room/Floor/Unit Building Name/Street, Barangay, City, Province, Zip Code" id="presentAddress" Maxlength="120" Minlength="10" runat="server" name="count"></asp:TextBox>
            </div>
            <div class="col-md-3 mb-3">
              <span class="text-required">*</span>Province: <br />
              <asp:DropDownList ID="DDProvince" class="form-control" runat="server" OnSelectedIndexChanged="DDProvince_SelectedIndexChanged" AutoPostBack="True"></asp:DropDownList>
            </div>
            <div class="col-md-3 mb-3">
              <span class="text-required">*</span>City: <br />
              <asp:DropDownList ID="DDcity" class="form-control" runat="server"></asp:DropDownList>
            </div>
             <div class="col-md-3 mb-3" align="left">
              <span class="text-required">*</span> Barangay: <br />
              <asp:TextBox type="text" class="form-control" placeholder="Enter Barangay" onpaste="return false" id="fld_Baranggay" runat="server" Maxlength="75" Minlength="2" ></asp:TextBox>
            </div>
             <div class="col-md-3 mb-3" align="left">
              <span class="text-required">*</span> Postal Code: <br />
              <asp:TextBox type="text" class="form-control" placeholder="Enter Postal Code" onpaste="return false" id="fld_ZipCode" runat="server" Maxlength="4" Minlength="4" pattern="[0-9]*" onkeypress="return validNumeric()" ></asp:TextBox>
            </div>
          </div>
        </div>
      </div>
      <div class="accordion-item">
        <button class="accordion-header active" type="button">
          <strong>
            <svg xmlns="http://www.w3.org/2000/svg" width="35" height="35" style="position:absolute;">
              <rect id="Rectangle_73" data-name="Rectangle 73" width="35" height="35" rx="9" fill="#fc7f90" opacity="0.1" />
              <path id="Icon_ionic-md-call" data-name="Icon ionic-md-call" d="M23.757,16.54a15.458,15.458,0,0,1-4.021-.53,1.19,1.19,0,0,0-1.133.241l-2.492,2.119a16,16,0,0,1-7.476-6.357l2.492-2.119a.9.9,0,0,0,.283-.963,9.068,9.068,0,0,1-.68-3.468A1.062,1.062,0,0,0,9.6,4.5H5.633A1.062,1.062,0,0,0,4.5,5.463c0,9.054,8.609,16.374,19.257,16.374a1.062,1.062,0,0,0,1.133-.963V17.5A1.062,1.062,0,0,0,23.757,16.54Z" transform="translate(3 4)" fill="#fc7f90" />
            </svg>
            <b style="margin-left:50px;">Contact</b>
          </strong>
          <i class="fas fa-angle-right"></i>
        </button>
        <div class="accordion-body active">
          <div class="row col-md-12" style="text-align:left;">
            <div class="col-md-4 mb-3">
              <span class="text-required">*</span>Contact No: <br />
              <asp:TextBox type="number" class="form-control" onpaste="return false" id="contactNumber" placeholder="09XXXXXXXXX" runat="server" value='09' data-initial='09' maxlength='11' minlength="11" pattern="[0-9]*" oninput="javascript: if (this.value.length > this.maxLength) this.value = this.value.slice(0, this.maxLength);" onkeypress="return validNumeric()" onchange="javascript:validateContactNumber();"/>
            </div>
            <div class="col-md-6 mb-3">
              <span class="text-required">*</span>Email: <br />
              <asp:TextBox type="email" class="form-control" onpaste="return false" id="emailAddress" placeholder="juandelacruz@gmail.com" onkeypress="return emailCheck(event)" MaxLength="50" Minlength="5" onchange="javascript:ValidateEmail();" runat="server"></asp:TextBox>
              <i class="text-required" style="font-size:smaller"> Your COC details will be sent here.</i>
            </div>
          </div>
        </div>
      </div>

     <% if (Session["IsExist"].ToString() == "False" || Session["IsExist"].ToString() == "True") { %> 
        <div class="accordion-item">
        <button class="accordion-header active" type="button">
          <strong>
            <svg xmlns="http://www.w3.org/2000/svg" width="35" height="35" style="position:absolute;">
              <rect id="Rectangle_73" data-name="Rectangle 73" width="35" height="35" rx="9" fill="#2d3248" opacity="0.1" />
              <path id="Icon_awesome-id-card" data-name="Icon awesome-id-card" d="M18.691,2.25H1.7A1.7,1.7,0,0,0,0,3.949v.566H20.39V3.949A1.7,1.7,0,0,0,18.691,2.25ZM0,16.41a1.7,1.7,0,0,0,1.7,1.7H18.691a1.7,1.7,0,0,0,1.7-1.7V5.648H0ZM12.461,8.2a.284.284,0,0,1,.283-.283h5.1a.284.284,0,0,1,.283.283v.566a.284.284,0,0,1-.283.283h-5.1a.284.284,0,0,1-.283-.283Zm0,2.266a.284.284,0,0,1,.283-.283h5.1a.284.284,0,0,1,.283.283v.566a.284.284,0,0,1-.283.283h-5.1a.284.284,0,0,1-.283-.283Zm0,2.266a.284.284,0,0,1,.283-.283h5.1a.284.284,0,0,1,.283.283v.566a.284.284,0,0,1-.283.283h-5.1a.284.284,0,0,1-.283-.283ZM6.23,7.914a2.266,2.266,0,1,1-2.266,2.266A2.268,2.268,0,0,1,6.23,7.914ZM2.375,15.142a2.269,2.269,0,0,1,2.156-1.565h.29a3.646,3.646,0,0,0,2.818,0h.29a2.269,2.269,0,0,1,2.156,1.565.558.558,0,0,1-.552.7H2.928A.559.559,0,0,1,2.375,15.142Z" transform="translate(7.305 7.321)" fill="#77DD77" />
            </svg>
            <b style="margin-left:50px;">Identification</b>
          </strong>
          <i class="fas fa-angle-right"></i>
        </button>
        <div class="accordion-body active">
          <div class="row col-md-12">
            <div class="col-md-4 mb-3">
              <span class="text-required">*</span>Gender: <br />
              <asp:DropDownList ID="gender" class="form-control" runat="server"></asp:DropDownList>
            </div>
            <div class="col-md-4 mb-3">
               <span class="text-required">*</span>Place of Birth: <br />
              <asp:TextBox type="text" onpaste="return false" class="form-control" id="placeOfBirth" placeholder="Place of Birth" runat="server" Maxlength="150" Minlength="10"></asp:TextBox>
            </div>
            <div class="col-md-4 mb-3">
              <span class="text-required">*</span>Nationality: <br />
              <asp:DropDownList ID="DDNationality" class="form-control" runat="server"></asp:DropDownList>
            </div>
            <div class="col-md-6 mb-3">
              <span class="text-required">*</span>Civil Status: <br />
              <asp:DropDownList ID="civilStatus" class="form-control" runat="server"></asp:DropDownList>
            </div>
            <div class="col-md-6 mb-3">
              <span class="text-required">*</span>Source of Funds: <br />
              <asp:DropDownList ID="DDSourceOfFunds" class="form-control" runat="server"></asp:DropDownList>
            </div>
            <div class="col-md-4 mb-3" align="left">
              <span class="text-required">*</span>Valid ID: <br />
              <asp:DropDownList ID="validIdDropDownList" class="form-control" runat="server"></asp:DropDownList>
            </div>
            <div class="col-md-4 mb-3" align="left">
              <span class="text-required">*</span> ID No.: <br />
              <asp:TextBox type="text" class="form-control" placeholder="Enter your ID Number" onpaste="return false" onkeypress="return characterAndNumbers(event)" id="idNumber" runat="server" Maxlength="25" Minlength="1" onkeydown="return /^[a-zA-Z0-9_-]*$]*$/i.test(event.key)" onchange="javascript:ValidateIdNumber();" oninput="javascript: if (this.value.length > this.maxLength) this.value = this.value.slice(0, this.maxLength);"></asp:TextBox>
            </div>
            <div class="col-md-4 mb-3" align="left">
              <span class="text-required"></span> TIN No.: <br />
              <asp:TextBox type="text" class="form-control" placeholder="Enter TIN Number" onpaste="return false" onkeypress="return characterAndNumbers(event)" id="fld_TinId" runat="server" Maxlength="15" Minlength="1" onkeydown="if (event.keyCode === 9) return true; return /^[0-9\-]+$/.test(event.key) || event.key === 'Backspace'" onchange="javascript:ValidateIdNumber();" oninput="javascript: if (this.value.length > this.maxLength) this.value = this.value.slice(0, this.maxLength);"></asp:TextBox>
            </div>
            <div class="col-md-4 mb-3" align="left">
              <span class="text-required"></span> Occupation: <br />
              <asp:TextBox type="text" class="form-control" placeholder="Enter Occupation" onpaste="return false" onkeypress="return characterAndNumbers(event)" id="fld_Occupation" runat="server" Maxlength="50" Minlength="1" onkeydown="return /^[a-zA-Z0-9 ]*$]*$/i.test(event.key)" onchange="javascript:ValidateIdNumber();" oninput="javascript: if (this.value.length > this.maxLength) this.value = this.value.slice(0, this.maxLength);"></asp:TextBox>
            </div>
            <div class="col-md-4 mb-3" align="left">
              <span class="text-required"></span> Name of Employer / Business: <br />
              <asp:TextBox type="text" class="form-control" placeholder="Name of Employer / Business" onpaste="return false" id="fld_EmployerBusinessName" runat="server" Maxlength="75" Minlength="1" ></asp:TextBox>
            </div>
             <div class="col-md-4 mb-3">
             <span class="text-required">*</span> Nature of Work: <br />
              <asp:DropDownList ID="fld_NatureOfWork" class="form-control" runat="server" Maxlength="50" Minlength="2"></asp:DropDownList>
            </div>
          </div>
        </div>
      </div>
        <% } %> 

     <% if (Session["CategoryId"].ToString() == "6") {%> 
      <div class="accordion-item">
        <button class="accordion-header active" type="button">
          <strong>
            <svg xmlns="http://www.w3.org/2000/svg" width="35" height="35" style="position:absolute;">
              <rect id="Rectangle_73" data-name="Rectangle 73" width="35" height="35" rx="9" fill="#f7a424" opacity="0.1" />
              <g id="Icon_ionic-ios-home" data-name="Icon ionic-ios-home" transform="translate(3.933 3.928)">
                <svg xmlns="http://www.w3.org/2000/svg" width="28" height="25" viewBox="0 0 121.47 121.47">
                  <path id="Icon_material-business" data-name="Icon material-business" d="M67.253,30.2V4.5H3V120.155H131.505V30.2ZM28.7,107.3H15.851V94.454H28.7Zm0-25.7H15.851V68.753H28.7Zm0-25.7H15.851V43.052H28.7Zm0-25.7H15.851V17.351H28.7Zm25.7,77.1H41.552V94.454H54.4Zm0-25.7H41.552V68.753H54.4Zm0-25.7H41.552V43.052H54.4Zm0-25.7H41.552V17.351H54.4Zm64.253,77.1h-51.4V94.454H80.1V81.6H67.253V68.753H80.1V55.9H67.253V43.052h51.4ZM105.8,55.9H92.954V68.753H105.8Zm0,25.7H92.954V94.454H105.8Z" transform="translate(-3 -4.5)" fill="#FEE7A7"></path>
                </svg> =
              </g>
            </svg> 
             <b style="margin-left:50px;">Property Information</b>
          </strong>
          <i class="fas fa-angle-right"></i>
        </button>
        <div class="accordion-body active">
          <div class="row col-md-12">
            <div class="col-md-12 mb-3"">
			  <span class=" text-required">*</span>Full Address of Property to be insured: <br />
              <asp:TextBox type="text" class="form-control" onpaste="return false" placeholder="Room/Floor/Unit Building Name/Street, Barangay, City, Province, Zip Code" id="fld_PropertyAddress" runat="server"  Maxlength="200" Minlength="10"></asp:TextBox>
            </div>
            <div class="col-md-4 mb-3">
              <span class="text-required">*</span>Province: <br />
              <asp:DropDownList ID="PDDLProvince" class="form-control" runat="server" OnSelectedIndexChanged="PDDLProvince_SelectedIndexChanged" AutoPostBack="True"></asp:DropDownList>
            </div>
            <div class="col-md-4 mb-3">
              <span class="text-required">*</span>City: <br />
              <asp:DropDownList ID="PDDLCity" class="form-control" runat="server"></asp:DropDownList>
            </div>
          </div>
          <div class="row col-md-12">
             <div class="col-md-8 mb-3"">
			  <span class=" text-required">*</span>Full Name of Resident: <br />
              <asp:TextBox type="text" id="fld_NameOfResident" class="form-control" onpaste="return false" placeholder="" runat="server"  Maxlength="50" Minlength="1" onkeydown="return /[A-Za-z0-9-' ]/.test(event.key)" onchange="javascript:ValidateFullNameofResident()"></asp:TextBox>
            </div>
          </div>
          <div class="row col-md-12">
             <div class="col-md-4 mb-3">
              <span class="text-required">*</span>Ownership Status: <br />
              <asp:DropDownList ID="DD_OwnershipStatus" class="form-control" runat="server"></asp:DropDownList>
            </div>
             <div class="col-md-3 mb-3">
                  <span class="text-required">*</span>Age of Home: <br />
                   <asp:TextBox type="number" class="form-control" onpaste="return false" id="fld_PropertyAge" runat="server"  maxlength="5" minlength="0" pattern="[0-9]*" oninput="javascript: if (this.value.length > this.maxLength) this.value = this.value.slice(0, this.maxLength);" placeholder="" onkeypress="return validNumeric()"></asp:TextBox>
             </div>
              <div class="col-md-3 mb-3">
                       <span class="text-required">*</span>Floor area: <br />
                    <div class="input-group">
                        <asp:TextBox type="text" class="form-control" onpaste="return false" id="fld_FloorArea" runat="server" maxlength="10" minlength="0" pattern="^\d+(\.\d{1,2})?$"  oninput="javascript: if (this.value.length > this.maxLength) this.value = this.value.slice(0, this.maxLength);" placeholder="" onblur="addFixedDecimalFloorArea()" onkeypress="return FloorAreavalidNumericWithDecimal(event)"></asp:TextBox>
                       <div class="input-group-append">
                           <span class="input-group-text">sqm</span>
                       </div>
                    </div>
             </div>
               <div class="col-md-2 mb-3">
                  <span class="text-required">*</span>No. of Storeys: <br />
                   <asp:TextBox type="number" class="form-control" onpaste="return false" id="fld_NoOfStoreys" maxlength="5" minlength="0" pattern="[0-9]*" oninput="javascript: if (this.value.length > this.maxLength) this.value = this.value.slice(0, this.maxLength);" runat="server"  onkeypress="return validNumeric()"></asp:TextBox>
             </div>

          </div>
           <div class="row col-md-12">
             <div class="col-md-5 mb-3">
              <span class="text-required">*</span>Is the property Mortgaged? <br />
              <asp:DropDownList ID="DD_IsThePropertyMortgaged" class="form-control" runat="server" OnSelectedIndexChanged="DD_IsThePropertyMortgaged_SelectedIndexChanged" AutoPostBack="true">
              </asp:DropDownList>
             </div>
               <div class="col-md-7 mb-3">
                  <div id="spanmortgagee" runat="server" visible=false><span class="text-required">*</span>Name of Mortgagee: <br /></div>
                   <asp:TextBox type="text" class="form-control" onpaste="return false" id="fld_MortgageeName" placeholder="" runat="server" Visible=false Maxlength="100" Minlength="0" onkeydown="return /[A-Za-z0-9 ]/.test(event.key)" onkeypress="return preventSpecialChars(event)"></asp:TextBox>
             </div>
           </div>
            <div class="row col-md-12">
             <div class="col-md-5 mb-3">
              <span class="text-required">*</span>Nature of Occupancy: <br />
              <asp:DropDownList ID="DD_NatureOfOccupancy" class="form-control" runat="server"></asp:DropDownList>
            </div>
              <div class="col-md-5 mb-3">
              <span class="text-required">*</span>Type of Home:<br />
              <asp:DropDownList ID="DD_TypeOfHome" class="form-control" runat="server"></asp:DropDownList>
            </div>
            </div>
             <p><b>Type of Materials:</b></p>
          <div class="container">
            <div class="row show-grid">
                
               <div class="col-sm-6 col-md-1-5 col-lg-2-5">
                    <span class="text-required">*</span>Exterior Walls: <br />
                     <asp:DropDownList ID="DD_TypeExteriorWalls" class="form-control" runat="server" OnSelectedIndexChanged="DD_TypeExteriorWalls_SelectedIndexChanged" AutoPostBack="True"></asp:DropDownList>
                </div>
                <div class="col-sm-6 col-md-1-5 col-lg-2-5">
                   <br />
                   <asp:TextBox ID="otherInputTypeExteriorWalls" runat="server" onpaste="return false" class="form-control" Style="display: none;"  Maxlength="25" Minlength="0" onkeydown="return /[A-Za-z0-9 ]/.test(event.key)" onkeypress="return preventSpecialChars(event)"></asp:TextBox>
                </div>
                <div class="col-sm-6 col-md-1-5 col-lg-2-5">
                    <span class="text-required">*</span>Roof: <br />
                    <asp:DropDownList ID="DD_TypeRoof" class="form-control" runat="server"  OnSelectedIndexChanged="DD_TypeRoof_SelectedIndexChanged" AutoPostBack="True"></asp:DropDownList>
                </div>
                <div class="col-sm-6 col-md-1-5 col-lg-2-5">
                   <br />
                   <asp:TextBox ID="otherInputTypeRoof" runat="server" onpaste="return false" class="form-control" Style="display: none;"  Maxlength="25" Minlength="0" onkeydown="return /[A-Za-z0-9 ]/.test(event.key)" onkeypress="return preventSpecialChars(event)"></asp:TextBox>
                </div>
                <div class="col-sm-6 col-md-1-5 col-lg-1-5">
                    <span class="text-required">*</span>Inner Partitions: <br />
                    <asp:DropDownList ID="DD_TypeInnerPartitions" class="form-control" runat="server" OnSelectedIndexChanged="DD_TypeInnerPartitions_SelectedIndexChanged" AutoPostBack="True"></asp:DropDownList>
                </div>
                <div class="col-sm-6 col-md-1-5 col-lg-1-5">
                    <br />
                   <asp:TextBox ID="otherInputTypeInnerPartitions" runat="server" onpaste="return false" class="form-control" Style="display: none;"  Maxlength="25" Minlength="0" onkeydown="return /[A-Za-z0-9 ]/.test(event.key)" onkeypress="return preventSpecialChars(event)"></asp:TextBox>
                </div>
                <div class="col-sm-6 col-md-1-5 col-lg-3-5">
                    <span class="text-required">*</span>Beams: <br />
                    <asp:DropDownList ID="DD_TypeBeams" class="form-control" runat="server" OnSelectedIndexChanged="DD_TypeBeams_SelectedIndexChanged" AutoPostBack="True"></asp:DropDownList>
                </div>
                <div class="col-sm-6 col-md-1-5 col-lg-3-5">
                   <br />
                   <asp:TextBox ID="otherInputTypeBeams" runat="server" onpaste="return false" class="form-control" Style="display: none;"  Maxlength="25" Minlength="0" onkeydown="return /[A-Za-z0-9 ]/.test(event.key)" onkeypress="return preventSpecialChars(event)"></asp:TextBox>
                </div>
                <div class="col-sm-6 col-md-1-5 col-lg-1-5">
                    <span class="text-required">*</span>Columns: <br />
                     <asp:DropDownList ID="DD_TypeColumns" class="form-control" runat="server" OnSelectedIndexChanged="DD_TypeColumns_SelectedIndexChanged" AutoPostBack="True"></asp:DropDownList>
                </div>
                <div class="col-sm-6 col-md-1-5 col-lg-1-5">
                    <br />
                   <asp:TextBox ID="otherInputTypeColumns" runat="server" onpaste="return false" class="form-control" Style="display: none;"  Maxlength="25" Minlength="0" onkeydown="return /[A-Za-z0-9 ]/.test(event.key)" onkeypress="return preventSpecialChars(event)"></asp:TextBox>
                </div>
            </div>
        </div>
            <br />
            <p><b>Boundaries:</b></p>
            <div class="row col-md-12">
                 <div class="col-sm-6 col-md-1-5 col-lg-2-5" style="float:left;">
                    <span class="text-required">*</span>Front: <br />
                   <asp:TextBox type="text" class="form-control" onpaste="return false" id="fld_BoundaryFront" placeholder="Ex. Main Road" runat="server"  Maxlength="25" Minlength="0" onkeydown="return /[A-Za-z0-9 ]/.test(event.key)" onkeypress="return preventSpecialChars(event)"></asp:TextBox>
                  </div>
                   <div class="col-sm-6 col-md-1-5 col-lg-2-5" style="float:left;">
                    <span class="text-required">*</span>Rear: <br />
                   <asp:TextBox type="text" class="form-control" onpaste="return false" id="fld_BoundaryRear" placeholder="Ex. Condo" runat="server"  Maxlength="25" Minlength="0" onkeydown="return /[A-Za-z0-9 ]/.test(event.key)" onkeypress="return preventSpecialChars(event)"></asp:TextBox>
                  </div>
                    <div class="col-sm-6 col-md-1-5 col-lg-2-5" style="float:left;">
                    <span class="text-required">*</span>Left: <br />
                   <asp:TextBox type="text" class="form-control" onpaste="return false" id="fld_BoundaryLeft" placeholder="Ex. Condo" runat="server" Maxlength="25" Minlength="0" onkeydown="return /[A-Za-z0-9 ]/.test(event.key)" onkeypress="return preventSpecialChars(event)"></asp:TextBox>
                  </div>
                   <div class="col-sm-6 col-md-1-5 col-lg-2-5" style="float:left;">
                    <span class="text-required">*</span>Right: <br />
                   <asp:TextBox type="text" class="form-control" onpaste="return false" id="fld_BoundaryRight" placeholder="Ex. House" runat="server" Maxlength="25" Minlength="0" onkeydown="return /[A-Za-z0-9 ]/.test(event.key)" onkeypress="return preventSpecialChars(event)"></asp:TextBox>
                  </div>
            </div>
            <br />
            <div class="row col-md-12">
                <div class="col-md-6" style="float:left; top: 0px; left: 0px;">
                 <span class="text-required">*</span>Has there any previous incident relating to fire? <br />
                 <asp:DropDownList ID="DD_IsThereAnyPreviousLoss" class="form-control" runat="server" OnSelectedIndexChanged="DD_IsThereAnyPreviousLoss_SelectedIndexChanged" AutoPostBack="True"> 
                 </asp:DropDownList>
                </div>
                <div class="col-sm-6 col-md-1-5 col-lg-3-5">
                   <div id="spandetailsofincident" runat="server" visible=false><span class="text-required">*</span>Please indicate date and detail of incident: <br /></div>
                   <asp:TextBox ID="fld_Remarks" runat="server" onpaste="return false" class="form-control" Style="display: none;"  Maxlength="250" Minlength="1"></asp:TextBox>
                </div>
            </div>
            <div class="row col-md-12">
                <div class="col-md-4" style="float:left;">
                      <span class="text-required">*</span>Distance from bodies of water: <br />
                    <div class="input-group">
                  <asp:TextBox type="text" class="form-control" onpaste="return false" id="fld_BodyOfWaterDistance" runat="server" maxlength="10" minlength="0" oninput="javascript: if (this.value.length > this.maxLength) this.value = this.value.slice(0, this.maxLength);" placeholder="" onblur="addFixedDecimalBodyofWater()" onkeypress="return BodyOfWatervalidNumericWithDecimal(event)"></asp:TextBox>

                       <div class="input-group-append">
                           <span class="input-group-text">m</span>
                       </div>
                    </div>
                </div>
            </div>
        </div>
      </div>
      <% } %> 

      <% else { %> 
            <div class="accordion-item">
            <button class="accordion-header active" type="button">
              <strong>
                <svg xmlns="http://www.w3.org/2000/svg" width="35" height="35" style="position:absolute;">
                  <rect id="Rectangle_73" data-name="Rectangle 73" width="35" height="35" rx="9" fill="#114b92" opacity="0.1" />
                  <g id="Icon_ionic-ios-briefcase" data-name="Icon ionic-ios-briefcase" transform="translate(3.93 2.589)">
                    <path id="Path_31" data-name="Path 31" d="M4.943,24.019H22.2c.868,0,1.568-.528,1.568-1.181V15.75H3.375v7.088C3.375,23.491,4.076,24.019,4.943,24.019Z" transform="translate(0 -0.697)" fill="#9ADEDB" />
                    <path id="Path_32" data-name="Path 32" d="M22.2,9H21.8V8.438c0-.309-.176-.562-.392-.562H19.844c-.216,0-.392.253-.392.563V9H18.079V6.75c0-1.238-.706-2.25-1.568-2.25H10.629c-.863,0-1.568,1.012-1.568,2.25V9H7.688V8.438c0-.309-.176-.562-.392-.562H5.728c-.216,0-.392.253-.392.563V9H4.943c-.868,0-1.568,1.005-1.568,2.25v2.813h20.39V11.25C23.765,10.005,23.064,9,22.2,9Zm-5.49,0H10.433V7.031c0-.309.176-.562.392-.562h5.49c.216,0,.392.253.392.563Z" fill="#9ADEDB" />
                  </g>
                </svg>
                <b style="margin-left:50px;">Beneficiary Information</b>
              </strong>
              <i class="fas fa-angle-right"></i>
            </button>
            <div class="accordion-body active">
              <div class="col-md-12">
                <div class="row">
                  <div class="col-md-8 mb-3">
                    <span class=text-required>*</span>Beneficiary Full Name: <br />
                    <asp:TextBox runat="server" class="form-control" onpaste="return false" placeholder="Beneficiary Full Name" ID="beneFullname" Maxlength="150" Minlength="1" onkeypress="return characterAndNumbers(event)" onkeydown="return /[A-Za-z0-9-' ]/.test(event.key)" onchange="javascript:ValidateBenefeciaryFullName();"></asp:TextBox>
                  </div>
                </div>
                <div class="row">
                  <div class="col-md-4 mb-3">
                    <span class="text-required">*</span> Relationship to the Insured: <br />
                    <asp:DropDownList runat="server" class="form-control" onpaste="return false" ID="relationshipDropDownList"></asp:DropDownList>
                  </div>
                </div>
              </div>
            </div>
          </div>
            <% if (Session["IsMinor"].ToString() == "True") { %> 
            <div class="accordion-item">
            <button class="accordion-header active" type="button">
              <strong>
                <svg xmlns="http://www.w3.org/2000/svg" width="35" height="35" style="position:absolute;">
                  <rect id="Rectangle_73" data-name="Rectangle 73" width="35" height="35" rx="9" fill="#f7a424" opacity="0.1" />
                  <path id="Icon_ionic-md-person" data-name="Icon ionic-md-person" d="M14.7,14.7A5.1,5.1,0,1,0,9.6,9.6,5.113,5.113,0,0,0,14.7,14.7Zm0,2.549c-3.378,0-10.2,1.721-10.2,5.1v2.549H24.894V22.345C24.894,18.967,18.075,17.246,14.7,17.246Z" transform="translate(2.803 2.803)" fill="#76B4FF" />
                </svg>
                <b style="margin-left:50px;">Guardian Information</b>
              </strong>
              <i class="fas fa-angle-right"></i>
            </button>
            <div class="accordion-body active">
              <div class="row">
                <div class="col-md-8 mb-3" align="left">
                  <span class=text-required>*</span>Guardian Full Name: <br />
                  <asp:TextBox runat="server" class="form-control" onpaste="return false" placeholder="Guardian Full Name" onkeypress="return characterAndNumbers(event)" ID="guardianFullName" Maxlength="150" Minlength="1" onkeydown="return /[A-Za-z0-9-' ]/.test(event.key)" onchange="javascript:ValidateGuardianFullName();"></asp:TextBox>
                </div>
                <div class="col-md-4 mb-3" align="left">
                  <span class="text-required">*</span> Birthdate: <br />
                 <asp:TextBox runat="server" class="form-control" onpaste="return false" type="date" ID="guardianBirthDate" onblur="ValidateDOB();"></asp:TextBox>
                </div>
              </div>
              <div class="row" align="left">
                <div class="col-md-4 mb-3" align="left">
                  <span class="text-required">*</span> Relationship to the Insured: <br />
                  <asp:DropDownList runat="server" class="form-control" onpaste="return false" ID="guardianRelationshipDropDownList"></asp:DropDownList>
                </div>
              </div>
            </div>
          </div>
            <% } %> 

      <% } %> 
      <%  if (Session["CategoryId"].ToString() == "10") { %>
       <div class="accordion-item">
        <button class="accordion-header active" type="button" style="display: flex; align-items: center; justify-content: space-between;">
          <strong style="display: flex; align-items: center;">
            <svg version="1.0" xmlns="http://www.w3.org/2000/svg"
            width="24.000000pt" height="24.000000pt" viewBox="0 0 64.000000 64.000000"
            preserveAspectRatio="xMidYMid meet" style="margin-right: 5px;">

            <!-- Add the background rectangle -->
            <rect width="64" height="64" rx="9" fill="#11CDEF" opacity="0.1" />

            <g transform="translate(0.000000,64.000000) scale(0.100000,-0.100000)" fill="#0CB795" stroke="none">
              <path d="M384 500 c-47 -19 -67 -77 -42 -123 26 -50 77 -61 121 -27 49 39 42 116 -14 145 -32 17 -36 17 -65 5z"/>
              <path d="M173 456 c-22 -19 -28 -32 -28 -65 0 -48 24 -77 72 -87 27 -5 37 -1 63 24 40 41 41 83 1 123 -36 35 -70 37 -108 5z"/>
              <path d="M357 302 c-16 -7 -15 -11 12 -37 17 -15 34 -43 37 -62 6 -33 6 -33 64 -33 89 0 117 41 63 92 -46 44 -122 61 -176 40z"/>
              <path d="M145 265 c-61 -31 -81 -77 -47 -108 15 -14 40 -17 126 -17 59 0 116 5 127 10 41 23 16 86 -48 119 -31 16 -123 14 -158 -4z"/>
            </g>
            </svg>

            <b style="margin-left:15px;">Dependent Information</b>
          </strong>
          <i class="fas fa-angle-right"></i>
        </button>
        <div class="accordion-body active">
            <div class="row">
                  <div class="col-md-4">
                  <span class="text-required">*</span> Type of Dependents: <br />
                  <asp:DropDownList runat="server" class="form-control" onpaste="return false" ID="DD_TypeOfDependents" AutoPostBack="true" OnSelectedIndexChanged="DD_TypeOfDependents_SelectedIndexChanged"></asp:DropDownList>
                   </div>            
            </div>
             <br />
                <p><b>Note:</b> Atleast 1 dependent is required to proceed</p>
            <div id="relationshipDropdownContainer" runat="server" style="background-color: #F0F2F5; border: solid 1px #D0D0D0; border-radius:10px; padding:30px;">
       
            </div>
        </div>
      </div>
    <% } %>
      <%  if (Session["CategoryId"].ToString() == "11") { %>
       <div class="accordion-item">
        <button class="accordion-header active" type="button" style="display: flex; align-items: center; justify-content: space-between;">
          <strong style="display: flex; align-items: center;">
           <svg version="1.0" xmlns="http://www.w3.org/2000/svg"
            width="25.000000pt" height="25.000000pt" viewBox="0 0 64.000000 64.000000"
            preserveAspectRatio="xMidYMid meet" style="margin-right: 5px;">
            <!-- Add the background rectangle -->
            <rect width="64" height="64" rx="9" fill="#354E5A" />
            <g transform="translate(7, 60) scale(0.080000,-0.080000)translate(10.000000,64.000000)" fill="#CDDEE6" stroke="none">
                <path d="M451.5,511.5 C458.5,511.5 465.5,511.5 472.5,511.5C 492.5,505.5 505.5,492.5 511.5,472.5C 511.5,465.5 511.5,458.5 511.5,451.5C 509.189,444.54 506.023,437.873 502,431.5C 463.833,394.667 425.667,357.833 387.5,321C 416.295,235.616 444.462,150.116 472,64.5C 454.5,47 437,29.5 419.5,12C 418.833,11.3333 418.167,11.3333 417.5,12C 373.708,82.4162 330.041,152.916 286.5,223.5C 249.485,187.984 212.318,152.651 175,117.5C 178.043,96.5418 181.376,75.5418 185,54.5C 185.667,51.8333 185.667,49.1667 185,46.5C 169.298,30.9652 153.798,15.2986 138.5,-0.5C 137.833,-0.5 137.167,-0.5 136.5,-0.5C 124.38,33.025 112.547,66.6917 101,100.5C 67.1171,112.405 33.2837,124.405 -0.5,136.5C -0.5,137.167 -0.5,137.833 -0.5,138.5C 15.2986,153.798 30.9652,169.298 46.5,184C 49.1667,184.667 51.8333,184.667 54.5,184C 75.5682,180.544 96.5682,177.21 117.5,174C 152.651,211.318 187.984,248.485 223.5,285.5C 152.916,329.041 82.4162,372.708 12,416.5C 11.3333,417.167 11.3333,417.833 12,418.5C 29.5,436 47,453.5 64.5,471C 150.105,443.465 235.605,415.299 321,386.5C 357.833,424.667 394.667,462.833 431.5,501C 437.874,505.023 444.54,508.19 451.5,509.5 Z"/>
            </g>
            </svg>
            <b style="margin-left:15px;">Travel Information</b>
          </strong>
          <i class="fas fa-angle-right"></i>
        </button>
        <div class="accordion-body active">
            <div class="container">
          <div class="row">

           <div class="col-md-4 mb-3">
            <span class="text-required">*</span> Origin: <br />
            <asp:DropDownList runat="server" class="form-control" onpaste="return false" ID="fld_Origin" AutoPostBack="true"></asp:DropDownList>
          </div>
          <div class="col-md-4 mb-3">
            <span class="text-required">*</span> Destination: <br />
            <asp:DropDownList runat="server" class="form-control" onpaste="return false" ID="fld_Destination" OnSelectedIndexChanged="fld_Destination_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
          </div>
         <div class="col-md-4 mb-3">
            <div id="lbl_VisaType" runat="server"><span class="text-required">*</span> Visa Type: <br /></div>  
         <asp:DropDownList onpaste="return false" class="form-control" id="fld_VisaType" runat="server" readonly="readonly" Enabled="false"></asp:DropDownList>
          </div>
        <div class="col-md-12 mb-3">
        <span class="text-required">*</span> Purpose of Travel: <br />
            <div class="plans">
              <div class="title">Choose a pricing plan</div>
              <label class="plan basic-plan" for="basic">
                <asp:RadioButton ID="basicRadio" runat="server" AutoPostBack="true" Text="&nbsp;&nbsp;Leisure" GroupName="plan"/>
                <div class="plan-content">
                  <img loading="lazy" src="Images/leisure.png" alt="" />
                  <div class="plan-details">
                    <span>Leisure</span>
                    <p>Travel for pleasure, relaxation, and exploration, offering a break from routine.</p>
                  </div>
                </div>
              </label>
              <label class="plan complete-plan" for="complete">
                <asp:RadioButton ID="completeRadio" runat="server" AutoPostBack="true"  Text="&nbsp;&nbsp;Business" GroupName="plan" />
                <div class="plan-content">
                  <img loading="lazy" src="Images/business.png" alt="" />
                  <div class="plan-details">
                    <span>Business</span>
                    <p>Travel for business activities, meetings, conferences, and negotiations.</p>
                  </div>
                </div>
              </label>
            </div>
        </div>
        <div class="col-md-12 mb-3">
           <span class="text-required">*</span>Travel Duration <br />
        </div>
         <div class="col-md-5 mb-3">
               <span class="text-required">*</span><label for="travelFrom">From:</label><br />
              <asp:TextBox runat="server"  type="text" id="travelFrom" data-datepicker="true" class="form-control"></asp:TextBox>
         </div>
        <div class="col-md-4 mb3">
        <span class="text-required">*</span><label for="travelTo">To:</label> <br />
              <asp:TextBox runat="server"  type="text" id="travelTo" data-datepicker="true" class="form-control"></asp:TextBox>
        </div>
        <div class="col-md-3 mb3">
           <span class="text-required">*</span><label for="numberOfDays">Number of Days:</label>
          <asp:TextBox runat="server" type="text" id="numberOfDays" class="form-control" Enabled="true" onkeydown="return false;"></asp:TextBox>

        </div>
        <input type="hidden" id="selectedStartDate" />
        <input type="hidden" id="selectedEndDate" />
        <input type="hidden" id="durationDays" />
        <div class="col-md-6 mb-3">
         <span class="text-required">*</span><label>Booking Reference No.</label><br />
          <asp:TextBox type="text" onpaste="return false" class="form-control" id="fld_BookingReferenceNo" placeholder="Enter Booking Reference No." runat="server" Maxlength="100" Minlength="1"></asp:TextBox>
        </div>
         <div class="col-md-6 mb-3">
        <div id="lb_PassPortNo" runat="server"><span class="text-required">*</span><label>Passport No.</label><br /></div> 
          <asp:TextBox type="text" onpaste="return false" class="form-control" id="fld_PassportNo" placeholder="Enter Passport No." runat="server" Maxlength="100" Minlength="1"></asp:TextBox>
        </div>
            <%if (Session["CheckIfCovidCoverage"].ToString() == "True") { %>
            <div class="col-md-12 mb-3">
                <br />
                <span class="text-required">*</span><label><b>Covid-19 Coverage:</b> Upgrades your plan to cover Covid-19 related conditions.</label>
            </div>
            <div class="col-md-6 mb-3">
                <asp:RadioButtonList ID="rbCovidCoverage" runat="server" RepeatDirection="Horizontal">
                    <asp:ListItem Text="&nbsp;Yes" Value="True" />
                    <asp:ListItem Text="&nbsp;No" Value="False" />
                </asp:RadioButtonList>
            </div>
            <%} %>
         </div>
            </div>
        </div>
      </div>
    <%  if (Session["DisplayCoverageModule"].ToString() == "True") { %>
           <div class="accordion-item">
              <button class="accordion-header active" type="button" style="display: flex; align-items: center; justify-content: space-between;">
                <strong style="display: flex; align-items: center;">
                  <b style="margin-left:15px;">Optional Coverage</b>
                </strong>
                <i class="fas fa-angle-right"></i>
              </button>
              <div class="accordion-body active">
              <div class="container">
            <asp:Repeater ID="repeaterQuestions" runat="server">
                <ItemTemplate>
                    <div class="container">
                        <span class="question">
                            <asp:HiddenField ID="hdnQuestionNo" runat="server" Value='<%# Eval("QuestionNo") %>' />
                            <%# "*" + " " + Eval("QuestionNo") + ". " %>
                        </span>
                        <span class="coverage-name"><b><%# Eval("OptionalCoverageName") %>:</b></span>
                        <span class="description"><%# Eval("OptionalCoverageDescription") %></span>
                        <br /><br />
                        <div class="radio-buttons">
                            <asp:RadioButtonList ID="rblYesNo" runat="server">
                                <asp:ListItem Text="Yes" Value="Yes" />
                                <asp:ListItem Text="No" Value="No" />
                            </asp:RadioButtonList>
                        </div>
                        <br /><br />
                    </div>
                </ItemTemplate>
            </asp:Repeater>

              </div>
             </div>
            </div>
           <% } %>
    <% } %>
      <% if (Session["CategoryId"].ToString() == "8"){ %>
        <div class="accordion-item">
            <button class="accordion-header active" type="button">
              <strong>
                <?xml version="1.0" encoding="iso-8859-1"?>
                <!DOCTYPE svg PUBLIC "-//W3C//DTD SVG 1.1//EN" "http://www.w3.org/Graphics/SVG/1.1/DTD/svg11.dtd">
                <svg fill="#7589C4" version="1.1" id="Capa_1" xmlns="http://www.w3.org/2000/svg" xmlns:xlink="http://www.w3.org/1999/xlink" 
	                 width="20px" height="20px" viewBox="0 0 485.69 485.69">
                <g>
	                <g>
		                <g>
			                <path d="M410.428,34.738h-76.405l5.155,23.852c0.634,2.961,0.603,5.934,0.271,8.859h54.621V452.98H91.588V67.449h54.637
				                c-0.332-2.941-0.348-5.914,0.285-8.891l5.156-23.82H75.248c-9.031,0-16.34,7.324-16.34,16.354v418.243
				                c0,9.016,7.309,16.354,16.34,16.354h335.18c9.031,0,16.354-7.341,16.354-16.354V51.093
				                C426.783,42.062,419.459,34.738,410.428,34.738z"/>
			                <path d="M355.568,152.949h-111.71c-9.047,0-16.355,7.324-16.355,16.34c0,9.035,7.309,16.355,16.355,16.355h111.71
				                c9.047,0,16.354-7.32,16.354-16.355C371.924,160.273,364.615,152.949,355.568,152.949z"/>
			                <path d="M355.568,253.254h-111.71c-9.047,0-16.355,7.323-16.355,16.354c0,9.021,7.309,16.357,16.355,16.357h111.71
				                c9.047,0,16.354-7.34,16.354-16.357C371.924,260.577,364.615,253.254,355.568,253.254z"/>
			                <path d="M119.556,156.792c-6.898,5.82-7.786,16.137-1.965,23.047l23.855,28.27c3.117,3.699,7.688,5.805,12.496,5.805
				                c0.398,0,0.792-0.016,1.203-0.047c5.219-0.379,9.949-3.258,12.703-7.719l42.914-69.477c4.746-7.688,2.375-17.75-5.312-22.492
				                c-7.688-4.777-17.75-2.375-22.497,5.313l-31.066,50.273l-9.301-11.012C136.763,151.843,126.467,150.956,119.556,156.792z"/>
			                <path d="M158.72,245.094c-13.554,0-24.535,10.978-24.535,24.517c0,13.543,10.98,24.52,24.535,24.52
				                c13.543,0,24.52-10.977,24.52-24.52C183.24,256.07,172.263,245.094,158.72,245.094z"/>
			                <path d="M355.568,351.359h-111.71c-9.047,0-16.355,7.309-16.355,16.358c0,9.017,7.309,16.34,16.355,16.34h111.71
				                c9.047,0,16.354-7.323,16.354-16.34C371.924,358.667,364.615,351.359,355.568,351.359z"/>
			                <path d="M158.72,343.199c-13.554,0-24.535,10.977-24.535,24.52c0,13.539,10.98,24.521,24.535,24.521
				                c13.543,0,24.52-10.979,24.52-24.521C183.24,354.176,172.263,343.199,158.72,343.199z"/>
			                <path d="M173.463,75.613h138.73c3.401,0,6.613-1.521,8.746-4.176c2.137-2.629,2.961-6.105,2.229-9.43L311.686,8.859
				                C310.564,3.687,305.994,0,300.708,0H184.963c-5.281,0-9.852,3.688-10.977,8.859l-11.5,53.148
				                c-0.695,3.324,0.125,6.801,2.247,9.43C166.868,74.093,170.08,75.613,173.463,75.613z"/>
		                </g>
	                </g>
                </g>
                </svg>
                <b style="margin-left:2px;">Insured Health Declaration</b>
              </strong>
              <i class="fas fa-angle-right"></i>
            </button>
            <div class="accordion-body active">
              <div class="row">
                <div class="col-md-6 mb-3" align="left">
                  <span class=text-required>*</span>Do you have any physical deformity?<br />
                    <asp:RadioButtonList ID="fld_PhysicalDeformity" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem Text="&nbsp;No" Value="No" />
                        <asp:ListItem Text="&nbsp;Yes" Value="Yes" />
                    </asp:RadioButtonList>
                </div>
                <div class="col-md-6 mb-3" align="left" id="textareaContainer_fld_PhysicalDeformity" style="display: none;">
                  <span class="text-required">*</span> Please specify: <br />
                    <asp:TextBox runat="server" class="form-control text-specify" ID="fld_PhysicalDeformityDetails" oninput="checkLength(this)" TextMode="MultiLine" Height="70"></asp:TextBox>
                </div>
              </div>
              <div class="row" align="left">
                <div class="col-md-6 mb-3" align="left">
                  <span class="text-required">*</span>Do you have any pre-existing illnesses?: <br />
                    <asp:RadioButtonList ID="fld_PreExistingIllness" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem Text="&nbsp;No" Value="No" />
                        <asp:ListItem Text="&nbsp;Yes" Value="Yes" />
                    </asp:RadioButtonList>
                </div>
               <div class="col-md-6 mb-3" align="left" id="textareaContainer_fld_PreExistingIllness" style="display: none;"> 
                  <span class="text-required">*</span>Please specify: <br />
               <asp:TextBox runat="server" class="form-control text-specify" ID="fld_PreExistingIllnessDetails" onpaste="return false" oninput="checkLength(this)" TextMode="MultiLine" Height="70"></asp:TextBox>
                </div>
              </div>
            </div>
          </div>
         <div class="accordion-item">
            <button class="accordion-header active" type="button">
              <strong>
                <svg height="20px" width="20px" version="1.1" id="Capa_1" xmlns="http://www.w3.org/2000/svg" xmlns:xlink="http://www.w3.org/1999/xlink" 
	                 viewBox="0 0 48.839 48.839" xml:space="preserve">
                <g>
	                <path style="fill:#EF6534;" d="M39.041,36.843c2.054,3.234,3.022,4.951,3.022,6.742c0,3.537-2.627,5.252-6.166,5.252
		                c-1.56,0-2.567-0.002-5.112-1.326c0,0-1.649-1.509-5.508-1.354c-3.895-0.154-5.545,1.373-5.545,1.373
		                c-2.545,1.323-3.516,1.309-5.074,1.309c-3.539,0-6.168-1.713-6.168-5.252c0-1.791,0.971-3.506,3.024-6.742
		                c0,0,3.881-6.445,7.244-9.477c2.43-2.188,5.973-2.18,5.973-2.18h1.093v-0.001c0,0,3.698-0.009,5.976,2.181
		                C35.059,30.51,39.041,36.844,39.041,36.843z M16.631,20.878c3.7,0,6.699-4.674,6.699-10.439S20.331,0,16.631,0
		                S9.932,4.674,9.932,10.439S12.931,20.878,16.631,20.878z M10.211,30.988c2.727-1.259,3.349-5.723,1.388-9.971
		                s-5.761-6.672-8.488-5.414s-3.348,5.723-1.388,9.971C3.684,29.822,7.484,32.245,10.211,30.988z M32.206,20.878
		                c3.7,0,6.7-4.674,6.7-10.439S35.906,0,32.206,0s-6.699,4.674-6.699,10.439C25.507,16.204,28.506,20.878,32.206,20.878z
		                 M45.727,15.602c-2.728-1.259-6.527,1.165-8.488,5.414s-1.339,8.713,1.389,9.972c2.728,1.258,6.527-1.166,8.488-5.414
		                S48.455,16.861,45.727,15.602z"/>
                </g>
                </svg>
                <b style="margin-left:10px;">Pet Details</b>
              </strong>
              <i class="fas fa-angle-right"></i>
            </button>
            <div class="accordion-body active">

                    <div class="row">
        <div class="col-md-8 col-12 pet-selection">
            <span class="text-required">&nbsp;&nbsp;*</span><label> Select Type of Pet</label><br />
            <div class="d-flex align-items-center justify-content-center flex-column flex-md-row">
                <asp:RadioButtonList ID="fld_PetCategory" runat="server" CssClass="image-radio-list" RepeatDirection="Horizontal" RepeatLayout="Table">
                    <asp:ListItem Value="Cat">
                        <div style="text-align:center;">
                            <label style="margin-left:10px">Cat</label>
                            <img src="/Images/pet/cat-icon.png" class="pet-image" alt="Cat" /><br />
                        </div>
                    </asp:ListItem>
                    <asp:ListItem Value="Dog">
                        <div style="text-align:center;">
                            <label style="margin-left:10px">Dog</label>
                            <img src="/Images/pet/dog-icon.png" class="pet-image" alt="Dog" /><br />
                        </div>
                    </asp:ListItem>
                </asp:RadioButtonList>
            </div>
        </div>
        <div class="col-md-3 col-12 pet-selection">
            <span class="text-required">&nbsp;&nbsp;*</span><label>Select Pet Gender:</label><br /><br />
            <div class="d-flex align-items-center justify-content-center">
                <asp:RadioButtonList ID="fld_Gender" runat="server" RepeatDirection="Vertical">
                    <asp:ListItem Text="&nbsp; &nbsp;Male" Value="M" />
                    <asp:ListItem Text="&nbsp; &nbsp;Female" Value="F" />
                </asp:RadioButtonList>
            </div>
        </div>
    </div>
          
            <div class="row">
                <div class="col-md-3 mb-3" align="left">
                   <span class="text-required">*</span><label>Name</label><br />
                   <asp:TextBox type="text" onpaste="return false" class="form-control" id="fld_PetName" runat="server"  onkeypress="return characterAndNumbers(event)"  Maxlength="20" Minlength="1" onkeydown="return /[A-Za-z0-9-' ]/.test(event.key)" onchange="javascript:ValidatePetName();"></asp:TextBox>
                </div>
               <div class="col-md-3 mb-3" align="left">
                   <span class="text-required">*</span><label>Breed</label><br />
                   <asp:TextBox type="text" onpaste="return false" class="form-control" id="fld_Breed" runat="server" Maxlength="40" Minlength="1" onkeypress="return characterAndNumbers(event)" onkeydown="return /[A-Za-z0-9 ]/.test(event.key)"></asp:TextBox>
               </div>
              <div class="col-md-3 mb-3" align="left">
                  <span class="text-required">*</span><label>Color</label><br />
                  <asp:TextBox type="text" onpaste="return false" class="form-control" id="fld_Color"  runat="server" Maxlength="25" Minlength="1" onkeypress="return characterAndNumbers(event)"  onkeydown="return /[A-Za-z0-9 ]/.test(event.key)"></asp:TextBox>
              </div>
              <div class="col-md-3 mb-3" align="left">
                   <span class="text-required">*</span><label>Date of Birth</label><br />
                  <asp:TextBox type="date" onpaste="return false" class="form-control disableFuturedate" id="fld_PetBirthdate" onblur="ValidateDOBPET();" runat="server" Maxlength="10" Minlength="10"></asp:TextBox>
              </div>
            </div>
             <div class="row">
                <div class="col-md-6 mb-3" align="left">
                     <span class="text-required"></span><label>Pedigree Certificate No.</label><br />
                    <asp:TextBox type="text" onpaste="return false" class="form-control" id="fld_PedigreeCertificateNo" runat="server"  Maxlength="25" Minlength="1" onkeypress="return characterAndNumbers(event)"  onkeydown="return /[A-Za-z0-9 ]/.test(event.key)"></asp:TextBox>
                </div>
                <div class="col-md-6 mb-3" align="left">
                        <span class="text-required"></span><label>RFID No.:</label><br />
                    <asp:TextBox type="text" onpaste="return false" class="form-control" id="fld_RFIDNo" runat="server" Maxlength="25" Minlength="1" onkeypress="return characterAndNumbers(event)"  onkeydown="return /[A-Za-z0-9 ]/.test(event.key)"></asp:TextBox>
                </div>
             </div>
              <div class="row">
                <div class="col-md-6 mb-3" align="left">
                  <span class=text-required>*</span>Does your pet undergo yearly examinations, checkups, treatments?<br />
                    <asp:RadioButtonList ID="fld_PetYearlyTreatment" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem Text="&nbsp;No" Value="No" />
                        <asp:ListItem Text="&nbsp;Yes" Value="Yes" />
                    </asp:RadioButtonList>
                </div>
                <div class="col-md-6 mb-3" align="left" id="textareaContainer_fld_PetYearlyTreatment" style="display: none;">
                  <span class="text-required">*</span> Please specify: <br />
                 <asp:TextBox runat="server" class="form-control text-specify" ID="fld_PetYearlyTreatmentDetails" onpaste="return false" oninput="checkLength(this)"  TextMode="MultiLine" Height="70"></asp:TextBox>
                </div>
              </div>
              <div class="row" align="left">
                <div class="col-md-6 mb-3" align="left">
                  <span class="text-required">*</span>Does your pet have any physical deformities, pre-existing illnesses, or underwent operation? <br />
                    <asp:RadioButtonList ID="fld_PetHistory" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem Text="&nbsp;No" Value="No" />
                        <asp:ListItem Text="&nbsp;Yes" Value="Yes" />
                    </asp:RadioButtonList>
                </div>
               <div class="col-md-6 mb-3" align="left" id="textareaContainer_fld_PetHistory" style="display: none;">
                  <span class="text-required">*</span>Please specify: <br />
                   <asp:TextBox runat="server" class="form-control text-specify" ID="fld_PetHistoryDetails" onpaste="return false" oninput="checkLength(this)" TextMode="MultiLine" Height="70"></asp:TextBox>
                </div>
              </div>
               <div class="row" align="left">
                <div class="col-md-6 mb-3" align="left">
                  <span class="text-required">*</span>Does your pet take any vitamins, or medications?: <br />
                    <asp:RadioButtonList ID="fld_PetVitamins" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem Text="&nbsp;No" Value="No" />
                        <asp:ListItem Text="&nbsp;Yes" Value="Yes" />
                    </asp:RadioButtonList>
                </div>
                <div class="col-md-6 mb-3" align="left" id="textareaContainer_fld_PetVitamins" style="display: none;">
                  <span class="text-required">*</span>Please specify: <br />
                 <asp:TextBox runat="server" class="form-control text-specify" ID="fld_PetVitaminsDetails" onpaste="return false" oninput="checkLength(this)"  TextMode="MultiLine" Height="70"></asp:TextBox>
                </div>
              </div>
            </div>
          </div>

      <%} %>
 </ContentTemplate>
</asp:UpdatePanel>
        <div class="accordion-item">
        <button class="accordion-header active" type="button">
          <strong>
            <svg xmlns="http://www.w3.org/2000/svg" width="35" height="35" style="position:absolute;">
              <rect id="Rectangle_73" data-name="Rectangle 73" width="35" height="35" rx="9" fill="#f2be1d" opacity="0.1" />
              <path id="Icon_ionic-ios-information" data-name="Icon ionic-ios-information" d="M16.552,13.007a2.1,2.1,0,1,1,2.092,2.041A2.043,2.043,0,0,1,16.552,13.007Zm.143,3.653h3.9V31.366h-3.9Z" transform="translate(-1.154 -3.671)" fill="#f2be1d" />
            </svg>  
              <b style="margin-left:50px;">Data Privacy Agreement & Client Consent Declaration</b>           

          </strong>
          <i class="fas fa-angle-right"></i>
        </button>
        <div class="accordion-body active">
          <div class="mb-3">
            <div id="dataPrivacyFormBody" class="tab-pane show active fade">
              <div class="container my-3">
                <div class="row">
                  <div class="col-sm-10 offset-sm-1" align="left">
                    <input type="checkbox" id="dataPrivacy1Checkbox" class="form-check-input ml-1" runat="server" />
                    <label class="form-check-label ml-4" for="dataPrivacyCheckbox">I hereby acknowledge that I have read, understand, and agree to the <a href="https://www.cebuanalhuillier.com/privacypolicy/" target="_blank">Data Privacy Policy</a> of Cebuana Lhuillier. </label>
                    <br />
                  </div>
                </div>
                <div class="row">
                  <div class="col-sm-10 offset-sm-1" align="left">
                    <input type="checkbox" id="dataPrivacy2Checkbox" class="form-check-input ml-1" runat="server" />
                    <label class="form-check-label ml-4" for="dataPrivacyCheckbox">I hereby acknowledge that I have read, understand, and agree to the <a href="<%=Server.HtmlEncode(Session["TermsAndConditions"].ToString())%>" target="_blank">Terms and Conditions </a> relating to insurance coverage and payment of my services. </label>
                    <br />
                  </div>
                </div>
              </div>
            </div>
          </div>
          <div id="submitFormBody">
            <div class="row">
              <div class="col-sm-10 offset-sm-1" align="center">
                <div id="main">
                  <br />
                  <br />
                  <asp:Image ID="captchaImage" runat="server" Style="width:300px;"/>
                  <br />
                  <asp:LinkButton ID="generateNewCaptcha" runat="server" onclick="generateNewCaptcha_Click" Font-Size="Small" Font-Italic="true">Generate new code</asp:LinkButton>
                  <br />
                  <div class="col-md-4 mb-3">
                    <input type="text" class="form-control" id="captchaText" placeholder="Please answer Captcha Text" runat="server" style="text-align:center;" />
                  </div>
                </div>
                <div class="col-md-12 mb-3">
                      <%--<button runat="server" type="button" class="button button-iqreqr" id="Button1" name="submitBtn" text="Submit" OnClientClick="this.disabled='true';" UseSubmitBehavior="false">SELECT PAYMENT METHOD</button>--%>

                      <button runat="server" type="button" class="button button-iqreqr" id="submitBtn" name="submitBtn" text="Submit" OnClientClick="this.disabled='true';" UseSubmitBehavior="false">SELECT PAYMENT METHOD</button>
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
       <% if (Session["ValidateSuccessfully"].ToString() == "True") { %>
       <div class="overlay overlayActive" id="overlay"></div>
       <div class="popup popupactive" id="popup">
          <div class="popup-title-container">
          </div>
           <center>
                     <div class="grid-wrapper grid-col-auto">
              <label for="radio-card-1" class="radio-card">
                <input type="radio" name="payment-option" id="radio-card-1" value="Cebuana Lhuillier Branch" checked />
                <div class="card-content-wrapper">
                  <span class="check-icon"></span>
                  <div class="card-content text-center">
                    <img src="Images/payments/cebuana-logo.png" class="img-fluid" />
                    <h5 class="payment-title">Cebuana Lhuillier Branch</h5>
                  </div>
                </div>
              </label>
              <!-- /.radio-card -->
              <label for="radio-card-2" class="radio-card">
                <input type="radio" name="payment-option" id="radio-card-2" value="Online Payment" />
                <div class="card-content-wrapper">
                  <span class="check-icon"></span>
                  <div class="card-content text-center">
                    <img src="Images/payments/OnlinePayment.png" class="img-fluid" />
                    <h4 class="payment-title">Online Payment</h4>
                  </div>
                </div>
              </label>

              <!-- /.radio-card -->
            </div>
           </center>
     

        <div class="popup-buttons">
              <button runat="server" type="button" class="popup-button proceed btn-payment-method" id="btnPaymentMethod">Proceed</button>
              <button runat="server" type="button" class="popup-button goBack">Go Back</button>
        </div>
        </div>
       <%} %>
       <%if (Session["PaymentMethod"].ToString() == "CL Branch" || Session["PaymentMethod"].ToString() == "GCASH" || Session["PaymentMethod"].ToString() == "PAYMAYA" || Session["PaymentMethod"].ToString() == "GRABPAY" || Session["PaymentMethod"].ToString() == "DD_BPI" || Session["PaymentMethod"].ToString() == "DD_UBP" || Session["PaymentMethod"].ToString() == "CREDIT_CARD") {%>
        <div class="overlay overlayActive-display-summary" id="overlay-display-preview"></div>
           <div class="popup-display-preview popupactive-display-summary" id="popup-display-summary">
             <div class="popup-title-container">
                 <br />
                 <h4><asp:Label ID="SummaryProductName" runat="server" Text=""></asp:Label></h4>
             </div>
              <div class="details">
               <table style="width:100%;">
                  <tr>
                     <td>Premium</td>
                     <td style="text-align: right;"><asp:Label ID="SummaryPremium" runat="server" Text=""></asp:Label></td>
                  </tr>
                   <tr>
                     <td class="column-left-50"><b>Count of COC</b></td>
                     <td class="column-right-50">
                         <div class="quantity">
                           <asp:Button ID="btnMinus" runat="server" CssClass="btn btn-quantity" Text="-" OnClick="btnMinus_Click" />
                               <asp:TextBox ID="numberInput" runat="server" Text="1" CssClass="numberInput" Enabled="false"></asp:TextBox>
                            <asp:Button ID="btnPlus" runat="server" CssClass="btn btn-quantity" Text="+" OnClick="btnPlus_Click" />
                        </div>
                     </td>
                  </tr>
                  <tr>
                     <td style="font-size:14px;" colspan="2">Note: You can avail up to a maximum of <b><asp:Label ID="lblAvailableCOCs" runat="server" Text=""></asp:Label></b> COC's</td>
                  </tr>
                   <tr>
                     <td colspan="2"><hr></td>
                  </tr>
                   <tr>
                       <td>Total Premium</td>
                       <td style="text-align: right;"> <asp:Label ID="SummaryTotalPremium" runat="server" Text=""></asp:Label></td>
                   </tr>
                  <tr>
                     <td>Convenience Fee <b><asp:Label ID="lblPaymentChannel" runat="server" Text=""></asp:Label></b></td>
                     <td style="text-align: right;"><asp:Label ID="SummaryConvinienceFee" runat="server" Text=""></asp:Label></td>
                  </tr>
                  <tr>
                     <td colspan="2"><hr></td>
                  </tr>
                  <tr>
                     <td><b>Total Amount Due</b></td>
                     <td style="text-align: right;"><b><asp:Label ID="SummaryTotalAmount" runat="server" Text=""></asp:Label></b></td>
                  </tr>
                   <tr>
                     <td colspan="2"><hr></td>
                  </tr>
                   <tr>
                     <td colspan="2">
                         <center>
                         <p style="font-size:14px;"><input type="checkbox" id="confirmpayment">
                             <%if (Session["PaymentMethod"].ToString() == "CL Branch")
                                 { %>
                               I hereby verify and confirm that the information provided is accurate and complete
                             <%} %>
                             <%else { %>
                                 I hereby verify and confirm that the information provided is accurate and complete before proceeding with the purchase.
                             <%} %>
                         </p>
                         </center>

                     </td>
                  </tr>
               </table>
            </div>  
        <div class="popup-buttons">
              <button runat="server" type="submit" class="popup-button btn-finalstep proceed" id="gotoFinalStep" disabled>Proceed</button>
              <button runat="server" type="button" class="popup-button goBackLoad" id="goBackStep1">Go Back</button>
        </div>
       </div>
        <%} %>
       <% if (Session["PaymentMethod"].ToString() == "CLDigital") { %>
            <div class="overlay overlayActive-paymenthmethod" id="overlay-paymentmethod"></div>
           <div class="popup-paymentmethod popupactive-paymentmethod" id="popup-paymentmethod">

               <section class="policy-section">
	            <div class="policy-steps">
		            <div class="row">
			            <div class="col-12 text-center policy-header">
				            <h3><b>Choose Payment Method</b></h3>
			            </div>
		            </div>
		            <div class="row mt-2rem">
			            <div class="col-12">
				            <label class="label-font">eWallet</label>
			            </div>
			            <div class="col-12">
				            <div class="eWallet-box">
					            <div class="form-check">
						            <input class="payment-gateway" type="radio" id="flexRadioDefault1" value="GCASH" name="payment.payment_method">
						            <label class="form-check-label payment-gateway-label" for="flexRadioDefault1">GCash <span class="float-right"><img src="Images/payments/gcash.png" class="payment-method-image" /></span></label>
					            </div>
				            </div>
				            <div class="eWallet-box">
					            <div class="form-check">
						            <input class="payment-gateway" type="radio" id="flexRadioDefault2" value="PAYMAYA" name="payment.payment_method">
						            <label class="form-check-label payment-gateway-label" for="flexRadioDefault2">PayMaya <span class="float-right"><img src="Images/payments/paymaya.png" class="payment-method-image" /></span></label>
					            </div>
				            </div>
				            <div class="eWallet-box">
					            <div class="form-check">
						            <input class="payment-gateway" type="radio"  id="flexRadioDefault3" value="GRABPAY" name="payment.payment_method">
						            <label class="form-check-label payment-gateway-label" for="flexRadioDefault3">GrabPay <span class="float-right"><img src="Images/payments/grabpay.png" class="payment-method-image" /></span></label>
					            </div>
				            </div>
			            </div>
		            </div>
		            <div class="row mt-2rem">
			            <div class="col-12">
				            <div class="form-check" style="margin-top:10px;">
					            <input class="payment-gateway" type="radio" id="flexRadioDefault4" value="CREDIT_CARD" name="payment.payment_method">
					            <label class="form-check-label payment-gateway-label" for="flexRadioDefault4">
						            Credit / Debit Card 
						            <span class="payment-gateway-sub">Mastercard & Visa card upon checkout.</span>
					            </label>
				            </div>
			            </div>
		            </div>
		            <div class="row mt-2rem">
			            <div class="col-12">
				            <label class="label-font">Bank Transfer Direct Debit</label>
			            </div>
			            <div class="col-12">
				            <div class="eWallet-box">
					            <div class="form-check">
						            <input class="payment-gateway" type="radio" id="flexRadioDefault5" value="DD_BPI" name="payment.payment_method">
						            <label class="form-check-label payment-gateway-label" for="flexRadioDefault5">BPI <span class="float-right"><img src="Images/payments/bpi.png" class="payment-method-image" /></span></label>
					            </div>
				            </div>
				            <div class="eWallet-box">
					            <div class="form-check">
						            <input class="payment-gateway" type="radio" id="flexRadioDefault6" value="DD_UBP" name="payment.payment_method">
						            <label class="form-check-label payment-gateway-label" for="flexRadioDefault6">Unionbank <span class="float-right"><img src="Images/payments/unionbank.png" class="payment-method-image" /></span></label>
					            </div>
				            </div>
			            </div>
		            </div>
	            </div>
               </section>
            <div class="popup-buttons">
                  <button runat="server" type="submit" class="popup-button btn-finalstep proceed" id="btnGotoPaymentSummary">Proceed</button>
                  <button runat="server" type="button" class="popup-button goBackLoad" id="btnGotoStepPamentOptions">Go Back</button>
            </div>
            </div>
       <%} %>
    <hr />
    <footer class="pb-3">
      <center>
        <p>© 2020 - Cebuana Lhuillier</p>
      </center>
    </footer>
  </form>
</asp:Content>
<asp:Content ContentPlaceHolderID="ScriptContent" runat="server">
    <script src="JScript/Plugins/Datepicker/datetimepicker_ajax_libs_jquery_2.1.3_jquery.min.js"></script>
    <script src="JScript/Plugins/Datepicker/datetimepicker_ajax_libs_jqueryui_1.11.2_jquery-ui.min.js"></script>
    <script src="JScript/Plugins/Datepicker/datetimepicker_jquery.validation_1.14.0_jquery.validate.min.js"></script>
    <script src="JScript/Plugins/Datepicker/datetimepicker_ajax_libs_jquery-validate_1.19.5_additional-methods.min.js"></script>
 <script>
var categoryid = '<%= Session["CategoryId"] %>';
// PET INSURANCE JS
if (categoryid == "8")
     {
         function toggleTextarea(radioButtonId, containerId, detailsTextboxId) {
             var radio = document.getElementById(radioButtonId);
             var container = document.getElementById(containerId);
             var detailsTextbox = document.getElementById(detailsTextboxId);

             if (radio && container && detailsTextbox) {
                 var selectedValue = '';
                 var radioInputs = radio.getElementsByTagName('input');

                 for (var i = 0; i < radioInputs.length; i++) {
                     if (radioInputs[i].checked) {
                         selectedValue = radioInputs[i].value;
                         break;
                     }
                 }

                 if (selectedValue === "Yes") {
                     container.style.display = "block";
                 } else {
                     container.style.display = "none";
                     // Clear the value of the details textbox
                     detailsTextbox.value = "";
                 }
             }
         }

       // Attach the function to the onchange event of the radio buttons for each group
     function attachChangeEventListeners() {
             document.getElementById('<%= fld_PhysicalDeformity.ClientID %>').addEventListener('change', function () {
                 toggleTextarea('<%= fld_PhysicalDeformity.ClientID %>', 'textareaContainer_fld_PhysicalDeformity', '<%= fld_PhysicalDeformityDetails.ClientID %>');
            });
            document.getElementById('<%= fld_PreExistingIllness.ClientID %>').addEventListener('change', function () {
                toggleTextarea('<%= fld_PreExistingIllness.ClientID %>', 'textareaContainer_fld_PreExistingIllness', '<%= fld_PreExistingIllnessDetails.ClientID %>');
            });
            document.getElementById('<%= fld_PetYearlyTreatment.ClientID %>').addEventListener('change', function () {
                toggleTextarea('<%= fld_PetYearlyTreatment.ClientID %>', 'textareaContainer_fld_PetYearlyTreatment', '<%= fld_PetYearlyTreatmentDetails.ClientID %>');
            });
            document.getElementById('<%= fld_PetHistory.ClientID %>').addEventListener('change', function () {
                toggleTextarea('<%= fld_PetHistory.ClientID %>', 'textareaContainer_fld_PetHistory', '<%= fld_PetHistoryDetails.ClientID %>');
            });
            document.getElementById('<%= fld_PetVitamins.ClientID %>').addEventListener('change', function () {
                toggleTextarea('<%= fld_PetVitamins.ClientID %>', 'textareaContainer_fld_PetVitamins', '<%= fld_PetVitaminsDetails.ClientID %>');
            });
       }

       // Function to handle postback events
       function handlePostback() {
         // Reapply event handlers and update textarea visibility after postback
         attachChangeEventListeners();
           toggleTextarea('<%= fld_PhysicalDeformity.ClientID %>', 'textareaContainer_fld_PhysicalDeformity', '<%= fld_PhysicalDeformityDetails.ClientID %>');
         toggleTextarea('<%= fld_PreExistingIllness.ClientID %>', 'textareaContainer_fld_PreExistingIllness', '<%= fld_PreExistingIllnessDetails.ClientID %>');
           toggleTextarea('<%= fld_PetYearlyTreatment.ClientID %>', 'textareaContainer_fld_PetYearlyTreatment', '<%= fld_PetYearlyTreatmentDetails.ClientID %>');
           toggleTextarea('<%= fld_PetHistory.ClientID %>', 'textareaContainer_fld_PetHistory', '<%= fld_PetHistoryDetails.ClientID %>');
         toggleTextarea('<%= fld_PetVitamins.ClientID %>', 'textareaContainer_fld_PetVitamins', '<%= fld_PetVitaminsDetails.ClientID %>');
     }
       
       // Call the function to attach event listeners on DOMContentLoaded
       document.addEventListener("DOMContentLoaded", function () {
         attachChangeEventListeners();
           toggleTextarea('<%= fld_PhysicalDeformity.ClientID %>', 'textareaContainer_fld_PhysicalDeformity', '<%= fld_PhysicalDeformityDetails.ClientID %>');
           toggleTextarea('<%= fld_PreExistingIllness.ClientID %>', 'textareaContainer_fld_PreExistingIllness', '<%= fld_PreExistingIllnessDetails.ClientID %>');
           toggleTextarea('<%= fld_PetYearlyTreatment.ClientID %>', 'textareaContainer_fld_PetYearlyTreatment', '<%= fld_PetYearlyTreatmentDetails.ClientID %>');
           toggleTextarea('<%= fld_PetHistory.ClientID %>', 'textareaContainer_fld_PetHistory', '<%= fld_PetHistoryDetails.ClientID %>');
           toggleTextarea('<%= fld_PetVitamins.ClientID %>', 'textareaContainer_fld_PetVitamins', '<%= fld_PetVitaminsDetails.ClientID %>');
     });
       
       // Attach a postback event handler using Sys.WebForms.PageRequestManager
       if (typeof Sys !== 'undefined' && Sys.WebForms && Sys.WebForms.PageRequestManager) {
           Sys.WebForms.PageRequestManager.getInstance().add_endRequest(handlePostback);
         }

        $(function () {
            var dtToday = new Date();

            var month = dtToday.getMonth() + 1;
            var day = dtToday.getDate();
            var year = dtToday.getFullYear();
            if (month < 10)
                month = '0' + month.toString();
            if (day < 10)
                day = '0' + day.toString();

            var maxDate = year + '-' + month + '-' + day;
            $('.disableFuturedate').attr('max', maxDate);
        });

    function ValidateDOBPET() {
        var val = document.getElementById("<%=fld_PetBirthdate.ClientID%>").value;
            var parts = val.split('-'); // Split the date string by '-'

            // Construct a date object using components
            var birthDate = new Date(parts[0], parts[1] - 1, parts[2]);

            var now = new Date();

            // Check if the birth date is in the future
            if (now < birthDate) {
                Swal.fire('Pet Birthdate cannot be in the future.');
                document.getElementById("<%=fld_PetBirthdate.ClientID%>").value = "";
        return false;
    }

    // Check if the year is less than 1900
    if (birthDate.getFullYear() < 1900) {
        Swal.fire('Please enter a valid pet birthdate.');
            document.getElementById("<%=fld_PetBirthdate.ClientID%>").value = "";
            return false;
        }

        return true; // Date is valid
    }

    function checkLength(textBox) {
        var maxLength = 200; // Change this to your desired maximum length
        if (textBox.value.length > maxLength) {
            textBox.value = textBox.value.substring(0, maxLength);

        } else {
  
        }
    }

    function ValidatePetName() {
        var petnametextbox = document.getElementById("<%=fld_PetName.ClientID%>").value;
        var petnameformat = /^[A-Za-z0-9\-' ]*$/;
        if (petnametextbox.match(petnameformat)) {
        console.log("VALID GUARDIAN FULL NAME");
        return true;
    }
    else
    {
        Swal.fire('You have entered invalid information. Please try again.');
                document.getElementById("<%=fld_PetName.ClientID%>").value = "";
                return false;
            }
        }

     }
// FAMILY INSURANCE JS
if (categoryid == "10")
     {
         //-------------------------------- ANIMATION FOR DEPENDENT --------------------///
         function applyGlowAnimation() {
             var dropdown = document.getElementById('<%= DD_TypeOfDependents.ClientID %>');

          // Add the class to apply the infinite glow animation
          dropdown.classList.add("dropdown-glow");
         }
         document.addEventListener("DOMContentLoaded", function () {
             var dropdown = document.getElementById('<%= DD_TypeOfDependents.ClientID %>');
             dropdown.classList.remove("dropdown-glow");
         });

         //-------------------------------- VALIDATE DEPENDENT BIRTHDAY --------------------///

         function validateDate(input) {
             var dateValue = input.value;
             var datePattern = /^\d{2}\/\d{2}\/\d{4}$/;

             if (!datePattern.test(dateValue)) {
                 Swal.fire('Please enter a date in MM/DD/YYYY format.');
                 input.value = ""; // Clear the input field
                 return;
             }

             var parts = dateValue.split("/");
             var month = parseInt(parts[0], 10);
             var day = parseInt(parts[1], 10);
             var year = parseInt(parts[2], 10);

             var currentDate = new Date();
             var minDate = new Date("01/01/1900");
             var maxDate = currentDate;

             if (isNaN(month) || isNaN(day) || isNaN(year) || month < 1 || month > 12 || day < 1 || day > 31) {
                 Swal.fire('Invalid date.');

                 input.value = ""; // Clear the input field
                 return;
             }

             var enteredDate = new Date(month + "/" + day + "/" + year);

             if (enteredDate < minDate || enteredDate > maxDate) {
                 Swal.fire("Date must be between 01/01/1900 and " + (currentDate.getMonth() + 1) + "/" + currentDate.getDate() + "/" + currentDate.getFullYear() + ".");
                 input.value = ""; // Clear the input field
             }
         }

         $(document).on('click', '[data-datepicker="true"]', function () {
             // Initialize datepicker for the clicked input element
             var currentDate = new Date();
             var currentYear = currentDate.getFullYear();
             var yearRange = "1900:" + currentYear; // Dynamic year range

             $(this).datepicker({
                 yearRange: yearRange,
                 changeYear: true,
                 changeMonth: true, // Enable month dropdown
                 dateFormat: "mm/dd/yy",
                 minDate: new Date(1900, 0, 1),
                 maxDate: currentDate,
                 showButtonPanel: false // Disable next and previous buttons
             });

             // Trigger the datepicker to show immediately
             $(this).datepicker('show');
         });

     }
// TRAVEL INSURANCE JS
if (categoryid == "11")
     {
         //---------------------------TRAVEL SELECTION PURPOSE OF TRAVEL--------------------------------------//
         $(document).ready(function () {
             // Function to initialize the selection state
             function initializeSelectionState() {
                 var storedPlan = localStorage.getItem('selectedPlan');

                 if (storedPlan) {
                     // Select the stored plan
                     $('.plan input[value="' + storedPlan + '"]').prop('checked', true);
                     $('.plan input[value="' + storedPlan + '"]').parent().addClass('selected');
                 }
             }

             // Handle the click event for the plan
             function attachClickHandler() {
                 $('.plans .plan').on('click', function () {
                     selectPlan(this);
                 });
             }

             // Function to select a plan
             function selectPlan(plan) {
                 // Remove the 'selected' class from all plans
                 $('.plans .plan').removeClass('selected');

                 // Add the 'selected' class to the clicked plan
                 $(plan).addClass('selected');

                 // Update the corresponding radio button
                 var radioInput = $(plan).find('input[type="radio"]');
                 radioInput.prop('checked', true);

                 // Store the selected plan in local storage
                 localStorage.setItem('selectedPlan', radioInput.val());


             }

             // Initialize selection state on page load
             initializeSelectionState();

             // Attach click event handler
             attachClickHandler();

             // Check if there's a postback (assuming you're using ASP.NET WebForms)
             if (typeof Sys !== 'undefined') {
                 var prm = Sys.WebForms.PageRequestManager.getInstance();

                 prm.add_endRequest(function () {
                     // Reapply event handlers and update selection state after the postback
                     initializeSelectionState();
                     attachClickHandler();
                 });
             }
         });

         //-------------------------------- TRAVEL DURATION FROM --------------------/
         $(document).on('click', '[data-datepicker="true"]', function () {
             var currentDate = new Date();

             var selectedStartDateField = $("#selectedStartDate");
             var selectedEndDateField = $("#selectedEndDate");
             var durationDaysField = $("#durationDays");

             $("#<%=travelFrom.ClientID%>").datepicker({
             minDate: currentDate,
             onSelect: function (selectedDate) {
                 selectedStartDateField.val(selectedDate);
                 $("#<%=travelTo.ClientID%>").datepicker("option", "minDate", selectedDate);

            var maxDate = new Date(selectedDate);
            maxDate.setDate(maxDate.getDate() + 59);
            $("#<%=travelTo.ClientID%>").datepicker("option", "maxDate", maxDate);

            $("#<%=travelTo.ClientID%>").datepicker("option", "disabled", false);
                 calculateDays();
             },
             showButtonPanel: false,
             changeMonth: true,
             changeYear: true
         });

         $("#<%=travelTo.ClientID%>").datepicker({
             beforeShow: function (input, inst) {
                 if ($("#<%=travelFrom.ClientID%>").val() === "") {
                $("#<%=travelFrom.ClientID%>").val("");
                $("#<%=numberOfDays.ClientID%>").val("");
                $("#<%=travelTo.ClientID%>").val("");
                $("#<%=travelFrom.ClientID%>").focus();
                Swal.fire('Please select Travel Duration From first.');
                return false;
            }
        },
        onSelect: function (selectedDate) {
            selectedEndDateField.val(selectedDate);
            calculateDays();
        },
        showButtonPanel: false,
        changeMonth: true,
        changeYear: true,
        disabled: true
    });

         function calculateDays() {
             var startDate = $("#<%=travelFrom.ClientID%>").datepicker("getDate");
        var endDate = $("#<%=travelTo.ClientID%>").datepicker("getDate");

        if (startDate && endDate) {
   
            var timezoneOffset = endDate.getTimezoneOffset() - startDate.getTimezoneOffset();
            var timeDiff = Math.abs(endDate.getTime() - startDate.getTime() - timezoneOffset * 60000);
            var diffDays = Math.floor(timeDiff / (1000 * 3600 * 24));

            // Add 1 day to include both the start and end dates
            diffDays = diffDays >= 0 ? diffDays + 1 : 0;

            if (diffDays > 60) {
                // If the duration exceeds 60 days, adjust the end date
                endDate.setDate(startDate.getDate() + 60 - 1); // Set end date to start date + 59 days
                $("#<%=travelTo.ClientID%>").datepicker("setDate", endDate);
            }

            $("#<%=numberOfDays.ClientID%>").val(diffDays); // Update visible duration input if needed
        }
    }

    // Event listener for change in "number of days" field
    $("#<%=numberOfDays.ClientID%>").on('input', function () {
        var numberOfDaysValue = $(this).val();
        if (numberOfDaysValue === "") {
            // Clear "Travel Duration From" and "Travel Duration To" datepickers
            $("#<%=travelFrom.ClientID%>").val("");
            $("#<%=travelTo.ClientID%>").val("");
        }
    });

    // Reinitialize datepickers and set values after postback
    Sys.WebForms.PageRequestManager.getInstance().add_endRequest(function () {
        var storedStartDate = selectedStartDateField.val();
        var storedEndDate = selectedEndDateField.val();

        // Restore values using ViewState
        $("#<%=travelFrom.ClientID%>").val(storedStartDate);
        $("#<%=travelTo.ClientID%>").val(storedEndDate);

        $("#<%=travelFrom.ClientID%>").datepicker({
            minDate: currentDate,
            defaultDate: storedStartDate,
            onSelect: function (selectedDate) {
                selectedStartDateField.val(selectedDate);
                $("#<%=travelTo.ClientID%>").datepicker("option", "minDate", selectedDate);

                // Disable dates beyond 60 days from the selected start date
                var maxDate = new Date(selectedDate);
                maxDate.setDate(maxDate.getDate() + 59); // Set max date to selected date + 59 days
                $("#<%=travelTo.ClientID%>").datepicker("option", "maxDate", maxDate);

                $("#<%=travelTo.ClientID%>").datepicker("option", "disabled", false);
                calculateDays();
            },
            showButtonPanel: false,
            changeMonth: true,
            changeYear: true
        });

        $("#<%=travelTo.ClientID%>").datepicker({
            defaultDate: storedEndDate,
            onSelect: function (selectedDate) {
                selectedEndDateField.val(selectedDate);
                calculateDays();
            },
            showButtonPanel: false,
            changeMonth: true,
            changeYear: true,
            disabled: storedStartDate === ""
        });

        // Set the calculated days
        calculateDays();
    });
     });
 }
// PROPERTY INSURANCE JS
if (categoryid == "6") {
function addFixedDecimalFloorArea() {
        var textBox = document.getElementById('<%= fld_FloorArea.ClientID %>');
        var value = textBox.value.trim(); // Trim any leading/trailing spaces

        // If the value is empty or already has a decimal part, do nothing
        if (value === '' || value.indexOf('.') !== -1) {
            return;
        }

        // Add .00 to the value
        textBox.value = value + '.00';
    }
    function addFixedDecimalBodyofWater() {
        var textBox = document.getElementById('<%= fld_BodyOfWaterDistance.ClientID %>');
        var value = textBox.value.trim(); // Trim any leading/trailing spaces

        // If the value is empty or already has a decimal part, do nothing
        if (value === '' || value.indexOf('.') !== -1) {
            return;
        }

        // Add .00 to the value
        textBox.value = value + '.00';
    }
function BodyOfWatervalidNumericWithDecimal(event) {
    var charCode = (event.which) ? event.which : event.keyCode;
        var textBox = document.getElementById('<%= fld_BodyOfWaterDistance.ClientID %>');
        var value = textBox.value.trim();
        var decimalIndex = value.indexOf('.');

        // Check if it's a valid numeric character or decimal point
        if ((charCode >= 48 && charCode <= 57) || charCode === 46) {
            // Check if there's already a decimal point
            if (charCode === 46 && decimalIndex !== -1) {
                // Prevent typing additional decimal points
                event.preventDefault();
            }
            // Allow editing of decimal part if it exists
            else if (decimalIndex !== -1 && value.substring(decimalIndex + 1).length >= 2) {
                // Prevent typing additional characters in the decimal part
                event.preventDefault();
            }
            // Check if the input exceeds 7 digits before the decimal point
            else if (decimalIndex === -1 && value.replace('.', '').length >= 7) {
                // Prevent typing additional digits
                event.preventDefault();
            }
            // Check if the input exceeds 10 characters in total
            else if (value.length >= 10 && decimalIndex === -1) {
                // Prevent typing additional characters
                event.preventDefault();
            }
            return true; // Allow valid input
        } else if (charCode === 8 || charCode === 46 || charCode === 37 || charCode === 39) {
            // Allow backspace (charCode 8), delete (charCode 46), left arrow (charCode 37), and right arrow (charCode 39) keys
            return true;
        } else {
            // Prevent typing non-numeric characters
            event.preventDefault();
        }
    }
    function FloorAreavalidNumericWithDecimal(event) {
        var charCode = (event.which) ? event.which : event.keyCode;
        var textBox = document.getElementById('<%= fld_FloorArea.ClientID %>');
        var value = textBox.value.trim();
        var decimalIndex = value.indexOf('.');

        // Check if it's a valid numeric character or decimal point
        if ((charCode >= 48 && charCode <= 57) || charCode === 46) {
            // Check if there's already a decimal point
            if (charCode === 46 && decimalIndex !== -1) {
                // Prevent typing additional decimal points
                event.preventDefault();
            }
            // Allow editing of decimal part if it exists
            else if (decimalIndex !== -1 && value.substring(decimalIndex + 1).length >= 2) {
                // Prevent typing additional characters in the decimal part
                event.preventDefault();
            }
            // Check if the input exceeds 7 digits before the decimal point
            else if (decimalIndex === -1 && value.replace('.', '').length >= 7) {
                // Prevent typing additional digits
                event.preventDefault();
            }
            // Check if the input exceeds 10 characters in total
            else if (value.length >= 10 && decimalIndex === -1) {
                // Prevent typing additional characters
                event.preventDefault();
            }
            return true; // Allow valid input
        } else if (charCode === 8 || charCode === 46 || charCode === 37 || charCode === 39) {
            // Allow backspace (charCode 8), delete (charCode 46), left arrow (charCode 37), and right arrow (charCode 39) keys
            return true;
        } else {
            // Prevent typing non-numeric characters
            event.preventDefault();
        }
    }
}

//------------------------ PAYMENT METHOD ------------------------------------------------------------//
document.addEventListener('DOMContentLoaded', function () {
         var checkbox = document.getElementById('confirmpayment');
         var proceedButton = document.getElementById('<%= gotoFinalStep.ClientID %>');

         proceedButton.classList.add('disabled-button');

              checkbox.addEventListener('change', function () {
                  if (this.checked) {
                      proceedButton.removeAttribute('disabled');
                      proceedButton.classList.remove('disabled-button');
                  } else {
                      proceedButton.setAttribute('disabled', 'disabled');
                      proceedButton.classList.add('disabled-button');
                  }
              });
          });

//-------------------------------- KEY UP FOR CONTACT NUMBER 09 --------------------///
$(document).ready(function () {
$("#<%=contactNumber.ClientID%>").on("keyup", function () {
    var value = $(this).val();
    $(this).val($(this).data("initial") + value.substring(2));
});
});
//-------------------------------- PREVENT FOR INSPECT ELEMENT AND FUNCTION F12 --------------------///
     $(document).bind("contextmenu", function (e) {
         e.preventDefault();
     });
     $(document).keydown(function (e) {
         if (e.which === 123) {
             return false;
         }
     });

     document.addEventListener('contextmenu', event => event.preventDefault());
     document.onkeydown = function (e) {
         if (event.keyCode == 123) {
             return false;
         }
         if (e.ctrlKey && e.shiftKey && e.keyCode == 'I'.charCodeAt(0)) {
             return false;
         }
         if (e.ctrlKey && e.shiftKey && e.keyCode == 'J'.charCodeAt(0)) {
             return false;
         }
         if (e.ctrlKey && e.keyCode == 'U'.charCodeAt(0)) {
             return false;
         }
     }

     localStorage.openpages = Date.now();
     window.addEventListener('storage', function (e) {
         if (e.key == "openpages") {
             // Listen if anybody else is opening the same page!
             localStorage.page_available = Date.now();
         }
         if (e.key == "page_available") {
             Swal.fire({
                 text: "The CLIB Microsite is open in another window. Click 'Ok' to close the window.",
                 icon: 'warning',
                 showCancelButton: false,
                 allowOutsideClick: false,
                 confirmButtonColor: '#3085d6',
                 cancelButtonColor: '#d33',
                 confirmButtonText: 'Ok'
             }).then((result) => {
                 if (result.isConfirmed) {
                     redirection();
                 }
             })
         }
     }, false);

     function redirection() {
         // Get the value of the query string parameter "PART"
         const urlParams = new URLSearchParams(window.location.search);
         const partParam = urlParams.get('PART');

         // Define the base URL
         let baseUrl = 'ProductRegistration.aspx';

         // Check if the PART parameter is not null or empty
         if (partParam) {
             // Construct the URL with the PART parameter
             baseUrl += `?PART=${partParam}`;
         }

         // Redirect to the constructed URL
         window.location.href = baseUrl;
     }

//-------------------------------- VALIDATE BENEFICIARY FULL NAME --------------------///

function ValidateBenefeciaryFullName() {
var benefeciaryfullnametextbox = document.getElementById("<%=beneFullname.ClientID%>").value;
    var benefeciaryfullnameformat = /^[A-Za-z0-9\-' ]*$/;

if (benefeciaryfullnametextbox.match(benefeciaryfullnameformat)) {
    console.log("VALID BENEFECIARY FULL NAME");
    return true;
}
else
{
    Swal.fire('You have entered invalid information. Please try again.');
    document.getElementById("<%=beneFullname.ClientID%>").value = "";
    return false;
}
}

//-------------------------------- VALIDATE SUFFIX--------------------///
function validateSuffix(event) {
    // Get the character code of the pressed key
    var charCode = event.which || event.keyCode;

    // Convert the character code to its corresponding string representation
    var charStr = String.fromCharCode(charCode);

    // Test if the character is a letter, number, period, comma, or space
    return /[a-zA-Z0-9., ]/.test(charStr);
}

//-------------------------------- VALIDATE GUARDIAN FULL NAME --------------------///

function ValidateGuardianFullName() {
var guardianfullnametextbox = document.getElementById("<%=guardianFullName.ClientID%>").value;
    var guardianfullnameformat = /^[A-Za-z0-9\-' ]*$/;
if (guardianfullnametextbox.match(guardianfullnameformat)) {
    console.log("VALID GUARDIAN FULL NAME");
    return true;
}
else
{
    Swal.fire('You have entered invalid information. Please try again.');
    document.getElementById("<%=guardianFullName.ClientID%>").value = "";
    return false;
}
}

//-------------------------------- VALIDATE FULL NAME RESIDENT --------------------///
function ValidateFullNameofResident() {
    var nametextbox = document.getElementById("<%=fld_NameOfResident.ClientID%>").value;
    var nameformat = /^[A-Za-z0-9\-' ]*$/;
if (nametextbox.match(nameformat)) {
    console.log("VALID FULL NAME OF RESIDENT");
    return true;
} else {
    Swal.fire('You have entered invalid information. Please try again.');
        document.getElementById("<%=fld_NameOfResident.ClientID%>").value = "";
        return false;
    }
}

//-------------------------------- VALIDATE CONTACT NUMBER --------------------///
function validateContactNumber() {
    var phone = document.getElementById("<%=contactNumber.ClientID%>").value;
    var re = /^(09|\+639)\d{11}$/;
    var contanctnumberlenght = phone.replace(/[^0-9]/g, "").length;
    if (phone.match(re)) {
        console.log("VALID ID NUMBER");
        return true;
    }
    if (contanctnumberlenght < 11)
    {
       Swal.fire('Please enter a valid PH Number');
        document.getElementById("<%=contactNumber.ClientID%>").value = "09";
        return true;
    }
    else {
        console.log("VALID PH NUMBER");
    }

}
//-------------------------------- PREVENT SPECIAL CHARACTERS --------------------///
function preventSpecialChars(event) {
    var charCode = (event.which) ? event.which : event.keyCode;

    if (typeof event.touches !== "undefined" && event.touches.length > 0) {
        // Mobile device: Touch event
        var key = String.fromCharCode(charCode);
        if (/[a-zA-Z0-9 ]/.test(key)) {
            return true; // Allow alphabets (A-Z and a-z), numbers (0-9), and spaces
        } else {
            event.preventDefault();
            return false; // Prevent the input of special characters
        }
    } else {
        // Desktop device: Key press event
        if ((charCode >= 65 && charCode <= 90) || (charCode >= 97 && charCode <= 122) || (charCode >= 48 && charCode <= 57) || charCode === 32) {
            return true; // Allow alphabets (A-Z and a-z), numbers (0-9), and spaces
        } else {
            event.preventDefault();
            return false; // Prevent the input of special characters
        }
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
//-------------------------------- VALID DECIMAL --------------------///
     function validNumericWithDecimal() {
         var charCode = (event.which) ? event.which : event.keyCode;

         if ((charCode >= 48 && charCode <= 57) || charCode === 46) {
             // Allow digits (48-57) and the decimal point (46)
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
//-------------------------------- VALID ID NUMBER --------------------///
function ValidateIdNumber() {
    var idnumbertextbox = document.getElementById("<%=idNumber.ClientID%>").value;
    var idnumberformat = /^[a-zA-Z0-9_-]*$]*$/;
    if (idnumbertextbox.match(idnumberformat)) {
        console.log("VALID ID NUMBER");
        return true;
    }
    else
    {
        Swal.fire('You have entered invalid information. Please try again.');
        document.getElementById("<%=idNumber.ClientID%>").value = "";
        return false;
    }
}
//-------------------------------- VALIDATE DOB --------------------///
 function ValidateDOB() {
    var val = document.getElementById("<%=guardianBirthDate.ClientID%>").value;
    var parts = val.split('-'); // Split the date string by '-'

    // Construct a date object using components
    var birthDate = new Date(parts[0], parts[1] - 1, parts[2]);

    var now = new Date();
    var age = now.getFullYear() - birthDate.getFullYear();

    // Check if the birth date is in the future
    if (now < birthDate) {
        Swal.fire('Invalid date. Guardian cannot be born in the future.');
        document.getElementById("<%=guardianBirthDate.ClientID%>").value = "";
        return false;
    }

    // Check if the age is less than 18
    if (age < 18) {
        Swal.fire('Invalid date. Guardian must be 18 years old and above.');
             document.getElementById("<%=guardianBirthDate.ClientID%>").value = "";
             return false;
         }

         return true; // Date is valid
     }


function text_changed(txtObj)
{
    var birthDate = new Date(txtObj.value);
    var birthDateYear = birthDate.getFullYear();
    var dateNow = new Date();
    var dateNowYear = dateNow.getFullYear();
    var age = dateNowYear - birthDateYear;
    var elem = document.getElementById("infoForm_guardianDiv");            

    if (age < 18) {
        elem.style.display = "block";
    }
    else {
        elem.style.display = "none";
        document.getElementById("infoForm_guardianBirthDate").value = "";

    }
}

function character(e) {
    isIE = document.all ? 1 : 0;
    keyEntry = !isIE ? e.which : event.keyCode;
    if (
        (keyEntry >= "65" && keyEntry <= "90") ||
        (keyEntry >= "97" && keyEntry <= "122") ||
        keyEntry == "46" ||
        keyEntry == "32" ||
        keyEntry == "45" ||
        keyEntry == "188"
    ) {
        return true;
    } else {
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
 </script>


</asp:Content> 