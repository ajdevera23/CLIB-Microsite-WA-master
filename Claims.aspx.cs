using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ClientReferral : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            // Get the value from web.config
            string docSubmissionUrl = System.Configuration.ConfigurationManager.AppSettings["DocumentsSubmission"];

            // Set the value of a hidden field
            hiddenDocSubmissionUrl.Value = docSubmissionUrl;
        }
    }

}