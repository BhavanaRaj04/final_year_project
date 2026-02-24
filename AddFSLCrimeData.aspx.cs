using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace CloudBasedEncryptedCrime
{
    public partial class AddFSLCrimeData : System.Web.UI.Page
    {
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
        protected void btnSave_Click(object sender, EventArgs e)
        {
            MyConnection obj = new MyConnection();
            string result = obj.AddForensicDC(int.Parse(Session["UserId"].ToString()), int.Parse(ddlCrime.SelectedItem.Value), txtDescription.Text);
            if (result == "1")
            {
                ddlPoliceStation.SelectedIndex = 0;
                ddlCrime.SelectedIndex = 0;
                txtDescription.Text = "";
                lblMsg.Text = "Crime Forensics Data Collected Successfully";
                lblMsg.ForeColor = System.Drawing.Color.Green;
            }

            else if (result == "0")
            {
                ddlPoliceStation.SelectedIndex = 0;
                ddlCrime.SelectedIndex = 0;
                txtDescription.Text = "";
                lblMsg.Text = "Crime Forensics Data Collect Error";
                lblMsg.ForeColor = System.Drawing.Color.Red;
            }
        }
    }
}