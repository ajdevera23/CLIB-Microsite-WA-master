 <%@ Page Title="" Language="C#" MasterPageFile="~/ProductRegistration.master" AutoEventWireup="true" CodeFile="MBizPACoverage.aspx.cs" Inherits="MBizPACoverage" MaintainScrollPositionOnPostback="true"  %>

<asp:Content ID="Header" ContentPlaceHolderID="HeaderContent" runat="Server">
    <div id="CLFRegHeader">
        <%--row pl-3 pt-3 pb-3--%>
        <%--<center style="width:100%;height:100%">--%>

        <%-- </center>--%>
    </div>
</asp:Content>
<asp:Content ID="MicroBizPageMain" ContentPlaceHolderID="BodyContent" runat="server">
    <form id="PACoverage" runat="server">     
        <div class="row" id="voucherDivBody" align="center" style="margin-top: 2%;">
            <h4 style="color:white; background-color:black;"> Personal Accident Coverage (Please Accident is an optional coverage for the Owner or employees)
</h4>
            <table style="margin-left: 5%; height: 30px; font-size: larger; width:100%; position:center"> 
            <tr>
                <td colspan="2">
                    <span>
                        Insured 1:
                         <asp:CheckBox ID="ownerCheckbox" runat="server" style="margin-left:5%" OnCheckedChanged="ownerCheckbox_CheckedChanged" AutoPostBack="true"/> Owner
                         <asp:CheckBox ID="employeeCheckbox" runat="server" style="margin-left:2%" OnCheckedChanged="employeeCheckbox_CheckedChanged" AutoPostBack="true"/> Employee

                        <%--<input type="radio" id="owner" name="Insured1" value="owner" style="margin-left:5%" runat="server" required="required"/>
                            <label for="owner">Owner</label>
                          <input type="radio" id="employee" name="Insured1" value="employee" style="margin-left:2%" runat="server" required="required"/>
                          <label for="employee">Employee</label>--%>
                    </span>
                </td>                
            </tr>
            <tr>
                <td style="width:20%" >
                    <span class="text-required" style="color: black">* First Name:</span>
                    <br />
                </td>
                <td style="width:20%">
                    <span class="text-required" style="color: black"> Middle Name:</span>
                    <br />
                </td>
                <td style="width:20%"  >
                    <span class="text-required" style="color: black">* Last Name:</span>
                    <br />
                </td>
                <td >
                    <span class="text-required" style="color: black"> Suffix:</span>
                    <br />
                </td>

                    
            </tr>
            <tr>                    
                <td >
                    <input type="text" id="FirstName" onpaste="return false" nkeypress="return characterAndNumbers(event)"
                        runat="server" maxlength="25" minlength="2" required="required" style="width:250px"/>
                </td>
                <td >
                    <input type="text" id="MiddleName" onpaste="return false" onkeypress="return characterAndNumbers(event)"
                        runat="server" maxlength="25" minlength="2" style="width:250px" />
                </td>
                <td >
                    <input type="text" id="LastName" onpaste="return false" onkeypress="return characterAndNumbers(event)"
                        runat="server" maxlength="25" minlength="2" required="required" style="width:250px" />
                </td>
                <td >
                    <input type="text" id="Suffix" onpaste="return false" onkeypress="return characterAndNumbers(event)"
                        runat="server" maxlength="5" minlength="1"  style="width:100px" />
                </td>
            </tr>
             <tr>
                <td style="width: 20%;">
                    <span class="text-required" style="color: black">* Date of Birth:</span>                    
                </td>   
                  <td style="width: 20%;">
                    <span class="text-required" >* Gender:</span>
                    <br />
                </td>
            </tr>
            <tr>
                <td style="width: 20%;">
                    <input id="birthDateTextBox" runat="server"  min="1753-01-01" max="9999-12-31" style="width:250px"
                        onblur="ValidateDOB()" ClientIDMode="Static" />
                </td>
                <td style="width:20%">
                    <span class="text-required" >
                       <asp:DropDownList ID="DDGender" runat="server" Width="250px"></asp:DropDownList>
                    </span>
                </td>
            </tr>
            <tr>
                <td>
                    <span class="text-required" style="color: black">* Name of Beneficiary:</span>
                    <br />
                </td>
                <td>
                    <span class="text-required" style="color: black">* Beneficiary Date of Birth:</span>
                    <br />
                </td>
                <td>
                    <span class="text-required" style="color: black">* Relationship to Insured:</span>
                    <br />
                </td>
            </tr>
            <tr>                    
                <td>
                    <input type="text" id="bene1Name" onpaste="return false" onkeypress="return characterAndNumbers(event)"
                        runat="server" maxlength="75" minlength="2" required="required" style="width:250px"/>
                </td>
                <td>
                    <input type="text" id="bene1DOB" min="1753-01-01" max="9999-12-31" runat="server" style="width:250px" />
                </td>
                <td>
                    <span class="text-required" >
                       <asp:DropDownList ID="bene1RelationshipDD" runat="server" Width="250px"></asp:DropDownList>
                    </span>
                </td>                
            </tr>
            <tr>
                
                <td colspan="2">   
                    <br />
                        Insured 2:
                    <span style="margin-left:5%">
                        <asp:CheckBox ID="insured2CheckBox" runat="server" OnCheckedChanged="insured2CheckBox_CheckedChanged" AutoPostBack="true"/> Employee
                    </span>
                </td>                
            </tr>
            <tr>
                <td style="width:20%" >
                    <span class="text-required" style="color: black">* First Name:</span>
                    <br />
                </td>
                <td style="width:20%">
                    <span class="text-required" style="color: black"> Middle Name:</span>
                    <br />
                </td>
                <td style="width:20%"  >
                    <span class="text-required" style="color: black">* Last Name:</span>
                    <br />
                </td>
                <td >
                    <span class="text-required" style="color: black"> Suffix:</span>
                    <br />
                </td>

                    
            </tr>
            <tr>                    
                <td >
                   
                    <input type="text" id="FirstName2" onpaste="return false" onkeypress="return characterAndNumbers(event)"
                        runat="server" maxlength="25" minlength="2" required="required" style="width:250px"/>
                </td>
                <td >
                    
                    <input type="text" id="MiddleName2" onpaste="return false" onkeypress="return characterAndNumbers(event)"
                        runat="server" maxlength="25" minlength="2" style="width:250px" />
                </td>
                <td >
                    
                    <input type="text" id="LastName2" onpaste="return false" onkeypress="return characterAndNumbers(event)"
                        runat="server" maxlength="25" minlength="2" required="required" style="width:250px" />
                </td>
                <td >
                   

                    <input type="text" id="Suffix2" onpaste="return false"  onkeypress="return characterAndNumbers(event)"
                        runat="server" maxlength="5" minlength="2"  style="width:100px" />
                </td>
            </tr>
             <tr>
                <td style="width: 20%;">
                    <span class="text-required" style="color: black">* Date of Birth:</span>                    
                </td>   
                  <td style="width: 20%;">
                    <span class="text-required" >* Gender:</span>
                    <br />
                </td>
            </tr>
            <tr>
                <td style="width: 20%;">
                    <input id="birthDateTextBox2" runat="server"  min="1753-01-01" max="9999-12-31" style="width:250px" 
                        onblur="ValidateDOB2()" ClientIDMode="Static" />
                </td>
                <td style="width:20%">
                    <span class="text-required" >
                       <asp:DropDownList ID="DDGender2" runat="server" Width="250px"></asp:DropDownList>
                    </span>
                </td>
            </tr>
            <tr>
                <td>
                    <span class="text-required" style="color: black">* Name of Beneficiary:</span>
                    <br />
                </td>
                <td>
                    <span class="text-required" style="color: black">* Beneficiary Date of Birth:</span>
                    <br />
                </td>
                <td>
                    <span class="text-required" style="color: black">* Relationship to Insured:</span>
                    <br />
                </td>
            </tr>
            <tr>                    
                <td>
                    <input type="text" id="bene2Name" onpaste="return false" onkeypress="return characterAndNumbers(event)"
                        runat="server" maxlength="75" minlength="2" required="required" style="width:250px"/>
                </td>
                <td>
                    <input type="text" id="bene2DOB" min="1753-01-01" max="9999-12-31" runat="server"  style="width:250px" />
                </td>
                <td>
                    <span class="text-required" >
                       <asp:DropDownList ID="bene2RelationshipDD" runat="server" Width="250px"></asp:DropDownList>
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
                    <asp:Button ID="btnBack" runat="server" Text="Previous" OnClick="btnBack_Click" Width="150" Height="35" Enabled="true" />

                    <asp:Button ID="btnNext" runat="server" Text="Next" OnClick="btnNext_Click" Width="100" Height="35" Enabled="true" />
                </h4>
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

        function ValidateDOB2() {
            
            var val = document.getElementById("birthDateTextBox2").value;
               
            var birthDate = new Date(val);
            var birthDateYear = birthDate.getFullYear();                                 
            var now = new Date();
            var dateNowYear = now.getFullYear();
            var age = dateNowYear - birthDateYear;
            var bdMonth = birthDate.getMonth();
            var bdDay = birthDate.getDate();
            var nowMonth = now.getMonth();
            var nowDay = now.getDate();     
            if (age < 18) {
                alert("You must be 18 years old to register");
                document.getElementById("birthDateTextBox2").value = "";
                return true;   
            }  

            if (age == 18) {
                if (nowMonth < bdMonth) {
                    alert("You must be 18 years old to register");
                    document.getElementById("birthDateTextBox2").value = "";
                    return true;  
                }
                if (nowMonth == bdMonth) {
                    if (nowDay < bdDay) {
                        alert("You must be 18 years old to register");
                        document.getElementById("birthDateTextBox2").value = "";
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

