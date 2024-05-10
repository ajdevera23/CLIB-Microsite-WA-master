<%@ Page Language="C#" MasterPageFile="~/Dashboard.Master" AutoEventWireup="true" CodeFile="Menu.aspx.cs" Inherits="Menu" %>

<asp:Content ContentPlaceHolderID="bodyContentForm" runat="server">
    <form id="menuForm" runat="server">
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
                    <center>
                        <a href="<asp:Literal runat="server" Text="<%$ AppSettings : ProductCategoryPage%>"/>">
                        <img src="Images/Sale.png" />
                            </a>
                    <br /><br />
                    <p><b>Input Paid Client Information</b></p></center>
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

