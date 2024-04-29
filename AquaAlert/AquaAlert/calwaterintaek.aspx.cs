using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AquaAlert
{
    public partial class calwaterintaek : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnCalculate_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtAge.Text) || string.IsNullOrEmpty(txtWeight.Text))
            {
               
                return;
            }
            int age;
            double weight;
            if (!int.TryParse(txtAge.Text, out age) || !double.TryParse(txtWeight.Text, out weight))
            {
                
                return;
            }

            double waterIntake = CalculateWaterIntake(age, weight);
            
           // Response.Redirect("trial.aspx");
            Response.Redirect("rough22.aspx?waterIntake=" + waterIntake.ToString());
           

        
        }
         
        
         private double CalculateWaterIntake(int age, double weight)
        {
            return weight * 0.035;
        
        }
    }
}