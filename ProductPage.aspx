<%@ Page Language="C#" MasterPageFile="~/Dashboard.Master" AutoEventWireup="true" CodeFile="ProductPage.aspx.cs" Inherits="Public_ProductPage" %>

<asp:Content ContentPlaceHolderID="bodyContentTitle" runat="server">
<div class="row">
     <div class="col-md-12 mt-60 mx-md-auto">
         <div id="particles-js3"></div>
             <div class="picture-box pl-lg-5 pl-0" style="background: rgb(0,44,64); background: linear-gradient(112deg, rgba(0,44,64,1) 0%, rgba(0,44,68,1) 47%, rgba(116,175,187,1) 100%); border-radius: 50px;   box-shadow: 0 3px 6px rgba(0,0,0,0.16), 0 3px 6px rgba(0,0,0,0.23);">
                  <div class="row no-gutters align-items-center">
                  <div class="col-md-6 form-wrap-background-header">
                      <a href="#productForm">
                         <div class="form-wrap">
                   <%--        <img src="../Images/ProductLogoSample.png" runat="server" id="categoryImage"/>--%>
                             <div runat="server" id="categoryImage" class="image-container-through-div"></div>

                       </div>
                      </a>
                  </div>
                  <div class="col-md-6">
                       <div class="content-who-we-are">
                           <div class="pb-5 mt-5">
                              <h1 class="c-black"><asp:Label runat="server" ID="categoryLabel"></asp:Label></h1>
                           </div>
                       </div>
                  </div>           
                  </div>
            </div>
      </div>                               
</div>
</asp:Content>
    <asp:Content ContentPlaceHolderID="bodyContentForm" runat="server">
        <form id="productForm" runat="server">
            <asp:GridView id="productGridView" runat="server" GridLines="None" AutoGenerateColumns="False" >
                <Columns>
                  <asp:TemplateField>
                      <ItemTemplate>
                           <section class="m8-profile">
                            <div class="container">
                                <div class="row">
                                    <div class="col-md-12 mt-60 mx-md-auto">
                                        <div id="particles-js3"></div>
                                        <div class="picture-box pl-lg-5 pl-0" style="background-color:#00263E; border: 1px solid #00263E; border-radius: 50px;">
                                            <div class="row no-gutters align-items-center">
                                                <div class="col-md-6 form-wrap-background">
                                                    <div class="form-wrap">
                                                           <asp:Literal ID="Literal1" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"IconPath") %>'></asp:Literal>
                                                    </div>
                                                </div>
                                                <div class="col-md-6">
                                                    <div class="content-who-we-are">
                                                        <div class="pb-5 mt-5">
                                                            <h1 class="c-black"><asp:Label ID="ProductImage" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"ProductName") %>'></asp:Label></h1>

                                                            <p><asp:Label ID="Label3" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"ProductDescription") %>'></asp:Label></p>
                                                             <div style="text-align:left">
                                                            <%--  <a href="<%=ConfigurationManager.AppSettings["LearnMoreLink"]%>" target="_blank" ID="link1">Learn More</a>--%>
                                                            </div>
                                                              <div style="text-align:right; padding-right:20px;">
                                                             <%-- <h1><asp:Label ID="Label5" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"IntegrrationId") %>'></asp:Label></h1>--%>
                                                       
                                                              <%if (Session["CategoryCodeForHidingPrice"].ToString() != "TRI")
                                                                  { %>
                                                              <h1>₱ <asp:Label ID="Label4" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"SRP") %>'></asp:Label></h1>
                                                               <%} %>
                                                              </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </section>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
       </asp:GridView>
           <div><br /><br /><br />
               <h6 id="label1" runat="server"></h6>
           </div>
    </form>
    <script src="<%= ResolveUrl("~/Scripts/particles/particles.js") %>"></script>
    <script src="<%= ResolveUrl("~/Scripts/particles/particlesdesign.js") %>"></script>
 </asp:Content>
