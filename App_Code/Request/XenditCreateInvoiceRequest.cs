using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public class  CIAddress
{
    public string city { get; set; }
    public string country { get; set; }
    public string postal_code { get; set; }
    public string state { get; set; }
    public string street_line1 { get; set; }
    public string street_line2 { get; set; }
}

public class CICustomer
{
    public string given_names { get; set; }
    public string surname { get; set; }
    public string email { get; set; }
    public string mobile_number { get; set; }
    public List<CIAddress> addresses { get; set; }
}

public class CreateInvoiceRequest
{
    public string external_id { get; set; }
    public string description { get; set; }
    public string currency { get; set; }
    public decimal amount { get; set; }
    public CICustomer customer { get; set; }
    public List<string> payment_methods { get; set; }
    public string success_redirect_url { get; set; }
    public string failure_redirect_url { get; set; }
}

