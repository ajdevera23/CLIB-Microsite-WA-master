<%@ Page Title="" Language="C#" MasterPageFile="~/ProductRegistration.master" AutoEventWireup="true" CodeFile="MBizOwnerDetails2.aspx.cs" Inherits="MBizOwnerDetails2" MaintainScrollPositionOnPostback="true"  %>

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
            <table style="margin-left: 5%; height: 30px; font-size: larger; width:100%; position:center">               
            <tr>
                <td style="width:300px">
                    <span class="text-required" >* Gender:</span>
                    <br />
                </td>
                <td style="width:300px">
                    <span class="text-required" >* Civil Status:</span>
                    <br />
                </td>                   
                <td >
                    <span class="text-required" style="color: black; margin-left:3%">* Nationality:</span>
                    <br />
                </td>    
            </tr>
            <tr>
                <td style="width:300px">
                    <span class="text-required" >
                       <asp:DropDownList ID="DDGender" runat="server" Width="250px"></asp:DropDownList>
                    </span>
                </td>
                <td style="width:300px" >
                    <span class="text-required" style="color: black; margin-left:3%">
                        <asp:DropDownList ID="DDCivilStatus" runat="server" Width="250px"></asp:DropDownList>
                    </span>
                </td>
                <td>
                    <span class="text-required" style="color: black; margin-left:3%">
                        <input type ="text" runat="server" name="" id="nationality" style="width:250px" required="required" maxlength="25"
                        minlength="2" title="Nationality accepts alphabet only." />
                    </span>
                </td>   
            </tr>
            <tr>
                <td>
                    <span class="text-required" style="color: black; ">* Address</span>
                </td>
            </tr>
            <tr>
                <td colspan="3">
                    <input type ="text" runat="server" name="Address" id="Address" style="width:55%" required="required" maxlength="200"
                        minlength="10"  />
                </td>                
            </tr>
            <tr>
                <td >
                    <span class="text-required" style="color: black; width:10%">* Province:</span>
                    <br />
                </td>
                <td >
                    <span class="text-required" style="color: black; width:10%; margin-left:3%">* City</span>
                    <br />
                </td>
                <td>
                    <span class="text-required" style="color: black; width:10%; margin-left:3%">* Postal Code</span>
                    <br />
                </td>               
            </tr>

            <tr>
                <td>
                    <span class="text-required" style="color: black; ">
                       <asp:DropDownList ID="DDProvince" runat="server" 
                           OnSelectedIndexChanged="DDProvince_SelectedIndexChanged" AutoPostBack="True" Width="250px"></asp:DropDownList>
                    </span>
                </td>
                <td>
                    <span class="text-required" style="color: black; margin-left:3%">
                        <asp:DropDownList ID="DDcity" runat="server" Width="250px"></asp:DropDownList>
                    </span>
                </td>
                <td>
                    <span class="text-required" style="color: black; margin-left:3%">
                        <input id="postalCode" runat="server" required="required" style="width:180px" maxlength="4" minlength="4" />
                    </span>
                </td>
            </tr>
            <tr>                
                <td>
                    <span class="text-required" style="color: black;">* Valid ID</span>
                </td>
                <td >
                    <span class="text-required" style="color: black;  margin-left:3%">* Valid ID Number</span>
                </td>
                <td >
                    <span class="text-required" style="color: black;  margin-left:3%">* Source of Funds</span>
                </td>
                <td >
                    <span class="text-required" style="color: black;  margin-left:3%">* Nature of Work/Business</span>
                </td>
            </tr>
            <tr>                
                <td >        
                    <span>
                        <asp:DropDownList ID="DDValidID" runat="server" style="width:250px" ></asp:DropDownList>                    
                    </span>
                </td>
                <td >
                    <span class="text-required" style="color: black; margin-left:3%">
                        <input type="text" id="validIDNumber"  onpaste="return false" onkeypress="return characterAndNumbers(event)"
                            runat="server" maxlength="25" minlength="1"  style="width:250px" required="required" />
                    </span>
                </td>
                <td >
                    <span class="text-required" style="color: black; margin-left:3%">
                       <asp:DropDownList ID="DDSourceOfFunds" runat="server" Width="250px"></asp:DropDownList>
                    </span>
                </td>
                <td >
                    <span class="text-required" style="color: black; margin-left:3%">
                        <asp:DropDownList ID="DDNatureofWork" runat="server" Width="250px"></asp:DropDownList>
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
                <asp:Button ID="btnBack" runat="server" Text="Previous" OnClick="btnBack_Click" Width="120" Height="35" Enabled="true" />
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
    <script>
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

