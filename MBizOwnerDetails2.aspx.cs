using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MBizOwnerDetails2 : System.Web.UI.Page
{
    TokenRequest token = new TokenRequest();
    GenerateToken generateToken = new GenerateToken();
    GetList getList = new GetList();
    public string ID = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        //Case: 1 When the page is submitted for the first time(First PostBack) and there is file 
        // in FileUpload control but session is Null then Store the values to Session Object as:
        postalCode.Attributes.Add("pattern", "^[0-9]+$");
        nationality.Attributes.Add("pattern", "^[a-zA-Z ]+");

        if (!IsPostBack)
        {
            GetListProvince();
            GetListGender();
            GetListCivilStatus();
            GetListValidID();
            GetListSourceOfFunds();
            GetListNatureOfWork();
            token.Token = generateToken.GenerateTokenAuth();
            token.ClientID = Session["ClientID"].ToString();
            if (getList.CheckIfMBPClientExists(token) == true)
            {
                RetrieveMBPClientDetails();
            }
            else
            {
                //do nothing 
            }

        }
    }


    public void RetrieveMBPClientDetails()
    {

        ID = Session["ClientID"].ToString();
        token.Token = generateToken.GenerateTokenAuth();
        token.ClientID = ID;

        TokenRequest retrieveClientRecord;
        retrieveClientRecord = getList.RetrieveMBPClientDetails(token);
        DDCivilStatus.SelectedValue = retrieveClientRecord.CivilStat;
        Address.Value = retrieveClientRecord.Address;
        postalCode.Value = retrieveClientRecord.ZipCode;
        DDProvince.SelectedValue = retrieveClientRecord.Province;
        GetListCity(retrieveClientRecord.Province);
        DDcity.SelectedValue = retrieveClientRecord.City;
        DDValidID.SelectedValue = retrieveClientRecord.ValidID;
        nationality.Value = retrieveClientRecord.Nationality;
        validIDNumber.Value = retrieveClientRecord.ValidIDNum;
        DDNatureofWork.SelectedValue = retrieveClientRecord.NatureofWork;
        DDSourceOfFunds.SelectedValue = retrieveClientRecord.SourceOfFunds;
        if (retrieveClientRecord.Gender == "M")
        {
            DDGender.SelectedValue = "Male";
        }
        else if (retrieveClientRecord.Gender == "F")
        {
            DDGender.SelectedValue = "Female";
        }
        else
        {
            DDGender.SelectedIndex = 0;
        }

    }
    public void GetListSourceOfFunds()
    {
        token.Token = generateToken.GenerateTokenAuth();
        IList<String> sourceofFUndsList;
        sourceofFUndsList = getList.GetListSourceOfFunds(token);
        DDSourceOfFunds.DataSource = sourceofFUndsList;
        DDSourceOfFunds.DataBind();
    }
    public void GetListNatureOfWork()
    {
        token.Token = generateToken.GenerateTokenAuth();
        IList<String> natureList;
        natureList = getList.GetListNatureOfWork(token);
        DDNatureofWork.DataSource = natureList;
        DDNatureofWork.DataBind();
    }
    public void GetListProvince()
    {
        token.Token = generateToken.GenerateTokenAuth();
        IList<String> provList;
        provList = getList.GetListProvince(token);
        DDProvince.DataSource = provList;
        DDProvince.DataBind();

    }
    public void GetListCity(string Prov)
    {
        token.Province = Prov;
        token.Token = generateToken.GenerateTokenAuth();
        IList<String> cityList;
        cityList = getList.GetListCity(token);
        DDcity.DataSource = cityList;
        DDcity.DataBind();
    }
    public void GetListGender()
    {
        token.Token = generateToken.GenerateTokenAuth();
        IList<String> genderList;
        genderList = getList.GetListGender(token);
        DDGender.DataSource = genderList;
        DDGender.DataBind();

    }
    public void GetListCivilStatus()
    {
        token.Token = generateToken.GenerateTokenAuth();
        IList<String> civilStatList;
        civilStatList = getList.GetListCivilStatus(token);
        DDCivilStatus.DataSource = civilStatList;
        DDCivilStatus.DataBind();

    }
   
    public void GetListValidID()
    {
        token.Token = generateToken.GenerateTokenAuth();
        IList<String> validIDList;
        validIDList = getList.GetListValidID(token);
        DDValidID.DataSource = validIDList;
        DDValidID.DataBind();

    }
    protected void DDProvince_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetListCity(DDProvince.SelectedValue);
    }

    
    protected void btnNext_Click(object sender, EventArgs e)
    {

        if(DDGender.SelectedValue == "Select")
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Gender is required.')", true);
        }
        else
        {
            if(DDCivilStatus.SelectedValue == "Select")
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Civil Status is required.')", true);
            }
            else
            {
                if (DDProvince.SelectedValue == "Select")
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Province is required.')", true);
                }
                else
                {
                    if (DDcity.SelectedValue == "Select")
                    {
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('City is required.')", true);
                    }
                    else
                    {
                        if (DDValidID.SelectedValue == "Select")
                        {
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Valid ID is required.')", true);
                        }
                        else
                        {
                            if(DDSourceOfFunds.SelectedValue == "Select" )
                            {
                                Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Source of Funds is required.')", true);
                            }
                            else 
                            {
                                if(DDNatureofWork.SelectedValue == "Select")
                                {
                                    Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Nature of Work/Business is required.')", true);
                                }
                                else
                                {
                                    string Gender = "";
                                    if (DDGender.SelectedValue == "Male")
                                    {
                                        Gender = "M";
                                    }
                                    else
                                    {
                                        Gender = "F";
                                    }

                                    token.Gender = Gender;
                                    token.CivilStat = DDCivilStatus.SelectedValue;
                                    token.Address = Address.Value;
                                    token.Province = DDProvince.SelectedValue;
                                    token.City = DDcity.SelectedValue;
                                    token.ZipCode = postalCode.Value;
                                    token.ValidID = DDValidID.SelectedValue;
                                    token.ValidIDNum = validIDNumber.Value;
                                    token.ClientID = Session["ClientID"].ToString();
                                    token.Nationality = nationality.Value;
                                    token.Token = generateToken.GenerateTokenAuth();
                                    token.SourceOfFunds = DDSourceOfFunds.SelectedValue;
                                    token.NatureofWork = DDNatureofWork.SelectedValue;
                                    TokenRequest UpdateClientTran;
                                    UpdateClientTran = getList.MBPAddtlBusOwnerDetails(token);

                                    Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Client Details has succesfully updated.');window.location = 'MBizPACoverage.aspx';", true);
                                }
                            }
                           
                        }
                    }
                }
            }
        }
        
        

        
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect(ConfigurationManager.AppSettings["MBizDetails"].Trim());

    }


}

  



   

