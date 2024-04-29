using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AquaAlert
{
    public partial class rough22 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DropDownList1.SelectedIndex = 0;

                if (Request.QueryString["waterIntake"] != null)
                {
                    double waterIntake = Convert.ToDouble(Request.QueryString["waterIntake"]);
                    Label1.Text = waterIntake.ToString("F2") + " liters per day.";
                    double intakePerCup = ((waterIntake / 8) * 1000);

                    cup1.Text = intakePerCup.ToString("F2") + " ml";
                    cup2.Text = intakePerCup.ToString("F2") + " ml";
                    cup3.Text = intakePerCup.ToString("F2") + " ml";
                    cup4.Text = intakePerCup.ToString("F2") + " ml";
                    cup5.Text = intakePerCup.ToString("F2") + " ml";
                    cup6.Text = intakePerCup.ToString("F2") + " ml";
                    cup7.Text = intakePerCup.ToString("F2") + " ml";
                    cup8.Text = intakePerCup.ToString("F2") + " ml";
                }
            }
        }

        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedValue = DropDownList1.SelectedValue;

            switch (selectedValue)
            {
                case "Profile":
                    Response.Redirect("profile.aspx");
                    break;
                case "Setting":
                    Response.Redirect("Setting.aspx");
                    break;
                case "Feedback":
                    Response.Redirect("feedback.aspx");
                    break;
                case "SignOut":
                    Response.Redirect("WebForm1.aspx");
                    break;
                default:
                    // Handle default case or show error message
                    break;
            }
        }
    }
}