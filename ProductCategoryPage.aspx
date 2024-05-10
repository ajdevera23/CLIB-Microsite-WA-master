<%@ Page Language="C#" MasterPageFile="~/Dashboard.Master" AutoEventWireup="true" CodeFile="ProductCategoryPage.aspx.cs" Inherits="Public_ProductCategoryPage" %>
    <asp:Content ContentPlaceHolderID="bodyContentForm" runat="server">
        <div style="margin:auto;">
        <form id="productCategoryForm" runat="server">
             <div class="categoryDivClass" id="categoryDiv">
                 <asp:Literal ID="table" runat="server" />
            </div> 
           <asp:Label ID="ErrorMessage" runat="server" Text=""></asp:Label>
        </form>
            &nbsp
            <hr />
            <footer class="pb-3">
            <center>
              <p>© 2020 - Cebuana Lhuillier</p>  
            </center>
        </footer>
            </div>
    </asp:Content>
 
