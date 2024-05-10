using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MBizPACoverage : System.Web.UI.Page
{
    TokenRequest token = new TokenRequest();
    GenerateToken generateToken = new GenerateToken();
    GetList getList = new GetList();
    protected void Page_Load(object sender, EventArgs e)
    {
        birthDateTextBox.Attributes.Add("type", "date");
        birthDateTextBox.Attributes.Add("required", "true");
        birthDateTextBox.Attributes.Add("min", "1900-01-01");
        birthDateTextBox.Attributes.Add("max", DateTime.Now.ToString("yyyy-MM-dd"));

        birthDateTextBox2.Attributes.Add("type", "date");
        birthDateTextBox2.Attributes.Add("required", "true");
        birthDateTextBox2.Attributes.Add("min", "1900-01-01");
        birthDateTextBox2.Attributes.Add("max", DateTime.Now.ToString("yyyy-MM-dd"));

        bene1DOB.Attributes.Add("type", "date");
        bene1DOB.Attributes.Add("required", "true");
        bene1DOB.Attributes.Add("min", "1900-01-01");
        bene1DOB.Attributes.Add("max", DateTime.Now.ToString("yyyy-MM-dd"));

        bene2DOB.Attributes.Add("type", "date");
        bene2DOB.Attributes.Add("required", "true");
        bene2DOB.Attributes.Add("min", "1900-01-01");
        bene2DOB.Attributes.Add("max", DateTime.Now.ToString("yyyy-MM-dd"));

        //if(ownerCheckbox.Checked == false && employeeCheckbox.Checked == false)
        //{
        //    FirstName.Value = string.Empty;
        //    LastName.Value = string.Empty;
        //    MiddleName.Value = string.Empty;
        //    Suffix.Value = string.Empty;
        //    birthDateTextBox.Value = string.Empty;
        //    DDGender.SelectedIndex = 0;
        //    FirstName.Disabled = true;
        //    LastName.Disabled = true;
        //    MiddleName.Disabled = true;
        //    Suffix.Disabled = true;
        //    birthDateTextBox.Disabled = true;
        //    DDGender.Enabled = false;
        //    bene1Name.Disabled = true; 
        //    bene1DOB.Disabled = true;
        //    bene1RelationshipDD.Enabled = false;
        //    bene1RelationshipDD.SelectedIndex = 0;
        //}


        if (!IsPostBack)
        {
            
            GetListGender();
            GetListRelation();
            FirstName.Disabled = true;
            LastName.Disabled = true;
            MiddleName.Disabled = true;
            Suffix.Disabled = true;
            birthDateTextBox.Disabled = true;
            DDGender.Enabled = false;
            bene1Name.Disabled = true;
            bene1DOB.Disabled = true;
            bene1RelationshipDD.Enabled = false;
            FirstName2.Disabled = true;
            MiddleName2.Disabled = true;
            LastName2.Disabled = true;
            Suffix2.Disabled = true;
            birthDateTextBox2.Disabled = true;
            DDGender2.Enabled = false;
            bene2Name.Disabled = true;
            bene2DOB.Disabled = true;
            bene2RelationshipDD.Enabled = false;
          
        }
       
    }
 

    public void GetListCity(string Prov)
    {
        token.Province = Prov;
        token.Token = generateToken.GenerateTokenAuth();
        IList<String> cityList;
        cityList = getList.GetListCity(token);

    }

    public void GetListGender()
    {
        token.Token = generateToken.GenerateTokenAuth();
        IList<String> genderList;
        genderList = getList.GetListGender(token);
        DDGender.DataSource = genderList;
        DDGender.DataBind();
        DDGender2.DataSource = genderList;
        DDGender2.DataBind();
    }

    public void GetListRelation()
    {
        token.Token = generateToken.GenerateTokenAuth();
        IList<String> relList;
        relList = getList.GetListRelation(token);
        bene1RelationshipDD.DataSource = relList;
        bene1RelationshipDD.DataBind();
        bene2RelationshipDD.DataSource = relList;
        bene2RelationshipDD.DataBind();

    }
    protected void insured2CheckBox_CheckedChanged(object sender, EventArgs e)
    {
        if(insured2CheckBox.Checked == true)
        {
            FirstName2.Disabled = false;
            MiddleName2.Disabled = false;
            LastName2.Disabled = false;
            Suffix2.Disabled = false;
            birthDateTextBox2.Disabled = false;
            DDGender2.Enabled = true;
            bene2Name.Disabled = false;
            bene2DOB.Disabled = false;
            bene2RelationshipDD.Enabled = true;
        }
        else
        {
            FirstName2.Value = string.Empty;
            MiddleName2.Value = string.Empty;
            LastName2.Value = string.Empty;
            Suffix2.Value = string.Empty;
            birthDateTextBox.Value = string.Empty;
            DDGender2.SelectedIndex = 0;
            bene2Name.Value = string.Empty;
            bene2RelationshipDD.SelectedIndex = 0;
            bene2DOB.Value = string.Empty;

            FirstName2.Disabled = true;
            MiddleName2.Disabled = true;
            LastName2.Disabled = true;
            Suffix2.Disabled = true;
            birthDateTextBox2.Disabled = true;
            DDGender2.Enabled = false;
            bene2Name.Disabled = true;
            bene2DOB.Disabled = true;
            bene2RelationshipDD.Enabled = false;
        }
        
    }
    protected void btnNext_Click(object sender, EventArgs e)
    {
        if(ownerCheckbox.Checked == true || employeeCheckbox.Checked == true)
        {
            if (DDGender.SelectedIndex == 0)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Gender is required.')", true);
            }
            else
            {
                if (bene1RelationshipDD.SelectedIndex == 0)
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Relationship is required.')", true);
                }
                else
                {

                    

                    token.FullName = FirstName.Value + " " + MiddleName.Value + " " + LastName.Value + " " + Suffix.Value;
                    token.FirstName = FirstName.Value;
                    token.LastName = LastName.Value;
                    token.DOB = birthDateTextBox.Value;
                    token.MiddleName = MiddleName.Value;
                    token.Suffix = Suffix.Value;
                    token.Gender = DDGender.SelectedValue;
                    token.ReferenceCode = Session["ReferenceCode"].ToString();
                    token.ClientID = Session["ClientID"].ToString();
                    token.Token = generateToken.GenerateTokenAuth();
                    TokenRequest InsertDependentTran;
                    InsertDependentTran = getList.MBPDependentTran(token);

                    token.FirstName = FirstName.Value;
                    token.LastName = LastName.Value;
                    token.DOB = birthDateTextBox.Value;
                    token.Token = generateToken.GenerateTokenAuth();

                    string dependentID1 = "";
                    TokenRequest GetDependentID;
                    GetDependentID = getList.GetMBPAppDependentID(token);
                    dependentID1 = GetDependentID.AppDependentID;
                    InsertBene1Details(dependentID1.ToString());
                }
            }
        }
        


        if (insured2CheckBox.Checked == true)
        {
            if (DDGender2.SelectedIndex == 0)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Gender is required.')", true);
            }
            else
            {
                if (bene2RelationshipDD.SelectedIndex == 0)
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Relationship is required.')", true);
                }
                else
                {

                    string dependentID = "";
                    //InsertDependent2Details();

                    token.FullName2 = FirstName2.Value + " " + MiddleName2.Value + " " + LastName2.Value + " " + Suffix2.Value;
                    token.FirstName2 = FirstName2.Value;
                    token.LastName2 = LastName2.Value;
                    token.DOB2 = birthDateTextBox2.Value;
                    token.MiddleName2 = MiddleName2.Value;
                    token.Suffix2 = Suffix2.Value;
                    token.Gender2 = DDGender2.SelectedValue;
                    token.ReferenceCode =  Session["ReferenceCode"].ToString();
                    token.ClientID =  Session["ClientID"].ToString();
                    token.Token2 = generateToken.GenerateTokenAuth();
                    TokenRequest InsertDependentTran2;
                    InsertDependentTran2 = getList.MBPDependentTran2(token);

                    token.FirstName = FirstName2.Value;
                    token.LastName = LastName2.Value;
                    token.DOB = birthDateTextBox2.Value;
                    token.Token = generateToken.GenerateTokenAuth();

                    TokenRequest GetDependentID2;
                    GetDependentID2 = getList.GetMBPAppDependentID(token);
                    dependentID = GetDependentID2.AppDependentID;
                    InsertBene2Details(dependentID.ToString());

                }
            }
        }


        Response.Redirect(ConfigurationManager.AppSettings["MBizDeclaration"].Trim());

    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect(ConfigurationManager.AppSettings["MBizOwnerDetails2"].Trim());

    }


    protected void ownerCheckbox_CheckedChanged(object sender, EventArgs e)
    {  
        
        if (ownerCheckbox.Checked)
        {
            FirstName.Disabled = true;
            MiddleName.Disabled = true;
            LastName.Disabled = true;
            Suffix.Disabled = true;
            birthDateTextBox.Disabled = true;
            DDGender.Enabled = false;
            bene1Name.Disabled = false;
            bene1DOB.Disabled = false;
            bene1RelationshipDD.Enabled = true;
            employeeCheckbox.Checked = false;
            ID = Session["ClientID"].ToString();
            token.ClientID = ID;
            token.Token = generateToken.GenerateTokenAuth();
            if (getList.CheckIfMBPClientExists(token) == true)
            {               
                token.ClientID = ID;
                token.Token = generateToken.GenerateTokenAuth();
                TokenRequest retrieveClientRecord;
                retrieveClientRecord = getList.RetrieveMBPClientDetails(token);
                FirstName.Value = retrieveClientRecord.FirstName;
                MiddleName.Value = retrieveClientRecord.MiddleName;
                LastName.Value = retrieveClientRecord.LastName;
                Suffix.Value = retrieveClientRecord.Suffix;

                birthDateTextBox.Value = Convert.ToDateTime(retrieveClientRecord.DOB).ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
                if (retrieveClientRecord.Gender == "M")
                {
                    DDGender.SelectedValue = "Male";
                }
                else
                {
                    DDGender.SelectedValue = "Female";
                }
            }
            else
            {
                //do nothing
            }
        }
        else
        {
            ownerCheckbox.Checked = false;
            FirstName.Value = string.Empty;
            MiddleName.Value = string.Empty;
            LastName.Value = string.Empty;
            Suffix.Value = string.Empty;
            birthDateTextBox.Value = string.Empty;
            DDGender.SelectedIndex = 0;
            bene1DOB.Value = string.Empty;
            bene1Name.Value = string.Empty;
            bene1RelationshipDD.SelectedIndex = 0;
            bene1RelationshipDD.Enabled = false;
            bene1Name.Disabled = true;
            bene1DOB.Disabled = true;
        }

    }

    protected void employeeCheckbox_CheckedChanged(object sender, EventArgs e)
    {
        ownerCheckbox.Checked = false;

      
        if (employeeCheckbox.Checked)
        {
                   
            FirstName.Value = string.Empty;
            MiddleName.Value = string.Empty;
            LastName.Value = string.Empty;
            Suffix.Value = string.Empty;
            birthDateTextBox.Value = string.Empty;
            DDGender.SelectedIndex = 0;
            FirstName.Disabled = false;
            MiddleName.Disabled = false;
            LastName.Disabled = false;
            Suffix.Disabled = false;
            birthDateTextBox.Disabled = false;
            DDGender.Enabled = true;
            bene1Name.Disabled = false;
            bene1DOB.Disabled = false;
            bene1RelationshipDD.Enabled = true;
            bene1RelationshipDD.SelectedIndex = 0;
        }
        else
        {
            FirstName.Value = string.Empty;
            MiddleName.Value = string.Empty;
            LastName.Value = string.Empty;
            Suffix.Value = string.Empty;
            birthDateTextBox.Value = string.Empty;
            DDGender.SelectedIndex = 0;
            FirstName.Disabled = true;
            MiddleName.Disabled = true;
            LastName.Disabled = true;
            Suffix.Disabled = true;
            birthDateTextBox.Disabled = true;
            DDGender.Enabled = false;
            bene1Name.Disabled = true;
            bene1DOB.Disabled = true;
            bene1RelationshipDD.Enabled = false;
            bene1RelationshipDD.SelectedIndex = 0;
        }
    }
    
    public void InsertDependent1Details()
    {

        token.FullName = FirstName.Value + " " + MiddleName.Value + " " + LastName.Value + " " + Suffix.Value;
        token.FirstName = FirstName.Value;
        token.LastName = LastName.Value;
        token.DOB = birthDateTextBox.Value;
        token.MiddleName = MiddleName.Value;
        token.Suffix = Suffix.Value;
        token.Gender = DDGender.SelectedValue;
        token.ReferenceCode =  Session["ReferenceCode"].ToString();
        token.ClientID =  Session["ClientID"].ToString();
        token.Token = generateToken.GenerateTokenAuth();
        TokenRequest InsertDependentTran;
        InsertDependentTran = getList.MBPDependentTran(token);
    }
    
    public void InsertBene1Details(string AppDependentID)
    {
        token.AppDependentID = AppDependentID;
        //token.FullName = FirstName.Value + " " + MiddleName.Value + " " + LastName.Value + " " +Suffix.Value;
        token.FullName = bene1Name.Value;
        
        token.Relationship = bene1RelationshipDD.SelectedValue;
        token.DOB = bene1DOB.Value;        
        if(ownerCheckbox.Checked == true)
        {
            token.AppDepRelationship = "Owner";
        }
        else if (employeeCheckbox.Checked  == true)
        {
            token.AppDepRelationship = "Employee";
        }

        token.ReferenceCode =  Session["ReferenceCode"].ToString();
        token.ClientID =  Session["ClientID"].ToString();
        token.Token = generateToken.GenerateTokenAuth();
        TokenRequest InsertBeneficiaryTran;
        InsertBeneficiaryTran = getList.MBPBeneficiaryTran(token);
    }
    public void InsertBene2Details(string AppDependentID)
    {
        token.AppDependentID = AppDependentID;
        //token.FullName = FirstName2.Value + " " + MiddleName2.Value + " " + LastName2.Value + " " + Suffix2.Value;

        token.FullName = bene2Name.Value;
        token.Relationship = bene2RelationshipDD.SelectedValue;
        token.DOB = bene2DOB.Value;
        token.AppDepRelationship = "Employee";
        token.ReferenceCode = Session["ReferenceCode"].ToString();
        token.ClientID =  Session["ClientID"].ToString();
        token.Token = generateToken.GenerateTokenAuth();
        TokenRequest InsertBeneficiaryTran;
        InsertBeneficiaryTran = getList.MBPBeneficiaryTran(token);
    }
}

  



   

