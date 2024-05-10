<%@ Page Title="" Language="C#" MasterPageFile="~/ProductRegistration.master" AutoEventWireup="true" CodeFile="MBizOwnerDetails.aspx.cs" Inherits="MBizOwnerDetails" MaintainScrollPositionOnPostback="true"  %>

<asp:Content ID="Header" ContentPlaceHolderID="HeaderContent" runat="Server">
    <div id="CLFRegHeader">
        <%--row pl-3 pt-3 pb-3--%>
        <%--<center style="width:100%;height:100%">--%>

        <%-- </center>--%>
    </div>
</asp:Content>
<asp:Content ID="MicroBizPageMain" ContentPlaceHolderID="BodyContent" runat="server">
    <form id="BizOwnerDet" runat="server">
        
        <div class="row" id="voucherDivBody" align="center" style="margin-top: 2%;">
            <h4 style="color:white; background-color:black;"> Business Owner Details</h4>
            <table style="margin-left: 5%; height: 30px; font-size: larger;" id=" ">
               
            <tr>
                <td style="width: 300px;">
                    <span class="text-required" style="color: black">* First Name:</span>
                    <br />
                </td>
                <td style="width: 300px;">
                    <span class="text-required" style="color: black"> Middle Name:</span>
                    <br />
                </td>
                <td style="width: 300px;">
                    <span class="text-required" style="color: black">* Last Name:</span>
                    <br />
                </td>
                <td style="width: 300px;">
                    <span class="text-required" style="color: black"> Suffix:</span>
                    <br />
                </td>

                    
            </tr>
            <tr>
                    
                    <td style="width: 300px;">
                        <input type="text" id="FirstName" onpaste="return false" onkeypress="return characterAndNumbers(event)"
                            runat="server" maxlength="25" minlength="2" required="required" style="width:250px"/>
                    </td>
                    <td style="width: 300px;">
                        <input type="text" id="MiddleName"  onpaste="return false" onkeypress="return characterAndNumbers(event)"
                            runat="server" maxlength="25" minlength="2" style="width:250px" />
                    </td>
                    <td style="width: 300px;">
                        <input type="text" id="LastName" onpaste="return false" onkeypress="return characterAndNumbers(event)"
                            runat="server" maxlength="25" minlength="2" required="required" style="width:250px" />
                    </td>
                    <td style="width: 300px;">
                        <input type="text" id="Suffix" onpaste="return false" onkeypress="return characterAndNumbers(event)"
                            runat="server" maxlength="5" minlength="2"  style="width:100px" />
                    </td>
                </tr>
           
                
            <tr>

                    <td style="width: 300px; height:30px;">
                        <span class="text-required" style="color: black">* Date of Birth:</span>
                        <br />

                    </td>
                <td>
                    <span class="text-required" style="color: black; ">* Contact Number</span>
                </td>
                <td>
                    <span class="text-required" style="color: black;">* Email Address</span>
                </td>
                    
            </tr>
            <tr>
                <td style="width: 300px;">
                    <input id="birthDateTextBox" runat="server"  min="1753-01-01" max="9999-12-31" style="width:250px"
                        onblur = "return ValidateDOB()" ClientIDMode="Static" />
                </td>
                <td>
                    <span>
                        <input type="text" id="contactNumber" onpaste="return false" onkeypress="return characterAndNumbers(event)"
                        runat="server" maxlength="11" minlength="11" required="required" style="width:250px" />
                    </span>
                </td>
                <td>
                    <span>
                        <input type="text" id="emailAddress" onpaste="return false" 
                            runat="server" maxlength="50" minlength="5" required="required"   style="width:250px"/>
                    </span>
                </td>
            </tr>
            &nbsp;
            &nbsp;
            &nbsp;
            &nbsp;
            &nbsp;
                
            
        </table>
             &nbsp;
            &nbsp;
            &nbsp;
            &nbsp;
            &nbsp;
            <h4>
                    <asp:Button ID="btnValidate" runat="server" Text="Next"  Width="100" Height="35" Enabled="true"
                        OnClick="btnValidate_Click"/>
                </h4>
            &nbsp;
            &nbsp;
            &nbsp;
            &nbsp;
            &nbsp;
        
        </div>
        
    </form>
    <br />
    <footer class="pb-1">
        <center>
              <p>© 2021 - Cebuana Lhuillier</p>  
            </center>
    </footer>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ScriptContent" runat="Server">
    <script type="text/javascript">

        function ValidateDOB() {
            
               var val = document.getElementById("birthDateTextBox").value;
               
                var birthDate = new Date(val);
                var birthDateYear = birthDate.getFullYear();                                 
               var now = new Date();
               var dateNowYear = now.getFullYear();
               var age = dateNowYear - birthDateYear;
               var bdMonth = birthDate.getMonth();
               var bdDay = birthDate.getDate();
               var nowMonth = now.getMonth();
               var nowDay = now.getDate();
               var month = nowMonth - bdMonth;
               var date = nowDay - bdDay;
               if (age < 18) {
                   alert("You must be 18 years old to register");
                   document.getElementById("birthDateTextBox").value = "";
                    return true;   
               }  

               if (age == 18) {
                   if (nowMonth < bdMonth) {
                       alert("You must be 18 years old to register");
                       document.getElementById("birthDateTextBox").value = "";
                        return true;  
                   }
                   if (nowMonth == bdMonth) {
                       if (nowDay < bdDay) {
                           alert("You must be 18 years old to register");
                          document.getElementById("birthDateTextBox").value = "";
                            return true;  
                       }
                   }
                   
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
            isIE = document.all ? 1 : 0
            keyEntry = !isIE ? e.which : event.keyCode;
            if (((keyEntry >= '48') && (keyEntry <= '57')) || ((keyEntry >= '65') && (keyEntry <= '90')) || ((keyEntry >= '97') && (keyEntry <= '122')) || (keyEntry == '46') || (keyEntry == '32') || (keyEntry == '45') || (keyEntry == '188') || (keyEntry == '39') || (keyEntry == '191'))
                return true;
            else {
                return false;
            }
        }



    </script>
  
</asp:Content>

