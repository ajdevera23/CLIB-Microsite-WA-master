<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Dashboard.Master" CodeFile="ConfirmationPage.aspx.cs" Inherits="ConfirmationPage" %>

<asp:Content ContentPlaceHolderID="bodyContentTitle" runat="server">
<style>
    /* Common styles for all screen sizes */
    html, body {
        background-color: #00263E !important;
    }

    .bodyContent {
        background-color: #00263E;
        padding-bottom: 20px;
    }

    .StyledReceipt {
        background-color: #fff;
        width: 100%;
        position: relative;
        padding: 1rem;
        box-shadow: 0 -0.4rem 1rem -0.4rem rgba(0, 0, 0, 0.2);
    }

    .StyledReceipt::after {
        /* Your existing styles for the pseudo-element */
    }

  .animated-message {
        font-size:15px;
        animation-name: bounce; /* Name of the animation */
        animation-duration: 1s; /* Duration of the animation */
        animation-iteration-count: infinite; /* Infinite loop */
    }

    /* Keyframes for the bounce animation */
    @keyframes bounce {
        0%, 20%, 50%, 80%, 100% {
            transform: translateY(0); /* Initial and final position */
        }
        40% {
            transform: translateY(-10px); /* Bounce up */
        }
        60% {
            transform: translateY(-5px); /* Bounce down */
        }
    }
    /* Default font size for large screens */
    .product-name,
    .reference-number {
        font-size: 24px;
    }

       .responsive-text {
        font-size: 20px;
        line-height: 30px;
        padding:20px;
    }

       .responsive-h2 {
        font-size: 35px;
        line-height:2;
        font-weight:900;
    }
    /* Media query for medium screens */
    @media (max-width: 768px) {
        .product-name,
        .reference-number {
            font-size: 20px;
        }
             .responsive-text {
            font-size: 18px;
            line-height: 28px;
        }

            .responsive-h2 {
            font-size: 30px;
        }
    }

    /* Media query for small screens */
    @media (max-width: 576px) {
        .product-name,
        .reference-number {
            font-size: 16px;
        }
           .responsive-text {
            font-size: 16px;
            line-height: 26px;
        }
            .responsive-h2 {
            font-size: 25px;
            line-height:2;
            font-weight:900;
        }
    }

            .StyledReceipt {
  
          background-color: #fff;
          width: 100%;
          position: relative;
          padding: 1rem;
          box-shadow: 0 -0.4rem 1rem -0.4rem rgba(0, 0, 0, 0.2);
        }

        .StyledReceipt::after {
          background-image: linear-gradient(135deg, #fff 0.5rem, transparent 0),
          linear-gradient(-135deg, #fff 0.5rem, transparent 0);
          background-position: left-bottom;
          background-repeat: repeat-x;
          background-size: 1rem;
          content: '';
          display: block;
          position: absolute;
          bottom: -1rem;
          left: 0;
          width: 100%;
          height: 1rem;
           
        }

</style>

    <form id="headTitle"></form>
</asp:Content>

<asp:Content ContentPlaceHolderID="bodyContentForm" runat="server" style="background-color:#808080;">
    <div class="StyledReceipt">
        <form id="headBody" runat="server">
            <br />
            <h2 class="responsive-h2">Thank you Ka-Cebuana!</h2>
            <div class="contaier" id="lblCLBranch">
            <h3 class="responsive-text">
             <asp:Label ID="lblInsurancePolicyMessage" runat="server" Text=""></asp:Label>  
            </h3>
            </div>
            <br />
            <br />
            <br />
            <h5 class="product-name"><b><asp:Label ID="lbNameofInsured" runat="server" Text=""></asp:Label></b> <asp:Label ID="lblNameOfInsured" runat="server" Text=""></asp:Label></h5>
            <h5 class="product-name"><b> <asp:Label ID="lbProductName" runat="server" Text=""></asp:Label></b> <asp:Label ID="lblNameOfProduct" runat="server" Text=""></asp:Label></h5>
            <h5 class="reference-number"><b><asp:Label ID="lblReferenceNumber" runat="server" Text=""></asp:Label></b> <asp:Label ID="lblRTN" runat="server" Text=""></asp:Label></h5>
            <h5 class="reference-number"><b><asp:Label ID="lblPremiumText" runat="server" Text=""></asp:Label></b> <asp:Label ID="lblPremium" runat="server" Text=""></asp:Label></h5>

            <% if (bool.Parse(Session["SummaryIsValidFreeInsurance"].ToString()) == false && !string.IsNullOrEmpty(Session["SummaryFreeInsuranceProductName"].ToString())) { %>
            <hr />
            <h5 class="reference-number"><b><asp:Label runat="server" Text="Fee Insurance"></asp:Label></b> <asp:Label ID="FreeInsurance" runat="server" Text=""></asp:Label></h5>
            <h5 class="reference-number"><b><asp:Label runat="server" Text="COC Number"></asp:Label></b> <asp:Label ID="FreeInsuranceCOCNumber" runat="server" Text=""></asp:Label></h5>
            <%} %>
            <br />
            <h6 class="animated-message">You may take a screenshot of this page for easy payment reference</h6>
            <br />
            <asp:Button id="btnReturntoProductCategoryPage" Text="DONE" runat="server" OnClick="btnReturntoProductCategoryPage_Click" Width="150px" Height="50px" /><br /><br /><br />
        </form>
        <footer class="pb-3">
            <center>
                <p>© 2021 - Cebuana Lhuillier</p>
            </center>
        </footer>
    </div>
</asp:Content>
