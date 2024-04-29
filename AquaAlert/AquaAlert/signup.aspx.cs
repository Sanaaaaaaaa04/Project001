using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace AquaAlert
{
    public partial class signup : System.Web.UI.Page
    {
        SqlConnection cnn = new SqlConnection();
        SqlCommand cmd = new SqlCommand();
        SqlDataAdapter da = new SqlDataAdapter();
        DataTable dt = new DataTable();
        DataSet ds = new DataSet();
        protected void Page_Load(object sender, EventArgs e)
        {
            cnn.ConnectionString = @"Data Source=asus\sqlexpress;Initial Catalog=aquaalert011;Integrated Security=True;Pooling=False";
            cnn.Open();
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            dt = new DataTable();
            cmd.CommandText = "Insert into signup(uname, contact, email, password, cpass)values('" + txtusername.Text.ToString() + "','" + txtcontact.Text.ToString() + "', '" + txtgmail.Text.ToString() + "','" + txtpassword.Text.ToString() + "','" + txtconpass.Text.ToString() + "')";
            cmd.Connection = cnn;
            cmd.ExecuteNonQuery();
            ShowMessageBox("Signup successfully");
            Response.Redirect("WebForm6.aspx");

        }
        private void ShowMessageBox(string message)
        {
            string script = "alert('" + message + "')";
            ScriptManager.RegisterStartupScript(this, GetType(), "MessageBox", script, true);
        }
    }
}