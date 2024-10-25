using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for GetList
/// </summary>
public class GetList
{
    public GetList()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public IList<String> GetProductList(TokenRequest token)
    {
        IList<String> returnValue;

        string method = "GetProductList";

        string jsonRequest = Newtonsoft.Json.JsonConvert.SerializeObject(token);

        string jsonResult = SystemUtility.JsonHttpPost(jsonRequest, ConfigurationManager.AppSettings["CLIBMicrositeWS"].Trim() + method);

        returnValue = Newtonsoft.Json.JsonConvert.DeserializeObject<IList<String>>(jsonResult);
        
        return returnValue;
    }

    public IList<String> GetPartnerList(TokenRequest token)
    {
        IList<String> returnValue;

        string method = "GetPartnerList";

        string jsonRequest = Newtonsoft.Json.JsonConvert.SerializeObject(token);

        string jsonResult = SystemUtility.JsonHttpPost(jsonRequest, ConfigurationManager.AppSettings["CLIBMicrositeWS"].Trim() + method);

        returnValue = Newtonsoft.Json.JsonConvert.DeserializeObject<IList<String>>(jsonResult);

        return returnValue;
    }

    public bool ifReferenceCodeExists(TokenRequest token)
    {
        bool returnValue;

        string method = "ifReferenceCodeExists";

        string jsonRequest = Newtonsoft.Json.JsonConvert.SerializeObject(token);

        string jsonResult = SystemUtility.JsonHttpPost(jsonRequest, ConfigurationManager.AppSettings["CLIBMicrositeWS"].Trim() + method);

        returnValue = Newtonsoft.Json.JsonConvert.DeserializeObject<bool>(jsonResult);

        return returnValue;
    }

    public bool ifReferenceCodeIsUsed(TokenRequest token)
    {
        bool returnValue;

        string method = "ifReferenceCodeIsUsed";

        string jsonRequest = Newtonsoft.Json.JsonConvert.SerializeObject(token);

        string jsonResult = SystemUtility.JsonHttpPost(jsonRequest, ConfigurationManager.AppSettings["CLIBMicrositeWS"].Trim() + method);

        returnValue = Newtonsoft.Json.JsonConvert.DeserializeObject<bool>(jsonResult);

        return returnValue;
    }

    public bool ifReferenceNumberExists(TokenRequest token)
    {
        bool returnValue;

        string method = "ifReferenceNumberExists";

        string jsonRequest = Newtonsoft.Json.JsonConvert.SerializeObject(token);

        string jsonResult = SystemUtility.JsonHttpPost(jsonRequest, ConfigurationManager.AppSettings["CLIBMicrositeWS"].Trim() + method);

        returnValue = Newtonsoft.Json.JsonConvert.DeserializeObject<bool>(jsonResult);

        return returnValue;
    }

    public bool ifProductCodeBaseOnIntegrationMappingExists(TokenRequest token)
    {
        bool returnValue;

        string method = "ifProductCodeBaseOnIntegrationMappingExists";

        string jsonRequest = Newtonsoft.Json.JsonConvert.SerializeObject(token);

        string jsonResult = SystemUtility.JsonHttpPost(jsonRequest, ConfigurationManager.AppSettings["CLIBMicrositeWS"].Trim() + method);

        returnValue = Newtonsoft.Json.JsonConvert.DeserializeObject<bool>(jsonResult);

        return returnValue;
    }


    public string GetProductImagePath(TokenRequest token)
    {
        string returnValue;

        string method = "GetProductImagePath";

        string jsonRequest = Newtonsoft.Json.JsonConvert.SerializeObject(token);

        string jsonResult = SystemUtility.JsonHttpPost(jsonRequest, ConfigurationManager.AppSettings["CLIBMicrositeWS"].Trim() + method);

        returnValue = Newtonsoft.Json.JsonConvert.DeserializeObject<string>(jsonResult);

        return returnValue;
    }

    

    public string GetPartnerImagePath(TokenRequest token)
    {
        string returnValue;

        string method = "GetPartnerImagePath";

        string jsonRequest = Newtonsoft.Json.JsonConvert.SerializeObject(token);

        string jsonResult = SystemUtility.JsonHttpPost(jsonRequest, ConfigurationManager.AppSettings["CLIBMicrositeWS"].Trim() + method);

        returnValue = Newtonsoft.Json.JsonConvert.DeserializeObject<string>(jsonResult);

        return returnValue;
    }

    public string GetCategory(TokenRequest token)
    {
        string returnValue;

        string method = "GetCategory";

        string jsonRequest = Newtonsoft.Json.JsonConvert.SerializeObject(token);

        string jsonResult = SystemUtility.JsonHttpPost(jsonRequest, ConfigurationManager.AppSettings["CLIBMicrositeWS"].Trim() + method);

        returnValue = Newtonsoft.Json.JsonConvert.DeserializeObject<string>(jsonResult);

        return returnValue;
    }

    public IList<CategoryResult> PopulateProductCategoryGridView(TokenRequest token)
        {
        IList<CategoryResult> returnValue;

        string method = "PopulateProductCategoryGridView";

        string jsonRequest = Newtonsoft.Json.JsonConvert.SerializeObject(token);

        string jsonResult = SystemUtility.JsonHttpPost(jsonRequest, ConfigurationManager.AppSettings["CLIBMicrositeWS"].Trim() + method);

        returnValue = Newtonsoft.Json.JsonConvert.DeserializeObject<IList<CategoryResult>>(jsonResult);

        return returnValue;
    }

    public IList<ProductList> PopulateProductGridView(TokenRequest token)
    {
        IList<ProductList> returnValue;

        string method = "PopulateProductGridView";

        string jsonRequest = Newtonsoft.Json.JsonConvert.SerializeObject(token);

        string jsonResult = SystemUtility.JsonHttpPost(jsonRequest, ConfigurationManager.AppSettings["CLIBMicrositeWS"].Trim() + method);

        returnValue = Newtonsoft.Json.JsonConvert.DeserializeObject<IList<ProductList>>(jsonResult);

        return returnValue;
    }

    public IList<ProductList> PopulateProductDetails(TokenRequest token)
    {
        IList<ProductList> returnValue;

        string method = "PopulateProductDetails";

        string jsonRequest = Newtonsoft.Json.JsonConvert.SerializeObject(token);

        string jsonResult = SystemUtility.JsonHttpPost(jsonRequest, ConfigurationManager.AppSettings["CLIBMicrositeWS"].Trim() + method);

        returnValue = Newtonsoft.Json.JsonConvert.DeserializeObject<IList<ProductList>>(jsonResult);

        return returnValue;
    }

    public IList<ProductList> PopulateProductByCodesAndIntegrationID(TokenRequest token)
    {
        IList<ProductList> returnValue;

        string method = "PopulateProductByCodesAndIntegrationID";

        string jsonRequest = Newtonsoft.Json.JsonConvert.SerializeObject(token);

        string jsonResult = SystemUtility.JsonHttpPost(jsonRequest, ConfigurationManager.AppSettings["CLIBMicrositeWS"].Trim() + method);

        returnValue = Newtonsoft.Json.JsonConvert.DeserializeObject<IList<ProductList>>(jsonResult);

        return returnValue;
    }


    public IList<String> GetListValidIds(TokenRequest token)
    {
        IList<String> returnValue;

        string method = "GetListValidIds";

        string jsonRequest = Newtonsoft.Json.JsonConvert.SerializeObject(token);

        string jsonResult = SystemUtility.JsonHttpPost(jsonRequest, ConfigurationManager.AppSettings["CLIBMicrositeWS"].Trim() + method);

        returnValue = Newtonsoft.Json.JsonConvert.DeserializeObject<IList<String>>(jsonResult);

        return returnValue;
    }
    public IList<String> GetListRelationship(TokenRequest token)
    {
        IList<String> returnValue;

        string method = "GetListRelationship";

        string jsonRequest = Newtonsoft.Json.JsonConvert.SerializeObject(token);

        string jsonResult = SystemUtility.JsonHttpPost(jsonRequest, ConfigurationManager.AppSettings["CLIBMicrositeWS"].Trim() + method);

        returnValue = Newtonsoft.Json.JsonConvert.DeserializeObject<IList<String>>(jsonResult);

        return returnValue;
    }
    public Int64 GetIntegrationId(TokenRequest token)
    {
        Int64 returnValue;

        string method = "GetIntegrationId";

        string jsonRequest = Newtonsoft.Json.JsonConvert.SerializeObject(token);

        string jsonResult = SystemUtility.JsonHttpPost(jsonRequest, ConfigurationManager.AppSettings["CLIBMicrositeWS"].Trim() + method);

        returnValue = Newtonsoft.Json.JsonConvert.DeserializeObject<Int64>(jsonResult);

        return returnValue;
    }

    #region Client Referral
    public bool CheckIfBranchExists(TokenRequest token)
    {
        bool returnValue;

        string method = "CheckIfBranchExists";

        string jsonRequest = Newtonsoft.Json.JsonConvert.SerializeObject(token);

        string jsonResult = SystemUtility.JsonHttpPost(jsonRequest, ConfigurationManager.AppSettings["CLIBMicrositeWS"].Trim() + method);

        returnValue = Newtonsoft.Json.JsonConvert.DeserializeObject<bool>(jsonResult);

        return returnValue;
    }

    public TokenRequest RetrieveBranchDetails(TokenRequest token)
    {
        TokenRequest returnValue;

        string method = "RetrieveBranchDetails";

        string jsonRequest = Newtonsoft.Json.JsonConvert.SerializeObject(token);

        string jsonResult = SystemUtility.JsonHttpPost(jsonRequest, ConfigurationManager.AppSettings["CLIBMicrositeWS"].Trim() + method);

        returnValue = Newtonsoft.Json.JsonConvert.DeserializeObject<TokenRequest>(jsonResult);

        return returnValue;
    }

    public IList<String> GetListProvince(TokenRequest token)
    {
        IList<String> returnValue;

        string method = "GetListProvince";

        string jsonRequest = Newtonsoft.Json.JsonConvert.SerializeObject(token);

        string jsonResult = SystemUtility.JsonHttpPost(jsonRequest, ConfigurationManager.AppSettings["CLIBMicrositeWS"].Trim() + method);

        returnValue = Newtonsoft.Json.JsonConvert.DeserializeObject<IList<String>>(jsonResult);

        return returnValue;
    }
    public IList<String> GetListCity(TokenRequest token)
    {
        IList<String> returnValue;

        string method = "GetListCity";

        string jsonRequest = Newtonsoft.Json.JsonConvert.SerializeObject(token);

        string jsonResult = SystemUtility.JsonHttpPost(jsonRequest, ConfigurationManager.AppSettings["CLIBMicrositeWS"].Trim() + method);

        returnValue = Newtonsoft.Json.JsonConvert.DeserializeObject<IList<String>>(jsonResult);

        return returnValue;
    }

    public PurchaseResult CheckIfClientExistsIQRNetworld(CheckEligibilityRequest token)
    {
        PurchaseResult returnValue = new PurchaseResult();

        string jsonRequest = Newtonsoft.Json.JsonConvert.SerializeObject(token);

        string jsonResult = SystemUtility.JsonHttpPost(jsonRequest, ConfigurationManager.AppSettings["EndPointMiddleLayer"].Trim());

        returnValue = Newtonsoft.Json.JsonConvert.DeserializeObject<PurchaseResult>(jsonResult);

        return returnValue;
     }


    public ADCClientIfExistResult GetADCClientIfExist(ADCClientIfExistRequest token)
    {
        ADCClientIfExistResult returnValue = new ADCClientIfExistResult();

        string jsonRequest = Newtonsoft.Json.JsonConvert.SerializeObject(token);

        string jsonResult = SystemUtility.JsonHttpPost(jsonRequest, ConfigurationManager.AppSettings["GetADCClientIfExist"].Trim());

        returnValue = Newtonsoft.Json.JsonConvert.DeserializeObject<ADCClientIfExistResult>(jsonResult);

        return returnValue;
    }

    public AffiliateDetailsResult GetAffiliateDetails(AffiliateDetailsRequest token)
    {
        AffiliateDetailsResult returnValue = new AffiliateDetailsResult();

        string jsonRequest = Newtonsoft.Json.JsonConvert.SerializeObject(token);

        string jsonResult = SystemUtility.JsonHttpPost(jsonRequest, ConfigurationManager.AppSettings["GetAffiliateDetails"].Trim());

        returnValue = Newtonsoft.Json.JsonConvert.DeserializeObject<AffiliateDetailsResult>(jsonResult);

        return returnValue;
    }

    public GetClaimsIfExistResult GetClaimsIfExists(GetClaimsIfExistRequest token)
    {
        GetClaimsIfExistResult returnValue = new GetClaimsIfExistResult();

        string jsonRequest = Newtonsoft.Json.JsonConvert.SerializeObject(token);

        string jsonResult = SystemUtility.JsonHttpPost(jsonRequest, ConfigurationManager.AppSettings["GetClaimsIfExists"].Trim());

        returnValue = Newtonsoft.Json.JsonConvert.DeserializeObject<GetClaimsIfExistResult>(jsonResult);

        return returnValue;
    }

    public GetNatureofClaimResult GetNatureofClaimRequest(GetNatureofClaimRequest token)
    {
        GetNatureofClaimResult returnValue = new GetNatureofClaimResult();

        string jsonRequest = Newtonsoft.Json.JsonConvert.SerializeObject(token);

        string jsonResult = SystemUtility.JsonHttpPost(jsonRequest, ConfigurationManager.AppSettings["GetNatureofClaimRequest"].Trim());

        returnValue = Newtonsoft.Json.JsonConvert.DeserializeObject<GetNatureofClaimResult>(jsonResult);

        return returnValue;
    }

    public GetBenefitByNatureOfClaimResult GetBenefitByNatureOfClaimRequest(GetBenefitByNatureOfClaimRequest token)
    {
        GetBenefitByNatureOfClaimResult returnValue = new GetBenefitByNatureOfClaimResult();

        string jsonRequest = Newtonsoft.Json.JsonConvert.SerializeObject(token);

        string jsonResult = SystemUtility.JsonHttpPost(jsonRequest, ConfigurationManager.AppSettings["GetBenefitByNatureOfClaimRequest"].Trim());

        returnValue = Newtonsoft.Json.JsonConvert.DeserializeObject<GetBenefitByNatureOfClaimResult>(jsonResult);

        return returnValue;
    }

    public GetDocumentBasedOnBenefitResult GetDocumentBasedOnBenefitRequest(GetDocumentBasedOnBenefitRequest token)
    {
        GetDocumentBasedOnBenefitResult returnValue = new GetDocumentBasedOnBenefitResult();

        string jsonRequest = Newtonsoft.Json.JsonConvert.SerializeObject(token);

        string jsonResult = SystemUtility.JsonHttpPost(jsonRequest, ConfigurationManager.AppSettings["GetDocumentBasedOnBenefitRequest"].Trim());

        returnValue = Newtonsoft.Json.JsonConvert.DeserializeObject<GetDocumentBasedOnBenefitResult>(jsonResult);

        return returnValue;
    }

    public GetExistingDocumentsResults GetExistingDocuments(GetExistingDocumentsRequest token)
    {
        GetExistingDocumentsResults returnValue = new GetExistingDocumentsResults();

        string jsonRequest = Newtonsoft.Json.JsonConvert.SerializeObject(token);

        string jsonResult = SystemUtility.JsonHttpPost(jsonRequest, ConfigurationManager.AppSettings["GetExistingDocuments"].Trim());

        returnValue = Newtonsoft.Json.JsonConvert.DeserializeObject<GetExistingDocumentsResults>(jsonResult);

        return returnValue;
    }

    public SaveClaimsRequirementsResult SaveClaimsRequirementsRequest(SaveClaimsRequirementsRequest token)
    {
        SaveClaimsRequirementsResult returnValue = new SaveClaimsRequirementsResult();

        string jsonRequest = Newtonsoft.Json.JsonConvert.SerializeObject(token);

        string jsonResult = SystemUtility.JsonHttpPost(jsonRequest, ConfigurationManager.AppSettings["SaveClaimsRequirementsRequest"].Trim());

        returnValue = Newtonsoft.Json.JsonConvert.DeserializeObject<SaveClaimsRequirementsResult>(jsonResult);

        return returnValue;
    }

    public SaveClaimsRequirementsResult SaveActimAIRequest(Claim token)
    {
        SaveClaimsRequirementsResult returnValue = new SaveClaimsRequirementsResult();

        string jsonRequest = Newtonsoft.Json.JsonConvert.SerializeObject(token);

        string jsonResult = SystemUtility.JsonHttpPost(jsonRequest, ConfigurationManager.AppSettings["SaveActimAIRequest"].Trim());

        returnValue = Newtonsoft.Json.JsonConvert.DeserializeObject<SaveClaimsRequirementsResult>(jsonResult);

        return returnValue;
    }

    public AgentReferralResult IQRSaveClientReferral(AgentReferralRequest token)
    {
        AgentReferralResult returnValue = new AgentReferralResult();

        string jsonRequest = Newtonsoft.Json.JsonConvert.SerializeObject(token);

        string jsonResult = SystemUtility.JsonHttpPost(jsonRequest, ConfigurationManager.AppSettings["IQRSaveClientReferral"].Trim());

        returnValue = Newtonsoft.Json.JsonConvert.DeserializeObject<AgentReferralResult>(jsonResult);

        return returnValue;
    }

    public DisplayPaymentSummaryResult DisplayPaymentSummary(DisplayPaymentSummaryRequest token)
    {
        DisplayPaymentSummaryResult returnValue = new DisplayPaymentSummaryResult();

        string jsonRequest = Newtonsoft.Json.JsonConvert.SerializeObject(token);

        string jsonResult = SystemUtility.JsonHttpPost(jsonRequest, ConfigurationManager.AppSettings["DisplayPaymentSummary"].Trim());

        returnValue = Newtonsoft.Json.JsonConvert.DeserializeObject<DisplayPaymentSummaryResult>(jsonResult);

        return returnValue;
    }

    public AvailableCOCResult NumberOfAvailableCOC(AvailableCOCRequest token)
    {
        AvailableCOCResult returnValue = new AvailableCOCResult();

        string jsonRequest = Newtonsoft.Json.JsonConvert.SerializeObject(token);

        string jsonResult = SystemUtility.JsonHttpPost(jsonRequest, ConfigurationManager.AppSettings["NumberOfAvailableCOC"].Trim());

        returnValue = Newtonsoft.Json.JsonConvert.DeserializeObject<AvailableCOCResult>(jsonResult);

        return returnValue;
    }

    public ProductProfileResult GetProductProfile(ProductProfileRequest token)
    {
        ProductProfileResult returnValue = new ProductProfileResult();

        string jsonRequest = Newtonsoft.Json.JsonConvert.SerializeObject(token);

        string jsonResult = SystemUtility.JsonHttpPost(jsonRequest, ConfigurationManager.AppSettings["GetProductProfile"].Trim());

        returnValue = Newtonsoft.Json.JsonConvert.DeserializeObject<ProductProfileResult>(jsonResult);

        return returnValue;
    }

    public ReferralCodeDisplayResult GetReferralDetails(ReferralCodeRequest token)
    {
        ReferralCodeDisplayResult returnValue = new ReferralCodeDisplayResult();

        string jsonRequest = Newtonsoft.Json.JsonConvert.SerializeObject(token);

        string jsonResult = SystemUtility.JsonHttpPost(jsonRequest, ConfigurationManager.AppSettings["GetReferralDetails"].Trim());

        returnValue = Newtonsoft.Json.JsonConvert.DeserializeObject<ReferralCodeDisplayResult>(jsonResult);

        return returnValue;
    }

    public CreateInvoiceResult XenditCreateInvoice(CreateInvoiceRequest token)
    {
        CreateInvoiceResult returnValue = new CreateInvoiceResult();

        string jsonRequest = Newtonsoft.Json.JsonConvert.SerializeObject(token);

        string jsonResult = SystemUtility.JsonHttpPostXendit(jsonRequest, ConfigurationManager.AppSettings["XenditCreateInvoice"].Trim());

        returnValue = Newtonsoft.Json.JsonConvert.DeserializeObject<CreateInvoiceResult>(jsonResult);

        return returnValue;
    }

    public TagInsuraceAsPaidResult TagInsuranceAsPaid(TagInsuranceAsPaidRequest token)
    {
        TagInsuraceAsPaidResult returnValue = new TagInsuraceAsPaidResult();

        string jsonRequest = Newtonsoft.Json.JsonConvert.SerializeObject(token);

        string jsonResult = SystemUtility.JsonHttpPost(jsonRequest, ConfigurationManager.AppSettings["TagInsuranceAsPaid"].Trim());

        returnValue = Newtonsoft.Json.JsonConvert.DeserializeObject<TagInsuraceAsPaidResult>(jsonResult);

        return returnValue;
        }

    public FieldValidationResult FieldValidation(FieldValidationRequest token)
    {
        FieldValidationResult returnValue = new FieldValidationResult();

        string jsonRequest = Newtonsoft.Json.JsonConvert.SerializeObject(token);

        string jsonResult = SystemUtility.JsonHttpPost(jsonRequest, ConfigurationManager.AppSettings["IQRValidateFields"].Trim());

        returnValue = Newtonsoft.Json.JsonConvert.DeserializeObject<FieldValidationResult>(jsonResult);

        return returnValue;
    }

    public SMSResults SendSMS(SMSRequest token)
    {
        SMSResults returnValue = new SMSResults();

        string jsonRequest = Newtonsoft.Json.JsonConvert.SerializeObject(token);

        string jsonResult = SystemUtility.JsonHttpPost(jsonRequest, ConfigurationManager.AppSettings["SendSMS"].Trim());

        returnValue = Newtonsoft.Json.JsonConvert.DeserializeObject<SMSResults>(jsonResult);

        return returnValue;
    }


    public PurchaseResult IQRPurchase(FirePropertyRequest token)
    {
        PurchaseResult returnValue = new PurchaseResult();

        string jsonRequest = Newtonsoft.Json.JsonConvert.SerializeObject(token);

        string jsonResult = SystemUtility.JsonHttpPost(jsonRequest, ConfigurationManager.AppSettings["IQRPurchase"].Trim());

        returnValue = Newtonsoft.Json.JsonConvert.DeserializeObject<PurchaseResult>(jsonResult);

        return returnValue;
    }

    public PurchaseResult IQRPurchaseFamily(FamilyRequest token)
    {
        PurchaseResult returnValue = new PurchaseResult();

        string jsonRequest = Newtonsoft.Json.JsonConvert.SerializeObject(token);

        string jsonResult = SystemUtility.JsonHttpPost(jsonRequest, ConfigurationManager.AppSettings["IQRPurchaseFamily"].Trim());

        returnValue = Newtonsoft.Json.JsonConvert.DeserializeObject<PurchaseResult>(jsonResult);

        return returnValue;
    }

    public PurchaseResult IQRPurchaseTravel(TravelRequest token)
    {
        PurchaseResult returnValue = new PurchaseResult();

        string jsonRequest = Newtonsoft.Json.JsonConvert.SerializeObject(token);

        string jsonResult = SystemUtility.JsonHttpPost(jsonRequest, ConfigurationManager.AppSettings["IQRPurchaseTravel"].Trim());

        returnValue = Newtonsoft.Json.JsonConvert.DeserializeObject<PurchaseResult>(jsonResult);

        return returnValue;
    }

    public PurchaseResult IQRPurchasePet(HealthDeclarationRequest token)
    {
        PurchaseResult returnValue = new PurchaseResult();

        string jsonRequest = Newtonsoft.Json.JsonConvert.SerializeObject(token);

        string jsonResult = SystemUtility.JsonHttpPost(jsonRequest, ConfigurationManager.AppSettings["IQRPurchasePet"].Trim());

        returnValue = Newtonsoft.Json.JsonConvert.DeserializeObject<PurchaseResult>(jsonResult);

        return returnValue;
    }

    public SelectionResult IMSSelectionList(SelectionListRequest token)
    {
        SelectionResult returnValue = new SelectionResult();

        string jsonRequest = Newtonsoft.Json.JsonConvert.SerializeObject(token);

        string jsonResult = SystemUtility.JsonHttpPost(jsonRequest, ConfigurationManager.AppSettings["IMSSelectionList"].Trim());

        returnValue = Newtonsoft.Json.JsonConvert.DeserializeObject<SelectionResult>(jsonResult);
        return returnValue;
    }

    public DestinationTypeResult IQRDestinationType(DestinationTypeRequest token)
    {
        DestinationTypeResult returnValue = new DestinationTypeResult();

        string jsonRequest = Newtonsoft.Json.JsonConvert.SerializeObject(token);

        string jsonResult = SystemUtility.JsonHttpPost(jsonRequest, ConfigurationManager.AppSettings["IQRDestinationType"].Trim());

        returnValue = Newtonsoft.Json.JsonConvert.DeserializeObject<DestinationTypeResult>(jsonResult);

        return returnValue;
    }

    public OptionalCoverageResult IQROptionalCoverage(OptionalCoverageRequest token)
    {
        OptionalCoverageResult returnValue = new OptionalCoverageResult();

        string jsonRequest = Newtonsoft.Json.JsonConvert.SerializeObject(token);

        string jsonResult = SystemUtility.JsonHttpPost(jsonRequest, ConfigurationManager.AppSettings["IQROptionalCoverage"].Trim());

        returnValue = Newtonsoft.Json.JsonConvert.DeserializeObject<OptionalCoverageResult>(jsonResult);

        return returnValue;
    }
    public SecondaryProductResult IQRSecondaryProduct(SecondaryProductRequest token)
    {
        SecondaryProductResult returnValue = new SecondaryProductResult();

        string jsonRequest = Newtonsoft.Json.JsonConvert.SerializeObject(token);

        string jsonResult = SystemUtility.JsonHttpPost(jsonRequest, ConfigurationManager.AppSettings["IQRSecondaryProduct"].Trim());

        returnValue = Newtonsoft.Json.JsonConvert.DeserializeObject<SecondaryProductResult>(jsonResult);

        return returnValue;
    }

    public GetIfCovidResult IQRGetIfCovidCoverage(GetIfCovidRequest token)
    {
        GetIfCovidResult returnValue = new GetIfCovidResult();

        string jsonRequest = Newtonsoft.Json.JsonConvert.SerializeObject(token);

        string jsonResult = SystemUtility.JsonHttpPost(jsonRequest, ConfigurationManager.AppSettings["IQRGetIfCovidCoverage"].Trim());

        returnValue = Newtonsoft.Json.JsonConvert.DeserializeObject<GetIfCovidResult>(jsonResult);

        return returnValue;
    }

    public RootDependent FamilyDependentUI(DependentRequest token)
    {
        RootDependent returnValue = new RootDependent();

        string jsonRequest = Newtonsoft.Json.JsonConvert.SerializeObject(token);

        string jsonResult = SystemUtility.JsonHttpPost(jsonRequest, ConfigurationManager.AppSettings["FamilyDependentUI"].Trim());

        returnValue = Newtonsoft.Json.JsonConvert.DeserializeObject<RootDependent>(jsonResult);
        return returnValue;
    }


    public bool CheckIfClientExistsIQR(TokenRequest token)
    {
        bool returnValue;

        string method = "CheckIfClientExistsIQR";

        string jsonRequest = Newtonsoft.Json.JsonConvert.SerializeObject(token);

        string jsonResult = SystemUtility.JsonHttpPost(jsonRequest, ConfigurationManager.AppSettings["CLIBMicrositeWS"].Trim() + method);

        returnValue = Newtonsoft.Json.JsonConvert.DeserializeObject<bool>(jsonResult);

        return returnValue;
    }


    public bool CheckIfADCClientExists(TokenRequest token)
    {
        bool returnValue;

        string method = "CheckIfADCClientExists";

        string jsonRequest = Newtonsoft.Json.JsonConvert.SerializeObject(token);

        string jsonResult = SystemUtility.JsonHttpPost(jsonRequest, ConfigurationManager.AppSettings["CLIBMicrositeWS"].Trim() + method);

        returnValue = Newtonsoft.Json.JsonConvert.DeserializeObject<bool>(jsonResult);

        return returnValue;
    }

    public TokenRequest RetrieveDetailsPerADCClient(TokenRequest token)
    {
        TokenRequest returnValue;

        string method = "RetrieveDetailsPerADCClient";

        string jsonRequest = Newtonsoft.Json.JsonConvert.SerializeObject(token);

        string jsonResult = SystemUtility.JsonHttpPost(jsonRequest, ConfigurationManager.AppSettings["CLIBMicrositeWS"].Trim() + method);

        returnValue = Newtonsoft.Json.JsonConvert.DeserializeObject<TokenRequest>(jsonResult);

        return returnValue;
    }
    public bool CheckIfADCGroupClientExists(TokenRequest token)
    {
        bool returnValue;

        string method = "CheckIfADCGroupClientExists";

        string jsonRequest = Newtonsoft.Json.JsonConvert.SerializeObject(token);

        string jsonResult = SystemUtility.JsonHttpPost(jsonRequest, ConfigurationManager.AppSettings["CLIBMicrositeWS"].Trim() + method);

        returnValue = Newtonsoft.Json.JsonConvert.DeserializeObject<bool>(jsonResult);

        return returnValue;
    }
    
    public bool ClientReferralAgingValidation(TokenRequest token)
    {
        bool returnValue;

        string method = "ClientReferralAgingValidation";

        string jsonRequest = Newtonsoft.Json.JsonConvert.SerializeObject(token);

        string jsonResult = SystemUtility.JsonHttpPost(jsonRequest, ConfigurationManager.AppSettings["CLIBMicrositeWS"].Trim() + method);

        returnValue = Newtonsoft.Json.JsonConvert.DeserializeObject<bool>(jsonResult);

        return returnValue;
    }
    public TokenRequest ClientReferralIndividualTran(TokenRequest token)
    {
        TokenRequest returnValue;

        string method = "ClientReferralIndividualTran";

        string jsonRequest = Newtonsoft.Json.JsonConvert.SerializeObject(token);

        string jsonResult = SystemUtility.JsonHttpPost(jsonRequest, ConfigurationManager.AppSettings["CLIBMicrositeWS"].Trim() + method);

        returnValue = Newtonsoft.Json.JsonConvert.DeserializeObject<TokenRequest>(jsonResult);

        return returnValue;

    }
    public TokenRequest ClientReferralTran(TokenRequest token)
    {
        TokenRequest returnValue;

        string method = "ClientReferralTran";

        string jsonRequest = Newtonsoft.Json.JsonConvert.SerializeObject(token);

        string jsonResult = SystemUtility.JsonHttpPost(jsonRequest, ConfigurationManager.AppSettings["CLIBMicrositeWS"].Trim() + method);

        returnValue = Newtonsoft.Json.JsonConvert.DeserializeObject<TokenRequest>(jsonResult);

        return returnValue;

    }
    public TokenRequest ClientReferralGroupTran(TokenRequest token)
    {
        TokenRequest returnValue;

        string method = "ClientReferralGroupTran";

        string jsonRequest = Newtonsoft.Json.JsonConvert.SerializeObject(token);

        string jsonResult = SystemUtility.JsonHttpPost(jsonRequest, ConfigurationManager.AppSettings["CLIBMicrositeWS"].Trim() + method);

        returnValue = Newtonsoft.Json.JsonConvert.DeserializeObject<TokenRequest>(jsonResult);

        return returnValue;

    }

    #endregion
    #region MicroBiz
    public IList<String> GetListGender(TokenRequest token)
    {
        IList<String> returnValue;

        string method = "GetListGender";

        string jsonRequest = Newtonsoft.Json.JsonConvert.SerializeObject(token);

        string jsonResult = SystemUtility.JsonHttpPost(jsonRequest, ConfigurationManager.AppSettings["CLIBMicrositeWS"].Trim() + method);

        returnValue = Newtonsoft.Json.JsonConvert.DeserializeObject<IList<String>>(jsonResult);

        return returnValue;
    }
    public IList<String> GetListCivilStatus(TokenRequest token)
    {
        IList<String> returnValue;

        string method = "GetListCivilStatus";

        string jsonRequest = Newtonsoft.Json.JsonConvert.SerializeObject(token);

        string jsonResult = SystemUtility.JsonHttpPost(jsonRequest, ConfigurationManager.AppSettings["CLIBMicrositeWS"].Trim() + method);

        returnValue = Newtonsoft.Json.JsonConvert.DeserializeObject<IList<String>>(jsonResult);

        return returnValue;
    }

    public IList<String> GetListValidID(TokenRequest token)
    {
        IList<String> returnValue;

        string method = "GetListValidID";

        string jsonRequest = Newtonsoft.Json.JsonConvert.SerializeObject(token);

        string jsonResult = SystemUtility.JsonHttpPost(jsonRequest, ConfigurationManager.AppSettings["CLIBMicrositeWS"].Trim() + method);

        returnValue = Newtonsoft.Json.JsonConvert.DeserializeObject<IList<String>>(jsonResult);

        return returnValue;
    }

    public IList<String> GetListRelation(TokenRequest token)
    {
        IList<String> returnValue;

        string method = "GetListRelation";

        string jsonRequest = Newtonsoft.Json.JsonConvert.SerializeObject(token);

        string jsonResult = SystemUtility.JsonHttpPost(jsonRequest, ConfigurationManager.AppSettings["CLIBMicrositeWS"].Trim() + method);

        returnValue = Newtonsoft.Json.JsonConvert.DeserializeObject<IList<String>>(jsonResult);

        return returnValue;
    }

    public TokenRequest TranMBPClient(TokenRequest token)
    {
        TokenRequest returnValue;

        string method = "TranMBPClient";

        string jsonRequest = Newtonsoft.Json.JsonConvert.SerializeObject(token);

        string jsonResult = SystemUtility.JsonHttpPost(jsonRequest, ConfigurationManager.AppSettings["CLIBMicrositeWS"].Trim() + method);

        returnValue = Newtonsoft.Json.JsonConvert.DeserializeObject<TokenRequest>(jsonResult);

        return returnValue;

    }
    public TokenRequest MBPQuestionnaireTran(TokenRequest token)
    {
        TokenRequest returnValue;

        string method = "MBPQuestionnaireTran";

        string jsonRequest = Newtonsoft.Json.JsonConvert.SerializeObject(token);

        string jsonResult = SystemUtility.JsonHttpPost(jsonRequest, ConfigurationManager.AppSettings["CLIBMicrositeWS"].Trim() + method);

        returnValue = Newtonsoft.Json.JsonConvert.DeserializeObject<TokenRequest>(jsonResult);

        return returnValue;

    }
    public TokenRequest GetMBPClientID(TokenRequest token)
    {
        TokenRequest returnValue;

        string method = "GetMBPClientID";

        string jsonRequest = Newtonsoft.Json.JsonConvert.SerializeObject(token);

        string jsonResult = SystemUtility.JsonHttpPost(jsonRequest, ConfigurationManager.AppSettings["CLIBMicrositeWS"].Trim() + method);

        returnValue = Newtonsoft.Json.JsonConvert.DeserializeObject<TokenRequest>(jsonResult);

        return returnValue;

    }

    public TokenRequest MBPBusinessDetailsTran(TokenRequest token)
    {
        TokenRequest returnValue;

        string method = "MBPBusinessDetailsTran";

        string jsonRequest = Newtonsoft.Json.JsonConvert.SerializeObject(token);

        string jsonResult = SystemUtility.JsonHttpPost(jsonRequest, ConfigurationManager.AppSettings["CLIBMicrositeWS"].Trim() + method);

        returnValue = Newtonsoft.Json.JsonConvert.DeserializeObject<TokenRequest>(jsonResult);

        return returnValue;

    }
    public TokenRequest MBPInsertAttachments(TokenRequest token)
    {
        TokenRequest returnValue;

        string method = "MBPInsertAttachments";

        string jsonRequest = Newtonsoft.Json.JsonConvert.SerializeObject(token);

        string jsonResult = SystemUtility.JsonHttpPost(jsonRequest, ConfigurationManager.AppSettings["CLIBMicrositeWS"].Trim() + method);

        returnValue = Newtonsoft.Json.JsonConvert.DeserializeObject<TokenRequest>(jsonResult);

        return returnValue;

    }
    public TokenRequest MBPAddtlBusOwnerDetails(TokenRequest token)
    {
        TokenRequest returnValue;

        string method = "MBPAddtlBusOwnerDetails";

        string jsonRequest = Newtonsoft.Json.JsonConvert.SerializeObject(token);

        string jsonResult = SystemUtility.JsonHttpPost(jsonRequest, ConfigurationManager.AppSettings["CLIBMicrositeWS"].Trim() + method);

        returnValue = Newtonsoft.Json.JsonConvert.DeserializeObject<TokenRequest>(jsonResult);

        return returnValue;

    }
    public TokenRequest RetrieveMBPClientDetails(TokenRequest token)
    {
        TokenRequest returnValue;

        string method = "RetrieveMBPClientDetails";

        string jsonRequest = Newtonsoft.Json.JsonConvert.SerializeObject(token);

        string jsonResult = SystemUtility.JsonHttpPost(jsonRequest, ConfigurationManager.AppSettings["CLIBMicrositeWS"].Trim() + method);

        returnValue = Newtonsoft.Json.JsonConvert.DeserializeObject<TokenRequest>(jsonResult);

            return returnValue;

    }
       
    public bool CheckIfMBPClientExists(TokenRequest token)
    {
        bool returnValue;

        string method = "CheckIfMBPClientExists";

        string jsonRequest = Newtonsoft.Json.JsonConvert.SerializeObject(token);

        string jsonResult = SystemUtility.JsonHttpPost(jsonRequest, ConfigurationManager.AppSettings["CLIBMicrositeWS"].Trim() + method);

        returnValue = Newtonsoft.Json.JsonConvert.DeserializeObject<bool>(jsonResult);

        return returnValue;
    }

    public TokenRequest MBPInsertClientPhoto(TokenRequest token)
    {
        TokenRequest returnValue;

        string method = "MBPInsertClientPhoto";

        string jsonRequest = Newtonsoft.Json.JsonConvert.SerializeObject(token);

        string jsonResult = SystemUtility.JsonHttpPost(jsonRequest, ConfigurationManager.AppSettings["CLIBMicrositeWS"].Trim() + method);

        returnValue = Newtonsoft.Json.JsonConvert.DeserializeObject<TokenRequest>(jsonResult);

        return returnValue;

    }

    public TokenRequest MBPBeneficiaryTran(TokenRequest token)
    {
        TokenRequest returnValue;

        string method = "MBPBeneficiaryTran";

        string jsonRequest = Newtonsoft.Json.JsonConvert.SerializeObject(token);

        string jsonResult = SystemUtility.JsonHttpPost(jsonRequest, ConfigurationManager.AppSettings["CLIBMicrositeWS"].Trim() + method);

        returnValue = Newtonsoft.Json.JsonConvert.DeserializeObject<TokenRequest>(jsonResult);

        return returnValue;

    }
    public TokenRequest MBPDependentTran(TokenRequest token)
    {
        TokenRequest returnValue;

        string method = "MBPDependentTran";

        string jsonRequest = Newtonsoft.Json.JsonConvert.SerializeObject(token);

        string jsonResult = SystemUtility.JsonHttpPost(jsonRequest, ConfigurationManager.AppSettings["CLIBMicrositeWS"].Trim() + method);

        returnValue = Newtonsoft.Json.JsonConvert.DeserializeObject<TokenRequest>(jsonResult);

        return returnValue;

    }
    public TokenRequest GetMBPAppDependentID (TokenRequest token)
    {
        TokenRequest returnValue;

        string method = "GetMBPAppDependentID";

        string jsonRequest = Newtonsoft.Json.JsonConvert.SerializeObject(token);

        string jsonResult = SystemUtility.JsonHttpPost(jsonRequest, ConfigurationManager.AppSettings["CLIBMicrositeWS"].Trim() + method);

        returnValue = Newtonsoft.Json.JsonConvert.DeserializeObject<TokenRequest>(jsonResult);

        return returnValue;

    }

    public TokenRequest MBPDependentTran2(TokenRequest token)
    {
        TokenRequest returnValue;

        string method = "MBPDependentTran2";

        string jsonRequest = Newtonsoft.Json.JsonConvert.SerializeObject(token);

        string jsonResult = SystemUtility.JsonHttpPost(jsonRequest, ConfigurationManager.AppSettings["CLIBMicrositeWS"].Trim() + method);

        returnValue = Newtonsoft.Json.JsonConvert.DeserializeObject<TokenRequest>(jsonResult);

        return returnValue;

    }
    public TokenRequest SaveIQRCodeKYC(TokenRequest token)
    {
        TokenRequest returnValue;

        string method = "SaveIQRCodeKYC";

        string jsonRequest = Newtonsoft.Json.JsonConvert.SerializeObject(token);

        string jsonResult = SystemUtility.JsonHttpPost(jsonRequest, ConfigurationManager.AppSettings["CLIBMicrositeWS"].Trim() + method);

        returnValue = Newtonsoft.Json.JsonConvert.DeserializeObject<TokenRequest>(jsonResult);

        return returnValue;

    }
    public TokenRequest GetMBPAppDependentID2(TokenRequest token)
    {
        TokenRequest returnValue;

        string method = "GetMBPAppDependentID2";

        string jsonRequest = Newtonsoft.Json.JsonConvert.SerializeObject(token);

        string jsonResult = SystemUtility.JsonHttpPost(jsonRequest, ConfigurationManager.AppSettings["CLIBMicrositeWS"].Trim() + method);

        returnValue = Newtonsoft.Json.JsonConvert.DeserializeObject<TokenRequest>(jsonResult);

        return returnValue;

    }

    
    public IList<String> GetListSourceOfFunds(TokenRequest token)
    {
        IList<String> returnValue;

        string method = "GetListSourceOfFunds";

        string jsonRequest = Newtonsoft.Json.JsonConvert.SerializeObject(token);

        string jsonResult = SystemUtility.JsonHttpPost(jsonRequest, ConfigurationManager.AppSettings["CLIBMicrositeWS"].Trim() + method);

        returnValue = Newtonsoft.Json.JsonConvert.DeserializeObject<IList<String>>(jsonResult);

        return returnValue;
    }

    public IList<String> GetListNatureOfWork(TokenRequest token)
    {
        IList<String> returnValue;

        string method = "GetListNatureOfWork";

        string jsonRequest = Newtonsoft.Json.JsonConvert.SerializeObject(token);

        string jsonResult = SystemUtility.JsonHttpPost(jsonRequest, ConfigurationManager.AppSettings["CLIBMicrositeWS"].Trim() + method);

        returnValue = Newtonsoft.Json.JsonConvert.DeserializeObject<IList<String>>(jsonResult);

        return returnValue;
    }

    #endregion
}