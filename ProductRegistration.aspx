<%@ Page Language="C#" MasterPageFile="~/ProductRegistration.Master" AutoEventWireup="true" CodeFile="ProductRegistration.aspx.cs" Inherits="Public_ProductRegistration" %>

<asp:Content ID="ProductRegistrationPage" ContentPlaceHolderID="BodyContent" runat="server">
    <form id="voucherForm"    runat="server">
        <div class="row" id="voucherDiv" align="center">
            <div class="col-12 col-md-12">
            <h1 id="welcome" >Welcome Ka-Cebuana!</h1>
                <br />
            <h5 id="enterVoucherCode" >Enter your voucher code</h5>
            </div>
        </div>
         
        <div class="row" id="voucherDivBody" align="center">
            
                <div class="col-12 col-md-4" >
                    <%--<asp:TextBox ID="voucherCode" Text=" " class="form-control voucherInput" MaxLength="20"  runat="server" TextMode="SingleLine" />--%>
   
                    <input type="text" class="form-control" id="voucherCode" name="voucherCode"  placeholder="Please enter your voucher code here" maxlength="21" min="12" runat="server" style="text-align:center;" />
                    <div id="main">
                        <br />
                        <asp:Image ID="captchaImage" runat="server" Style="width:80%;" />
                        <br />
                        <asp:LinkButton ID="generateNewCaptcha" runat="server" Font-Size="Small" Font-Italic="true" onclick="generateNewCaptcha_Click">Generate new code</asp:LinkButton><br />
                        <br />
                        <p style="font-size:0.9rem; color:#ffffff;">Type the characters above in the field below</p>
                        <input type="text" id="captchaText" class="form-control" placeholder="Please answer captcha" runat="server" style="text-align:center;"></input>
                            
                        <br />
                        <%--<asp:Button ID="validateCaptchaButton" CssClass="btn" runat="server" Text="Validate Captcha" onclick="validateCaptchaButton_Click" />--%>
                    </div>
                     </div>
                <%--<br /><div class="g-recaptcha" id="captcha" runat="server" data-sitekey="<%$ AppSettings : DataSiteKey %>" data-callback="recaptcha_callback" ></div><br />--%>
                
            
            <div class="">
                <div class="col-12 col-md-8" align="center">
                    <asp:Button runat="server" type="submit" class="btn btn-info btnSubmit" id="productRegBtn" name="productRegBtn" text="Submit" OnClick="Submit_Click"  ></asp:Button>
                </div>
            </div>
        </div>
        
    </form>
    <br />
    <footer class="pb-3">
            <center>
              <p>© 2020 - Cebuana Lhuillier</p>  
            </center>
    </footer>
</asp:Content>
<asp:Content ContentPlaceHolderID="ScriptContent" runat="server">    <%--<script type="text/javascript">
        $(document).ready(function (){
            $("#productRegBtn").on("click", function (e) {
                var voucherCode = $("#voucherCode").val();
                if (voucherCode.length == 0) {
                    e.preventDefault();
                    alert("Please enter your voucher code.");
                }
                else if (voucherCode.length > 12 || voucherCode.length < 12) {
                    e.preventDefault();
                    alert("Invalid voucher code.");
                }
                
            })
        });
       
    </script> --%>
</asp:Content>