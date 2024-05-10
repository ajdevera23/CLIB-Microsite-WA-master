<%@ Page Language="C#" MasterPageFile="~/Dashboard.Master" AutoEventWireup="true" CodeFile="UploadPage.aspx.cs" Inherits="UploadPage" %>

<asp:Content ContentPlaceHolderID="bodyContentForm" runat="server">
    <form runat="server">
        <div class="container">
            <div class="row">
                <div class="col-md-6 mb-4">
                    <center>
                        <a href="<asp:Literal runat="server" Text="<%$ AppSettings : UploadPage%>"/>">
                        <img src="Images/Upload.png" />
                            </a>
                    <br /><br />
                    <p><b>Upload Client Data</b></p>
                    </center>
                </div>
                <div class="col-md-6 mb-4">
                    <%--<button id="uploadBtn" onclick="toggleSMSFunction()">Upload Data with SMS Activation</button>--%>
                    <div id="col">
                        <br />
                        <br />
                        <br />
                        <asp:FileUpload ID="FileUpload1" runat="server" />
                        <br />
                        <br />
                        <asp:Button ID="uploadBtn" runat="server" class="btn btn-primary btn-block" Text="Upload" OnClick="SubmitBtn_Click" />
                           
                        <%--<button runat="server" type="submit" class="btn btn-primary btn-block" id="submitBtn" name="submitBtn" text="Submit">Submit</button>--%>
                    </div>
                    
                </div>
            </div>
        </div>
    </form>

     <hr />
            <footer class="pb-3">
            <center>
              <p>© 2020 - Cebuana Lhuillier</p>  
            </center>
        </footer>
    </asp:Content>

