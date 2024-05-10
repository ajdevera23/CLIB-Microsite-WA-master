<%@ Page Language="C#" MasterPageFile="~/Enrollment.Master" AutoEventWireup="true" CodeFile="CheckEligibility.aspx.cs" Inherits="CheckEligibility" %>

<asp:Content ContentPlaceHolderID="productLogo" runat="server">
<div id="productImage" runat="server" class="image-container-through-div"></div>
<%--  <asp:Image runat="server" ID="productImage" ImageUrl="../Images/ProductLogoSample.png" />--%>
</asp:Content>
<asp:Content ContentPlaceHolderID="partnerLogo" runat="server">
<%--<asp:Image runat="server" ID="partnerImage" ImageUrl="../Images/ProductLogoSample.png" />--%>
    <div id="partnerImage" runat="server" class="image-container-through-div"></div>
</asp:Content>
<asp:Content ContentPlaceHolderID="infoForm" runat="server"  >
    <meta http-equiv='cache-control' content='no-cache'>
<meta http-equiv='expires' content='0'>
<meta http-equiv='pragma' content='no-cache'>
 <form id="enrollmentForm" class="container body-content  container-enrollment" method="post" autocomplete="off"  runat="server" novalidate>
     <div class="container my-3">
          <div class="accordion">
                <div class="accordion-item">
                <button class="accordion-header active" id="basicinformation" type="button">
                  <strong>
                    <svg xmlns="http://www.w3.org/2000/svg" width="35" height="35" style="position:absolute;">
                      <rect id="Rectangle_73" data-name="Rectangle 73" width="35" height="35" rx="9" fill="#f7a424" opacity="0.1"/>
                      <path id="Icon_ionic-md-person" data-name="Icon ionic-md-person" d="M14.7,14.7A5.1,5.1,0,1,0,9.6,9.6,5.113,5.113,0,0,0,14.7,14.7Zm0,2.549c-3.378,0-10.2,1.721-10.2,5.1v2.549H24.894V22.345C24.894,18.967,18.075,17.246,14.7,17.246Z" transform="translate(2.803 2.803)" fill="#f7a424"/>
                    </svg>  
                    <b style="margin-left:50px;">Insured Information</b>
                  </strong>
                  <i class="fas fa-angle-right"></i>
                </button>
                <div class="accordion-body active">
                        <div class="row col-md-12" align="center">
                                <div class="col-md-3 mb-3" align="left" >
                                    <span class="text-required">*</span>First Name: <br />
                                    <input type="text" id="firstName" class="form-control" onpaste="return false" placeholder="First Name" runat="server"  maxlength="75" minlength="1" onkeypress="return characterAndNumbers(event)" onkeydown="return /[A-Za-z0-9-' ]/.test(event.key)" onchange="javascript:ValidateFirstName();" title="First Name" required/>
                                </div>
                                <div class="col-md-3 mb-3" align="left">
                                     Middle Name: <br />
                                     <input type="text" id="middleName" class="form-control" onpaste="return false" placeholder="Middle Name" runat="server"  maxlength="20" minlength="1"  onkeypress="return characterAndNumbers(event)" onkeydown="return /[A-Za-z0-9-' ]/.test(event.key)"  onchange="javascript:ValidateMiddleName();" title="Middle Name"/>
                                 </div> 
                                <div class="col-md-3 mb-3" align="left">
                                     <span class="text-required">*</span>Last Name: <br />
                                    <input type="text" id="lastName" class="form-control" onpaste="return false" placeholder="Last Name" runat="server"  maxlength="20" minlength="1" onkeypress="return characterAndNumbers(event)" onkeydown="return /[A-Za-z0-9-' ]/.test(event.key)" onchange="javascript:ValidateLastName();" title="Last Name" required/>
                                </div>
                                <div class="col-md-3 mb-3" align="left">
                                     Suffix: <br />
                                    <input type="text" id="suffix" class="form-control" onpaste="return false" placeholder="Suffix" runat="server" maxlength="5" minlength="1" onkeypress="return validateSuffix(event)"/>
                                </div>
                          </div>
                          <div class="row col-md-12"" align="left">
                                <div class="col-md-3 mb-3">
                                    <span class="text-required">*</span> Birthdate: <br />
                                    <asp:TextBox runat="server" class="form-control disableFuturedate" onpaste="return false" type="date" maxlength="10" minlength="10" ID="birthDateTextBox"></asp:TextBox>
                                </div>
                                <div class="col-md-6">
                                     <br />
                                      <asp:Button ID="btnCheckEligibility" runat="server" OnClick="btnCheckEligibility_OnClick" Text="CHECK ELIGIBILITY" style="width:100%; top:100px; border:3px solid #000000; font-weight:900;"  class="btn btn-info checkeligibility"/>
                                </div>
                         </div>
                   </div>
              </div>
          </div>
      </div>   
</form>
</asp:Content>
<asp:Content ContentPlaceHolderID="ScriptContent" runat="server">
     <script>    
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
        function ValidateFirstName() {

            var nametextbox = document.getElementById("<%=firstName.ClientID%>").value;
            var nameformat = /^[A-Za-z0-9-' ]+$/;

            if (nametextbox.match(nameformat)) {
                console.log("VALID FIRST NAME");
                return true;
            }
            else
            {
                Swal.fire('You have entered invalid information. Please try again.');
                document.getElementById("<%=firstName.ClientID%>").value = "";
                return false;
             }
        }
        function ValidateMiddleName() {

            var nametextbox = document.getElementById("<%=middleName.ClientID%>").value;
            var nameformat = /^[A-Za-z0-9-' ]+$/;
            
            if (nametextbox.match(nameformat)) {
                console.log("VALID MIDDLE NAME");
                return true;
            }
            else
            {
              
               Swal.fire('You have entered invalid information. Please try again.');
               document.getElementById("<%=middleName.ClientID%>").value = "";
               return false;
             }
        }
        function ValidateLastName()
        {

            var nametextbox = document.getElementById("<%=lastName.ClientID%>").value;
            var nameformat = /^[A-Za-z0-9-' ]+$/;
            
            if (nametextbox.match(nameformat)) {
                console.log("VALID LAST NAME");
                return true;
            }
            else
            {
               Swal.fire('You have entered invalid information. Please try again.');
               document.getElementById("<%=lastName.ClientID%>").value = "";
               return false;
             }
        }
         function validateSuffix(event) {
             // Get the character code of the pressed key
             var charCode = event.which || event.keyCode;

             // Convert the character code to its corresponding string representation
             var charStr = String.fromCharCode(charCode);

             // Test if the character is a letter, number, period, comma, or space
             return /[a-zA-Z0-9., ]/.test(charStr);
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


       function character(e)
       {
        isIE = document.all ? 1 : 0
        keyEntry = !isIE ? e.which : event.keyCode;
        if (((keyEntry >= '65') && (keyEntry <= '90')) || ((keyEntry >= '97') && (keyEntry <= '122')) || (keyEntry == '46') || (keyEntry == '32') || keyEntry == '45' || keyEntry=='188')
        return true;
        else
        {
            return false;
        }
        }

         function characterAndNumbers(e) {
             var inputChar = String.fromCharCode(e.charCode);
             var regex = /^[A-Za-z0-9-' ,.]+$/; // Allow letters, numbers, hyphen, comma, period, space, and single quote

             if (regex.test(inputChar)) {
                 return true;
             } else {
                 return false;
             }
         }



        function emailCheck(e)
       {
        isIE = document.all ? 1 : 0
        keyEntry = !isIE ? e.which : event.keyCode;
        if (((keyEntry >= '48') && (keyEntry <= '57')) || ((keyEntry >= '64') && (keyEntry <= '90')) || ((keyEntry >= '97') && (keyEntry <= '122')) || (keyEntry == '46') || (keyEntry == '32') || (keyEntry == '45') || keyEntry == '186' || keyEntry == '189' || keyEntry == '95')  
        return true;
        else
        {
            return false;
        }
       }
     </script>
</asp:Content> 