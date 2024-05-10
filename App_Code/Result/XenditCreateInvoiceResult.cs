using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public class CIRAddress
{
    public string city { get; set; }
    public string country { get; set; }
    public string postal_code { get; set; }
    public string state { get; set; }
    public string street_line1 { get; set; }
    public string street_line2 { get; set; }
}

public class CIRAvailableEwallet
{
    public string ewallet_type { get; set; }
}

public class CIRCustomer
{
    public string given_names { get; set; }
    public string surname { get; set; }
    public string email { get; set; }
    public string mobile_number { get; set; }
    public List<CIRAddress> addresses { get; set; }
}

public class CreateInvoiceResult
{
    public string id { get; set; }
    public string external_id { get; set; }
    public string user_id { get; set; }
    public string status { get; set; }
    public string merchant_name { get; set; }
    public string merchant_profile_picture_url { get; set; }
    public decimal amount { get; set; }
    public string description { get; set; }
    public DateTime expiry_date { get; set; }
    public string invoice_url { get; set; }
    public List<object> available_banks { get; set; }
    public List<object> available_retail_outlets { get; set; }
    public List<CIRAvailableEwallet> available_ewallets { get; set; }
    public List<object> available_qr_codes { get; set; }
    public List<object> available_direct_debits { get; set; }
    public List<object> available_paylaters { get; set; }
    public bool should_exclude_credit_card { get; set; }
    public bool should_send_email { get; set; }
    public string success_redirect_url { get; set; }
    public string failure_redirect_url { get; set; }
    public DateTime created { get; set; }
    public DateTime updated { get; set; }
    public string currency { get; set; }
    public CIRCustomer customer { get; set; }
    public string error_code { get; set; }
    public string message { get; set; }
}

