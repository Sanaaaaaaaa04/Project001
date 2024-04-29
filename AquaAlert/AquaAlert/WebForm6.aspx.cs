using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace AquaAlert
{
    public partial class WebForm6 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string userEmail = txtmail.Text.ToString();
            string userPassword = txtpass.Text.ToString();

            bool isAuthenticated = ValidateUser(userEmail, userPassword);

            if (isAuthenticated)
            {
                // Redirect to home page if login is successful
                Response.Redirect("personalinfo.aspx");
            }
            else
            {
                // Show error message if login failed
                ClientScript.RegisterStartupScript(GetType(), "InvalidLogin", "alert('Invalid email or password. Please try again.');", true);
            }
        }
        private bool ValidateUser(string userEmail, string userPassword)
{
    // Connection string to your SQL Server database
    string connectionString = @"Data Source=asus\sqlexpress;Initial Catalog=aquaalert011;Integrated Security=True;Pooling=False";

    string query = "SELECT password FROM signup WHERE email = @gmail";
    string savedPassword = null;

    using (SqlConnection connection = new SqlConnection(connectionString))
    {
        SqlCommand command = new SqlCommand(query, connection);
        command.Parameters.AddWithValue("@gmail", userEmail);

        try
        {
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                savedPassword = reader["password"].ToString();
            }
            reader.Close();
        }
        catch (Exception ex)
        {
            // Handle exception (e.g., log or display error)
            Console.WriteLine("Error: " + ex.Message);
        }
    }

    // Compare retrieved password with user's input
    return (savedPassword != null && savedPassword == userPassword);

        }
    }
}