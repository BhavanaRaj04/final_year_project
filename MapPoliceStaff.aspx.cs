using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace CloudBasedEncryptedCrime
{
    public partial class MapPoliceStaff : System.Web.UI.Page
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

                obj = new MyConnection();
                DataTable tabps = new DataTable();
                tabps = obj.GetPoliceStaff_Role();
                ddlPoliceStaff.DataSource = tabps;
                ddlPoliceStaff.DataTextField = "Name";
                ddlPoliceStaff.DataValueField = "PoliceStaffId";
                ddlPoliceStaff.DataBind();
                ddlPoliceStaff.Items.Insert(0, "--Select--");
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            MyConnection obj = new MyConnection();
            string result = obj.MapPoliceStaff_PS(int.Parse(ddlPoliceStaff.SelectedItem.Value),int.Parse(ddlPoliceStation.SelectedItem.Value));
            if (result == "1")
            {
                ddlPoliceStaff.SelectedIndex = 0;
                ddlPoliceStation.SelectedIndex = 0;
                lblMsg.Text = "Police Staff Mapped to Police Station Successfully";
                lblMsg.ForeColor = System.Drawing.Color.Green;
            }
            else if (result == "2")
            {
                ddlPoliceStaff.SelectedIndex = 0;
                ddlPoliceStation.SelectedIndex = 0;
                lblMsg.Text = "Police Staff Mapped to Police Station Already";
                lblMsg.ForeColor = System.Drawing.Color.Red;
            }
            else if (result == "0")
            {
                ddlPoliceStaff.SelectedIndex = 0;
                ddlPoliceStation.SelectedIndex = 0;
                lblMsg.Text = "Police Staff Mapped to Police Station Error";
                lblMsg.ForeColor = System.Drawing.Color.Red;
            }
        }
    }
}