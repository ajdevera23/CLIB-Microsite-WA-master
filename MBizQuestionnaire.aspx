<%@ Page Title="" Language="C#" MasterPageFile="~/ProductRegistration.master" AutoEventWireup="true" CodeFile="MBizQuestionnaire.aspx.cs" Inherits="MBizQuestionnaire" MaintainScrollPositionOnPostback="true"  %>

<asp:Content ID="Header" ContentPlaceHolderID="HeaderContent" runat="Server">
    <div id="CLFRegHeader">
        <%--row pl-3 pt-3 pb-3--%>
        <%--<center style="width:100%;height:100%">--%>

        <%-- </center>--%>
    </div>
</asp:Content>
<asp:Content ID="MicroBizQuesPageMain" ContentPlaceHolderID="BodyContent" runat="server">
    <form id="BizQuesDet" runat="server">
        <div class="row" id="voucherDiv" align="center">
        </div>
        <div class="row" id="voucherDivBody" align="center" style="margin-top: 5%;">
            <h4 style="color:white; background-color:black;"> Business Questionnaire</h4>
            <table style="margin-left: 3%; height: 30px; font-size: larger; margin-top:3%;" id="tblQuestionnaire ">
               
            <tr>
                <td>
                    <span class="text-required" style="color: black; width: 1000px;">1. *Is the property used for business/income generating/commercial purposes?</span>
                    <br />
                </td>       
            </tr>
            <tr>
                <td>
                    <input type="radio" id="y1" name="q1" value="Yes" style="margin-left:5%;" required="required"  runat="server" />
                    <label for="y1">Yes</label>
                    <input type="radio" id="n1" name="q1" value="No" style="margin-left:1%;" required="required" runat="server"/>
                    <label for="n1">No</label>
                </td>
            </tr>
            <tr>
                <td style="width: 1000px;">
                    <span class="text-required" style="color: black; width: 1000px;">2. *Is the property to be insure more than 50 meters(100 steps away) from a creek or any body of water?</span>
                    <br />
                </td>
            </tr>
            <tr>
                <td>
                    <input type="radio" id="y2" name="q2" value="Yes" style="margin-left:5%;" required="required" runat ="server" />
                    <label for="y2">Yes</label>
                    <input type="radio" id="n2" name="q2" value="No" style="margin-left:1%;" required="required" runat="server"/>
                    <label for="n2">No</label>
                </td>
            </tr>
            <tr>
                <td style="width: 1000px;">
                    <span class="text-required" style="color: black; width: 1000px;">3. *Is the property to be insured made of concrete walls and roofing is made of iron/steel/concrete?</span>
                    <br />
                </td>
            </tr>
            <tr>
                <td>
                    <input type="radio" id="y3" name="q3" value="Yes" style="margin-left:5%;" required="required"  runat="server"/>
                    <label for="y3">Yes</label>
                    <input type="radio" id="n3" name="q3" value="No" style="margin-left:1%;" required="required" runat="server"/>
                    <label for="n3">No</label>
                </td>
            </tr>
            <tr>
                <td style="width: 1000px;">
                    <span class="text-required" style="color: black; width: 1000px;">4. *Is the property to be insured outside a public market?</span>
                    <br />
                </td>
            </tr>
            <tr >
                <td>            
                   
                    <asp:RadioButton ID="y4" runat="server" Text=" Yes" style="margin-left:5%;" GroupName="q4" required="required" OnCheckedChanged="y4_CheckedChanged" AutoPostBack="True"  />
                    <asp:RadioButton ID="n4" runat="server" Text=" No" style="margin-left:1%;" GroupName="q4" required="required" OnCheckedChanged="n4_CheckedChanged" AutoPostBack="True" />
                </td>
            </tr>            
            <asp:Panel ID="pnl4A"  runat="server" >
                <tr>
                    <td style="width: 1000px;">
                        <span class="text-required" style="color: black; width: 1000px; margin-left:5%;">4a.*Does the public market have sprinklers installed?</span>
                        <br />
                    </td>
                </tr>
                <tr style="margin-left:10px">
                    <td style="margin-left:10px">
                        <input type="radio" id="y5" name="q5" value="Yes" style="margin-left:10%;" runat="server" required="required"/>
                        <label for="y5">Yes</label>
                        <input type="radio" id="n5" name="q5" value="No" style="margin-left:1%" runat="server" required="required"/>
                        <label for="n5">No</label>
                    </td>
                </tr>
            </asp:Panel>
        </table>
           
        <h4>
            <asp:Button ID="btnPrevious" runat="server" Text="Previous" OnClick="btnPrevious_Click" Width="120" Height="35" Enabled="true" />

            <asp:Button ID="btnNext" runat="server" Text="Next" OnClick="btnNext_Click" Width="100" Height="35" Enabled="true" />
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
    <script>        
         function text_changed(txtObj) {

            var birthDate = new Date(txtObj.value);
            var birthDateYear = birthDate.getFullYear();
            var dateNow = new Date();
            var dateNowYear = dateNow.getFullYear();
            var age = dateNowYear - birthDateYear;
            var elem = document.getElementById("infoForm_guardianDiv");

            if (birthDateYear.value < 1753 || birthDate.value > 9999) {
                ae input correct year");
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

       lert("Pleas function characterAndNumbers(e) {
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

