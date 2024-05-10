<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Dashboard.Master"  CodeFile="ProductDetails.aspx.cs" Inherits="ProductDetails" %>


<asp:Content ContentPlaceHolderID="bodyContentTitle" runat="server">
 <link href="<%= ResolveUrl("~/Style/productDetails.css") %>" rel="stylesheet" />
 <link href="<%= ResolveUrl("~/JScript/Plugins/PDFViewer/pdf-main.css") %>" rel="stylesheet" />
<meta charset="UTF-8">
<meta name="viewport" content="width=device-width, initial-scale=1.0">
</asp:Content>
<asp:Content ContentPlaceHolderID="bodyContentForm" runat="server">

<% if (Session["IsPDFAvailable"].ToString() == "Available") {%>
      <button id="zoom-in"><i class='bx bx-zoom-in' ></i></button>
      <div id="zoom-percent">60</div>
      <button id="zoom-out"><i class='bx bx-zoom-out' ></i></button>
      <button id="zoom-reset"><i class='bx bx-reset' ></i></button>
      <div id="pages"></div>

<%} %>
<% if (Session["IsPDFAvailable"].ToString() == "Not Available") {%>
      <div class="container justify-content-center text-center mb-5">
      <br /><br /><br />
      <h1><b><i class='bx bx-image' style="font-size:80px;"></i></b></h1>
      <h1> <b>Oops! Product Profile Not Found</b></h1>
      <br />
      <p>We're sorry, but it seems that the profile for the product you're looking for cannot be located at the moment.<br /> This might be due to various reasons, including updates or changes in our system.</p>
      <p>Our team is already on it to ensure that you get the information you need.<br /> Please check back later as we strive to resolve this issue promptly.</p>
    </div>

<%} %>

<div class="navbar-bottom">
 <% if (Session["ProductCode"].ToString() == "FPGIT" || Session["ProductCode"].ToString() == "FPGDT") { %>
 <button class="plus" onclick="openModal()">
    Purchase
</button>
<%} %>
<%else { %>

  <button class="plus" onclick="window.location.href='<%=Server.HtmlEncode(Session["CheckEligibility"].ToString())%>';">
     Purchase
 </button>
<%} %>
</div>

  <script src="<%= ResolveUrl("~/JScript/Plugins/PDFViewer/pdf.js") %>"></script>
  <script src="<%= ResolveUrl("~/JScript/Plugins/PDFViewer/pdf.worker.js") %>"></script>
  <script>
      pdfjsLib.GlobalWorkerOptions.workerSrc = 'JScript/Plugins/PDFViewer/pdf.worker.js';

      // Simulate file input change with a base64 encoded PDF
      var base64PDF = "<%= Session["pdfBase64"] %>";


      var bytes = window.atob(base64PDF);
      var byteNumbers = new Array(bytes.length);
      for (var i = 0; i < bytes.length; i++) {
          byteNumbers[i] = bytes.charCodeAt(i);
      }
      var byteArray = new Uint8Array(byteNumbers);

      pdfjsLib.getDocument(byteArray).promise.then(function (pdf) {
          for (var i = 0; i < pdf.numPages; i++) {
              (function (pageNum) {
                  pdf.getPage(i + 1).then(function (page) {
                      var viewport = page.getViewport(2.0);
                      var pageNumDiv = document.createElement("div");
                      pageNumDiv.className = "pageNumber";
                      pageNumDiv.innerHTML = "Page " + pageNum;
                      var canvas = document.createElement("canvas");
                      canvas.className = "page";
                      canvas.title = "Page " + pageNum;
                      document.querySelector("#pages").appendChild(pageNumDiv);
                      document.querySelector("#pages").appendChild(canvas);
                      canvas.height = viewport.height;
                      canvas.width = viewport.width;

                      page.render({
                          canvasContext: canvas.getContext('2d'),
                          viewport: viewport
                      }).promise.then(function () {
                          console.log('Page rendered');
                      });
                      page.getTextContent().then(function (text) {
                          console.log(text);
                      });
                  });
              })(i + 1);
          }
      });

      // Add zoom functionality
      var curWidth = 60;
      function zoomIn() {
          if (curWidth < 150) {
              curWidth += 10;
              document.querySelector("#zoom-percent").innerHTML = curWidth;
              document.querySelectorAll(".page").forEach(function (page) {
                  page.style.width = curWidth + "%";
              });
          }
      }
      function zoomOut() {
          if (curWidth > 20) {
              curWidth -= 10;
              document.querySelector("#zoom-percent").innerHTML = curWidth;
              document.querySelectorAll(".page").forEach(function (page) {
                  page.style.width = curWidth + "%";
              });
          }
      }
      function zoomReset() {
          curWidth = 60;
          document.querySelector("#zoom-percent").innerHTML = curWidth;
          document.querySelectorAll(".page").forEach(function (page) {
              page.style.width = curWidth + "%";
          });
      }
      document.querySelector("#zoom-in").onclick = zoomIn;
      document.querySelector("#zoom-out").onclick = zoomOut;
      document.querySelector("#zoom-reset").onclick = zoomReset;
      window.onkeypress = function (e) {
          if (e.code == "Equal") {
              zoomIn();
          }
          if (e.code == "Minus") {
              zoomOut();
          }
      };
  </script>

<!--MODAL CONTAINER-->
<section>
    <div class="overlay-modal-travel" id="modalOverlay">
    <div class="modal-travel">
        <button class="close-btn-travel" onclick="closeModal()">&times;</button>
        <h5><center>Select Plan Type</center></h5>
        <div class="mt-5" style="font-size:12px;">
        <div id="productPlaceholder">
            <div class="modal-content">
                  <%= placeholderHtml %>
            </div>
        </div>
        </div>
    
</div>
        </div>
</section>
<script src="<%= ResolveUrl("~/JScript/productDetails.js") %>"></script>
</asp:Content>