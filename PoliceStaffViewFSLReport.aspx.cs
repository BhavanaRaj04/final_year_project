using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Amazon.S3;
using Amazon.S3.Model;
using System.Text;

namespace CloudBasedEncryptedCrime
{
    public partial class PoliceStaffViewFSLReport : System.Web.UI.Page
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
            LoadData();
        }

        protected void ddlCrime_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                LoadData();
            }
            catch
            {
            }
        }
        private void LoadData()
        {
            try
            {
                MyConnection obj = new MyConnection();
                DataTable tab = new DataTable();
                tab = obj.GetFSReport(int.Parse(ddlCrime.SelectedItem.Value));
                Table1.Controls.Clear();
                if (tab.Rows.Count > 0)
                {
                    TableRow hr = new TableRow();
                    TableHeaderCell hc1 = new TableHeaderCell();
                    TableHeaderCell hc2 = new TableHeaderCell();
                    TableHeaderCell hc3 = new TableHeaderCell();


                    hc1.Text = "Sl No";
                    hr.Cells.Add(hc1);
                    hc2.Text = "Log Date";
                    hr.Cells.Add(hc2);
                    hc3.Text = "";
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

                        LinkButton View = new LinkButton();
                        View.Text = "Download";
                        View.ID = "lnkView" + i.ToString();
                        View.CommandArgument = tab.Rows[i]["FSRId"].ToString();
                        View.Click += View_Click;

                        TableCell ViewCell = new TableCell();
                        ViewCell.Controls.Add(View);



                        row.Controls.Add(SlNo);
                        row.Controls.Add(logDate);
                        row.Controls.Add(ViewCell);
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


        AmazonS3Client _s3ClientObj = null;
        void View_Click(object sender, EventArgs e)
        {
            LinkButton lnk = (LinkButton)sender;
            Random rnd = new Random();
            MyConnection obj = new MyConnection();

            _s3ClientObj = new AmazonS3Client("AKIA2ZBFNXZ3UIQMZIVG", "zESzpfaD2e65rAcg4tlQk6hXXcCnoIqA4NVQTDL1", Amazon.RegionEndpoint.USEast1);

            DataTable tab = new DataTable();
            tab = obj.GetFSReportData(int.Parse(lnk.CommandArgument));
            string fpath = "~/CrimeRecord/" + tab.Rows[0]["FilePath"].ToString().Split('/')[1];
            GetObjectResponse _responseObj = _s3ClientObj.GetObject(new GetObjectRequest() { BucketName = tab.Rows[0]["FilePath"].ToString().Split('/')[0], Key = tab.Rows[0]["FilePath"].ToString().Split('/')[1] });
            _responseObj.WriteResponseStreamToFile(Server.MapPath(fpath));
            byte[] key = ASCIIEncoding.ASCII.GetBytes(tab.Rows[0]["FKey2"].ToString());
            SymmetricCryptoClass.Decryption(Server.MapPath(fpath), key.ToString());
            System.Diagnostics.Process.Start(Server.MapPath(fpath));
        }
    }
}