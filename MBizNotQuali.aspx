<%@ Page Title="" Language="C#" MasterPageFile="~/ProductRegistration.master" AutoEventWireup="true" CodeFile="MBizNotQuali.aspx.cs" Inherits="MBizNotQuali" MaintainScrollPositionOnPostback="true"  %>

<asp:Content ID="Header" ContentPlaceHolderID="HeaderContent" runat="Server">
    <div id="CLFRegHeader">
        <%--row pl-3 pt-3 pb-3--%>
        <%--<center style="width:100%;height:100%">--%>

        <%-- </center>--%>
    </div>
</asp:Content>
<asp:Content ID="MBizNotQuali" ContentPlaceHolderID="BodyContent" runat="server">
    <form id="eRaffleDetailsForm" runat="server">
        
        <div class="row" id="voucherDivBody" align="center" style="margin-top: 5%;">
            <h1 style="font-size:40px">Thank you Ka-Cebuana!</h1>
            &nbsp
            <h2 style="font-size:20px">Your property may not be qualified for Microbiz Protek, you may</h2>
            <h2 style="font-size:20px">still inquire with our sales team for other Cebuana insurance</h2>
            <h2 style="font-size:20px">products that may suite your property.</h2>
            &nbsp
            &nbsp
            <h2 style="font-size:20px">Please contact:</h2>
            <h2 style="font-size:20px">Email: <asp:Label ID="emailLbl" runat="server"></asp:Label></h2>
            <h2 style="font-size:20px">Mobile: (0968)856.54.59</h2>

          <br />
            <asp:Button ID="btnContinue" runat="server" Text="Done" OnClick="btnContinue_Click" Width="120" Height="40"  />

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

