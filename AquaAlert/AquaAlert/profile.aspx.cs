using System;

namespace YourNamespace
{
    public partial class profile : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadUserProfile();
                DisableEditMode();
            }
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            EnableEditMode();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            SaveUserProfile();
            DisableEditMode();
        }

        private void LoadUserProfile()
        {
            // Load user information (replace with your logic)
            txtFirstName.Text = "John";
            txtLastName.Text = "Doe";
            txtMobile.Text = "1234567890";
            txtEmail.Text = "john@example.com";
            // Load profile picture (if available)
            imgProfile.ImageUrl = "default-profile.png"; // Provide a default image path
        }

        private void EnableEditMode()
        {
            txtFirstName.Enabled = true;
            txtLastName.Enabled = true;
            txtMobile.Enabled = true;
            txtEmail.Enabled = true;
            btnSave.Visible = true;
            btnEdit.Visible = false;
        }

        private void DisableEditMode()
        {
            txtFirstName.Enabled = false;
            txtLastName.Enabled = false;
            txtMobile.Enabled = false;
            txtEmail.Enabled = false;
            btnSave.Visible = false;
            btnEdit.Visible = true;
        }

        private void SaveUserProfile()
        {
            // Save user information (replace with your logic)
        }
    }
}
