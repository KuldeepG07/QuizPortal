using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.SqlClient;
using System.Configuration;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace QuizPortal
{
    public partial class changepwd : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Username"]==null)
            {
                Response.Redirect("login.aspx");
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                string oldPassword = txtOldPassword.Text;
                string newPassword = txtNewPassword.Text;

                string username = Session["Username"].ToString();


                if (!string.IsNullOrEmpty(username))
                {
                    string connectionString = ConfigurationManager.ConnectionStrings["conQuizPortal"].ConnectionString;

                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        conn.Open();

                        string query = "SELECT COUNT(*) FROM Users WHERE username = @Username AND password = @OldPassword";
                        SqlCommand cmd = new SqlCommand(query, conn);
                        cmd.Parameters.AddWithValue("@Username", username);
                        cmd.Parameters.AddWithValue("@OldPassword", oldPassword);

                        int count = (int)cmd.ExecuteScalar();

                        if (count == 1)
                        {
                            string updateQuery = "UPDATE Users SET password = @NewPassword WHERE username = @Username";
                            SqlCommand updateCmd = new SqlCommand(updateQuery, conn);
                            updateCmd.Parameters.AddWithValue("@NewPassword", newPassword);
                            updateCmd.Parameters.AddWithValue("@Username", username);
                            updateCmd.ExecuteNonQuery();

                            Response.Redirect("profile.aspx?status=passwordChanged");
                        }
                        else
                        {
                            lblErrorMessage.Text = "Incorrect old password.";
                            lblErrorMessage.Visible = true;
                        }
                    }
                }
            }
        }
    }
}