using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace CloudBasedEncryptedCrime
{
    public partial class MapCrimePoliceStaff : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                MyConnection obj = new MyConnection();
                DataTable tab = new DataTable();
                tab = obj.GetPoliceStaff_Role(int.Parse(Session["UserId"].ToString()));
                ddlPoliceStaff.DataSource = tab;
                ddlPoliceStaff.DataTextField = "Name";
                ddlPoliceStaff.DataValueField = "PoliceStaffId";
                ddlPoliceStaff.DataBind();
                ddlPoliceStaff.Items.Insert(0, "--Select--");

                obj = new MyConnection();
                DataTable tabcourt = new DataTable();
                tabcourt = obj.GetCrime(int.Parse(Session["UserId"].ToString()));
                ddlCrime.DataSource = tabcourt;
                ddlCrime.DataTextField = "CrimeName";
                ddlCrime.DataValueField = "CrimeId";
                ddlCrime.DataBind();
                ddlCrime.Items.Insert(0, "--Select--");

            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            MyConnection obj = new MyConnection();
            string res = obj.MapPoliceStaffCrime(int.Parse(ddlCrime.SelectedItem.Value), int.Parse(ddlPoliceStaff.SelectedItem.Value));
            if (res == "1")
            {
                ddlCrime.SelectedIndex = 0;
                ddlPoliceStaff.SelectedIndex = 0;
                lblMsg.Text = "Police Staff Mapped to Crime Successfully";
                lblMsg.ForeColor = System.Drawing.Color.Green;
            }
            else if (res == "2")
            {
                ddlCrime.SelectedIndex = 0;
                ddlPoliceStaff.SelectedIndex = 0;
                lblMsg.Text = "Police Staff Mapped to Crime Already";
                lblMsg.ForeColor = System.Drawing.Color.Red;
            }
            else
            {
                ddlCrime.SelectedIndex = 0;
                ddlPoliceStaff.SelectedIndex = 0;
                lblMsg.Text = "Police Staff Mapped to Crime Error";
                lblMsg.ForeColor = System.Drawing.Color.Red;
            }
        }
    }
}