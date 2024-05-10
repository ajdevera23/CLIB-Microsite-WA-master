<%@ Page Title="" Language="C#" MasterPageFile="~/ProductRegistration.master" AutoEventWireup="true" CodeFile="ClientReferral.aspx.cs" Inherits="ClientReferral" MaintainScrollPositionOnPostback="true" %>

<asp:Content ID="Header" ContentPlaceHolderID="HeaderContent" runat="Server">
    <div id="CLFRegHeader">
        <%--row pl-3 pt-3 pb-3--%>
        <%--<center style="width:100%;height:100%">--%>

        <%-- </center>--%>
    </div>
</asp:Content>
<asp:Content ID="ClientReferralPageMain" ContentPlaceHolderID="BodyContent" runat="server">
    <form id="adcForm" runat="server">
        <div class="row" id="voucherDiv" align="center">
        </div>
        <div class="row" id="voucherDivBody" align="center" style="margin-top: 2%">
            <h4>Hi Ka Cebuana!</h4>
            <h4>Welcome to the Client Referral Module</h4>
            <table style="margin-left: 5%; height: 30px; font-size: larger;">
                <tr>
                    <td style="width: 300px;">Referral Transaction Number:
                    </td>
                    <td style="width: 300px;">
                        <asp:Label ID="rtnLbl" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="width: 300px;">Region:
                    </td>
                    <td style="width: 300px;">
                        <asp:Label ID="regionLbl" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="width: 300px;">Area Code:
                    </td>
                    <td style="width: 300px;">
                        <asp:Label ID="areaCodeLbl" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="width: 300px;">Branch Code:
                    </td>
                    <td style="width: 300px;">
                        <asp:Label ID="branchCodeLbl" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="width: 300px;">Branch Name:
                    </td>
                    <td style="width: 300px;">
                        <asp:Label ID="branchNameLbl" runat="server"></asp:Label>
                    </td>
                </tr>
                &nbsp;
                &nbsp;
                &nbsp;
                <tr>
                    <td></td>
                </tr>
                <tr>
                    <td style="width: 300px;">Type of Client:
                        <br />
                    </td>
                    <td style="width: 300px; padding: 5px; align-items:stretch;">
                        <asp:RadioButton ID="individualRbtn" runat="server" Text="  Individual" GroupName="client" OnCheckedChanged="RadioButton_CheckedChanged" Checked="true" AutoPostBack="True" Width="150px" />
                        <asp:RadioButton ID="groupRbtn" runat="server" Text="  Group" GroupName="client" OnCheckedChanged="RadioButton_CheckedChanged" AutoPostBack="True" />

                    </td>
                    
                </tr>
                <asp:Panel ID="groupPnl" runat="server">
                    <tr>
                        <td style="width: 300px;">
                            <span class="text-required">*</span> Group / Business Name:
                        <br />
                        </td>
                        <td style="width: 300px;">
                            <input type="text" id="GroupName" class="form-control" onpaste="return false" onkeypress="return characterAndNumbers(event)"
                                runat="server" maxlength="25" minlength="2" required="required" />
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 300px;">
                            <span class="text-required">*</span> Contact Person Designation:
                        <br />
                        </td>
                        <td style="width: 300px;">
                            <input type="text" id="ContactPerson" class="form-control" onpaste="return false" onkeypress="return characterAndNumbers(event)"
                                runat="server" maxlength="25" minlength="2" required="required" />
                        </td>
                    </tr>


                </asp:Panel>
                <asp:Panel ID="individualPnl" runat="server">
                    <tr>
                        <td style="width: 300px;">
                            <span class="text-required" style="color: red">* Last Name:</span>
                            <br />
                        </td>
                        <td style="width: 300px;">
                            <input type="text" id="LastName" class="form-control" onpaste="return false" onkeypress="return characterAndNumbers(event)"
                                runat="server" maxlength="25" minlength="2" required="required" />
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 300px;">
                            <span class="text-required" style="color: red">* First Name:</span>
                            <br />
                        </td>
                        <td style="width: 300px;">
                            <input type="text" id="FirstName" class="form-control" onpaste="return false" onkeypress="return characterAndNumbers(event)"
                                runat="server" maxlength="25" minlength="2" required="required" />
                        </td>
                    </tr>


                </asp:Panel>
                <tr>
                    <td style="width: 300px;">
                        <span class="text-required" style="color: red">* Date of Birth:</span>
                        <br />

                    </td>
                    <td style="width: 300px;">
                        <input id="birthDateTextBox" runat="server"  min="1753-01-01" max="9999-12-31"/>

                        <%--asp:TextBox runat="server" class="form-control" onkeypress="return text_changed(this);"
                            onchange="this.onkeypress();" oninput="this.onkeypress();"
                            onpaste="return false" type="date" ID="birthDateTextBox" contentEditable="false"></asp:TextBox>--%>
                    </td>
                </tr>
                <tr>
                    <td style="width: 300px;">

                        
                    </td>
                    <td style="width: 300px;">
                        
                          
                    </td>
                </tr>
                <tr>
                    <td style="width: 300px;">

                        <asp:Button ID="btnValidate" runat="server" Text="Validate" OnClick="btnValidate_Click" Width="100" Height="35" Enabled="true" />
                    </td>
                    <td style="width: 300px; color: red;">
                        <%--<asp:Label ID="validateLbl" runat="server"></asp:Label>--%>
                    </td>

                </tr>
                <asp:Panel ID="otherDetailsPnl" runat="server">
                    <tr>
                        <td style="width: 300px;">
                            <span class="text-required">* Email Address:</span>
                            <br />
                        </td>
                        <td style="width: 300px;">


                            <input type="text" runat="server" class="form-control" id="emailAddress" onpaste="return false" minlength="5"
                                maxlength="50" required="required" />
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 300px;">
                            <span class="text-required">* Cellphone Number:</span>
                            <br />
                        </td>
                        <td style="width: 300px;">
                            <input type="text" runat="server" class="form-control" id="cellphoneNo" onpaste="return false" required="required"
                                 />
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 300px;">
                            <span class="text-required">*</span>Address Building / Street Name:
                            <br />
                        </td>
                        <td style="width: 300px;">
                            <input type="text" id="Address" class="form-control" onpaste="return false"
                                runat="server" onkeypress="return characterAndNumbers(event)" maxlength="120" minlength="2" required="required" />
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 300px;">
                            <span class="text-required">*</span>Province:
                            <br />
                        </td>
                        <td style="width: 300px;">
                            <asp:DropDownList ID="DDProvince" class="form-control" runat="server" OnSelectedIndexChanged="DDProvince_SelectedIndexChanged" AutoPostBack="True"></asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 300px;">
                            <span class="text-required">*</span>City:
                            <br />
                        </td>
                        <td style="width: 300px;">
                            <asp:DropDownList ID="DDcity" class="form-control" runat="server"></asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 300px;">
                            <span class="text-required"></span>Zip Code:
                            <br />
                        </td>
                        <td style="width: 300px;">
                            <input type="text" id="ZipCode" class="form-control" onpaste="return false"
                                runat="server" onkeypress="return characterAndNumbers(event)" maxlength="5" />
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 300px;">
                            <span class="text-required">*</span>Interested with following products:
                            <br />
                        </td>
                        <td style="width: 300px;">
                            <input type="text" id="Interests" class="form-control" maxlength="250" minlength="5"
                                runat="server" required="required" />
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 300px;">
                            <span class="text-required">*</span>Preferred appointment time and Notes:
                            <br />
                        </td>
                        <td style="width: 300px;">
                            <input type="text" id="Appointments" class="form-control" maxlength="250" minlength="5"
                                runat="server" required="required" />
                        </td>
                    </tr>
                    <tr id="photoTR" runat="server">
                        <td style="width: 300px;">
                            <span class="text-required">*</span>Photo:
                        <br />
                        </td>
                        <td style="width: 300px;">
                            <asp:FileUpload ID="PhotoUpload" runat="server" />
                            <asp:Label ID="lblImageName" runat="server"></asp:Label>
                        </td>
                    </tr>


                </asp:Panel>
                




            </table>
            &nbsp;
            &nbsp;
            &nbsp;
            &nbsp;
            &nbsp;

        </div>
        <div id="dpaView" align="center">
        <asp:Panel ID="dpaPnl" runat="server"  align="center" >
                    <div id="dataPrivacyFormHead" class="tab-pane show active fade" align="left" style="margin-left: 5%;">
                        <h4 style="color: black;" align="center">Data Privacy Agreement & Client Consent Declaration </h4>
                    </div>
                    <div id="dataPrivacyFormBody" class="tab-pane show active fade">
                        <div>
                            <div>
                                <div align="center" style="margin-left: 6%">
                                    <asp:CheckBox ID="dataPrivacyCheckbox" CssClass="form-check-input ml-1" runat="server" OnCheckedChanged="dataPrivacyCheckbox_CheckedChanged" AutoPostBack="True" />
                                    <%--<input type="checkbox" id="dataPrivacyCheckbox" class="form-check-input ml-1" runat="server"  />--%>
                                    <label class="form-check-label ml-4" for="dataPrivacyCheckbox">I hereby acknowledge that I have read, understand, and agree to the <a href="DataPrivacy.aspx" target="_blank">Data Privacy Policy</a> of Cebuana Lhuillier.</label><br />
                                </div>
                            </div>

                        </div>
                    </div>
                    &nbsp
                </asp:Panel>
        
            <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" class="form-control" Width="300" Height="50" />
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

