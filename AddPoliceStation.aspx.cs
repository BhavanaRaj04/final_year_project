using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace CloudBasedEncryptedCrime
{
    public partial class AddPoliceStation : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                MyConnection obj = new MyConnection();
                DataTable tab = new DataTable();
                tab = obj.GetArea();
                ddlArea.DataSource = tab;
                ddlArea.DataTextField = "AreaName";
                ddlArea.DataValueField = "AreaId";
                ddlArea.DataBind();
                ddlArea.Items.Insert(0, "--Select--");
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            MyConnection obj = new MyConnection();
            Random rnd = new Random();
            int PoliceStationId = (rnd.Next(100000, 999999) + DateTime.Now.Second);
            string Password = (rnd.Next(1000, 9999) + DateTime.Now.Second).ToString();
            string result = obj.CreatePoliceStation(PoliceStationId, int.Parse(ddlArea.SelectedItem.Value), txtName.Text, Password, txtMobileNo.Text, txtEmailId.Text, txtPincode.Text, txtAddress.Text);
            if (result == "1")
            {
                ddlArea.SelectedIndex = 0;
                string Message = "Login Credentials Police Station Id:" + PoliceStationId + " & Password:" + Password;
                SendEmail.Send(txtEmailId.Text, Message,"Login Credentials");
                txtName.Text = txtEmailId.Text = txtMobileNo.Text = txtAddress.Text = txtPincode.Text = "";
                lblMsg.Text = "Police Station Created Successfully & Credentials Mailed";
                lblMsg.ForeColor = System.Drawing.Color.Green;
            }
            else if (result == "2")
            {
                ddlArea.SelectedIndex = 0;
                txtName.Text = txtEmailId.Text = txtMobileNo.Text = txtAddress.Text = txtPincode.Text = "";
                lblMsg.Text = "Police Station Creation Already,Selected Area";
                lblMsg.ForeColor = System.Drawing.Color.Red;
            }
            else if (result == "0")
            {
                ddlArea.SelectedIndex = 0;
                txtName.Text = txtEmailId.Text = txtMobileNo.Text = txtAddress.Text = txtPincode.Text = "";
                lblMsg.Text = "Police Station Creation Error";
                lblMsg.ForeColor = System.Drawing.Color.Red;
            }
        }
    }
}