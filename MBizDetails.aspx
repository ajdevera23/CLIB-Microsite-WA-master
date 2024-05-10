<%@ Page Title="" Language="C#" MasterPageFile="~/ProductRegistration.master" AutoEventWireup="true" CodeFile="MBizDetails.aspx.cs" Inherits="MBizDetails" MaintainScrollPositionOnPostback="true"  %>

<asp:Content ID="Header" ContentPlaceHolderID="HeaderContent" runat="Server">
    <div id="CLFRegHeader">
        <%--row pl-3 pt-3 pb-3--%>
        <%--<center style="width:100%;height:100%">--%>

        <%-- </center>--%>
    </div>
</asp:Content>
<asp:Content ID="MicroBizDetsPageMain" ContentPlaceHolderID="BodyContent" runat="server">
    <form id="BizDets" runat="server">
        <div class="row" id="voucherDiv" align="center">
        </div>
        <div class="row" id="voucherDivBody" align="center" style="margin-top: 5%;">
            <h4 style="color:white; background-color:black;"> Business Details</h4>
            <table style="margin-left: 3%; height: 30px; font-size: larger; margin-top:3%;" id="tblBizDets ">
            <tr>
                <td>
                    <span class="text-required" style="color: black; ">* Amount to be insured
                         <input type="radio" id="ten" name="Amount" value="10000" style="margin-left:5%" runat="server" required="required"/>
                            <label for="ten">Php 10,000</label>
                          <input type="radio" id="twenty" name="Amount" value="25000" style="margin-left:2%" runat="server" required="required"/>
                          <label for="twenty">Php 25,000</label>
                          <input type="radio" id="fifty" name="Amount" value="50000" style="margin-left:2%" runat="server" required="required"/>
                          <label for="fifty">Php 50,000</label>
                    </span>
                </td>
            </tr>
            <tr>
                <td style="width:1000px">
                    <span class="text-required" style="color: black; ">* Name of Business</span>
                    <span class="text-required" style="color: black; margin-left:40%">* Date Started</span>
                </td>       
               
            </tr>
            <tr>
                <td>
                    <span class="text-required" style="color: black; ">
                        <input type ="text" runat="server" name="businessName" id="businessName" style="width:50%" required="required" />
                    </span>
                    <span class="text-required" style="color: black; margin-left:6%">
                        <input id="startDateTextBox" runat="server"  min="1753-01-01" max="9999-12-31"/>
                    </span>
                </td>       
                <td>
                    
                </td>
            </tr>
           <tr>
                <td style="width:800px">
                    <span class="text-required" style="color: black; ">* Type of Business</span>
                    <span class="text-required" style="color: black; margin-left:40%"">* Property Ownership</span>
                </td>
             
            </tr>
            <tr>
                <td>
                    <span class="text-required" style="color: black; ">
                        <input type ="text" runat="server" name="businessType" id="businessType" style="width:50%" 
                            placeholder="e.g. Sari-sari Store, motor repair shop" required="required" />
                    </span>
                    <span class="text-required" style="color: black; margin-left:6%" ">
                       <asp:DropDownList ID="DDOwnership" runat="server" 
                           Width="180px">
                            <asp:ListItem Text="Select" Value="Select" />
                            <asp:ListItem Text="Owned" Value="Owner" />
                            <asp:ListItem Text="Tenant" Value="Tenant" />
                       </asp:DropDownList>
                    </span>
                </td>              
            </tr>
            <tr>
                <td style="width:1000px">
                    <span class="text-required" style="color: black; ">* Address of Business</span>
                </td>
            </tr>
            <tr>
                <td>
                    <input type ="text" runat="server" name="businessAddress" id="businessAddress" style="width:74%" required="required" />
                </td>                
            </tr>
            <tr>
                <td style="width:1000px">
                    <span class="text-required" style="color: black; ">* Province</span>
                    <span class="text-required" style="color: black; margin-left:20% ">* City</span>
                    <span class="text-required" style="color: black; margin-left:24%">* Postal Code</span>

                </td>
            </tr>
            <tr>
                <td style="width:1000px">
                    <span class="text-required" style="color: black; ">
                       <asp:DropDownList ID="DDProvince" runat="server" 
                           OnSelectedIndexChanged="DDProvince_SelectedIndexChanged" AutoPostBack="True" Width="250px"></asp:DropDownList>
                    </span>
                    <span class="text-required" style="color: black; margin-left:3%">
                        <asp:DropDownList ID="DDcity" runat="server" Width="250px"></asp:DropDownList>
                    </span>
                    <span class="text-required" style="color: black; margin-left:2%">
                        <input id="postalCode" runat="server" required="required" style="width:180px" maxlength="4" minlength="4" />
                    </span>
                </td>
            </tr>
            <tr>
                <td>
                    <span class="text-required" style="color: black; ">Please provide the following files:</span>
                    <br />
                    <span class="text-required" style="color: black; margin-left:3%">1. * Photo of front of property with owner.</span>
                    <br />
                    <span class="text-required" style="color: black; margin-left:3%">2. * Photo of interior of property with owner.</span>
                    <br />
                    <span class="text-required" style="color: black; margin-left:3%; ">* Allowed formats are as follows: Image (*.jpg, *.jpeg)</span>
                    <br />
                    <span class="text-required" style="color: black; margin-left:7%;"> and attachment file size is up to 3MB per file.</span>
                    <br />
                    <br />   
                </td>
            </tr>
            
           
            
            <tr>   
                <td>                   

                    <span class="text-required" style="color: black;">
                        <%--<asp:ListBox ID="lstAttachedItems" runat="server" Height="150px" style="width:50%" BackColor="White"></asp:ListBox>--%>
                    </span>
                    <span class="text-required" style="color: black; margin-left:2%; display:inline-block; vertical-align:top;">
                    
                        Files Uploader:
                        <br />
                        <%--<asp:FileUpload ID="FileUpload2" runat="server" AllowMultiple="true"  />--%>
                         <input type="file" name="FileUpload2" id="FileUpload2" runat="server" class="btn btn-default" multiple="multiple" required="required"/>
                        <br />
                        * (Maximum of 2 photos. To upload multiple files, press CTRL and select the files)
                    </span> 
                    
                    
                    
                </td>
            </tr>
            <tr>               
                <td>
                    <%--Photo/s added:<asp:Label ID="countLbl" runat="server" Text="0"></asp:Label>--%>
                </td>
            </tr>


        </table>
       
           <br />
                <br />
                <br />
                <br />
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
   <%-- <script type="text/javascript" language="javascript">
    function showBrowseDialog() {
        var fileuploadctrl = document.getElementById('<%= FileUpload1.ClientID %>');
        fileuploadctrl.click();
    }

    function upload() {
        var btn = document.getElementById('<%= hideButton.ClientID %>');
        btn.click();
    }
</script>--%>
</asp:Content>

