using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MBizQuestionnaire : System.Web.UI.Page
{
    TokenRequest token = new TokenRequest();
    GenerateToken generateToken = new GenerateToken();
    GetList getList = new GetList();
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            pnl4A.Visible = false;
        }
    }
    protected void n4_CheckedChanged(object sender, EventArgs e)
    {
        if (n4.Checked == true)
        {
            pnl4A.Visible = true;
        }        
    }
    protected void y4_CheckedChanged(object sender, EventArgs e)
    {
        if (y4.Checked == true)
        {
            pnl4A.Visible = false;            
            n5.Checked = false;
           
        }
    }
    public static String GetTimestamp(DateTime value)
    {
        return value.ToString("yyyyMMddHHmm");
    }
    protected void btnNext_Click(object sender, EventArgs e)
    {
        if (y4.Checked == false && n4.Checked == false)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Question 4 is required.');", true);
        }       
        else
        {
            string Y = "Yes";
            string N = "No";

            if (y1.Checked == true)
            {
                token.Q1 = Y;
            }
            else
            {
                token.Q1 = N;
            }
            if (y2.Checked == true)
            {
                token.Q2 = Y;
            }
            else
            {
                token.Q2 = N;
            }
            if (y3.Checked == true)
            {
                token.Q3 = Y;
            }
            else
            {
                token.Q3 = N;
            }
            if (y4.Checked == true)
            {
                token.Q4 = Y;
            }
            else
            {
                token.Q4 = N;
            }
            if (y5.Checked == true)
            {
                token.Q5 = Y;
            }
            else
            {
                token.Q5 = N;
            }
            string timeStamp;
            timeStamp = GetTimestamp(DateTime.Now);

            token.ClientID = Session["ClientID"].ToString();
            token.ReferenceCode = "MBP" + timeStamp;
            Session["ReferenceCode"] = "MBP" + timeStamp;
            //token.ServicingUnit = "CLIB Internal";
            if (Session["GroupMail"].ToString() == ConfigurationManager.AppSettings["CLINT"])
            {
                token.ServicingUnit = "CLIB Internal";
            }
            else if (Session["GroupMail"].ToString() == ConfigurationManager.AppSettings["CLEXT"])
            {
                token.ServicingUnit = "CLIB External";
            }
            else if (Session["GroupMail"].ToString() == ConfigurationManager.AppSettings["CLVIS"])
            {
                token.ServicingUnit = "CLIB VisMin";
            }
            else if (Session["GroupMail"].ToString() == ConfigurationManager.AppSettings["CLADC"])
            {
                token.ServicingUnit = "ADC";
            }
            token.Token = generateToken.GenerateTokenAuth();


        
            if (n1.Checked == true || n2.Checked == true || n3.Checked == true || n5.Checked == true)
            {
                Response.Redirect(ConfigurationManager.AppSettings["MBizNotQuali"].Trim());
            }
            else if (y1.Checked == true || y2.Checked == true || y3.Checked == true || y5.Checked == true)
            {
                TokenRequest InsertQuestionnaireTran;
                InsertQuestionnaireTran = getList.MBPQuestionnaireTran(token);

                Response.Redirect(ConfigurationManager.AppSettings["MBizDetails"].Trim());
            }
        }
       
    }
    protected void btnPrevious_Click(object sender, EventArgs e)
    {
        Response.Redirect(ConfigurationManager.AppSettings["MBizOwnerDetails"].Trim());
    }
  
    
}

  



   

