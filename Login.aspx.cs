using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace CloudBasedEncryptedCrime
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            MyConnection obj = new MyConnection();
            int result = obj.LoginVerify(txtUserId.Text, txtPassword.Text, ddlUserType.SelectedItem.Text);
            if (result == 1)
            {
                Session["UserId"] = txtUserId.Text;
                Session["Password"] = txtPassword.Text;
                Session["UserType"] = ddlUserType.SelectedItem.Text;
                if (ddlUserType.SelectedItem.Text == "Application Manager")
                {
                    Response.Redirect("ApplicationManagerHome.aspx");
                }
                else {
                    obj = new MyConnection();
                    DataTable tab = new DataTable();
                    tab = obj.GetEmailStaff(txtUserId.Text, ddlUserType.SelectedItem.Text);
                    Random rnd = new Random();
                    int OTP = rnd.Next(1000, 9999);
                    Session["OTP"] = OTP;
                    string Message = "OTP:" + OTP ;
                    SendEmail.Send(tab.Rows[0]["EmailId"].ToString(), Message, "Login Verify OTP");
                    Response.Redirect("OTPVerify.aspx");
                }
            }
            else
            {
                lblMsg.Text = "Invalid UserId/Password";
                lblMsg.ForeColor = System.Drawing.Color.Red;
            }
        }

        protected void btnHome_Click(object sender, EventArgs e)
        {
            Response.Redirect("index.aspx");
        }
    }
}