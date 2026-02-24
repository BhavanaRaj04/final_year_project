using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CloudBasedEncryptedCrime
{
    public partial class OTPVerify : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnOTPVerify_Click(object sender, EventArgs e)
        {
            if (Session["OTP"].ToString() == txtOTP.Text)
            {
                switch (Session["UserType"].ToString())
                {
                    case "Police Station":
                        Response.Redirect("PoliceStationHome.aspx");
                        break;
                    case "FSL Staff":
                        Response.Redirect("FSLHome.aspx");
                        break;
                    case "Police Staff":
                        Response.Redirect("PoliceStaffHome.aspx");
                        break;
                    case "Court Judge":
                        Response.Redirect("CJHome.aspx");
                        break;
                }
            }
        }

        protected void btnHome_Click(object sender, EventArgs e)
        {
            Response.Redirect("index.aspx");
        }
    }
}