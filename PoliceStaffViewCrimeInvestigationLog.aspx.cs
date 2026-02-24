using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json;
using System.IO;
using System.Xml;
using System.Data;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.Runtime;
using Amazon.S3.Transfer;
using Amazon.S3.IO;
using System.Text;

namespace CloudBasedEncryptedCrime
{
    public partial class PoliceStaffViewCrimeInvestigationLog : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                MyConnection obj = new MyConnection();
                DataTable tab = new DataTable();
                tab = obj.GetCrime_PoliceStaff(int.Parse(Session["UserId"].ToString()));
                ddlCrime.DataSource = tab;
                ddlCrime.DataTextField = "CrimeName";
                ddlCrime.DataValueField = "CrimeId";
                ddlCrime.DataBind();
                ddlCrime.Items.Insert(0, "--Select--");
                Panel1.Visible = false;
            }
        }
        List<CrimeInvestigationLog> CrimeLoglst = new List<CrimeInvestigationLog>();
        AmazonS3Client _s3ClientObj = null;
        Tuple<byte[], byte[]> T = null;
        string _path = "~/DownloadFile/";
        protected void ddlCrime_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                _s3ClientObj = new AmazonS3Client("AKIA2ZBFNXZ3UIQMZIVG", "zESzpfaD2e65rAcg4tlQk6hXXcCnoIqA4NVQTDL1", Amazon.RegionEndpoint.USEast1);
                string fname = ddlCrime.SelectedItem.Value + "_crimelog.json";
                string filepath = _path + fname;

                if (File.Exists(Server.MapPath(filepath)))
                {
                    File.Delete(Server.MapPath(filepath));
                }
                GetObjectResponse _responseObj = _s3ClientObj.GetObject(new GetObjectRequest() { BucketName = "cloudtrackcrime2026", Key = fname });
                _responseObj.WriteResponseStreamToFile(Server.MapPath(filepath));
                MyConnection obj = new MyConnection();
                DataTable tab = new DataTable();
                tab = obj.GetCrimeInvestigationLog(int.Parse(ddlCrime.SelectedItem.Value));
                byte[] key = ASCIIEncoding.ASCII.GetBytes(tab.Rows[0]["FKey2"].ToString());
                SymmetricCryptoClass.Decryption(Server.MapPath(filepath), key.ToString());

                // Deserialize the ArrayList from the JSON file
                string jsonFromFile = File.ReadAllText(Server.MapPath(filepath));
                List<CrimeInvestigationLog> deserializedlst = JsonConvert.DeserializeObject<List<CrimeInvestigationLog>>(jsonFromFile);
                foreach (var item in deserializedlst)
                {

                    CrimeLoglst.Add(new CrimeInvestigationLog
                    {
                        StaffId=item.StaffId,
                        CrimeId = item.CrimeId,
                        LogDate = item.LogDate,
                        Description = item.Description
                    });

                }

                Table1.Controls.Clear();
                TableRow hr = new TableRow();
                TableHeaderCell hc1 = new TableHeaderCell();
                TableHeaderCell hc2 = new TableHeaderCell();
                TableHeaderCell hc3 = new TableHeaderCell();
                TableHeaderCell hc4 = new TableHeaderCell();
                TableHeaderCell hc5 = new TableHeaderCell();

                hc1.Text = "Sl No";
                hr.Cells.Add(hc1);
                hc2.Text = "Staff Name";
                hr.Cells.Add(hc2);
                hc3.Text = "Staff Role";
                hr.Cells.Add(hc3);
                hc4.Text = "Log Date";
                hr.Cells.Add(hc4);
                hc5.Text = "Crime Log";
                hr.Cells.Add(hc5);


                Table1.Rows.Add(hr);

                int i = 0;
                foreach (CrimeInvestigationLog objCrimeLog in CrimeLoglst)
                {

                    Panel1.Visible = true;
                    TableRow row = new TableRow();

                    Label lblSlNo = new Label();
                    lblSlNo.Text = (i + 1).ToString();
                    TableCell SlNo = new TableCell();
                    SlNo.Controls.Add(lblSlNo);

                    Label lblStaffName = new Label();
                    obj = new MyConnection();
                    DataTable tabps = new DataTable();
                    tabps = obj.GetPoliceStaff_Info(int.Parse(objCrimeLog.StaffId));
                    lblStaffName.Text = tabps.Rows[0]["Name"].ToString();
                    TableCell StaffName = new TableCell();
                    StaffName.Controls.Add(lblStaffName);

                    Label lblStaffRole = new Label();
                    lblStaffRole.Text = tabps.Rows[0]["StaffRole"].ToString();
                    TableCell StaffRole = new TableCell();
                    StaffRole.Controls.Add(lblStaffRole);

                    Label lblDateTime = new Label();
                    lblDateTime.Text = objCrimeLog.LogDate;
                    TableCell DateTime = new TableCell();
                    DateTime.Controls.Add(lblDateTime);

                    Label lblDescription = new Label();
                    lblDescription.Text = objCrimeLog.Description;
                    TableCell Description = new TableCell();
                    Description.Controls.Add(lblDescription);

                    row.Controls.Add(SlNo);
                    row.Controls.Add(StaffName);
                    row.Controls.Add(StaffRole);
                    row.Controls.Add(DateTime);
                    row.Controls.Add(Description);
                    Table1.Controls.Add(row);

                    i++;


                }
            }
            catch
            {
            }
        }
    }
}