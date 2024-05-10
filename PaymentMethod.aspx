<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Dashboard.Master" CodeFile="PaymentMethod.aspx.cs" Inherits="ConfirmationPage" %>

<asp:Content ContentPlaceHolderID="bodyContentTitle" runat="server">

    <form id="form1" runat="server">
        <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Button" />

    <link href="Style/paymentmethod.css" rel="stylesheet" />
    </form>
</asp:Content>

<asp:Content ContentPlaceHolderID="bodyContentForm" runat="server" style="background-color:#808080;">


	    <section class="policy-section">
	<div class="policy-steps">
		<div class="row">
			<div class="col-12 text-center policy-header">
				<h3>Choose Payment Method</h3>
			</div>
		</div>
		<div class="row mt-2rem">
			<div class="col-12">
				<label class="label-font">eWallet</label>
			</div>
			<div class="col-12">
				<div class="eWallet-box">
					<div class="form-check">
						<input class="payment-gateway" type="radio" name="gcash" id="flexRadioDefault1" value="GCASH" v-model="payment.payment_method">
						<label class="form-check-label payment-gateway-label" for="flexRadioDefault1">GCash <span class="float-right"><img src="Images/payments/gcash.svg" class="payment-method-image" /></span></label>
					</div>
				</div>
				<div class="eWallet-box">
					<div class="form-check">
						<input class="payment-gateway" type="radio" name="paymaya" id="flexRadioDefault2" value="PAYMAYA" v-model="payment.payment_method">
						<label class="form-check-label payment-gateway-label" for="flexRadioDefault2">PayMaya <span class="float-right"><img src="Images/payments/paymaya.svg" class="payment-method-image" /></span></label>
					</div>
				</div>
				<div class="eWallet-box">
					<div class="form-check">
						<input class="payment-gateway" type="radio" name="grabpay" id="flexRadioDefault3" value="GRABPAY" v-model="payment.payment_method">
						<label class="form-check-label payment-gateway-label" for="flexRadioDefault3">GrabPay <span class="float-right"><img src="Images/payments/grabpay.svg" class="payment-method-image" /></span></label>
					</div>
				</div>
			</div>
		</div>
		<div class="row mt-2rem">
			<div class="col-12">
				<div class="form-check">
					<input class="payment-gateway" type="radio" name="credit_card" id="flexRadioDefault4" value="CREDIT_CARD" v-model="payment.payment_method">
					<label class="form-check-label payment-gateway-label" for="flexRadioDefault4">
						Credit / Debit Card 
						<span class="payment-gateway-sub">Mastercard & Visa card upon checkout.</span>
					</label>
				</div>
			</div>
		</div>
		<div class="row mt-2rem">
			<div class="col-12">
				<label class="label-font">Bank Transfer Direct Debit</label>
			</div>
			<div class="col-12">
				<div class="eWallet-box">
					<div class="form-check">
						<input class="payment-gateway" type="radio" name="bpi" id="flexRadioDefault5" value="DD_BPI" v-model="payment.payment_method">
						<label class="form-check-label payment-gateway-label" for="flexRadioDefault5">BPI <span class="float-right"><img src="Images/payments/bpi.svg" class="payment-method-image" /></span></label>
					</div>
				</div>
				<div class="eWallet-box">
					<div class="form-check">
						<input class="payment-gateway" type="radio" name="unionbank" id="flexRadioDefault6" value="DD_UBP" v-model="payment.payment_method">
						<label class="form-check-label payment-gateway-label" for="flexRadioDefault6">Unionbank <span class="float-right"><img src="Images/payments/unionbank.svg" class="payment-method-image" /></span></label>
					</div>
				</div>
			</div>
		</div>

		<div class="row mt-1per">
			<div class="col-6">
				<button class="btn btn-outlined continue-button" @click="goBack">Back</button>
			</div>
			<div class="col-6">
		

			</div>
		</div>
	</div>
    </section>
</asp:Content>
