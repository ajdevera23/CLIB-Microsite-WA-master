<%@ Page Title="" Language="C#" MasterPageFile="~/ProductRegistration.master" AutoEventWireup="true" CodeFile="MBizThankYou.aspx.cs" Inherits="MBizThankYou" MaintainScrollPositionOnPostback="true"  %>

<asp:Content ID="Header" ContentPlaceHolderID="HeaderContent" runat="Server">
    <div id="CLFRegHeader">
        <%--row pl-3 pt-3 pb-3--%>
        <%--<center style="width:100%;height:100%">--%>

        <%-- </center>--%>
    </div>
</asp:Content>
<asp:Content ID="MBizNotQuali" ContentPlaceHolderID="BodyContent" runat="server">
    <form id="eRaffleDetailsForm" runat="server">
        
        <div class="row" id="voucherDivBody"  style="margin-top: 7%;">
            <h1 style="font-size:40px">Thank you Ka-Cebuana!</h1>
            &nbsp
            <h2 style="font-size:20px">We have received your application. Expect to receive your email of </h2>
            <h2 style="font-size:20px">confirmation of application in 3-5 business days.</h2>
            
            &nbsp
            &nbsp
            <h4 style="font-size:30px">Application Reference Number: <asp:Label ID="lblRTN" runat="server" Text=""></asp:Label></h4>
            <br />
            <br />
          
            <asp:Button ID="btnContinue" runat="server" Text="Done" OnClick="btnContinue_Click" Width="150px" Height="50px"  />

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

