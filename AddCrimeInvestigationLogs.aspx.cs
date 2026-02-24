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
    public partial class AddCrimeInvestigationLogs : System.Web.UI.Page
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

        List<CrimeInvestigationLog> CrimeLoglst = new List<CrimeInvestigationLog>();
        AmazonS3Client _s3ClientObj = null;
        Tuple<byte[], byte[]> T = null;
        string _path = "~/DownloadFile/";
        protected void btnSave_Click(object sender, EventArgs e)
        {
            string fname = ddlCrime.SelectedItem.Value + "_crimelog.json";
            string filepath = _path + fname;

            _s3ClientObj = new AmazonS3Client("AKIA2ZBFNXZ3UIQMZIVG", "zESzpfaD2e65rAcg4tlQk6hXXcCnoIqA4NVQTDL1", Amazon.RegionEndpoint.USEast1);

            S3FileInfo file = new S3FileInfo(_s3ClientObj, "cloudtrackcrime2026", fname);
            bool filechk = file.Exists;

            if (filechk == false)
            {
                CrimeLoglst.Add(new CrimeInvestigationLog
                {
                    StaffId=Session["UserId"].ToString(),
                    StaffRole = Session["UserType"].ToString(),
                    CrimeId = ddlCrime.SelectedItem.Value,
                    LogDate = DateTime.Now.ToString(),
                    Description = txtDescription.Text
                });

                // Serialize the ArrayList to a JSON file
                string json = JsonConvert.SerializeObject(CrimeLoglst, Newtonsoft.Json.Formatting.Indented);
                File.WriteAllText(Server.MapPath(filepath), json);

                BasicAWSCredentials credentials = new BasicAWSCredentials("AKIA2ZBFNXZ3UIQMZIVG", "zESzpfaD2e65rAcg4tlQk6hXXcCnoIqA4NVQTDL1");

                // Create a new Amazon S3 client
                AmazonS3Client s3Client = new AmazonS3Client(credentials, Amazon.RegionEndpoint.USEast1);
                TransferUtility fileTransferUtility = new TransferUtility(s3Client);

                Random rnd = new Random();
                string Securekey = "KEY" + rnd.Next(9999);
                T = SymmetricCryptoClass.GenerateSymmetricKeys(Securekey);

                SymmetricCryptoClass.Encryption(Server.MapPath(filepath), T.Item1.ToString());
                string key2 = System.Text.ASCIIEncoding.ASCII.GetString(T.Item2);

                fileTransferUtility.Upload(Server.MapPath(filepath), "cloudtrackcrime2026", fname);
                //File.Delete((Server.MapPath(filepath)));

                MyConnection obj = new MyConnection();
                string File_Path = "cloudtrackcrime2026" + "/" + fname;
                string FKey1 = T.Item1.ToString();
                string FKey2 = T.Item2.ToString();
                string res = obj.CrimeInvestigationLog(int.Parse(ddlCrime.SelectedItem.Value), File_Path, FKey1, FKey2);

                if (res == "1")
                {
                    ddlCrime.SelectedIndex = 0;
                    txtDescription.Text = "";

                    lblMsg.Text = "Crime Investigation Log Uploaded to AWS Successfully";
                    lblMsg.ForeColor = System.Drawing.Color.Green;
                }

            }
            else
            {
                _s3ClientObj = new AmazonS3Client("AKIA2ZBFNXZ3UIQMZIVG", "zESzpfaD2e65rAcg4tlQk6hXXcCnoIqA4NVQTDL1", Amazon.RegionEndpoint.USEast1);
                fname = ddlCrime.SelectedItem.Value + "_crimelog.json";
                filepath = _path + fname;

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

                _s3ClientObj.DeleteObject(new Amazon.S3.Model.DeleteObjectRequest() { BucketName = "cloudtrackcrime2026", Key = fname });

                BasicAWSCredentials credentials = new BasicAWSCredentials("AKIA2ZBFNXZ3UIQMZIVG", "zESzpfaD2e65rAcg4tlQk6hXXcCnoIqA4NVQTDL1");

                // Create a new Amazon S3 client
                AmazonS3Client s3Client = new AmazonS3Client(credentials, Amazon.RegionEndpoint.USEast1);
                TransferUtility fileTransferUtility = new TransferUtility(s3Client);

                // Deserialize the ArrayList from the JSON file
                string jsonFromFile = File.ReadAllText(Server.MapPath(filepath));
                List<CrimeInvestigationLog> deserializedlst = JsonConvert.DeserializeObject<List<CrimeInvestigationLog>>(jsonFromFile);
                foreach (var item in deserializedlst)
                {

                    CrimeLoglst.Add(new CrimeInvestigationLog
                    {
                        StaffId=item.StaffId,
                        StaffRole=item.StaffRole,
                        CrimeId = item.CrimeId,
                        LogDate = item.LogDate,
                        Description = item.Description
                    });

                }

                CrimeLoglst.Add(new CrimeInvestigationLog
                {
                    StaffId = Session["UserId"].ToString(),
                    StaffRole = Session["UserType"].ToString(),
                    CrimeId = ddlCrime.SelectedItem.Value,
                    LogDate = DateTime.Now.ToString(),
                    Description = txtDescription.Text
                });

                // Serialize the ArrayList to a JSON file
                string json = JsonConvert.SerializeObject(CrimeLoglst, Newtonsoft.Json.Formatting.Indented);
                File.WriteAllText(Server.MapPath(filepath), json);

                credentials = new BasicAWSCredentials("AKIA2ZBFNXZ3UIQMZIVG", "zESzpfaD2e65rAcg4tlQk6hXXcCnoIqA4NVQTDL1");

                // Create a new Amazon S3 client
                s3Client = new AmazonS3Client(credentials, Amazon.RegionEndpoint.USEast1);
                fileTransferUtility = new TransferUtility(s3Client);

                Random rnd = new Random();
                string Securekey = "KEY" + rnd.Next(9999);
                T = SymmetricCryptoClass.GenerateSymmetricKeys(Securekey);

                SymmetricCryptoClass.Encryption(Server.MapPath(filepath), T.Item1.ToString());
                string key2 = System.Text.ASCIIEncoding.ASCII.GetString(T.Item2);

                fileTransferUtility.Upload(Server.MapPath(filepath), "cloudtrackcrime2026", fname);
                //File.Delete((Server.MapPath(filepath)));

                obj = new MyConnection();
                string File_Path = "cloudtrackcrime2026" + "/" + fname;
                string FKey1 = T.Item1.ToString();
                string FKey2 = T.Item2.ToString();
                string res = obj.UpdateCrimeInvestigationLog(int.Parse(ddlCrime.SelectedItem.Value), File_Path, FKey1, FKey2);

                if (res == "1")
                {
                    ddlCrime.SelectedIndex = 0;
                    txtDescription.Text = "";

                    lblMsg.Text = "Crime Investigation Log Uploaded to AWS Successfully";
                    lblMsg.ForeColor = System.Drawing.Color.Green;
                }
            }
        }
    }
}