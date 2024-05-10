using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

public class HealthDeclarationRequest:PetRequest
{
    public string PhysicalDeformity { get; set; }
    public string PhysicalDeformityDetails { get; set; }
    public string PreExistingIllness { get; set; }
    public string PreExistingIllnessDetails { get; set; }

}


