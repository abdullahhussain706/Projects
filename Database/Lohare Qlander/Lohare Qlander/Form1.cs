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

namespace Lohare_Qlander
{
    public partial class Form1 : Form
    {
        SqlConnection con = new SqlConnection("Data Source=DESKTOP-P8LSDET\\SQLEXPRESS;Initial Catalog=lahoreqalanders;Integrated Security=True");
        private bool isPasswordVisible = false; // Track password visibility
       

        public Form1()
        {
            InitializeComponent();
            pictureBox3.Image = Properties.Resources.close_eye;
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }



        private void button1_Click_1(object sender, EventArgs e)
        {
            string username = textBox1.Text;
            string email = textBox2.Text;
            string password = textBox3.Text;

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Please fill out all fields.");
                return;
            }

            // SQL query to check for existing username or email
            string checkQuery = "SELECT COUNT(*) FROM Users WHERE Username = @Username OR Email = @Email";

            // SQL query to insert a new user
            string insertQuery = "INSERT INTO Users (Username, Email, PasswordHash, Role) VALUES (@Username, @Email, @PasswordHash, 'User')";

            try
            {
                con.Open();

                // Check for existing username or email
                using (SqlCommand checkCmd = new SqlCommand(checkQuery, con))
                {
                    checkCmd.Parameters.AddWithValue("@Username", username);
                    checkCmd.Parameters.AddWithValue("@Email", email);

                    int existingCount = (int)checkCmd.ExecuteScalar();

                    if (existingCount > 0)
                    {
                        MessageBox.Show("Username or Email already exists. Please choose another.");
                        return;
                    }
                }

                // Insert the new user
                using (SqlCommand insertCmd = new SqlCommand(insertQuery, con))
                {
                    insertCmd.Parameters.AddWithValue("@Username", username);
                    insertCmd.Parameters.AddWithValue("@Email", email);
                    insertCmd.Parameters.AddWithValue("@PasswordHash", password); // Hash the password in real applications

                    int result = insertCmd.ExecuteNonQuery();

                    if (result > 0)
                    {
                        MessageBox.Show("Registration successful!");

                        // Redirect to the login form
                        Form2 loginForm = new Form2();
                        loginForm.Show();
                        this.Hide();
                    }
                    else
                    {
                        MessageBox.Show("Registration failed.");
                    }
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Database error: " + ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
            finally
            {
                con.Close();
            }
        
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form2 loginForm = new Form2();

            // Show the login form
            loginForm.Show();

            // Optionally hide the current form (Form1)
            this.Hide();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click_1(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            isPasswordVisible = !isPasswordVisible; // Toggle visibility
            pictureBox3.Refresh();
            if (isPasswordVisible)
            {
                textBox3.PasswordChar = '\0'; // Show password
                pictureBox3.Image = Properties.Resources.eye; // Set to open eye image
            }
            else
            {
                textBox3.PasswordChar = '*'; // Hide password
                pictureBox3.Image = Properties.Resources.close_eye; // Set to closed eye image
            }
            pictureBox3.Refresh();
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {

        }
    }
}
