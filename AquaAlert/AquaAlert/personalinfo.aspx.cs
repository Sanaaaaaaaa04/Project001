using System;
using System.Configuration;
using System.Data.SqlClient;

namespace AquaAlert
{
    public partial class personalinfo : System.Web.UI.Page
    {
        protected void Button1_Click(object sender, EventArgs e)
        {
            string name = TextBox1.Text;
            string gender = DropDownList1.SelectedValue;
            int height;
            if (!int.TryParse(TextBox3.Text, out height))
            {
                // Handle invalid height input
                // For example, display an error message or take appropriate action
                return;
            }
            string wakeUpTime = DropDownList2.SelectedValue;
            string sleepingTime = DropDownList3.SelectedValue;

            // Insert data into database
            InsertPersonalInfo(name, gender, height, wakeUpTime, sleepingTime);

            // Clear input fields after submission
            TextBox1.Text = "";
            DropDownList1.SelectedIndex = 0;
            TextBox3.Text = "";
            DropDownList2.SelectedIndex = 0;
            DropDownList3.SelectedIndex = 0;
            Response.Redirect("calwaterintaek.aspx");
        }

        private void InsertPersonalInfo(string name, string gender, int height, string wakeUpTime, string sleepingTime)
        {
            string connectionString = @"Data Source=asus\sqlexpress;Initial Catalog=aquaalert011;Integrated Security=True;Pooling=False";
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO PersonalInfo (Name, Gender, Height, WakeUpTime, SleepingTime) VALUES (@Name, @Gender, @Height, @WakeUpTime, @SleepingTime)";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Name", name);
                cmd.Parameters.AddWithValue("@Gender", gender);
                cmd.Parameters.AddWithValue("@Height", height);
                cmd.Parameters.AddWithValue("@WakeUpTime", wakeUpTime);
                cmd.Parameters.AddWithValue("@SleepingTime", sleepingTime);
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}
