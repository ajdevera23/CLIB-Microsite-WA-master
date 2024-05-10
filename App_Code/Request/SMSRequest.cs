using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


    public class SMSRequest
    {
        public string Token { get; set; }
        public string CocNumber { get; set; }
        public string PlatformKey { get; set; }
        public string ReferenceNumber { get; set; }
        public SendSMSDetails SendSMSDetails { get; set; }
    }

    public class SendSMSDetails
    {
        public string Content { get; set; }
        public bool IsInternational { get; set; }
        public string MobileNumber { get; set; }
    }



