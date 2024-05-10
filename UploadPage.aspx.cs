using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using System.Configuration;
using System.Data.OleDb;
using ExcelDataReader;
using System.Globalization;

public partial class UploadPage : System.Web.UI.Page
{
    ProcessTransaction processTransaction = new ProcessTransaction();
    TokenRequest token = new TokenRequest();
    GenerateToken generateToken = new GenerateToken();
    BaseResult result = new BaseResult();
    GetList getList = new GetList();
    

    string voucherCode;

    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.Cache.SetNoStore();

        if (!string.IsNullOrEmpty(Session["voucherCode"] as string))
        {
            voucherCode = Session["voucherCode"].ToString();

            if (voucherCode == ConfigurationManager.AppSettings["CLIBvoucherCode"]
                || voucherCode == ConfigurationManager.AppSettings["CLIBvoucherCode1"]
                || voucherCode == ConfigurationManager.AppSettings["CLIBvoucherCode2"])
            {
                

            }
            else
            {
                Response.Redirect(ConfigurationManager.AppSettings["ProductRegistration"].Trim());
            }
        }
        else
        {
            Response.Redirect(ConfigurationManager.AppSettings["ProductRegistration"].Trim());
        }
    }

    public void SubmitBtn_Click(object sender, EventArgs e)
    {
        if (FileUpload1.HasFile)
        {
            string FileName = Path.GetFileName(FileUpload1.PostedFile.FileName);
            string Extension = Path.GetExtension(FileUpload1.PostedFile.FileName);
            string FolderPath = ConfigurationManager.AppSettings["FolderPath"];
            string FilePath = Server.MapPath(FolderPath + FileName);

            try
            {
                FileUpload1.SaveAs(FilePath);
            }
            catch (Exception ex)
            {
                SystemUtility.EventLog.SaveError(ex.Message);
                Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('An error occured while opening the file. Please close your file from other application/s and then try again.')", true);
            }
            //Import_To_Grid(FilePath, Extension, "true");
            ReadDataExcel(FilePath, Extension);
        }
        else
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Please select a file to upload.')", true);
        }
    }
    //EXCELREADER
    public void ReadDataExcel(string filepath, string Extension)
    {
        try
        {
            FileStream stream = File.Open(filepath, FileMode.Open, FileAccess.Read);
            IExcelDataReader excelReader;
            DataTable dt = new DataTable();
            if (Extension == ".xls")
            {
                excelReader = ExcelReaderFactory.CreateBinaryReader(stream);
                DataSet result = excelReader.AsDataSet(new ExcelDataSetConfiguration()
                {
                    ConfigureDataTable = (_) => new ExcelDataTableConfiguration()
                    {
                        UseHeaderRow = true
                    }
                });

                dt = result.Tables[0];
                stream.Close();
            }
            else if (Extension == ".xlsx")
            {
                excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream);
                DataSet result = excelReader.AsDataSet(new ExcelDataSetConfiguration()
                {
                    ConfigureDataTable = (_) => new ExcelDataTableConfiguration()
                    {
                        UseHeaderRow = true
                    }
                });

                dt = result.Tables[0];
                stream.Close();
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Invalid file chosen. Please use .xls or .xlsx file to proceed.')", true);
            }

            string[] columnNames = dt.Columns.Cast<DataColumn>()
                                     .Select(x => x.ColumnName)
                                     .ToArray();

            string[] referenceColumnNamesWithSMS = {"fld_FirstName", "fld_MiddleName", "fld_LastName", "fld_MobileNumber1",
                    "fld_MobileNumber2", "fld_PartnerCode", "fld_ProductCode", "fld_ReferenceNumber", "fld_ReferenceNumberProvider",
                    "fld_TerminationDate","fld_EffectiveDate"};

            string[] referenceColumnNamesNoSMS = {"fld_FirstName", "fld_MiddleName", "fld_LastName", "fld_MobileNumber1",
                    "fld_MobileNumber2", "fld_PartnerCode", "fld_ProductCode", "fld_ReferenceNumber", "fld_ReferenceNumberProvider",
                    "fld_TerminationDate","fld_BirthDate","fld_Gender","fld_EmailAddress","fld_CivilStatus","fld_HomeAddress","fld_EffectiveDate"};
            bool noSMS = IsSubSet(columnNames, referenceColumnNamesNoSMS);
            bool withSMS = IsSubSet(columnNames, referenceColumnNamesWithSMS);


            //check if needed col names are complete 
            if (noSMS == true || withSMS == true)
            {
                UploadToDatabaseNonOleDB(dt, noSMS, withSMS);
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Incomplete file. Please complete needed columns for upload then try again.')", true);
            }
        }
        catch (IOException ex)
        {
            
            SystemUtility.EventLog.SaveError(ex.ToString());
            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('An error occured while opening your file. Please close your file from other application/s and then try again.')", true);
        }
        catch (InvalidCastException ex)
        {
            SystemUtility.EventLog.SaveError(ex.ToString());
            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('An error occured while uploading your file. Please ensure that all entries in your file is in text format and then try again.')", true);
        }
        catch (Exception ex)
        {
            SystemUtility.EventLog.SaveError(ex.ToString());
            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('An error occured while uploading your file. Please try again.')", true);
        }
        
    }

    #region OLEDB (NOT IN USE)
    private void Import_To_Grid(string FilePath, string Extension, string isHDR)
    {
        string conStr = "";
        if (Extension == ".xls" || Extension == ".xlsx")
        {
            try
            {
                switch (Extension)
                {
                    case ".xls": //Excel 97-03
                        conStr = ConfigurationManager.ConnectionStrings["Excel03ConString"].ConnectionString;
                        break;

                    case ".xlsx": //Excel 07
                        conStr = ConfigurationManager.ConnectionStrings["Excel07ConString"].ConnectionString;
                        break;
                }

                conStr = String.Format(conStr, FilePath, isHDR);
                OleDbConnection connExcel = new OleDbConnection(conStr);
                OleDbCommand cmdExcel = new OleDbCommand();
                OleDbDataAdapter oda = new OleDbDataAdapter();

                DataTable dt = new DataTable();

                cmdExcel.Connection = connExcel;
                connExcel.Open();
                //Get the name of First Sheet
                DataTable dtExcelSchema;
                dtExcelSchema = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                string SheetName = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();

                //Get column_name
                DataTable dtCol = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Columns, null);
                connExcel.Close();

                string[] columnNames = dtCol.AsEnumerable().Select(s => s.Field<string>("COLUMN_NAME")).ToArray<string>();
                string[] referenceColumnNamesWithSMS = {"fld_FirstName", "fld_MiddleName", "fld_LastName", "fld_MobileNumber1",
                    "fld_MobileNumber2", "fld_PartnerCode", "fld_ProductCode", "fld_ReferenceNumber", "fld_ReferenceNumberProvider",
                    "fld_TerminationDate","fld_EffectiveDate"};

                string[] referenceColumnNamesNoSMS = {"fld_FirstName", "fld_MiddleName", "fld_LastName", "fld_MobileNumber1",
                    "fld_MobileNumber2", "fld_PartnerCode", "fld_ProductCode", "fld_ReferenceNumber", "fld_ReferenceNumberProvider",
                    "fld_TerminationDate","fld_BirthDate","fld_Gender","fld_EmailAddress","fld_CivilStatus","fld_HomeAddress","fld_EffectiveDate"};

                bool noSMS = IsSubSet(columnNames, referenceColumnNamesNoSMS);
                bool withSMS = IsSubSet(columnNames, referenceColumnNamesWithSMS);


                //check if needed col names are complete 
                if (noSMS == true || withSMS == true)
                {
                    UploadToDatabase(connExcel, cmdExcel, oda, dt, SheetName, noSMS, withSMS);
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Incomplete file. Please complete needed columns for upload then try again.')", true);
                }

            }
            catch (Exception ex)
            {
                SystemUtility.EventLog.SaveError(ex.Message);
                Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('An error occured while uploading your file. Please try again.')", true);


            }
        }
        else
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Invalid file chosen. Please use .xls or .xlsx file to proceed.')", true);
        }
    }
    #endregion


    private List<UploadCollection> SaveExcelToDataTable(DataTable dt, bool noSMS, bool withSMS) //Uploading with SMS activation
    {
        //string dateToday = DateTime.Now.ToString(ConfigurationManager.AppSettings["DateTimeFormat"]);
        //string effectiveDate;
        //string terminationDate;
        List<UploadCollection> uploadCollection = new List<UploadCollection>();
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            UploadCollection upload = new UploadCollection();
            upload.fld_FirstName = dt.Rows[i]["fld_FirstName"].ToString();
            upload.fld_MiddleName = dt.Rows[i]["fld_MiddleName"].ToString();
            upload.fld_LastName = dt.Rows[i]["fld_LastName"].ToString();
            upload.fld_MobileNumber1 = dt.Rows[i]["fld_MobileNumber1"].ToString();
            upload.fld_MobileNumber2 = dt.Rows[i]["fld_MobileNumber2"].ToString();
            upload.fld_PartnerCode = dt.Rows[i]["fld_PartnerCode"].ToString();
            upload.fld_ProductCode = dt.Rows[i]["fld_ProductCode"].ToString();
            upload.fld_ReferenceNumber = dt.Rows[i]["fld_ReferenceNumber"].ToString();
            upload.fld_ReferenceNumberProvider = dt.Rows[i]["fld_ReferenceNumberProvider"].ToString();
            //effectiveDate = dt.Rows[i]["fld_EffectiveDate"].ToString();
            upload.fld_EffectiveDate = dt.Rows[i]["fld_EffectiveDate"].ToString(); //Convert.ToString(DateTime.ParseExact(effectiveDate, "MM/dd/yyyy HH:mm:ss", CultureInfo.CurrentCulture, DateTimeStyles.None));  //dateToday.ToString("MM/dd/yyyy HH:mm:ss");
            //terminationDate = dt.Rows[i]["fld_TerminationDate"].ToString();
            upload.fld_TerminationDate = dt.Rows[i]["fld_TerminationDate"].ToString(); //Convert.ToString(DateTime.ParseExact(terminationDate, "MM/dd/yyyy HH:mm:ss", CultureInfo.CurrentCulture, DateTimeStyles.None));
            //upload.fld_CreatedDate = dateToday;//Convert.ToString(DateTime.ParseExact(dateToday, ConfigurationManager.AppSettings["DateTimeFormat"], CultureInfo.CurrentCulture, DateTimeStyles.None)); 

            if (noSMS == true)
            {
                upload.fld_BirthDate = dt.Rows[i]["fld_BirthDate"].ToString();
                upload.fld_Gender = dt.Rows[i]["fld_Gender"].ToString();
                upload.fld_EmailAddress = dt.Rows[i]["fld_EmailAddress"].ToString();
                upload.fld_HomeAddress = dt.Rows[i]["fld_HomeAddress"].ToString();
                upload.fld_CivilStatus = dt.Rows[i]["fld_CivilStatus"].ToString();
            }

            uploadCollection.Add(upload);
        }
        return uploadCollection;
    }

    private bool HasNull(DataTable table)
    {
        foreach (DataColumn column in table.Columns)
        {
            if (table.Rows.OfType<DataRow>().Any(r => r.IsNull(column)))
                if (column.ToString() != "fld_MiddleName")
                {
                    return true;
                }
        }

        return false;
    }

    

    private bool IsValidMobileNumber(DataTable dt)
    {
        string dataType = dt.Columns["fld_MobileNumber1"].DataType.ToString();
        if (dataType == "double" || dataType == "System.Double")
        {
            string[] mobileNumbers1 = dt.AsEnumerable().Select(s => s.Field<double>("fld_MobileNumber1").ToString()).ToArray<string>();
            foreach (string mobileNumber in mobileNumbers1)
            {
                string num = mobileNumber.Substring(0, 1);
                if (num != "0")
                {
                    return false;
                }
            }
            return true;
        }
        else
        {
            string[] mobileNumbers1 = dt.AsEnumerable().Select(s => s.Field<string>("fld_MobileNumber1")).ToArray<string>();
            foreach (string mobileNumber in mobileNumbers1)
            {
                string num = mobileNumber.Substring(0, 1);
                if (num != "0")
                {
                    return false;
                }
            }
            return true;
        }
        
        
    }
    private bool IsSubSet(string[] columnNames, string[] referenceColumnNames)
    {
        bool bit = !referenceColumnNames.Except(columnNames).Any();
        return bit;
    }
    

    private void UploadToDatabase(OleDbConnection connExcel, OleDbCommand cmdExcel, OleDbDataAdapter oda, DataTable dt, string SheetName, bool noSMS, bool withSMS)
    {
        //Read Data from First Sheet where first Name is not null
        connExcel.Open();
        cmdExcel.CommandText = "SELECT * From [" + SheetName + "] WHERE [fld_FirstName] IS NOT NULL ";
        oda.SelectCommand = cmdExcel;
        oda.Fill(dt);
        connExcel.Close();

        if (HasNull(dt) == true)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Invalid upload. Entries are incomplete.')", true);
        }
        else
        {
            if (dt.Rows.Count > 3500)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('File exceeded maximum number of entries allowed to be uploaded. Maximum of 3500 entries are allowed per upload.')", true);
            }
            else
            {
                //check if referenceNumber is unique for all entries
                var referenceNumberCol = dt.Rows.Cast<DataRow>().Select(r => r["fld_ReferenceNumber"]).Distinct().ToList();
                var uniquereferenceNumberCol = referenceNumberCol.Count == dt.Rows.Count;

                if (uniquereferenceNumberCol == true)
                {
                    bool isValidMobileNumber = IsValidMobileNumber(dt);
                    if (isValidMobileNumber == true)
                    {
                        token.IpAddress = GetIpValue();
                        //token.ReferenceNumber = referenceNumberCol[0].ToString();
                        token.UploadCollection = SaveExcelToDataTable(dt, noSMS, withSMS);
                        token.Token = generateToken.GenerateTokenAuth();
                        result = processTransaction.UploadExcel(token);

                        if (result.ResultStatus == ResultType.Success)
                        {
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('" + result.Message + "')", true);
                        }
                        else
                        {
                            SystemUtility.EventLog.SaveError(result.Message);
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('" + result.Message + "')", true);
                        }
                    }
                    else
                    {
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('One or more entries has invalid mobile number. Please modify entries and then try again.')", true);
                    }
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('" + "The entries in your file has redundant fld_ReferenceNumber. Please make sure that all entries for upload is unique before proceeding." + "')", true);
                }
                
            }
        }
    }

    private void UploadToDatabaseNonOleDB(DataTable dt, bool noSMS, bool withSMS)
    {
        

        if (HasNull(dt) == true)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Invalid upload. Entries are incomplete.')", true);
        }
        else
        {
            if (dt.Rows.Count > 3000)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('File exceeded the 3000 maximum number of entries allowed to be uploaded. Please reduce number of entries and then try again.')", true);
            }
            else
            {
                //check if referenceNumber is unique for all entries
                var referenceNumberCol = dt.Rows.Cast<DataRow>().Select(r => r["fld_ReferenceNumber"]).Distinct().ToList();
                var uniquereferenceNumberCol = referenceNumberCol.Count == dt.Rows.Count;

                if (uniquereferenceNumberCol == true)
                {
                    bool isValidMobileNumber = IsValidMobileNumber(dt);
                    if (isValidMobileNumber == true)
                    {
                        token.IpAddress = GetIpValue();
                        //token.ReferenceNumber = referenceNumberCol[0].ToString();
                        token.UploadCollection = SaveExcelToDataTable(dt, noSMS, withSMS);
                        token.Token = generateToken.GenerateTokenAuth();
                        result = processTransaction.UploadExcel(token);

                        if (result.ResultStatus == ResultType.Success)
                        {
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('" + result.Message + "')", true);
                        }
                        else
                        {
                            SystemUtility.EventLog.SaveError(result.Message);
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('" + result.Message + "')", true);
                        }
                    }
                    else
                    {
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('One or more entries has invalid mobile number. Please modify entries and then try again.')", true);
                    }
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('" + "The entries in your file has redundant fld_ReferenceNumber. Please make sure that all entries for upload is unique before proceeding." + "')", true);
                }

            }
        }
    }

    public string GetIpValue()
    {
        string ipAdd = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

        if (string.IsNullOrEmpty(ipAdd))
        {
            ipAdd = Request.ServerVariables["REMOTE_ADDR"];
        }
        return ipAdd;
    }
}