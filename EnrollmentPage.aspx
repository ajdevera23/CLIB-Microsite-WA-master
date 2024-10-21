<%@ Page Language="C#" MasterPageFile="~/Enrollment.Master" AutoEventWireup="true" CodeFile="EnrollmentPage.aspx.cs" Inherits="EnrollmentPage2" %> <asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
 <link type="text/css" href="../Style/enrollment.css" rel="stylesheet" />  
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
            <div class="form-group mb-0">
              <div class="row" align="center">
                <div class="col-md-3 mb-3" align="left">
                  <span class="text-required">*</span>First Name: <br />
                <input type="text" id="firstName" class="form-control" onpaste="return false" placeholder="First Name" runat="server" maxlength="75" minlength="1" onkeypress="return characterAndNumbers(event)" onkeydown="return /[A-Za-z0-9-' ]/.test(event.key)" readonly="readonly"/>
                </div>
                <div class="col-md-3 mb-3" align="left">
                  <span class="text-required"></span>Middle Name: <br />
                  <input type="text" id="middleName" class="form-control" onpaste="return false" placeholder="Middle Name" runat="server" maxlength="20" minlength="1" onkeypress="return characterAndNumbers(event)" onkeydown="return /[A-Za-z0-9-' ]/.test(event.key)" readonly="readonly"/>
                </div>
                <div class="col-md-3 mb-3" align="left">
                  <span class="text-required">*</span>Last Name: <br />
                  <input type="text" id="lastName" class="form-control" onpaste="return false" placeholder="Last Name" runat="server" maxlength="20" minlength="1" onkeypress="return characterAndNumbers(event)" onkeydown="return /[A-Za-z0-9-' ]/.test(event.key)" readonly="readonly" />
                </div>
                <div class="col-md-3 mb-3" align="left">
                  <span class="text-required"></span>Suffix: <br />
                  <input type="text" id="suffix" class="form-control" onpaste="return false" placeholder="Suffix" runat="server" onkeypress="return validateSuffix(event)" Maxlength="5" Minlength="1" readonly="readonly" />
                </div>
              </div>
              <div class="row" align="left">
                <div class="col-md-4 mb-3">
                  <span class="text-required">*</span> Birthdate: <br />
                  <asp:TextBox runat="server" class="form-control" onkeypress="return text_changed(this);" onchange="this.onkeypress();" oninput="this.onkeypress();" onpaste="return false" type="date" ID="birthDateTextBox" ReadOnly="true"></asp:TextBox>
                </div>
              </div>
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
					    <span class=" text-required">*</span>Present Address: <br />
               <asp:TextBox type="text" class="form-control" onpaste="return false" placeholder="Room/Floor/Unit Building Name/Street, Barangay, City, Province, Zip Code" id="presentAddress" runat="server" Maxlength="120" Minlength="10"></asp:TextBox>
             </div>
             <div class="col-md-3 mb-3">
               <span class="text-required">*</span>Province: <br />
               <asp:DropDownList ID="DDProvince" class="form-control" runat="server" OnSelectedIndexChanged="DDProvince_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
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
          <div class="row col-md-12">
            <div class="col-md-4 mb-3" align="left">
              <span class="text-required">*</span>Contact No: <br />
              <asp:TextBox type="number" class="form-control" onpaste="return false" id="contactNumber" placeholder="09XXXXXXXXX" runat="server" value='09' data-initial='09' maxlength='11' minlength="11" pattern="[0-9]*" oninput="javascript: if (this.value.length > this.maxLength) this.value = this.value.slice(0, this.maxLength);" onkeypress="return validNumeric()" onchange="javascript:validateContactNumber();"/>
            </div>
            <div class="col-md-8 mb-3" align="left">
              <span class="text-required">*</span>Email: <br />
              <asp:TextBox type="email" class="form-control" onpaste="return false" id="emailAddress" placeholder="juandelacruz@gmail.com" onkeypress="return emailCheck(event)" MaxLength="50" Minlength="5" onchange="javascript:ValidateEmail();" runat="server"></asp:TextBox>
              <i class="text-required" style="font-size:smaller"> Your COC details will be sent here.</i>
            </div>
          </div>
        </div>
      </div>

      <% if( Session["IsExist"].ToString() == "False" || Session["IsExist"].ToString() == "True") { %> 
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
            <div class="col-md-6 mb-3" align="left">
              <span class="text-required">*</span>Source of Funds: <br />
              <asp:DropDownList ID="DDSourceOfFunds" class="form-control" runat="server"></asp:DropDownList>
            </div>
            <div class="col-md-4 mb-3" align="left">
              <span class="text-required">*</span>Valid ID: <br />
              <asp:DropDownList ID="validIdDropDownList" class="form-control" runat="server"></asp:DropDownList>
            </div>
            <div class="col-md-4 mb-3" align="left">
              <span class="text-required">*</span> ID No.: <br />
              <asp:TextBox type="text" class="form-control" placeholder="Enter ID Number" onpaste="return false" onkeypress="return characterAndNumbers(event)" id="idNumber" runat="server" Maxlength="25" Minlength="1" onkeydown="return /^[a-zA-Z0-9_-]*$]*$/i.test(event.key)" onchange="javascript:ValidateIdNumber();" oninput="javascript: if (this.value.length > this.maxLength) this.value = this.value.slice(0, this.maxLength);"></asp:TextBox>
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

     <% if( Session["categoryCode"].ToString() == "PRI") {%> 
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
              <asp:TextBox type="text" id="fld_NameOfResident" class="form-control" onpaste="return false" placeholder="" runat="server"  Maxlength="150" Minlength="1" onkeydown="return /[A-Za-z0-9-' ]/.test(event.key)" onchange="javascript:ValidateFullNameofResident()"></asp:TextBox>
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
              <div class="col-md-8 mb-3" align="left">
                <span class=text-required>*</span>Beneficiary Full Name: <br />
                <asp:TextBox runat="server" class="form-control" onpaste="return false" placeholder="Beneficiary Full Name" ID="beneFullname" Maxlength="150" Minlength="1" onkeypress="return characterAndNumbers(event)" onkeydown="return /[A-Za-z0-9-' ]/.test(event.key)" onchange="javascript:ValidateBenefeciaryFullName();"></asp:TextBox>
              </div>
               <div class="col-md-4 mb-3" align="left">
                <span class="text-required">*</span> Relationship to the Insured: <br />
                <asp:DropDownList runat="server" class="form-control" onpaste="return false" ID="relationshipDropDownList"></asp:DropDownList>
              </div>
            </div>
          </div>
        </div>
      </div>
    
          <% if( Session["IsMinor"].ToString() == "True") { %> 
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
                <div class="col-md-12">
                  <div id="guardianInfoBody" class="tab-pane show active fade" runat="server">
                    <div class="row">
                      <div class="col-md-8 mb-3" align="left">
                        <span class=text-required>*</span>Guardian Full Name: <br />
                        <asp:TextBox runat="server" class="form-control" onpaste="return false" placeholder="Guardian Full Name" onkeypress="return characterAndNumbers(event)" ID="guardianFullName" Maxlength="150" Minlength="1" onkeydown="return /[A-Za-z0-9-' ]/.test(event.key)" onchange="javascript:ValidateGuardianFullName();"></asp:TextBox>
                      </div>
                      <div class="col-md-4 mb-3">
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
              </div>
            </div>
          <% } %> 
    
      <% } %> 

      <%  if( Session["categoryCode"].ToString() == "FMI") { %>
       <div class="accordion-item">
        <button class="accordion-header active" type="button" style="display: flex; align-items: center; justify-content: space-between;">
          <strong style="display: flex; align-items: center;">
            <svg version="1.0" xmlns="http://www.w3.org/2000/svg"
            width="24.000000pt" height="24.000000pt" viewBox="0 0 64.000000 64.000000"
            preserveAspectRatio="xMidYMid meet" style="margin-right: 5px;">

            <!-- Add the background rectangle -->
            <rect width="64" height="64" rx="9" fill="#11CDEF" opacity="0.1" />

            <g transform="translate(0.000000,64.000000) scale(0.100000,-0.100000)"
            fill="#0CB795" stroke="none">
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
      </ContentTemplate>
 </asp:UpdatePanel>
      <div class="container my-3">
        <div id="vehicleInfoHead" class="tab-pane show active fade" runat="server" align="center">
          <h3 style="color:white;" align="center"> Vehicle Information </h3>
        </div>
        <div id="vehicleInfoBody" runat="server" class="container-fluid"></div>
      </div>
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
          <div class="col-md-12">
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
                    <label class="form-check-label ml-4" for="dataPrivacyCheckbox">I hereby acknowledge that I have read, understand, and agree to the <a href="https://www.cebuanalhuillier.com/microinsurance" target="_blank">Terms and Conditions</a> relating to insurance coverage and payment of my services. </label>
                    <br />
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
                      <input type="text" class="form-control" id="captchaText" placeholder="Please answer Captcha Text" runat="server" style="text-align:center;"/>
                    </div>
                  </div>
                  <div class="col-md-2 mb-3">
                      <button runat="server" type="button" class="btnSubmitWithPopUp">Submit</button>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
    <div class="popup" id="popup">
        <div class="popup-title-container">
            <center>
               <p style="margin:auto; align-content:center; justify-content:center; display:flex; padding:10px;"><svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24" style="fill: #ffd800;transform: ;msFilter:;"><path d="M11.953 2C6.465 2 2 6.486 2 12s4.486 10 10 10 10-4.486 10-10S17.493 2 11.953 2zM12 20c-4.411 0-8-3.589-8-8s3.567-8 7.953-8C16.391 4 20 7.589 20 12s-3.589 8-8 8z"></path><path d="M11 7h2v7h-2zm0 8h2v2h-2z"></path></svg> &nbsp; Please make sure that the information you provided is correct</p> 
            </center>
        </div>
        <div class="popup-buttons">
               <button runat="server" type="submit" class="button button-microsite popup-button proceed" id="submitBtn" name="submitBtn" text="Submit" OnClientClick="this.disabled='true';" UseSubmitBehavior="false">Proceed</button>
              <button runat="server" type="button" class="popup-button goBackMicrosite">Go Back</button>
        </div>
    </div>
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

//--------- FOR FIRE PROPERTY DOUBLE FLOAT FIELDS --------//
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
 //-------------------------------- VALIDATE DEPENDENT BIRTHDAY--------------------///
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
else {
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
var contactNumberLength = phone.replace(/[^0-9]/g, "").length;
if (phone.match(re)) {
    console.log("VALID CONTACT NUMBER");
    return true;
}
if (contactNumberLength < 11) {
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
} else {
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
} else {
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


function text_changed(txtObj) {
    var birthDate = new Date(txtObj.value);
    var birthDateYear = birthDate.getFullYear();
    var dateNow = new Date();
    var dateNowYear = dateNow.getFullYear();
    var age = dateNowYear - birthDateYear;
    var elem = document.getElementById("infoForm_guardianDiv");
    if (age < 18) {
        elem.style.display = "block";
    } else {
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
