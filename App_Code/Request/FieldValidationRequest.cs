using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

// Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
public class DependentCollections
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

public class FieldValidationRequest
{
    public string Token { get; set; }
    public string Address { get; set; }
    public string Barangay { get; set; }
    public string BeneficiaryName { get; set; }
    public string BeneficiaryRelationship { get; set; }
    public string Birthdate { get; set; }
    public decimal BodyOfWaterDistance { get; set; }
    public string BookingReferenceNumber { get; set; }
    public string BoundaryFront { get; set; }
    public string BoundaryLeft { get; set; }
    public string BoundaryRear { get; set; }
    public string BoundaryRight { get; set; }
    public string CategoryCode { get; set; }
    public string City { get; set; }
    public string CivilStatus { get; set; }
    public string DateTimeFormat { get; set; }
    public List<DependentCollections> DependentCollection { get; set; }
    public string Destination { get; set; }
    public string EmailAddress { get; set; }
    public string EmployerBusinessName { get; set; }
    public string FirstName { get; set; }
    public decimal FloorArea { get; set; }
    public string Gender { get; set; }
    public string GuardianBirthday { get; set; }
    public string GuardianContactNo { get; set; }
    public string GuardianName { get; set; }
    public string GuardianRelationship { get; set; }
    public string IDNumber { get; set; }
    public long IntegrationId { get; set; }
    public bool IsCovidCoverage { get; set; }
    public bool IsExist { get; set; }
    public bool IsWithVoucher { get; set; }
    public string LastName { get; set; }
    public string MiddleName { get; set; }
    public string MobileNumber { get; set; }
    public string MortgageeName { get; set; }
    public string NameOfResident { get; set; }
    public string Nationality { get; set; }
    public string NatureOfOccupancy { get; set; }
    public string NatureOfWork { get; set; }
    public long NoOfStoreys { get; set; }
    public long NumberOfCOCs { get; set; }
    public string Occupation { get; set; }
    public string OwnershipStatus { get; set; }
    public string PartnerCode { get; set; }
    public string PassportNumber { get; set; }
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
    public string PhysicalDeformity { get; set; }
    public string PhysicalDeformityDetails { get; set; }
    public string PreExistingIllness { get; set; }
    public string PreExistingIllnessDetails { get; set; }
    public string PetCategory { get; set; }
    public string PlaceOfBirth { get; set; }
    public string PlatformKey { get; set; }
    public string PlatformName { get; set; }
    public string PreviousLossStatus { get; set; }
    public string ProductCode { get; set; }
    public string PropertyAddress { get; set; }
    public long PropertyAge { get; set; }
    public string PropertyCity { get; set; }
    public string PropertyMortgageStatus { get; set; }
    public string ProviderCode { get; set; }
    public string PropertyProvince { get; set; }
    public string Province { get; set; }
    public string PurposeOfTravel { get; set; }
    public string ReferenceNumber { get; set; }
    public string SecondaryProductCode { get; set; }
    public string SourceOfFunds { get; set; }
    public string Suffix { get; set; }
    public string TinId { get; set; }
    public int TravelDurationDays { get; set; }
    public string TravelDurationFrom { get; set; }
    public string TravelDurationTo { get; set; }

    public string TravelOrigin { get; set; }
    public string TypeBeams { get; set; }
    public string TypeColumns { get; set; }
    public string TypeExteriorWalls { get; set; }
    public string TypeInnerPartitions { get; set; }
    public string TypeOfHome { get; set; }
    public string TypeRoof { get; set; }
    public string UserId { get; set; }
    public string ValidID { get; set; }
    public string VisaType { get; set; }
    public string VoucherCode { get; set; }
    public string ZipCode { get; set; }
}

