using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Web.UI.WebControls;

    public class Nationality
    {
        [DataMember(Name = "num_code")]
        public string num_code { get; set; }



        [DataMember(Name = "alpha_2_code")]
        public string alpha_2_code { get; set; }



        [DataMember(Name = "alpha_3_code")]
        public string alpha_3_code { get; set; }



        [DataMember(Name = "en_short_name")]
        public string en_short_name { get; set; }



        [DataMember(Name = "nationality")]
        public string nationality { get; set; }

}
