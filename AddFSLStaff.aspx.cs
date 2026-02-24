using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CloudBasedEncryptedCrime
{
    public partial class AddFSLStaff : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            MyConnection obj = new MyConnection();
            Random rnd = new Random();
            int FSLId = (rnd.Next(100000, 999999) + DateTime.Now.Second);
            string Password = (rnd.Next(1000, 9999) + DateTime.Now.Second).ToString();
            string result = obj.CreateFSLStaff(FSLId, txtFSLStaff.Text, Password, txtMobileNo.Text, txtEmailId.Text, txtAddress.Text);
            if (result == "1")
            {

                string Message = "Login Credentials FSL Staff Id:" + FSLId + " & Password:" + Password;
                SendEmail.Send(txtEmailId.Text, Message,"Login Credentials");
                txtFSLStaff.Text = txtEmailId.Text = txtMobileNo.Text = txtAddress.Text = "";
                lblMsg.Text = "FSL Staff Created Successfully & Credentials Mailed";
                lblMsg.ForeColor = System.Drawing.Color.Green;
            }
            else if (result == "2")
            {
                txtFSLStaff.Text = txtEmailId.Text = txtMobileNo.Text = txtAddress.Text = "";
                lblMsg.Text = "FSL Staff Email Id Already Registered";
                lblMsg.ForeColor = System.Drawing.Color.Red;
            }
            else if (result == "0")
            {
                txtFSLStaff.Text = txtEmailId.Text = txtMobileNo.Text = txtAddress.Text = "";
                lblMsg.Text = "FSL Staff Creation Error";
                lblMsg.ForeColor = System.Drawing.Color.Red;
            }
        }
    }
}