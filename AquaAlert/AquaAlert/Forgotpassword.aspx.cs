using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography; // For password hashing
using System.Text.RegularExpressions;

namespace AquaAlert
{
    public partial class Forgotpassword : System.Web.UI.Page
    {
        private const int otpLength = 6;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Initialize OTP on page load
                GenerateOTP();
            }
        }

        protected void btnSendOTP_Click(object sender, EventArgs e)
        {
            string email = txtEmail.Text.Trim();

            // Validate email format using a regular expression
            if (IsValidEmail(email))
            {
                try
                {
                    // Send OTP to the user's email with improved security
                    SendOTP(email);
                    lblMessage.Text = "OTP has been sent to your email.";

                }
                catch (Exception ex)
                {
                    lblMessage.Text = "An error occurred while sending OTP. Please try again later.";
                    // Log the exception for debugging (optional)
                    Console.WriteLine(ex.Message);
                }
            }
            else
            {
                lblMessage.Text = "Invalid email address.";
            }
        }

        protected void btnVerifyOTP_Click(object sender, EventArgs e)
        {
            string Enteredotp = txtOTP.Text.Trim();
            string storedOTP = (string)Session["OTP"];

            if (Enteredotp.Equals(storedOTP))
            {
                lblMessage.Text = "OTP verified successfully.";

                // *Optional:* Store user email in session for password reset (consider using a secure mechanism like a signed cookie)
                Session["ResetEmail"] = txtEmail.Text.Trim();
                
            }
            else
            {
                lblMessage.Text = "Wrong OTP. Please try again.";
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (txtNewPassword.Text.Trim() != txtConfirmPassword.Text.Trim())
            {
                lblMessage.Text = "New password and confirm password do not match.";
                return;
            }

            // Retrieve email from session (if stored)
            string email = (string)Session["ResetEmail"];

            if (string.IsNullOrEmpty(email))
            {
                lblMessage.Text = "Invalid reset request. Please initiate password reset again.";
                return;
            }

            string newPassword = txtNewPassword.Text.Trim();
            string conpass = txtConfirmPassword.Text.Trim();

            // Update password in the database with hashed password
            UpdatePassword(email, newPassword, conpass);
            lblMessage.Text = "Password updated successfully.";

            // Clear session data after successful reset
            Session["ResetEmail"] = null;
        }
        private bool IsValidEmail(string email)
        {
            // Use a regular expression to validate email format (example)
            string pattern = @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+[a-zA-Z]{2,}))$";
            Match match = Regex.Match(email, pattern);
            return match.Success;
        }

        private void GenerateOTP()
        {
            Console.WriteLine("inside generate otp");
            // Generate a random OTP
            string otp = new Random().Next((int)Math.Pow(10, otpLength - 1), (int)Math.Pow(10, otpLength)).ToString();
            // Store the OTP in a session variable
            Session["OTP"] = otp;
        }

        private void SendOTP(string toEmail)
        {
            GenerateOTP();


            string fromEmail = "yours0296@gmail.com";
            string password = "utntqlabfdfrbles";

            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress(fromEmail);
            mailMessage.To.Add(toEmail);
            mailMessage.Subject = "OTP for Password Reset";
            mailMessage.Body = "Your OTP is: " + (string)Session["OTP"];

            SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587); // Gmail SMTP server and port
            smtpClient.EnableSsl = true;
            smtpClient.Credentials = new NetworkCredential(fromEmail, password);

            try
            {
                smtpClient.Send(mailMessage);
                
                Session["RemainingSeconds"] = 180;
            }
            catch (Exception ex)
            {
                // Handle exception (log the error or display a user-friendly message)
                Console.WriteLine(ex.Message);
                lblMessage.Text = "An error occurred while sending OTP. Please try again later.";
            }
        }

        /*  private string HashPassword(string password)
           {
               // Use a secure hashing algorithm (e.g., bcrypt or PBKDF2)
               // This example uses SHA256 (not recommended for password hashing)
               // for demonstration purposes only. Replace with a stronger algorithm.

               using (var sha256 = SHA256.Create())
               {
                   var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                   return Convert.ToHexString(hashedBytes);
               }
           }
               */
        private void UpdatePassword(string email, string newPassword,string conpass)
        {
            // Update the password in the database with hashed password
            string connectionString = @"Data Source=asus\sqlexpress;Initial Catalog=aquaalert011;Integrated Security=True;Pooling=False";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "UPDATE signup SET password = @Password,cpass=@Cpass WHERE email = @Email";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Email", email);
                    command.Parameters.AddWithValue("@Password", newPassword);
                    command.Parameters.AddWithValue("@Cpass", conpass);
                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }

        protected void otpTimer_Tick(object sender, EventArgs e)
        {
            int remainingSeconds = (int)Session["RemainingSeconds"];
            remainingSeconds--;

            // Update the session variable
            Session["RemainingSeconds"] = remainingSeconds;

            // Format the remaining time as minutes and seconds
            int minutes = remainingSeconds / 60;
            int seconds = remainingSeconds % 60;
            string formattedTime = string.Format("{0:00}:{1:00}", minutes, seconds);

            
            // Check if the timer has expired
            if (remainingSeconds <= 0)
            {
                lblMessage.Text = "OTP expired. Please request a new OTP.";
                
            }

        }
    }
}