using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CloudBasedEncryptedCrime
{
    public partial class AddCrime : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            MyConnection obj = new MyConnection();
            string result = obj.AddCrime(int.Parse(Session["UserId"].ToString()), txtName.Text, txtCrimePlace.Text, txtDescription.Text);
            if (result == "1")
            {

                txtName.Text = txtCrimePlace.Text = txtDescription.Text = "";
                lblMsg.Text = "Police Station Crime Case Registered Successfully";
                lblMsg.ForeColor = System.Drawing.Color.Green;
            }

            else if (result == "0")
            {
                txtName.Text = txtCrimePlace.Text = txtDescription.Text = "";
                lblMsg.Text = "Police Station Crime Case Registration Error";
                lblMsg.ForeColor = System.Drawing.Color.Red;
            }
        }
    }
}