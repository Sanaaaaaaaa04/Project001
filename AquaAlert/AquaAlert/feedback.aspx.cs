using System;
using System.Configuration;
using System.Data.SqlClient;

namespace AquaAlert
{
    public partial class feedback : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            string name = txtName.Text;
            string email = txtEmail.Text;
            string feedback = txtFeedback.Text;

            // Save feedback to database or perform other actions
            SaveFeedbackToDatabase(name, email, feedback);

            // Show success message
            lblMessage.Text = "Thank you for your feedback!";
            lblMessage.Visible = true;

            // Clear input fields
            txtName.Text = "";
            txtEmail.Text = "";
            txtFeedback.Text = "";
        }

        private void SaveFeedbackToDatabase(string name, string email, string feedback)
        {
            string connectionString = @"Data Source=asus\sqlexpress;Initial Catalog=aquaalert011;Integrated Security=True;Pooling=False";
            string query = "INSERT INTO Feedback (Name, Email, FeedbackText, DateSubmitted) VALUES (@Name, @Email, @FeedbackText, GETDATE())";

            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Name", name);
                command.Parameters.AddWithValue("@Email", email);
                command.Parameters.AddWithValue("@FeedbackText", feedback);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }
    }
}