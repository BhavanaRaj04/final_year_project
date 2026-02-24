using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;
using System.IO;
using System.Collections.ObjectModel;
using Sha2;
using ZXing;
using System.Drawing;
using System.Drawing.Imaging;
using Amazon.S3;
using Amazon.S3.Model;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Amazon.Runtime;
using Amazon.S3.Transfer;

namespace CloudBasedEncryptedCrime
{
    public partial class FSLCrimeReportGenerate : System.Web.UI.Page
    {
        AmazonS3Client _s3ClientObj = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                MyConnection obj = new MyConnection();
                DataTable tab = new DataTable();
                tab = obj.GetPoliceStation();
                ddlPoliceStation.DataSource = tab;
                ddlPoliceStation.DataTextField = "Name";
                ddlPoliceStation.DataValueField = "PoliceStationId";
                ddlPoliceStation.DataBind();
                ddlPoliceStation.Items.Insert(0, "--Select--");
            }
        }

        protected void ddlPoliceStation_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                MyConnection obj = new MyConnection();
                DataTable tab = new DataTable();
                tab = obj.GetCrime(int.Parse(ddlPoliceStation.SelectedItem.Value));
                ddlCrime.DataSource = tab;
                ddlCrime.DataTextField = "CrimeName";
                ddlCrime.DataValueField = "CrimeId";
                ddlCrime.DataBind();
                ddlCrime.Items.Insert(0, "--Select--");
            }
            catch
            {
            }
        }

        static string filename;
        Tuple<byte[], byte[]> T = null;
        protected void btnSave_Click(object sender, EventArgs e)
        {
            MyConnection obj = new MyConnection();
            string logdate = DateTime.Now.ToString();
            //objDTO.LogDate = logdate;
            Random rnd = new Random();


            rnd = new Random();

            filename = ddlCrime.SelectedItem.Value + "_" + rnd.Next(2000) + DateTime.Now.Second + ".pdf";
            string filepath = "~/CrimeRecord/";
            filepath += filename;
            FileStream fs = new FileStream(Server.MapPath(filepath), FileMode.Create, FileAccess.Write, FileShare.None);
            iTextSharp.text.Rectangle rec2 = new iTextSharp.text.Rectangle(PageSize.A4);
            Document doc = new Document(rec2, 36, 72, 20, 180);  // page size , left , right , top , bottom
            PdfWriter writer = PdfWriter.GetInstance(doc, fs);
            doc.Open();

            string Body = "Forensic Lab Report";
            Paragraph Header = new Paragraph(Body);
            Header.Font.Color = iTextSharp.text.BaseColor.BLACK;
            Header.Alignment = Element.ALIGN_CENTER;
            Header.Font.Size = 16;
            doc.Add(Header);

            string Space = "\n\n";
            Paragraph ReasonSpace = new Paragraph(Space);
            doc.Add(ReasonSpace);

            PdfContentByte cb = writer.DirectContent;
            cb.SetLineWidth(2.0f);   // Make a bit thicker than 1.0 default
            cb.SetGrayStroke(0.95f); // 1 = black, 0 = white
            cb.MoveTo(20, 30);
            cb.LineTo(400, 30);
            cb.Stroke();

            PdfPTable table = new PdfPTable(3);
            table.DefaultCell.Border = 0;

            string Police = "Police Station:" + ddlPoliceStation.SelectedItem.Text + "\n" + "Crime Name:" + ddlCrime.SelectedItem.Text + "\n\n\n";
            Paragraph PoliceHeader = new Paragraph(Police);
            PoliceHeader.Font.Color = iTextSharp.text.BaseColor.BLACK;
            PoliceHeader.Font.Size = 10;

            PdfPCell cellName = new PdfPCell(new Phrase(PoliceHeader));
            cellName.Colspan = 2;
            cellName.Border = 0;
            table.AddCell(cellName);

            DataTable tab = new DataTable();
            tab = obj.GetForensicStaff(int.Parse(Session["UserId"].ToString()));

            string Forensic = "Forensic Name:" + tab.Rows[0]["Name"].ToString() + "\n" + "MobileNo:" + tab.Rows[0]["MobileNo"].ToString() + "\n\n\n";
            Paragraph ForensicHeader = new Paragraph(Forensic);
            ForensicHeader.Font.Color = iTextSharp.text.BaseColor.BLACK;
            ForensicHeader.Font.Size = 10;

            PdfPCell cellForensic = new PdfPCell(new Phrase(ForensicHeader));
            cellForensic.Border = 0;
            table.AddCell(cellForensic);

            doc.Add(table);

            Paragraph p = new Paragraph(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(0.0F, 100.0F, BaseColor.BLACK, Element.ALIGN_LEFT, 0.0f)));
            doc.Add(p);

            string date = "Date : " + DateTime.Now.ToString();
            Paragraph DateHeader = new Paragraph(date);
            DateHeader.Font.Color = iTextSharp.text.BaseColor.BLACK;
            DateHeader.Alignment = Element.ALIGN_LEFT;
            DateHeader.Font.Size = 10;
            doc.Add(DateHeader);

            string Space1 = "\n\n";
            Paragraph ReasonSpace1 = new Paragraph(Space1);
            doc.Add(ReasonSpace1);

            string Description = txtDescription.Text;
            Paragraph DescriptionHeader = new Paragraph(Description);
            DescriptionHeader.Font.Color = iTextSharp.text.BaseColor.BLACK;
            DescriptionHeader.Alignment = Element.ALIGN_LEFT;
            DescriptionHeader.Font.Size = 12;

            doc.Add(DescriptionHeader);

            doc.Close();

            string Securekey = "KEY" + rnd.Next(9999);
            T = SymmetricCryptoClass.GenerateSymmetricKeys(Securekey);

            SymmetricCryptoClass.Encryption(Server.MapPath(filepath), T.Item1.ToString());
            string key2 = System.Text.ASCIIEncoding.ASCII.GetString(T.Item2);


            ////Amazon AWS 
            BasicAWSCredentials credentials = new BasicAWSCredentials("AKIA2ZBFNXZ3UIQMZIVG", "zESzpfaD2e65rAcg4tlQk6hXXcCnoIqA4NVQTDL1");
            // Create a new Amazon S3 client
            AmazonS3Client s3Client = new AmazonS3Client(credentials, Amazon.RegionEndpoint.USEast1);
            TransferUtility fileTransferUtility = new TransferUtility(s3Client);
            fileTransferUtility.Upload(Server.MapPath(filepath), "cloudtrackcrime2026", filename);


            obj = new MyConnection();
            string File_Path = "cloudtrackcrime2026" + "/" + filename;
            string FKey1 = T.Item1.ToString();
            string FKey2 = T.Item2.ToString();
            string res = obj.CreateTable_FSRG(int.Parse(ddlPoliceStation.SelectedItem.Value), int.Parse(ddlCrime.SelectedItem.Value), int.Parse(Session["UserId"].ToString()), logdate, FKey1, FKey2, File_Path);
            if (res == "1")
            {
                ddlCrime.SelectedIndex = 0;
                ddlPoliceStation.SelectedIndex = 0;
                txtDescription.Text = "";
                lblMsg.Text = "Forensic Report Generated Successfully";
                lblMsg.ForeColor = System.Drawing.Color.Green;
            }

            else if (res == "0")
            {
                // txtName.Text = txtCrimePlace.Text = "";
                lblMsg.Text = "Forensic Report Generate Error";
                lblMsg.ForeColor = System.Drawing.Color.Red;
            }
        }

        
    }
}