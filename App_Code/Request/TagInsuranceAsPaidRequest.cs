using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public class PaymentDetails
{
    public string DatePaid { get; set; }
    public string NotificationDate { get; set; }
    public long NumberOfCOCsPaid { get; set; }
    public string ORNumber { get; set; }
    public double PaymentGatewayFee { get; set; }
    public string PaymentMethod { get; set; }
    public string PaymentNotes { get; set; }
    public string PaymentOption { get; set; }
    public string PaymentOrigin { get; set; }
    public string PaymentReferenceNo { get; set; }
    public double ProductAmount { get; set; }
    public string ReferenceNo { get; set; }
    public double TotalAmountPaid { get; set; }
    public string TransactionCheckNumber { get; set; }
}

public class TagInsuranceAsPaidRequest
{
    public string Token { get; set; }
    public bool isActive { get; set; }
    public string PlatformKey { get; set; }
    public PaymentDetails PaymentDetails { get; set; }
    public string ReferralCode { get; set; }
    public bool IsValidFreeInsurance { get; set;}
}

