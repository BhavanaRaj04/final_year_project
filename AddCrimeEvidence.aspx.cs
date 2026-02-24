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
using System.Drawing.Imaging;

namespace CloudBasedEncryptedCrime
{
    public partial class AddCrimeEvidence : System.Web.UI.Page
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
            }
        }
        static string filename;
        AmazonS3Client _s3ClientObj = null;
        protected void btnSave_Click(object sender, EventArgs e)
        {
            Random rnd = new Random();
            Shamir objsh = new Shamir();
            int key = rnd.Next(1000, 9999);
            string attributedata = objsh.AttributeValue(key);
            attributedata = attributedata.Remove(0, 1);
            string DataKey = attributedata;

            string evidencelog = txtDescription.Text;

            string EncryptData = AESCrypto.EncryptData(evidencelog, key.ToString());

            Random rnd1 = new Random();
            var QCwriter = new BarcodeWriter();
            QCwriter.Format = BarcodeFormat.QR_CODE;
            var result = QCwriter.Write(EncryptData);
            string v = rnd1.Next(1000, 9999).ToString();
            filename = key + "_" + v + ".jpg";

            string filepath = "~/DownloadFile/" + filename;
            var barcodeBitmap = new Bitmap(result);

            using (MemoryStream memory = new MemoryStream())
            {
                using (FileStream fs = new FileStream(Server.MapPath(filepath),
                   FileMode.Create, FileAccess.ReadWrite))
                {
                    barcodeBitmap.Save(memory, ImageFormat.Jpeg);
                    byte[] bytes = memory.ToArray();
                    fs.Write(bytes, 0, bytes.Length);
                }
            }

            ////Amazon AWS 

            // Set up your AWS credentials
            BasicAWSCredentials credentials = new BasicAWSCredentials("AKIA2ZBFNXZ3UIQMZIVG", "zESzpfaD2e65rAcg4tlQk6hXXcCnoIqA4NVQTDL1");

            // Create a new Amazon S3 client
            AmazonS3Client s3Client = new AmazonS3Client(credentials, Amazon.RegionEndpoint.USEast1);
            TransferUtility fileTransferUtility = new TransferUtility(s3Client);

            fileTransferUtility.Upload(Server.MapPath(filepath), "cloudtrackcrime2026", filename);


            string FilePath = "cloudtrackcrime2026" + "/" + filename;


            ////Key write to QRCode Image

            Random rnd2 = new Random();
            var QCwriter_1 = new BarcodeWriter();
            QCwriter_1.Format = BarcodeFormat.QR_CODE;
            var result_1 = QCwriter.Write(DataKey);
            string v_1 = rnd2.Next(1000, 9999).ToString();
            filename = key + "_" + v_1 + ".jpg";

            string filepath_1 = "~/DownloadFile/" + filename;
            var barcodeBitmap_1 = new Bitmap(result_1);

            using (MemoryStream memory = new MemoryStream())
            {
                using (FileStream fs = new FileStream(Server.MapPath(filepath_1),
                   FileMode.Create, FileAccess.ReadWrite))
                {
                    barcodeBitmap_1.Save(memory, ImageFormat.Jpeg);
                    byte[] bytes = memory.ToArray();
                    fs.Write(bytes, 0, bytes.Length);
                }
            }

            ////Amazon AWS 

            // Set up your AWS credentials
            credentials = new BasicAWSCredentials("AKIA2ZBFNXZ3UIQMZIVG", "zESzpfaD2e65rAcg4tlQk6hXXcCnoIqA4NVQTDL1");

            // Create a new Amazon S3 client
            s3Client = new AmazonS3Client(credentials, Amazon.RegionEndpoint.USEast1);
            fileTransferUtility = new TransferUtility(s3Client);

            fileTransferUtility.Upload(Server.MapPath(filepath_1), "cloudtrackcrime2026", filename);


            string FilePath_1 = "cloudtrackcrime2026" + "/" + filename;


            MyConnection obj = new MyConnection();
            string res = obj.AddEvidenceLog(int.Parse(ddlCrime.SelectedItem.Value), int.Parse(Session["UserId"].ToString()), FilePath, FilePath_1);
            if (res == "1")
            {
                ddlCrime.SelectedIndex = 0;
                txtDescription.Text = "";
                lblMsg.Text = "Evidence Details Uploaded AWS Successfully";
                lblMsg.ForeColor = System.Drawing.Color.Green;
            }
            else if (res == "0")
            {
                ddlCrime.SelectedIndex = 0;
                txtDescription.Text = "";
                lblMsg.Text = "Evidence Details Uploaded AWS Error";
                lblMsg.ForeColor = System.Drawing.Color.Red;
            }
        }
    }
}