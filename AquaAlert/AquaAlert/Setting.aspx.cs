using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace AquaAlert
{
    public partial class Setting : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Load existing alarms from the database
                LoadAlarms();
            }
        }

        protected void LoadAlarms()
        {
            // Load alarms from the database and render them on the page
            List<DateTime> alarms = new List<DateTime>(); // Dummy list, replace it with your logic to load alarms from the database
            foreach (DateTime alarm in alarms)
            {
                AddAlarmToClient(alarm);
                string connectionString = @"Data Source=asus\sqlexpress;Initial Catalog=aquaalert011;Integrated Security=True;Pooling=False";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "SELECT AlarmDateTime FROM Alarms";
                    SqlCommand command = new SqlCommand(query, connection);
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        DateTime alarmDateTime = Convert.ToDateTime(reader["AlarmDateTime"]);
                        AddAlarmToClient(alarmDateTime);
                    }
                    reader.Close();
                }
            }
        }

        protected void AddAlarmToClient(DateTime alarmDateTime)
        {
            string script = "<script type=\"text/javascript\"> addAlarmToClient('" + alarmDateTime.ToString() + "'); </script>";

            if (!Page.ClientScript.IsStartupScriptRegistered("AddAlarm"))
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "AddAlarm", script);
            }
            Page.ClientScript.RegisterStartupScript(this.GetType(), "AddAlarm",
    string.Format("<script type='text/javascript'>addAlarmToClient('{0}');</script>", alarmDateTime.ToString()), false);
        }

        protected void btnAddAlarm_Click(object sender, EventArgs e)
        {
            // Add alarm to the database and render it on the page
            DateTime alarmDateTime = GetDateTimeFromInput(); // Get alarm date and time from the input controls
            AddAlarmToClient(alarmDateTime);
        }

        protected DateTime GetDateTimeFromInput()
        {
            // Parse date and time from the input controls and return as DateTime
            string date = Request.Form["date"];
            string time = Request.Form["time"];
            DateTime alarmDateTime = DateTime.Parse(date + " " + time); // Combine date and time
            return alarmDateTime;
        }
            protected void AddAlarmToDatabase(DateTime alarmDateTime)
            {
                string connectionString = @"Data Source=asus\sqlexpress;Initial Catalog=aquaalert011;Integrated Security=True;Pooling=False";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO Alarms (AlarmDateTime) VALUES (@AlarmDateTime)";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@AlarmDateTime", alarmDateTime);
                connection.Open();
                command.ExecuteNonQuery();
            }
            }
                protected void btnAddAlarm1_Click(object sender, EventArgs e)
        {
            DateTime alarmDateTime = DateTime.Parse(Request.Form["AlarmDateTime"]);
            AddAlarmToDatabase(alarmDateTime);
            AddAlarmToClient(alarmDateTime);
         }
    }
}