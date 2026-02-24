using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CloudBasedEncryptedCrime
{
    public partial class AddPoliceStaff : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            MyConnection obj = new MyConnection();
            Random rnd = new Random();
            int PoliceStaffId = (rnd.Next(100000, 999999) + DateTime.Now.Second);
            string Password = (rnd.Next(1000, 9999) + DateTime.Now.Second).ToString();
            string result = obj.AddPoliceStaff(PoliceStaffId, txtName.Text, Password,ddlRole.SelectedItem.Text, txtMobileNo.Text, txtEmailId.Text, txtAddress.Text);
            if (result == "1")
            {

                string Message = "Login Credentials Police Staff Id:" + PoliceStaffId + " & Password:" + Password;
                SendEmail.Send(txtEmailId.Text, Message, "Login Credentials");
                txtName.Text = txtEmailId.Text = txtMobileNo.Text = txtAddress.Text = "";
                lblMsg.Text = "Police Staff Created Successfully & Credentials Mailed";
                lblMsg.ForeColor = System.Drawing.Color.Green;
            }
            else if (result == "2")
            {
                txtName.Text = txtEmailId.Text = txtMobileNo.Text = txtAddress.Text = "";
                lblMsg.Text = "Police Staff Email Id Already Registered";
                lblMsg.ForeColor = System.Drawing.Color.Red;
            }
            else if (result == "0")
            {
                txtName.Text = txtEmailId.Text = txtMobileNo.Text = txtAddress.Text = "";
                lblMsg.Text = "Police Staff Creation Error";
                lblMsg.ForeColor = System.Drawing.Color.Red;
            }
        }
    }
}