using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

public class PetRequest:BaseRequest
{
    public string PetName { get; set; }
    public string PetBreed { get; set; }
    public string PetColor { get; set; }
    public string PetGender { get; set; }
    public string PetBirth { get; set; }
    public int PetAgeYear { get; set; }
    public int PetAgeMonth { get; set; }
    public string PetPedigree { get; set; }
    public string PetRFID { get; set; }
    //public string PetFunction { get; set; }
    public string PetYearlyTreatment { get; set; }
    public string PetHistory { get; set; }
    public string PetHistoryDetails { get; set; }
    public string PetVitamins { get; set; }
    public string PetVitaminsDetails { get; set; }
    public string PetYearlyTreatmentDetails { get; set; }
    public string PetCategory { get; set; }

}


