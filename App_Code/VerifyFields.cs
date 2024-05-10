/*CHANGE LOG
 20201202 - addition of guardian fields verfication*/

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for VerifyFields
/// </summary>
public class VerifyFields:BaseResult
{
    BaseResult result = new BaseResult();
    GetList getList = new GetList();
    TokenRequest token = new TokenRequest();
    GenerateToken generateToken = new GenerateToken();
    public VerifyFields()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public BaseResult VerifyTransactionFields(ProcessTransactionRequest trans)
    {
        
        string beneName;
        string beneRelationShip;

        token.Token = generateToken.GenerateTokenAuth();
        token.ReferenceCode = trans.GeneralDetails.ReferenceCode;
        
        foreach (BeneficiaryCollection item in trans.BeneficiaryCollection)
        {
            beneName = item.BeneficiaryName;
            beneRelationShip = item.BeneficiaryRelationship;
            if (IsValidDate(trans.CustomerDetails.Birthdate) == true)
            {
                DateTime oDate = DateTime.Parse(trans.CustomerDetails.Birthdate);

                if (trans.CustomerDetails.FirstName.Length == 0 )
                {
                    result.Message = "First name is required.";
                    result.ResultStatus = ResultType.Failed;
                }
                else if (trans.CustomerDetails.MiddleName.Length == 0)
                {
                    result.Message = "Middle name is required.";
                    result.ResultStatus = ResultType.Failed;
                }

                else if(trans.CustomerDetails.LastName.Length == 0)
                {
                    result.Message = "Last name is required.";
                    result.ResultStatus = ResultType.Failed;
                }
                else if (System.Text.RegularExpressions.Regex.IsMatch(trans.CustomerDetails.FirstName, @"[^a-zA-Z0-9\s]") ||
                    System.Text.RegularExpressions.Regex.IsMatch(trans.CustomerDetails.MiddleName, @"[^a-zA-Z0-9\s]") ||
                    System.Text.RegularExpressions.Regex.IsMatch(trans.CustomerDetails.LastName, @"[^a-zA-Z\s0-9]"))
                {
                    result.Message = "Please enter valid name.";
                    result.ResultStatus = ResultType.Failed;
                }
                else if (System.Text.RegularExpressions.Regex.IsMatch(beneName, @"[^a-zA-Z0-9\s]"))
                {
                    result.Message = "Please enter valid beneficiary name.";
                    result.ResultStatus = ResultType.Failed;
                }

                else if (System.Text.RegularExpressions.Regex.IsMatch(trans.CustomerDetails.ValidIDNumber, "[^a-zA-Z0-9]"))
                {
                    result.Message = "Please enter valid ID number.";
                    result.ResultStatus = ResultType.Failed;
                }
                else if (System.Text.RegularExpressions.Regex.IsMatch(trans.CustomerDetails.PlaceOfBirth, @"[^a-zA-Z0-9'-.,\s]"))
                {
                    result.Message = "Please enter valid place of birth.";
                    result.ResultStatus = ResultType.Failed;
                }

                else if (System.Text.RegularExpressions.Regex.IsMatch(trans.CustomerDetails.Address, @"[^0-9a-zA-Z\s'-.,]"))
                {
                    result.Message = "Please enter valid address.";
                    result.ResultStatus = ResultType.Failed;
                }
                else if (System.Text.RegularExpressions.Regex.IsMatch(trans.CustomerDetails.SourceOfFunds, @"[^0-9a-zA-Z\s]"))
                {
                    result.Message = "Please enter valid source of funds.";
                    result.ResultStatus = ResultType.Failed;
                }
                else if (!System.Text.RegularExpressions.Regex.IsMatch(trans.CustomerDetails.MobileNumber, @"[0-9]"))
                {
                    result.Message = "Please enter valid contact number.";
                    result.ResultStatus = ResultType.Failed;
                }
                else if(trans.CustomerDetails.SourceOfFunds.Length==0)
                {
                    result.Message = "Please enter your source of funds.";
                    result.ResultStatus = ResultType.Failed;
                }
                else if(trans.CustomerDetails.Gender.Length==0)
                {
                    result.Message = "Please select Gender.";
                    result.ResultStatus = ResultType.Failed;
                }
                else if (trans.CustomerDetails.Address.Length == 0)
                {
                    result.Message = "Please enter your complete address.";
                    result.ResultStatus = ResultType.Failed;
                }
                else if (oDate.Year > DateTime.Today.Year)
                {
                    result.Message = "Invalid birth date year.";
                    result.ResultStatus = ResultType.Failed;
                }
                else if (trans.CustomerDetails.PlaceOfBirth.Length == 0)
                {
                    result.Message = "Please enter your place of birth.";
                    result.ResultStatus = ResultType.Failed;
                }
                else if (trans.CustomerDetails.Gender.Length == 0)
                {
                    result.Message = "Please select Gender.";
                    result.ResultStatus = ResultType.Failed;
                }
                else if (trans.CustomerDetails.CivilStatus.Length == 0)
                {
                    result.Message = "Please select civil status.";
                    result.ResultStatus = ResultType.Failed;
                }
                else if (trans.CustomerDetails.MobileNumber.Length == 0 || trans.CustomerDetails.MobileNumber.Length != 11 || trans.CustomerDetails.MobileNumber.Substring(0, 1) != "0")
                {
                    result.Message = "Please enter valid contact number.";
                    result.ResultStatus = ResultType.Failed;
                }
                else if (trans.CustomerDetails.MobileNumber.Substring(1, 1) != "9")
                {
                    result.Message = "Please enter valid contact number.";
                    result.ResultStatus = ResultType.Failed;
                }

                else if (trans.CustomerDetails.EmailAddress.Length == 0 || IsValidEmail(trans.CustomerDetails.EmailAddress) == false)
                {
                    result.Message = "Please enter valid email address.";
                    result.ResultStatus = ResultType.Failed;
                }
                else if (trans.CustomerDetails.ValidIDPresented == "Select")
                {
                    result.Message = "Please select valid ID.";
                    result.ResultStatus = ResultType.Failed;
                }
                else if (trans.CustomerDetails.ValidIDNumber.Length == 0)
                {
                    result.Message = "Please enter id number.";
                    result.ResultStatus = ResultType.Failed;
                }
                else if (beneName.Length == 0)
                {
                    result.Message = "Please enter beneficiary name.";
                    result.ResultStatus = ResultType.Failed;
                }
                else if (beneRelationShip == "Select")
                {
                    result.Message = "Please select beneficiary relationship.";
                    result.ResultStatus = ResultType.Failed;
                }
                else if (IfReferenceCodeIsUsed(token) == true)
                {
                    result.Message = "Reference code is already used. Please use another reference code.";
                    result.ResultStatus = ResultType.Failed;
                }
                
                else
                {
                    result.ResultStatus = ResultType.Success;
                }

            }
            else
            {
                result.Message = "Please enter valid birth date.";
                result.ResultStatus = ResultType.Failed;
            }
        }

        return result;
    }

    bool IsValidEmail(string email)
    {
        try
        {

            if (System.Text.RegularExpressions.Regex.IsMatch(email, @"^([0-9a-zA-Z]" + //Start with a digit or alphabetical
                                                @"([\+\-_\.][0-9a-zA-Z]+)*" + // No continuous or ending +-_. chars in email
                                                @")+" +
                                                @"@(([0-9a-zA-Z][-\w]*[0-9a-zA-Z]*\.)+[a-zA-Z0-9]{2,17})$"))
            {
                result.ResultStatus = ResultType.Success;
                return true;
            }
            else
            {
                result.ResultStatus = ResultType.Failed;
                return false;
            }
                //var addr = new System.Net.Mail.MailAddress(email);
            
        }
        catch
        {
            result.ResultStatus = ResultType.Failed;
            return false;
        }
    }
   public bool IsValidDate(string date)
    {
        DateTime temp;
        if (DateTime.TryParse(date, out temp))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public bool IfReferenceCodeIsUsed(TokenRequest token)
    {
        bool bit = false;
        if (string.IsNullOrEmpty(token.ReferenceCode) == false)
        {
            if (CLIBRefCodeIsUsed(token.ReferenceCode) == false)
            {
                if (getList.ifReferenceCodeIsUsed(token) == true)
                {
                    bit = true;

                }
                else
                {
                    bit = false;
                }
            }
            else
            {
                bit = false;
            }
        }
        else
        {
            bit = false; //meaning refcode is not used
        }
        return bit;
    }

    public bool CLIBRefCodeIsUsed(string refCode)
    {
        if (refCode==ConfigurationManager.AppSettings["CLIBvoucherCode"])
        {
            return true;
        }
        else if(refCode == ConfigurationManager.AppSettings["CLIBvoucherCode1"])
        {
            return true;
        }
        else if(refCode == ConfigurationManager.AppSettings["CLIBvoucherCode2"])
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public BaseResult IsValidGuardian(ProcessTransactionRequest trans)
    {
        try
        {
            if ((DateTime.Now.Year - DateTime.Parse(trans.CustomerDetails.Birthdate).Year) < 18)
            {
                BaseResult result = new BaseResult();
                DateTime oDate = DateTime.Parse(trans.GuardianDetails.GuardianBirthday);

                if (System.Text.RegularExpressions.Regex.IsMatch(trans.GuardianDetails.GuardianName, @"[^a-zA-Z0-9\s]"))
                {
                    result.Message = "Please enter valid guardian name.";
                    result.ResultStatus = ResultType.Failed;
                }
                else if (trans.GuardianDetails.GuardianRelationship == "Select")
                {
                    result.Message = "Please select valid guardian relationship.";
                    result.ResultStatus = ResultType.Failed;
                }
                else if (trans.GuardianDetails.GuardianContactNo.Length == 0 || trans.GuardianDetails.GuardianContactNo.Length != 11 || trans.GuardianDetails.GuardianContactNo.Substring(0, 1) != "0")
                {
                    result.Message = "Please enter valid guardian contact number.";
                    result.ResultStatus = ResultType.Failed;
                }
                else if (trans.GuardianDetails.GuardianContactNo.Substring(1, 1) != "9")
                {
                    result.Message = "Please enter valid guardian contact number.";
                    result.ResultStatus = ResultType.Failed;
                }
                else if (oDate.Year > DateTime.Today.Year)
                {
                    result.Message = "Invalid guardian birth date year.";
                    result.ResultStatus = ResultType.Failed;
                }
                else
                {
                    
                    result.ResultStatus = ResultType.Success;
                }
            }
            else
            {
                result.ResultStatus = ResultType.Success;
            }
            
        }
        catch(FormatException ex)
        {
            result.Message = "Invalid birth date.";
            result.ResultStatus = ResultType.Failed;

        }
        
        catch (Exception ex)
        {

            result.Message = "Invalid guardian details. Please check details and then try again.";
            result.ResultStatus = ResultType.Failed;
        }
        

        return result;
    }
}