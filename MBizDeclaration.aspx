<%@ Page Title="" Language="C#" MasterPageFile="~/ProductRegistration.master" AutoEventWireup="true" CodeFile="MBizDeclaration.aspx.cs" Inherits="MBizDeclaration" MaintainScrollPositionOnPostback="true"  %>

<asp:Content ID="Header" ContentPlaceHolderID="HeaderContent" runat="Server">
    <div id="CLFRegHeader">
        <%--row pl-3 pt-3 pb-3--%>
        <%--<center style="width:100%;height:100%">--%>

        <%-- </center>--%>
    </div>
</asp:Content>
<asp:Content ID="MBizNotQuali" ContentPlaceHolderID="BodyContent" runat="server">
    <form id="eRaffleDetailsForm" runat="server">
       <h2 style="color:white; background-color:black; text-align:center;"> Declaration</h2>

        <div class="row" id="voucherDivBody" align="center" style="margin-top: 5%;">
            
            <table style="position:center; font: bolder; width:70%; ">
                <tr>
                    <td colspan="2">
                        <p style="text-align:justify">I understand that I can only be covered under one Micro Property policy. Should I have enrolled for more than one, I agree to have premiums of excess policies refunded and no claims will be payable under any policy in excess of one.</p>
<p>I want to receive transaction notifications, be regularly updated by Cebuana Lhuillier and its affiliate companies of their products and services, and thus understand fully that my foregoing information will accordingly be shared.</p>
<p>I have been informed that I have the option not to give the above information, in which case I understand that my transaction will not be processed. I have also been informed that I can make corrections to any inaccurate or deficient information and that I have an option to withdraw my consent prior to the processing of my transaction by emailing Cebuana Lhuillier at clismarketing@pjlhuillier.com or calling Telephone Number (02) 895 1093.</p>
                        
                        <br />                
                    </td>
                </tr>
                
                <tr>
                    <td>
                        <asp:CheckBox ID="certifyCheckbox" runat="server" OnCheckedChanged="certifyCheckbox_CheckedChanged" BorderWidth="20px" BorderColor="White" Font-Bold="true" /> 

                    </td>
                    <td>
                        <p  style="text-align:justify">
                        
                            I hereby certify that the foregoing information are freely and voluntary given and are true and correct to the best of my knowledge. Further, I hereby authorize Cebuana Lhuillier to disclose to AXA Philippines my above information to aid and all investigations that may be initiated on account of, or in relation to any concerns that may arise out of this transaction.
                        </p>
                        <br />

                    </td>
                </tr>
                <tr>
                    <td colspan ="2">
                        <p style="text-align:center">
                         *upload a photo of a valid ID with 3 specimen signatures 
                            <asp:FileUpload ID="PhotoUpload" runat="server" />
                            <asp:Label ID="lblImageName" runat="server"></asp:Label>
                        </p>
                    </td>
                </tr>
                
            </table>
          <br />
            
             <h4>
                    <asp:Button ID="btnBack" runat="server" Text="Previous" OnClick="btnBack_Click" Width="150" Height="35" Enabled="true" />

                    <asp:Button ID="btnNext" runat="server" Text="Submit" OnClick="btnNext_Click" Width="100" Height="35" Enabled="true" />
                </h4>

        </div>
        
    </form>
    <br />
    <footer class="pb-3">
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

