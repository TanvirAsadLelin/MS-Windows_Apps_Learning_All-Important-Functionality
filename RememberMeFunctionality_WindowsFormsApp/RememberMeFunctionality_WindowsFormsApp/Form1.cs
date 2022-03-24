using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Data.SqlClient;
using System.Configuration;

namespace RememberMeFunctionality_WindowsFormsApp
{
    public partial class Form1 : Form
    {
        string cs = ConfigurationManager.ConnectionStrings["DBConnectiostring"].ConnectionString;
        public Form1()
        {
            InitializeComponent();
            LoadCredentials();
        }

        void SaveCredentials()
        {

            if(rememberMeCheckBox.Checked == true)
            {
                Properties.Settings.Default.Username = usernameTextBox.Text;
                Properties.Settings.Default.Password = PasswordTextBox.Text;
                Properties.Settings.Default.Save();
            }
            else
            {
                Properties.Settings.Default.Username = "";
                Properties.Settings.Default.Password = "";
                Properties.Settings.Default.Save();
            }
        }

        void LoadCredentials()
        {

            if (Properties.Settings.Default.Username != String.Empty)
            {
                usernameTextBox.Text =  Properties.Settings.Default.Username;
                PasswordTextBox.Text=Properties.Settings.Default.Password;
                
                rememberMeCheckBox.Checked = true;
            }
            else
            {
               
            }
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {   

            if(string.IsNullOrEmpty(usernameTextBox.Text) == true && string.IsNullOrEmpty(PasswordTextBox.Text) == true)
            {

                MessageBox.Show("Username and Password is empty!" ,"Empty",MessageBoxButtons.OK,MessageBoxIcon.Information);

            }

            else
            {
                SqlConnection con = new SqlConnection(cs);
                string query = "select * from RememberLogin_Tbl where Username = @username and Password = @pass";
                SqlCommand cmd =  new SqlCommand(query, con);

                cmd.Parameters.AddWithValue("@username", usernameTextBox.Text);  
                cmd.Parameters.AddWithValue("@pass", PasswordTextBox.Text); 
                
                con.Open();

                SqlDataReader dr  =  cmd.ExecuteReader();

                if(dr.HasRows == true)
                {
                    MessageBox.Show("Successfuly Login!");
                    SaveCredentials();
                    this.Hide();
                    WelcomeForm welcomeForm = new WelcomeForm();
                    welcomeForm.ShowDialog();
                }

                else
                {
                    MessageBox.Show("Login failed!");
                }
                con.Close();

            }




            //if(usernameTextBox.Text == "lelin" && PasswordTextBox.Text == "1234")
            //{
            //    MessageBox.Show("Successfuly Login!");
            //    SaveCredentials();
            //    this.Hide();
            //    WelcomeForm welcomeForm = new WelcomeForm();
            //    welcomeForm.ShowDialog();
            //}
            //else
            //{
            //    MessageBox.Show("Login failed!");
            //}
        }
    }
}
