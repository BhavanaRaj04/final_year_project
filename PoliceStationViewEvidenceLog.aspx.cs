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
using ZXing;
using System.Drawing;

namespace CloudBasedEncryptedCrime
{
    public partial class PoliceStationViewEvidenceLog : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                MyConnection obj = new MyConnection();
                DataTable tab = new DataTable();
                tab = obj.GetCrime(int.Parse(Session["UserId"].ToString()));
                ddlCrime.DataSource = tab;
                ddlCrime.DataTextField = "CrimeName";
                ddlCrime.DataValueField = "CrimeId";
                ddlCrime.DataBind();
                ddlCrime.Items.Insert(0, "--Select--");

                Panel1.Visible = false;
            }
        }
        AmazonS3Client _s3ClientObj = null;
        protected void ddlCrime_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

                MyConnection obj = new MyConnection();
                DataTable tab = new DataTable();
                tab = obj.GetCrimeEvidenceLog(int.Parse(ddlCrime.SelectedItem.Value));
                Table1.Controls.Clear();
                if (tab.Rows.Count > 0)
                {
                    Panel1.Visible = true;
                    TableRow hr = new TableRow();
                    TableHeaderCell hc1 = new TableHeaderCell();
                    TableHeaderCell hc2 = new TableHeaderCell();
                    TableHeaderCell hc3 = new TableHeaderCell();


                    hc1.Text = "Sl No";
                    hr.Cells.Add(hc1);
                    hc2.Text = "Log Date";
                    hr.Cells.Add(hc2);
                    hc3.Text = "Evidence Data";
                    hr.Cells.Add(hc3);


                    Table1.Rows.Add(hr);
                    for (int i = 0; i < tab.Rows.Count; i++)
                    {
                        TableRow row = new TableRow();

                        Label lblSlNo = new Label();
                        lblSlNo.Text = (i + 1).ToString();
                        TableCell SlNo = new TableCell();
                        SlNo.Controls.Add(lblSlNo);


                        Label lbllogDate = new Label();
                        lbllogDate.Text = tab.Rows[i]["LogDate"].ToString();
                        TableCell logDate = new TableCell();
                        logDate.Controls.Add(lbllogDate);

                        string filepath = tab.Rows[i]["AWSFilePath"].ToString();

                        ////Amazon AWS 
                        _s3ClientObj = new AmazonS3Client("AKIA2ZBFNXZ3UIQMZIVG", "zESzpfaD2e65rAcg4tlQk6hXXcCnoIqA4NVQTDL1", Amazon.RegionEndpoint.USEast1);
                        string fname = "~/DownloadFile/" + filepath.ToString().Split('/')[1];
                        if (File.Exists(Server.MapPath(fname)))
                        {
                            File.Delete(Server.MapPath(fname));
                        }
                        GetObjectResponse _responseObj = _s3ClientObj.GetObject(new GetObjectRequest() { BucketName = filepath.ToString().Split('/')[0], Key = filepath.ToString().Split('/')[1] });
                        _responseObj.WriteResponseStreamToFile(Server.MapPath(fname));

                        var QCreader = new BarcodeReader();
                        string QCfilename = Path.Combine(Server.MapPath(fname));
                        var QCresult = QCreader.Decode(new Bitmap(QCfilename));


                        string filepath_key = tab.Rows[i]["DataKeyPath"].ToString();

                        ////Amazon AWS 
                        _s3ClientObj = new AmazonS3Client("AKIA2ZBFNXZ3UIQMZIVG", "zESzpfaD2e65rAcg4tlQk6hXXcCnoIqA4NVQTDL1", Amazon.RegionEndpoint.USEast1);
                        string fname_key = "~/DownloadFile/" + filepath_key.ToString().Split('/')[1];
                        if (File.Exists(Server.MapPath(fname_key)))
                        {
                            File.Delete(Server.MapPath(fname_key));
                        }
                        _responseObj = _s3ClientObj.GetObject(new GetObjectRequest() { BucketName = filepath_key.ToString().Split('/')[0], Key = filepath_key.ToString().Split('/')[1] });
                        _responseObj.WriteResponseStreamToFile(Server.MapPath(fname_key));


                        var QCreader_key = new BarcodeReader();
                        string QCfilename_key = Path.Combine(Server.MapPath(fname_key));
                        var QCresult_key = QCreader_key.Decode(new Bitmap(QCfilename_key));

                        GetDecryptData objdk = new GetDecryptData();
                        string result = objdk.GetData(QCresult_key.ToString());

                        Label lblIL = new Label();
                        lblIL.Text = AESCrypto.Decrypt(QCresult.Text, result);
                        TableCell IL = new TableCell();
                        IL.Controls.Add(lblIL);


                        row.Controls.Add(SlNo);
                        row.Controls.Add(logDate);
                        row.Controls.Add(IL);
                        Table1.Controls.Add(row);


                    }
                }
                else
                {
                    //lblMsg.Text = "No Record Found";
                }
            }
            catch
            {

            }
        }
    }
}