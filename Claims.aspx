<%@ Page Title="" Language="C#" MasterPageFile="~/Claims.master" AutoEventWireup="true" CodeFile="Claims.aspx.cs" Inherits="ClientReferral" MaintainScrollPositionOnPostback="true" %>

<asp:Content ContentPlaceHolderID="infoForm" runat="server">
    <form id="enrollmentForm" class="container body-content container-enrollment" method="post" autocomplete="off" runat="server" novalidate>
        <div class="container my-2">
            <div class="text-center mt-2">
                <h1>Welcome to Cebuana Lhuillier Insurance Brokers Online</h1>
                <p class="lead">Self-Service Claims Management System</p>
                <p>Please select your service</p>
                <div class="row justify-content-center">
                    <div class="col-md-6 mt-4">
                        <div class="service-box disabled">
                            <div class="icon-container position-relative">
                                <span class="coming-soon-text">Coming Soon</span>
                            </div>
                            <h5 class="service-title">New Claims Application</h5>
                        </div>
                    </div>
                   <div class="col-md-6 mt-4">
                       <a href="#" >
                       <div class="service-box active">

                     
                              <div class="icon-container position-relative">
                                    <svg xmlns="http://www.w3.org/2000/svg" width="100" height="100" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" class="feather feather-file-text">
                                        <path d="M14 2H6a2 2 0 0 0-2 2v16a2 2 0 0 0 2 2h12a2 2 0 0 0 2-2V8z"></path>
                                        <polyline points="14 2 14 8 20 8"></polyline>
                                        <line x1="16" y1="13" x2="8" y2="13"></line>
                                        <line x1="16" y1="17" x2="8" y2="17"></line>
                                        <polyline points="10 9 9 9 8 9"></polyline>
                                    </svg>
                                </div>
                            <h5 class="service-title">Claims Documents Submission</h5>
                        </div>
                      </a>

                    </div>
                      <div class="col-md-12 mt-4">
                        <div class="service-box disabled">
                            <div class="icon-container position-relative">
                                <span class="coming-soon-text">Coming Soon</span>
                            </div>
                            <h5 class="service-title">How to Claim</h5>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>
</asp:Content>
