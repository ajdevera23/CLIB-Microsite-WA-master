using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for ProductList
/// </summary>
public class ProductList:BaseResult
{
    public ProductList()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    private string _partnerCode;
    private string _categoryCode;
    private string _productCode;
    private string _productName;
    private decimal _SRP;
    private string _integrationId;
    private string _providerCode;
    private string _COCEffectiveDateBasis;
    private string _CoverageDurationInDays;
    private Int64 _categoryId;
    private Int64 _productId;
    private Int64 _providerId;
    private Int64 _partnerId;
    private Int64 _platformId;




    public Int64 CategoryId
    {
        get { return _categoryId; }
        set { _categoryId = value; }
    }

    public Int64 ProductId
    {
        get { return _productId; }
        set { _productId = value; }
    }

    public Int64 ProviderId
    {
        get { return _providerId; }
        set { _providerId = value; }
    }
    public Int64 PartnerId
    {
        get { return _partnerId; }
        set { _partnerId = value; }
    }
    public Int64 PlatformId
    {
        get { return _platformId; }
        set { _platformId = value; }
    }
    public string IntegrrationId
    {
        get { return _integrationId; }
        set { _integrationId = value; }
    }


    public string PartnerCode
    {
        get { return _partnerCode; }
        set { _partnerCode = value; }
    }

    public string CategoryCode
    {
        get { return _categoryCode; }
        set { _categoryCode = value; }
    }

    public string ProviderCode
    {
        get { return _providerCode; }
        set { _providerCode = value; }
    }
    public string ProductCode
    {
        get { return _productCode; }
        set { _productCode = value; }
    }
    private string _productDesc;
    public string ProductDescription
    {
        get { return _productDesc; }
        set { _productDesc = value; }
    }
    private string _iconPath;
    public string IconPath
    {
        get { return _iconPath; }
        set { _iconPath = value; }
    }

    public string ProductName
    {
        get { return _productName; }
        set { _productName = value; }
    }
    public decimal SRP
    {
        get { return _SRP; }
        set { _SRP = value; }
    }

    public string COCEffectiveDateBasis
    {
        get { return _COCEffectiveDateBasis; }
        set { _COCEffectiveDateBasis = value; }
    }

    public string CoverageDurationInDays
    {
        get { return _CoverageDurationInDays; }
        set { _CoverageDurationInDays = value; }
    }

}