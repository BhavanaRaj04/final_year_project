using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CloudBasedEncryptedCrime
{
    public partial class AddArea : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            MyConnection obj = new MyConnection();
            string result = obj.CreateArea(txtAreaName.Text);
            if (result == "1")
            {
                txtAreaName.Text = "";
                lblMsg.Text = "Area Added Successfully";
                lblMsg.ForeColor = System.Drawing.Color.Green;
            }
            else if (result == "2")
            {
                txtAreaName.Text = "";
                lblMsg.Text = "Area Added Already";
                lblMsg.ForeColor = System.Drawing.Color.Red;
            }
            else if (result == "0")
            {
                txtAreaName.Text = "";
                lblMsg.Text = "Area Creation Error";
                lblMsg.ForeColor = System.Drawing.Color.Red;
            }
        }
    }
}