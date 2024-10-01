using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace QuizPortal
{
    public partial class profile : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if(Session["Username"] != null)
                {
                    string username = Session["Username"].ToString();
                    if(IsProfileExists(GetUserIdByUsername(username)))
                    {
                        LoadProfileData();
                    }
                    else
                    {
                        alertContent.InnerHtml = "Profile is required to proceed with Quiz.";
                        alertMessage.Attributes["class"] = "alert alert-warning";
                        alertMessage.Style["display"] = "block";

                        profileInfo.Visible = false;
                        btnMakeProfile.Visible = true;
                    }
                } 
                else
                {
                    Response.Redirect("login.aspx");
                }
            }
        }

        private bool IsProfileExists(int userid)
        {
            string connString = ConfigurationManager.ConnectionStrings["conQuizPortal"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connString))
            {
                string query = "SELECT Id FROM Profile WHERE user_id = @UserID";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@UserID", userid);
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    return reader.HasRows;
                }
            }
        }
        private int GetUserIdByUsername(string username)
        {
            int userID = 0;
            string connString = ConfigurationManager.ConnectionStrings["conQuizPortal"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connString))
            {
                string query = "SELECT Id FROM Users WHERE username = @Username";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Username", username);
                    conn.Open();
                    userID = Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
            return userID;
        }

        private void LoadProfileData()
        {
            alertMessage.Style["display"] = "none";
            int UserId = GetUserIdByUsername(Session["Username"].ToString());
            string connString = ConfigurationManager.ConnectionStrings["conQuizPortal"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connString))
            {
                string query = @"SELECT u.username, u.name, p.mobileno, p.dob 
                                 FROM Users u
                                 INNER JOIN Profile p ON u.Id = p.user_id
                                 WHERE u.Id = @UserId";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@UserId", UserId);
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        lblUsername.Text = reader["username"].ToString();
                        lblName.Text = reader["name"].ToString();
                        lblMobileNo.Text = reader["mobileno"].ToString();
                        lblDOB.Text = Convert.ToDateTime(reader["dob"]).ToString("yyyy-MM-dd");
                    }
                    conn.Close();
                }
            }
        }

        protected void btnMakeProfile_Click(object sender, EventArgs e)
        {
            txtModalName.Text = Session["Username"].ToString();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "showModal", "$('#profileModal').modal('show');", true);
        }

        protected void btnSubmitProfile_Click(object sender, EventArgs e)
        {
            int userId = GetUserIdByUsername(Session["Username"].ToString());
            string connString = ConfigurationManager.ConnectionStrings["conQuizPortal"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connString))
            {
                string query = "INSERT INTO Profile (user_id, mobileno, dob) VALUES (@UserID, @MobileNo, @DOB)";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@UserID", userId);
                    cmd.Parameters.AddWithValue("@MobileNo", txtModalMobileNo.Text);
                    cmd.Parameters.AddWithValue("@DOB", Convert.ToDateTime(txtModalDOB.Text));
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            LoadProfileData();
            profileInfo.Visible = true;
            btnMakeProfile.Visible = false;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "hideModal", "$('#profileModal').modal('hide');", true);
        }
        protected void btnLogout_Click(object sender, EventArgs e)
        {
            Session.Abandon();
            Response.Redirect("login.aspx");
        }
    }
}