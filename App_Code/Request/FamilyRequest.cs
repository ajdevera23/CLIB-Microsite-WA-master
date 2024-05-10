using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

public class DependentCollection 
{
    [JsonIgnore]
    public TextBox FirstnameID { get; set; }
    [JsonIgnore]
    public TextBox LastnameID { get; set; }
    [JsonIgnore]
    public TextBox BirthID { get; set; }
    public string DependentBirth { get; set; }
    public string DependentFirstName { get; set; }
    public string DependentLastName { get; set; }
    public string DependentName { get; set; }
    public string DependentRelationship { get; set; }
}

public class FamilyRequest:BaseRequest
{
    public long Limit { get; set; }
    public long Page { get; set; }


    public List<DependentCollection> DependentCollection { get; set; }



}

